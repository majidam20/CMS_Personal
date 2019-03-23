using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.UI;

public partial class ControlPanel_PhotoGallery : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = string.Format("{0} : پنل مدیریت وبسایت {1}", "مدیریت تصاویر", CultureResource.ResourceM.GetString("Company"));
        if (!Page.IsPostBack)
        {
            lblHeader.Text = "مدیریت تصاویر";
            //_setProductsCategory();
            if (!String.IsNullOrEmpty(Request.QueryString["pageNo"]) && drpPhotoAlbum.SelectedValue != "-1")
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

        #region Load Photo Albums
        drpPhotoAlbum.DataTextField = "AlbumTitle";
        drpPhotoAlbum.DataValueField = "ID";
        List<DataAccess.Album> albums = Business.Albums.GetAlbumByType(DataAccess.Album.AlbumTypeNames.Image, (Session["Cult"].ToString())).Where(a => a.IsActive == true).ToList<DataAccess.Album>();
        List<DataAccess.Album> alBaners = Business.Albums.GetAlbumByType(DataAccess.Album.AlbumTypeNames.PageGallery, (Session["Cult"].ToString())).Where(a => a.IsActive == true).ToList<DataAccess.Album>();
        
        albums.AddRange(alBaners);
        drpPhotoAlbum.DataSource = albums;
        drpPhotoAlbum.DataBind();

        if (drpPhotoAlbum.Items.Count > 0)
        {
            if (!Reset)
            {
                pnlEditPhoto.Visible = true;
                ltrAlbumNull.Visible = false;
                _setPhoto(Convert.ToInt32(drpPhotoAlbum.SelectedValue));
                return;
            }
            drpPhotoAlbum.Items.Insert(0, new ListItem("انتخاب کنید", "-1"));
        }
        else
        {
            pnlEditPhoto.Visible = false;
            ltrAlbumNull.Visible = true;
        }
        #endregion
    }
    
    protected void drpLang_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (EditIndex != -1)
            _fillFields(Convert.ToInt32(((Label)lstPhotos.Items[EditIndex].FindControl("lblID")).Text), drpLang.SelectedValue);
    }

    private void _fillFields(int ID, string Culture)
    {
        if (Culture == "fa-IR")
        {
            txtTitle.Text = ((Label)lstPhotos.Items[EditIndex].FindControl("lblFATitle")).Text;
            radEdtText.Content = ((Label)lstPhotos.Items[EditIndex].FindControl("lblFAText")).Text;
        }
        else
        {
            txtTitle.Text = Business.OtherLanguages.SelectOtherLanguages(Culture, "PhotoGallery", "Title", ID);
            radEdtText.Content = Business.OtherLanguages.SelectOtherLanguages(Culture, "PhotoGallery", "Describe", ID);
        }
    }

    protected void drpPhotoAlbum_SelectedIndexChanged(object sender, EventArgs e)
    {
        //string qur = (Request.Url.Query != "") ?
        //        Request.Url.AbsoluteUri.Replace(Request.Url.Query, string.Format("?catId={0}&pageNo=1", drpPhotoAlbum.SelectedIndex)) :
        //        Request.Url.AbsoluteUri + string.Format("?catId={0}&pageNo=1", drpPhotoAlbum.SelectedIndex);
        //Response.Redirect(qur);

        if (drpPhotoAlbum.SelectedValue != "-1")
        {
            pnlEditPhoto.Visible = true;
            ltrAlbumNull.Visible = false;
            _setPhoto(Convert.ToInt32(drpPhotoAlbum.SelectedValue));
        }
        else
        {
            pnlEditPhoto.Visible = false;
            ltrAlbumNull.Visible = true;
        }
    }

    private void _setPhoto(int AlbumId)
    {
        Business.PhotoGallery.AllPhotos photos = new Business.PhotoGallery.AllPhotos(Session["Cult"].ToString(), AlbumId);
        lstPhotos.DataSource = photos.PhotoData;
        lstPhotos.DataBind();
    }

    protected void lstPhotos_ItemDeleting(object sender, ListViewDeleteEventArgs e)
    {
        string strUploadPath = Server.MapPath(String.Format("~/images/Uploads/PhotoGallery/{0}/", drpPhotoAlbum.SelectedValue));
        string strProd = ((Label)lstPhotos.Items[e.ItemIndex].FindControl("Label3")).Text;
        try
        {
            int intID = Convert.ToInt32(((Label)lstPhotos.Items[e.ItemIndex].FindControl("lblID")).Text);
            new Business.PhotoGallery.AllPhotos().DeletePhoto(strUploadPath, intID);
            _setPhoto(Convert.ToInt32(drpPhotoAlbum.SelectedValue));
            lblFileName.Text = "";
            cliMsgBox.alert(String.Format("حذف تصویر '{0}' با موفقیت انجام شد", strProd));
        }
        catch (Exception ex)
        {
            cliMsgBox.alert(String.Format("خطا در حذف تصویر:\\n {0}", ex.Message));
        }
    }

    public static int EditIndex;
    protected void lstPhotos_ItemEditing(object sender, ListViewEditEventArgs e)
    {
        lstPhotos.EditIndex = e.NewEditIndex;
        EditIndex = lstPhotos.EditIndex;
        _setPhoto(Convert.ToInt32(drpPhotoAlbum.SelectedValue));
        drpLang.SelectedIndex = 0;
        lblFileName.Text = "";
        _fillFields(Convert.ToInt32(((Label)lstPhotos.Items[EditIndex].FindControl("lblID")).Text), drpLang.SelectedValue);

        pnlCulture.Visible = true;
        btnEdit.Visible = true;
        btnCancel.Visible = true;
        btnInsert.Visible = false;
        reqImage.Enabled = false;
    }
    
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        btnEdit.Visible = false;
        btnCancel.Visible = false;
        btnInsert.Visible = true;
        reqImage.Enabled = true;

        lstPhotos.EditIndex = -1;
        _setPhoto(Convert.ToInt32(drpPhotoAlbum.SelectedValue));
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (filupImage.HasFile && drpLang.SelectedValue != "fa-IR")
        {
            cliMsgBox.alert("ویرایش فایل بایستی با زبان فارسی انجام شود.\\nزبان فارسی را انتخاب و سپس فایل را انتخاب کنید\\nدرصورتیکه فقط متن را ویرایش می کنید هیچ فایلی را انتخاب نکنید");
            return;
        }
        string strUploadPath = Server.MapPath(String.Format("~/images/Uploads/PhotoGallery/{0}/", drpPhotoAlbum.SelectedValue));
        int intID = Convert.ToInt32(((Label)lstPhotos.Items[lstPhotos.EditIndex].FindControl("lblID")).Text);
        string FileName = filupImage.HasFile ? Business.Controls.CommonMethods.UploadFile(strUploadPath, filupImage.PostedFile, Guid.NewGuid().ToString()) :
            ((Image)lstPhotos.Items[lstPhotos.EditIndex].FindControl("Image1")).ToolTip;
        string strTitle = txtTitle.Text;
        string strText = radEdtText.Content;

        try
        {
            if (drpLang.SelectedValue == "fa-IR")
                new Business.PhotoGallery.AllPhotos().UpdatePhoto(intID, FileName, strTitle, strText, DateTime.Now);
            else
            {
                if (Business.OtherLanguages.SelectOtherLanguages(drpLang.SelectedValue, "PhotoGallery", "Title", intID) != "")
                {
                    Business.OtherLanguages.UpdateOtherLanguage(drpLang.SelectedValue, "PhotoGallery", "Title", intID, strTitle);
                    Business.OtherLanguages.UpdateOtherLanguage(drpLang.SelectedValue, "PhotoGallery", "Describe", intID, strText);
                }
                else
                {
                    Business.OtherLanguages.InsertOtherLanguageData(drpLang.SelectedValue, "PhotoGallery", "Title", intID, strTitle);
                    Business.OtherLanguages.InsertOtherLanguageData(drpLang.SelectedValue, "PhotoGallery", "Describe", intID, strText);
                }
                new Business.PhotoGallery.AllPhotos().UpdatePhoto(intID, FileName, DateTime.Now);
            }
            txtTitle.Text = "";
            radEdtText.Content = "";
            lstPhotos.EditIndex = -1;
            EditIndex = -1;
            drpLang.SelectedIndex = 0;
            _setPhoto(Convert.ToInt32(drpPhotoAlbum.SelectedValue));
            cliMsgBox.alert(String.Format("ویرایش تصویر در زبان '{0}' با موفقیت انجام شد.", drpLang.SelectedItem.Text));
            pnlCulture.Visible = false;
            btnEdit.Visible = false;
            btnCancel.Visible = false;
            btnInsert.Visible = true;
            reqImage.Enabled = true;
        }
        catch (Exception ex)
        {
            cliMsgBox.alert(String.Format("خطا در ویرایش تصویر:\\n{0}", ex.Message));
        }
    }
    
    protected void btnInsert_Click(object sender, EventArgs e)
    {
        if (drpLang.SelectedValue == "fa-IR")
        {
            string strUploadPath = Server.MapPath(String.Format("~/images/Uploads/PhotoGallery/{0}/", drpPhotoAlbum.SelectedValue));
            int intCatID = Convert.ToInt32(drpPhotoAlbum.SelectedItem.Value);
            string FileName = filupImage.HasFile ?
                Business.Controls.CommonMethods.UploadFile(strUploadPath, filupImage.PostedFile, Guid.NewGuid().ToString()) :
                ((Image)lstPhotos.Items[lstPhotos.EditIndex].FindControl("Image1")).ToolTip;
            string strTitle = txtTitle.Text;
            string strText = radEdtText.Content;

            lblFileName.Text = FileName;
            reqImage.Enabled = false;
            try
            {
                new Business.PhotoGallery.AllPhotos().InsertPhoto(intCatID, FileName, strTitle, strText, DateTime.Now);
                txtTitle.Text = "";
                radEdtText.Content = "";
                _setPhoto(Convert.ToInt32(drpPhotoAlbum.SelectedValue));
                lstPhotos.EditIndex = -1;
                lblFileName.Text = "";
                reqImage.Enabled = true;
                cliMsgBox.alert(String.Format("درج تصویر در زبان {0} با موفقیت انجام شد.\\n اکنون میتوانید توضیحات تصویر را به زبانهای دیگر وارد کنید.", drpLang.SelectedItem.Text));
            }
            catch (Exception ex)
            {
                cliMsgBox.alert(String.Format("خطا در درج تصویر جدید:\\n{0}", ex.Message));
            }
        }
    }
}