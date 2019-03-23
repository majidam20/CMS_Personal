using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Business.Videos
{
    public class AllVideos
    {
        #region Properties
        public List<DataAccess.VideoGallery> VideoData
        { get; set; }
        #endregion

        #region Constrauctors
        public AllVideos() { }
        public AllVideos(string Culture)
        {
            DataAccess.TeraByteCMSEntities dc = new DataAccess.TeraByteCMSEntities();
            List<DataAccess.VideoGallery> videos = (from vido in dc.VideoGallery
                                                    orderby vido.EditDate descending
                                                    select vido).ToList<DataAccess.VideoGallery>();
            if (Culture != "fa-IR")
                for (int i = 0; i < videos.Count(); i++)
                {
                    videos[i].Title = Business.OtherLanguages.SelectOtherLanguages(Culture, "VideoGallery", "Title", videos[i].ID);
                    videos[i].Describe = Business.OtherLanguages.SelectOtherLanguages(Culture, "VideoGallery", "Describe", videos[i].ID);
                }
            VideoData = videos.ToList<DataAccess.VideoGallery>();
        }
        public AllVideos(string Culture, int AlbumId)
        {
            DataAccess.TeraByteCMSEntities dc = new DataAccess.TeraByteCMSEntities();
            List<DataAccess.VideoGallery> videos = (from vido in dc.VideoGallery
                                                    where vido.AlbumID.Equals(AlbumId)
                                                    orderby vido.EditDate descending
                                                    select vido).ToList<DataAccess.VideoGallery>();
            if (Culture != "fa-IR")
                for (int i = 0; i < videos.Count(); i++)
                {
                    videos[i].Title = Business.OtherLanguages.SelectOtherLanguages(Culture, "VideoGallery", "Title", videos[i].ID);
                    videos[i].Describe = Business.OtherLanguages.SelectOtherLanguages(Culture, "VideoGallery", "Describe", videos[i].ID);
                }
            VideoData = videos.ToList<DataAccess.VideoGallery>();
        }
        #endregion

        #region Methods
        public List<DataAccess.Album> GetVideoAlbums(string Culture)
        {
            DataAccess.TeraByteCMSEntities dc = new DataAccess.TeraByteCMSEntities();
            List<DataAccess.Album> videoAlbum = (from vAl in dc.Album
                                                 where vAl.AlbumTypeName == DataAccess.Album.AlbumTypeNames.Video
                                                 select vAl).ToList<DataAccess.Album>();
            if (videoAlbum.Count() > 0)
            {
                if (Culture != "fa-IR")
                    for (int i = 0; i < videoAlbum.Count(); i++)
                        videoAlbum[i].AlbumTitle = Business.OtherLanguages.SelectOtherLanguages(Culture, "Album", "AlbumTitle", videoAlbum[i].ID);
                return videoAlbum.ToList<DataAccess.Album>();
            }
            else
                return null;
        }

        public int OneVideo(string VideoFileName)
        {
            DataAccess.TeraByteCMSEntities dc = new DataAccess.TeraByteCMSEntities();
            return (from vid in dc.VideoGallery
                    where vid.VideoFileName.Equals(VideoFileName)
                    select vid).First<DataAccess.VideoGallery>().ID;
        }

        public DataAccess.VideoGallery OneVideo(int VideoId)
        {
            DataAccess.TeraByteCMSEntities dc = new DataAccess.TeraByteCMSEntities();
            DataAccess.VideoGallery video = (from v in dc.VideoGallery
                                             where v.ID.Equals(VideoId)
                                             select v).First();
            return video;
        }

        public void DeleteVideo(string FileBasePath, int VideoId)
        {
            DataAccess.TeraByteCMSEntities dc = new DataAccess.TeraByteCMSEntities();
            DataAccess.VideoGallery video = new DataAccess.VideoGallery();

            video = (from v in dc.VideoGallery
                     where v.ID.Equals(VideoId)
                     select v).First();

            #region DeleteFiles
            try
            {
                Business.Controls.CommonMethods.DeleteFile(FileBasePath, video.VideoFileName);
                Business.Controls.CommonMethods.DeleteFile(FileBasePath, video.ImageFileName);
            }
            catch (System.IO.IOException ex)
            {
                //throw new Exception("Delete file Error: " + ex.Message);
            }
            #endregion

            try
            {
                dc.VideoGallery.DeleteObject(video);
                dc.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            try
            {
                Business.OtherLanguages.DeleteOtherLanguageData("VideoGallery", "Title", VideoId);
                Business.OtherLanguages.DeleteOtherLanguageData("VideoGallery", "Describe", VideoId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteAllVideosByAlbum(int AlbumId, string PhisicalPath)
        {
            DataAccess.TeraByteCMSEntities ent = new DataAccess.TeraByteCMSEntities();
            try
            {
                List<DataAccess.VideoGallery> videos = (from video in ent.VideoGallery
                                                      where video.AlbumID.Equals(AlbumId)
                                                      select video).ToList();
                try
                {
                    System.IO.Directory.Delete(System.IO.Path.Combine(PhisicalPath, AlbumId.ToString()));
                }
                catch { }

                foreach (DataAccess.VideoGallery video in videos)
                {
                    Business.OtherLanguages.DeleteOtherLanguageData("VideoGallery", "Title", video.ID);
                    Business.OtherLanguages.DeleteOtherLanguageData("VideoGallery", "Describe", video.ID);
                    ent.VideoGallery.DeleteObject(video);
                }
                ent.SaveChanges();
            }
            catch (System.IO.IOException ex) { throw ex; }
            catch (System.Exception ex) { throw ex; }
        }

        public void UpdateVideo(int VideoId,
                                string ImageFileName,
                                string VideoFileName,
                                string Title,
                                string Describe,
                                string VideoLength,
                                List<string> OtherCultures,
                                List<string> OtherTitles,
                                List<string> OtherTexts,
                                DateTime EditDate)
        {
            try
            {
                UpdateVideo(VideoId, ImageFileName, VideoFileName, Title, Describe, VideoLength, EditDate);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            try
            {
                for (int i = 0; i < OtherCultures.Count; i++)
                {
                    Business.OtherLanguages.UpdateOtherLanguage(OtherCultures[i], "VideoGallery", "Title", VideoId, OtherTitles[i]);
                    Business.OtherLanguages.UpdateOtherLanguage(OtherCultures[i], "VideoGallery", "Describe", VideoId, OtherTitles[i]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateVideo(int VideoId,
                                string ImageFileName,
                                string VideoFileName,
                                string Title,
                                string Describe,
                                string VideoLength,
                                DateTime EditDate)
        {
            DataAccess.TeraByteCMSEntities dc = new DataAccess.TeraByteCMSEntities();
            DataAccess.VideoGallery vid = new DataAccess.VideoGallery();

            vid = (from v in dc.VideoGallery
                   where v.ID.Equals(VideoId)
                   select v).First();
            vid.ImageFileName = ImageFileName;
            vid.VideoFileName = VideoFileName;
            vid.Title = Title;
            vid.Describe = Describe;
            vid.VideoLengthTime = VideoLength;
            vid.EditDate = EditDate;

            try
            {
                dc.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateVideo(int VideoId,
                                string ImageFileName,
                                string VideoFileName,
                                string VideoLength,
                                DateTime EditDate)
        {
            DataAccess.TeraByteCMSEntities dc = new DataAccess.TeraByteCMSEntities();
            DataAccess.VideoGallery vid = new DataAccess.VideoGallery();

            vid = (from v in dc.VideoGallery
                   where v.ID.Equals(VideoId)
                   select v).First();
            vid.ImageFileName = ImageFileName;
            vid.VideoFileName = VideoFileName;
            vid.VideoLengthTime = VideoLength;
            vid.EditDate = EditDate;

            try
            {
                dc.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertVideo(int AlbumeId,
                                string ImageFileName,
                                string VideoFileName,
                                string VideoLength,
                                string Title,
                                string Describe,
                                List<string> OtherCultures,
                                List<string> OtherTitles,
                                List<string> OtherTexts,
                                DateTime EditDate)
        {
            try
            {
                InsertVideo(AlbumeId, ImageFileName, VideoFileName, VideoLength, Title, Describe, EditDate);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            DataAccess.TeraByteCMSEntities dc = new DataAccess.TeraByteCMSEntities();
            int intID = (from vid in dc.VideoGallery
                         where vid.ImageFileName.Equals(ImageFileName)
                         orderby vid.ID descending
                         select vid).First<DataAccess.VideoGallery>().ID;
            try
            {
                for (int i = 0; i < OtherCultures.Count; i++)
                {
                    Business.OtherLanguages.InsertOtherLanguageData(OtherCultures[i], "VideoGallery", "Title", intID, OtherTitles[i]);
                    Business.OtherLanguages.InsertOtherLanguageData(OtherCultures[i], "VideoGallery", "Describe", intID, OtherTitles[i]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertVideo(int AlbumeId,
                                string ImageFileName,
                                string VideoFileName,
                                string VideoLength,
                                string Title,
                                string Describe,
                                DateTime EditDate)
        {
            DataAccess.TeraByteCMSEntities dc = new DataAccess.TeraByteCMSEntities();
            DataAccess.VideoGallery video = new DataAccess.VideoGallery();

            video.AlbumID = AlbumeId;
            video.ImageFileName = ImageFileName;
            video.VideoFileName = VideoFileName;
            video.VideoLengthTime = VideoLength;
            video.Title = Title;
            video.Describe = Describe;
            video.EditDate = EditDate;

            dc.AddToVideoGallery(video);
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

    public class OneVideo
    {
        #region Properties
        public DataAccess.VideoGallery Video
        { get; set; }
        #endregion

        #region Constrauctors
        public OneVideo(int VideoId, string Culture)
        {
            DataAccess.TeraByteCMSEntities dc = new DataAccess.TeraByteCMSEntities();
            DataAccess.VideoGallery video = (from vid in dc.VideoGallery
                                             where vid.ID.Equals(VideoId)
                                             select vid).First<DataAccess.VideoGallery>();
            if (video != null)
            {
                if (Culture != "fa-IR")
                {
                    video.Title = Business.OtherLanguages.SelectOtherLanguages(Culture, "VideoGallery", "Title", video.ID);
                    video.Describe = Business.OtherLanguages.SelectOtherLanguages(Culture, "VideoGallery", "Describe", video.ID);
                }
                Video = video;
            }
        }
        #endregion
    }
}