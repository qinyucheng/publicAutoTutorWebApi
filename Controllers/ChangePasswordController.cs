using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using System.IO;
using System.Configuration;  //read web.config xml files
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Text;
using System.Xml;
using System.Net.Mail;
using System.Web.Security;
using System.Security.Cryptography;
using publicAutoTutorWebApi.Models;

namespace publicAutoTutorWebApi.Controllers
{
    public class ChangePasswordController : ApiController
    {
        Models.OprationMongo opm = new OprationMongo();
        string strconn = ConfigurationManager.AppSettings["connectionString"];
        public bool put(Users userinfo ) {
            string result="";
            string password=Membership.GeneratePassword(6, 2);
            string MD5pwd=MD5Hash(password);
            userinfo.Password = MD5pwd;
            //changePassword(userinfo);
            if(changePassword(userinfo)){
               result= sendGmail(userinfo.Email, password);
            }
            if (result == "Mail has been successfully sent!")
            {
                return true;
            }
            else
                return false;
        }




        public string sendGmail(string userEmail, string value)
        {
             #region email
            string emailFromAddress ="AutoTutorMem@gmail.com";
            string emailPassword = "TNMemFIT410";
            string emailToAddress1 = userEmail;
          
            string emailSubject = "AutoTutor--Find your password";
            string emailBody = "You new password is <b>" + value + "</b> Please change to new password ASAP! <br/>Do not reply this Email";
            #endregion
            try
            {
                MailMessage myMail = new MailMessage();
                myMail.From = new MailAddress(emailFromAddress);
                myMail.To.Add(new MailAddress(emailToAddress1));
               
                myMail.Subject = emailSubject;
                myMail.SubjectEncoding = Encoding.UTF8;

                myMail.Body = emailBody;
                myMail.BodyEncoding = Encoding.UTF8;
                myMail.IsBodyHtml = true;
                
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;                    
                smtp.Credentials = new NetworkCredential(emailFromAddress, emailPassword);
                smtp.EnableSsl = true;             
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network; 
                //smtp.DeliveryFormat=HtmlString
                smtp.Send(myMail);
                return "Mail has been successfully sent!";
            }
            catch (Exception err) 
            {
                return "false";
            }
        
            /*
            string reciverEmail = "qinyucheng711@gmail.com";
            //string reciverEmail = ConfigurationManager.AppSettings["sendEmail"];
            //string content ="' send a new message from AutoTutor Website\n\n Email address: " + userEmail + "\n\n Message content: " + value;
            string content = "test";
            MailMessage msg = new MailMessage();

            msg.From = new MailAddress(reciverEmail);
            msg.To.Add(reciverEmail);
            msg.Subject = "From AutoTutor Website! " + DateTime.Now.ToString();
            msg.Body = content;
            SmtpClient client = new SmtpClient();
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(reciverEmail, "TNMemFIT410");
            client.Timeout = 20000;
            try
            {
                client.Send(msg);
                return "Mail has been successfully sent!";
            }
            catch (Exception ex)
            {
                return "Fail Has error" + ex.Message;
            }
            finally
            {
                msg.Dispose();
            }*/
        }

        public bool changePassword(Users info) {
            opm.ConnDatabase(strconn);
            return opm.updateUserPassword(info);
        }

        public string MD5Hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

            //get hash result after compute it
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits
                //for each byte
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }
    }

}
