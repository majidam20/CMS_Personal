using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Business
{
    public class Contacts
    {
        #region Properties
        public List<DataAccess.Contacts> ContactsData
        { get; set; }
        #endregion

        #region Constracors
        public Contacts(Business.Comments.CommentPlaceType PlaceType, int PlaceID)
        {
            DataAccess.TeraByteCMSEntities dc = new DataAccess.TeraByteCMSEntities();
            ContactsData = (from contact in dc.Contacts
                            orderby contact.SentDate descending
                            select contact).ToList<DataAccess.Contacts>();
        }
        #endregion

        #region Methods
        public static void InsertContact(string Subject, string SenderMail, string SenderName, string Text, DateTime SentDate)
        {
            DataAccess.TeraByteCMSEntities dc = new DataAccess.TeraByteCMSEntities();
            DataAccess.Contacts cont = new DataAccess.Contacts();

            cont.Subject = Subject;
            cont.e_Mail = SenderMail;
            cont.SenderName = SenderName;
            cont.MessageText = Text;
            cont.SentDate = SentDate;
            cont.Readed = false;
            cont.WhoAnswer = null;

            dc.AddToContacts(cont);
            dc.SaveChanges();
        }
        #endregion
    }
}