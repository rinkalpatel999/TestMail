using MailTester.Utility;

namespace MailTester.Model
{
    public class EmailConfig
    {
        public EmailConfig()
        {
            //MailHost = "smtp.gmail.com";
            //Port = 587;
            //User = "";
            //Password = "";
            //From = "";
            //DisplayName = "Library";
            //ReplyTo = "";
            //IsSecureSocket = true;
            //IsEnabled = true;
        }

        public string MailHost { get; set; }
        public int Port { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string From { get; set; }
        public string DisplayName { get; set; }
        public string ReplyTo { get; set; }
        public bool IsSecureSocket { get; set; }
        public bool IsEnabled { get; set; }
        public void Load()
        {
            var config = EmailConfigProvider.Instance;
            MailHost = config.MailHost;
            Port = config.Port;
            User = config.User;
            Password = config.Password;
            From = config.From;
            DisplayName = config.DisplayName;
            ReplyTo = config.ReplyTo;
            IsSecureSocket = config.IsSecureSocket;
            IsEnabled = config.IsEnabled;
        }
        public void Save()
        {
            Json2Obj<EmailConfig>.Save(this);
        }

        public override string ToString()
        {
            return
                $"Host:{MailHost}, Port:{Port}, User:{User}, Password: {Password}, From{From}, DisplayName:{DisplayName}, ReplyTo:{ReplyTo}, SSL:{IsSecureSocket}";
        }
    }
}
