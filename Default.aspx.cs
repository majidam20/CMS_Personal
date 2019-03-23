using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Business.UI;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ((HtmlTableCell)Master.FindControl("cliM_Home")).Attributes["class"] = "menActive";
        Title = String.Format(CultureResource.ResourceM.GetString("PageTitle"), CultureResource.ResourceM.GetString("menu_Home"));
        _bindPageData();
        _bindRotator();
    }

    private void _bindPageData()
    {
        DataAccess.PagesData page = Business.PagesData.GetPageData("Home", Session["Cult"].ToString());
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

    private void _bindRotator()
    {
        DataAccess.TeraByteCMSEntities dc = new DataAccess.TeraByteCMSEntities();
        List<DataAccess.PhotoGallery> lstPhotos = new Business.PhotoGallery.AllPhotos().GetGalleryData(Session["Cult"].ToString(), "~/images/Uploads/PhotoGallery/{0}/{1}", 1);
        foreach (DataAccess.PhotoGallery photo in lstPhotos)
        {
         
            ltrGallery.Text+= String.Format("<a href=\"{0}\" target=\"\">" +
                                             "<img src='{1}'/></a>", Page.ResolveUrl(photo.FileName), Page.ResolveUrl(photo.FileName));
        }
    }
}