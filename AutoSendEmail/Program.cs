using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Runtime.InteropServices;

namespace AutoSendEmail
{
    class Program
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();


        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);


        const int SW_HIDE = 0;
        const int SW_SHOW = 5;

        static void Main(string[] args)
        {
            var handle = GetConsoleWindow();
            // Hide
            ShowWindow(handle, SW_HIDE);

            // Show
            //ShowWindow(handle, SW_SHOW);

            if (SendEmail("New login in: " + System.Environment.MachineName))
            {
                Console.Write("success!");
                return;
            }
            Console.Write("failed!");
        }
        public static bool SendEmail(string message)
        {
            try
            {
                string timenow = DateTime.Now.ToString();

                MailMessage messSend = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                messSend.From = new MailAddress("from_email");
                messSend.To.Add(new MailAddress("to_email"));
                messSend.Subject = "New login in: " + System.Environment.MachineName + " at: " + timenow;
                messSend.Body = message;
                messSend.IsBodyHtml = false;
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com"; //for gmail host
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("from_email", "password");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Timeout = 20000;
                smtp.Send(messSend);
                return true;
            }
            catch (Exception) { }
            return false;
        }
    }
}
