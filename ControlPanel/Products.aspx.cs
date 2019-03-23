using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.UI;

public partial class ControlPanel_Products : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = string.Format("{0} : پنل مدیریت وبسایت {1}", "مدیریت محصولات", CultureResource.ResourceM.GetString("Company"));
        if (!Page.IsPostBack)
        {
            lblHeader.Text = "مدیریت محصولات";
            if (!String.IsNullOrEmpty(Request.QueryString["pageNo"]) && drpProductAlbum.SelectedValue != "-1")
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

        #region Load Products Albums
        drpProductAlbum.DataTextField = "AlbumTitle";
        drpProductAlbum.DataValueField = "ID";
        List<DataAccess.Album> albums = Business.Albums.GetAlbumByType(DataAccess.Album.AlbumTypeNames.Products, (Session["Cult"].ToString())).Where(a => a.IsActive == true).ToList<DataAccess.Album>();
        drpProductAlbum.DataSource = albums;
        drpProductAlbum.DataBind();

        if (drpProductAlbum.Items.Count > 0)
        {
            if (!Reset)
            {
                pnlEditVideo.Visible = true;
                ltrAlbumNull.Visible = false;
                _setProducts(Convert.ToInt32(drpProductAlbum.SelectedValue));
                return;
            }
            drpProductAlbum.Items.Insert(0, new ListItem("انتخاب کنید", "-1"));
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
            int ProductID = Convert.ToInt32(((Label)lstVideos.Items[lstVideos.EditIndex].FindControl("lblID")).Text);
            DataAccess.Products Product = new Business.Products.AllProducts().GetOneProduct(drpLang.SelectedValue, ProductID);
            txtTitle.Text = Product.Title;
            radEdtText.Content = Product.Describ;
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
            txtTitle.Text = Business.OtherLanguages.SelectOtherLanguages(Culture, "Products", "Title", ID);
            radEdtText.Content = Business.OtherLanguages.SelectOtherLanguages(Culture, "Products", "Describ", ID);
        }
    }

    protected void drpProductAlbum_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpProductAlbum.SelectedValue != "-1")
        {
            pnlEditVideo.Visible = true;
            ltrAlbumNull.Visible = false;
            _setProducts(Convert.ToInt32(drpProductAlbum.SelectedValue));
        }
        else
        {
            pnlEditVideo.Visible = false;
            ltrAlbumNull.Visible = true;
        }
    }

    private void _setProducts(int AlbumId)
    {
        Business.Products.AllProducts alProducts = new Business.Products.AllProducts(Session["Cult"].ToString(), AlbumId);
        lstVideos.DataSource = alProducts.ProductData;
        lstVideos.DataBind();
    }

    protected void lstVideos_ItemDeleting(object sender, ListViewDeleteEventArgs e)
    {
        string strProduct = ((Label)lstVideos.Items[e.ItemIndex].FindControl("Label3")).Text;
        try
        {
            int intID = Convert.ToInt32(((Label)lstVideos.Items[e.ItemIndex].FindControl("lblID")).Text);
            Business.Products.AllProducts alProducts = new Business.Products.AllProducts();
            alProducts.DeleteProduct(Server.MapPath(String.Format("~/images/Uploads/Products/{0}/", drpProductAlbum.SelectedValue)), intID);
            _setProducts(Convert.ToInt32(drpProductAlbum.SelectedValue));
            lblImageFileName.Text = "";
            cliMsgBox.alert(string.Format("حذف محصول با عنوان '{0}' با موفقیت انجام شد", strProduct));
        }
        catch (Exception ex)
        {
            cliMsgBox.alert(String.Format("خطا در حذف محصول:\\n{0}", ex.Message));
        }
    }

    protected void lstVideos_ItemEditing(object sender, ListViewEditEventArgs e)
    {
        lstVideos.EditIndex = e.NewEditIndex;
        _setProducts(Convert.ToInt32(drpProductAlbum.SelectedValue));
        drpLang.SelectedIndex = 0;
        lblImageFileName.Text = ((Label)lstVideos.Items[lstVideos.EditIndex].FindControl("lblImageFileNameLST")).Text;
        _fillFields(lstVideos.EditIndex, Convert.ToInt32(((Label)lstVideos.Items[lstVideos.EditIndex].FindControl("lblID")).Text), drpLang.SelectedValue);

        btnEdit.Visible = true;
        btnCancel.Visible = true;
        btnInsert.Visible = false;
        reqImage.Enabled = false;
        lblRecivedImage.Visible = true;

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
        _setProducts(Convert.ToInt32(drpProductAlbum.SelectedValue));
        ViewState["IsEdit"] = false;
        pnlCulture.Visible = false;
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if ((filupImage.HasFile && drpLang.SelectedValue != "fa-IR"))
        {
            cliMsgBox.alert("ویرایش فایل تصویر بایستی با زبان فارسی انجام شود.\\nزبان فارسی را انتخاب و سپس فایل را انتخاب کنید\\nدرصورتیکه فقط متن را ویرایش می کنید هیچ فایلی را انتخاب نکنید");
            return;
        }
        int intID = Convert.ToInt32(((Label)lstVideos.Items[lstVideos.EditIndex].FindControl("lblID")).Text);
        string strUploadPath = String.Format("~/images/Uploads/Products/{0}/", drpProductAlbum.SelectedValue);
        
        #region Manage Uploaded Image
        string ImageFileName = filupImage.HasFile ?
            Business.Controls.CommonMethods.UploadFile(Server.MapPath(strUploadPath), filupImage.PostedFile, lblImageFileName.Text) :
            lblImageFileName.Text;
        lblImageFileName.Text = ImageFileName;
        lblRecivedImage.Visible = true;
        #endregion

        string strTitle = txtTitle.Text;
        string strText = radEdtText.Content;

        try
        {
            Business.Products.AllProducts alVideos = new Business.Products.AllProducts();
            if (drpLang.SelectedValue == "fa-IR")
                alVideos.UpdateProduct(intID, ImageFileName, strTitle, strText, DateTime.Now);
            else
            {
                if (Business.OtherLanguages.SelectOtherLanguages(drpLang.SelectedValue, "Products", "Title", intID) != "")
                {
                    Business.OtherLanguages.UpdateOtherLanguage(drpLang.SelectedValue, "Products", "Title", intID, strTitle);
                    Business.OtherLanguages.UpdateOtherLanguage(drpLang.SelectedValue, "Products", "Describ", intID, strText);
                }
                else
                {
                    Business.OtherLanguages.InsertOtherLanguageData(drpLang.SelectedValue, "Products", "Title", intID, strTitle);
                    Business.OtherLanguages.InsertOtherLanguageData(drpLang.SelectedValue, "Products", "Describ", intID, strText);
                }
                alVideos.UpdateProduct(intID, ImageFileName, DateTime.Now);
            }
            txtTitle.Text = "";
            radEdtText.Content = "";
            lstVideos.EditIndex = -1;
            ViewState["IsEdit"] = false;
            pnlCulture.Visible = false;

            drpLang.SelectedIndex = 0;
            _setProducts(Convert.ToInt32(drpProductAlbum.SelectedValue));
            btnEdit.Visible = false;
            btnCancel.Visible = false;
            btnInsert.Visible = true;
            reqImage.Enabled = true;
            lblRecivedImage.Visible = false;
            lblImageFileName.Text = "";
            cliMsgBox.alert(String.Format("ویرایش محصول در زبان '{0}' با موفقیت انجام شد.", drpLang.SelectedItem.Text));
        }
        catch (Exception ex)
        {
            cliMsgBox.alert(String.Format("خطا در ویرایش اطلاعات محصول:\\n{0}", ex.Message));
        }
    }

    protected void btnInsert_Click(object sender, EventArgs e)
    {
        string strUploadPath = String.Format("~/images/Uploads/Products/{0}/", drpProductAlbum.SelectedValue);
        if (drpLang.SelectedValue == "fa-IR")
        {
            #region Manage Uploaded Image
            //string ImageFileName = filupImage.HasFile ?
            //    Business.Controls.CommonMethods.UploadFile(Server.MapPath(strUploadPath), filupImage.PostedFile, lblImageFileName.Text) :
            //    lblImageFileName.Text;

            string ImageFileName = Business.Controls.CommonMethods.UploadFile(strUploadPath, filupImage.PostedFile, Guid.NewGuid().ToString());

            lblImageFileName.Text = ImageFileName;
            reqImage.Enabled = false;
            lblRecivedImage.Visible = true;
            #endregion

            string strTitle = txtTitle.Text;
            string strText = radEdtText.Content;

            try
            {
                Business.Products.AllProducts alVideos = new Business.Products.AllProducts();
                alVideos.InsertProduct(Convert.ToInt32(drpProductAlbum.SelectedItem.Value), ImageFileName, strTitle, strText, DateTime.Now);
                txtTitle.Text = "";
                radEdtText.Content = "";
                _setProducts(Convert.ToInt32(drpProductAlbum.SelectedValue));
                lstVideos.EditIndex = -1;
                reqImage.Enabled = true;
                lblRecivedImage.Visible = false;
                cliMsgBox.alert(String.Format("درج محصول در زبان {0} با موفقیت انجام شد.\\n اکنون میتوانید توضیحات محصول را به زبانهای دیگر وارد کنید.", drpLang.SelectedItem.Text));
            }
            catch (Exception ex)
            {
                cliMsgBox.alert(String.Format("خطا در درج اطلاعات محصول\\n{0}", ex.Message));
            }
        }
        else if (lblImageFileName.Text != "")
        {
            int intID = (new Business.Products.AllProducts()).GetOneProduct(lblImageFileName.Text);
            if (Business.OtherLanguages.SelectOtherLanguages(drpLang.SelectedValue, "Products", "Title", intID) == "")
            {
                Business.OtherLanguages.UpdateOtherLanguage(drpLang.SelectedValue, "Products", "Title", intID, txtTitle.Text);
                Business.OtherLanguages.UpdateOtherLanguage(drpLang.SelectedValue, "Products", "Describ", intID, radEdtText.Content);
                cliMsgBox.alert(String.Format("درج محصول در زبان {0} با موفقیت انجام شد.", drpLang.SelectedItem.Text));
            }
            else
            {
                cliMsgBox.alert(String.Format("قبلاً درج محصول در زبان {0} باانجام شده است.", drpLang.SelectedItem.Text));
            }
        }
        else
            cliMsgBox.alert("ابتدا محصول بایستی به زبان فارسی درج شود");
    }
}