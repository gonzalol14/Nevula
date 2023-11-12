using NevulaForo.Models.ViewModels;

namespace NevulaForo.Services.Contract
{
    public interface IEmailSenderService
    {
        Task SendEmailAsync(MailRequestVM request);
    }
}
