using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.UI;

public partial class ControlPanel_Comments : System.Web.UI.Page
{
    private string _MakeJSdata(string Comment)
    {
        string strComNo = "ComNo";
        Page.ClientScript.RegisterHiddenField(strComNo + grdComments.Rows.Count.ToString(), Comment);
        return string.Format("document.getElementById('{0}').value", strComNo + grdComments.Rows.Count.ToString());
    }

    protected string _MakeContactShow(string Comment, string SenderName, string e_Mail, DateTime SentDate)
    {
        #region FaTemplate
        //string faTemp = "<table class='text'>" +
        //"<tr><td dir='rtl' align='right' class='HeaderTop'>" +
        //    "<div style='float:right'>{0} گفته:</div>" +
        //"</td></tr>" +
        //"<tr>" +
        //    "<td class='ShowRTL'>" +
        //        "{2}" +
        //    "</td>" +
        //"</tr>" +
        //"<tr>" +
        //    "<td dir='rtl' align='left' class='text' style='border-top-width:1px; border-top-style:solid; border-top-color:Orange; font-size:11px'>" +
        //        "<div style='float:left'>ارسال در: {3}</div>" +
        //        "<div style='float:left'>ایمیل: {1}</div>" +
        //    "</td>" +
        //"</tr>" +
        //"</table>";
        #endregion

        return _MakeJSdata(String.Format(CultureResource.ResourceM.GetString("cmnAdminViewTemp"), SenderName, e_Mail, Comment, CultureResource.DateByCulture(SentDate, drpLang.SelectedValue)));
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = string.Format("{0} : پنل مدیریت وبسایت {1}", "مدیریت نظرات کاربران", CultureResource.ResourceM.GetString("Company"));
        if (!Page.IsPostBack)
        {
            lblHeader.Text = "مدیریت نظرات کاربران";
            _bindGrid();
        }
    }

    private void _bindGrid()
    {
        Business.Comments.Comments cmnt = new Business.Comments.Comments();
        grdComments.DataSource = cmnt.GetAllCommnets(drpLang.SelectedValue);
        grdComments.DataBind();
    }

    protected void grdComments_RowCreated(object sender, GridViewRowEventArgs e)
    {
        Literal ref_LitRowNumberNormal = new Literal();
        if (e.Row.DataItem != null)
        {
            ref_LitRowNumberNormal = (Literal)e.Row.FindControl("litRowNumberNormal");
            if (grdComments.PageIndex != 0)
                ref_LitRowNumberNormal.Text = GridViewRowsIndexing(grdComments.PageIndex + 1, 5, e.Row.RowIndex);
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

    protected void drpLang_SelectedIndexChanged(object sender, EventArgs e)
    {
        CultureResource.SetCulture(drpLang.SelectedValue, Context);
        _bindGrid();
    }

    protected void grdComments_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Visible")
        {
            int rowIndx = Convert.ToInt32(e.CommandArgument.ToString());
            int cmntID = Convert.ToInt32(((Label)grdComments.Rows[rowIndx].FindControl("lblID")).Text);
            Boolean IsVis = Boolean.Parse(((Label)grdComments.Rows[rowIndx].FindControl("lblVisible")).Text);
            Business.Comments.Comments.SetCommentVisible(cmntID, !IsVis);
            _bindGrid();
            if (!IsVis)
                cliMsgBox.alert("نظر اکنون قابل نمایش است");
            else
                cliMsgBox.alert("نظر اکنون غیر قابل نمایش است");
        }
    }

    protected void grdComments_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Business.Comments.Comments.DeleteComment(Convert.ToInt32(((Label)grdComments.Rows[e.RowIndex].FindControl("lblID")).Text));
        _bindGrid();
        cliMsgBox.alert("نظر با موفقیت حذف شد");
    }
}