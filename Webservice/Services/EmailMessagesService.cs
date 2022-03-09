using System.Text;
using System.Web;
using Webservice.Helper;

namespace Webservice.Services
{
    public static class EmailMessagesService
    {
        public static string CreateContactFormMailMessage(Mail mail)
        {
            return $"<h3>Kontaktformular wurde ausgefüllt von: \"{HttpUtility.HtmlEncode(mail.Name)}\"</h3>" +
                          $"<p>Kontaktgrund: <b>{HttpUtility.HtmlEncode(mail.Subject)}</b></p>" +
                          $"<p>Email: <a href='{HttpUtility.HtmlEncode(mail.SenderAddress)}'></a>{HttpUtility.HtmlEncode(mail.SenderAddress)}</p><p><b>Nachricht:</b></p>" +
                          $"<p>{GetText(mail.Message)}</p>" +
                          "<br><br><small>Diese E - Mail wurde automatisch generiert, bitte nicht auf diese E - Mail Antworten </small>";
        }

        public static string CreateTestMailMessage()
        {
            return $"<h2>E-Mail Test vom: {DateTime.Now:dd.MMMM} </h2><p>Automatischer E-Mail Test vom: " +
                   $"{DateTime.Now:dd.MM.yyyy HH:mm:ss} </p></br><p>Liebe Gruss</p><p>Patrick</p>" +
                   "<br><br><small>Diese E - Mail wurde automatisch generiert, bitte nicht auf diese E - Mail Antworten </small>";
        }

        private static string GetText(string message)
        {
            StringBuilder builder = new StringBuilder();
            string[] lines = message.Split('\n');
            for (int i = 0; i < lines.Length; i++)
            {
                if (i > 0)
                    builder.Append("<br/>\n");
                builder.Append(HttpUtility.HtmlEncode(lines[i]));
            }
            return builder.ToString();
        }
    }
}
