using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.UI;
public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CultureResource.SetCulture("fa-IR", Context);
        if (!Page.IsPostBack)
        {
            Page.Title = string.Format("{0} : پنل مدیریت وبسایت {1}", "صفحه اصلی", CultureResource.ResourceM.GetString("Company"));
            lblHeader.Text = String.Format("پنل مدیریت {0}", CultureResource.ResourceM.GetString("Company"));
        }
    }
}
