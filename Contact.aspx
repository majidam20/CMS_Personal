<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Contact.aspx.cs" Inherits="Contact" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="Business.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="Scripts/lightBox/js/prototype.js"></script>
    <script type="text/javascript" src="Scripts/lightBox/js/scriptaculous.js?load=effects,builder"></script>
    <script type="text/javascript" src="Scripts/lightBox/js/lightbox.js"></script>
    <link href="Scripts/lightBox/css/lightbox.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="TitleContent" runat="Server">
    <h1 class="Title" style='float: <%# CultureResource.ResourceM.GetString("Align")%>;
        text-align: <%# CultureResource.ResourceM.GetString("Align")%>' dir='<%# CultureResource.ResourceM.GetString("Direction")%>'>
        <asp:Literal ID="ltrHeader" runat="server"></asp:Literal>
    </h1>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <table style="width: 100%;" cellpadding="0" cellspacing="0">
        <tr>
            <td colspan="3" style="background-color: #FFFFFF" align="center" class="content"
                valign="top">
                <div>
                    <table style="width: 100%;" cellpadding="0" cellspacing="0" dir='<%# CultureResource.ResourceM.GetString("Direction")%>'>
                        <tr>
                            <td style='height: 400px; text-align: <%# CultureResource.ResourceM.GetString("Align")%>;
                                width: 48%;' dir='<%# CultureResource.ResourceM.GetString("Direction")%>' class="Text"
                                valign="top">
                                <div style="float: <%# CultureResource.ResourceM.GetString("Align")%>; width: 45%">
                                    <asp:Literal ID="ltrText" runat="server"></asp:Literal>
                                </div>
                                <table dir='<%=CultureResource.ResourceM.GetString("Direction") %>' style="float: <%# CultureResource.ResourceM.GetString("AlignN")%>;
                                    width: 55%">
                                    <tr>
                                        <td style="padding: 5px 5px 5px 5px; font-family: Tahoma;" align='<%= CultureResource.ResourceM.GetString("Align") %>'
                                            colspan="2">
                                            <asp:Label ID="lblSerPM" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align='<%= CultureResource.ResourceM.GetString("AlignN") %>' style="width: 130px;">
                                            <div class="Header">
                                                <%=CultureResource.ResourceM.GetString("Fullname") %></div>
                                        </td>
                                        <td align='<%= CultureResource.ResourceM.GetString("Align") %>' dir='<%= CultureResource.ResourceM.GetString("Direction") %>'>
                                            <asp:TextBox ID="txtName" runat="server" Width="250px" Font-Names="Tahoma" CssClass="textbox"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtName"
                                                Display="Dynamic" ErrorMessage="*" ValidationGroup="ContactUS" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align='<%= CultureResource.ResourceM.GetString("AlignN") %>'>
                                            <div class="Header">
                                                <%=CultureResource.ResourceM.GetString("e_Mail") %>
                                            </div>
                                        </td>
                                        <td align='<%= CultureResource.ResourceM.GetString("Align") %>' dir='<%= CultureResource.ResourceM.GetString("Direction") %>'>
                                            <asp:TextBox ID="txtMail" runat="server" Width="250px" Style="text-align: left; direction: ltr"
                                                Font-Names="Tahoma" CssClass="textbox"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtMail"
                                                Display="Dynamic" ErrorMessage="*" ValidationGroup="ContactUS" ForeColor="Red"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="regularExpMail" runat="server" ControlToValidate="txtMail"
                                                Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                ValidationGroup="ContactUS" ErrorMessage='<%= CultureResource.ResourceM.GetString("vMailFormat") %>'
                                                ForeColor="Red"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align='<%= CultureResource.ResourceM.GetString("AlignN") %>'>
                                            <div class="Header">
                                                <%=CultureResource.ResourceM.GetString("Subject") %>
                                            </div>
                                        </td>
                                        <td align='<%= CultureResource.ResourceM.GetString("Align") %>' dir='<%= CultureResource.ResourceM.GetString("Direction") %>'>
                                            <asp:TextBox ID="txtSubject" runat="server" Width="250px" Font-Names="Tahoma" CssClass="textbox"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtSubject"
                                                ErrorMessage="*" ValidationGroup="ContactUS" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align='<%= CultureResource.ResourceM.GetString("AlignN") %>' valign="top">
                                            <div class="Header">
                                                <%=CultureResource.ResourceM.GetString("Text") %>
                                            </div>
                                        </td>
                                        <td align='<%= CultureResource.ResourceM.GetString("Align") %>' style="color: Black;">
                                            <telerik:RadEditor ID="radEdtText" runat="server" EditModes="Design" EnableResize="False"
                                                Height="150px" ToolsFile="~/BasicTools.xml" ToolsWidth="330px" Width="350px"
                                                ForeColor="Black" Enabled="true">
                                            </telerik:RadEditor>
                                            <asp:RequiredFieldValidator ID="ResumeValidator" runat="server" ControlToValidate="radEdtText"
                                                Display="Static" ValidationGroup="ContactUS"><%= CultureResource.ResourceM.GetString("Text_Err") %></asp:RequiredFieldValidator>
                                            <asp:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="checkLength"
                                                ControlToValidate="radEdtText" Display="Dynamic" ValidationGroup="ContactUS"><%= string.Format(CultureResource.ResourceM.GetString("YourText"), "50") %></asp:CustomValidator>
                                            <script type="text/javascript">
                                                var limitNum = 50;
                                                function checkLength(sender, args) {
                                                    //Note that sender is NOT the RadEditor. sender is the span of the validator. 
                                                    //The content is contained in the args.Value variable 
                                                    var editorText = args.Value;
                                                    args.IsValid = editorText.length > limitNum;
                                                } 
                                            </script>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align='<%= CultureResource.ResourceM.GetString("AlignN") %>'>
                                            <div class="Header">
                                                <%=CultureResource.ResourceM.GetString("RadCaptHelp")%>
                                            </div>
                                        </td>
                                        <td align='<%= CultureResource.ResourceM.GetString("Align") %>'>
                                            <telerik:RadCaptcha ID="radCaptcha" runat="server" CaptchaMaxTimeout="240" ValidationGroup="ContactUS"
                                                Display="Dynamic" CaptchaTextBoxLabel="" CaptchaTextBoxCssClass="textbox" Width="50%"
                                                CaptchaLinkButtonText='<%# CultureResource.ResourceM.GetString("RadCaptNew") %>'
                                                EnableRefreshImage="True">
                                                <CaptchaImage FontFamily="Tahoma" TextColor="#333333" LineNoise="Low" BackgroundNoise="Low"
                                                    TextLength="4" />
                                            </telerik:RadCaptcha>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="2">
                                            <asp:Button Text='<%# CultureResource.ResourceM.GetString("Send")%>' ID="btnSend"
                                                runat="server" OnClick="btnSend_Click" ValidationGroup="ContactUS" Font-Names="Tahoma"
                                                Height="28px" Width="80px" CssClass="button" />
                                        </td>
                                        <td align="center">
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="LargFrameFooter" style='float: <%# CultureResource.ResourceM.GetString("Align")%>;'>
                                <div class="Text" style='float: <%# CultureResource.ResourceM.GetString("AlignN")%>;
                                    padding-top: 15px; text-align: <%# CultureResource.ResourceM.GetString("AlignN")%>'
                                    dir='<%# CultureResource.ResourceM.GetString("Direction")%>'>
                                    <asp:Label ID="lblDate" runat="server" Text=""></asp:Label>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
