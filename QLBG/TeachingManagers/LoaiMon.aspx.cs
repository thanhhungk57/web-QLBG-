﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;


public partial class Admin_LoaiMon : System.Web.UI.Page
{
    QuanLyGiangVienDataContext db = new QuanLyGiangVienDataContext ();
    //ExecutedID MaTuDong = new ExecutedID();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session.Contents["TrangThai"].ToString() == "DaDangNhap")
        {
            var tt = from c in db.TaiKhoans
                     where (c.TenDangNhap == Session["Dangnhap"].ToString() && c.MaGV.ToString() == Session["MemberID"].ToString() && c.MaGV == c.GiaoVien.MaGV)
                     select new { c.MaGV, c.GiaoVien.TenGV, c.Quyen };
            foreach (var item in tt)
            {

                if (Session["MemberID"].ToString() == item.MaGV.ToString() && item.Quyen == "Giáo viên")
                {
                    //Response.Redirect("ThongTinCaNhan.aspx?url="+Request.Url.PathAndQuery);
                    Response.Redirect("Logout.aspx?url=" + Request.Url.PathAndQuery);
                }
            }
        }
        else
        {
            if (Session.Contents["TrangThai"].ToString() == "ChuaDangNhap")
                //Response.Redirect("Login.aspx");
                Response.Redirect("Login.aspx?url=" + Request.Url.PathAndQuery);
        }
        if (!IsPostBack)
        {
            
            Refresh1();
            LoadGrid();
            txtTenLoai.Focus();
        }
        
    }
    /// <summary>
    /// Xây dựng phương thức kiểm tra trùng mã
    /// </summary>
    /// <param name="ma"></param>
    /// <returns></returns>
    public bool KiemTraTrungMa(string maloai)
    {
        bool kt = false;
        var loaimon = from c in db.LoaiMons
                  select c;
        foreach (LoaiMon cl in loaimon)
        {
            if (maloai == cl.MaLoai.ToString())
            {
                kt = true;
            }
        }
        return kt;

    }
    //xây dựng phương thức làm rỗng
    public void Refresh1()
    {
        txtMaLoai.Text = "";
        txtTenLoai.Text = "";
        txtGhiChu.Text = "";
 
    }

    public bool KiemTraRong()
    {
        bool kt = false;
        if (txtMaLoai.Text == "" || txtTenLoai.Text == "")
        //if (txtMaLop.Text == "")
        {
            return true;
        }
        else
        {
            if (txtMaLoai.Text != "" && txtTenLoai.Text != "")
            {
                return false;
            }

        }
        return kt;

    }
   
    //xây dựng phương thức load gridview
    public void LoadGrid()
    {
        var DSLoaiMon = from c in db.LoaiMons
                        select c;
        GrvLoaiMon.DataSource = DSLoaiMon;
        GrvLoaiMon.DataBind();
    }
   
    protected void btnThem_Click(object sender, EventArgs e)
    {
        //txtMaLoai.Text = MaTuDong.LayMaLoaiMon().ToString();
        try
        {
            if (KiemTraRong() == true)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Không được để trống tên loại môn  hoặc mã loại môn');", true);
                Refresh1();
            }
            else
            {
                if (KiemTraRong() == false)
                {
                    if (KiemTraTrungMa(txtMaLoai.Text) == false)
                    {
                        LoaiMon ca = new LoaiMon();
                        ca.MaLoai = txtMaLoai.Text;
                        ca.TenLoai = txtTenLoai.Text;
                        ca.GhiChu = txtGhiChu.Text;
                        db.LoaiMons.InsertOnSubmit(ca);
                        db.SubmitChanges();
                        LoadGrid();
                        //Refresh1();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Bạn đã thêm thành công');", true);
                        Response.Redirect("LoaiMon.aspx");

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Mã bạn nhập đã tồn tại');", true); 
                    }
                }
            }
        }

        catch (Exception)
        {

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Không được để trống thông tin');", true);
        }
    }
    protected void btnSua_Click(object sender, EventArgs e)
    {
        try
        {
                LoaiMon ca = db.LoaiMons.SingleOrDefault(c => c.MaLoai == txtMaLoai.Text);
                ca.MaLoai = txtMaLoai.Text;
                ca.TenLoai = txtTenLoai.Text;
                ca.GhiChu = txtGhiChu.Text;               
                db.SubmitChanges();
                LoadGrid();
                  ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Bạn sửa thành công');", true);
                //Refresh1();
                txtTenLoai.Focus();
                Response.Redirect("LoaiMon.aspx");
 
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Chưa chọn loại môn muốn sửa');", true);
        }

    }
    protected void btnXoa_Click(object sender, EventArgs e)
    {
        try
        {
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Bạn có muốn xóa không');", true);
            LoaiMon dis = db.LoaiMons.SingleOrDefault(c => c.MaLoai == txtMaLoai.Text);
            db.LoaiMons.DeleteOnSubmit(dis);
            db.SubmitChanges();
            LoadGrid();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Bạn xóa thành công');", true);

            //Refresh1();
            txtTenLoai.Focus();
            Response.Redirect("LoaiMon.aspx");
        }

        catch (Exception)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Chưa chọn loại môn học muốn xóa');", true);
        }
    }
    protected void GrvLoaiMon_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Label lblMa = (Label)GrvLoaiMon.Rows[e.NewSelectedIndex].FindControl("lblMa");
        LoaiMon cate = db.LoaiMons.SingleOrDefault(c => c.MaLoai == lblMa.Text);
        txtMaLoai.Text = cate.MaLoai.ToString();
        txtTenLoai.Text = cate.TenLoai.ToString();
        txtGhiChu.Text = cate.GhiChu.ToString();
    }


    protected void GrvLoaiMon_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GrvLoaiMon.PageIndex = e.NewPageIndex;
        LoadGrid();
    }
    protected void btnMoi_Click(object sender, EventArgs e)
    {
        Response.Redirect("LoaiMon.aspx");
    }
}
