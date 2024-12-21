<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginForm.aspx.cs" Inherits="TicketRaisingSystem.Ticket.LoginForm" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login Forms</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <link href="https://fonts.googleapis.com/css?family=Lato:300,400,700&display=swap" rel="stylesheet" />
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" />
    <link rel="stylesheet" href="/LogFiles/css/style.css" />
    <!--Sweet Alerts Links-->
    <link href="https://cdn.jsdelivr.net/npm/sweetalert2@11.6.0/dist/sweetalert2.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11.6.0/dist/sweetalert2.all.min.js"></script>
    <style>
        .wrap {
            border-radius: 40px !important;
            background: rgba(255, 255, 255, 0.1); /* Semi-transparent white background */
            backdrop-filter: blur(10px); /* Apply blur effect behind the element */
            -webkit-backdrop-filter: blur(20px); /* Safari support */
            border: 1px solid rgba(255, 255, 255, 0.3); /* Light border for subtle definition */
            padding: 20px; /* Optional: Add some padding */
        }

        .Back {
            /*background: rgb(255,187,187);
            background: linear-gradient(56deg, rgba(255,187,187,0.9752275910364145) 7%, rgba(169,241,223,1) 53%);*/
            /*background: rgb(191,240,152);
            background: linear-gradient(63deg, rgba(191,240,152,1) 14%, rgba(111,214,255,1) 90%);*/
            background: rgb(216,181,255);
            background: linear-gradient(63deg, rgba(216,181,255,1) 14%, rgba(30,174,152,1) 90%);
        }

        .ImgCls {
            border-radius: 20px !important;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <section class="ftco-section Back">
            <div class="container">
                <div class="row justify-content-center">
                    <div class="col-md-6 text-center mb-5">
                        <h2 class="heading-section">Login Form</h2>
                    </div>
                </div>
                <div class="row justify-content-center">
                    <div class="col-md-12 col-lg-10">
                        <div class="wrap d-md-flex" style="border-radius: 40px!important">
                            <div class="img ImgCls" style="background-image: url(/LogFiles/images/SRM.jpg);">
                            </div>
                            <div class="login-wrap p-4 p-md-5">
                                <div class="d-flex">
                                    <div class="w-100">
                                        <h3 class="mb-4">Sign In</h3>
                                    </div>
                                    <%--<div class="w-100">
                                        <p class="social-media d-flex justify-content-end">
                                            <a href="#" class="social-icon d-flex align-items-center justify-content-center"><span class="fa fa-facebook"></span></a>
                                            <a href="#" class="social-icon d-flex align-items-center justify-content-center"><span class="fa fa-twitter"></span></a>
                                        </p>
                                    </div>--%>
                                </div>
                                <div action="#" class="signin-form">
                                    <div class="form-group mb-3">
                                        <label class="label" for="name">Username</label>
                                        <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" placeholder="Username" AutoCompleteType="Disabled" autocomplete="off"></asp:TextBox>
                                    </div>
                                    <div class="form-group mb-3">
                                        <label class="label" for="password">Password</label>
                                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" placeholder="Password" AutoCompleteType="Disabled" autocomplete="off"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <asp:Button ID="btnSubmit" runat="server" Text="Sign In" CssClass="form-control btn btn-primary rounded submit px-3" OnClick="btnSubmit_Click" />
                                    </div>
                                    <div class="form-group d-md-flex">
                                        <div class="w-50 text-left">
                                            <label class="checkbox-wrap checkbox-primary mb-0">
                                                Remember Me
									  <asp:CheckBox ID="CheckBox1" runat="server" />
                                                <span class="checkmark"></span>
                                            </label>
                                        </div>
                                        <div class="w-50 text-md-right">
                                            <a href="#">Forgot Password</a>
                                        </div>
                                    </div>
                                </div>
                                <p class="text-center">Not a member? <a data-toggle="tab" href="Registration.aspx">Sign Up</a></p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <script src="/LogFiles/js/jquery.min.js"></script>
        <script src="/LogFiles/js/popper.js"></script>
        <script src="/LogFiles/js/bootstrap.min.js"></script>
        <script src="/LogFiles/js/main.js"></script>
    </form>
</body>
</html>
