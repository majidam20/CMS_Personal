using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Business.UI;

public partial class Images : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ((HtmlTableCell)Master.FindControl("cliM_Images")).Attributes["class"] = "menActive";

        Title = String.Format(CultureResource.ResourceM.GetString("PageTitle"), CultureResource.ResourceM.GetString("menu_Images"));
        _bindPageData();

        List<DataAccess.PhotoGallery> p = new Business.PhotoGallery.AllPhotos().GetGalleryData(Session["Cult"].ToString(), 2);
        lstPhoto.DataSource = p;
        lstPhoto.DataBind();
    }

    private void _bindPageData()
    {
        DataAccess.PagesData page = Business.PagesData.GetPageData("Images", Session["Cult"].ToString());
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
}