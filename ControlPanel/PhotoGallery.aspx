<%@ Page Title="" Language="C#" MasterPageFile="~/AdminCPMaster.master" AutoEventWireup="true" CodeFile="PhotoGallery.aspx.cs" Inherits="ControlPanel_PhotoGallery" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="BunnyBear" Assembly="msgBox" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cntphSideBar" runat="Server">
    <table align="center">
        <tr>
            <td>
                <asp:ImageButton ID="imgbtnAlbums" runat="server" ImageUrl="~/ControlPanel/Images/Albums.png"
                    PostBackUrl="~/ControlPanel/Albums.aspx" Width="60px" />
            </td>
        </tr>
        <tr>
            <td class="titleBar">
                مدیریت آلبومها
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphInnerTitle" Runat="Server">
    <div style="float: right; color:White;" class="HeaderTop">
        <asp:Label ID="lblHeader" runat="server"></asp:Label>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphMain" Runat="Server">
    <table style="width: 100%;">
        <tr>
            <td colspan="2">
                <div class="HeaderTop" dir="rtl" style="padding:10px 10px 10px 0;">
                    ابتدا دسته محصولات را انتخاب و سپس در آن دسته محصول جدیدی را درج و یا ویرایش نمایید.</div>
            </td>
        </tr>
        <tr>
            <td style="padding-top: 5px; width:60%" align='right'>
                <asp:DropDownList Style="font-family: Tahoma;" ID="drpPhotoAlbum" runat="server" AutoPostBack="True"
                    OnSelectedIndexChanged="drpPhotoAlbum_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td style="padding-top: 5px; text-align:left;" class="HeaderTop" dir="rtl">درج و ویرایش محصولات در این دسته: </td>
        </tr>
		<tr>
			<td style="padding-top: 5px;" align='right' colspan="2" dir="rtl">
                <asp:Literal ID="ltrAlbumNull" runat="server" Visible="false"><div class="HeaderTop" style="color:Red;"> آلبومی  تعریف نشده است و یا آلبومهای تصویر موجود غیر فعال هستند.<br/> برای تعریف و ویرایش آلبومها به <a href="Albums.aspx" style="color:Green;">مدیریت آلبومها</a> مراجعه کنید.</div></asp:Literal>
            </td>
		</tr>
        <tr>
            <td colspan="2">
                <asp:Panel ID="pnlEditPhoto" runat="server" Visible="false">
                <table>
                    <tr>
                        <td colspan="2">
                            <img alt="" style="width: 348px; height: 1px; padding: 5px 0 5px 0;" src="Images/line.png" />
                            <asp:Label ID="lblFileName" runat="server" Text="" Visible="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:RequiredFieldValidator ID="reqImage" runat="server" ControlToValidate="filupImage"
                                ErrorMessage="&lt;strong&gt;تصویر محصول را وارد کنید&lt;/strong&gt;" Text="&lt;strong&gt;تصویر محصول را وارد کنید&lt;/strong&gt;"
                                ValidationGroup="Fields"></asp:RequiredFieldValidator>
                            <asp:FileUpload ID="filupImage" runat="server" />
                        </td>
                        <td class="Header" style="text-align: left; direction: rtl;">
                            تصویر:</td>
                    </tr>
                    <tr>
						<td align="right" colspan="2">
                            <asp:Panel ID="pnlCulture" runat="server" Visible="false">
                                <table style="width:100%">
                                    <tr>
                                        <td colspan="2" align="center">
                                            <img alt="" style="width: 348px; height: 1px; padding: 5px 0 5px 0;" src="Images/line.png" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <telerik:RadComboBox ID="drpLang" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpLang_SelectedIndexChanged">
                                            </telerik:RadComboBox>
                                        </td>
                                        <td class="Header" style="text-align: left; direction: rtl; width: 50px">
                                            زبان:
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
						</td>
					</tr>
                    <tr>
                        <td align="right">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtTitle"
                                ErrorMessage="&lt;strong&gt;عنوان را وارد کنید&lt;/strong&gt;" Text="&lt;strong&gt;عنوان را وارد کنید&lt;/strong&gt;"
                                ValidationGroup="Fields"></asp:RequiredFieldValidator>
                            <asp:TextBox ID="txtTitle" runat="server" Width="200px"></asp:TextBox>
                        </td>
                        <td class="Header" style="text-align: left; direction: rtl;">
                            عنوان:
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <img alt="" style="width: 348px; height: 1px; padding: 5px 0 5px 0;" src="Images/line.png" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="radEdtText"
                                ErrorMessage="<strong>متن را وارد کنید</strong>" Text="<strong>متن را وارد کنید</strong>"
                                ValidationGroup="Fields"></asp:RequiredFieldValidator>
                        </td>
                        <td class="Header" style="text-align: left; direction: rtl;">
                            متن:
                        </td>
                    </tr>
                    <tr>
                        <td align="right" colspan="2">
                            <telerik:RadEditor runat="server" ID="radEdtText" SkinID="DefaultSetOfTools" Width="650px"
                                EnableResize="false" AllowScripts="True">
                                <Content>
                                </Content>
                                <ImageManager ViewPaths="~/images/Uploads" UploadPaths="~/images/Uploads/Images" DeletePaths="~/images/Uploads/Images" />
                                <FlashManager ViewPaths="~/images/Uploads" UploadPaths="~/images/Uploads/FLV" DeletePaths="~/images/Uploads/FLV" />
                                <MediaManager ViewPaths="~/images/Uploads" UploadPaths="~/images/Uploads/Media" DeletePaths="~/images/Uploads/Media" />
                                <DocumentManager ViewPaths="~/images/Uploads" UploadPaths="~/images/Uploads/Doc" DeletePaths="~/images/Uploads/Doc" />
                            </telerik:RadEditor>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <img alt="" style="width: 348px; height: 1px; padding: 5px 0 5px 0;" src="Images/line.png" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="padding-right: 10px">
                            <asp:Label ID="lblDate" runat="server" ForeColor="Red" Text=""></asp:Label>
                        </td>
                        <td class="Header" style="text-align: left; direction: rtl;">
                            تاریخ:</td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2">
                            <div style="width: 200px;">
                                <asp:Button Style="float: left;" ID="btnEdit" runat="server" CssClass="submit-login"
                                    Font-Names="Tahoma" Visible="false" ForeColor="White" ValidationGroup="Fields"
                                    Text="ویرایش" onclick="btnEdit_Click" />
                                <asp:Button Style="float: right;" ID="btnCancel" runat="server" CssClass="submit-login"
                                    Font-Names="Tahoma" Visible="false" ForeColor="White" Text="لغو" ValidationGroup="Fields"
                                    OnClick="btnCancel_Click" />
                            </div>
                            <div style="width: 70px;">
                                <asp:Button ID="btnInsert" runat="server" CssClass="submit-login" Font-Names="Tahoma"
                                    ValidationGroup="Fields" ForeColor="White" Text="درج" OnClick="btnInsert_Click" />
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <img alt="" style="width: 348px; height: 1px; padding: 5px 0 5px 0;" src="Images/line.png" />
                        </td>
                    </tr>
                </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td colspan="2">
            <asp:ListView ID="lstPhotos" runat="server" GroupItemCount="3" 
                    onitemdeleting="lstPhotos_ItemDeleting" 
                    onitemediting="lstPhotos_ItemEditing">
                <LayoutTemplate>
                    <table cellspacing="5" runat="server" id="tblProducts" width="100%"
                        style="padding-top: 8px;">
                        <tr runat="server" id="groupPlaceholder">
                        </tr>
                    </table>
                    <div align="center">
                        <asp:DataPager runat="server" ID="prodPager" PageSize="9" QueryStringField="pageNo">
                            <Fields>
                                <asp:NextPreviousPagerField ButtonType="Image" FirstPageImageUrl="~/images/first.png"
                                    ShowFirstPageButton="true" PreviousPageImageUrl="~/images/prev.png"
                                    ShowLastPageButton="false" ShowNextPageButton="false" FirstPageText='ابتدا' PreviousPageText='قبلی'/>
                                <asp:NumericPagerField ButtonCount="5" NumericButtonCssClass="title" NextPreviousButtonCssClass="title" />
                                <asp:NextPreviousPagerField ButtonType="Image" ShowLastPageButton="true" ShowNextPageButton="true" LastPageText='انتها'  NextPageText='بعدی'
                                    ShowPreviousPageButton="false" LastPageImageUrl="~/images/last.png" NextPageImageUrl="~/images/next.png" />
                            </Fields>
                        </asp:DataPager>
                    </div>
                </LayoutTemplate>
                <GroupTemplate>
                    <tr runat="server" id="productRow" width="100%">
                        <td runat="server" id="itemPlaceholder" width="100%">
                        </td>
                    </tr>
                </GroupTemplate>
                <ItemTemplate>
                    <td valign="top" style="padding-bottom: 15px; border:1px solid orange" align="center">
                    <table>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lblID" runat="server" Visible="false" Text='<%# Eval("ID") %>'></asp:Label>
                                <asp:Image ID="Image1" runat="server" ToolTip='<%# Eval("FileName") %>' ImageUrl='<%# string.Format("~/ImageReSizer.ashx?aid={0}&if={1}&w={2}&h={3}&t={4}",Eval("AlbumID"), Eval("FileName"), 180, 180, "i") %>' />
                                <br />
                                <asp:Label ID="Label3" colspan="2" runat="server" Text='<%# Eval("Title")%>'></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-top:5px;">
                                <asp:Button style="font-weight:normal;" CommandName="Edit" ID="btnEdit" runat="server" Text="ویرایش" CssClass="submit-login" />
                            </td>
                            <td style="padding-top:5px">
                                <asp:Button style="font-weight:normal;" CommandName="Delete" ID="btnDelete" runat="server" Text="حذف" CssClass="submit-login" OnClientClick=<%# string.Format("return confirm(\"آیا از حذف محصول '{0}'  مطمئن هستید؟ \");", Eval("Title")) %> />
                            </td>
                        </tr>
                    </table>
                    </td>
                </ItemTemplate>
                <EditItemTemplate>
                    <td valign="top" style="padding-bottom: 15px; border:1px solid orange; background-color:#FFFF99;" align="center">
                    <table>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lblID" runat="server" Visible="false" Text='<%# Eval("ID") %>'></asp:Label>
                                <asp:Label ID="lblFATitle" runat="server" Visible="false" Text='<%# Eval("Title") %>'></asp:Label>
                                <asp:Label ID="lblFAText" runat="server" Visible="false" Text='<%# Eval("Describe") %>'></asp:Label>
                                <asp:Image ID="Image1" runat="server" ToolTip='<%# Eval("FileName") %>' ImageUrl='<%# string.Format("~/ImageReSizer.ashx?aid={0}&if={1}&w={2}&h={3}&t={4}",Eval("AlbumID"), Eval("FileName"), 180, 180, "i") %>' />
                                <br />
                                <asp:Label ID="Label3" colspan="2" runat="server" Text='<%# Eval("Title")%>'></asp:Label>
                                <div style="color: Red; text-align: center; width: 100%" class="HeaderTop">
                                    در حال ویرایش</div>
                            </td>
                        </tr>
                    </table>
                    </td>
                </EditItemTemplate>
            </asp:ListView>
            </td>
        </tr>
        </table>
        <cc1:msgBox ID="cliMsgBox" Style="z-index: 103; left: 536px; position: absolute;
        top: 184px" runat="server"></cc1:msgBox>
</asp:Content>

