using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace SMTPExample
{
    class Program
    {
        /// <summary>
        /// Biar bisa jalan lu harus nyalain less secure apps di mail google lu, https://support.google.com/accounts/answer/6010255?hl=en
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //Server smtp
            //Seting ini harusnya ada di appsettings.json
            var server = "smtp.gmail.com"; //Dalam contoh ini pakai smtpnya google
            var port = 587; // ssl port smtp google 

            //Initialize class buat smtpclient, kalo di project kan yang di-inject ke constructor
            var client = new SmtpClient(server, port);
            client.EnableSsl = true; //Buat google biasanya harus enable ssl

            //Ini gw gak tau, cuma harus dipake biar bisa jalan di google mail server
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;

            //User credential email lu
            //Seting ini harusnya ada di appsettings.json
            var senderEmail = "pengirim@gmail.com"; //Email lu
            var password = "masukin password lu"; //password email lu

            //Masukin credential,harusnya di project krn dah di setting di startup gak usah lagi
            var credential = new NetworkCredential();
            credential.UserName = senderEmail;
            credential.Password = password;
            client.Credentials = credential;

            //Set sender info header(yang akan ditampilkan di email), receiver info, body, dll
            //pengirim
            var sender = new MailAddress("pengirim@gmail.com", "pengirim"); //address email pengirim, Nama lu yang mo ditampilkan(opsional)
            //penerima
            var receiver = new MailAddress("penerima@gmail.com"); //address email penerima

            //Isi subject, dan body email
            var message = new MailMessage(sender, receiver); //Isi parameter pengirim, dan penerimanya
            message.IsBodyHtml = true; //biar bisa bodynya baca tag html
            message.Subject = "Ini judul"; //Subject email
            message.Body = "<html><b>Test body email</b></html>"; //Ini body emailnya, bisa pake html biar bagus

            client.Send(message);
        }
    }
}
