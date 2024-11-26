using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;

public partial class Admin_ThongTinCaNhan : Page
{
    private string connectionString = "Data Source=DESKTOP-MROL53J;Initial Catalog=QuanLyGiangVien;Integrated Security=True";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Dangnhap"] != null && Session["TrangThai"].ToString() == "DaDangNhap")
        {
            lblThongtin.Text = "Thông tin cá nhân của bạn:&nbsp;" + Session["Dangnhap"];
        }
        else if (Session["TrangThai"].ToString() == "ChuaDangNhap" && Session["Dangnhap"] == null)
        {
            Response.Redirect("Login.aspx?url=" + Request.Url.PathAndQuery);
        }

        if (!IsPostBack)
        {
            LoadThongTin(Session["Dangnhap"].ToString());
        }
    }

    public void LoadThongTin(string tendn)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            string query = @"SELECT hs.HoTenHS, hs.NgaySinh, hs.GioiTinh, hs.DiaChi, hs.SDT,hs.Email
                             FROM TaiKhoan tk
                             JOIN HocSinh hs ON tk.MaHS = hs.MaHS
                             WHERE tk.TenDangNhap = @TenDangNhap";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@TenDangNhap", tendn);

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                txtTenDN.Text = tendn;
                txtHoten.Text = reader["HoTenHS"].ToString();
                txtNgaysinh.Text = Convert.ToDateTime(reader["NgaySinh"]).ToString("yyyy-MM-dd");
                txtGioiTinh.Text = reader["GioiTinh"].ToString();
                txtDiaChi.Text = reader["DiaChi"].ToString();
                txtCMND.Text = reader["SDT"].ToString();
                txtEmail.Text = reader["Email"].ToString();
            }
        }
    }

    protected void btnCapNhat_Click(object sender, EventArgs e)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            string query = @"UPDATE HocSinh
                             SET HoTenHS = @HoTenHS,
                                 NgaySinh = @NgaySinh,
                                 GioiTinh = @GioiTinh,
                                 DiaChi = @DiaChi,
                                 SDT = @SDT
                             WHERE MaHS = (SELECT MaHS FROM TaiKhoan WHERE TenDangNhap = @TenDangNhap)";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@HoTenHS", txtHoten.Text);
            cmd.Parameters.AddWithValue("@NgaySinh", Convert.ToDateTime(txtNgaysinh.Text));
            cmd.Parameters.AddWithValue("@GioiTinh", txtGioiTinh.Text);
            cmd.Parameters.AddWithValue("@DiaChi", txtDiaChi.Text);
            cmd.Parameters.AddWithValue("@SDT", txtCMND.Text);
            cmd.Parameters.AddWithValue("@TenDangNhap", Session["Dangnhap"].ToString());

            cmd.ExecuteNonQuery();

            ScriptManager.RegisterStartupScript(this, GetType(), "Alert", "alert('Bạn cập nhật thông tin thành công');", true);
            Response.Redirect("ThongTinCaNhanHS.aspx");
        }
    }
}
