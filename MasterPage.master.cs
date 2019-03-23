using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    public string plTY, plID, strLan;
    protected void Page_Load(object sender, EventArgs e)
    {
        #region Culture Management
        string strTempCult = "";
        if (Request.QueryString["lan"] == null || Request.QueryString["lan"] == "")
        {
            strTempCult = Session["Cult"] == null ? "fa-IR" : Session["Cult"].ToString();
            strLan = strTempCult.Substring(0, 2);
            Response.Redirect(Request.QueryString.Count > 0 ? Request.Url.OriginalString + "&lan=" + strLan : Request.Url.OriginalString + "?lan=" + strLan);
        }
        else
        {
            switch (Request.QueryString["lan"])
            {
                case "fa": strTempCult = "fa-IR"; break;
                case "en": strTempCult = "en-US"; break;
                default: strTempCult = "fa-IR"; break;
            }
            strLan = strTempCult.Substring(0, 2);
            Business.UI.CultureResource.SetCulture(strTempCult, Context);
        }

        if (String.IsNullOrEmpty(Request.QueryString["cc"]))
        {
            if (Request.QueryString.Count > 0)
                Response.Redirect(Request.Url.OriginalString + "&cc=cy");
            else
                Response.Redirect(Request.Url.OriginalString + "?cc=cy");
        }
        else if (Request.QueryString["cc"] == "cn")
        {
            Response.Redirect(Request.Url.OriginalString.Replace("cc=cn", "cc=cy"));
        }

        if (!Page.IsPostBack)
        {
            Page.DataBind();
        }
        #endregion
    }
}
