using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

using System.Configuration;

namespace Business.UI
{
    public static class CultureResource
    {
        #region Properties
        public static System.Resources.ResourceManager ResourceM
        { get; set; }
        #endregion

        #region Methods
        public static void SetCulture(string Culture, HttpContext PageContext)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(Culture);
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(Culture);
            switch (Culture)
            {
                case "fa-IR": Business.UI.CultureResource.ResourceM = Resources.Resource_fa_IR.ResourceManager; break;
                case "en-US": Business.UI.CultureResource.ResourceM = Resources.Resource.ResourceManager; break;
                  
            }
            //Business.UI.CultureResource.ResourceM = Culture == "fa-IR" ? Resources.Resource_fa_IR.ResourceManager : Resources.Resource.ResourceManager;
            PageContext.Session["Cult"] = Culture;
        }

        public static string DateByCulture(DateTime Date, string Culture)
        {
            switch (Culture)
            {
                case "en-US":
                default: return string.Format("<span dir=\"ltr\">{0}/{1}/{2} {3}:{4}:{5}</span>", Date.Year, Date.Month, Date.Day, Date.Hour, Date.Minute, Date.Second);
                case "fa-IR":
                    {
                        Persia.SunDate pd = Persia.Calendar.ConvertToPersian(Date);
                        return string.Format("<span dir=\"rtl\">{0}/{1}/{2} {3}:{4}:{5}</span>", pd.ArrayType[0], pd.ArrayType[1], pd.ArrayType[2], Date.Hour, Date.Minute, Date.Second);
                    }
            }
        }
        #endregion
    }

    public static class CultureUI
    {
        public static string ChangeCultureLink(HttpRequest Request,string TargetCulture)
        {
                        
            string tempTC = TargetCulture.Substring(0, 2);
            if (String.IsNullOrEmpty(Request.QueryString["lan"] ))
            {
                if (Request.QueryString.Count > 0)
                    return (Request.Url.OriginalString + "&lan=" + tempTC).Replace("cc=cy", "cc=cn");
                else
                    return (Request.Url.OriginalString + "?lan=" + tempTC).Replace("cc=cy", "cc=cn");
            }
            else
            {
                return Request.Url.OriginalString.Replace("lan=" + Request.QueryString["lan"], "lan=" + tempTC).Replace("cc=cy", "cc=cn");
            }
        }

        public static void UnderCinstructionPage(ref DataAccess.PagesData page, string MenuTitle)
        {
            page.Header = Business.UI.CultureResource.ResourceM.GetString(MenuTitle);
            page.Content = String.Format(Business.UI.CultureResource.ResourceM.GetString("UnderConst"),
                                                    Business.UI.CultureResource.ResourceM.GetString("Company"),
                                                    String.Format("{0}/Images/C_{1}.jpg",PublicFunctions.GetDomainUrl(), new Random().Next(1, 7).ToString()),
                                                    Business.UI.CultureResource.ResourceM.GetString(MenuTitle),
                                                    Business.UI.CultureResource.ResourceM.GetString("UnderConstDescrib"));
            page.Keywords = Business.UI.CultureResource.ResourceM.GetString("DefaultKeywords");
            
        }

        public static DataAccess.PagesData LoadPage(string Menu, string PageName, string Culture)
        {
            DataAccess.PagesData page = new DataAccess.PagesData();
            page = Business.PagesData.GetPageData(PageName, Culture);
            if (page == null || (String.IsNullOrEmpty(page.Header) && (String.IsNullOrEmpty(page.Content))))
            {
                page = new DataAccess.PagesData();
                Business.UI.CultureUI.UnderCinstructionPage(ref page, Menu);
            }
            return page;
        }
    }

    public static class ImageResize
    {
        #region Image Resize
        /// <summary> 
        /// Resizes and rotates an image, keeping the original aspect ratio. Does not dispose the original 
        /// Image instance. 
        /// </summary> /// 
        /// <param name="image">Image instance</param> 
        /// <param name="width">desired width</param> 
        /// <param name="height">desired height</param>
        /// <param name="rotateFlipType">desired RotateFlipType</param> 
        /// <returns>new resized/rotated Image instance</returns> 
        public static System.Drawing.Image ImageResizer(System.Drawing.Image image, int width, int height, RotateFlipType rotateFlipType)
        {
            // clone the Image instance, since we don't want to resize the original Image instance     
            var rotatedImage = image.Clone() as System.Drawing.Image;
            rotatedImage.RotateFlip(rotateFlipType);
            var newSize = CalculateResizedDimensions(rotatedImage, width, height);
            var resizedImage = new Bitmap(newSize.Width, newSize.Height, PixelFormat.Format24bppRgb);
            resizedImage.SetResolution(72, 72);
            using (var graphics = Graphics.FromImage(resizedImage))
            {
                // set parameters to create a high-quality thumbnail         
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                // use an image attribute in order to remove the black/gray border around image after resize         
                // (most obvious on white images), see this post for more information:         
                // http://www.codeproject.com/KB/GDI-plus/imgresizoutperfgdiplus.aspx         
                using (var attribute = new ImageAttributes())
                {
                    attribute.SetWrapMode(WrapMode.TileFlipXY);
                    // draws the resized image to the bitmap             
                    graphics.DrawImage(rotatedImage, new Rectangle(new Point(0, 0), newSize), 0, 0, rotatedImage.Width, rotatedImage.Height, GraphicsUnit.Pixel, attribute);
                }
            }
            return resizedImage;
        }

        /// <summary> 
        /// Calculates resized dimensions for an image, preserving the aspect ratio. 
        /// </summary> 
        /// <param name="image">Image instance</param> 
        /// <param name="desiredWidth">desired width</param> 
        /// <param name="desiredHeight">desired height</param> 
        /// <returns>Size instance with the resized dimensions</returns> 
        private static Size CalculateResizedDimensions(System.Drawing.Image image, int desiredWidth, int desiredHeight)
        {
            var widthScale = (double)desiredWidth / image.Width;
            var heightScale = (double)desiredHeight / image.Height;
            // scale to whichever ratio is smaller, this works for both scaling up and scaling down     
            var scale = widthScale < heightScale ? widthScale : heightScale;
            return new Size
            {
                Width = (int)(scale * image.Width),
                Height = (int)(scale * image.Height)
            };
        }
        #endregion
    }

    public static class PublicFunctions
    {
        public static string GetDomainUrl()
        {
            try
            {
                string temp = GetKeyValue("SiteDomain");
                if (String.IsNullOrEmpty(temp))
                    return null;
                else
                    return temp;
            }
            catch { return null; }
        }

        public static string[] GetSiteCultures()
        {
            try
            {
                string temp = GetKeyValue("Cultures");
                if (String.IsNullOrEmpty(temp))
                    return null;
                else
                    return temp.Split(';');
            }
            catch { return null; }
        }

        public static string UploadBasePath()
        {
            try
            {
                string temp = GetKeyValue("UploadBasePath");
                if (String.IsNullOrEmpty(temp))
                    return null;
                else
                    return temp;
            }
            catch { return null; }
        }

        public static string GetKeyValue(string Key)
        {
            try
            {
                if (String.IsNullOrEmpty(ConfigurationManager.AppSettings[Key]))
                    return null;
                else
                    return ConfigurationManager.AppSettings[Key];
            }
            catch (System.Configuration.ConfigurationException ex) { throw ex; }
        }
    }
}