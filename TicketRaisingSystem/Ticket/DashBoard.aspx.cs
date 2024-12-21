using System;

namespace TicketRaisingSystem.Ticket
{
    public partial class DashBoard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Role"]==null&& Session["Email"]==null)
                {
                    Response.Redirect("LoginForm.aspx");
                }
            }
        }
    }
}