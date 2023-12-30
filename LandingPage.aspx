<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="LandingPage.aspx.vb" Inherits="SelfCheckinUC.LandingPage" %>


<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" />
    <title>PatientPathPnline</title>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.3.1/dist/css/bootstrap.min.css" integrity="sha384-ggOyR0iXCbMQv3Xipma34MD+dH/1fQ784/j6cY/iJTQUOhcWr7x9JvoRxT2MZw1T" crossorigin="anonymous">
    <style>
    img {
        display:block;
        margin-left:auto;
        margin-right:auto;
    }
    h1 {
        text-align:center;
    }
    a {
        text-align: center;
        color: white;
        text-decoration: none;
    }
    .lead {
        text-align: center!important;
    }
    .contact {
        text-align: center;
    }
    .pps-button {
/*        background-color: rgb(0,120,209);
        color: white;*/
	outline: 0; 
/*	background:#f34727; 
*/     background: rgb(109, 57, 108);
	color: #fff; 
        padding-top: 15px;
        padding-bottom: 15px;
        padding-left: 18px;
        padding-right: 18px;
        margin: auto;
/*        border: 0;*/
        border-radius: 25px;
        text-align: center;
        border-color: rgb(109, 57, 108);
/*        border-color: rgb(0,120,209);

        border-width: 1px;
*/        border-style: solid;
        transition: background-color .5s, color .5s, border-color .5s, border-style .5s;
    }
        .pps-button:hover {
/*            font-size: 17px;*/
            color: rgb(0,120,209)!important;
            background: rgb(109, 57, 108);
            border-width:1px;
            border-style:solid;
        }
        .button-div {
            margin: auto;
            text-align: center;
        }
</style>
        <script>
            function sendToHome() {
                //alert('yo');
                window.location.href = "https://patientpathonline.com/PtPthDev/RCUC.aspx";
            }
        </script>
</head>
<body>
    <form id="form1" runat="server">
    <div >
        <img id="imgLogo" class="imgLogo" src="assets/img/logo_SaratogaHosp.png" />
        <br /><br />
        <h1>Urgent Care Patient Self Check-In</h1>

        <br /><br />
        <div class="button-div">
           <%-- <button class="pps-button" onclick="sendToHome();"><a rel="link" href="RCUC.aspx">Click to Continue</a></button>--%>
<%--             <button class="pps-button" onclick="sendToHome();">Click to Continue</button>--%>
            <asp:Button ID="Button1" class="pps-button" runat="server" Text="Click to Continue" />
        </div>/
        
      
        
    </div>
<br /><br />
    <div class="row">
        <div class="col-md-4">
        </div>
        <div class="col-md-4">

        </div>
    </div>

    </form>

</body>
</html>
