using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Business.UI;

public partial class Products : System.Web.UI.Page
{
    public string strVideo, strImage;
    protected void Page_Load(object sender, EventArgs e)
    {
        ((HtmlTableCell)Master.FindControl("cliM_Videos")).Attributes["class"] = "menActive";

        Title = String.Format(CultureResource.ResourceM.GetString("PageTitle"), CultureResource.ResourceM.GetString("menu_Videos"));
        _bindPageData();
        if (!Page.IsPostBack)
        {
            _setVideos();
        }
        if (!String.IsNullOrEmpty(Request.QueryString["vid"]))
        {
            int vID = 0;
            if (Int32.TryParse(Request.QueryString["vid"], out vID))
                _setSelectedVideo(vID);
        }
        else if (lstVideos.Items.Count > 0)
            _setSelectedVideo(Int32.Parse(((Label)lstVideos.Items.First().FindControl("lblId")).Text));
        else
            pnlPlayer.Visible = false;
    }

    private void _bindPageData()
    {
        DataAccess.PagesData page = Business.PagesData.GetPageData("Videos", Session["Cult"].ToString());
        if (page != null)
        {
            Title = string.Format(CultureResource.ResourceM.GetString("TitlePattern"), page.Header);
            ltrHeader.Text = page.Header;
            ltrText.Text = page.Content;
            lblDate.Text = string.Format(CultureResource.ResourceM.GetString("lastEdit"), CultureResource.DateByCulture(page.EditDate, Session["Cult"].ToString()));
            Page.MetaKeywords = page.Keywords + ", " + Business.UI.CultureResource.ResourceM.GetString("DefaultKeywords");
            Page.MetaDescription = page.Header;
        }
    }

    private void _setSelectedVideo(int VideoId)
    {
        Business.Videos.OneVideo videos = new Business.Videos.OneVideo(VideoId, Session["Cult"].ToString());
        if (videos.Video != null)
        {
            pnlPlayer.Visible = true;
            strVideo = String.Format("{0}/{1}/{2}/{3}", Business.UI.PublicFunctions.GetDomainUrl(), "images/Uploads/VideoGallery", videos.Video.AlbumID, videos.Video.VideoFileName);
            strImage = String.Format("{0}/{1}/{2}/{3}", Business.UI.PublicFunctions.GetDomainUrl(), "images/Uploads/VideoGallery", videos.Video.AlbumID, videos.Video.ImageFileName);
            ltrHeader.Text = videos.Video.Title;
            ltrText.Text = videos.Video.Describe;
            lblDate.Text = Business.UI.CultureResource.DateByCulture(videos.Video.EditDate, Session["Cult"].ToString());
            pnlPlayer.Visible = true;
            lblVidPM.Visible = false;
        }
    }

    private void _setVideos()
    {
        Business.Videos.AllVideos videos = new Business.Videos.AllVideos(Session["Cult"].ToString());
        lstVideos.DataSource = videos.VideoData;
        lstVideos.DataBind();
    }
}