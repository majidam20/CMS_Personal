using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Business.Products
{
    public class AllProducts
    {
        #region Properties
        public List<DataAccess.Products> ProductData
        { get; set; }
        #endregion

        #region Constrauctors
        public AllProducts() { }
        public AllProducts(string Culture)
        {
            DataAccess.TeraByteCMSEntities dc = new DataAccess.TeraByteCMSEntities();
            List<DataAccess.Products> products = (from product in dc.Products
                                                    orderby product.EditDate descending
                                                    select product).ToList<DataAccess.Products>();
            if (Culture != "fa-IR")
                for (int i = 0; i < products.Count(); i++)
                {
                    products[i].Describ = Business.OtherLanguages.SelectOtherLanguages(Culture, "Products", "Describ", products[i].ID);
                    products[i].Title = Business.OtherLanguages.SelectOtherLanguages(Culture, "Products", "Title", products[i].ID);
                }
            ProductData = products.ToList<DataAccess.Products>();
        }
        public AllProducts(string Culture, int AlbumId)
        {
            DataAccess.TeraByteCMSEntities dc = new DataAccess.TeraByteCMSEntities();
            List<DataAccess.Products> products = (from product in dc.Products
                                                  where product.AlbumID.Equals(AlbumId)
                                                  orderby product.EditDate descending
                                                  select product).ToList<DataAccess.Products>();
            if (Culture != "fa-IR")
                for (int i = 0; i < products.Count(); i++)
                {
                    products[i].Title = Business.OtherLanguages.SelectOtherLanguages(Culture, "Products", "Title", products[i].ID);
                    products[i].Describ = Business.OtherLanguages.SelectOtherLanguages(Culture, "Products", "Describ", products[i].ID);
                }
            ProductData = products.ToList<DataAccess.Products>();
        }
        #endregion

        #region Methods
        public void UpdateProduct(int ProductId,
                                         string FileName,
                                         string Title,
                                         string Describe,
                                         DateTime EditDate)
        {
            DataAccess.TeraByteCMSEntities dc = new DataAccess.TeraByteCMSEntities();
            DataAccess.Products product = new DataAccess.Products();

            product = (from pr in dc.Products
                     where pr.ID.Equals(ProductId)
                     select pr).First();
            product.ID = ProductId;
            product.FileName = FileName;
            product.Title = Title;
            product.Describ = Describe;
            product.EditDate = EditDate;

            try
            {
                dc.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateProduct(int ProductId,
                                         string FileName,
                                         DateTime EditDate)
        {
            DataAccess.TeraByteCMSEntities dc = new DataAccess.TeraByteCMSEntities();
            DataAccess.Products photo = new DataAccess.Products();

            photo = (from pr in dc.Products
                     where pr.ID.Equals(ProductId)
                     select pr).First();
            photo.ID = ProductId;
            photo.FileName = FileName;
            photo.EditDate = EditDate;

            try
            {
                dc.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertProduct(int AlbumId,
                                         string FileName,
                                         string Title,
                                         string Describ,
                                         DateTime EditDate)
        {
            DataAccess.TeraByteCMSEntities dc = new DataAccess.TeraByteCMSEntities();
            DataAccess.Products product = new DataAccess.Products();

            product.AlbumID = AlbumId;
            product.FileName = FileName;
            product.Title = Title;
            product.Describ = Describ;
            product.EditDate = EditDate;

            dc.AddToProducts(product);
            try
            {
                dc.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DataAccess.Products> GetProductsData(string Culture, string BaseProductsUrl, int AlbumId)
        {
            DataAccess.TeraByteCMSEntities dc = new DataAccess.TeraByteCMSEntities();
            var prods = from product in dc.Products
                       orderby product.EditDate descending
                       where product.AlbumID.Equals(AlbumId)
                       select product;
            List<DataAccess.Products> Products = new List<DataAccess.Products>();
            if (prods.Count() > 0)
            {
                foreach (DataAccess.Products p in prods)
                {
                    if (Culture != "fa-IR")
                    {
                        p.Title = Business.OtherLanguages.SelectOtherLanguages(Culture, "Products", "Title", p.ID);
                        p.Describ = Business.OtherLanguages.SelectOtherLanguages(Culture, "Products", "Describ", p.ID);
                    }
                    p.FileName = String.Format(BaseProductsUrl, p.AlbumID, p.FileName);
                }
                Products = prods.ToList<DataAccess.Products>();
            }
            return Products;
        }

        public List<DataAccess.Products> GetProductsData(string Culture, int AlbumId)
        {
            DataAccess.TeraByteCMSEntities dc = new DataAccess.TeraByteCMSEntities();
            var prods = from product in dc.Products
                        orderby product.EditDate descending
                        where product.AlbumID.Equals(AlbumId) && product.IsActive
                        select product;
            List<DataAccess.Products> Products = new List<DataAccess.Products>();
            if (prods.Count() > 0)
            {
                foreach (DataAccess.Products p in prods)
                {
                    if (Culture != "fa-IR")
                    {
                        p.Title = Business.OtherLanguages.SelectOtherLanguages(Culture, "Products", "Title", p.ID);
                        p.Describ = Business.OtherLanguages.SelectOtherLanguages(Culture, "Products", "Describ", p.ID);
                    }
                }
                Products = prods.ToList<DataAccess.Products>();
                return Products;
            }
            else
                return null;
            
        }

        public DataAccess.Products GetOneProduct(string Culture, int ProductId)
        {
            DataAccess.TeraByteCMSEntities dc = new DataAccess.TeraByteCMSEntities();
            var prods = from product in dc.Products
                        orderby product.EditDate descending
                        where product.ID.Equals(ProductId)
                        select product;
            DataAccess.Products OneProduct = new DataAccess.Products();
            if (prods.Count() > 0)
            {
                OneProduct = prods.First();
                if (Culture != "fa-IR")
                {
                    OneProduct.Title = Business.OtherLanguages.SelectOtherLanguages(Culture, "Products", "Title", OneProduct.ID);
                    OneProduct.Describ = Business.OtherLanguages.SelectOtherLanguages(Culture, "Products", "Describ", OneProduct.ID);
                }
            }
            return OneProduct;
        }

        public int GetOneProduct(string Filename)
        {
            DataAccess.TeraByteCMSEntities dc = new DataAccess.TeraByteCMSEntities();
            return (from prod in dc.Products
                    where prod.FileName.Equals(Filename)
                    select prod).First().ID;
        }

        public void DeleteProduct(string FileBasePath, int ProductId)
        {
            DataAccess.TeraByteCMSEntities dc = new DataAccess.TeraByteCMSEntities();
            DataAccess.Products product = new DataAccess.Products();

            product = (from p in dc.Products
                       where p.ID.Equals(ProductId)
                       select p).First();

            #region DeleteFiles
            try
            {
                Business.Controls.CommonMethods.DeleteFile(FileBasePath, product.FileName);
            }
            catch (System.IO.IOException ex)
            {
                //throw new Exception("Delete file Error: " + ex.Message);
            }
            #endregion

            try
            {
                dc.Products.DeleteObject(product);
                dc.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            try
            {
                Business.OtherLanguages.DeleteOtherLanguageData("Products", "Title", ProductId);
                Business.OtherLanguages.DeleteOtherLanguageData("Products", "Describ", ProductId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteAllProductsByAlbum(int AlbumId, string PhisicalPath)
        {
            DataAccess.TeraByteCMSEntities ent = new DataAccess.TeraByteCMSEntities();
            try
            {
                List<DataAccess.Products> products = (from product in ent.Products
                                                      where product.AlbumID.Equals(AlbumId)
                                                      select product).ToList();
                try
                {
                    System.IO.Directory.Delete(System.IO.Path.Combine(PhisicalPath, AlbumId.ToString()));
                }
                catch { }
                foreach (DataAccess.Products product in products)
                {
                    Business.OtherLanguages.DeleteOtherLanguageData("Products", "Title", product.ID);
                    Business.OtherLanguages.DeleteOtherLanguageData("Products", "Describ", product.ID);
                    ent.Products.DeleteObject(product);
                }
                ent.SaveChanges();
            }
            catch (System.IO.IOException ex) { throw ex; }
            catch (System.Exception ex) { throw ex; }
        }
        #endregion
    }
}