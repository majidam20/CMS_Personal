<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>
<%@ Import Namespace="Business.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="App_Themes/AdminCP/AdminMaster.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .LgnTitle
        {
            color: #201E1E;
            font-family: Tahoma;
        }
        #Login_head
        {
            font-family:"B Nazanin","2 Nazanin", Nazanin,"B Titr", "2 Titr", Titr, "B Zar", "2 Zar", Zar, Tahoma;
            color: #201E1E;
            font-size:18px;
            text-align:right;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <center>
            <table id="Template-Table" style="width:210px; margin-top:110px" align="center">
            <tr>
                <td id="header-left" style="width:5px;">&nbsp;</td>
                <td id="header-logo" style="width:390px; background-image: url(App_Themes/AdminCP/Images/Header.png); background-repeat:repeat-x;">
                    <div style="background-image: url(App_Themes/AdminCP/Images/HeaderLogo.png); background-repeat: no-repeat; background-position:-50px bottom; width:220px; height:158px;"></div>
                </td>
                <td id="header-right" style="width: 5px">
                    &nbsp;</td>
            </tr>
            <tr>
                <td id="title-left" style="width:5px;">
                </td>
                <td id="title-center">
                    <div id="Login_head">ورود به بخش مدیریت</div>
                </td>
                <td id="title-right" style="width:5px;">
                </td>
            </tr>
            <tr>
                <td id="sidebar" colspan="2" style="width: 200px; padding-top: 10px; padding-right: 10px">
                    <asp:Login ID="Login1" runat="server" Width="200px">
                        <LayoutTemplate>
                            <table cellpadding="1" width="100%" dir="rtl">
                                <tr>
                                    <td align="right" class="LgnTitle">
                                        <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">نام کاربری: </asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:TextBox ID="UserName" runat="server" Width="150px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                            ErrorMessage="User Name is required." ToolTip="User Name is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" class="LgnTitle">
                                        <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">کلمه عبور: </asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:TextBox ID="Password" runat="server" TextMode="Password" Width="150px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                            ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:CheckBox CssClass="LgnTitle" ID="RememberMe" runat="server" Text="مرا به خاطر بسپار" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" style="color: Red;">
                                        <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" class="LgnTitle" style="padding-top:10px;">
                                        <asp:Button CssClass="submit-login" style="font-family:Tahoma;" 
                                            ID="LoginButton" runat="server" CommandName="Login" Text="ورود" 
                                            ValidationGroup="Login1" onclick="LoginButton_Click" />
                                    </td>
                                </tr>
                            </table>
                        </LayoutTemplate>
                    </asp:Login>
                </td>
                <td id="sidebar-right" style="width:5px;">
                </td>
            </tr>
            <tr>
                <td id="status-left" style="width:5px;">
                </td>
                <td id="status-center" style="text-align:center;">
                    <a href="Default.aspx" style="color:#201E1E;" class="LgnTitle"><%= CultureResource.ResourceM.GetString("Bak2Site") %></a>
                </td>
                <td id="status-right" style="width:5px;">
                </td>
            </tr>
        </table>
    </center>
    </form>
</body>
</html>
