using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.UI;

public partial class ControlPanel_VideoGallery : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = string.Format("{0} : پنل مدیریت وبسایت {1}", "مدیریت ویدئو", CultureResource.ResourceM.GetString("Company"));
        if (!Page.IsPostBack)
        {  
            lblHeader.Text = "مدیریت ویدئو";
            if (!String.IsNullOrEmpty(Request.QueryString["pageNo"]) && drpVideoAlbum.SelectedValue != "-1")
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

        #region Load Video Albums
        drpVideoAlbum.DataTextField = "AlbumTitle";
        drpVideoAlbum.DataValueField = "ID";
        List<DataAccess.Album> albums = Business.Albums.GetAlbumByType(DataAccess.Album.AlbumTypeNames.Video, (Session["Cult"].ToString())).Where(a => a.IsActive == true).ToList<DataAccess.Album>();
        drpVideoAlbum.DataSource = albums;
        drpVideoAlbum.DataBind();

        if (drpVideoAlbum.Items.Count > 0)
        {
            if (!Reset)
            {
                pnlEditVideo.Visible = true;
                ltrAlbumNull.Visible = false;
                _setVideos(Convert.ToInt32(drpVideoAlbum.SelectedValue));
                return;
            }
            drpVideoAlbum.Items.Insert(0, new ListItem("انتخاب کنید", "-1"));
        }
        else
        {
            pnlEditVideo.Visible = false;
            ltrAlbumNull.Visible = true;
        }
        #endregion
    }

    protected void drpLang_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (ViewState["IsEdit"] != null && Boolean.Parse(ViewState["IsEdit"].ToString()) && lstVideos.EditIndex != -1)
        {
            int VideoId = Convert.ToInt32(((Label)lstVideos.Items[lstVideos.EditIndex].FindControl("lblID")).Text);
            DataAccess.VideoGallery Video = new Business.Videos.OneVideo(VideoId, drpLang.SelectedValue).Video;
            txtTitle.Text = Video.Title;
            radEdtText.Content = Video.Describe;
        }
        else
        {
            drpLang.SelectedIndex = 0;
            cliMsgBox.alert("درج آلبوم جدید ابتدا بایستی به زبان فارسی صورت گیرد\\nپس از آن با ویرایش آلبوم ثبت شده میتوانید زبانهای دیگر را برای آن تنظیم نمایید");
        }
    }

    private void _fillFields(int EditIndex, int ID, string Culture)
    {
        if (Culture == "fa-IR")
        {
            txtTitle.Text = ((Label)lstVideos.Items[EditIndex].FindControl("lblFATitle")).Text;
            radEdtText.Content = ((Label)lstVideos.Items[EditIndex].FindControl("lblFAText")).Text;
        }
        else
        {
            txtTitle.Text = Business.OtherLanguages.SelectOtherLanguages(Culture, "VideoGallery", "Title", ID);
            radEdtText.Content = Business.OtherLanguages.SelectOtherLanguages(Culture, "VideoGallery", "Describe", ID);
        }
    }

    protected void drpVideoAlbum_SelectedIndexChanged(object sender, EventArgs e)
    {
        //string qur = (Request.Url.Query != "") ?
        //        Request.Url.AbsoluteUri.Replace(Request.Url.Query, string.Format("?catId={0}&pageNo=1", drpVideoAlbum.SelectedIndex)) :
        //        Request.Url.AbsoluteUri + string.Format("?catId={0}&pageNo=1", drpVideoAlbum.SelectedIndex);
        //Response.Redirect(qur);

        if (drpVideoAlbum.SelectedValue != "-1")
        {
            pnlEditVideo.Visible = true;
            ltrAlbumNull.Visible = false;
            _setVideos(Convert.ToInt32(drpVideoAlbum.SelectedValue));
        }
        else
        {
            pnlEditVideo.Visible = false;
            ltrAlbumNull.Visible = true;
        }
    }

    private void _setVideos(int AlbumId)
    {
        Business.Videos.AllVideos alVideos = new Business.Videos.AllVideos(Session["Cult"].ToString(), AlbumId);
        lstVideos.DataSource = alVideos.VideoData;
        lstVideos.DataBind();
    }

    protected void lstVideos_ItemDeleting(object sender, ListViewDeleteEventArgs e)
    {
        string strVideo = ((Label)lstVideos.Items[e.ItemIndex].FindControl("Label3")).Text;
        try
        {
            int intID = Convert.ToInt32(((Label)lstVideos.Items[e.ItemIndex].FindControl("lblID")).Text);
            Business.Videos.AllVideos alVideos = new Business.Videos.AllVideos();
            alVideos.DeleteVideo(Server.MapPath(String.Format("~/images/Uploads/VideoGallery/{0}/", drpVideoAlbum.SelectedValue)), intID);
            _setVideos(Convert.ToInt32(drpVideoAlbum.SelectedValue));
            lblImageFileName.Text = "";
            lblVideoFileName.Text = "";
            cliMsgBox.alert(string.Format("حذف ویدئو با عنوان '{0}' با موفقیت انجام شد", strVideo));
        }
        catch (Exception ex)
        {
            cliMsgBox.alert(String.Format("خطا در حذف ویدئو:\\n{0}", ex.Message));
        }
    }

    protected void lstVideos_ItemEditing(object sender, ListViewEditEventArgs e)
    {
        lstVideos.EditIndex = e.NewEditIndex;
        _setVideos(Convert.ToInt32(drpVideoAlbum.SelectedValue));
        drpLang.SelectedIndex = 0;
        lblImageFileName.Text = ((Label)lstVideos.Items[lstVideos.EditIndex].FindControl("lblImageFileNameLST")).Text;
        lblVideoFileName.Text = ((Label)lstVideos.Items[lstVideos.EditIndex].FindControl("lblVideoFileNameLST")).Text;
        radmaskVideoLength.TextWithLiterals = ((Label)lstVideos.Items[lstVideos.EditIndex].FindControl("lblVideoLength")).Text;
        _fillFields(lstVideos.EditIndex, Convert.ToInt32(((Label)lstVideos.Items[lstVideos.EditIndex].FindControl("lblID")).Text), drpLang.SelectedValue);

        btnEdit.Visible = true;
        btnCancel.Visible = true;
        btnInsert.Visible = false;
        reqImage.Enabled = false;
        lblRecivedImage.Visible = true;
        lblRecivedVideo.Visible = true;

        ViewState["IsEdit"] = true;
        pnlCulture.Visible = true;
    }
    
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        btnEdit.Visible = false;
        btnCancel.Visible = false;
        btnInsert.Visible = true;
        reqImage.Enabled = true;

        lstVideos.EditIndex = -1;
        _setVideos(Convert.ToInt32(drpVideoAlbum.SelectedValue));
        ViewState["IsEdit"] = false;
        pnlCulture.Visible = false;
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if ((filupImage.HasFile && drpLang.SelectedValue != "fa-IR") || (radVideoUp.UploadedFiles.Count > 0 && drpLang.SelectedValue != "fa-IR"))
        {
            cliMsgBox.alert("ویرایش فایل تصویر و ویدئو بایستی با زبان فارسی انجام شود.\\nزبان فارسی را انتخاب و سپس فایل را انتخاب کنید\\nدرصورتیکه فقط متن را ویرایش می کنید هیچ فایلی را انتخاب نکنید");
            return;
        }
        int intID = Convert.ToInt32(((Label)lstVideos.Items[lstVideos.EditIndex].FindControl("lblID")).Text);
        string strUploadPath = String.Format("~/images/Uploads/VideoGallery/{0}/", drpVideoAlbum.SelectedValue);
        
        #region Manage Uploaded Video
        string VideoFileName = "";
        try
        {
            if (radmaskVideoLength.TextWithLiterals == "00:00:00")
            {
                cliMsgBox.alert("مدت ویدئو را وارد کنید");
                return;
            }
            ///Checking Uploaded Video => If there isn't any video in server, User must Upload Video
            if (radVideoUp.UploadedFiles.Count > 0)
                VideoFileName = _uploadVideo(strUploadPath);
            else
                VideoFileName = lblVideoFileName.Text;
        }
        catch (Exception ex)
        {
            cliMsgBox.alert(String.Format("خطا در ذخیره ویدئو در سرور:\\n{0}", ex.Message));
            return;
        }
        lblVideoFileName.Text = VideoFileName;
        lblRecivedVideo.Visible = true;
        #endregion

        #region Manage Uploaded Image
        string ImageFileName = filupImage.HasFile ?
            Business.Controls.CommonMethods.UploadFile(Server.MapPath(strUploadPath), filupImage.PostedFile, lblVideoFileName.Text) :
            lblImageFileName.Text;
        lblImageFileName.Text = ImageFileName;
        lblRecivedImage.Visible = true;
        #endregion

        string strTitle = txtTitle.Text;
        string strText = radEdtText.Content;

        try
        {
            Business.Videos.AllVideos alVideos = new Business.Videos.AllVideos();
            if (drpLang.SelectedValue == "fa-IR")
                alVideos.UpdateVideo(intID, ImageFileName, VideoFileName, strTitle, strText, radmaskVideoLength.TextWithLiterals, DateTime.Now);
            else
            {
                if (Business.OtherLanguages.SelectOtherLanguages(drpLang.SelectedValue, "VideoGallery", "Title", intID) != "")
                {
                    Business.OtherLanguages.UpdateOtherLanguage(drpLang.SelectedValue, "VideoGallery", "Title", intID, strTitle);
                    Business.OtherLanguages.UpdateOtherLanguage(drpLang.SelectedValue, "VideoGallery", "Describe", intID, strText);
                }
                else
                {
                    Business.OtherLanguages.InsertOtherLanguageData(drpLang.SelectedValue, "VideoGallery", "Title", intID, strTitle);
                    Business.OtherLanguages.InsertOtherLanguageData(drpLang.SelectedValue, "VideoGallery", "Describe", intID, strText);
                }
                alVideos.UpdateVideo(intID, ImageFileName, VideoFileName,radmaskVideoLength.TextWithLiterals, DateTime.Now);
            }
            txtTitle.Text = "";
            radEdtText.Content = "";
            radmaskVideoLength.Text = "000000";
            lstVideos.EditIndex = -1;
            ViewState["IsEdit"] = false;
            pnlCulture.Visible = false;

            drpLang.SelectedIndex = 0;
            _setVideos(Convert.ToInt32(drpVideoAlbum.SelectedValue));
            btnEdit.Visible = false;
            btnCancel.Visible = false;
            btnInsert.Visible = true;
            reqImage.Enabled = true;
            lblRecivedImage.Visible = false;
            lblRecivedVideo.Visible = false;
            lblImageFileName.Text = "";
            lblVideoFileName.Text = "";
            cliMsgBox.alert(String.Format("ویرایش ویدئو در زبان '{0}' با موفقیت انجام شد.", drpLang.SelectedItem.Text));
        }
        catch (Exception ex)
        {
            cliMsgBox.alert(String.Format("خطا در ویرایش اطلاعات ویدئو:\\n{0}", ex.Message));
        }
    }

    protected void btnInsert_Click(object sender, EventArgs e)
    {
        string strUploadPath = String.Format("~/images/Uploads/VideoGallery/{0}/", drpVideoAlbum.SelectedValue);
        if (drpLang.SelectedValue == "fa-IR")
        {
            #region Manage Uploaded Video
            string VideoFileName = "";
            try
            {
                ///Checking Uploaded Video => If there isn't any video in server, User must Upload Video
                if (radVideoUp.UploadedFiles.Count > 0)
                {
                    if (radVideoUp.UploadedFiles[0].GetExtension() != ".flv")
                    {
                        cliMsgBox.alert("فرمت فایل ویدئویی مناسب نیست");
                        return;
                    }
                    VideoFileName = _uploadVideo(strUploadPath);
                    lblVideoFileName.Text = VideoFileName;
                    lblRecivedVideo.Visible = true;
                }
                else if (lblVideoFileName.Text != "")
                    VideoFileName = lblVideoFileName.Text;
                else
                {
                    cliMsgBox.alert("فایل ویدئو را انتخاب کنید");
                    return;
                }

                if (radmaskVideoLength.TextWithLiterals == "00:00:00")
                {
                    cliMsgBox.alert("مدت ویدئو را وارد کنید");
                    return;
                }
            }
            catch(Exception ex)
            {
                cliMsgBox.alert(String.Format("خطا در ذخیره ویدئو در سرور:\\n{0}", ex.Message));
                return;
            }
            #endregion

            #region Manage Uploaded Image
            string ImageFileName = filupImage.HasFile ?
                Business.Controls.CommonMethods.UploadFile(Server.MapPath(strUploadPath), filupImage.PostedFile, lblVideoFileName.Text) :
                lblImageFileName.Text;
            lblImageFileName.Text = ImageFileName;
            reqImage.Enabled = false;
            lblRecivedImage.Visible = true;
            #endregion

            string strTitle = txtTitle.Text;
            string strText = radEdtText.Content;

            try
            {
                Business.Videos.AllVideos alVideos = new Business.Videos.AllVideos();
                alVideos.InsertVideo(Convert.ToInt32(drpVideoAlbum.SelectedItem.Value), ImageFileName, VideoFileName, radmaskVideoLength.TextWithLiterals, strTitle, strText, DateTime.Now);
                txtTitle.Text = "";
                radEdtText.Content = "";
                radmaskVideoLength.Text = "000000";
                _setVideos(Convert.ToInt32(drpVideoAlbum.SelectedValue));
                lstVideos.EditIndex = -1;
                reqImage.Enabled = true;
                lblRecivedImage.Visible = false;
                lblRecivedVideo.Visible = false;
                cliMsgBox.alert(String.Format("درج ویدئو در زبان {0} با موفقیت انجام شد.\\n اکنون میتوانید توضیحات ویدئو را به زبانهای دیگر وارد کنید.", drpLang.SelectedItem.Text));
            }
            catch (Exception ex)
            {
                cliMsgBox.alert(String.Format("خطا در درج اطلاعات ویدئو\\n{0}", ex.Message));
            }
        }
        else if (lblImageFileName.Text != "")
        {
            int intID = (new Business.Videos.AllVideos()).OneVideo(lblVideoFileName.Text);
            if (Business.OtherLanguages.SelectOtherLanguages(drpLang.SelectedValue, "VideoGallery", "Title", intID) == "")
            {
                Business.OtherLanguages.UpdateOtherLanguage(drpLang.SelectedValue, "VideoGallery", "Title", intID, txtTitle.Text);
                Business.OtherLanguages.UpdateOtherLanguage(drpLang.SelectedValue, "VideoGallery", "Describe", intID, radEdtText.Content);
                cliMsgBox.alert(String.Format("درج ویدئو در زبان {0} با موفقیت انجام شد.", drpLang.SelectedItem.Text));
            }
            else
            {
                cliMsgBox.alert(String.Format("قبلاً درج ویدئو در زبان {0} باانجام شده است.", drpLang.SelectedItem.Text));
            }
        }
        else
            cliMsgBox.alert("ابتدا ویدئو بایستی به زبان فارسی درج شود");
    }

    private string _uploadVideo(string UploadPath)
    {
        radVideoUp.TargetPhysicalFolder = UploadPath;

        string[] strFiles;
        try
        {
            strFiles = SaveUploadFiles(radVideoUp.UploadedFiles);
            return strFiles[0];
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private string[] SaveUploadFiles(Telerik.Web.UI.UploadedFileCollection uploadedFileCollection)
    {
        string[] _strF = new string[uploadedFileCollection.Count];
        for (int i = 0; i < uploadedFileCollection.Count; i++)
        {
            string videoFileName = Guid.NewGuid().ToString() + uploadedFileCollection[i].GetExtension();
            string fullPath = System.IO.Path.Combine(Server.MapPath(radVideoUp.TargetPhysicalFolder), videoFileName);
            uploadedFileCollection[i].SaveAs(fullPath, true);
            _strF[i] = videoFileName;
        }
        return _strF;
    }
}