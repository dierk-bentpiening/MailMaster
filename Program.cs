using System.Net.Mail;
using System.Net.Mime;
using System.Threading;
using MailMaster.structs;
using MailMaster;

namespace MailMaster
{
   
    internal class Program
    {
        private static System.Net.Mail.Attachment _data;
        private static bool _withAttachment = false;
        private static bool _withCC = false;
        private static Mail mail;
        private static Logging logger;

        public static void Main(string[] args)
        {

            logger = new Logging();
            logger.LogInfo("Application started");
            GlobalAppInfo appInfo = new GlobalAppInfo();
            Console.WriteLine(appInfo.GetAppInfoMessage());
            appInfo.ArgStructure =
                "<required: host:port> <required: email_address_sender> <required: password> <required: email_address_recipient> <required: email_subject> <required: email_body> <optional: path_attachment> <optional: email_recipient_cc>";
            if (args.Length < 5)
            {
                Console.WriteLine(appInfo.GetArgMissmatchErrorMessage(args.Length, 5));
                System.Environment.Exit(2);
            }

            mail = new Mail();
            mail.FromEmail = new MailAddress(args[1]);
            string[] recipient = args[3].Split(',');
            if (recipient.Length > 1)
            {
                var counter = 0;
                List<Recipient> recipients = new List<Recipient>();
                foreach (string address in recipient)
                {
                    if (counter == 0)
                    {
                        recipients.Add(new Recipient {EmailAddress = new MailAddress(address)});
                        mail.RecipientEmail = recipients;
                    }
                    else
                    {
                        mail.RecipientEmail.Add(new Recipient {EmailAddress = new MailAddress(address)});
                    }

                    counter++;
                }
            }
            else
            {
                List<Recipient> recipients = new List<Recipient>();
                recipients.Add(new Recipient {EmailAddress = new MailAddress(recipient[0])});
                mail.RecipientEmail = recipients;
            }

            mail.SubjectEmail = args[4];
            mail.BodyEmail = args[5];
            
            if (args.Length >= 6)
            {
                int indexAttachment = 0;
                int indexCC = 0;
                try
                {
                    if (args[6].Contains("cc:"))
                    {
                        indexCC = 6;
                        _withCC = true;
                    }
                    else if (args[7].Contains("cc:"))
                    {
                        indexCC = 7;
                        _withCC = true;
                    }

                }
                catch (IndexOutOfRangeException e)
                {
                    logger.LogSilent("Index out of range Command Line Interface: Command: (CC)");
                }
                
                if (indexCC > 0)
                {
                    string[] _recipients = args[indexCC].Split(':');
                    string[] recipientsCc = _recipients[1].Split(',');
                    List<Recipient> recipientsCC = new List<Recipient>();
                    if (recipientsCc.Length > 0)
                    {

                        foreach (string recipientcc in recipientsCc)
                        {

                            recipientsCC.Add(new Recipient {EmailAddress = new MailAddress(recipientcc)});

                        }
                    }
                    else
                    {
                        recipientsCC.Add(new Recipient {EmailAddress = new MailAddress(recipientsCc[0])});
                    }

                    mail.RecipientsCC = recipientsCC;
                }
                try {
                if (args[6].Contains("attachment:"))
                {
                    indexAttachment = 6;
                    _withAttachment = true;
                } else if (args[7].Contains("attachment:"))
                    {
                        indexAttachment = 7;
                    }
                }
                catch (IndexOutOfRangeException e)
                {
                    logger.LogSilent("Index out of range Command Line Interface: Command: (Attachment)");
                    
                }
                

                if (indexAttachment > 0)
                {
                    string[] path = args[indexAttachment].Split(':');
                    _data = new System.Net.Mail.Attachment(path[1], MediaTypeNames.Application.Octet);
                    _withAttachment = true;
                }
            }
            WaitHandle[] waitHandles = new WaitHandle[mail.RecipientEmail.Count];
            int threadCounter = 0;
            foreach (Recipient mailaddress in mail.RecipientEmail)
            {
                var countHandle = threadCounter;
                var handle = new EventWaitHandle(false, EventResetMode.ManualReset);
                logger.LogInfo("Stating new Thread for Mail sending....");
                
                var thread = new Thread(() =>
                {
                    SendMail(mailaddress, args);
                    handle.Set();
                });
                
                threadCounter++;
                waitHandles[countHandle] = handle;
                thread.Start();
            }
            WaitHandle.WaitAll(waitHandles);
            
            
        }

        private static void SendMail(Recipient mailaddress, string[] args)
        {
            SmtpClient client = new SmtpClient(args[0]);
            client.EnableSsl = true;
            client.Credentials = new System.Net.NetworkCredential(args[1], args[2]);
            MailMessage message = new MailMessage(mail.FromEmail, mailaddress.EmailAddress);
            message.Subject = mail.SubjectEmail;
            message.Body = mail.BodyEmail;
            if (_withAttachment)
            {
                message.Attachments.Add(_data);
                logger.LogInfo($"Added {_data.Name} as Attachment.");
            }

            if (_withCC)
            {
                foreach (Recipient mailCc in mail.RecipientsCC)
                {
                    message.CC.Add(mailCc.EmailAddress);
                }
            }

            logger.LogInfo(
                $"Sending Message:\n    From {mail.FromEmail}\n    To: {mailaddress.EmailAddress}\n    Subject: {mail.SubjectEmail}\n    Body: {mail.BodyEmail}");
            try
            {
                client.Send(message);
            }
            catch (Exception error)
            {
                logger.LogError($"Fatal Error: Could not send Message!\nCause: {error}");
            }
            finally
            {
                logger.LogInfo($"Successfully send Message {mail.SubjectEmail} to {mailaddress.EmailAddress}");
            }
        }

    }
}