<%@ Page Title="" EnableEventValidation="false" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="DoAnTotNghiep.aspx.cs" Inherits="Admin_DoAnTotNghiep" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <center>
        <table cellpadding="0" cellspacing="0" style="width: 100%" border="1px">
            <!-- Form content here -->
            <tr>
                <td colspan="2" align="center" class="dongtieude">
                    <b>ĐĂNG KÝ MÔN HỌC CHO HỌC SINH</b></td>
            </tr>
            <tr>
                <td style="width: 40%" class="theten">
                    Lớp</td>
                <td class="dieukhien">
                    <asp:DropDownList ID="ddlLop" runat="server" Height="27px" Width="186px">
                    <asp:ListItem Value="0">---Ch&#7885;n l&#7899;p---</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 40%" class="theten">
                    Môn Học</td>
                <td class="dieukhien">
                    <asp:DropDownList ID="ddlMonHoc" runat="server" Height="27px" Width="186px">
                        <asp:ListItem Value="0">---Ch&#7885;n Môn---</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 40%" class="theten">
                    Giáo viên</td>
                <td class="dieukhien">
                    <asp:DropDownList ID="ddlGiaoVien" runat="server" Height="27px" Width="186px">
                    <asp:ListItem Value="0">---Ch&#7885;n Giáo viên---</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 40%" class="theten">
                    Năm học</td>
                <td class="dieukhien">
                    <asp:DropDownList ID="ddlNamHoc" runat="server" Height="27px" Width="186px">
                    <asp:ListItem Value="0">---Ch&#7885;n năm học---</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 40%" class="theten">
                    Ghi chú</td>
                <td class="dieukhien">
                    <asp:TextBox ID="txtGhiChu" runat="server" Height="45px" TextMode="MultiLine" Width="355px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center" style="border: 0px; width: 100%; height: 25px; font-size: 15px; text-align: left; color: Red;">
                    <asp:Label ID="lblThongBao" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center" class="dongdieukhien" valign="middle">
                    <asp:Button ID="btnThem" runat="server" Text="Thêm mới" OnClick="btnThem_Click" />
                </td>
            </tr>
        </table>
    </center>

    <asp:GridView ID="gvRegisteredInfo" runat="server" AutoGenerateColumns="False" DataKeyNames="MaHS" EmptyDataText="Không có dữ liệu" CssClass="gridview" Width="100%">
        <Columns>
            <asp:BoundField DataField="MaLop" HeaderText="Mã Lớp" />
            <asp:BoundField DataField="TenLop" HeaderText="Tên Lớp" />
            <asp:BoundField DataField="TenMon" HeaderText="Tên Môn" />
            <asp:BoundField DataField="TenGV" HeaderText="Tên Giáo Viên" />
            <asp:BoundField DataField="NamHoc" HeaderText="Năm Học" />
            <asp:BoundField DataField="GhiChu" HeaderText="Ghi Chú" />
        </Columns>
    </asp:GridView>
</asp:Content>
