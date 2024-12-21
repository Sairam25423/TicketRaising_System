<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="TicketRaisingSystem.Ticket.Registration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Register</title>
    <link rel="stylesheet" href="https://unpkg.com/bootstrap@5.3.3/dist/css/bootstrap.min.css" />
    <link rel="stylesheet" href="https://unpkg.com/bs-brain@2.0.4/components/registrations/registration-5/assets/css/registration-5.css" />
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" />
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <!--Sweet Alerts Links-->
    <link href="https://cdn.jsdelivr.net/npm/sweetalert2@11.6.0/dist/sweetalert2.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11.6.0/dist/sweetalert2.all.min.js"></script>
    <!--Calender styling-->
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.min.js"></script>
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
    <script>
        $(document).ready(function () {
            // Apply the date picker to the TextBox
            $('#<%= txtDob.ClientID %>').datepicker({
                changeYear: true,          // Enables year dropdown
                showButtonPanel: true,     // Shows button panel to quickly close or reset
                dateFormat: 'yy-mm-dd',    // Set the format to 'yyyy-mm-dd'
                yearRange: '1900:2100'     // Set the range of years (1900 to 2100)
            });
        });
        document.addEventListener("DOMContentLoaded", function () {
            debugger;
            // Ensure elements are accessible
            const btnSendOtp = document.getElementById('btnSendOtp');
            const BtnVerify = document.getElementById('BtnVerify');
            const btnSignup = document.getElementById('btnSignup');
            // Check if the elements exist before attaching event listeners
            if (btnSendOtp) {
                btnSendOtp.addEventListener('click', function (event) {
                    validateForm(['txtName', 'txtMobile', 'txtEmail'], event);
                });
            }
            if (BtnVerify) {
                BtnVerify.addEventListener('click', function (event) {
                    validateForm(['txtName', 'txtMobile', 'txtEmail', 'txtVerifyOtp'], event);
                });
            }
            if (btnSignup) {
                btnSignup.addEventListener('click', function (event) {
                    validateForm(['txtName', 'txtMobile', 'txtEmail', 'txtDob', 'txtPassword', 'txtConfirmPassword'], event);
                });
            }
            // Validation function to handle common validation logic
            function validateForm(fields, event) {
                let IsValid = true;
                // Clear all previous error messages
                fields.forEach(field => {
                    document.getElementById(field + 'Error').innerHTML = '';
                });
                // Name Validation
                if (fields.includes('txtName')) {
                    var nameRegex = /^[A-Za-z\s]{3,}$/;
                    var Name = document.getElementById('txtName').value;
                    if (!nameRegex.test(Name)) {
                        document.getElementById('txtNameError').innerHTML = "Please enter a valid name (min 3 characters, only alphabets and spaces allowed).";
                        IsValid = false;
                    }
                }
                // Mobile Validation
                if (fields.includes('txtMobile')) {
                    var mobileRegex = /^[6-9]\d{9}$/;
                    var Mobile = document.getElementById('txtMobile').value;
                    if (!mobileRegex.test(Mobile)) {
                        document.getElementById('txtMobileError').innerHTML = "Please enter a valid Indian mobile number (10 digits, starting with 6, 7, 8, or 9).";
                        IsValid = false;
                    }
                }
                // Email Validation
                if (fields.includes('txtEmail')) {
                    var emailRegex = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,6}$/;
                    var Email = document.getElementById('txtEmail').value;
                    if (!emailRegex.test(Email)) {
                        document.getElementById('txtEmailError').innerHTML = "Please enter a valid email address.";
                        IsValid = false;
                    }
                }
                // OTP Validation (For BtnVerify)
                if (fields.includes('txtVerifyOtp')) {
                    var Otp = document.getElementById('txtVerifyOtp').value;
                    var OtpRegex = /^[0-9]+$/;
                    if (!OtpRegex.test(Otp)) {
                        document.getElementById('txtVerifyOtpError').innerHTML = "Please enter numbers only";
                        IsValid = false;
                    }
                }
                // Date of Birth Validation (For btnSignup)
                if (fields.includes('txtDob')) {
                    var Dob = document.getElementById('txtDob').value;
                    var DobError = document.getElementById('txtDobError');
                    var minDate = new Date(1947, 0, 1);
                    var dobDate = new Date(Dob);
                    var today = new Date();
                    var age = today.getFullYear() - dobDate.getFullYear();

                    if (!Dob || dobDate == 'Invalid Date' || dobDate > today || age < 18 || dobDate < minDate) {
                        DobError.innerHTML = "You must be at least 18 years old and the date cannot be in the future or before 1st Jan 1947.";
                        IsValid = false;
                    }
                }
                // Password Validation (For btnSignup)
                if (fields.includes('txtPassword')) {
                    var Password = document.getElementById('txtPassword').value;
                    var passwordRegex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*])[A-Za-z\d!@#$%^&*]{8,}$/;
                    if (!passwordRegex.test(Password)) {
                        document.getElementById('txtPasswordError').innerHTML = "Password must be at least 8 characters long and contain a mix of uppercase, lowercase, number, and special character.";
                        IsValid = false;
                    }
                }
                // Confirm Password Validation (For btnSignup)
                if (fields.includes('txtConfirmPassword')) {
                    var ConfirmPassword = document.getElementById('txtConfirmPassword').value;
                    if (ConfirmPassword === '') {
                        document.getElementById('txtConfirmPasswordError').innerHTML = "Please enter confirm password.";
                        IsValid = false;
                    } else if (document.getElementById('txtPassword').value !== ConfirmPassword) {
                        document.getElementById('txtConfirmPasswordError').innerHTML = "Passwords do not match.";
                        IsValid = false;
                    }
                }
                // Prevent form submission if not valid
                if (!IsValid) {
                    event.preventDefault();
                }
                return IsValid;
            }
        });
    </script>
