using System;
using System.Data.SqlClient;

public partial class Admin_Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        hplQuenMK.Visible = true;
    }

    protected void btDangNhap_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtUserName.Text.Trim()) || string.IsNullOrEmpty(txtPassword.Text.Trim()))
        {
            lblthongbao.Text = "Hãy kiểm tra lại tên đăng nhập và mật khẩu";
            return;
        }

        string connectionString = "Data Source=DESKTOP-MROL53J;Initial Catalog=QuanLyGiangVien;Integrated Security=True";
        string query = "SELECT * FROM TaiKhoan WHERE TenDangNhap = @TenDangNhap AND MatKhau = @MatKhau";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@TenDangNhap", txtUserName.Text);
                command.Parameters.AddWithValue("@MatKhau", txtPassword.Text);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    Session["Dangnhap"] = txtUserName.Text;
                    Session["MemberID"] = reader["MaTK"];
                    Session["TrangThai"] = "DaDangNhap";

                    string quyen = reader["Quyen"].ToString();
                    if (quyen == "Giáo vụ")
                    {
                        Response.Redirect("ChaoMung.aspx");
                    }
                    else if (quyen == "Giáo viên")
                    {
                        Response.Redirect("ChaoMung.aspx");
                    }
                    else if (quyen == "Học sinh")
                    {
                        Response.Redirect("ChaoMung.aspx");
                    }
                    else
                    {
                        lblthongbao.Text = "Tài khoản không có quyền truy cập.";
                    }
                }
                else
                {
                    lblthongbao.Text = "Bạn đăng nhập không thành công";
                    hplQuenMK.Visible = false;
                }
            }
        }
    }
}
