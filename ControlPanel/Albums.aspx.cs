using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.UI;

public partial class ControlPanel_Albums : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = string.Format("{0} : پنل مدیریت وبسایت {1}", "مدیریت آلبوم ها", CultureResource.ResourceM.GetString("Company"));
        if (!Page.IsPostBack)
        {
            lblHeader.Text = "مدیریت آلبوم ها";
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
                    drpLang.Items[i].Text = strCults[i].Split(',')[1];
                    drpLang.Items[i].Value = strCults[i].Split(',')[0];
                    drpLang.Items[i].ForeColor = System.Drawing.Color.Red;
                }
            }
        #endregion
    }
        
    protected void btnInsert_Click(object sender, EventArgs e)
    {
        if (!Boolean.Parse(ViewState["IsEdit"].ToString()))
        {
            if (drpLang.SelectedValue == "fa-IR")
            {
                DataAccess.Album.AlbumTypeNames alType = (DataAccess.Album.AlbumTypeNames)Enum.Parse(typeof(DataAccess.Album.AlbumTypeNames), drpAlbumsTypes.SelectedValue, true);
                string strUploadPath = Server.MapPath(Business.Albums.AlbumBasePath(alType) + "/");
                string FileName = Business.Controls.CommonMethods.UploadFile(strUploadPath, filUpThumb.PostedFile, Guid.NewGuid().ToString());
                try
                {
                    List<string> lstTitles = new List<string>();
                    lstTitles.Add(txtTitle.Text);
                    List<string> lstCult = new List<string>();
                    lstCult.Add("en-US");

                    Business.Albums.InsertAlbum(alType, txtTitle.Text, FileName);
                    _bindGrid(alType);

                    cliMsgBox.alert("درج آلبوم جدید با موفقیت انجام شد \\n اکنون می توانید در حالت ویرایش این آلبوم، عنوان را در زبانهای دیگر اضافه کنید");
                }
                catch (Exception ex)
                {
                    cliMsgBox.alert(ex.Message);
                }
            }
            else
            {
                cliMsgBox.alert("درج آلبوم بایستی ابتدا در زبان فارسی انجام شود");
                return;
            }
        }
        else
            try
            {
                DataAccess.Album.AlbumTypeNames alType = (DataAccess.Album.AlbumTypeNames)Enum.Parse(typeof(DataAccess.Album.AlbumTypeNames), drpAlbumsTypes.SelectedValue, true);
                string FileName = lblThumbName.Text;
                if (filUpThumb.HasFile)
                {
                    try
                    {
                        try
                        {
                            Business.Controls.CommonMethods.DeleteFile(Server.MapPath(Business.Albums.AlbumBasePath(alType)), lblThumbName.Text);
                        }
                        catch { int i = 0; }

                        string strUploadPath = Server.MapPath(Business.Albums.AlbumBasePath(alType) + "/");
                        FileName = Business.Controls.CommonMethods.UploadFile(strUploadPath, filUpThumb.PostedFile, Guid.NewGuid().ToString());
                    }
                    catch (Exception ex) { }
                }
                Business.Albums.UpdateAlbum(Convert.ToInt32(((Label)grdAlbums.SelectedRow.FindControl("lblId")).Text), txtTitle.Text, FileName, drpLang.SelectedValue);
                _bindGrid(alType);
                cliMsgBox.alert(String.Format("ویرایش آلبوم در زبان {0} با موفقیت انجام شد", drpLang.SelectedItem.Text));
                btnCancel.Visible = false;
            }
            catch (Exception ex)
            {
                cliMsgBox.alert(ex.Message);
            }

        drpLang.Items[drpLang.SelectedIndex].ForeColor = System.Drawing.Color.Green;
        txtTitle.Text = "";
        imgThumb.Visible = false;
        reqValThum.Enabled = true;
    }
        
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtTitle.Text = "";
        ViewState["IsEdit"] = false;
        btnCancel.Visible = false;
        reqValThum.Enabled = true;
        imgThumb.Visible = false;

        _bindGrid((DataAccess.Album.AlbumTypeNames)Enum.Parse(typeof(DataAccess.Album.AlbumTypeNames), drpAlbumsTypes.SelectedValue));
        grdAlbums.SelectedIndex = -1;
    }

    protected void drpAlbumsTypes_SelectedIndexChanged(object sender, EventArgs e)
    {
        _bindGrid((DataAccess.Album.AlbumTypeNames)Enum.Parse(typeof(DataAccess.Album.AlbumTypeNames), drpAlbumsTypes.SelectedValue));
        if (drpAlbumsTypes.SelectedValue != "0")
            pnlEdit.Visible = true;
        else
            pnlEdit.Visible = false;

        for (int i = 0; i < drpLang.Items.Count; i++)
            drpLang.Items[i].ForeColor = System.Drawing.Color.Red;
    }

    protected void grdAlbums_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdAlbums.PageIndex = e.NewPageIndex;
        grdAlbums.SelectedIndex = -1;
        _bindGrid((DataAccess.Album.AlbumTypeNames)Enum.Parse(typeof(DataAccess.Album.AlbumTypeNames), drpAlbumsTypes.SelectedValue));
    }

    protected void grdAlbums_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Select":
                {
                    DataAccess.Album al = Business.Albums.OnAlbum(Convert.ToInt32(e.CommandArgument), "fa-IR");
                    DataAccess.Album.AlbumTypeNames alType = (DataAccess.Album.AlbumTypeNames)Enum.Parse(typeof(DataAccess.Album.AlbumTypeNames), drpAlbumsTypes.SelectedValue);
                    drpLang.SelectedIndex = 0;
                    txtTitle.Text = al.AlbumTitle;
                    imgThumb.ImageUrl = String.Format("~/ImageReSizer.ashx?fp={0}&w={1}&h={2}", Business.Albums.AlbumBasePath(alType).Replace("~", "") + "/" + al.Thumb, 200, 200);
                    btnCancel.Visible = true;
                    reqValThum.Enabled = false;
                    imgThumb.Visible = true;
                    _bindGrid(alType);
                    ViewState["IsEdit"] = true;
                    for (int i = 0; i < drpLang.Items.Count; i++)
                        drpLang.Items[i].ForeColor = System.Drawing.Color.Red;
                    
                    break;
                }
            case "Visible":
                {
                    try
                    {
                        Business.Albums.IsActive(Convert.ToInt32(e.CommandArgument.ToString().Split(';')[0]), !Boolean.Parse(e.CommandArgument.ToString().Split(';')[1]));
                        grdAlbums_PageIndexChanging(new object(), new GridViewPageEventArgs(grdAlbums.PageIndex));
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    break;
                }
        }
    }

    private void _bindGrid(DataAccess.Album.AlbumTypeNames albumTypeName)
    {
        if (albumTypeName != 0)
        {
            grdAlbums.PageIndex = grdAlbums.PageIndex;
            grdAlbums.DataSource = Business.Albums.GetAlbumByType(albumTypeName, "fa-IR");
            grdAlbums.DataBind();
            grdAlbums.SelectedIndex = -1;
        }
        else
        {
            grdAlbums.DataSource = null;
            grdAlbums.DataBind();
        }

        ViewState["IsEdit"] = false;
    }

    protected void grdAlbums_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataAccess.Album.AlbumTypeNames alType = (DataAccess.Album.AlbumTypeNames)Enum.Parse(typeof(DataAccess.Album.AlbumTypeNames), drpAlbumsTypes.SelectedValue);
        try
        {
            Business.Albums.DeleteAlbum(alType, Convert.ToInt32(((Label)grdAlbums.Rows[e.RowIndex].FindControl("lblID")).Text),
                                        Server.MapPath(Business.UI.PublicFunctions.UploadBasePath()));
            _bindGrid(alType);
            txtTitle.Text = "";
            imgThumb.Visible = false;
            reqValThum.Enabled = true;
            btnCancel.Visible = false;
            cliMsgBox.alert("آلبوم مورد نظر با موفقیت حذف شد");
        }
        catch (Exception ex)
        {
            cliMsgBox.alert(String.Format("خطا در حذف آلبوم:\n{0}", ex.Message));
        }
    }

    protected void grdAlbums_RowCreated(object sender, GridViewRowEventArgs e)
    {
        Literal ref_LitRowNumberNormal = new Literal();
        if (e.Row.DataItem != null)
        {
            ref_LitRowNumberNormal = (Literal)e.Row.FindControl("litRowNumberNormal");
            if (grdAlbums.PageIndex != 0)
                ref_LitRowNumberNormal.Text = GridViewRowsIndexing(grdAlbums.PageIndex + 1, 5, e.Row.RowIndex);
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

    protected void drpLang_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (Boolean.Parse(ViewState["IsEdit"].ToString()) && grdAlbums.SelectedIndex != -1)
        {
            int AlbumId = Convert.ToInt32(((Label)grdAlbums.SelectedRow.FindControl("lblId")).Text);
            DataAccess.Album al = Business.Albums.OnAlbum(AlbumId, drpLang.SelectedValue);
            txtTitle.Text = al.AlbumTitle;
        }
        else
        {
            drpLang.SelectedIndex = 0;
            cliMsgBox.alert("درج آلبوم جدید ابتدا بایستی به زبان فارسی صورت گیرد\\nپس از آن با ویرایش آلبوم ثبت شده میتوانید زبانهای دیگر را برای آن تنظیم نمایید");
        }
    }
}