using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Business.UI;

public partial class Catalogs : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ((HtmlTableCell)Master.FindControl("cliM_Catalog")).Attributes["class"] = "menActive";

        Title = String.Format(CultureResource.ResourceM.GetString("PageTitle"), CultureResource.ResourceM.GetString("menu_Catalog"));
        _bindPageData();
        
        if (!String.IsNullOrEmpty(Request.QueryString["acid"]))
        {
            int CatId = 0;
            if (Int32.TryParse(Request.QueryString["acid"], out CatId))
                _setCatalogs(CatId);
        }
        else
            _setCatalogs(null);
    }

    private void _bindPageData()
    {
        DataAccess.PagesData page = Business.PagesData.GetPageData("Catalog", Session["Cult"].ToString());
        if (page != null)
        {
            Title = String.Format(CultureResource.ResourceM.GetString("PageTitle"), page.Header);
            ltrHeader.Text = page.Header;
            ltrText.Text = page.Content;
            lblDate.Text = String.Format(CultureResource.ResourceM.GetString("lastEdit"), CultureResource.DateByCulture(page.EditDate, Session["Cult"].ToString()));
            Page.MetaKeywords = page.Keywords + ", " + Business.UI.CultureResource.ResourceM.GetString("DefaultKeywords");
            Page.MetaDescription = page.Header;
        }
    }

    private void _setCatalogs(int? SelIndex)
    {
        List<DataAccess.Album> lstAlbums = Business.Albums.GetAlbumByType(DataAccess.Album.AlbumTypeNames.Products, Session["Cult"].ToString());

        if (SelIndex != null)
        {
            lstCatalogsCats.DataSource = lstAlbums;
            lstCatalogsCats.SelectedIndex = lstAlbums.IndexOf(lstAlbums.Where(l => l.ID.Equals(SelIndex.Value)).First());
            lstCatalogsCats.DataBind();
            ltrCatTitle.Text = lstAlbums.Where(l => l.ID.Equals(SelIndex.Value)).First().AlbumTitle;
            _setSelectedCategory(SelIndex.Value);
        }
        else
        {
            if (lstAlbums.Count > 0)
                Response.Redirect(Request.Url.OriginalString + "&acid=" + lstAlbums.First().ID);
        }
    }

    private void _setSelectedCategory(int CategoryId)
    {
        lstCatalogs.DataSource = new Business.Articles.AllArticles(Session["Cult"].ToString(), CategoryId).ArticlesData;
        lstCatalogs.DataBind();
    }
    protected void lstCatalogsCats_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}