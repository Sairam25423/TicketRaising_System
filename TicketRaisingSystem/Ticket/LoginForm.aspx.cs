using System;
using System.Data;
using System.Web.Security;
using TicketRaisingSystem.App_Start;
namespace TicketRaisingSystem.Ticket
{
    public partial class LoginForm : System.Web.UI.Page
    {
        private const int MaxFailedAttempts = 3;  // Max failed attempts before locking
        private const string FailedAttemptsSessionKey = "FailedAttempts";
        DataAccessLayer ObjDl = new DataAccessLayer();
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!(string.IsNullOrEmpty(txtUsername.Text.Trim()) && string.IsNullOrWhiteSpace(txtPassword.Text.Trim())))
            {
                // Check if the user is locked out 
                if (Session[FailedAttemptsSessionKey] != null && (int)Session[FailedAttemptsSessionKey] >= MaxFailedAttempts)
                {
                    string script = @"Swal.fire({title: 'Failure!',text: 'Your account is locked due to multiple failed login attempts. Please try again later.',icon: 'error',confirmButtonText: 'OK'});";
                    ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", script, true);
                    return;
                }
                else
                {
                    // Validate the username and password with the membership provider
                    if (Membership.ValidateUser(txtUsername.Text.Trim(), txtPassword.Text.Trim()))
                    {
                        // Reset failed attempts on successful login
                        Session[FailedAttemptsSessionKey] = null;
                        string[] ParamName = { "@Email" }; string[] ParamVal = { txtUsername.Text.Trim() };
                        DataSet Result = ObjDl.RetriveData("Sp_UserData", ParamName, ParamVal);
                        if (Result != null && Result.Tables.Count > 0 && Result.Tables[0].Rows.Count > 0)
                        {
                            DataRow row = Result.Tables[0].Rows[0];
                            Session["UserCode"] = row["UserCode"].ToString();
                            Session["UserName"] = row["UserName"].ToString();
                            Session["Mobile"] = row["Mobile"].ToString();
                            Session["Email"] = row["Email"].ToString();
                            Session["Dob"] = row["Dob"].ToString();
                            Session["Role"] = row["Role"].ToString();
                            Session["Created_at"] = row["Created_at"].ToString();
                            Session["Updated_at"] = row["Updated_at"].ToString();
                            Response.Redirect("DashBoard.aspx");  // Redirect to another page upon successful login
                        }
                    }
                    else
                    {
                        // Increment the failed attempts counter
                        if (Session[FailedAttemptsSessionKey] == null)
                        {
                            Session[FailedAttemptsSessionKey] = 1;
                        }
                        else
                        {
                            Session[FailedAttemptsSessionKey] = (int)Session[FailedAttemptsSessionKey] + 1;
                        }
                        // Show error message
                        string script = @"Swal.fire({title: 'Failure!',text: 'Invalid username or password.',icon: 'error',confirmButtonText: 'OK'});";
                        ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", script, true);
                        // Lock account after 3 failed attempts
                        if ((int)Session[FailedAttemptsSessionKey] >= MaxFailedAttempts)
                        {
                            string script1 = @"Swal.fire({title: 'Failure!',text: 'Your account is locked due to multiple failed login attempts. Please try again later.',icon: 'error',confirmButtonText: 'OK'});";
                            ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", script1, true);
                        }
                    }
                }
            }
            else
            {
                string script = @"Swal.fire({title: 'Failure!',text: 'Please enter Username & Password.',icon: 'error',confirmButtonText: 'OK'});";
                ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", script, true);
            }
        }
    }
}