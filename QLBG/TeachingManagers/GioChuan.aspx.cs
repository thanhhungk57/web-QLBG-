using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_GioChuan : System.Web.UI.Page
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
            Refresh1();
            LoadGrid();
            txtTenChucDanh.Focus();
            txtMaChucDanh.Enabled = false;

        }
    }
    //làm rỗng các điều khiển
    public void Refresh1()
    {
        txtMaChucDanh.Text = "";
        txtTenChucDanh.Text = "";
        txtSoGioChuan.Text = "";
        txtGhiChu.Text = "";
        //ddlLoaiMon.SelectedValue = "";

    }
    /// <summary>
    /// Xây dựng phương thức kiểm tra rỗng
    /// </summary>
    /// <returns></returns>
    public bool KiemTraRong()
    {
        bool kt = false;
        if (txtTenChucDanh.Text == "" || txtSoGioChuan.Text == "")
        //if (txtMaLop.Text == "")
        {
            return true;
        }
        else
        {
            if (txtTenChucDanh.Text != "" && txtSoGioChuan.Text != "")
            {
                return false;
            }

        }
        return kt;

    }

    //xây dựng phương thức load gridview
    public void LoadGrid()
    {
        var DS = from c in db.GioChuans
                       select c;
        GrvGioChuan.DataSource = DS;
        GrvGioChuan.DataBind();
    }
    protected void btnThem_Click(object sender, EventArgs e)
    {
        txtMaChucDanh.Text = MaTuDong.LayMaChucDanh().ToString();
        try
        {
            if (KiemTraRong() == true)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Không được để trống tên chức danh hoặc số giờ chuẩn');", true);
               
            }
            else
            {
                if (KiemTraRong() == false)
                {
                    GioChuan st = new GioChuan();
                    st.MaChucDanh = txtMaChucDanh.Text;
                    st.TenChucDanh = txtTenChucDanh.Text;
                    st.SoGioChuan = Convert.ToInt32(txtSoGioChuan.Text);
                    st.GhiChu = txtGhiChu.Text;
                    db.GioChuans.InsertOnSubmit(st);
                    db.SubmitChanges();
                    LoadGrid();
                    //Refresh1();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Bạn đã thêm thành công');", true);
                    Response.Redirect("GioChuan.aspx");
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
            GioChuan st = db.GioChuans.SingleOrDefault(c => c.MaChucDanh == txtMaChucDanh.Text);
            st.MaChucDanh = txtMaChucDanh.Text;
            st.TenChucDanh = txtTenChucDanh.Text;
            st.SoGioChuan = Convert.ToInt32(txtSoGioChuan.Text);
            st.GhiChu = txtGhiChu.Text;
            db.SubmitChanges();
            LoadGrid();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Bạn sửa thành công');", true);
            //Refresh1();
            txtTenChucDanh.Focus();
            Response.Redirect("GioChuan.aspx");

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Chưa chọn chức danh muốn sửa');", true);
            Refresh1();
        }

    }
    protected void btnXoa_Click(object sender, EventArgs e)
    {

        try
        {
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Bạn có muốn xóa không');", true);
            GioChuan st = db.GioChuans.SingleOrDefault(c => c.MaChucDanh == txtMaChucDanh.Text);
            db.GioChuans.DeleteOnSubmit(st);
            db.SubmitChanges();
            LoadGrid();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Thông báo", "alert('Bạn xóa thành công');", true);            

            //Refresh1();
            txtTenChucDanh.Focus();
            Response.Redirect("GioChuan.aspx");
        }

        catch (Exception)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Chưa chọn chức danh muốn xóa');", true);
            Refresh1();
        }
    }
    protected void GrvGioChuan_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Label lblMa = (Label)GrvGioChuan.Rows[e.NewSelectedIndex].FindControl("lblMa");
        GioChuan st = db.GioChuans.SingleOrDefault(c => c.MaChucDanh == lblMa.Text);
        txtMaChucDanh.Text = st.MaChucDanh.ToString();
        txtTenChucDanh.Text = st.TenChucDanh.ToString();
        txtSoGioChuan.Text = st.SoGioChuan.ToString();
        txtGhiChu.Text = st.GhiChu.ToString();
    }
    protected void GrvGioChuan_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GrvGioChuan.PageIndex = e.NewPageIndex;
        LoadGrid();
    }
    protected void btnMoi_Click(object sender, EventArgs e)
    {
        Response.Redirect("GioChuan.aspx");
    }
}