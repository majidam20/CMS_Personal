using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Business.UI;

public partial class Contact : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ((HtmlTableCell)Master.FindControl("cliM_Contact")).Attributes["class"] = "menActive";

        Title = String.Format(CultureResource.ResourceM.GetString("PageTitle"), CultureResource.ResourceM.GetString("menu_Contact"));
        _bindPageData();
    }

    private void _bindPageData()
    {
        DataAccess.PagesData page = Business.PagesData.GetPageData("Contact", Session["Cult"].ToString());
        if (page != null)
        {
            Title = string.Format(CultureResource.ResourceM.GetString("PageTitle"), page.Header);
            ltrHeader.Text = page.Header;
            ltrText.Text = page.Content;
            lblDate.Text = string.Format(CultureResource.ResourceM.GetString("lastEdit"), CultureResource.DateByCulture(page.EditDate, Session["Cult"].ToString()));
            Page.MetaKeywords = page.Keywords + ", " + Business.UI.CultureResource.ResourceM.GetString("DefaultKeywords");
            Page.MetaDescription = page.Header;
        }
    }

    protected void btnSend_Click(object sender, EventArgs e)
    {
        if (radCaptcha.IsValid)
        {
            try
            {
                Business.Contacts.InsertContact(
                    txtSubject.Text,
                    txtMail.Text,
                    txtName.Text,
                    radEdtText.Content,
                    DateTime.Now);
                Business.Controls.CommonMethods.SendToAdminMailBox(txtMail.Text, txtName.Text, txtSubject.Text, radEdtText.Content);
                lblSerPM.Text = String.Format("<span style=\"color:green\">{0}</span>", String.Format(CultureResource.ResourceM.GetString("ContactRec"), txtName.Text));
                txtMail.Text = "";
                txtName.Text = "";
                txtSubject.Text = "";
                radEdtText.Content = "";
            }
            catch (Exception ex)
            {
                lblSerPM.Text = String.Format("<span style=\"color:red\">{0}</span>", CultureResource.ResourceM.GetString("ContactErr"));
            }
        }
    }
}