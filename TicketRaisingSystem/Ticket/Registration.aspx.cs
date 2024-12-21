using System;
using System.IO;
using System.Net.Mail;
using System.Net;
using TicketRaisingSystem.App_Start;
using System.Data;

namespace TicketRaisingSystem.Ticket
{
    public partial class Registration : System.Web.UI.Page
    {
        DataAccessLayer objDl = new DataAccessLayer();
        BussinessLogicLayer objBL = new BussinessLogicLayer();
        #region PageLoad
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtDob.Attributes["max"] = DateTime.Now.ToString("yyyy-MM-dd");
                txtDob.Attributes["min"] = DateTime.Now.ToString("1947-01-01");
            }
        }
        #endregion
        #region SignUp
        protected void btnSignup_Click(object sender, EventArgs e)
        {
            if (chkAgree.Checked == true)
            {
                if (!(string.IsNullOrEmpty(txtName.Text.Trim()) && string.IsNullOrEmpty(txtEmail.Text.Trim()) && string.IsNullOrWhiteSpace(txtDob.Text.Trim())
                && string.IsNullOrEmpty(txtPassword.Text.Trim()) && string.IsNullOrEmpty(txtMobile.Text.Trim())))
                {
                    var Result = objBL.CreateUser(txtName.Text.Trim(), txtPassword.Text.Trim(), txtEmail.Text.Trim(), txtMobile.Text.Trim(), txtDob.Text.Trim());
                    if (Result != null)
                    {
                        string script;
                        if (Result.Status == true && Result.Message == "The user account was successfully created")
                        {
                            txtName.Text = txtEmail.Text = txtDob.Text = txtPassword.Text = txtMobile.Text = txtDob.Text = txtConfirmPassword.Text = txtVerifyOtp.Text = string.Empty; chkAgree.Checked = false;
                            script = $@"Swal.fire({{title: 'Success!', text: '{Result.Message}', icon: 'success', confirmButtonText: 'OK'}});";
                        }
                        else
                        {
                            script = $@"Swal.fire({{title: 'Failure!', text: '{Result.Message}', icon: 'error', confirmButtonText: 'OK'}});";
                        }
                        ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", script, true);
                    }
                }
                else
                {
                    string script = @"Swal.fire({title: 'Failure!',text: 'Please fill all the details.',icon: 'error',confirmButtonText: 'OK'});";
                    ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", script, true);
                }
            }
            else
            {
                string script = @"Swal.fire({title: 'Failure!',text: 'Please caaept the terms & Conditions.',icon: 'error',confirmButtonText: 'OK'});";
                ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", script, true);
            }
        }
        #endregion
        #region BtnSendOtp
        protected void btnSendOtp_Click(object sender, EventArgs e)
        {
            string Result = string.Empty;
            if (!string.IsNullOrEmpty(txtEmail.Text))
            {
                Random Rnd = new Random();
                string Otp = Rnd.Next(100000, 999999).ToString();
                DateTime CreatedAt = DateTime.Now; DateTime ExpiryAt = CreatedAt.AddMinutes(10);
                string StoredProc = "Sp_OtpCount"; string[] ParamNames = { "@Email" }; string[] ParamValues = { txtEmail.Text };
                DataSet Ds = objDl.RetriveData(StoredProc, ParamNames, ParamValues);
                if (Ds.Tables[0].Rows.Count > 0)
                {
                    Result = Ds.Tables[0].Rows[0]["RequestCount"].ToString();
                }
                if (string.IsNullOrEmpty(Result))
                {
                    bool Request = OtpData(txtEmail.Text, Otp, CreatedAt.ToString(), ExpiryAt.ToString());
                    if (Request == true)
                    {
                        string script = @"Swal.fire({title: 'Success!',text: 'An OTP has been sent to your registered email. Please verify it and proceed with the further registration process.',icon: 'success',confirmButtonText: 'OK'});";
                        ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", script, true);
                    }
                    else
                    {
                        string script = @"Swal.fire({title: 'Failure!',text: 'Something went wrong.',icon: 'error',confirmButtonText: 'OK'});";
                        ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", script, true);
                    }
                }
                else
                {
                    if (Convert.ToInt32(Result) >= 3)
                    {
                        string script = @"Swal.fire({title: 'Failure!',text: 'Daily OTP request limit reached.',icon: 'error',confirmButtonText: 'OK'});";
                        ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", script, true);
                    }
                    else
                    {
                        bool Request = OtpData(txtEmail.Text, Otp, CreatedAt.ToString(), ExpiryAt.ToString());
                        if (Request == true)
                        {
                            string script = @"Swal.fire({title: 'Success!',text: 'Registration successful!',icon: 'success',confirmButtonText: 'OK'});";
                            ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", script, true);
                        }
                        else
                        {
                            string script = @"Swal.fire({title: 'Failure!',text: 'Something went wrong.',icon: 'error',confirmButtonText: 'OK'});";
                            ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", script, true);
                        }
                    }
                }
            }
        }
        public bool OtpData(string Email, string Otp, string CreatedAt, string ExpiryAt)
        {
            bool Result = false;
            try
            {
                string StoredProcedure = "Sp_OtpInsert"; string[] ParameterNames = { "@Email", "@Otp", "@Createdat", "@Expiryat" };
                string[] ParameterValues = { Email, Otp, CreatedAt, ExpiryAt };
                bool ResultData = objDl.InsertData(StoredProcedure, ParameterNames, ParameterValues);
                SentOtp(txtEmail.Text, Otp);
                Result = true;
            }
            catch (Exception Ex) { new Exception("Unkown Error.", Ex); Result = false; }
            return Result;
        }
        #endregion
        #region ValidateOtpMethod
        public bool ValidateOTP(string Email, string EnteredOtp)
        {
            string StoredProc = "Sp_OtpConfirm"; string[] ParamNames = { "@Email" }; string[] ParamValues = { Email };
            DataSet Ds = objDl.RetriveData(StoredProc, ParamNames, ParamValues);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                string storedOtp = Ds.Tables[0].Rows[0]["OTP"].ToString();
                DateTime ExpiryAt = Convert.ToDateTime(Ds.Tables[0].Rows[0]["ExpiryAt"]);
                if (storedOtp == EnteredOtp && DateTime.Now <= ExpiryAt)
                {
                    return true;
                }
            }
            return false;
        }
        #endregion
        #region SendOtp
        public bool SentOtp(string Email, string Otp)
        {
            bool Result = false;
            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    StreamReader str = new StreamReader(Server.MapPath(@"~/Email/MailTemp.html"));
                    string MailText = str.ReadToEnd();
                    str.Close();
                    MailText = MailText.Replace("PIN", Otp);
                    MailText = MailText.Replace("UserName", Email);
                    mail.From = new MailAddress("sairamb.netdeveloper@gmail.com");
                    mail.To.Add(Email);
                    MailAddress bcc1 = new MailAddress("sairamb.netdeveloper@gmail.com");
                    mail.Subject = "Validate OTP (One Time Passcode)";
                    mail.Bcc.Add(bcc1);
                    mail.Body = MailText;
                    mail.IsBodyHtml = true;
                    using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                    {
                        smtp.Credentials = new NetworkCredential("sairamb.netdeveloper@gmail.com", "rkiqodcjajtdmulm");
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                        Result = true;
                    }
                }
            }
            catch (Exception Ex)
            {
                new Exception("Something went wrong please try again.", Ex); Result = false;
            }
            return Result;
        }
        #endregion
        #region VerifyOtpButton
        protected void BtnVerify_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtVerifyOtp.Text.Trim()))
            {
                string script = @"Swal.fire({title: 'Failure!',text: 'Please enter Otp.',icon: 'error',confirmButtonText: 'OK'});";
                ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", script, true);
            }
            else if (txtVerifyOtp.Text.Length < 6)
            {
                string script = @"Swal.fire({title: 'Failure!',text: 'Otp must have 6 Digit.',icon: 'error',confirmButtonText: 'OK'});";
                ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", script, true);
            }
            else
            {
                ValidateOTP(txtEmail.Text.Trim(), txtVerifyOtp.Text.Trim());
                txtVerifyOtp.Text = string.Empty; PnlComplete.Visible = chkBox.Visible = true; PnlOtp.Visible = false;
            }
        }
        #endregion
    }
}