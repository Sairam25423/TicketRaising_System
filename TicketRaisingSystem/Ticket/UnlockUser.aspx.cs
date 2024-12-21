using System;
using System.Web.Security;

namespace TicketRaisingSystem.Ticket
{
    public partial class UnlockUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnUnlock_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtEmail.Text.Trim()))
            {
                MembershipUser user = Membership.GetUser(txtEmail.Text.Trim());
                if (user != null)
                {
                    if (user.IsLockedOut)
                    {
                        bool isUnlocked = user.UnlockUser();
                        if (isUnlocked)
                        {
                            string script = @"Swal.fire({title: 'Success!',text: 'The user has been successfully unlocked.',icon: 'success',confirmButtonText: 'OK'});";
                            ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", script, true);
                            Response.Redirect("LoginForm.aspx");
                        }
                        else
                        {
                            string script = @"Swal.fire({title: 'Failure!',text: 'Failed to unlock the user.',icon: 'error',confirmButtonText: 'OK'});";
                            ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", script, true);
                        }
                    }
                    else
                    {
                        string script = @"Swal.fire({title: 'Failure!',text: 'The user is not locked.',icon: 'error',confirmButtonText: 'OK'});";
                        ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", script, true);
                    }
                }
                else
                {
                    string script = @"Swal.fire({title: 'Failure!',text: 'User does not exist.',icon: 'error',confirmButtonText: 'OK'});";
                    ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", script, true);
                }
                txtEmail.Text = string.Empty;
            }
            else
            {
                string script = @"Swal.fire({title: 'Failure!',text: 'Please enter the Email.',icon: 'error',confirmButtonText: 'OK'});";
                ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", script, true);
            }
        }
    }
}