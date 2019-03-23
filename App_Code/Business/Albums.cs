using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Business
{
    public class Albums
    {
        #region Properties
        public List<DataAccess.Album> AlbumsData { get; set; }
        #endregion

        #region Constrauctors
        public Albums(string Culture)
        {
            DataAccess.TeraByteCMSEntities dc = new DataAccess.TeraByteCMSEntities();
            List<DataAccess.Album> albums = (from album in dc.Album
                                             orderby album.AlbumType
                                             select album).ToList<DataAccess.Album>();
            if (Culture != "fa-IR")
                for (int i = 0; i < albums.Count(); i++)
                {
                    albums[i].AlbumTitle = Business.OtherLanguages.SelectOtherLanguages(Culture, "Album", "AlbumTitle", albums[i].ID);
                }
            AlbumsData = albums.ToList();
        }
        #endregion

        #region Methods

        /// <summary>
        /// Return base path of Album type
        /// </summary>
        /// <param name="AlbumType">Album type</param>
        /// <returns></returns>
        public static string AlbumBasePath(DataAccess.Album.AlbumTypeNames AlbumType)
        {
            switch (AlbumType)
            {
                case DataAccess.Album.AlbumTypeNames.Article: return "~/Images/Uploads/Articles";
                case DataAccess.Album.AlbumTypeNames.Image: return "~/Images/Uploads/PhotoGallery";
                case DataAccess.Album.AlbumTypeNames.PageGallery: return "~/Images/Uploads/PhotoGallery";
                case DataAccess.Album.AlbumTypeNames.Products: return "~/Images/Uploads/Products";
                case DataAccess.Album.AlbumTypeNames.Video: return "~/Images/Uploads/VideoGallery";
                default: return "";
            }
        }

        public static List<DataAccess.Album> GetAlbumByType(DataAccess.Album.AlbumTypeNames AlbumType, string Culture)
        {
            int intAlbumType = (int)Enum.Parse(typeof(DataAccess.Album.AlbumTypeNames), Enum.GetName(typeof(DataAccess.Album.AlbumTypeNames), AlbumType));
            DataAccess.TeraByteCMSEntities dc = new DataAccess.TeraByteCMSEntities();
            List<DataAccess.Album> albums = (from album in dc.Album
                                             where album.AlbumType == intAlbumType
                                             select album).ToList<DataAccess.Album>();
            if (albums.Count() > 0)
            {
                if (Culture != "fa-IR")
                    for (int i = 0; i < albums.Count(); i++)
                        albums[i].AlbumTitle = Business.OtherLanguages.SelectOtherLanguages(Culture, "Album", "AlbumTitle", albums[i].ID);
                return albums.ToList();
            }
            else
                return null;
        }

        public static DataAccess.Album OnAlbum(int AlbumId, string Culture)
        {
            DataAccess.Album albums = new DataAccess.Album();
            DataAccess.TeraByteCMSEntities dc = new DataAccess.TeraByteCMSEntities();
                albums = (from album in dc.Album
                                           where album.ID.Equals(AlbumId)
                                           select album).First();

            if (Culture != "fa-IR")
            {
                albums.AlbumTitle = Business.OtherLanguages.SelectOtherLanguages(Culture, "Album", "AlbumTitle", AlbumId);
            }
            return albums;
        }

        public static void IsActive(int AlbumId, bool Active)
        {
            DataAccess.Album albums = new DataAccess.Album();
            DataAccess.TeraByteCMSEntities dc = new DataAccess.TeraByteCMSEntities();
            albums = (from album in dc.Album
                      where album.ID.Equals(AlbumId)
                      select album).First();
            albums.IsActive = Active;

            try
            {
                dc.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void UpdateAlbum(int AlbumId, string Title, string ThumbFileName, string Cultrue)
        {
            if (Cultrue == "fa-IR")
            {
                DataAccess.TeraByteCMSEntities dc = new DataAccess.TeraByteCMSEntities();
                DataAccess.Album albums = new DataAccess.Album();
                albums = (from album in dc.Album
                          where album.ID.Equals(AlbumId)
                          select album).First();

                albums.ID = AlbumId;
                albums.AlbumTitle = Title;
                albums.Thumb = ThumbFileName;

                try
                {
                    dc.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
                try
                {
                    Business.OtherLanguages.UpdateOtherLanguage(Cultrue, "Album", "AlbumTitle", AlbumId, Title);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
        }

        public static void InsertAlbum(DataAccess.Album.AlbumTypeNames Type, string Title, string ThumbFileName)
        {
            DataAccess.TeraByteCMSEntities dc = new DataAccess.TeraByteCMSEntities();
            DataAccess.Album albums = new DataAccess.Album();
            albums.AlbumTitle = Title;
            albums.AlbumTypeName = Type;
            albums.Thumb = ThumbFileName;

            try
            {
                dc.AddToAlbum(albums);
                dc.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void DeleteAlbum(DataAccess.Album.AlbumTypeNames Type, int AlbumId, string PhisicalPath)
        {
            DataAccess.TeraByteCMSEntities ent = new DataAccess.TeraByteCMSEntities();
            DataAccess.Album album = new DataAccess.Album();
            try
            {
                album = (from albm in ent.Album
                         where albm.ID.Equals(AlbumId)
                         select albm).First();

                try
                {
                    Business.Controls.CommonMethods.DeleteFile(HttpContext.Current.Server.MapPath(Business.Albums.AlbumBasePath(album.AlbumTypeName)), album.Thumb);
                }
                catch { int i = 0; }

                switch (Type)
                {
                    case DataAccess.Album.AlbumTypeNames.Image:
                        {
                            new Business.PhotoGallery.AllPhotos().DeleteAllPhotosByAlbum(album.ID, PhisicalPath + "/PhotoGallery");
                            break;
                        }
                    case DataAccess.Album.AlbumTypeNames.PageGallery:
                        {
                            new Business.PhotoGallery.AllPhotos().DeleteAllPhotosByAlbum(album.ID, PhisicalPath + "/PhotoGallery");
                            break;
                        }
                    case DataAccess.Album.AlbumTypeNames.Products:
                        {
                            new Business.Products.AllProducts().DeleteAllProductsByAlbum(album.ID, System.IO.Path.Combine(PhisicalPath, "Products"));
                            break;
                        }
                    case DataAccess.Album.AlbumTypeNames.Video:
                        {
                            new Business.Videos.AllVideos().DeleteAllVideosByAlbum(album.ID, PhisicalPath + "/Products");
                            break;
                        }
                    case DataAccess.Album.AlbumTypeNames.Article:
                        {
                            new Business.Articles.AllArticles().DeleteAllArticlesByAlbum(album.ID);
                            break;
                        }
                }

                Business.OtherLanguages.DeleteOtherLanguageData(album.ID);
                ent.Album.DeleteObject(album);
                ent.SaveChanges();
            }
            catch (Exception ex) { throw ex; }
        }
        #endregion
    }
}