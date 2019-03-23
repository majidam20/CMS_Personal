using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Net.Configuration;

using System.IO;

namespace Business.Controls
{
    public static class ControlComments
    {
        #region Mehtods
        public static bool CommentsIsEnabled(string PageName)
        {
            bool blIsEnabled = System.Configuration.ConfigurationManager.AppSettings[PageName] != null ? Boolean.Parse(System.Configuration.ConfigurationManager.AppSettings[PageName]) : false;
            return blIsEnabled;
        }
        #endregion
    }

    public static class CommonMethods
    {
        /// <summary>
        /// Save uploaded file in defined path and filename
        /// </summary>
        /// <param name="BasePath">Save Path</param>
        /// <param name="PostedFile">Uploaded file</param>
        /// <param name="SaveFilename">Filename without Extention. Extention of file gets from Posted file</param>
        /// <returns>Complete Path and Filename with extention</returns>
        public static string UploadFile(string BasePath, HttpPostedFile PostedFile, string SaveFilename)
        {
            string temp = BasePath;
            if (!System.IO.Directory.Exists(temp))
            {
                System.IO.Directory.CreateDirectory(temp);
            }
            HttpPostedFile f;
            f = PostedFile;
            try
            {
                //if ((f.ContentLength / 1048576) < 1) 
                //{
                //    throw new System.IO.IOException(string.Format("حجم فایل بیش از مجاز است\\n{0}",(Math.Round(Convert.ToDecimal(f.ContentLength / 1024), 3)).ToString() + " KB"));
                //}
                string strFileName = SaveFilename + System.IO.Path.GetExtension(f.FileName);
                f.SaveAs(Path.Combine(temp, strFileName));

                return strFileName;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Delete file from host
        /// </summary>
        /// <param name="BasePath">Base path of file</param>
        /// <param name="FileName">Filename to delete</param>
        public static void DeleteFile(string BasePath, string FileName)
        {
            string strPath = Path.Combine(BasePath, FileName);
            if (!File.Exists(strPath))
                throw new System.IO.FileNotFoundException("File not found", strPath);
            try
            {
                System.IO.File.Delete(strPath);
            }
            catch (System.IO.IOException ex)
            {
                throw ex;
            }
        }

        public static void SendToAdminMailBox(string FromAddress,string FromName, string Subject, string BodyText)
        {
            System.Net.Mail.MailAddressCollection mailTo = new System.Net.Mail.MailAddressCollection();

            System.Configuration.Configuration config = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);
            MailSettingsSectionGroup settings = (MailSettingsSectionGroup)config.GetSectionGroup("system.net/mailSettings");

            if (System.Configuration.ConfigurationManager.AppSettings["ToAddress"] != null)
                mailTo.Add(System.Configuration.ConfigurationManager.AppSettings["ToAddress"]);
            else
                throw new Exception("To Address is empty. Set it in Web.Config");

            if (settings == null)
                throw new Exception("SMTP not set in Web.Config");


            System.Net.Mail.MailAddress mailFrom = new System.Net.Mail.MailAddress(FromAddress, FromName);
            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient(settings.Smtp.Network.Host, settings.Smtp.Network.Port);
            System.Net.NetworkCredential netCred = new System.Net.NetworkCredential(settings.Smtp.Network.UserName, settings.Smtp.Network.Password);

            try
            {
                SendMail(mailTo, mailFrom, String.Format("(Website){0}", Subject), BodyText, true, smtp, netCred);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        private static void SendMail(System.Net.Mail.MailAddressCollection ToList,
                             System.Net.Mail.MailAddress FromAddress,
                             string Subject,
                             string BodyText,
                             bool IsHTML,
                             System.Net.Mail.SmtpClient SMTP,
                             System.Net.NetworkCredential Credential)
        {
            try
            {
                System.Net.Mail.MailMessage Mail_ = new System.Net.Mail.MailMessage();
                Mail_.BodyEncoding = System.Text.Encoding.UTF8;
                foreach (System.Net.Mail.MailAddress mail in ToList)
                    Mail_.To.Add(mail);

                Mail_.From = FromAddress;
                Mail_.Subject = Subject;
                Mail_.HeadersEncoding = System.Text.Encoding.UTF8;
                Mail_.SubjectEncoding = System.Text.Encoding.UTF8;
                Mail_.BodyEncoding = System.Text.Encoding.UTF8;
                Mail_.Body = BodyText;
                Mail_.IsBodyHtml = IsHTML;
                System.Net.Mail.SmtpClient SMTP_S = SMTP;
                SMTP_S.Credentials = Credential;
                
                //SMTP_S.Send(Mail_);
            }
            catch (FormatException ex)
            {
                throw ex;
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}