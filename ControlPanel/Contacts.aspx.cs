using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.UI;

public partial class ControlPanel_Contacts : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = string.Format("{0} : پنل مدیریت وبسایت {1}", "مدیریت تماسهای دریافتی", CultureResource.ResourceM.GetString("Company"));
        if (!Page.IsPostBack)
        {
            lblHeader.Text = "مدیریت اطلاعات صفحات";
            if (System.Configuration.ConfigurationManager.AppSettings["ToAddress"] != null)
                lblMailAddress.Text = System.Configuration.ConfigurationManager.AppSettings["ToAddress"];
        }
    }
}