</head>
<body class="bg-gradient-primary">
    <form id="form1" runat="server">
        <!-- Registration 5 - Bootstrap Brain Component -->
        <section class="p-3 p-md-4 p-xl-5">
            <div class="container">
                <div class="card border-light-subtle shadow-sm">
                    <div class="row g-0">
                        <div class="col-12 col-md-6 text-bg-primary">
                            <div class="d-flex align-items-center justify-content-center h-50">
                                <div class="col-10 col-xl-9 py-3">
                                    <img class="img-fluid rounded" loading="lazy" src="../img/cloud-support-ticket-system.png" alt="BootstrapBrain Logo" />
                                </div>
                            </div>
                            <div class="d-flex align-items-center justify-content-center h-50">
                                <div class="col-10 col-xl-9 py-3">
                                    <h6 class="h3">Have a problem? We’ve got the solution! Register to submit your ticket, and we’ll help you resolve it in no time.</h6>
                                    <p class="lead m-0">Welcome to our support portal. Please register and provide the necessary details to raise a support ticket. Our team is here to assist you!</p>
                                </div>
                            </div>
                        </div>
                        <div class="col-12 col-md-6">
                            <div class="card-body p-3 p-md-4 p-xl-5">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="mb-5">
                                            <h2 class="h3">Registration</h2>
                                            <h3 class="fs-6 fw-normal text-secondary m-0">Enter your details to register</h3>
                                        </div>
                                    </div>
                                </div>
                                <div action="#!">
                                    <div class="row gy-3 gy-md-4 overflow-hidden">
                                        <div class="col-12">
                                            <label for="txtName" class="form-label">Name <span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtName" runat="server" CssClass="form-control" placeholder="Name" MaxLength="100" AutoCompleteType="Disabled" autocomplete="off"></asp:TextBox>
                                            <div id="txtNameError" style="color: red"></div>
                                        </div>
                                        <div class="col-12">
                                            <label for="txtMobile" class="form-label">Mobile Number <span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control" placeholder="Mobile Number" MaxLength="10" AutoCompleteType="Disabled" autocomplete="off"></asp:TextBox>
                                            <div id="txtMobileError" style="color: red"></div>
                                        </div>
                                        <div class="col-12">
                                            <label for="txtEmail" class="form-label">Email <span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Email" AutoCompleteType="Disabled" autocomplete="off"></asp:TextBox>
                                            <div id="txtEmailError" style="color: red"></div>
                                        </div>
                                        <asp:Panel ID="PnlOtp" runat="server">
                                            <div class="col-12">
                                                <asp:Button ID="btnSendOtp" runat="server" Text="Send Otp" CssClass="btn btn-success" OnClick="btnSendOtp_Click" />
                                            </div>
                                            <div class="col-12">
                                                <label for="txtVerifyOtp" class="form-label">Verify Otp <span class="text-danger">*</span></label>
                                                <asp:TextBox ID="txtVerifyOtp" runat="server" CssClass="form-control" placeholder="Otp" MaxLength="6" AutoCompleteType="Disabled" autocomplete="off"></asp:TextBox>
                                                <div id="txtVerifyOtpError" style="color: red"></div>
                                            </div>
                                            <div class="col-12 mt-1">
                                                <asp:Button ID="BtnVerify" runat="server" Text="Verify Otp" CssClass="btn btn-outline-success" OnClick="BtnVerify_Click" />
                                            </div>
                                        </asp:Panel>
                                        <asp:Panel ID="PnlComplete" runat="server" Visible="false">
                                            <div class="col-12">
                                                <label for="txtDob" class="form-label">Date of Birth <span class="text-danger">*</span></label>
                                                <asp:TextBox ID="txtDob" runat="server" CssClass="form-control" AutoCompleteType="Disabled" autocomplete="off"></asp:TextBox>
                                                <div id="txtDobError" style="color: red"></div>
                                            </div>
                                            <div class="col-12">
                                                <label for="password" class="form-label">Password <span class="text-danger">*</span></label>
                                                <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" MaxLength="10"></asp:TextBox>
                                                <div id="txtPasswordError" style="color: red"></div>
                                            </div>
                                            <div class="col-12">
                                                <label for="password" class="form-label">Confirm Password <span class="text-danger">*</span></label>
                                                <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="form-control" TextMode="Password" MaxLength="10"></asp:TextBox>
                                                <div id="txtConfirmPasswordError" style="color: red"></div>
                                            </div>
                                            <div class="col-12">
                                                <div class="form-check">
                                                    <asp:CheckBox ID="chkAgree" runat="server" CssClass="form-check-input" />
                                                    <label class="form-check-label text-secondary" for="chkAgree">
                                                        I agree to the <a href="../Email/Terms&Conditions.html" class="link-primary text-decoration-none">terms and conditions</a>
                                                    </label>
                                                </div>
                                            </div>
                                            <div class="col-12">
                                                <div class="d-grid">
                                                    <asp:Button ID="btnSignup" runat="server" Text="Sign up" CssClass="btn bsb-btn-xl btn-primary" OnClick="btnSignup_Click" />
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                </div>
                                <div class="row" id="chkBox" runat="server" visible="false">
                                    <div class="col-12">
                                        <hr class="mt-5 mb-4 border-secondary-subtle" />
                                        <p class="m-0 text-secondary text-center">Already have an account? <a href="#!" class="link-primary text-decoration-none">Sign in</a></p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </form>
</body>
</html>
