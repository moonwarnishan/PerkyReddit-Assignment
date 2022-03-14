using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Mvc;
using PerkyRadditEmailSender.Models;

namespace PerkyRadditEmailSender.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration config;
        public HomeController(IConfiguration _config)
        {
            config = _config;
        }

        public IActionResult SentResult()
        {
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]

        
        public IActionResult SendingMail(SendingDetails model)
        {
            try
            {
                string smtpServer = config.GetSection("EmailConfiguration").GetSection("SmtpServer").Value;
                int smtpPort = Convert.ToInt16(config.GetSection("EmailConfiguration").GetSection("SmtpPort").Value);
                string smtpUsername = config.GetSection("EmailConfiguration").GetSection("SmtpUsername").Value;
                string smtpPassword = config.GetSection("EmailConfiguration").GetSection("SmtpPassword").Value;
                string name = config.GetSection("EmailConfiguration").GetSection("Name").Value;
                var message = new MailMessage();
                message.From = new MailAddress("noreply@your-domain-name.com", name);
                message.Subject = model.Subject;
                message.IsBodyHtml = true;
                message.Body = model.Message;
                message.To.Add(new MailAddress(model.To));
                message.Bcc.Add(new MailAddress(model.Bcc));
                message.CC.Add(new MailAddress(model.CC));
                using (var client = new SmtpClient())
                {
                    client.UseDefaultCredentials = false;
                    client.Host = smtpServer;
                    client.Port = smtpPort;
                    client.Credentials = new NetworkCredential()
                    {
                        UserName = smtpUsername,
                        Password = smtpPassword
                    };
                    client.EnableSsl = true;
                    client.Send(message);
                }
                return RedirectToAction("SentResult");
            }
            catch (Exception e)
            {
                return StatusCode(404);
            }
        }
    }
}