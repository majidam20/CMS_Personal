using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.UI;

public partial class ControlPanel_CustomersMap : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = string.Format("{0} : پنل مدیریت وبسایت {1}", "مدیریت موقعیت مشتریان روی نقشه", CultureResource.ResourceM.GetString("Company"));
        if (!Page.IsPostBack)
        {
            lblHeader.Text = "مدیریت موقعیت مشتریان";
            _loadForm();
        }
    }

    private void _loadForm()
    {
        #region Load Cultures Dropdown list
        string[] strCults = Business.UI.PublicFunctions.GetSiteCultures();
        if (strCults != null)
            for (int i = 0; i < strCults.Length; i++)
            {
                if (!String.IsNullOrEmpty(strCults[i]))
                {
                    drpLang.Items.Add(new Telerik.Web.UI.RadComboBoxItem());
                    drpLang.Items[i].Text = strCults[i].Split(',')[1].Trim();
                    drpLang.Items[i].Value = strCults[i].Split(',')[0].Trim();
                    drpLang.Items[i].ForeColor = System.Drawing.Color.Red;
                }
            }
        #endregion
        _bindMap();
    }

    private void _bindMap()
    {
        gMap.DataAddressField = "Id";
        gMap.DataLatitudeField = "Latitude";
        gMap.DataLongitudeField = "Longitude";
        gMap.DataTextField = "Title";
        gMap.DataSource = Business.CustomersMap.AllCustomers.SelectAllCustomers("fa-IR");
        gMap.DataBind();
    }

    protected void gMap_Markers_OnClick(object sender, Artem.Web.UI.Controls.GoogleLocationEventArgs e)
    {
        drpLang.SelectedIndex = 0;
        ViewState["IsEdit"] = true;
        ViewState["SelectedMarker"] = sender;
        pnlCulture.Visible = true;
        btnCancel.Visible = true;
        _fillFields(Convert.ToInt32(((Artem.Google.UI.GoogleMarker)sender).Address), drpLang.SelectedValue);
    }

    private void _fillFields(int CustomerId, string CultureIndex)
    {
        DataAccess.CustomersMap cmap = new Business.CustomersMap.AllCustomers().SelectOneCustomer(CustomerId, CultureIndex);
        txtTitle.Text = cmap.Title;
        txtText.Text = cmap.Text;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ViewState["IsEdit"] == null || Boolean.Parse(ViewState["IsEdit"].ToString()) == false)
        {
            try
            {
                Business.News.AllNews news = new Business.News.AllNews();
                //news.InsertNews(null, txtTitle.Text, radEdtText.Content, "fa-IR", DateTime.Now);
                _bindMap();
                RadAjaxManage.Alert(String.Format("درج موقعیت مشتری در زبان {0} با موفقیت انجام شد.\\n اکنون میتوانید با ویرایش همین موقعیت، آن را به زبانهای دیگر وارد کنید.", drpLang.SelectedItem.Text));
            }
            catch (Exception ex)
            {
                RadAjaxManage.Alert(String.Format("خطا در درج موقعیت مشتری جدید:\\n{0}", ex.Message));
            }
        }
        else
        {
            try
            {
                new Business.News.AllNews().UpdateNews(
                    Convert.ToInt32(((Artem.Google.UI.GoogleMarker)ViewState["SelectedMarker"]).Address),
                    txtTitle.Text,
                    txtText.Text,
                    drpLang.SelectedValue,
                    DateTime.Now);
                _bindMap();
                RadAjaxManage.Alert(String.Format("ویرایش موقعیت مشتری در زبان '{0}' با موفقیت انجام شد.", drpLang.SelectedItem.Text));
            }
            catch (Exception ex)
            {
                RadAjaxManage.Alert(String.Format("خطا در ویرایش مشتری:\\n{0}", ex.Message));
            }
        }
    }

    protected void drpLang_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        _fillFields(Convert.ToInt32(((Artem.Google.UI.GoogleMarker)ViewState["SelectedMarker"]).Address), drpLang.SelectedValue);
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            new Business.CustomersMap.AllCustomers().DeleteCustomer(Convert.ToInt32(((Artem.Google.UI.GoogleMarker)ViewState["SelectedMarker"]).Address));
            _bindMap();
            RadAjaxManage.Alert("حذف موقعیت مشتری با موفقیت انجام شد.");
        }
        catch (Exception ex)
        { }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //grdNews.SelectedIndex = -1;
        _bindMap();
        txtTitle.Text = "";
        txtText.Text = "";
        drpLang.SelectedIndex = 0;
        pnlCulture.Visible = false;
        btnCancel.Visible = false;
        ViewState["IsEdit"] = false;
    }
}