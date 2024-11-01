using eStore.DataAccess.Interface;
using eStore.Helpers;
using eStore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using NuGet.Versioning;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace eStore.DataAccess.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly eStoreContext context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IConfiguration configuration;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ISendMailServiceRepository sendMailServiceRepository;
        private readonly IShoppingCartRepository shoppingCartRepository;
        string otpCode;

        public AccountRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration, RoleManager<IdentityRole> roleManager, ISendMailServiceRepository sendMailServiceRepository, eStoreContext context, IShoppingCartRepository shoppingCartRepository)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
            this.roleManager = roleManager;
            this.sendMailServiceRepository = sendMailServiceRepository;
            this.shoppingCartRepository = shoppingCartRepository;
            this.otpCode = sendMailServiceRepository.RandomOTP();
            this.context = context;
        }
        public async Task<string> SignInAsync(SignInModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            var passwordValid = await userManager.CheckPasswordAsync(user, model.Password);
            if (user == null || !passwordValid)
            {
                return string.Empty;
            }

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, model.Email),
                new Claim("email", model.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var userRoles = await userManager.GetRolesAsync(user);
            foreach(var role in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role.ToString()));
                authClaims.Add(new Claim("role", role.ToString()));
            }
            var authenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));
            var token = new JwtSecurityToken(
                issuer: configuration["JWT:ValidIssuer"],
                audience: configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(30),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authenKey, SecurityAlgorithms.Aes192CbcHmacSha384)
                );
            
            Console.WriteLine(token);
            var userJson = JsonConvert.SerializeObject(user);
            return new JwtSecurityTokenHandler().WriteToken(token) + "~" + userJson;
            /*return new JwtSecurityTokenHandler().WriteToken(token);*/
        }

        public async Task<IdentityResult> SignUpAsync(SignUpModel model)
        {

            /*MailContent content = new MailContent
            {
                To = "choyvinhthien0209@gmail.com",
                Subject = "OTP Code",
                Body = "<p><strong>"+otpCode+"</strong></p>"
            };

            await sendMailServiceRepository.SendMail(content);
            DateTime? created = DateTime.Now;
            DateTime? expired = DateTime.Now.AddMinutes(1);
            while(created.Value > expired.Value)
            {
                created = DateTime.Now;
                if()
            }*/
            var user = new ApplicationUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Email,
                Address = model.Address,
                Gender = model.Gender,
                PhoneNumber = model.PhoneNumber,
                Status = 1
            };

            string role = model.RoleName;

            if (role == "RentalProvider")
            {
                user.Bank = model.Bank;
                user.BankAccountNumber = model.BankAccountNumber;
                user.Status = 0;
            }

            var result = await userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {

                if (role == AppRole.Customer || role == "RentalProvider")
                {
                    await userManager.AddToRoleAsync(user, role);
                }
                else
                {
                    throw new ArgumentException("Role is not valid.");
                }
            }
            return result;
        }
        public async Task<ApplicationUser> GetUserAsync(SignInModel model)
        {
            return await userManager.FindByEmailAsync(model.Email);
        }
        public async Task<ApplicationUser> GetUserByUserIdAsync(string userId)
        {
            return await userManager.FindByIdAsync(userId);
        }
        /*public async Task<bool> VerifyEmail(OTP)
        {

            return true;
        }*/
    }
}
