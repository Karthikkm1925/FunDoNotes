using FunDoNotes.Model.DTOs.Email;

namespace FunDoNotes.BusinessLogic.Interfaces
{
    public interface IEmailService
    {
        void SendEmail(SendEmailDto dto);
    }
}
