using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Business.CustomersMap
{
    public class AllCustomers
    {        
        #region Properties
        public List<DataAccess.CustomersMap> CustomersData { get; set; }
        #endregion

        #region Constractors
        public AllCustomers(){ }
        public AllCustomers(string Culture)
        {
            DataAccess.TeraByteCMSEntities dc = new DataAccess.TeraByteCMSEntities();
            List<DataAccess.CustomersMap> lstCustomers = new List<DataAccess.CustomersMap>();
            try
            {
                lstCustomers = (from cmap in dc.CustomersMap
                           select cmap).ToList();
                if (Culture == "fa-IR")
                    CustomersData = lstCustomers;
                else
                {
                    foreach (DataAccess.CustomersMap cmap in lstCustomers)
                    {
                        cmap.Title = Business.OtherLanguages.SelectOtherLanguages(Culture, "CustomersMap", "Title", cmap.Id);
                        cmap.Text = Business.OtherLanguages.SelectOtherLanguages(Culture, "CustomersMap", "Text", cmap.Id);
                    }
                    CustomersData = lstCustomers;
                }
            }
            catch (System.Exception ex)
            { throw ex; }
        }
        #endregion

        #region Methods
        public static List<DataAccess.CustomersMap> SelectAllCustomers(string Culture)
        {
            DataAccess.TeraByteCMSEntities dc = new DataAccess.TeraByteCMSEntities();
            List<DataAccess.CustomersMap> lstCustomers = new List<DataAccess.CustomersMap>();
            try
            {
                lstCustomers = (from cmap in dc.CustomersMap
                           select cmap).ToList();
                if (Culture == "fa-IR")
                    return lstCustomers;
                else
                {
                    foreach (DataAccess.CustomersMap customer in lstCustomers)
                    {
                        customer.Title = Business.OtherLanguages.SelectOtherLanguages(Culture, "CustomersMap", "Title", customer.Id);
                        customer.Text = Business.OtherLanguages.SelectOtherLanguages(Culture, "CustomersMap", "Text", customer.Id);
                    }
                    return lstCustomers;
                }
            }
            catch (System.Exception ex)
            { throw ex; }
        }

        public DataAccess.CustomersMap SelectOneCustomer(int CustomerId, string Culture)
        { 
            DataAccess.TeraByteCMSEntities dc = new DataAccess.TeraByteCMSEntities();
            DataAccess.CustomersMap Customer = (from cmap in dc.CustomersMap
                                    where cmap.Id.Equals(CustomerId)
                                    select cmap).First();
            if (Customer != null)
            {
                if (Culture == "fa-IR")
                    return Customer;
                else
                {
                    Customer.Title = Business.OtherLanguages.SelectOtherLanguages(Culture, "CustomersMap", "Title", Customer.Id);
                    Customer.Text = Business.OtherLanguages.SelectOtherLanguages(Culture, "CustomersMap", "Text", Customer.Id);
                    return Customer;
                }
            }
            else
                return null;
        }

        public void InsertCustomer(int? CustomerId,
                               float Latitude,
                               float Longitiude,
                               string Title,
                               string Text,
                               string Culture)
        {
            if (Culture != "fa-IR")
            {
                if (CustomerId != null)
                {
                    try
                    {
                        Business.OtherLanguages.InsertOtherLanguageData(Culture, "CustomersMap", "Title", CustomerId.Value, Title);
                        Business.OtherLanguages.UpdateOtherLanguage(Culture, "CustomersMap", "Text", CustomerId.Value, Title);
                    }
                    catch (Exception ex)
                    { throw ex; }
                }
                else
                    throw new Exception("Customer Id is null");
            }
            else
            {
                DataAccess.TeraByteCMSEntities dc = new DataAccess.TeraByteCMSEntities();
                DataAccess.CustomersMap customer = new DataAccess.CustomersMap();

                customer.Latitude = Latitude;
                customer.Longitude = Longitiude;
                customer.Title = Title;
                customer.Text = Text;
                try
                {
                    dc.AddToCustomersMap(customer);
                    dc.SaveChanges();
                }
                catch (Exception ex)
                { throw ex; }
            }
        }

        public void UpdateCustomer(int CustomerId,
                               float Latitude,
                               float Longitiude,
                               string Title,
                               string Text,
                               string Culture)
        {
            DataAccess.TeraByteCMSEntities dc = new DataAccess.TeraByteCMSEntities();
            DataAccess.CustomersMap customer = new DataAccess.CustomersMap();

            customer = (from cmap in dc.CustomersMap
                    where cmap.Id.Equals(CustomerId)
                    select cmap).First();

            customer.Latitude = Latitude;
            customer.Longitude = Longitiude;

            if (Culture != "fa-IR")
            {
                try
                {
                    Business.OtherLanguages.UpdateOtherLanguage(Culture, "CustomersMap", "Title", CustomerId, Title);
                    Business.OtherLanguages.UpdateOtherLanguage(Culture, "CustomersMap", "Text", CustomerId, Title);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                customer.Title = Title;
                customer.Text = Text;
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

        public void DeleteCustomer(int CustomerId)
        {
            try
            {
                Business.OtherLanguages.DeleteOtherLanguageData("CustomersMap", "Title", CustomerId);
                Business.OtherLanguages.DeleteOtherLanguageData("CustomersMap", "Text", CustomerId);
            }
            catch (Exception ex) { throw ex; }

            DataAccess.TeraByteCMSEntities dc = new DataAccess.TeraByteCMSEntities();
            DataAccess.CustomersMap Customer = (from cmap in dc.CustomersMap
                                    where cmap.Id.Equals(CustomerId)
                                    select cmap).First<DataAccess.CustomersMap>();
            dc.DeleteObject(Customer);
            dc.SaveChanges();
        }
        #endregion
    }
}