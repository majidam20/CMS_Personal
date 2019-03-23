using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Business
{
    public class OtherLanguages
    {
        #region Methods
        public static string SelectOtherLanguages(string Culture, string TableName, string FieldName, int RowID)
        {
            DataAccess.TeraByteCMSEntities dc = new DataAccess.TeraByteCMSEntities();
            var datas = (from data in dc.OtherLanguage
                                 where data.Culture.Equals(Culture) &&
                                       data.TableName.Equals(TableName) &&
                                       data.Field.Equals(FieldName) &&
                                       data.RowID.Equals(RowID)
                                 select data);
            if (datas.Count() > 0)
                return datas.First<DataAccess.OtherLanguage>().Text;
            else
                return "";
        }

        public static void InsertOtherLanguageData(string Culture, string TableName, string FieldName, int RowID, string Text)
        {
            DataAccess.TeraByteCMSEntities dc = new DataAccess.TeraByteCMSEntities();
            DataAccess.OtherLanguage olData = new DataAccess.OtherLanguage();

            olData.Culture = Culture;
            olData.TableName = TableName;
            olData.Field = FieldName;
            olData.RowID = RowID;
            olData.Text = Text;
            
            dc.AddToOtherLanguage(olData);
            dc.SaveChanges();
        }

        public static void DeleteOtherLanguageData(string Culture, string TableName, string FieldName, int RowID)
        {
            DataAccess.TeraByteCMSEntities dc = new DataAccess.TeraByteCMSEntities();
            DataAccess.OtherLanguage olData = (from data in dc.OtherLanguage
                                                       where data.Culture.Equals(Culture) &&
                                                       data.TableName.Equals(TableName) &&
                                                       data.Field.Equals(FieldName) &&
                                                       data.RowID.Equals(RowID)
                                                       select data).First<DataAccess.OtherLanguage>();
            dc.DeleteObject(olData);
            dc.SaveChanges();
        }

        public static void DeleteOtherLanguageData(string TableName, string FieldName, int RowID)
        {
            DataAccess.TeraByteCMSEntities dc = new DataAccess.TeraByteCMSEntities();
            List<DataAccess.OtherLanguage> olData = (from data in dc.OtherLanguage
                                                       where data.TableName.Equals(TableName) &&
                                                       data.Field.Equals(FieldName) &&
                                                       data.RowID.Equals(RowID)
                                                       select data).ToList<DataAccess.OtherLanguage>();
            foreach (DataAccess.OtherLanguage o in olData)
                dc.DeleteObject(o);
            dc.SaveChanges();
        }

        public static void DeleteOtherLanguageData(int ID)
        {
            DataAccess.TeraByteCMSEntities dc = new DataAccess.TeraByteCMSEntities();
            var x = (from data in dc.OtherLanguage
                     where data.ID.Equals(ID)
                     select data);
            if (x != null && x.Count() > 0)
            {
                dc.DeleteObject(x.First());
                dc.SaveChanges();
            }
        }

        public static void UpdateOtherLanguage(string Culture, string TableName, string FieldName, int RowID, string Text)
        {
            DataAccess.TeraByteCMSEntities dc = new DataAccess.TeraByteCMSEntities();
            var x = from data in dc.OtherLanguage
                    where data.Culture.Equals(Culture) &&
                    data.TableName.Equals(TableName) &&
                    data.Field.Equals(FieldName) &&
                    data.RowID.Equals(RowID)
                    select data;
            if (x.Count() > 0)
            {
                DataAccess.OtherLanguage olData = x.First<DataAccess.OtherLanguage>();
                olData.Text = Text;
                dc.SaveChanges();
            }
            else
                InsertOtherLanguageData(Culture, TableName, FieldName, RowID, Text);
        }

        public static void UpdateOtherLanguage(int ID, string Text)
        {
            DataAccess.TeraByteCMSEntities dc = new DataAccess.TeraByteCMSEntities();
            DataAccess.OtherLanguage olData = (from data in dc.OtherLanguage
                                                       where data.ID.Equals(ID)
                                                       select data).First<DataAccess.OtherLanguage>();
            olData.Text = Text;
            dc.SaveChanges();
        }
        #endregion
    }
}