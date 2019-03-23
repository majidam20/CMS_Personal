using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Business.UI;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = string.Format("{0} پنل مدیریت وبسایت", CultureResource.ResourceM.GetString("Company"));
    }
    protected void LoginButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/ControlPanel/Default.aspx");
    }
}