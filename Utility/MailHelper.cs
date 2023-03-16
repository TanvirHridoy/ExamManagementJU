using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Net.Mail;

namespace EMSJu.Utility
{
    public class MailHelper
    {
        public IConfiguration _config;
        public MailHelper(IConfiguration configuration)
        {
            _config = configuration;
        }
        public bool SendMail2(string ToMailAddr, string ToName, string Subject, string Message)
        {
            try
            {
                MailAccountInfo accountInfo = new MailAccountInfo();
                _config.GetSection("EmailAccount").Bind(accountInfo);
                var fromAddress = new MailAddress(accountInfo.EmailAddress, accountInfo.FromName);
                var toAddress = new MailAddress(ToMailAddr, ToName);
                string fromPassword = accountInfo.Password;
                string subject = "New Certificate Application Alert";
                string body = "Hello your have a New certificate Application Please Check" + Message;

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    UseDefaultCredentials = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                    Timeout = 20000
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                }
                return true;
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return false;
            }

        }

        public void SendEmailToDept(string Dept, string TrackingID)
        {
            MailAccountInfo accountInfo = new MailAccountInfo();
            _config.GetSection("EmailAccount").Bind(accountInfo);
            try
            {
                var sectionsemail = _config.GetValue<string>($"SectionEmails:{Dept}");
                MailMessage m = new System.Net.Mail.MailMessage(
                        new System.Net.Mail.MailAddress(accountInfo.EmailAddress, accountInfo.FromName),
                        new System.Net.Mail.MailAddress(sectionsemail));
                m.Subject = "New Application In Certification RMS";
                m.Body = string.Format($"Dear  <BR/>There is an new Certificate application Pending for you. <br/>Student Id: {TrackingID}");
                m.IsBodyHtml = true;

                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.gmail.com");
                smtp.Credentials = new System.Net.NetworkCredential(accountInfo.EmailAddress, accountInfo.Password);
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com";
                smtp.Send(m);
            }
            catch (Exception Ex)
            {
                var msg = Ex.Message;
            }

        }

        public void SendEmailApproved(string StudentEmail, string Date, string TrackingID)
        {
            MailAccountInfo accountInfo = new MailAccountInfo();
            _config.GetSection("EmailAccount").Bind(accountInfo);
            try
            {
                var sectionsemail = StudentEmail;
                MailMessage m = new System.Net.Mail.MailMessage(
                        new System.Net.Mail.MailAddress(accountInfo.EmailAddress, accountInfo.FromName),
                        new System.Net.Mail.MailAddress(sectionsemail));
                m.Subject = "Application Approved -Certification RMS-NUB";
                m.Body = string.Format($"Dear Student <BR/>Your Application with TrackingID {TrackingID} for Certificate has been approved.Please collect your certificate from Examination Section On or After {Date} between 10am to 4pm <br/>");
                m.IsBodyHtml = true;

                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.gmail.com");
                smtp.Credentials = new System.Net.NetworkCredential(accountInfo.EmailAddress, accountInfo.Password);
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com";
                smtp.Send(m);
            }
            catch (Exception Ex)
            {
                var msg = Ex.Message;
            }

        }
        public void SendEmailRejection(string StudentEmail,string RejectedFrom,string TrackingID)
        {
            MailAccountInfo accountInfo = new MailAccountInfo();
            _config.GetSection("EmailAccount").Bind(accountInfo);
            try
            {
                var sectionsemail = StudentEmail;
                MailMessage m = new System.Net.Mail.MailMessage(
                        new System.Net.Mail.MailAddress(accountInfo.EmailAddress, accountInfo.FromName),
                        new System.Net.Mail.MailAddress(sectionsemail));
                m.Subject = "Application Rejected Certification RMS-NUB";
                m.Body = string.Format($"Dear Student <BR/>Your Application with TrackingID {TrackingID} for Certificate has been rejected from {RejectedFrom} Section. Contact with mentioned section for more information <br/>");
                m.IsBodyHtml = true;

                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.gmail.com");
                smtp.Credentials = new System.Net.NetworkCredential(accountInfo.EmailAddress, accountInfo.Password);
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com";
                smtp.Send(m);
            }
            catch (Exception Ex)
            {
                var msg = Ex.Message;
            }

        }
        public void SendEmail(string ToMailAddr, string ToName, string Subject, string Message)
        {
            MailAccountInfo accountInfo = new MailAccountInfo();
            _config.GetSection("EmailAccount").Bind(accountInfo);
            try
            {
                MailMessage m = new System.Net.Mail.MailMessage(
                        new System.Net.Mail.MailAddress(accountInfo.EmailAddress, accountInfo.FromName),
                        new System.Net.Mail.MailAddress(ToMailAddr));
                m.Subject = "Email confirmation";
                m.Body = string.Format("Dear  <BR/>There is an new Certificate application Pending for you.");
                m.IsBodyHtml = true;

                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.gmail.com");
                smtp.Credentials = new System.Net.NetworkCredential(accountInfo.EmailAddress, accountInfo.Password);
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com";
                smtp.Send(m);
            }
            catch (Exception Ex)
            {
                var msg = Ex.Message;
            }

        }

    }
    public class MailAccountInfo
    {
        public string EmailAddress { get; set; }
        public string FromName { get; set; }
        public string Password { get; set; }
    }


}
