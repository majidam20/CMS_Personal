using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.UI;

public partial class ControlPanel_News : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = string.Format("{0} : پنل مدیریت وبسایت {1}", "مدیریت اخبار", CultureResource.ResourceM.GetString("Company"));
        if (!Page.IsPostBack)
        {
            lblHeader.Text = "مدیریت اخبار";
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
        grdNews.DataSource = new Business.News.AllNews("fa-IR").NewsData;
        grdNews.DataBind();
    }

    protected void grdNews_RowCreated(object sender, GridViewRowEventArgs e)
    {
        Literal ref_LitRowNumberNormal = new Literal();
        if (e.Row.DataItem != null)
        {
            ref_LitRowNumberNormal = (Literal)e.Row.FindControl("litRowNumberNormal");
            if (grdNews.PageIndex != 0)
                ref_LitRowNumberNormal.Text = GridViewRowsIndexing(grdNews.PageIndex + 1, 5, e.Row.RowIndex);
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

    protected void grdNews_SelectedIndexChanged(object sender, EventArgs e)
    {
        drpLang.SelectedIndex = 0;
        ViewState["IsEdit"] = true;
        pnlCulture.Visible = true;
        btnCancel.Visible = true;
        _fillFields(Convert.ToInt32(((Label)grdNews.SelectedRow.FindControl("lblID")).Text), drpLang.SelectedValue);
        _bindGrid();
    }

    private void _fillFields(int NewsId, string CultureIndex)
    {
        DataAccess.News news = new Business.News.AllNews().SelectOneNews(NewsId, CultureIndex);
        txtTitle.Text = news.Title;
        radEdtText.Content = news.Text;
        lblDate.Text = Persia.Calendar.ConvertToPersian(DateTime.Now).Simple + "  " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ViewState["IsEdit"] == null || Boolean.Parse(ViewState["IsEdit"].ToString()) == false)
        {
            try
            {
                Business.News.AllNews news = new Business.News.AllNews();
                news.InsertNews(null, txtTitle.Text, radEdtText.Content, "fa-IR", DateTime.Now);
                _bindGrid();
                RadAjaxManage.Alert(String.Format("درج خبر در زبان {0} با موفقیت انجام شد.\\n اکنون میتوانید با ویرایش همین خبر، آن را به زبانهای دیگر وارد کنید.", drpLang.SelectedItem.Text));
            }
            catch (Exception ex)
            {
                RadAjaxManage.Alert(String.Format("خطا در درج خبر جدید:\\n{0}", ex.Message));
            }
        }
        else
        {
            try
            {
                new Business.News.AllNews().UpdateNews(
                    Convert.ToInt32(((Label)grdNews.SelectedRow.FindControl("lblID")).Text),
                    txtTitle.Text,
                    radEdtText.Content,
                    drpLang.SelectedValue,
                    DateTime.Now);
                _bindGrid();
                RadAjaxManage.Alert(String.Format("ویرایش خبر در زبان '{0}' با موفقیت انجام شد.", drpLang.SelectedItem.Text));
            }
            catch (Exception ex)
            {
                RadAjaxManage.Alert(String.Format("خطا در ویرایش خبر:\\n{0}", ex.Message));
            }
        }
    }

    protected void drpLang_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        _fillFields(Convert.ToInt32(((Label)grdNews.SelectedRow.FindControl("lblID")).Text), drpLang.SelectedValue);
    }

    protected void grdNews_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdNews.PageIndex = e.NewPageIndex;
        grdNews.SelectedIndex = -1;
        //_bindGrid();
        _loadForm();
    }

    protected void grdNews_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "DelNews":
                {
                    try
                    {
                        new Business.News.AllNews().DeleteNews(Convert.ToInt32(e.CommandArgument.ToString().Split(',')[0]));
                        _bindGrid();
                        RadAjaxManage.Alert(String.Format("حذف خبر با عنوان '{0}' با موفقیت انجام شد.", e.CommandArgument.ToString().Split(',')[1]));
                    }
                    catch (Exception ex)
                    { }
                    break;
                }
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        grdNews.SelectedIndex = -1;
        _bindGrid();
        txtTitle.Text = "";
        radEdtText.Content = "";
        drpLang.SelectedIndex = 0;
        pnlCulture.Visible = false;
        btnCancel.Visible = false;
        ViewState["IsEdit"] = false;
    }

}