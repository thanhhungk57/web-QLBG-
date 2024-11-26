using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_ThongTinCaNhan : System.Web.UI.Page
{
    QuanLyGiangVienDataContext st = new QuanLyGiangVienDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["Dangnhap"] != null) && (Session.Contents["TrangThai"].ToString() == "DaDangNhap"))
        {
            var tt = from c in st.TaiKhoans where c.TenDangNhap == Session["Dangnhap"].ToString() select new { c.TenDangNhap };

            foreach (var item in tt)
            {
                lblThongtin.Text = "Thông tin cá nhân của bạn:&nbsp;" + item.TenDangNhap.Trim().ToString();
            }
            //Download source code FREE tai Sharecode.vn
        }
        else
            if ((Session.Contents["TrangThai"].ToString() == "ChuaDangNhap") && (Session["Dangnhap"] == null))
            {
                Response.Redirect("Login.aspx?url=" + Request.Url.PathAndQuery);
            }
        if (!IsPostBack)
        {
            //Download source code FREE tai Sharecode.vn
            LoadThongTin(Session["Dangnhap"].ToString());
            //LoadThongTin(Convert.ToInt32(Session["Dangnhap"]));

        }

    }
    /// <summary>
    /// Xây dựng phương thức lấy về thông tin của người đăng nhập
    /// </summary>
    /// <param name="tendn"></param>
    public void LoadThongTin(string tendn)
    {
        try
        {
             string ten = (Session["Dangnhap"].ToString());
            //st_ThongtincanhanResult thongtin = st.st_Thongtincanhan(ten).FirstOrDefault();
             //ThongtincanhanResult thongtin = st.st_Thongtincanhan().FirstOrDefault();
             var thongtin = st.st_Thongtincanhan(ten).FirstOrDefault();
             //st_ThongtincanhanResult thongtin = st.st_Thongtincanhan(t).FirstOrDefault();
            txtTenDN.Text = Session["Dangnhap"].ToString();
            txtHoten.Text = thongtin.tengv;
            string[] mang = thongtin.ngaysinh.ToString().Split(' ');
            txtNgaysinh.Text = mang[0].ToString().Trim();
            txtGioiTinh.Text = thongtin.gioitinh;
            txtCMND.Text = thongtin.socmtnd;
            txtBoMon.Text = thongtin.tenbomon;
            txtTDHVan.Text = thongtin.trinhdohocvan;
            txtChucDanh.Text = thongtin.tenchucdanh;
            txtNamVaoLam.Text = thongtin.namvaolam;
            txtEmail.Text = thongtin.email;
            txtDiaChi.Text = thongtin.diachi;
            txtGhiChu.Text = thongtin.ghichu;
            //imgAnhDaiDien.ImageUrl = thongtin.anh;
        }
        catch (Exception)
        {
        }
    }
    public void refresh()
    {
        txtHoten.Text = "";
        txtNgaysinh.Text = "";
        txtGioiTinh.Text = "";
        txtCMND.Text = "";
        txtTDHVan.Text = "";
        txtNamVaoLam.Text = "";
        txtEmail.Text = "";
        txtDiaChi.Text = "";
        txtGhiChu.Text = "";


    }
    protected void btnCapNhat_Click(object sender, EventArgs e)
    {
        TaiKhoan thongtintv = st.TaiKhoans.SingleOrDefault(c => c.TenDangNhap == Session["Dangnhap"].ToString() && c.MaGV == c.GiaoVien.MaGV);
        thongtintv.GiaoVien.TenGV = txtHoten.Text;
        thongtintv.GiaoVien.NgaySinh = DateTime.Parse(txtNgaysinh.Text);
        thongtintv.GiaoVien.GioiTinh = txtGioiTinh.Text;
        thongtintv.GiaoVien.SoCMTND = txtCMND.Text;
        thongtintv.GiaoVien.TrinhDoHocVan = txtTDHVan.Text;
        thongtintv.GiaoVien.NamVaoLam = txtNamVaoLam.Text;
        thongtintv.GiaoVien.Email = txtEmail.Text;
        thongtintv.GiaoVien.DiaChi = txtDiaChi.Text;
        thongtintv.GiaoVien.GhiChu = txtGhiChu.Text;
        st.SubmitChanges();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Bạn cập nhật thông tin thành công');", true);
        Response.Redirect("ThongTinCaNhan.aspx");
        //refresh();
    }
}