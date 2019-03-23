using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Business.UI;

public partial class ProductDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(Request.QueryString["ProdID"]))
        {
            int intId;
            if (Int32.TryParse(Request.QueryString["ProdID"], out intId))
                _bindPageData(intId);
            else Response.Redirect("~/Products.aspx");
        }
        else Response.Redirect("~/Products.aspx");
    }

    private void _bindPageData(int ProdId)
    {
        DataAccess.Products selProd = new Business.Products.AllProducts().GetOneProduct(Session["Cult"].ToString(), ProdId);
        if (selProd != null)
        {
            Title = String.Format(CultureResource.ResourceM.GetString("TitlePattern"), selProd.Title);
            ltrHeader.Text = selProd.Title;
            ltrText.Text = selProd.Describ;
            hypProdImage.NavigateUrl = String.Format("~/Images/Uploads/Products/{0}/{1}", selProd.AlbumID, selProd.FileName);
            imgProd.ImageUrl = String.Format("~/ImageReSizer.ashx?aid={0}&if={1}&w={2}&h={3}&t={4}", selProd.AlbumID, selProd.FileName, (900 * 50 / 100), (900 * 50 / 100), "p");
            lblDate.Text = String.Format(CultureResource.ResourceM.GetString("lastEdit"), CultureResource.DateByCulture(selProd.EditDate, Session["Cult"].ToString()));
            Page.MetaKeywords = selProd.Title + ", " + Business.UI.CultureResource.ResourceM.GetString("DefaultKeywords");
        }
    }
}