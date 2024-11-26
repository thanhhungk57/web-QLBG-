<%@ Page Title="Bai Giang" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="QuanLyBaiGiang.aspx.cs" Inherits="TeachingManagers.QuanLyBaiGiang" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server"> </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server"> </asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <center>
        <div style="text-align:center;">
            <h2>Bài Giảng</h2>
            <asp:GridView ID="gvLectures" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="gvLectures_SelectedIndexChanged">
                <Columns>
                    <asp:BoundField DataField="MaBaiGiang" HeaderText="Mã Bài Giảng" />
                    <asp:BoundField DataField="MaLop" HeaderText="Mã Lớp" />
                    <asp:BoundField DataField="TenMon" HeaderText="Tên Môn" />
                    <asp:BoundField DataField="TenBaiGiang" HeaderText="Tên Bài Giảng" />
                    <asp:TemplateField HeaderText="Link bài giảng">
                        <ItemTemplate>
                            <asp:HyperLink ID="linkBaiGiang" runat="server" NavigateUrl='<%# Eval("BaiGiangPath") %>' Text="Xem Bài Giảng" Target="_blank"></asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Link bài tập">
                        <ItemTemplate>
                            <asp:HyperLink ID="linkBaiTap" runat="server" NavigateUrl='<%# Eval("BaiTapPath") %>' Text="Xem Bài Tập" Target="_blank"></asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </center>
</asp:Content>
