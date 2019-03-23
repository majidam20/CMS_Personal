using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class AdminCPMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            lblTitle.Text = string.Format("کنترل پنل وبسایت {0}", Business.UI.CultureResource.ResourceM.GetString("Company"));
            //_initialize();
        }
    }

    private void _initialize()
    {
        if (!Context.User.Identity.IsAuthenticated)
            Response.Redirect("~/Default.aspx");
    }

    protected void btnSignOut_Click(object sender, EventArgs e)
    {
        FormsAuthentication.SignOut();
        Response.Redirect("~/Default.aspx");
    }
}