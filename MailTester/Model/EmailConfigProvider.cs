using System;
using MailTester.Utility;

namespace MailTester.Model
{
    public class EmailConfigProvider
    {
        private static readonly Lazy<EmailConfig> Lazy = new Lazy<EmailConfig>(Json2Obj<EmailConfig>.Load);

        public static EmailConfig Instance => Lazy.Value;

        public EmailConfigProvider()
        {
        }
    }
}
