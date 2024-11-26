﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_ChucVu : System.Web.UI.Page
{
    QuanLyGiangVienDataContext db = new QuanLyGiangVienDataContext();
    ExecutedID MaTuDong = new ExecutedID();

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
            txtMaChucVu.Enabled = false;
            LoadGrid();
            Refresh1();
            txtTenChucVu.Focus();
        }
    }
    //xây dựng phương thức làm rỗng
    public void Refresh1()
    {
        txtMaChucVu.Text = "";
        txtTenChucVu.Text = "";
        txtPhanTramGiam.Text = "";
        txtGhiChu.Text = "";

    }

    /// <summary>
    /// Xây dựng phương thức kiểm tra rỗng
    /// </summary>
    /// <returns></returns>
    public bool KiemTraRong()
    {
        bool kt = false;
        if (txtTenChucVu.Text == "" || txtPhanTramGiam.Text == "")
        //if (txtMaLop.Text == "")
        {
            return true;
        }
        else
        {
            if (txtTenChucVu.Text != "" && txtPhanTramGiam.Text != "")
            {
                return false;
            }

        }
        return kt;

    }

    //xây dựng phương thức load gridview
    public void LoadGrid()
    {
        var DSChucVu = from c in db.ChucVus
                        select c;
        GrvChucVu.DataSource = DSChucVu;
        GrvChucVu.DataBind();
    }
    protected void btnThem_Click(object sender, EventArgs e)
    {
        txtMaChucVu.Text = MaTuDong.LayMaChucVu().ToString();
        try
        {
            if (KiemTraRong() == true)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Không được để trống tên chức vụ hoặc số phần trăm được giảm');", true);
                Refresh1();
            }
          
            
                if (KiemTraRong() == false)
                {
                    ChucVu ps = new ChucVu();
                    ps.MaChucVu = txtMaChucVu.Text;
                    ps.TenChucVu = txtTenChucVu.Text;
                    ps.PhanTramDuocGiam = Convert.ToInt32(txtPhanTramGiam.Text);
                    ps.GhiChu = txtGhiChu.Text;
                    db.ChucVus.InsertOnSubmit(ps);
                    db.SubmitChanges();
                    LoadGrid();
                    Refresh1();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Bạn đã thêm thành công');", true);

                
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
            ChucVu ps = db.ChucVus.SingleOrDefault(c => c.MaChucVu == txtMaChucVu.Text);
            ps.MaChucVu = txtMaChucVu.Text;
            ps.TenChucVu = txtTenChucVu.Text;
            ps.PhanTramDuocGiam =Convert.ToInt32( txtPhanTramGiam.Text);
            ps.GhiChu = txtGhiChu.Text;
            db.SubmitChanges();
            LoadGrid();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Bạn sửa thành công');", true);
            Refresh1();
            txtTenChucVu.Focus();

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Chưa chọn chức vụ muốn sửa');", true);
        }

    }
    protected void btnXoa_Click(object sender, EventArgs e)
    {
        try
        {
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Bạn có muốn xóa không');", true);
            ChucVu ps = db.ChucVus.SingleOrDefault(c => c.MaChucVu == txtMaChucVu.Text);
            db.ChucVus.DeleteOnSubmit(ps);
            db.SubmitChanges();
            LoadGrid();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Bạn xóa thành công');", true);
            Refresh1();
            txtTenChucVu.Focus();
        }

        catch (Exception)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Chưa chọn chức vụ muốn xóa');", true);
        }
    }
    protected void GrvChucVu_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Label lblMa = (Label)GrvChucVu.Rows[e.NewSelectedIndex].FindControl("lblMa");
        ChucVu ps = db.ChucVus.SingleOrDefault(c => c.MaChucVu == lblMa.Text);
        txtMaChucVu.Text = ps.MaChucVu.ToString();
        txtTenChucVu.Text = ps.TenChucVu.ToString();
        txtPhanTramGiam.Text = ps.PhanTramDuocGiam.ToString();
        txtGhiChu.Text = ps.GhiChu.ToString();

    }
    protected void GrvChucVu_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GrvChucVu.PageIndex = e.NewPageIndex;
        LoadGrid();
    }
    protected void btnMoi_Click(object sender, EventArgs e)
    {
        Refresh1();
    }
}