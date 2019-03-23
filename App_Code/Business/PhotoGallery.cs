using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Business.PhotoGallery
{
    public class AllPhotos
    {
        #region Properties
        public List<DataAccess.PhotoGallery> PhotoData
        { get; set; }
        #endregion

        #region Constrauctors
        public AllPhotos() { }
        public AllPhotos(string Culture)
        {
            DataAccess.TeraByteCMSEntities dc = new DataAccess.TeraByteCMSEntities();
            List<DataAccess.PhotoGallery> photos = (from photo in dc.PhotoGallery
                                                    orderby photo.EditDate descending
                                                    select photo).ToList<DataAccess.PhotoGallery>();
            if (Culture != "fa-IR")
                for (int i = 0; i < photos.Count(); i++)
                {
                    photos[i].Title = Business.OtherLanguages.SelectOtherLanguages(Culture, "PhotoGallery", "Title", photos[i].ID);
                    photos[i].Describe = Business.OtherLanguages.SelectOtherLanguages(Culture, "PhotoGallery", "Describe", photos[i].ID);
                }
            PhotoData = photos.ToList<DataAccess.PhotoGallery>();
        }
        public AllPhotos(string Culture, int AlbumId)
        {
            DataAccess.TeraByteCMSEntities dc = new DataAccess.TeraByteCMSEntities();
            List<DataAccess.PhotoGallery> photos = (from photo in dc.PhotoGallery
                                                    where photo.AlbumID.Equals(AlbumId)
                                                    orderby photo.EditDate descending
                                                    select photo).ToList<DataAccess.PhotoGallery>();
            if (Culture != "fa-IR")
                for (int i = 0; i < photos.Count(); i++)
                {
                    photos[i].Title = Business.OtherLanguages.SelectOtherLanguages(Culture, "PhotoGallery", "Title", photos[i].ID);
                    photos[i].Describe = Business.OtherLanguages.SelectOtherLanguages(Culture, "PhotoGallery", "Describe", photos[i].ID);
                }
            PhotoData = photos.ToList<DataAccess.PhotoGallery>();
        }
        #endregion

        #region Methods
        public void UpdatePhoto(int PhotoId,
                                         string FileName,
                                         string Title,
                                         string Describe,
                                         DateTime EditDate)
        {
            DataAccess.TeraByteCMSEntities dc = new DataAccess.TeraByteCMSEntities();
            DataAccess.PhotoGallery photo = new DataAccess.PhotoGallery();

            photo = (from pr in dc.PhotoGallery
                     where pr.ID.Equals(PhotoId)
                     select pr).First();
            photo.ID = PhotoId;
            photo.FileName = FileName;
            photo.Title = Title;
            photo.Describe = Describe;
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

        public void UpdatePhoto(int PhotoId,
                                         string FileName,
                                         DateTime EditDate)
        {
            DataAccess.TeraByteCMSEntities dc = new DataAccess.TeraByteCMSEntities();
            DataAccess.PhotoGallery photo = new DataAccess.PhotoGallery();

            photo = (from pr in dc.PhotoGallery
                    where pr.ID.Equals(PhotoId)
                    select pr).First();
            photo.ID = PhotoId;
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

        public void InsertPhoto(int AlbumId,
                                         string FileName,
                                         string Title,
                                         string Describ,
                                         DateTime EditDate)
        {
            DataAccess.TeraByteCMSEntities dc = new DataAccess.TeraByteCMSEntities();
            DataAccess.PhotoGallery photo = new DataAccess.PhotoGallery();

            photo.AlbumID = AlbumId;
            photo.FileName = FileName;
            photo.Title = Title;
            photo.Describe = Describ;
            photo.EditDate = EditDate;

            dc.AddToPhotoGallery(photo);
            try
            {
                dc.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DataAccess.PhotoGallery> GetGalleryData(string Culture, string BaseGalleryUrl, int AlbumId)
        {
            DataAccess.TeraByteCMSEntities dc = new DataAccess.TeraByteCMSEntities();
            var imgs = from photo in dc.PhotoGallery
                       orderby photo.EditDate descending
                       where photo.AlbumID.Equals(AlbumId)
                       select photo;
            List<DataAccess.PhotoGallery> Images = new List<DataAccess.PhotoGallery>();
            if (imgs.Count() > 0)
            {
                foreach (DataAccess.PhotoGallery p in imgs)
                {
                    if (Culture != "fa-IR")
                    {
                        p.Title = Business.OtherLanguages.SelectOtherLanguages(Culture, "PhotoGallery", "Title", p.ID);
                        p.Describe = Business.OtherLanguages.SelectOtherLanguages(Culture, "PhotoGallery", "Describe", p.ID);
                    }
                    p.FileName = String.Format(BaseGalleryUrl, p.AlbumID, p.FileName);
                }
                Images = imgs.ToList<DataAccess.PhotoGallery>();
            }
            return Images;
        }

        public List<DataAccess.PhotoGallery> GetGalleryData(string Culture, int AlbumId)
        {
            DataAccess.TeraByteCMSEntities dc = new DataAccess.TeraByteCMSEntities();
            var imgs = from photo in dc.PhotoGallery
                       orderby photo.EditDate descending
                       where photo.AlbumID.Equals(AlbumId)
                       select photo;
            List<DataAccess.PhotoGallery> Images = new List<DataAccess.PhotoGallery>();
            if (imgs.Count() > 0)
            {
                foreach (DataAccess.PhotoGallery p in imgs)
                {
                    if (Culture != "fa-IR")
                    {
                        p.Title = Business.OtherLanguages.SelectOtherLanguages(Culture, "PhotoGallery", "Title", p.ID);
                        p.Describe = Business.OtherLanguages.SelectOtherLanguages(Culture, "PhotoGallery", "Describe", p.ID);
                    }
                }
                Images = imgs.ToList<DataAccess.PhotoGallery>();
            }
            return Images;
        }

        public void DeletePhoto(string FileBasePath, int PhotoId)
        {
            DataAccess.TeraByteCMSEntities dc = new DataAccess.TeraByteCMSEntities();
            DataAccess.PhotoGallery photo = new DataAccess.PhotoGallery();

            photo = (from p in dc.PhotoGallery
                     where p.ID.Equals(PhotoId)
                     select p).First();

            #region DeleteFiles
            try
            {
                Business.Controls.CommonMethods.DeleteFile(FileBasePath, photo.FileName);
            }
            catch (System.IO.IOException ex)
            {
                //throw new Exception("Delete file Error: " + ex.Message);
            }
            #endregion

            try
            {
                dc.PhotoGallery.DeleteObject(photo);
                dc.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            try
            {
                Business.OtherLanguages.DeleteOtherLanguageData("PhotoGallery", "Title", PhotoId);
                Business.OtherLanguages.DeleteOtherLanguageData("PhotoGallery", "Describe", PhotoId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteAllPhotosByAlbum(int AlbumId, string PhisicalPath)
        {
            DataAccess.TeraByteCMSEntities ent = new DataAccess.TeraByteCMSEntities();
            try
            {
                List<DataAccess.PhotoGallery> photos = (from photo in ent.PhotoGallery
                                                        where photo.AlbumID.Equals(AlbumId)
                                                        select photo).ToList();

                try
                {
                    System.IO.Directory.Delete(System.IO.Path.Combine(PhisicalPath, AlbumId.ToString()));
                }
                catch { }

                foreach (DataAccess.PhotoGallery photo in photos)
                {
                    Business.OtherLanguages.DeleteOtherLanguageData("PhotoGallery", "Title", photo.ID);
                    Business.OtherLanguages.DeleteOtherLanguageData("PhotoGallery", "Describe", photo.ID);
                    ent.PhotoGallery.DeleteObject(photo);
                }
                ent.SaveChanges();
            }
            catch (System.IO.IOException ex) { throw ex; }
            catch (System.Exception ex) { throw ex; }
        }
        #endregion
    }
}