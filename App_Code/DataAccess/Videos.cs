using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccess
{
    public partial class VideoGallery
    {
        #region Properties
        public string VideoLengthTime
        {
            get 
            {
                return String.Format("{0:HH:mm:ss}", this.VideoLength);
            }
            set 
            {
                this.VideoLength = DateTime.Parse("1900-01-01 " + value);
            }
        }
        #endregion
    }
}