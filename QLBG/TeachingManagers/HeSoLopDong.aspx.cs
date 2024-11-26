using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_HeSoLopDong : System.Web.UI.Page
{
    QuanLyGiangVienDataContext db= new QuanLyGiangVienDataContext();
     ExecutedID ex= new ExecutedID ();


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
            txtMaHeSo.Enabled = false;
            LoadGrid();
            Refresh1();
        }
    }
    //xây dựng phương thức làm rỗng
    public void Refresh1()
    {
        txtMaHeSo.Text = "";
        txtTu.Text = "";
        txtDen.Text = "";
        txtHeSo.Text = "";
        ddlHinhThucDay.SelectedValue = "0";
        

    }


    //xây dựng phương thức load gridview
    public void LoadGrid()
    {
        var DSHDT = from c in db.HeSoLopDongs
                    select c;
        GrvHeSoLopDong.DataSource = DSHDT;
        GrvHeSoLopDong.DataBind();
    }
    //xây dựng phương thức kiểm tra rỗng
    public bool KtraRong()
    {
        //bool kt = false;
        //if (txtTu.Text == "" || txtDen.Text == "" || txtHeSo.Text == "")
        
             if (txtTu.Text == ""|| txtHeSo.Text == "")
             {
            return false;

        }
        else
        {
            return true;
        }
    }
    protected void GrvHeSoLopDong_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Label lblMa = (Label)GrvHeSoLopDong.Rows[e.NewSelectedIndex].FindControl("lblMa");
        HeSoLopDong co = db.HeSoLopDongs.SingleOrDefault(c => c.MaHSLopDong == lblMa.Text);
        txtMaHeSo.Text = co.MaHSLopDong.ToString();
        txtTu.Text = co.Tu.ToString();
        txtDen.Text = co.Den.ToString();
        txtHeSo.Text = co.HeSo.ToString();
        ddlHinhThucDay.SelectedItem.Text = co.HinhThucDay.ToString();

    }

    protected void btnThem_Click(object sender, EventArgs e)
    {
        txtMaHeSo.Text = ex.LayMa().ToString();
        try
        {
            if (txtDen.Text == "" && txtTu.Text == "" && txtHeSo.Text == "" && ddlHinhThucDay.SelectedValue == "0")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Bạn chưa nhập vào dữ liệu');", true);
            }
            if (txtDen.Text=="")
            {
                if (KtraRong() == true)
                {
                    HeSoLopDong co = new HeSoLopDong();
                    co.MaHSLopDong = txtMaHeSo.Text;
                    co.HinhThucDay = ddlHinhThucDay.SelectedItem.Text;
                    co.Tu = Convert.ToInt32(txtTu.Text);
                    co.Den = null;
                    //co.Coefficient = float.Parse(txtHeSo.Text);
                    double heso;
                    double.TryParse(txtHeSo.Text, out heso);
                    co.HeSo = heso;

                    db.HeSoLopDongs.InsertOnSubmit(co);
                    db.SubmitChanges();
                    LoadGrid();
                    Refresh1();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Bạn đã thêm thành công');", true);
 
                }
            }
            if (txtDen.Text!="")
            {
                if (KtraRong() == true)
                {
                    HeSoLopDong co = new HeSoLopDong();
                    co.MaHSLopDong = txtMaHeSo.Text;
                    co.HinhThucDay = ddlHinhThucDay.SelectedItem.Text;
                    co.Tu = Convert.ToInt32(txtTu.Text);
                    co.Den = Convert.ToInt32(txtDen.Text);
                    //co.Coefficient = float.Parse(txtHeSo.Text);
                    double heso;
                    double.TryParse(txtHeSo.Text, out heso);
                    co.HeSo = heso;
                    db.HeSoLopDongs.InsertOnSubmit(co);
                    db.SubmitChanges();
                    LoadGrid();
                    Refresh1();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Bạn đã thêm thành công');", true);

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
            //if (txtDen.Text == "" && txtTu.Text == "" && txtHeSo.Text == "" && ddlHinhThucDay.SelectedValue == "0")
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Bạn chưa chọn mã muốn sửa');", true);
            //}

            if (txtDen.Text=="")
            {
                 HeSoLopDong co = db.HeSoLopDongs.SingleOrDefault(c => c.MaHSLopDong == txtMaHeSo.Text);
            co.MaHSLopDong = txtMaHeSo.Text;
            co.HinhThucDay = ddlHinhThucDay.SelectedItem.Text;
            co.HeSo =Convert.ToInt32( txtHeSo.Text);
            co.Tu  = Convert.ToInt32(txtTu.Text);
            co.Den = null;
            db.SubmitChanges();
            LoadGrid();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Bạn sửa thành công');", true);
            Refresh1();
            txtHeSo.Focus();
            }
            if (txtDen.Text!="")
            {
                HeSoLopDong co = db.HeSoLopDongs.SingleOrDefault(c => c.MaHSLopDong == txtMaHeSo.Text);
                co.MaHSLopDong = txtMaHeSo.Text;
                co.HinhThucDay = ddlHinhThucDay.SelectedItem.Text;
                co.HeSo = Convert.ToInt32(txtHeSo.Text);
                co.Tu = Convert.ToInt32(txtTu.Text);
                co.Den = Convert.ToInt32(txtDen.Text);
                db.SubmitChanges();
                LoadGrid();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Bạn sửa thành công');", true);
                Refresh1();
                txtHeSo.Focus();

            }
           
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Bạn chưa chọn mã muốn sửa');", true);
        }

    }
    protected void btnXoa_Click(object sender, EventArgs e)
    {
        try
        {         
            HeSoLopDong co = db.HeSoLopDongs.SingleOrDefault(c => c.MaHSLopDong == txtMaHeSo.Text);
            db.HeSoLopDongs.DeleteOnSubmit(co);
            db.SubmitChanges();
            LoadGrid();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Bạn xóa thành công');", true);

            Refresh1();
            txtDen.Focus();
        }

        catch (Exception)
        {
            throw;
        }
    }
    protected void GrvHeSoLopDong_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GrvHeSoLopDong.PageIndex=e.NewPageIndex;
        LoadGrid();

    }
    protected void btnMoi_Click(object sender, EventArgs e)
    {
        Refresh1();
    }
}