using eStore.Helpers;

namespace eStore.DataAccess.Interface
{
    public interface ISendMailServiceRepository
    {
        Task SendMail(MailContent mailContent);

        Task SendEmailAsync(string email, string subject, string htmlMessage);
        string RandomOTP();
    }
}
