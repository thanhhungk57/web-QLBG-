<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ThongTinCaNhanHS.aspx.cs" Inherits="Admin_ThongTinCaNhan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .theten
        {
            border-bottom: 0.01px solid #5f9ae0;
            text-align: right;
            padding: 5px;
            width: 349px;
            border-left-style: none;
            border-left-color: inherit;
            border-left-width: 0px;
            border-right-style: none;
            border-right-color: inherit;
            border-right-width: 0px;
            border-top-style: none;
            border-top-color: inherit;
            border-top-width: 0px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <center>
        <table cellpadding="0" cellspacing="0" style="width: 100%" border="1px">
            <tr>
                <td colspan="2" align="center" class="dongtieude">
                    <b>THÔNG TIN CÁ NHÂN HỌC SINH</b>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblThongtin" runat="server" Font-Bold="true"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="theten">
                    Tên đăng nhập
                </td>
                <td class="dieukhien">
                    <asp:TextBox ID="txtTenDN" ReadOnly="true" runat="server" Width="311px" MaxLength="100"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="theten">
                    Họ tên
                </td>
                <td class="dieukhien">
                    <asp:TextBox ID="txtHoten" runat="server" Width="311px" MaxLength="200"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="theten">
                    Ngày sinh
                </td>
                <td class="dieukhien">
                    <asp:TextBox ID="txtNgaysinh" runat="server" MaxLength="50" Width="311px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="theten">
                    Giới tính
                </td>
                <td class="dieukhien">
                    <asp:TextBox ID="txtGioiTinh" runat="server" MaxLength="50" Width="311px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="theten">
                    Số CMND
                </td>
                <td class="dieukhien">
                    <asp:TextBox ID="txtCMND" runat="server" MaxLength="50" Width="311px"></asp:TextBox>
                </td>
            </tr>
             <tr>
                <td class="theten">
                    Lớp
                </td>
                <td class="dieukhien" align="left">
                    <asp:TextBox ID="txtLop" runat="server" Width="311px"></asp:TextBox>
                </td>
            </tr>
             <tr>
                <td class="theten">
                    Email
                </td>
                <td class="dieukhien" align="left">
                    <asp:TextBox ID="txtEmail" runat="server" Width="311px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="theten">
                    Địa chỉ
                </td>
                <td class="dieukhien">
                    <asp:TextBox ID="txtDiaChi" runat="server" Width="671px" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="theten">
                    Ghi chú
                </td>
                <td class="dieukhien">
                    <asp:TextBox ID="txtGhiChu" runat="server" Height="42px" MaxLength="30" 
                        Width="98%" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2"  class="dongdieukhien" align="center">
                    <asp:Button ID="btnCapNhat" runat="server" Text="Cập nhật thông tin" onclick="btnCapNhat_Click" />
                </td>
            </tr>
        </table>
    </center>
</asp:Content>
