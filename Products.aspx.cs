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
    protected void Page_Load(object sender, EventArgs e)
    {
        ((HtmlTableCell)Master.FindControl("cliM_Product")).Attributes["class"] = "menActive";

        Title = String.Format(CultureResource.ResourceM.GetString("PageTitle"), CultureResource.ResourceM.GetString("menu_Product"));
        _bindPageData();
        
        if (!String.IsNullOrEmpty(Request.QueryString["pcid"]))
        {
            int pCatId = 0;
            if (Int32.TryParse(Request.QueryString["pcid"], out pCatId))
                _setVideos(pCatId);
        }
        else
            _setVideos(null);
    }

    private void _bindPageData()
    {
        DataAccess.PagesData page = Business.PagesData.GetPageData("Products", Session["Cult"].ToString());
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

    private void _setVideos(int? SelIndex)
    {
        List<DataAccess.Album> lstAlbums = Business.Albums.GetAlbumByType(DataAccess.Album.AlbumTypeNames.Products, Session["Cult"].ToString());

        if (SelIndex != null)
        {
            lstProductCats.DataSource = lstAlbums;
            lstProductCats.SelectedIndex = lstAlbums.IndexOf(lstAlbums.Where(l => l.ID.Equals(SelIndex.Value)).First());
            lstProductCats.DataBind();
            ltrCatTitle.Text = lstAlbums.Where(l => l.ID.Equals(SelIndex.Value)).First().AlbumTitle;
            _setSelectedCategory(SelIndex.Value);
        }
        else
        {
            if (lstAlbums.Count > 0)
                Response.Redirect(Request.Url.OriginalString + "&pcid=" + lstAlbums.First().ID);
        }
    }

    private void _setSelectedCategory(int CategoryId)
    {
        lstProducts.DataSource = new Business.Products.AllProducts(Session["Cult"].ToString(), CategoryId).ProductData;
        lstProducts.DataBind();
    }
}