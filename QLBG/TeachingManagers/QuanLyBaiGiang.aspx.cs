using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace TeachingManagers
{
    public partial class QuanLyBaiGiang : System.Web.UI.Page
    {
        string connectionString = "Data Source=DESKTOP-MROL53J;Initial Catalog=QuanLyGiangVien;Integrated Security=True";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindLectureData();
            }
        }

        private void BindLectureData()
        {
            // Tạo kết nối và truy vấn SQL
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"SELECT g.MaBaiGiang, g.MaLop, m.TenMon, g.TenBaiGiang, g.filepath as BaiGiangPath, t.filepath as BaiTapPath
                                 FROM BaiGiang g 
                                 JOIN BaiTap t ON g.MaBaiGiang = t.MaBaiGiang
                                 JOIN MonHoc m ON g.MaMon = m.MaMon";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();

                try
                {
                    // Mở kết nối và điền dữ liệu vào DataTable
                    conn.Open();
                    da.Fill(dt);

                    // Liên kết dữ liệu với GridView
                    gvLectures.DataSource = dt;
                    gvLectures.DataBind();
                }
                catch (Exception ex)
                {
                    // Xử lý lỗi
                    Response.Write("Error: " + ex.Message);
                }
            }
        }

        protected void gvLectures_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Implement any action to be taken when a row is selected, if needed
        }
    }
}
