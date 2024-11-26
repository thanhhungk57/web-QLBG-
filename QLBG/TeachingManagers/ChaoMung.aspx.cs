using System;
using System.Activities.Expressions;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e, Label lblThongTin)
    {
        if (Session["TrangThai"] != null && Session["TrangThai"].ToString() == "DaDangNhap")
        {
            if (Session["MemberID"] != null)
            {
                string quyen = Session["Quyen"].ToString();
                if (quyen == "Giáo vụ" || quyen == "Giáo viên" || quyen == "Học sinh")
                {

                    lblThongTin.Text = "Xin chào: ";
                }
                else
                {
                    Response.Redirect("Login.aspx?url=" + Request.Url.PathAndQuery);
                }
            }
            else
            {
                Response.Redirect("Login.aspx?url=" + Request.Url.PathAndQuery);
            }
        }
        else
        {
            Response.Redirect("Login.aspx?url=" + Request.Url.PathAndQuery);
        }
    }

}

