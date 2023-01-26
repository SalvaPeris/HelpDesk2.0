using System;
using System.Threading.Tasks;
using HelpDesk.Shared.Models;
using Microsoft.Extensions.Options;
using MailKit;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;

namespace HelpDesk.Server.Services.Mail
{
    public class EmailSender
    {
        private readonly IOptions<Configuracion> _configuracion;

        public EmailSender(IOptions<Configuracion> configuracion)
        {
            _configuracion = configuracion;
        }

        public async Task SendEmailAsync(string email, string titulo, string mensaje)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress(_configuracion.Value.NombreAMostrarSMTP, _configuracion.Value.NombreUsuarioSMTP));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = titulo;
            emailMessage.Body = new TextPart("html") { Text = mensaje };
            try
            {
                var client = new SmtpClient();
                await client.ConnectAsync(_configuracion.Value.NombreServidorSMTP, _configuracion.Value.PuertoSMTP, SecureSocketOptions.Auto);
                await client.AuthenticateAsync(_configuracion.Value.NombreUsuarioSMTP, _configuracion.Value.ContrasenaSMTP);

                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                var e = ex;
                throw;
            }

        }
    }
}

