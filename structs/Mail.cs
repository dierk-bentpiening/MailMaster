using System.Collections.Generic;
using System.Net.Mail;

namespace MailMaster.structs
{
    public struct Mail
    {
        public MailAddress FromEmail { get; set; }
        public string SubjectEmail{ get; set; }
        public string BodyEmail{ get; set; }
        public List<Recipient> RecipientEmail;
        public List<Recipient> RecipientsCC;

    }
}