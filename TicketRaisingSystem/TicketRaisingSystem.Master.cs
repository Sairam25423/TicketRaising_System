using System;
using System.Data;
using System.Web.Security;

namespace TicketRaisingSystem
{
    public partial class TicketRaisingSystem : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void lnkLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            FormsAuthentication.SignOut();
        }
    }
}