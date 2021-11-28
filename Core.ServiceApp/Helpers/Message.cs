using Microsoft.AspNetCore.Http;
using MimeKit;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;


namespace Core.ServiceApp.Helpers
{
    public class Message
    {
        public List<MailboxAddress> To { get; set; }
        public List<MailboxAddress> CC { get; set; }
        public List<MailboxAddress> BCC { get; set; }
        // public List<Attachment> Attachments { get; set; }
        public IFormFileCollection Attachments { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        public Message()
        {

        }

        public Message(IEnumerable<string> to, string subject, string content)
        {
            To = new List<MailboxAddress>();
            To.AddRange(to.Select(x => new MailboxAddress(x)));

            Subject = subject;
            Body = content;
        }

        // public Message(IEnumerable<string> to, string subject, string content, List<Attachment> attC)
        public Message(IEnumerable<string> to, string subject, string content, IFormFileCollection attC)
        {
            To = new List<MailboxAddress>();
            To.AddRange(to.Select(x => new MailboxAddress(x)));
            Attachments = attC;
            Subject = subject;
            Body = content;
        }

        public Message(IEnumerable<string> to, IEnumerable<string> cc, IEnumerable<string> bcc, string subject, string content)
        {
            To = new List<MailboxAddress>();
            To.AddRange(to.Select(x => new MailboxAddress(x)));

            CC = new List<MailboxAddress>();
            CC.AddRange(cc.Select(x => new MailboxAddress(x)));

            BCC = new List<MailboxAddress>();
            BCC.AddRange(bcc.Select(x => new MailboxAddress(x)));

            Subject = subject;
            Body = content;
        }

        public Message(IEnumerable<string> to, IEnumerable<string> cc, IEnumerable<string> bcc, string subject, string content, IFormFileCollection attC)
        {
            To = new List<MailboxAddress>();
            To.AddRange(to.Select(x => new MailboxAddress(x)));

            CC = new List<MailboxAddress>();
            CC.AddRange(cc.Select(x => new MailboxAddress(x)));

            BCC = new List<MailboxAddress>();
            BCC.AddRange(bcc.Select(x => new MailboxAddress(x)));

            Attachments = attC;
            Subject = subject;
            Body = content;
        }

    }

    public class MessageRequest
    {

        public List<string> To { get; set; }
        //public List<string> CC { get; set; }
        //public List<string> BCC { get; set; }
        //public string Subject { get; set; }
        //public string Body { get; set; }
    }

    public class AttachRequest
    {
        public string To { get; set; }
        public string CC { get; set; }
        public string BCC { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
