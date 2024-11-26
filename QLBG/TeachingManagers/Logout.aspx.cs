using System;
using System.Data.SqlClient;
using System.Linq;

public partial class Admin_Logout : System.Web.UI.Page
{

    protected void btnHome_Click(object sender, EventArgs e)
    {
        Response.Redirect("ChaoMung.aspx");
    }

    private bool CheckMaHSTonTai(string maHS)
    {
        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-MROL53J;Initial Catalog=QuanLyGiangVien;Integrated Security=True");

        {
            connection.Open();
            string query = "SELECT COUNT(*) FROM HocSinh WHERE MaHS = @MaHS";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@MaHS", maHS);
            int count = (int)command.ExecuteScalar();
            return count > 0;
        }
    }

    private bool CheckMaGVTonTai(string maGV)
    {
        using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-MROL53J;Initial Catalog=QuanLyGiangVien;Integrated Security=True"))
        {
            connection.Open();
            string query = "SELECT COUNT(*) FROM GiaoVien WHERE MaGV = @MaGV";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@MaGV", maGV);
            int count = (int)command.ExecuteScalar();
            return count > 0;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        hplQuenMK.Visible = true;
        Session.Contents["TrangThai"] = "ChuaDangNhap";
        Session["MemberID"] = "";
    }

    QuanLyGiangVienDataContext tm = new QuanLyGiangVienDataContext();
    MaHoa mh = new MaHoa();

    protected void btTaoTaiKhoan_Click(object sender, EventArgs e)
    {
        // Kiểm tra xem đã nhập đủ thông tin hay chưa
        if (string.IsNullOrEmpty(txtUserName.Text) || string.IsNullOrEmpty(txtPassword.Text) || string.IsNullOrEmpty(txtMa.Text))
        {
            lblthongbao.Text = "Hãy điền đầy đủ thông tin.";
            return;
        }

        // Kiểm tra xem Mã đã tồn tại trong bảng HocSinh hay không
        if (CheckMaHSTonTai(txtMa.Text))
        {
            // Nếu Mã tồn tại trong bảng HocSinh, thêm tài khoản mới và gán Mã Học Sinh vào
            string insertQuery = "INSERT INTO TaiKhoan (TenDangNhap, MatKhau, NgayTaoTK, Quyen, MaTK) VALUES (@TenDangNhap, @MatKhau, @NgayTaoTK, @Quyen, @MaTK)";
            using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-MROL53J;Initial Catalog=QuanLyGiangVien;Integrated Security=True"))
            {
                connection.Open();
                SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
                insertCommand.Parameters.AddWithValue("@TenDangNhap", txtUserName.Text);
                insertCommand.Parameters.AddWithValue("@MatKhau", txtPassword.Text);
                insertCommand.Parameters.AddWithValue("@NgayTaoTK", DateTime.Now);
                insertCommand.Parameters.AddWithValue("@Quyen", "QuyenCuaTaiKhoan"); // Thay "QuyenCuaTaiKhoan" bằng quyền cụ thể của tài khoản mới
                insertCommand.Parameters.AddWithValue("@MaTK", txtMa.Text);

                try
                {
                    insertCommand.ExecuteNonQuery();
                    lblthongbao.Text = "Tạo tài khoản thành công!";
                }
                catch (SqlException ex)
                {
                    lblthongbao.Text = "Đã xảy ra lỗi khi tạo tài khoản: " + ex.Message;
                }
            }
        }
        else if (CheckMaGVTonTai(txtMa.Text))
        {
            // Nếu Mã tồn tại trong bảng GiaoVien, thêm tài khoản mới và gán Mã Giáo Viên vào
            string insertQuery = "INSERT INTO TaiKhoan (TenDangNhap, MatKhau, NgayTaoTK, Quyen, MaTK) VALUES (@TenDangNhap, @MatKhau, @NgayTaoTK, @Quyen, @MaTK)";
            using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-MROL53J;Initial Catalog=QuanLyGiangVien;Integrated Security=True"))
            {
                connection.Open();
                SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
                insertCommand.Parameters.AddWithValue("@TenDangNhap", txtUserName.Text);
                insertCommand.Parameters.AddWithValue("@MatKhau", txtPassword.Text);
                insertCommand.Parameters.AddWithValue("@NgayTaoTK", DateTime.Now);
                insertCommand.Parameters.AddWithValue("@Quyen", "QuyenCuaTaiKhoan"); // Thay "QuyenCuaTaiKhoan" bằng quyền cụ thể của tài khoản mới
                insertCommand.Parameters.AddWithValue("@MaTK", txtMa.Text);

                try
                {
                    insertCommand.ExecuteNonQuery();
                    lblthongbao.Text = "Tạo tài khoản thành công!";
                }
                catch (SqlException ex)
                {
                    lblthongbao.Text = "Đã xảy ra lỗi khi tạo tài khoản: " + ex.Message;
                }
            }

        }
        else
        {
            // Nếu Mã không tồn tại trong bảng HocSinh hoặc GiaoVien, thông báo lỗi
            lblthongbao.Text = "Mã không tồn tại trong hệ thống.";
        }
    }

}



