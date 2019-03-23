using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccess
{
    public partial class Album
    {
        #region Enums
        public enum AlbumTypeNames
        {
            Products = 1, Image = 2, Video = 3, PageGallery = 4, Article = 5
        }
        #endregion

        #region Properties
        public AlbumTypeNames AlbumTypeName
        {
            get 
            {
                switch (this.AlbumType)
                {
                    case 1: return AlbumTypeNames.Products;
                    case 2: return AlbumTypeNames.Image;
                    case 3: return AlbumTypeNames.Video;
                    case 4: return AlbumTypeNames.PageGallery;
                    case 5: return AlbumTypeNames.Article;
                    default: return AlbumTypeNames.Products;
                }
            }
            set 
            {
                switch (value)
                {
                    case AlbumTypeNames.Products: this.AlbumType = 1; break;
                    case AlbumTypeNames.Image: this.AlbumType = 2; break;
                    case AlbumTypeNames.Video: this.AlbumType = 3; break;
                    case AlbumTypeNames.PageGallery: this.AlbumType = 4; break;
                    case AlbumTypeNames.Article: this.AlbumType = 5; break;
                }
            }
        }
        #endregion
    }
}