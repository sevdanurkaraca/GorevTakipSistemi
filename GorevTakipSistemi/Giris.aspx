<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Giris.aspx.cs" Inherits="GorevTakipSistemi.Giris" %>

<!DOCTYPE html>

<html lang="tr-TR">
<!-- Mirrored from iamsrinu.com/bluemoon2.1/login.html by HTTrack Website Copier/3.x [XR&CO'2014], Tue, 31 Oct 2017 13:46:41 GMT -->
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width,initial-scale=1">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="description" content="Bluemoon Admin">
    <meta name="keywords" content="Admin, Dashboard, Bootstrap3, Sass, transform, CSS3, HTML5, Web design, UI Design, Responsive Dashboard, Responsive Admin, Admin Theme, Best Admin UI, Bootstrap Theme, Wrapbootstrap, Bootstrap, C3 Graphs, D3 Graphs, NVD3 Graphs, Admin Skin, Black Admin Dashboard, Grey Admin Dashboard, Dark Admin Dashboard, Simple Admin Dashboard, Simple Admin Theme, Simple Bootstrap Dashboard, Invoice, Tasks, Profile">
    <meta name="author" content="Srinu Basava">
    <link rel="shortcut icon" href="img/Protak7.png">
    <title>ProTak Giriş</title>
    <link href="css/bootstrap.min.css" rel="stylesheet" media="screen">
    <link href="css/main.css" rel="stylesheet">
    <link href="fonts/icomoon/icomoon.css" rel="stylesheet">
    <link href="css/alertify/core.css" rel="stylesheet" />
    <link href="css/alertify/default.css" rel="stylesheet" /> 
    <script src="js/alertify/alertify.js"></script>
</head>
<body class="login-bg">
    <form runat="server">
        <div class="login-wrapper">
            <div class="login">
                <div class="login-header">
                    <div class="logo">
                        <img src="img/Protak7.png" alt="Bluemoon Admin Dashboard Logo"></div>
                    <h5>ProTak Giriş </h5>
                </div>
                <div class="login-body">
                    <div class="form-group">
                        <label for="emailID">Email</label><input id="txtEmail" runat="server" type="text" class="form-control" placeholder="Email"></div>
                    <div class="form-group">
                        <label for="password">Parola</label><input id="txtParola" runat="server" type="password" class="form-control" placeholder="Parola"></div>
                    <button class="btn btn-danger btn-block" id="btnGiris" runat="server" onserverclick="btnGiris_ServerClick" type="submit">Giriş Yap</button></div>
                <%--<div class="checkbox no-margin">
                    <input type="checkbox" id="remember" checked="checked"><label for="remember">Beni Hatırla</label></div>--%>
            </div>
            <%--<p>Yeni Üyelik İçin  <a href="signup.html">Kayıt Ol</a></p>--%>
        </div>
    </form>

    <script src="js/jquery.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <script src="js/custom.js"></script>
</body>
</html>
