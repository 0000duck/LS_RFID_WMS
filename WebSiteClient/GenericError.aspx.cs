using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class GenericError : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Application["AppError"] != null)
        {
            if (Session["RoleID"].ToString() == "1")
            {
                lblErrMsg.Text = Application["AppError"].ToString();
                lblErrMsg.Visible = true;
            }
        }
    }
}
