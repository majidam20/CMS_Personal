using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Business.UI;

public partial class CatalogDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(Request.QueryString["ArtID"]))
        {
            int intId;
            if (Int32.TryParse(Request.QueryString["ArtID"], out intId))
                _bindPageData(intId);
            else Response.Redirect("~/Catalogs.aspx");
        }
        else Response.Redirect("~/Catalogs.aspx");
    }

    private void _bindPageData(int ArtId)
    {
        DataAccess.Articles selArt = new Business.Articles.AllArticles().SelectOneArticle(Session["Cult"].ToString(), ArtId);
        if (selArt != null)
        {
            Title = String.Format(CultureResource.ResourceM.GetString("TitlePattern"), selArt.Title);
            ltrHeader.Text = selArt.Title;
            ltrText.Text = selArt.Content;
            lblDate.Text = String.Format(CultureResource.ResourceM.GetString("lastEdit"), CultureResource.DateByCulture(selArt.EditDate.Value, Session["Cult"].ToString()));
            Page.MetaKeywords = selArt.Title + ", " + Business.UI.CultureResource.ResourceM.GetString("DefaultKeywords");
        }
    }
}