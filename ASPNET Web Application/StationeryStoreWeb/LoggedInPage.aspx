<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoggedInPage.aspx.cs" Inherits="ADProjectSA43_Team1.LoggedInPage" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <%-- Title Bar Image --%>
    <link rel="shortcut icon" href="image/warehouse.png"/>
    <title>Stationery Store</title>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <%--Style--%>
    <link rel="stylesheet" href="Style/menubar.css" />
    <%--Jquery--%>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>
    <%--Bootstrap--%>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" />
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <%--Bootstrap Date-Picker Plugin--%>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/js/bootstrap-datepicker.min.js"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/css/bootstrap-datepicker3.css" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/moment.js/2.12.0/moment.js"></script>
    <%--FontAwesome--%>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" />
</head>
<body>
<form id="form1" runat="server">
<div class="container-fluid">
    <div class="row">
        <div class="col-md-3"></div>
        <div class="col-md-6 pg-content" style="margin-top: 60px;">
            <h2><i class="fa fa-sign-in" aria-hidden="true"></i> Login</h2> 
				
            <div class="row" style="margin-bottom:10px;">
					<div class="col-md-12">
						<div class="form-group">
							<label class="control-label col-md-3" for="id_username">User Name:</label>
							<div class="col-md-9">
                                <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control" placeholder="Type UserName"></asp:TextBox>
							</div>
						</div>
					</div>
				</div>
            <div class="row" style="margin-bottom:10px;">
                <div class="col-md-12">
						<div class="form-group">
							<label class="control-label col-md-3" for="id_password">Password:</label>
							<div class="col-md-9">
                                <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" placeholder="Type Password" TextMode="Password"></asp:TextBox>
							</div>
						</div>
					</div>
            </div>
            
            <div class="row" style="margin-bottom:10px;">
                <div class="col-md-12" style="height: 36px">
                    <asp:Button ID="btnLogin" runat="server" Text="LOGIN" CssClass="btn btn-primary btn-block" OnClick="btnLogin_Click" />
                </div>
            </div>

            <div class="row" style="margin-bottom:10px;">
                <div class="col-md-12">
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" />
                </div>
            </div>

            <asp:RequiredFieldValidator ID="UserNameValidator" runat="server" ErrorMessage="Please enter Username" ControlToValidate="txtUserName" Display="None"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="PasswordValidator" runat="server" ErrorMessage="Please enter Password" ControlToValidate="txtPassword" Display="None"></asp:RequiredFieldValidator>
        </div>
        <div class="col-md-3"></div>
    </div>
</div>
</form>
</body>
</html>
