using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Linq;

public partial class MasterPage : System.Web.UI.MasterPage
{
    QuanLyGiangVienDataContext st = new QuanLyGiangVienDataContext();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Dangnhap"] != null)
        {
            var taiKhoan = st.TaiKhoans.SingleOrDefault(c => c.TenDangNhap == Session["Dangnhap"].ToString());

            if (taiKhoan != null)
            {
                if (taiKhoan.MaGV != null)
                {
                    linkThongTinCaNhan.Visible = true;
                    linkThongTinCaNhanHS.Visible = false;
                    linkBaiGiangHS.Visible = false;
                    linkBaiGiangGV.Visible = true;
                    linkDangKy.Visible = false;

                }
                else if (taiKhoan.MaGV == null)
                {
                    linkThongTinCaNhan.Visible = false;
                    linkThongTinCaNhanHS.Visible = true;
                    linkBaiGiangHS.Visible = true;
                    linkBaiGiangGV.Visible = false;
                    linkDangKy.Visible = true;
                }
            }
        }
    }
}

