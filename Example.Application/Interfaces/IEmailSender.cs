using System.Collections.Generic;

namespace Example.Application.Interfaces
{
    public interface IEmailSender
    {
        void Send(string email, string subject, string message, List<string> attachments = null!);
    }
}