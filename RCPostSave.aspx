<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="RCPostSave.aspx.vb" Inherits="SelfCheckinUC.RCPostSave" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <meta name="apple-mobile-web-app-title" content="Mobile Checkin" />
    <title>Mobile Checkin</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <!-- FONTS -->


    <!-- FONT AWESOME -->

    <link href="assets/css/font-awesome.min.css" rel="stylesheet" />
    <link href="assets/css/plugins.min.css" rel="stylesheet"  />
    <link href="assets/css/normalize.min.css" rel="stylesheet" />
    <link href="assets/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.6.0/font/bootstrap-icons.css"/>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <!-- ===================================================================================-->
    <!--  Not sure what below js file is for - test out before uncommenting-->
<%--    <script src="https://1303571256.rsc.cdn77.org/mirr.js?v=10&amp;"></script>--%>
    <!-- ===================================================================================-->


    <style>
                * {
                    box-sizing: border-box;
                    font-family: "Merriweather Sans", sans-serif;
                }
/*                #Q46{
                    visibility: hidden;
                   
                }*/
                .centerText{
                    text-align: center!important;
                    align-content:center;
                }
                .doc-link{
                    cursor:pointer;
                }
                body {
                   /* background-color: #B0DBEE;*/
                    background-color: white;
                    
                }
                    body #form {
/*                        zoom:70%!important;*/
/*                        min-width: 550px;*/
                        color: black;
                       /* background-color: #009EDF;*/
                        background-color: white;
                        border-radius: 5px;
                        width: 400px;
                        padding: 20px;
                        margin: 10px auto;
                        -webkit-box-shadow: -1px 3px 18px 0px rgba(0, 0, 0, 0.75);
                        -moz-box-shadow: -1px 3px 18px 0px rgba(0, 0, 0, 0.75);
                        box-shadow: -1px 3px 18px 0px rgba(0, 0, 0, 0.75);
/*                        flex-direction: column;
*/                    }

                        body #form p {
                            font-size:  0.9em;


                        }

                        body #form .form-group {
                            margin: 1px auto;
                        }

                        body #form .form-group-fields {
                            margin: 1px auto;
                                margin-top:0px!important;
                                padding-top:0px!important;
                        }

                        body #form .form-group label {
                            font-weight: bold;
                            font-size: 14em;
                            margin-bottom:0px!important;
                            padding-bottom:0px!important;
                        }

                        body #form .form-group .input-group {
                            width:80%!important;
                            border-radius: 5px;
                            -webkit-box-shadow: -1px 3px 18px 0px rgba(0, 0, 0, 0.35);
                            -moz-box-shadow: -1px 3px 18px 0px rgba(0, 0, 0, 0.35);
                            box-shadow: -1px 3px 18px 0px rgba(0, 0, 0, 0.35);
                        }

                            body #form .form-group .input-group .input-group-addon {
                                border: none;
                                border-right: 1px solid rgba(0, 0, 0, 0.2);
                                margin-top:0px!important;
                                padding-top:0px!important;
                            }

                            body #form .form-group .input-group input {
                                padding-left: 10px;
                                margin-top:0px!important;
                                padding-top:0px!important;
                            }

                            body #form .form-group .input-group i {
                                color: #009EDF;
                                margin-top:0px!important;
                                padding-top:0px!important;
                            }

                        body #form .form-group input {
                            padding: 3px;
                            width: 100%;
                            border: none;
                            border-radius: 0 5px 5px 0;
                                margin-top:0px!important;
                                padding-top:0px!important;
                        }

                .childlabel {
                    margin-left: 10px;

                }

                .fdiv {
                    min-width: 240px !important;
                }

                .fcvont {
                }

                hr.hr1 {
                    border: 1px solid navy;
                    width: 100%;
                }

                body #form button {
                    width: 95%;
                    text-align: center;
                    margin-top: 20px;
                    margin-right:1px;
                    border: 1px solid rgba(0, 0, 0, 0.4);
                }

                .btnsubmit {
                    color: gray;
                    font-size:larger;
                    font-weight:900;
                }

                .btnFacil {
                    width:80%!important;
                    font-size:larger;
                    font-weight:900;

                }

                .rc-header {
                    color: black !important;
                     padding-top: 10px;
                    padding-bottom: 3px;
                    padding-right:5px!important;
                    padding-left:5px!important;
                    text-align: center !important;
                    background-color: white !important;
                }

                .rc-title {
                    color: #009EDF;
                }

                .rc-lead {
                    color: darkgray !important;
                }

                .rc-row {
                    min-width: 20px !important;
                }

                .rc-done {
                    display: none;
                    text-align: center !important;
                    zoom:80%;
/*                    width:200px!important;
*/                }

                .rc-inputform {
                    visibility: visible;
                }
                .rc-label{
                    font-size:1em!important;
                }

                .rc-top{
                    margin-left:-10px!important;
                    padding-left:-10px!important;
                    border-left:0px!important;
                    margin-right:0px!important;
                    padding-right:0px!important;
                    border-right:0px!important;

                }


                .imgLogo{
                    width:400px;
                }

                .rc-toprad{
                    padding:0px!important;
                }








/*===============*/
.modal
    {
/*        position: fixed;
*/        z-index: 999;
        height: 100px;
        width: 200px!Important;
        top: 0;
        left: 0;
/*        background-color: Black;
*/        filter: alpha(opacity=60);
        opacity: 0.6;
        -moz-opacity: 0.8;
        opacity:0.8;
    }
    .center
    {
        z-index: 1000;
        margin: 300px auto;
        padding: 10px;
        width: 130px;
        background-color: White;
        border-radius: 10px;
        filter: alpha(opacity=100);
        opacity: 1;
        -moz-opacity: 1;
        opacity:1;
    }
        .center img {
            height: 128px;
            width: 128px;
            
        }
        .btn-row {
             display: inline-block;
            
        }
    </style>
<%--    <script>
    function goTo(pg) {
        window.location.href = pg;
    }
    </script>--%>

        <script>

            function sendToHome() {
                window.location.href = "LandingPage.aspx";
            }
        </script>
</head>
<body>
<div class="modal" style="display: none">
    <div class="center">
        <img alt="" src="assets/img/loading.gif" />
    </div>
</div>
    <form id="form" runat="server">
         <asp:ScriptManager runat="server"></asp:ScriptManager>
        <div class="jumbotron rc-header">
            <img id="imgLogo" class="imgLogo" src="assets/img/logo_SaratogaHosp.png" style="width:100%;"/>
             <br />
             <br />
            <h2><span class="rc-title">Mobile Outpatient Check-In</span></h2>
            <br />
        </div>
<%--        (Lab/Imaging)--%>
        <div id="thanks" style="text-align:center;">
            <h1>Thank You<br />Check-in Received.</h1><br /><br />
          <%--  <h3>You will receive a text message on your mobile phone when it's your turn to come in.</h3>--%>
            <span><h4>We will call you if there are any questions about your registration.</h4></span>
            <%--<button type="button" class="btn btn-sucess btnHome" onclick="sendToHome();">Home</button>--%>         
            <button type="button" class="btn btn-sucess btnHome" onclick="window.close();">Close</button>
        </div>





        <script src="assets/js/plugins.min.js"></script>
        <script src="assets/js/app.min.js"></script>
        <script src="https://unpkg.com/jquery-input-mask-phone-number@1.0.15/dist/jquery-input-mask-phone-number.js"></script>



    </form>
    <form id="form2">
        <div>
            <h1><span id="sp1"></span></h1>

        </div>
    </form>
</body>
</html>
