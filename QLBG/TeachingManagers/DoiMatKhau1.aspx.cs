using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class Admin_DoiMatKhau : System.Web.UI.Page
{
    QuanLyGiangVienDataContext db = new QuanLyGiangVienDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["Dangnhap"] != null) && (Session.Contents["TrangThai"].ToString() == "DaDangNhap"))
        {
            var tt = from c in db.TaiKhoans where c.TenDangNhap == Session["Dangnhap"].ToString() select new { c.TenDangNhap };

            foreach (var item in tt)
            {
                txtUserName.Text = item.TenDangNhap.Trim();

            }
        }
        else
            if ((Session.Contents["TrangThai"].ToString() == "ChuaDangNhap") && (Session["Dangnhap"] == null))
            {
                Response.Redirect("Login.aspx?url=" + Request.Url.PathAndQuery);
            }
       
    }
    protected void btDoiMK_Click(object sender, EventArgs e)
    {
        TaiKhoan ac = db.TaiKhoans.SingleOrDefault(c => c.TenDangNhap == txtUserName.Text && c.MaGV == c.GiaoVien.MaGV && c.MatKhau == txtPasswordcu.Text.Trim());
        if (txtnhappassmoi.Text == txtpassmoi.Text)
        {
            ac.MatKhau = txtpassmoi.Text;
            //db.SubmitChanges();

            db.SubmitChanges();
            lblThongbao.Text = "Bạn đổi mật khẩu thành công";
            Response.Redirect("ThongTinCaNhan.aspx");
        }
        else
            lblThongbao.Text = "Bạn nhập lại mật khẩu mới không đúng";
           
    }
}
