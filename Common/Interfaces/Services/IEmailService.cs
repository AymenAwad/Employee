namespace Shared.Interfaces.Services

{
    using Shared.DTOs.Email;
    using System.Threading.Tasks;

    public interface IEmailService
    {
        Task SendAsync(EmailRequest request);
    }
}
