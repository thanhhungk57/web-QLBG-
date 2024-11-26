using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace TeachingManagers
{
    public partial class QuanLyBaiGiangGV : System.Web.UI.Page
    {
        private string connectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            connectionString = ConfigurationManager.ConnectionStrings["QuanLyGiangVien"].ConnectionString;

            if (!IsPostBack)
            {
                BindClassDropDown();
                BindSubjectDropDown();
                BindLectureData();
            }
        }

        protected void btnThem_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("ThemBaiGiang", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MaLop", ddlLop.SelectedValue);
                cmd.Parameters.AddWithValue("@MaMon", ddlMon.SelectedValue);
                cmd.Parameters.AddWithValue("@TenBaiGiang", txtTenBaiGiang.Text);
                cmd.Parameters.AddWithValue("@BaiGiangPath", txtBaiGiangPath.Text);
                cmd.Parameters.AddWithValue("@BaiTapPath", txtBaiTapPath.Text);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    lblThongBao.Text = "Thêm mới thành công!";
                    BindLectureData();
                }
                catch (Exception ex)
                {
                    lblThongBao.Text = "Error: " + ex.Message;
                }
            }
        }

        protected void btnSua_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SuaBaiGiang", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MaBaiGiang", ViewState["MaBaiGiang"]);
                cmd.Parameters.AddWithValue("@MaLop", ddlLop.SelectedValue);
                cmd.Parameters.AddWithValue("@MaMon", ddlMon.SelectedValue);
                cmd.Parameters.AddWithValue("@TenBaiGiang", txtTenBaiGiang.Text);
                cmd.Parameters.AddWithValue("@BaiGiangPath", txtBaiGiangPath.Text);
                cmd.Parameters.AddWithValue("@BaiTapPath", txtBaiTapPath.Text);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    lblThongBao.Text = "Sửa thành công!";
                    BindLectureData();
                }
                catch (Exception ex)
                {
                    lblThongBao.Text = "Error: " + ex.Message;
                }
            }
        }

        protected void btnXoa_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("XoaBaiGiang", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MaBaiGiang", ViewState["MaBaiGiang"]);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    lblThongBao.Text = "Xóa thành công!";
                    BindLectureData();
                }
                catch (Exception ex)
                {
                    lblThongBao.Text = "Error: " + ex.Message;
                }
            }
        }

        protected void btRefresh_Click(object sender, EventArgs e)
        {
            ddlLop.SelectedIndex = 0;
            ddlMon.SelectedIndex = 0;
            txtTenBaiGiang.Text = "";
            txtBaiGiangPath.Text = "";
            txtBaiTapPath.Text = "";
            lblThongBao.Text = "";
        }

        private void BindLectureData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("LayThongTinBaiGiang", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                try
                {
                    conn.Open();
                    da.Fill(dt);
                    grvDoAn.DataSource = dt;
                    grvDoAn.DataBind();
                }
                catch (Exception ex)
                {
                    lblThongBao.Text = "Error: " + ex.Message;
                }
            }
        }

        protected void grvDoAn_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvDoAn.PageIndex = e.NewPageIndex;
            BindLectureData();
        }

        private void BindClassDropDown()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT MaLop, TenLop FROM Lop";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();

                try
                {
                    conn.Open();
                    da.Fill(dt);
                    ddlLop.DataSource = dt;
                    ddlLop.DataTextField = "TenLop";
                    ddlLop.DataValueField = "MaLop";
                    ddlLop.DataBind();
                }
                catch (Exception ex)
                {
                    lblThongBao.Text = "Error: " + ex.Message;
                }
            }
        }

        private void BindSubjectDropDown()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT MaMon, TenMon FROM MonHoc";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();

                try
                {
                    conn.Open();
                    da.Fill(dt);
                    ddlMon.DataSource = dt;
                    ddlMon.DataTextField = "TenMon";
                    ddlMon.DataValueField = "MaMon";
                    ddlMon.DataBind();
                }
                catch (Exception ex)
                {
                    lblThongBao.Text = "Error: " + ex.Message;
                }
            }
        }
    }
}
