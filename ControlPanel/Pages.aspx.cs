using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.UI;

public partial class ControlPanel_Pages : System.Web.UI.Page
{
    //public static string[] CulturesTitle = { "فارسی", "انگلیسی" };

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = string.Format("{0} : پنل مدیریت وبسایت {1}", "مدیریت اطلاعات صفحات", CultureResource.ResourceM.GetString("Company"));
        if (!Page.IsPostBack)
        {
            lblHeader.Text = "مدیریت اطلاعات صفحات";
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
        _bindGrid();
    }

    private void _bindGrid()
    {
        grdPages.DataSource = Business.PagesData.GetAllPagesData();
        grdPages.DataBind();
    }

    protected void grdPages_RowCreated(object sender, GridViewRowEventArgs e)
    {
        Literal ref_LitRowNumberNormal = new Literal();
        if (e.Row.DataItem != null)
        {
            ref_LitRowNumberNormal = (Literal)e.Row.FindControl("litRowNumberNormal");
            if (grdPages.PageIndex != 0)
                ref_LitRowNumberNormal.Text = GridViewRowsIndexing(grdPages.PageIndex + 1, 5, e.Row.RowIndex);
            else
                ref_LitRowNumberNormal.Text = GridViewRowsIndexing(0, 5, e.Row.RowIndex);
        }
    }
    protected string GridViewRowsIndexing(int PageIndex, int PageSize, int RowIndex)
    {
        string resultIndex;
        resultIndex = ((PageIndex * PageSize) + 1 + RowIndex).ToString();
        return resultIndex;
    }
    protected void grdPages_SelectedIndexChanged(object sender, EventArgs e)
    {
        for (int i = 0; i < drpLang.Items.Count; i++)
        {
            drpLang.Items[i].ForeColor = System.Drawing.Color.Red;
            drpLang.Items[i].Text = drpLang.Items[i].Text.Replace("ویرایش شده", "ویرایش نشده");
        }
            drpLang.SelectedIndex = 0;
        _fillFields(((Label)grdPages.SelectedRow.FindControl("lblName")).Text, drpLang.SelectedValue);
    }

    private void _fillFields(string PageName, string CultureIndex)
    {
        Business.PagesData p = new Business.PagesData(PageName, CultureIndex);

        txtTitle.Text = p.Title;
        radEdtText.Content = p.Text;

        lblDate.Text = Persia.Calendar.ConvertToPersian(DateTime.Now).Simple + "  " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;

        pnlEdit.Visible = true;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (drpLang.SelectedValue == "fa-IR")
                Business.PagesData.UpdatePagesData(
                    Convert.ToInt32(((Label)grdPages.SelectedRow.FindControl("lblID")).Text),
                    ((Label)grdPages.SelectedRow.FindControl("lblName")).Text,
                    txtTitle.Text,
                    radEdtText.Content,
                    DateTime.Now);
            else
            {
                Business.OtherLanguages.UpdateOtherLanguage(
                                        drpLang.SelectedValue,
                                        "PagesData",
                                        "Header",
                                        Convert.ToInt32(((Label)grdPages.SelectedRow.FindControl("lblID")).Text),
                                        txtTitle.Text);
                Business.OtherLanguages.UpdateOtherLanguage(
                                        drpLang.SelectedValue,
                                        "PagesData",
                                        "Content",
                                        Convert.ToInt32(((Label)grdPages.SelectedRow.FindControl("lblID")).Text),
                                        radEdtText.Content);
                Business.PagesData.UpdateEditDate(Convert.ToInt32(((Label)grdPages.SelectedRow.FindControl("lblID")).Text), DateTime.Now);
            }

            drpLang.SelectedItem.Text = drpLang.SelectedItem.Text.Replace("ویرایش نشده", "").Replace("ویرایش شده", "") + "ویرایش شده";
            drpLang.SelectedItem.ForeColor = System.Drawing.Color.Green;
            cliMsgBox.alert(String.Format("ویرایش اطلاعات صفحه در زبان '{0}' با موفقیت انجام شد.", drpLang.SelectedItem.Text));
        }
        catch (Exception ex)
        {
            cliMsgBox.alert(String.Format("خطا در ثبت اطلاعات:\\n{0}", ex.Message));
        }
    }
    protected void drpLang_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        _fillFields(((Label)grdPages.SelectedRow.FindControl("lblName")).Text, drpLang.SelectedValue);
    }
    protected void grdPages_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdPages.PageIndex = e.NewPageIndex;
        grdPages.SelectedIndex = -1;
        _bindGrid();
    }
}