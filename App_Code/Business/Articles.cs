using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Business.Articles
{
    public class AllArticles
    {        
        #region Properties
        public List<DataAccess.Articles> ArticlesData { get; set; }
        #endregion

        #region Constractors
        public AllArticles() { }
        public AllArticles(string Culture)
        {
            DataAccess.TeraByteCMSEntities dc = new DataAccess.TeraByteCMSEntities();
            List<DataAccess.Articles> lstArticles = new List<DataAccess.Articles>();
            try
            {
                lstArticles = (from article in dc.Articles
                               orderby article.EditDate descending
                               select article).ToList();
                if (Culture == "fa-IR")
                    ArticlesData = lstArticles;
                else
                {
                    foreach (DataAccess.Articles article in lstArticles)
                    {
                        article.Title = Business.OtherLanguages.SelectOtherLanguages(Culture, "Articles", "Title", article.Id);
                        article.Content = Business.OtherLanguages.SelectOtherLanguages(Culture, "Articles", "Content", article.Id);
                    }
                    ArticlesData = lstArticles;
                }
            }
            catch (System.Exception ex)
            { throw ex; }
        }

        public AllArticles(string Culture, int AlbumId)
        {
            DataAccess.TeraByteCMSEntities dc = new DataAccess.TeraByteCMSEntities();
            List<DataAccess.Articles> lstArticles = new List<DataAccess.Articles>();
            try
            {
                lstArticles = (from article in dc.Articles
                               where article.AlbumId.Equals(AlbumId)
                               orderby article.EditDate descending
                               select article).ToList();
                if (Culture == "fa-IR")
                    ArticlesData = lstArticles;
                else
                {
                    foreach (DataAccess.Articles article in lstArticles)
                    {
                        article.Title = Business.OtherLanguages.SelectOtherLanguages(Culture, "Articles", "Title", article.Id);
                        article.Content = Business.OtherLanguages.SelectOtherLanguages(Culture, "Articles", "Content", article.Id);
                    }
                    ArticlesData = lstArticles;
                }
            }
            catch (System.Exception ex)
            { throw ex; }
        }
        #endregion

        #region Methods
        public static List<DataAccess.Articles> SelectAllArticles(string Culture, int AlbumId)
        {
            DataAccess.TeraByteCMSEntities dc = new DataAccess.TeraByteCMSEntities();
            List<DataAccess.Articles> lstArticles = new List<DataAccess.Articles>();
            try
            {
                lstArticles = (from article in dc.Articles
                               where AlbumId.Equals(AlbumId)
                               orderby article.EditDate descending
                               select article).ToList();
                if (Culture == "fa-IR")
                    return lstArticles;
                else
                {
                    foreach (DataAccess.Articles article in lstArticles)
                    {
                        article.Title = Business.OtherLanguages.SelectOtherLanguages(Culture, "Articles", "Title", article.Id);
                        article.Content = Business.OtherLanguages.SelectOtherLanguages(Culture, "Articles", "Content", article.Id);
                    }
                    return lstArticles;
                }
            }
            catch (System.Exception ex)
            { throw ex; }
        }

        public DataAccess.Articles SelectOneArticle(string Culture, int ArticleId)
        {
            DataAccess.TeraByteCMSEntities dc = new DataAccess.TeraByteCMSEntities();
            DataAccess.Articles articles = (from article in dc.Articles
                                            where article.Id.Equals(ArticleId)
                                            select article).First();
            if (articles != null)
            {
                if (Culture == "fa-IR")
                    return articles;
                else
                {
                    articles.Title = Business.OtherLanguages.SelectOtherLanguages(Culture, "Articles", "Title", articles.Id);
                    articles.Content = Business.OtherLanguages.SelectOtherLanguages(Culture, "Articles", "Content", articles.Id);
                    return articles;
                }
            }
            else
                return null;
        }

        public void InsertArticle(int? ArticleId,
                                  int AlbumId,
                                  string Title,
                                  string Content,
                                  string Culture,
                                  DateTime EditDate)
        {
            if (Culture != "fa-IR")
            {
                if (ArticleId != null)
                {
                    try
                    {
                        Business.OtherLanguages.InsertOtherLanguageData(Culture, "Articles", "Title", ArticleId.Value, Title);
                        Business.OtherLanguages.UpdateOtherLanguage(Culture, "Articles", "Content", ArticleId.Value, Content);
                    }
                    catch (Exception ex)
                    { throw ex; }
                }
                else
                    throw new Exception("Article Id in null");
            }
            else
            {
                DataAccess.TeraByteCMSEntities dc = new DataAccess.TeraByteCMSEntities();
                DataAccess.Articles article = new DataAccess.Articles();
                article.AlbumId = AlbumId;
                article.Title = Title;
                article.Content = Content;
                article.EditDate = EditDate;
                try
                {
                    dc.AddToArticles(article);
                    dc.SaveChanges();
                }
                catch(Exception ex)
                { throw ex; }
            }
        }

        public void UpdateArticle(int ArticleId,
                                  int AlbumId,
                                  string Title,
                                  string Content,
                                  string Culture,
                                  DateTime EditDate)
        {
            DataAccess.TeraByteCMSEntities dc = new DataAccess.TeraByteCMSEntities();
            DataAccess.Articles articles = new DataAccess.Articles();

            articles = (from article in dc.Articles
                    where article.Id.Equals(ArticleId)
                    select article).First();
            articles.EditDate = EditDate;

            if (Culture != "fa-IR")
            {
                try
                {
                    Business.OtherLanguages.UpdateOtherLanguage(Culture, "Articles", "Title", ArticleId, Title);
                    Business.OtherLanguages.UpdateOtherLanguage(Culture, "Articles", "Content", ArticleId, Content);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                articles.AlbumId = AlbumId;
                articles.Title = Title;
                articles.Content = Content;
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

        public void DeleteArticle(int ArticleId)
        {
            try
            {
                Business.OtherLanguages.DeleteOtherLanguageData("Articles", "Title", ArticleId);
                Business.OtherLanguages.DeleteOtherLanguageData("Articles", "Content", ArticleId);
            }
            catch (Exception ex) { throw ex; }

            DataAccess.TeraByteCMSEntities dc = new DataAccess.TeraByteCMSEntities();
            DataAccess.Articles articles = (from article in dc.Articles
                                    where article.Id.Equals(ArticleId)
                                    select article).First<DataAccess.Articles>();
            dc.DeleteObject(articles);
            dc.SaveChanges();
        }

        public void DeleteAllArticlesByAlbum(int AlbumId)
        {
            DataAccess.TeraByteCMSEntities ent = new DataAccess.TeraByteCMSEntities();
            try
            {
                List<DataAccess.Articles> articles = (from article in ent.Articles
                                                      where article.AlbumId.Equals(AlbumId)
                                                      select article).ToList();

                foreach (DataAccess.Articles article in articles)
                {
                    Business.OtherLanguages.DeleteOtherLanguageData("Articles", "Title", article.Id);
                    Business.OtherLanguages.DeleteOtherLanguageData("Articles", "Content", article.Id);
                    ent.Articles.DeleteObject(article);
                }
                ent.SaveChanges();
            }
            catch (System.Exception ex) { throw ex; }
        }
        #endregion
    }
}