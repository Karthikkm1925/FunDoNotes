using FunDoNotes.BusinessLogic.Interfaces;
using Microsoft.Extensions.Configuration;
using FunDoNotes.Model.DTOs.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace FunDoNotes.BusinessLogic.Services
{
   
        public class EmailService : IEmailService
        {
            private readonly IConfiguration _configuration;

            public EmailService(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            public void SendEmail(SendEmailDto dto)
            {
                var smtpSettings = _configuration.GetSection("Smtp");

                var message = new MailMessage
                {
                    From = new MailAddress(smtpSettings["FromEmail"]!),
                    Subject = dto.Subject,
                    Body = dto.Body,
                    IsBodyHtml = true
                };

                message.To.Add(dto.ToEmail);

                var client = new SmtpClient
                {
                    Host = smtpSettings["Host"]!,
                    Port = int.Parse(smtpSettings["Port"]!),
                    EnableSsl = true,
                    Credentials = new NetworkCredential(
                        smtpSettings["Username"],
                        smtpSettings["Password"]
                    )
                };

                client.Send(message);
            }
        }
    }

