using System;
using System.Net.Mail;
using MailTester.Model;

namespace MailTester
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Please enter email address on which you want to send mail.");
            string command;
            while ((command= Console.ReadLine()) != "exit")
            {
                if (IsValid(command))
                {
                    SendEmail(command,"testMail","This is a test mail to check mail configuration");
                }
                else
                {
                    Console.WriteLine("Please enter valid email address.");
                }
            }
        }
        public static bool IsValid(string emailaddress)
        {
            try
            {
                var m = new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
        public static bool SendEmail(string toAddress, string subject, string body)
        {
            toAddress = toAddress.Trim();

            if (!EmailConfigProvider.Instance.IsEnabled)
            {
                Console.WriteLine("Mail is not enabled from config");
                return false;
            }

            try
            {
                if (string.IsNullOrEmpty(toAddress))
                    throw new Exception($"Invalid Email address : {toAddress}");

                var message = new MailMessage
                {
                    From = new MailAddress(EmailConfigProvider.Instance.From, EmailConfigProvider.Instance.DisplayName)
                };

                message.ReplyToList.Add(new MailAddress(EmailConfigProvider.Instance.ReplyTo));

                message.To.Add(new MailAddress(toAddress));
                message.Subject = subject;
                message.BodyEncoding = System.Text.Encoding.UTF8;
                message.Body = body;
                message.IsBodyHtml = true;

                //NOTE: Do not use object initializer
                var client = new SmtpClient(EmailConfigProvider.Instance.MailHost, EmailConfigProvider.Instance.Port);

                client.Host = EmailConfigProvider.Instance.MailHost;
                client.Port = EmailConfigProvider.Instance.Port;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.EnableSsl = EmailConfigProvider.Instance.IsSecureSocket;
                client.UseDefaultCredentials = false;
                client.Timeout = 10000;
                client.Credentials = new System.Net.NetworkCredential(EmailConfigProvider.Instance.User,
                    EmailConfigProvider.Instance.Password);
                client.Send(message);
                Console.WriteLine("Mail sent successfully");
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
