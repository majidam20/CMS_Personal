using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Business.Comments
{
    public enum CommentPlaceType
    {
        ContactUS = 0,
        Pages = 1,
        Products = 2,
        News = 3,
        PhotoGallery = 4
    }

    public class Comments
    {
        #region Properties
        public List<CommentsData> CommentDatas
        { get; set; }
        #endregion

        #region Constractors
        public Comments()
        { }

        public Comments(CommentPlaceType PlaceType, int PlaceID)
        {
            DataAccess.TeraByteCMSEntities dc = new DataAccess.TeraByteCMSEntities();
            CommentDatas = (from comment in dc.Comments
                            where comment.PlaceNum.Equals((int)PlaceType) && comment.PlaceID.Equals(PlaceID) && comment.Visible.Equals(true)
                            orderby comment.SentDate descending
                            select new CommentsData
                            {
                                e_Mail = comment.e_Mail,
                                SenderName = comment.SenderName,
                                SendDate = comment.SentDate,
                                Comment = comment.Comment,
                                Culture = comment.SenderCulture
                            }).ToList<CommentsData>();
        }
        #endregion

        #region Methods
        public static void InsertComment(CommentPlaceType PlaceType, int PlaceID, string SenderMail, string SenderName, string Comment, DateTime SentDate, string Culture)
        {
            DataAccess.TeraByteCMSEntities dc = new DataAccess.TeraByteCMSEntities();
            DataAccess.Comments cmnts = new DataAccess.Comments();

            cmnts.PlaceNum = (int)PlaceType;
            cmnts.PlaceID = PlaceID;
            cmnts.e_Mail = SenderMail;
            cmnts.SenderName = SenderName;
            cmnts.Comment = Comment;
            cmnts.SentDate = SentDate;
            cmnts.SenderCulture = Culture;
            
            dc.AddToComments(cmnts);
            dc.SaveChanges();
        }
        public static void InsertComment(int PlaceType, int PlaceID, string SenderMail, string SenderName, string Comment, DateTime SentDate, string Culture)
        {
            CommentPlaceType c = CommentPlaceType.News;
            switch (PlaceType)
            {
                case 1: c = CommentPlaceType.Pages; break;
                case 2: c = CommentPlaceType.Products; break;
                case 3: c = CommentPlaceType.News; break;
                case 4: c = CommentPlaceType.PhotoGallery; break;
            }
            InsertComment(c, PlaceID, SenderMail, SenderName, Comment, SentDate, Culture);
        }
        public static void SetCommentVisible(int CommentID, bool Visible)
        {
            DataAccess.TeraByteCMSEntities dc = new DataAccess.TeraByteCMSEntities();
            DataAccess.Comments cmnt;
            cmnt = (from cmn in dc.Comments
                    where cmn.ID.Equals(CommentID)
                    select cmn).First<DataAccess.Comments>();
            cmnt.Visible = Visible;

            dc.SaveChanges();
        }
        public static void DeleteComment(int CommentID)
        {
            DataAccess.TeraByteCMSEntities dc = new DataAccess.TeraByteCMSEntities();
            DataAccess.Comments cmnt = (from cmn in dc.Comments
                                                where cmn.ID.Equals(CommentID)
                                                select cmn).First<DataAccess.Comments>();
            dc.DeleteObject(cmnt);
            dc.SaveChanges();
        }


        public List<Business.Comments.CommentsDataAdminView> GetAllCommnets(string Culture)
        {
            DataAccess.TeraByteCMSEntities dc = new DataAccess.TeraByteCMSEntities();
            List<Business.Comments.CommentsDataAdminView> comments;
            comments = (from comment in dc.Comments
                        where comment.SenderCulture.Equals(Culture) && comment.PlaceNum != (int)CommentPlaceType.ContactUS
                        select new CommentsDataAdminView
                            {
                                ID = comment.ID,
                                PlaceNum = comment.PlaceNum,
                                //PlaceNum = Business.Comments.Comments._PlaceNum2Name(comment.PlaceNum),
                                //PlaceID = Business.Comments.Comments._PlaceID2Name(comment.PlaceNum, comment.PlaceID),
                                PlaceID = comment.PlaceID,
                                e_Mail = comment.e_Mail,
                                SenderName = comment.SenderName,
                                Comment = comment.Comment,
                                SentDate = comment.SentDate,
                                Visible = comment.Visible,
                                Culture = comment.SenderCulture
                            }).ToList<Business.Comments.CommentsDataAdminView>();
            return comments;
        }

        public static string _PlaceNum2Name(int PlaceNum)
        {
            switch (PlaceNum)
            {
                case 1: return "صفحات";
                case 2: return "محصولات";
                case 3: return "اخبار";
                case 4: return "تور";
                default: return "صفحات";
            }
        }
        public static string _PlaceID2Name(int PlaceNum, int PlaceID)
        {
            DataAccess.TeraByteCMSEntities dc = new DataAccess.TeraByteCMSEntities();
            switch (PlaceNum)
            {
                case 1:
                    {
                        return (from page in dc.PagesData
                                where page.ID.Equals(PlaceID)
                                select page).First<DataAccess.PagesData>().Header;
                    }
                case 2:
                    {
                        var x = (from prod in dc.Products
                                 where prod.ID.Equals(PlaceID)
                                 select prod);
                        if (x.Count() > 0)
                            return x.First<DataAccess.Products>().Title;
                        else
                            return "محصول حذف شده";

                        //return (from prod in dc.Products
                        //        where prod.ID.Equals(PlaceID)
                        //        select prod).First<DataAccess.Products>().Title_FA;
                    }
                case 3: return "خبر";
                case 4:
                    {
                        return (from photo in dc.PhotoGallery
                                where photo.ID.Equals(PlaceID)
                                select photo).First<DataAccess.PhotoGallery>().Title;
                    }
                default: return "صفحات";
            }
        }
        #endregion
    }

    public class CommentsData
    {
        #region Properties
        public string e_Mail
        { get; set; }
        public string SenderName
        { get; set; }
        public string Comment
        { get; set; }
        public DateTime SendDate
        { get; set; }
        public string Culture
        { get; set; }
        #endregion
    }

    public class CommentsDataAdminView
    {
        #region Properties
        public int ID
        { get; set; }
        public int PlaceNum
        { get; set; }
        public int PlaceID
        { get; set; }
        public string e_Mail
        { get; set; }
        public string SenderName
        { get; set; }
        public string Comment
        { get; set; }
        public DateTime SentDate
        { get; set; }
        public string Culture
        { get; set; }
        public Boolean Visible
        { get; set; }
        #endregion
    }
}