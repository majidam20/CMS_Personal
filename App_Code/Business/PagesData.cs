using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Business
{
    public class PagesData
    {
        #region Properties
        public int PageID
        { get; set; }
        public string Title
        { get; set; }
        public string Text
        { get; set; }
        public DateTime EditDate
        { get; set; }
        public string[][] PageOtherInfo
        { get; set; }
        #endregion

        #region Constractors
        public PagesData(string PageName, string Culture)
        {
            try
            {
                _getPageData(PageName, Culture);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region Methods
        private void _getPageData(string PageName, string Culture)
        {
            DataAccess.TeraByteCMSEntities dc = new DataAccess.TeraByteCMSEntities();
            var PageData = from page in dc.PagesData
                           where page.PageName.ToLower().Equals(PageName.ToLower())
                           select page;
            DataAccess.PagesData curPage = null;
            if (PageData.Count() > 0)
            {
                curPage = PageData.First<DataAccess.PagesData>();
                if (Culture != "fa-IR")
                {
                    curPage.Header = Business.OtherLanguages.SelectOtherLanguages(Culture, "PagesData", "Header", curPage.ID);
                    curPage.Content = Business.OtherLanguages.SelectOtherLanguages(Culture, "PagesData", "Content", curPage.ID);
                }
            }
            if (curPage != null)
            {
                PageID = curPage.ID;
                Title = curPage.Header;
                Text = curPage.Content;
                EditDate = curPage.EditDate;
            }
        }

        public static DataAccess.PagesData GetPageData(string PageName, string Culture)
        {
            DataAccess.TeraByteCMSEntities dc = new DataAccess.TeraByteCMSEntities();
            var curPage = from page in dc.PagesData
                           where page.PageName.ToLower().Equals(PageName.ToLower())
                           select page;
            DataAccess.PagesData PageData = null;
            if (curPage.Count() > 0)
            {
                PageData = curPage.First<DataAccess.PagesData>();
                if (Culture != "fa-IR")
                {
                    PageData.Header = Business.OtherLanguages.SelectOtherLanguages(Culture, "PagesData", "Header", PageData.ID);
                    PageData.Content = Business.OtherLanguages.SelectOtherLanguages(Culture, "PagesData", "Content", PageData.ID);
                }
            }
            return PageData;
        }

        public static List<DataAccess.PagesData> GetAllPagesData()
        {
            DataAccess.TeraByteCMSEntities dc = new DataAccess.TeraByteCMSEntities();
            var PageData = from page in dc.PagesData
                           select page;
            return PageData.ToList<DataAccess.PagesData>();
        }

        public static void UpdatePagesData(int PageID,
                                           string PageName,
                                           string Title,
                                           string Text,
                                           DateTime EditDate)
        {
            DataAccess.TeraByteCMSEntities dc = new DataAccess.TeraByteCMSEntities();
            DataAccess.PagesData p = new DataAccess.PagesData();
            p = (from page in dc.PagesData
                 where page.ID.Equals(PageID)
                 select page).First();

            p.PageName = PageName;
            p.Header = Title;
            p.Content = Text;
            p.EditDate = EditDate;

            try
            {
                dc.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void UpdatePagesData(int PageID,
                                           string PageName,
                                           string Title,
                                           string Text, 
                                           string[] OtherCultures,
                                           string[] OtherTitles,
                                           string[] OtherTexts,
                                           DateTime EditDate)
        {
            DataAccess.TeraByteCMSEntities dc = new DataAccess.TeraByteCMSEntities();
            DataAccess.PagesData p = new DataAccess.PagesData();
            p = (from page in dc.PagesData
                 where page.ID.Equals(PageID)
                 select page).First();

            p.PageName = PageName;
            p.Header = Title;
            p.Content = Text;
            p.EditDate = EditDate;

            try
            {
                dc.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            try
            {
                for (int i = 0; i < OtherCultures.Length; i++)
                {
                    Business.OtherLanguages.UpdateOtherLanguage(OtherCultures[i], "PagesData", "Header", PageID, OtherTitles[i]);
                    Business.OtherLanguages.UpdateOtherLanguage(OtherCultures[i], "PagesData", "Content", PageID, OtherTitles[i]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void UpdateEditDate(int PageID, DateTime EditDate)
        {
            DataAccess.TeraByteCMSEntities dc = new DataAccess.TeraByteCMSEntities();
            DataAccess.PagesData p = new DataAccess.PagesData();
            p = (from page in dc.PagesData
                 where page.ID.Equals(PageID)
                 select page).First();
            p.EditDate = EditDate;

            try
            {
                dc.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}