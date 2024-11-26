<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Logout.aspx.cs" Inherits="Admin_Logout" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <link href="CSS/adminstyle.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            height: 34px;
            background-color: #5f9ae0;
            font-size: 16px;
            font-weight: bold;
        }
        td
        {
            border: 0px;
        }
        .style2
        {
            width: 370px;
        }
        .style3
        {
            margin-left: 0px;
        }
        .auto-style2 {
            width: 85px;
        }
        .auto-style3 {
            width: 40%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <center>
            <table cellpadding="0" cellspacing="0" border="1px" class="style2">
                <tr>
                    <td colspan="2" class="style1" align="center">
                        Tạo Tài Khoản</td>
                </tr>
                <tr>
                    <td colspan="2" style="height: 30px">
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                    </td>
                </tr>

                <tr>
                    <td align="left" style="padding-left: 10px; line-height: 28px; padding-bottom:1px; padding-top:1px;" class="auto-style3" >
                        Tên đăng nhập
                    </td>
                    <td align="left" style="line-height: 28px; padding-left: 1px;" class="auto-style2">
                        <asp:TextBox ID="txtUserName" runat="server" Width="96%" ></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="left" style="padding-left: 10px; line-height: 28px; padding-bottom:1px; padding-top:1px;">
                        Mật khẩu
                    </td>
                    <td align="left" style="line-height: 28px; padding-left: 1px;" class="auto-style2">
                        <asp:TextBox ID="txtPassword" runat="server" Width="96%" TextMode="Password"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="left" style="padding-left: 10px; line-height: 28px; padding-bottom:1px; padding-top:1px;" class="auto-style3" >
                        Mã
                    </td>
                    <td align="left" style="line-height: 28px; padding-left: 1px;" class="auto-style2">
                        <asp:TextBox ID="txtMa" runat="server" Width="96%" ></asp:TextBox
>
                    </td>
                </tr>    
                <tr>
                    <td style="height: 38px" colspan="2" >
                        <span lang="en-us">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btTaoTaiKhoan" runat="server" Text="Tạo Tài Khoản" OnClick="btTaoTaiKhoan_Click" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:ImageButton ID="btnHome" runat="server" ImageUrl="~/Image/home.png" AlternateText="Home" OnClick="btnHome_Click" Height="37px" Width="50px" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </span>
                    </td>            
                </tr>
                <tr>
                    <td align="center" style="padding-left: 10px; line-height: 28px" colspan="2">
                        <asp:Label ID="lblthongbao" runat="server" Font-Bold="true" Font-Size="12" ForeColor="Red" ></asp:Label>
                        <asp:HyperLink ID="hplQuenMK" runat="server" NavigateUrl="~/QuenMatKhau.aspx">Quên 
                    mật khẩu?</asp:HyperLink>
                    </td>
                </tr>
            </table>
        </center>
    </div>
    </form>
</body>
</html>
