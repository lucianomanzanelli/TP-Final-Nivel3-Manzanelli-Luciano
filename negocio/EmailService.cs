using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class EmailService
    {
        private MailMessage email;
        private SmtpClient server;

        //con gmail
        public EmailService()
        {
            server = new SmtpClient();
            server.Credentials = new NetworkCredential("lvmanzanelli@gmail.com", "idkk rrrt kumz btkl");
            server.EnableSsl = true;
            server.Port = 587;
            server.Host = "smtp.gmail.com";
        }

        //con mailtrap
        //public EmailService()
        //{
        //    server = new SmtpClient();
        //    server.Credentials = new NetworkCredential("0dbeb265cd9819", "56611a38932449");
        //    server.EnableSsl = true;
        //    server.Port = 2525;
        //    server.Host = "sandbox.smtp.mailtrap.io";
        //}

        public void armarCorreo(string emailDestino, string asunto, string cuerpo)
        {
            email = new MailMessage();
            email.From = new MailAddress("webstore@account.com");
            email.To.Add(emailDestino);
            email.Subject = asunto;
            //email.IsBodyHtml = true;
            //email.Body = "<h1>Reporte de materias a las que se ha inscripto</h1> <br>Hola, te inscribiste a ...";
            email.Body = cuerpo;
        }

        public void enviarEmail()
        {
            try
            {
                server.Send(email);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
