using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.UI.WebControls;

public partial class Admin_DoAnTotNghiep : System.Web.UI.Page
{
    // Load danh sách năm học từ cơ sở dữ liệu hoặc mãng
    private DataTable GetNamHocList()
    {
        // Thực hiện truy vấn cơ sở dữ liệu hoặc tạo DataTable từ mảng
        DataTable dtNamHoc = new DataTable();
        // Thực hiện các thao tác để lấy danh sách năm học
        return dtNamHoc;
    }

    // Load danh sách lớp học từ cơ sở dữ liệu
    private DataTable GetLopHocList()
    {
        // Thực hiện truy vấn cơ sở dữ liệu hoặc tạo DataTable từ mảng
        DataTable dtLopHoc = new DataTable();
        // Thực hiện các thao tác để lấy danh sách lớp học
        return dtLopHoc;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["TrangThai"] == null || Session["TrangThai"].ToString() != "DaDangNhap")
        {
            Response.Redirect("Login.aspx?url=" + Request.Url.PathAndQuery);
            return;
        }

        if (!IsPostBack)
        {
            LoadNamHoc();
            LoadLopHoc();
            ddlNamHoc.Items.Insert(0, new ListItem("--Chọn năm học--", "0"));
        }

        LoadRegisteredInfo();
    }

    // Load thông tin đăng ký cho học sinh
    private void LoadRegisteredInfo()
    {
        string maHS = Session["MemberID"].ToString();
        DataTable dtRegisteredInfo = GetRegisteredInfo(maHS);

        if (dtRegisteredInfo != null && dtRegisteredInfo.Rows.Count > 0)
        {
            gvRegisteredInfo.DataSource = dtRegisteredInfo;
            gvRegisteredInfo.DataBind();
        }
        else
        {
            gvRegisteredInfo.DataSource = null;
            gvRegisteredInfo.DataBind();
        }
    }

    // Lấy thông tin đăng ký từ stored procedure9
    private DataTable GetRegisteredInfo(string maHS)
    {
        DataTable dtResult = new DataTable();
        string connectionString = ConfigurationManager.ConnectionStrings["QuanLyGiangVien"].ConnectionString;
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            SqlCommand command = new SqlCommand("LayThongTinDangKy", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@MaHS", maHS);

            try
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dtResult);
            }
            catch (Exception ex)
            {
                // Xử lý exception nếu cần
                // Ví dụ: lblThongBao.Text = "Có lỗi xảy ra: " + ex.Message;
            }

            return dtResult;
        }
    }

    // Thêm mới thông tin đăng ký
    protected void btnThem_Click(object sender, EventArgs e)
    {
        string maHS = Session["MemberID"].ToString();
        string maLop = ddlLop.SelectedValue;
        string maMon = ddlMonHoc.SelectedValue;
        string maGV = ddlGiaoVien.SelectedValue;
        string namHoc = ddlNamHoc.SelectedItem.Text;
        string ghiChu = txtGhiChu.Text;

        if (string.IsNullOrEmpty(maLop) || string.IsNullOrEmpty(maMon) || string.IsNullOrEmpty(maGV) || string.IsNullOrEmpty(namHoc))
        {
            lblThongBao.Text = "Vui lòng điền đầy đủ thông tin.";
            return;
        }

        // Gọi stored procedure DangKyMonHocChoHocSinh để thêm mới
        if (AddNewRegistration(maHS, maLop, maMon, maGV, namHoc, ghiChu))
        {
            // Load lại thông tin đăng ký sau khi thêm mới
            LoadRegisteredInfo();

            // Xóa các giá trị đã nhập sau khi thêm mới thành công
            ddlLop.SelectedIndex = 0;
            ddlMonHoc.SelectedIndex = 0;
            ddlGiaoVien.SelectedIndex = 0;
            ddlNamHoc.SelectedIndex = 0;
            txtGhiChu.Text = "";

            lblThongBao.Text = "Thêm mới thành công.";
        }
        else
        {
            lblThongBao.Text = "Thêm mới không thành công.";
        }
    }

    // Thực hiện thêm mới thông tin đăng ký bằng stored procedure
    private bool AddNewRegistration(string maHS, string maLop, string maMon, string maGV, string namHoc, string ghiChu)
    {
        string connectionString = WebConfigurationManager.ConnectionStrings["YourConnectionString"].ConnectionString;
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            SqlCommand command = new SqlCommand("DangKyMonHocChoHocSinh", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@MaHS", maHS);
            command.Parameters.AddWithValue("@MaLop", maLop);
            command.Parameters.AddWithValue("@MaMon", maMon);
            command.Parameters.AddWithValue("@MaGV", maGV);
            command.Parameters.AddWithValue("@NamHoc", namHoc);
            command.Parameters.AddWithValue("@GhiChu", ghiChu);

            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                // Xử lý exception nếu cần
                // Ví dụ: lblThongBao.Text = "Có lỗi xảy ra khi thêm mới: " + ex.Message;
                return false;
            }
        }
    }

    // Load danh sách năm học từ cơ sở dữ liệu hoặc mãng
    private void LoadNamHoc()
    {
        // Logic để load danh sách năm học
        // Ví dụ:
        ddlNamHoc.DataSource = GetNamHocList(); // Hàm GetNamHocList() trả về danh sách năm học
        ddlNamHoc.DataTextField = "TenNamHoc";
        ddlNamHoc.DataValueField = "MaNamHoc";
        ddlNamHoc.DataBind();
    }

    // Load danh sách lớp học từ cơ sở dữ liệu
    private void LoadLopHoc()
    {
        // Logic để load danh sách lớp học
        // Ví dụ:
        ddlLop.DataSource = GetLopHocList(); // Hàm GetLopHocList() trả về danh sách lớp học
        ddlLop.DataTextField = "TenLop";
        ddlLop.DataValueField = "MaLop";
        ddlLop.DataBind();
    }
}
