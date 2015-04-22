using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class desktop_home : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //try
        //{
            using (CCSEntities db = new CCSEntities())
            {
                int id = 0;
                try
                {
                    id = int.Parse(Session["UserId"].ToString());
                }
                catch
                {
                    Response.Redirect(Config.DOMAIN()+"login.aspx?callback=/desktop/home.aspx");
                }

                var userInfoQuery = (from c in db.Users
                                     where c.UserID == id
                                     select new { c.UserID, c.UserName, c.FirstName, c.LastName, c.Admin });

                grdUserInfo.DataSource = userInfoQuery.ToList();
                grdUserInfo.DataBind();
            }
        //}
        //catch (System.Threading.ThreadAbortException) { }
        //catch (Exception ex)
        //{
        //    LogError.logError(ex);
        //    Response.Redirect("../errorpages/error.aspx");
        //}
    }
}