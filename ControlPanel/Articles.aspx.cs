using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.UI;

public partial class ControlPanel_Articles : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = string.Format("{0} : پنل مدیریت وبسایت {1}", "مدیریت مقالات", CultureResource.ResourceM.GetString("Company"));
        if (!Page.IsPostBack)
        {
            lblHeader.Text = "مدیریت مقالات";
            if (!String.IsNullOrEmpty(Request.QueryString["pageNo"]) && drpArticleAlbum.SelectedValue != "-1")
                _loadForm(false);
            else
                _loadForm(true);
        }
        lblDate.Text = Business.UI.CultureResource.DateByCulture(DateTime.Now, "fa-IR");
    }

    private void _loadForm(bool Reset)
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

        #region Load Articles Albums
        drpArticleAlbum.DataTextField = "AlbumTitle";
        drpArticleAlbum.DataValueField = "ID";
        List<DataAccess.Album> albums = Business.Albums.GetAlbumByType(DataAccess.Album.AlbumTypeNames.Products, (Session["Cult"].ToString())).Where(a => a.IsActive == true).ToList<DataAccess.Album>();
        drpArticleAlbum.DataSource = albums;
        drpArticleAlbum.DataBind();

        if (drpArticleAlbum.Items.Count > 0)
        {
            if (!Reset)
            {
                pnlEdit.Visible = true;
                ltrAlbumNull.Visible = false;
                _setArticles(Convert.ToInt32(drpArticleAlbum.SelectedValue));
                return;
            }
            drpArticleAlbum.Items.Insert(0, new ListItem("انتخاب کنید", "-1"));
        }
        else
        {
            pnlEdit.Visible = false;
            ltrAlbumNull.Visible = true;
        }
        #endregion
    }

    protected void drpLang_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (ViewState["IsEdit"] != null && Boolean.Parse(ViewState["IsEdit"].ToString()) && grdArticles.SelectedIndex != -1)
        {
            int ArticleId = Convert.ToInt32(((Label)grdArticles.SelectedRow.FindControl("lblID")).Text);
            DataAccess.Articles Articles = new Business.Articles.AllArticles().SelectOneArticle(drpLang.SelectedValue, ArticleId);
            txtTitle.Text = Articles.Title;
            radEdtText.Content = Articles.Content;
        }
        else
        {
            drpLang.SelectedIndex = 0;
            RadAjaxManage.Alert("درج آلبوم جدید ابتدا بایستی به زبان فارسی صورت گیرد\\nپس از آن با ویرایش آلبوم ثبت شده میتوانید زبانهای دیگر را برای آن تنظیم نمایید");
        }
    }

    protected void drpArticleAlbum_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpArticleAlbum.SelectedValue != "-1")
        {
            pnlEdit.Visible = true;
            ltrAlbumNull.Visible = false;
            _setArticles(Convert.ToInt32(drpArticleAlbum.SelectedValue));
        }
        else
        {
            pnlEdit.Visible = false;
            ltrAlbumNull.Visible = true;
        }
    }

    private void _setArticles(int AlbumId)
    {
        Business.Articles.AllArticles alArticles = new Business.Articles.AllArticles(Session["Cult"].ToString(), AlbumId);
        grdArticles.DataSource = alArticles.ArticlesData;
        grdArticles.DataBind();
    }

    protected void grdArticles_RowCreated(object sender, GridViewRowEventArgs e)
    {
        Literal ref_LitRowNumberNormal = new Literal();
        if (e.Row.DataItem != null)
        {
            ref_LitRowNumberNormal = (Literal)e.Row.FindControl("litRowNumberNormal");
            if (grdArticles.PageIndex != 0)
                ref_LitRowNumberNormal.Text = GridViewRowsIndexing(grdArticles.PageIndex + 1, 5, e.Row.RowIndex);
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

    protected void grdArticles_SelectedIndexChanged(object sender, EventArgs e)
    {
        drpLang.SelectedIndex = 0;
        ViewState["IsEdit"] = true;
        pnlCulture.Visible = true;
        btnCancel.Visible = true;
        _fillFields(Convert.ToInt32(((Label)grdArticles.SelectedRow.FindControl("lblID")).Text), drpLang.SelectedValue);
        _setArticles(Convert.ToInt32(drpArticleAlbum.SelectedValue));
    }

    private void _fillFields(int ArticleId, string CultureIndex)
    {
        DataAccess.Articles news = new Business.Articles.AllArticles().SelectOneArticle(CultureIndex, ArticleId);
        txtTitle.Text = news.Title;
        radEdtText.Content = news.Content;
        lblDate.Text = Persia.Calendar.ConvertToPersian(DateTime.Now).Simple + "  " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ViewState["IsEdit"] == null || Boolean.Parse(ViewState["IsEdit"].ToString()) == false)
        {
            try
            {
                Business.Articles.AllArticles article = new Business.Articles.AllArticles();
                article.InsertArticle(null, Convert.ToInt32(drpArticleAlbum.SelectedItem.Value), txtTitle.Text, radEdtText.Content, "fa-IR", DateTime.Now);
                _setArticles(Convert.ToInt32(drpArticleAlbum.SelectedValue));
                radEdtText.Content = "";
                txtTitle.Text = "";
                RadAjaxManage.Alert(String.Format("درج کاتالوگ در زبان {0} با موفقیت انجام شد.\\n اکنون میتوانید با ویرایش همین کاتالوگ، آن را به زبانهای دیگر وارد کنید.", drpLang.SelectedItem.Text));
            }
            catch (Exception ex)
            {
                RadAjaxManage.Alert(String.Format("خطا در درج کاتالوگ جدید:\\n{0}", ex.Message));
            }
        }
        else
        {
            try
            {
                new Business.Articles.AllArticles().UpdateArticle(
                    Convert.ToInt32(((Label)grdArticles.SelectedRow.FindControl("lblID")).Text),
                    Convert.ToInt32(drpArticleAlbum.SelectedValue),
                    txtTitle.Text,
                    radEdtText.Content,
                    drpLang.SelectedValue,
                    DateTime.Now);
                grdArticles.SelectedIndex = -1;
                btnCancel.Visible = false;
                ViewState["IsEdit"] = null;
                pnlCulture.Visible = false;
                _setArticles(Convert.ToInt32(drpArticleAlbum.SelectedValue));
                RadAjaxManage.Alert(String.Format("ویرایش کاتالوگ در زبان '{0}' با موفقیت انجام شد.", drpLang.SelectedItem.Text));
            }
            catch (Exception ex)
            {
                RadAjaxManage.Alert(String.Format("خطا در ویرایش کاتالوگ:\\n{0}", ex.Message));
            }
        }
    }

    protected void grdArticles_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdArticles.PageIndex = e.NewPageIndex;
        grdArticles.SelectedIndex = -1;
        //_bindGrid();
        _loadForm(false);
    }

    protected void grdArticles_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "DelArticle":
                {
                    try
                    {
                        new Business.Articles.AllArticles().DeleteArticle(Convert.ToInt32(e.CommandArgument.ToString().Split(',')[0]));
                        _setArticles(Convert.ToInt32(drpArticleAlbum.SelectedValue));
                        RadAjaxManage.Alert(String.Format("حذف کاتالوگ با عنوان '{0}' با موفقیت انجام شد.", e.CommandArgument.ToString().Split(',')[1]));
                    }
                    catch (Exception ex)
                    { }
                    break;
                }
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        grdArticles.SelectedIndex = -1;
        _setArticles(Convert.ToInt32(drpArticleAlbum.SelectedValue));
        txtTitle.Text = "";
        radEdtText.Content = "";
        drpLang.SelectedIndex = 0;
        pnlCulture.Visible = false;
        btnCancel.Visible = false;
        ViewState["IsEdit"] = false;
    }
}