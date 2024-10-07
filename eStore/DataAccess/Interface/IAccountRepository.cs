using eStore.Models;
using Microsoft.AspNetCore.Identity;

namespace eStore.DataAccess.Interface
{
    public interface IAccountRepository
    {
        public Task<IdentityResult> SignUpAsync(SignUpModel model);
        public Task<string> SignInAsync(SignInModel model);
        Task<ApplicationUser> GetUserAsync(SignInModel model);
        Task<ApplicationUser> GetUserByUserIdAsync(string userId);
        /*public Task<bool> VerifyEmail();*/
    }
}
