<%@ Page Title="Bai Giang" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="QuanLyBaiGiangGV.aspx.cs" Inherits="TeachingManagers.QuanLyBaiGiangGV" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <meta charset="utf-8" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <center>
        <table cellpadding="0" cellspacing="0" style="width: 100%" border="1px">
            <tr>
                <td colspan="2" align="center" class="dongtieude">
                    <b>QU&#7842;N LÝ BÀI GI&#7842;NG</b>
                </td>
            </tr>
            <tr>
                <td style="width: 40%" class="theten">
                    Mã L&#7899;p
                </td>
                <td class="dieukhien">
                    <asp:DropDownList ID="ddlLop" runat="server" Height="27px" Width="186px">
                        <asp:ListItem Value="0">---Ch&#7885;n l&#7899;p---</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 40%" class="theten">
                    Mã Môn
                </td>
                <td class="dieukhien">
                    <asp:DropDownList ID="ddlMon" runat="server" Height="27px" Width="186px">
                        <asp:ListItem Value="0">---Ch&#7885;n môn---</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 40%" class="theten">
                    Tên Bài Gi&#7843;ng
                </td>
                <td class="dieukhien">
                    <asp:TextBox ID="txtTenBaiGiang" runat="server" CssClass="auto-style1"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 40%" class="theten">
                    &#272;&#432;&#7901;ng D&#7851;n Bài Gi&#7843;ng
                </td>
                <td class="dieukhien">
                    <asp:TextBox ID="txtBaiGiangPath" runat="server" CssClass="auto-style1"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 40%" class="theten">
                    &#272;&#432;&#7901;ng D&#7851;n Bài T&#7853;p
                </td>
                <td class="dieukhien">
                    <asp:TextBox ID="txtBaiTapPath" runat="server" CssClass="auto-style1"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="left" style="border:0px; width:100%; height:25px; font-size:15px; text-align:left; color:Red;">
                    <asp:Label ID="lblThongBao" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center" class="dongdieukhien" valign="middle">
                    <asp:Button ID="btnThem" runat="server" Text="Thêm m&#7899;i" onclick="btnThem_Click" />&nbsp;
                    <asp:Button ID="btnSua" runat="server" Text="S&#7917;a thông tin" onclick="btnSua_Click" />&nbsp;
                    <asp:Button ID="btnXoa" runat="server" Text="Xóa thông tin" onclick="btnXoa_Click" />&nbsp;
                    <asp:Button ID="btRefresh" runat="server" Text="Làm m&#7899;i" onclick="btRefresh_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center" style="padding: 5px;" width="76%">
                    <asp:Panel ID="Panel1" runat="server" Width="100%">
                        <asp:GridView ID="grvDoAn" runat="server" Width="100%" AllowPaging="true" PageSize="10" OnPageIndexChanging="grvDoAn_PageIndexChanging" AutoGenerateColumns="False">
                            <Columns>
                                <asp:BoundField DataField="MaBaiGiang" HeaderText="Mã Bài Gi&#7843;ng" />
                                <asp:BoundField DataField="MaLop" HeaderText="Mã L&#7899;p" />
                                <asp:BoundField DataField="TenMon" HeaderText="Tên Môn" />
                                <asp:BoundField DataField="TenBaiGiang" HeaderText="Tên Bài Gi&#7843;ng" />
                                <asp:BoundField DataField="BaiGiangPath" HeaderText="&#272;&#432;&#7901;ng D&#7851;n Bài Gi&#7843;ng" />
                                <asp:BoundField DataField="BaiTapPath" HeaderText="&#272;&#432;&#7901;ng D&#7851;n Bài T&#7853;p" />
                            </Columns>
                        </asp:GridView>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </center>
</asp:Content>
