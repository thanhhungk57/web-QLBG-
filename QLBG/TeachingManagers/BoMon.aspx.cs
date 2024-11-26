using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_BoMon : System.Web.UI.Page
{
    QuanLyGiangVienDataContext ql = new QuanLyGiangVienDataContext();
    ExecutedID ex = new ExecutedID();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session.Contents["TrangThai"].ToString() == "DaDangNhap")
        {
            var tt = from c in ql.TaiKhoans
                     where (c.TenDangNhap == Session["Dangnhap"].ToString() && c.MaGV.ToString() == Session["MemberID"].ToString() && c.MaGV == c.GiaoVien.MaGV)
                     select new { c.MaGV, c.GiaoVien.TenGV, c.Quyen };
            foreach (var item in tt)
            {
                //Download source code FREE tai Sharecode.vn
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
            LoadGridview();
        }
    }
    public void LoadGridview()
    {
        var bomon = from c in ql.BoMons
                    select c;
        GrvBoMon.DataSource = bomon;
        GrvBoMon.DataBind();
    }
    /// <summary>
    /// xóa trắng các control
    /// </summary>
    public void LamMoi()
    {
        txtGhiChu.Text = "";
        txtMaBoMon.Text = "";
        txtTen.Text = "";
    }
    public bool Kiemtrarong()
    {
        if (txtTen.Text == ""  || txtMaBoMon.Text == "")
        { return true; }
        else return false;
    }
    protected void btnThem_Click(object sender, EventArgs e)
    {
        try
        {
            txtMaBoMon.Text = ex.LayMaBoMon().ToString();
            if (Kiemtrarong() == false)
            {
                BoMon bm = new BoMon();
                bm.MaBoMon = txtMaBoMon.Text;
                bm.TenBoMon = txtTen.Text;
                bm.GhiChu = txtGhiChu.Text;
                ql.BoMons.InsertOnSubmit(bm);
                ql.SubmitChanges();
                LoadGridview();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Bạn thêm thành công');", true);
                //LamMoi();
                Response.Redirect("BoMon.aspx");
            }
            else { ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Bạn không đơợc để trống thông tin');", true); }
        }
        catch (Exception)
        {
            
            throw;
        }
    }
    protected void btnSua_Click(object sender, EventArgs e)
    {
        try
        {
            BoMon bm = ql.BoMons.SingleOrDefault(c=>c.MaBoMon==txtMaBoMon.Text);
            bm.TenBoMon = txtTen.Text;
            bm.GhiChu = txtGhiChu.Text;
            ql.SubmitChanges();
            LoadGridview();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Bạn sửa thành công');", true);
            //LamMoi();
            Response.Redirect("BoMon.aspx");

        }
        catch (Exception)
        {
            
            throw;
        }
    }
    protected void btnXoa_Click(object sender, EventArgs e)
    {
        try
        {
            BoMon bm = ql.BoMons.SingleOrDefault(c => c.MaBoMon == txtMaBoMon.Text);
            ql.BoMons.DeleteOnSubmit(bm);
            ql.SubmitChanges();
            LoadGridview();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Bạn xóa thành công');", true);
            //LamMoi();
            Response.Redirect("BoMon.aspx");
        }
        catch (Exception)
        {
            throw;
        }
    }
    protected void btnMoi_Click(object sender, EventArgs e)
    {
        //LamMoi();
        Response.Redirect("BoMon.aspx");
    }
    protected void GrvChucVu_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Label lblMa = (Label)GrvBoMon.Rows[e.NewSelectedIndex].FindControl("lblMa");
        BoMon bm = ql.BoMons.SingleOrDefault(c=>c.MaBoMon==lblMa.Text);
        txtMaBoMon.Text = bm.MaBoMon.ToString();
        txtTen.Text = bm.TenBoMon.ToString();
        txtGhiChu.Text = bm.GhiChu.ToString();
    }
    protected void GrvChucVu_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GrvBoMon.PageIndex = e.NewPageIndex;
        LoadGridview();
    }
}