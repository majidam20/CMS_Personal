<%@ WebHandler Language="C#" Class="TeraByte.ImageResizerHandler" %>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace TeraByte
{
    /// <summary>
    /// Summary description for ImageResizer
    /// </summary>
    public class ImageResizerHandler : IHttpHandler
    {///factPercent: Factor Percent for width and height (Must Set)
     ///maxS: Maximum Size
     ///aid: Album Id
     ///if: File Id
     ///fp: filepath -> /images/...
        public void ProcessRequest(HttpContext context)
        {
            int Width = 0, Height = 0;
            string FileName = "";
            
            if (!String.IsNullOrEmpty(context.Request.QueryString["fp"]))
            {
                FileName = System.IO.Path.Combine(context.Server.MapPath("~" + context.Request.QueryString["fp"]));
            }
            else if (String.IsNullOrEmpty(context.Request.QueryString["aid"]) || String.IsNullOrEmpty(context.Request.QueryString["if"]) || String.IsNullOrEmpty(context.Request.QueryString["t"]))
                return;
            else
            {
                switch (context.Request.QueryString["t"])
                {
                    case "i": FileName = System.IO.Path.Combine(context.Server.MapPath("~/images/Uploads/PhotoGallery/" + context.Request.QueryString["aid"]), context.Request.QueryString["if"]); break;
                    case "p": FileName = System.IO.Path.Combine(context.Server.MapPath("~/images/Uploads/Products/" + context.Request.QueryString["aid"]), context.Request.QueryString["if"]); break;
                    case "v": FileName = System.IO.Path.Combine(context.Server.MapPath("~/images/Uploads/VideoGallery/" + context.Request.QueryString["aid"]), context.Request.QueryString["if"]); break;
                    case "pa": FileName = System.IO.Path.Combine(context.Server.MapPath("~/images/Uploads/Products/"), context.Request.QueryString["if"]); break;
                    case "a": FileName = System.IO.Path.Combine(context.Server.MapPath("~/images/Uploads/Articles/" + context.Request.QueryString["aid"]), context.Request.QueryString["if"]); break;
                    case "aa": FileName = System.IO.Path.Combine(context.Server.MapPath("~/images/Uploads/Articles/"), context.Request.QueryString["if"]); break;
                }
            }
            Image img = null;
            try
            {
                img = Bitmap.FromFile(FileName);
            }
            catch
            {
                return;
            }

            if ((!String.IsNullOrEmpty(context.Request.QueryString["w"])) && (!String.IsNullOrEmpty(context.Request.QueryString["h"])))
            {
                Width = Convert.ToInt32(context.Request.QueryString["w"]);
                Height = Convert.ToInt32(context.Request.QueryString["h"]);
            }
            else if (!String.IsNullOrEmpty(context.Request.QueryString["maxS"]))
            {
                int maxSize;
                if (!Int32.TryParse(context.Request.QueryString["maxS"], out maxSize))
                    maxSize = 100;
                if (img.Width > maxSize)
                {
                    Height = (img.Height * maxSize) / img.Width;
                    Width = maxSize;
                }

                if (img.Height > maxSize)
                {
                    Width = (img.Width * maxSize) / img.Height;
                    Height = maxSize;
                }
                else
                {
                    Width = img.Width;
                    Height = img.Height;
                }
            }
            else
            {
                Width = img.Width / 2;
                Height = img.Height / 2;
            }

            img = Business.UI.ImageResize.ImageResizer(img, Width, Height, RotateFlipType.RotateNoneFlipNone);
            MemoryStream m = new MemoryStream();
            img.Save(m, ImageFormat.Png);
            m.Position = 0;
            byte[] b = new byte[m.Length];
            m.Read(b, 0, (int)m.Length);
            context.Response.ContentType = "image/png";
            context.Response.BinaryWrite(b);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}