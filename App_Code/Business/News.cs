using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Business.News
{
    public class AllNews
    {        
        #region Properties
        public List<DataAccess.News> NewsData { get; set; }
        #endregion

        #region Constractors
        public AllNews(){ }
        public AllNews(string Culture)
        {
            DataAccess.TeraByteCMSEntities dc = new DataAccess.TeraByteCMSEntities();
            List<DataAccess.News> lstNews = new List<DataAccess.News>();
            try
            {
                lstNews = (from news in dc.News
                           orderby news.SendDate descending
                           select news).ToList();
                if (Culture == "fa-IR")
                    NewsData = lstNews;
                else
                {
                    foreach (DataAccess.News news in lstNews)
                    {
                        news.Title = Business.OtherLanguages.SelectOtherLanguages(Culture, "News", "Title", news.ID);
                        news.Text = Business.OtherLanguages.SelectOtherLanguages(Culture, "News", "Text", news.ID);
                    }
                    NewsData = lstNews;
                }
            }
            catch (System.Exception ex)
            { throw ex; }
        }
        #endregion

        #region Methods
        public static List<DataAccess.News> SelectAllNews(string Culture)
        {
            DataAccess.TeraByteCMSEntities dc = new DataAccess.TeraByteCMSEntities();
            List<DataAccess.News> lstNews = new List<DataAccess.News>();
            try
            {
                lstNews = (from news in dc.News
                           orderby news.SendDate descending
                           select news).ToList();
                if (Culture == "fa-IR")
                    return lstNews;
                else
                {
                    foreach (DataAccess.News news in lstNews)
                    {
                        news.Title = Business.OtherLanguages.SelectOtherLanguages(Culture, "News", "Title", news.ID);
                        news.Text = Business.OtherLanguages.SelectOtherLanguages(Culture, "News", "Text", news.ID);
                    }
                    return lstNews;
                }
            }
            catch (System.Exception ex)
            { throw ex; }
        }

        public DataAccess.News SelectOneNews(int NewsId, string Culture)
        { 
            DataAccess.TeraByteCMSEntities dc = new DataAccess.TeraByteCMSEntities();
            DataAccess.News News = (from news in dc.News
                                    where news.ID.Equals(NewsId)
                                    select news).First();
            if (News != null)
            {
                if (Culture == "fa-IR")
                    return News;
                else
                {
                    News.Title = Business.OtherLanguages.SelectOtherLanguages(Culture, "News", "Title", News.ID);
                    News.Text = Business.OtherLanguages.SelectOtherLanguages(Culture, "News", "Text", News.ID);
                    return News;
                }
            }
            else
                return null;
        }

        public void InsertNews(int? NewsId,
                               string Title,
                               string Text,
                               string Culture,
                               DateTime EditDate)
        {
            if (Culture != "fa-IR")
            {
                if (NewsId != null)
                {
                    try
                    {
                        Business.OtherLanguages.InsertOtherLanguageData(Culture, "News", "Title", NewsId.Value, Title);
                        Business.OtherLanguages.InsertOtherLanguageData(Culture, "News", "Text", NewsId.Value, Text);
                    }
                    catch (Exception ex)
                    { throw ex; }
                }
                else
                    throw new Exception("News Id in null");
            }
            else
            {
                DataAccess.TeraByteCMSEntities dc = new DataAccess.TeraByteCMSEntities();
                DataAccess.News news = new DataAccess.News();

                news.Title = Title;
                news.Text = Text;
                news.SendDate = EditDate;
                try
                {
                    dc.AddToNews(news);
                    dc.SaveChanges();
                }
                catch(Exception ex)
                { throw ex; }
            }
        }

        public void UpdateNews(int NewsId,
                                  string Title,
                                  string Text,
                                  string Culture,
                                  DateTime EditDate)
        {
            DataAccess.TeraByteCMSEntities dc = new DataAccess.TeraByteCMSEntities();
            DataAccess.News News = new DataAccess.News();

            News = (from news in dc.News
                    where news.ID.Equals(NewsId)
                    select news).First();
            News.SendDate = EditDate;

            if (Culture != "fa-IR")
            {
                try
                {
                    Business.OtherLanguages.UpdateOtherLanguage(Culture, "News", "Title", NewsId, Title);
                    Business.OtherLanguages.UpdateOtherLanguage(Culture, "News", "Text", NewsId, Text);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                News.Title = Title;
                News.Text = Text;
            }

            try
            {
                dc.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteNews(int NewsId)
        {
            try
            {
                Business.OtherLanguages.DeleteOtherLanguageData("News", "Title", NewsId);
                Business.OtherLanguages.DeleteOtherLanguageData("News", "Text", NewsId);
            }
            catch (Exception ex) { throw ex; }

            DataAccess.TeraByteCMSEntities dc = new DataAccess.TeraByteCMSEntities();
            DataAccess.News News = (from news in dc.News
                                    where news.ID.Equals(NewsId)
                                    select news).First<DataAccess.News>();
            dc.DeleteObject(News);
            dc.SaveChanges();
        }
        #endregion
    }
}