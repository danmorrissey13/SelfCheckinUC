<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="RCUC.aspx.vb" Inherits="SelfCheckinUC.RCUC" %>

<!DOCTYPE html>
<meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

     <script type = "text/javascript" >  
         function preventBack() { window.history.forward(); }
         setTimeout("preventBack()", 0);
         window.onunload = function () { null };
     </script>    

    <script src="Scripts/jquery-3.4.1.js"></script>
    <script src="Scripts/modernizr-2.8.3.js"></script>
    <script src="Scripts/bootstrap.js"></script>
    <link href="Content/font-awesome.min.css" rel="stylesheet" />
    <link href="Content/bootstrap.css" rel="stylesheet" />
     <link href="assets/css/RCUC.css" rel="stylesheet" />
    
    <style>


    </style>
</head>
    <body>
            <div class="modal" id="modal">
            <div class="modal-content">
                <span class="closeBtn" id="closeBtn">&times;</span>
            <h2>Consent Required</h2>
                <h3>Please proceed inside to see a registrar..</h3>
<%--                <p> Please select another time slot.</p>--%>
            </div>
</div>

<section class="form-box">
            <div class="container" style="width:100%;">
                
                <div class="row">
                    <div class="form-wizard" style="width:100%;">

					
						<!-- Form Wizard -->
                    	<form role="form" id="form1" runat="server" style="width:100%;margin-right:50px;">
                             <div style="margin:auto;padding-top:20px;width:70%;background-color:white;display:flex;align-items:center;justify-content:center; text-align: center;">
                             <img id="imgFacilLogo" runat="server" src="assets/img/AH_logo.png" class="responsive-img"/>
                            </div>
                           <br /><br />
                            <asp:Label ID="lblFacilPhone" runat="server" Text=""></asp:Label><br /><br />
                    		<h3>Mobile Urgent Care Check-In</h3> 
                            <br />
     						
							<!-- Form progress -->
                    		<div class="form-wizard-steps form-wizard-tolal-steps-4">
                    			<div class="form-wizard-progress">
                    			    <div class="form-wizard-progress-line" data-now-value="12.25" data-number-of-steps="4" style="width: 12.25%;"></div>
                    			</div>
								<!-- Step 1 -->
                    			<div class="form-wizard-step active">
                    				<div class="form-wizard-step-icon"><i class="fa fa-user" aria-hidden="true"></i></div>
                    				<p>Personal</p>
                    			</div>
								<!-- Step 1 -->Y
								
								<!-- Step 2 -->
                    			<div class="form-wizard-step">
                    				<div class="form-wizard-step-icon"><i class="fa fa-phone" aria-hidden="true"></i></div>
                    				<p>Contact</p>
                    			</div>
								<!-- Step 2 -->
								
								<!-- Step 3 -->
								<div class="form-wizard-step">
                    				<div class="form-wizard-step-icon"><i class="fa fa-medkit" aria-hidden="true"></i></div>
                    				<p>Insurance</p>
                    			</div>
								<!-- Step 3 -->
								
								<!-- Step 4 -->
								<div class="form-wizard-step">
                    				<div class="form-wizard-step-icon"><i class="fa fa-question" aria-hidden="true"></i></div>
                    				<p>Verification</p>
                    			</div>
								<!-- Step 4 -->
                    		</div>
							<!-- Form progress -->
                    		
							
							<!-- Form Step 1 -->
                    		<fieldset>

                    		    <h4>Personal Information: <span>Step 1 of 4</span></h4>
<br /><br />
<%--////////////////////////////////////////////////////////////////////////////////////////////////////////////--%>

                    <div class="container-fluid">
                        <div class="row form-inline">
	                        <div class="form-group col-md-8 col-xs-8" style="padding:0px;">
                                <label id="lblConsTreat"  runat="server"  onclick="openDoc('consent.pdf')" class="doc-link lnk-label">Do you consent to treatment?</label>
	                        </div>
                            <div class="form-group col-md-3 col-xs-3" style="padding:0px;">
	                            <label class="radio-inline">
	                              <input type="radio" id="radConsTreatY" name="radConsTreat" runat="server" value="Yes" class="radQ  required"/>Yes
	                            </label>
	                            <label class="radio-inline" style="margin:0px;">
	                              <input type="radio" id="radConsTreatN" name="radConsTreat" runat="server" value="No" class="radQ required" onclick="openModal();"/>No
	                            </label>
                            </div>
                        </div>
                    </div>


<%--////////////////////////////////////////////////////////////////////////////////////////////////////////////--%>
                    <div class="container-fluid">
                        <div class="row form-inline">
                            <div class="form-group col-md-8 col-xs-8" style="padding:0px;">
	                            <label id="lblConsFin"  runat="server"  onclick="openDoc('fin.pdf')" class="doc-link lnk-label">Do you agree with the Financial Agreement?</label>
                            </div>
                        <div class="form-group col-md-3 col-xs-3" style="padding:0px;">
     	                        <label class="radio-inline ">
	  	                        <input type="radio" id="radConsFinY" name="radConsFin" value="Yes" runat="server" class="required"/>Yes
	                        </label>
	                        <label class="radio-inline" style="margin:0px;">
	  	                        <input type="radio" id="radConsFinN" name="radConsFin" value="No" runat="server" class="required" onclick="openModal();"/>No
	                        </label>
                        </div>
                        </div>
                    </div>
<%--/////////////////////////////////////////////////////////////////////////////////////////////////////////////////--%>
<%--                                <div class="form-group">
                                    <input type="text" id="consent-agree" class="form-control consent-agree required" text="Consent to treatment and Financial Agreement are required."/>
                                </div>--%>

                             
								<div class="form-group">
                    			    <label class="lbl-ctl">First Name: <span>*</span></label><br />
                                    <input type="text" id="txtFName" runat="server" name="First Name" MaxLength="25" placeholder="First Name" class="form-control target required"/>
                                </div>
                                <div class="form-group">
                    			    <label class="lbl-ctl">Last Name: <span>*</span></label><br />
                                    <input type="text" id="txtLName" runat="server" name="Last Name" MaxLength="25" placeholder="Last Name" class="form-control target required"/>
                                </div>

				<div class="container-fluid">
<%--                                <div class="row form-inline">--%>
                                <div class="row">

                                     <label class="lbl-ctl">Date Of Birth: </label>
                                </div>
								<div class="row form-inline">
								    <div class="form-group col-md-4 col-xs-4" style="padding-left:0px;padding-right:0px;">
									    <label>Month: </label>
    <%--                                    <select id="ddlMonth" runat="server" class="form-control" style="margin-left:5px;margin-right:5px;">--%>
                                        <select id="ddlMonth" runat="server" class="form-control target">

									      <option>Jan</option>
									      <option>Feb</option>
									      <option>Mar</option>
									      <option>Apr</option>
									      <option>May</option>
									      <option>Jun</option>
									      <option>Jul</option>
									      <option>Aug</option>
									      <option>Sep</option>
									      <option>Oct</option>
									      <option>Nov</option>
									      <option>Dec</option>
									    </select>
								    </div>

								    <div class="form-group col-md-4 col-xs-4" style="padding-left:0px;padding-right:0px;">
									    <label>Day: </label>
    <%--                                    <select id="ddlDay" runat="server" class="form-control" style="margin-left:5px;margin-right:5px;">--%>
                                        <select id="ddlDay" runat="server" class="form-control target">

									          <option>01</option>
									          <option>02</option>
									          <option>03</option>
									          <option>04</option>
									          <option>05</option>
									          <option>01</option>
									          <option>02</option>
									          <option>03</option>
									          <option>04</option>
									          <option>05</option>
									          <option>06</option>
									          <option>07</option>
									          <option>08</option>
									          <option>09</option>
									          <option>10</option>
									          <option>11</option>
									          <option>12</option>
									          <option>13</option>
									          <option>14</option>
									          <option>15</option>
									          <option>16</option>
									          <option>17</option>
									          <option>18</option>
									          <option>19</option>
									          <option>20</option>
									          <option>21</option>
									          <option>22</option>
									          <option>23</option>
									          <option>24</option>
									          <option>25</option>
									          <option>26</option>
									          <option>27</option>
									          <option>28</option>
									          <option>29</option>
									          <option>30</option>
									          <option>31</option>

									    </select>
								    </div>

								    <div class="form-group col-md-4 col-xs-4" style="padding-left:0px;padding-right:0px;">
									    <label>Year: </label>
    <%--                                    <select name="ddlYear" id="ddlYear" runat="server" class="form-control ddlYear" style="margin-right:5px;margin-left:5px;">--%>
                                        <select name="ddlYear" id="ddlYear" runat="server" class="form-control ddlYear target">
<%--                                        <option value="2022">2022</option>--%>
                                        <option value="2021">2023</option>
                                        <option value="2021">2022</option>
                                        <option value="2021">2021</option>
                                        <option value="2020">2020</option>
                                        <option value="2019">2019</option>
                                        <option value="2018">2018</option>
                                        <option value="2017">2017</option>
                                        <option value="2016">2016</option>
                                        <option value="2015">2015</option>
                                        <option value="2014">2014</option>
                                        <option value="2013">2013</option>
                                        <option value="2012">2012</option>
                                        <option value="2011">2011</option>
                                        <option value="2010">2010</option>
                                        <option value="2009">2009</option>
                                        <option value="2008">2008</option>
                                        <option value="2007">2007</option>
                                        <option value="2006">2006</option>
                                        <option value="2005">2005</option>
                                        <option value="2004">2004</option>
                                        <option value="2003">2003</option>
                                        <option value="2002">2002</option>
                                        <option value="2001">2001</option>
                                        <option value="2000">2000</option>
                                        <option value="1999">1999</option>
                                        <option value="1998">1998</option>
                                        <option value="1997">1997</option>
                                        <option value="1996">1996</option>
                                        <option value="1995">1995</option>
                                        <option value="1994">1994</option>
                                        <option value="1993">1993</option>
                                        <option value="1992">1992</option>
                                        <option value="1991">1991</option>
                                        <option value="1990">1990</option>
                                        <option value="1989">1989</option>
                                        <option value="1988">1988</option>
                                        <option value="1987">1987</option>
                                        <option value="1986">1986</option>
                                        <option value="1985">1985</option>
                                        <option value="1984">1984</option>
                                        <option value="1983">1983</option>
                                        <option value="1982">1982</option>
                                        <option value="1981">1981</option>
                                        <option value="1980">1980</option>
                                        <option value="1979">1979</option>
                                        <option value="1978">1978</option>
                                        <option value="1977">1977</option>
                                        <option value="1976">1976</option>
                                        <option value="1975">1975</option>
                                        <option value="1974">1974</option>
                                        <option value="1973">1973</option>
                                        <option value="1972">1972</option>
                                        <option value="1971">1971</option>
                                        <option value="1970">1970</option>
                                        <option value="1969">1969</option>
                                        <option value="1968">1968</option>
                                        <option value="1967">1967</option>
                                        <option value="1966">1966</option>
                                        <option value="1965">1965</option>
                                        <option value="1964">1964</option>
                                        <option value="1963">1963</option>
                                        <option value="1962">1962</option>
                                        <option value="1961">1961</option>
                                        <option value="1960">1960</option>
                                        <option value="1959">1959</option>
                                        <option value="1958">1958</option>
                                        <option value="1957">1957</option>
                                        <option value="1956">1956</option>
                                        <option value="1955">1955</option>
                                        <option value="1954">1954</option>
                                        <option value="1953">1953</option>
                                        <option value="1952">1952</option>
                                        <option value="1951">1951</option>
                                        <option value="1950">1950</option>
                                        <option value="1949">1949</option>
                                        <option value="1948">1948</option>
                                        <option value="1947">1947</option>
                                        <option value="1946">1946</option>
                                        <option value="1945">1945</option>
                                        <option value="1944">1944</option>
                                        <option value="1943">1943</option>
                                        <option value="1942">1942</option>
                                        <option value="1941">1941</option>
                                        <option value="1940">1940</option>
                                        <option value="1939">1939</option>
                                        <option value="1938">1938</option>
                                        <option value="1937">1937</option>
                                        <option value="1936">1936</option>
                                        <option value="1935">1935</option>
                                        <option value="1934">1934</option>
                                        <option value="1933">1933</option>
                                        <option value="1932">1932</option>
                                        <option value="1931">1931</option>
                                        <option value="1930">1930</option>
                                        <option value="1929">1929</option>
                                        <option value="1928">1928</option>
                                        <option value="1927">1927</option>
                                        <option value="1926">1926</option>
                                        <option value="1925">1925</option>
                                        <option value="1924">1924</option>
                                        <option value="1923">1923</option>
                                        <option value="1922">1922</option>
                                        <option value="1921">1921</option>
                                        <option value="1920">1920</option>
                                        <option value="1919">1919</option>
                                        <option value="1918">1918</option>
                                        <option value="1917">1917</option>
                                        <option value="1916">1916</option>
                                        <option value="1915">1915</option>
                                        <option value="1914">1914</option>
                                        <option value="1913">1913</option>
                                        <option value="1912">1912</option>
                                        <option value="1911">1911</option>
                                        <option value="1910">1910</option>
                                        <option value="1909">1909</option>
                                        <option value="1908">1908</option>
                                        <option value="1907">1907</option>
                                        <option value="1906">1906</option>
                                        <option value="1905">1905</option>
                                        <option value="1904">1904</option>
                                        <option value="1903">1903</option>
                                        <option value="1902">1902</option>
                                        <option value="1901">1901</option>
                                        <option value="1900">1900</option>
                                        </select>
                                        <input type="hidden" id="hidddlYear" runat="server"/>
								    </div>
                                </div>

                </div>

								<div class="form-group">
                    			    <label class="lbl-ctl">Phone: <span>*</span></label>
                                    <input type="text" id="txtCell" runat="server" name="Phone" MaxLength="20" placeholder="Phone" class="form-control target"/>
                                </div>
								<div class="form-group">
                    			    <label class="lbl-ctl">Email:</label>
                                    <input type="text" id="txtEmail" runat="server" name="Email" MaxLength="50" placeholder="Email" class="form-control target"/>
                                </div>
                                <div class="form-group">
                    			    <label class="lbl-ctl">Address: <span>*</span></label>
                                    <input type="text"  id="txtStreet" runat="server" name="txtStreet" MaxLength="75" placeholder="Address" class="form-control target required"/>
                                </div>
                                <div class="form-group">
                    			    <label class="lbl-ctl">#: </label>
                                   <input type="text" id="txtApt" name="75" runat="server"  MaxLength="25" placeholder="Apt/Suite" class="form-control target"/>
                                </div>

								<div class="form-group">
                    			    <label class="lbl-ctl">City: <span>*</span></label>
                                     <input type="text" id="txtCity" name="City" runat="server" MaxLength="50" placeholder="City" class="form-control target required"/>
                                </div>
								<div class="form-group">
                    			    <label class="lbl-ctl">State: <span>*</span></label>
                                    <input type="text" id="txtState" name="State" runat="server" MaxLength="2" placeholder="State" class="form-control target required"/>
                                </div>
								<div class="form-group">
                    			    <label class="lbl-ctl">Zip Code: <span>*</span></label>
                                   <input type="text" id="txtZip" name="Zip Code" runat="server" MaxLength="15" placeholder="Zip Code" class="form-control target required"/>
                                </div>

                                <br /><br />
                                <div class="form-wizard-buttons">
                                    <button type="button"  class="btn btn-next" onclick="window.location.href ='RCUC.aspx?id=0';" style="float:left;margin-top:15px;margin-bottom:2px;">Cancel</button>
                                    <button type="button" id="btnNext1" class="btn btn-next">Next</button>
                                </div>


                            </fieldset>
							<!-- Form Step 1 -->

							<!-- Form Step 2 -->
                            <fieldset>

                                <h4>Contact Information : <span>Step 2 - 4</span></h4>


                                <label class="lbl-ctl">Patient under 18: </label>
                                <input id="cbUnder18" type="checkbox" />
<%--===============================PARENT======================================>--%>
                                <div id="divUnder18">

								<div class="form-group">
                    			    <label class="lbl-ctl">Parent Name: </label><br />
                                    <input id="txtPName" runat="server" type="text" name="Parent Name" MaxLength="50" placeholder="Parent Name" />
                                </div>
								<div class="form-group">
                    			    <label class="lbl-ctl">Parent Phone: </label><br />
                                   <input type="text" id="txtPCell" runat="server" name="Parent Phone" MaxLength="20" placeholder="Parent Phone" class="form-control target"/>
                                    <label>same as patient: </label>
                                    <input id="cbPhoneSame" type="checkbox" onclick="copyPhone();" />
                                </div>


				                <div class="container-fluid">
								    <div class="row form-inline">
								    <div class="form-group col-md-3 col-xs-3">
                                        <label class="lbl-ctl">Date Of Birth: </label>
								    </div>
								    <div class="form-group col-md-3 col-xs-3">
									    <label>Month: </label>
                                        <select id="ddlMonthP" runat="server" class="form-control target">
									      <option>Jan</option>
									      <option>Feb</option>
									      <option>Mar</option>
									      <option>Apr</option>
									      <option>May</option>
									      <option>Jun</option>
									      <option>Jul</option>
									      <option>Aug</option>
									      <option>Sep</option>
									      <option>Oct</option>
									      <option>Nov</option>
									      <option>Dec</option>
									    </select>
								    </div>

								    <div class="form-group col-md-3 col-xs-3">
									    <label>Day: </label>
                                        <select id="ddlDayP" runat="server" class="form-control target">
									      <option>01</option>
									      <option>02</option>
									      <option>03</option>
									      <option>04</option>
									      <option>05</option>
									      <option>01</option>
									      <option>02</option>
									      <option>03</option>
									      <option>04</option>
									      <option>05</option>
									      <option>06</option>
									      <option>07</option>
									      <option>08</option>
									      <option>09</option>
									      <option>10</option>
									      <option>11</option>
									      <option>12</option>
									      <option>13</option>
									      <option>14</option>
									      <option>15</option>
									      <option>16</option>
									      <option>17</option>
									      <option>18</option>
									      <option>19</option>
									      <option>20</option>
									      <option>21</option>
									      <option>22</option>
									      <option>23</option>
									      <option>24</option>
									      <option>25</option>
									      <option>26</option>
									      <option>27</option>
									      <option>28</option>
									      <option>29</option>
									      <option>30</option>
									      <option>31</option>

									    </select>
								    </div>
								    <div class="form-group col-md-3 col-xs-3">
									    <label>Year: </label>
                                        <select name="ddlYearP" id="ddlYearP" runat="server" class="form-control ddlYear target">
                                        <option value="2022">2022</option>
                                        <option value="2021">2021</option>
                                        <option value="2020">2020</option>
                                        <option value="2019">2019</option>
                                        <option value="2018">2018</option>
                                        <option value="2017">2017</option>
                                        <option value="2016">2016</option>
                                        <option value="2015">2015</option>
                                        <option value="2014">2014</option>
                                        <option value="2013">2013</option>
                                        <option value="2012">2012</option>
                                        <option value="2011">2011</option>
                                        <option value="2010">2010</option>
                                        <option value="2009">2009</option>
                                        <option value="2008">2008</option>
                                        <option value="2007">2007</option>
                                        <option value="2006">2006</option>
                                        <option value="2005">2005</option>
                                        <option value="2004">2004</option>
                                        <option value="2003">2003</option>
                                        <option value="2002">2002</option>
                                        <option value="2001">2001</option>
                                        <option value="2000">2000</option>
                                        <option value="1999">1999</option>
                                        <option value="1998">1998</option>
                                        <option value="1997">1997</option>
                                        <option value="1996">1996</option>
                                        <option value="1995">1995</option>
                                        <option value="1994">1994</option>
                                        <option value="1993">1993</option>
                                        <option value="1992">1992</option>
                                        <option value="1991">1991</option>
                                        <option value="1990">1990</option>
                                        <option value="1989">1989</option>
                                        <option value="1988">1988</option>
                                        <option value="1987">1987</option>
                                        <option value="1986">1986</option>
                                        <option value="1985">1985</option>
                                        <option value="1984">1984</option>
                                        <option value="1983">1983</option>
                                        <option value="1982">1982</option>
                                        <option value="1981">1981</option>
                                        <option value="1980">1980</option>
                                        <option value="1979">1979</option>
                                        <option value="1978">1978</option>
                                        <option value="1977">1977</option>
                                        <option value="1976">1976</option>
                                        <option value="1975">1975</option>
                                        <option value="1974">1974</option>
                                        <option value="1973">1973</option>
                                        <option value="1972">1972</option>
                                        <option value="1971">1971</option>
                                        <option value="1970">1970</option>
                                        <option value="1969">1969</option>
                                        <option value="1968">1968</option>
                                        <option value="1967">1967</option>
                                        <option value="1966">1966</option>
                                        <option value="1965">1965</option>
                                        <option value="1964">1964</option>
                                        <option value="1963">1963</option>
                                        <option value="1962">1962</option>
                                        <option value="1961">1961</option>
                                        <option value="1960">1960</option>
                                        <option value="1959">1959</option>
                                        <option value="1958">1958</option>
                                        <option value="1957">1957</option>
                                        <option value="1956">1956</option>
                                        <option value="1955">1955</option>
                                        <option value="1954">1954</option>
                                        <option value="1953">1953</option>
                                        <option value="1952">1952</option>
                                        <option value="1951">1951</option>
                                        <option value="1950">1950</option>
                                        <option value="1949">1949</option>
                                        <option value="1948">1948</option>
                                        <option value="1947">1947</option>
                                        <option value="1946">1946</option>
                                        <option value="1945">1945</option>
                                        <option value="1944">1944</option>
                                        <option value="1943">1943</option>
                                        <option value="1942">1942</option>
                                        <option value="1941">1941</option>
                                        <option value="1940">1940</option>
                                        <option value="1939">1939</option>
                                        <option value="1938">1938</option>
                                        <option value="1937">1937</option>
                                        <option value="1936">1936</option>
                                        <option value="1935">1935</option>
                                        <option value="1934">1934</option>
                                        <option value="1933">1933</option>
                                        <option value="1932">1932</option>
                                        <option value="1931">1931</option>
                                        <option value="1930">1930</option>
                                        <option value="1929">1929</option>
                                        <option value="1928">1928</option>
                                        <option value="1927">1927</option>
                                        <option value="1926">1926</option>
                                        <option value="1925">1925</option>
                                        <option value="1924">1924</option>
                                        <option value="1923">1923</option>
                                        <option value="1922">1922</option>
                                        <option value="1921">1921</option>
                                        <option value="1920">1920</option>
                                        <option value="1919">1919</option>
                                        <option value="1918">1918</option>
                                        <option value="1917">1917</option>
                                        <option value="1916">1916</option>
                                        <option value="1915">1915</option>
                                        <option value="1914">1914</option>
                                        <option value="1913">1913</option>
                                        <option value="1912">1912</option>
                                        <option value="1911">1911</option>
                                        <option value="1910">1910</option>
                                        <option value="1909">1909</option>
                                        <option value="1908">1908</option>
                                        <option value="1907">1907</option>
                                        <option value="1906">1906</option>
                                        <option value="1905">1905</option>
                                        <option value="1904">1904</option>
                                        <option value="1903">1903</option>
                                        <option value="1902">1902</option>
                                        <option value="1901">1901</option>
                                        <option value="1900">1900</option>
                                        </select>
                                        <input type="hidden" id="hidddlYearP" runat="server"/>
								    </div>
                                    </div>
                                </div>

                                </div>

<%-- =================================================================================================================== --%>
<%-- NEW SECTION PAGE 2 --%>
<%-- ================================================================================================================== --%>
                                <h4>Emergency Contact:</h4>
				                <div class="form-group">
					                <label class="lbl-ctl" for="txtEcLastName">Last Name: </label>
					                <input type="text" id="txtEcLastName" name="EC" runat="server" MaxLength="25" placeholder="Last Name" class="form-control target"  />
                                </div>
                                <div class="form-group">
                    			    <label class="lbl-ctl" for="txtEcFirstName">First Name: </label>
                                    <input type="text" id="txtEcFirstName" name="EC" runat="server" MaxLength="25" placeholder="First Name" class="form-control target" />
                                </div>
					 
                                <div class="form-group">
                    			    <label class="lbl-ctl" for="txtEcCell">Cell Phone: </label>
                                    <input type="text" id="txtEcCell" name="EC"  runat="server" MaxLength="50" placeholder="Emergency contact cell" class="form-control target" />
                                </div>
<%--                            <div class="form-group">
                    			    <label class="lbl-ctl" for="txtEcEmail">Email: </label>
                                    <input type="text" id="txtEcEmail" name="EC"  runat="server" MaxLength="150" placeholder="Email" class="form-control target" />
                                </div>--%>
                                <h4>Next of Kin:</h4>
				                <div class="form-group">
					                <label class="lbl-ctl" for="txtNokLastName">Last Name: </label>
					                <input type="text" id="txtNokLastName" name="Nok" runat="server" MaxLength="25" placeholder="Last Name" class="form-control target" />
                                </div>
                                <div class="form-group">
                    			    <label class="lbl-ctl" for="txtNokFirstName">First Name: </label>
                                    <input type="text" id="txtNokFirstName" name="Nok" runat="server" MaxLength="25" placeholder="First Name" class="form-control target" />
                                </div>
<%--                                <div class="form-group">
                    			    <label class="lbl-ctl" for="txtNokEmail">Email: </label>
                                    <input type="text" id="txtNokEmail" name="Nok" runat="server" MaxLength="50" placeholder="Email" class="form-control target" />
                                </div>	--%>				 
                                <div class="form-group">
                    			    <label class="lbl-ctl" for="txtNokCell">Cell Phone: </label>
                                    <input type="text" id="txtNokCell" name="Nok"  runat="server" MaxLength="50" placeholder="Next of Kin Cell" class="form-control target" />
                                </div>
<%-- =================================================================================================================== --%>


<%--===============================PARENT======================================>--%>
								
                                <div class="form-wizard-buttons">
                                    <button type="button" class="btn btn-previous  prev-btn">Previous</button>
                                    <button type="button" class="btn btn-next">Next</button>
                                </div>
                            </fieldset>
							<!-- Form Step 2 -->

							<!-- Form Step 3 -->
                            <fieldset>

                                <h4>Insurance Information: <span>Step 3 of 4</span></h4>
								<div class="form-group">
                    			    <label class="lbl-ctl">Insurance Carrier: </label>
                                     <input type="text" id="txtInsCarrier" name="Insurance Carrier" runat="server" MaxLength="50" placeholder="Insurance Carrier" class="form-control target"/>
                                </div>
                                <div class="form-group">
                    			    <label class="lbl-ctl">Policy#: </label>
                                    <input type="text" id="txtInsPolicy" name="Policy"  runat="server" MaxLength="35" placeholder="Policy" class="form-control target"/>
                                </div>
								<div class="form-group">
                    			    <label class="lbl-ctl">Group: </label>
                                    <input type="text" id="txtInsGroup" name="Group"  runat="server" MaxLength="50" placeholder="Group" class="form-control target"/>
                                </div>
                                <label class="lbl-ctl">Include photo of insurance card: </label>
<%--                                <input id="cbInsPhoto1" type="checkbox" />--%>

                                 <div id="divInsPhoto1">
                                    <div class="form-group">
                                       <div class="container" style="align-content:center;">
                                            <div class="row">
                                                <div class="col-sm-5">
                                                    <button type="button" id="btnUpload1F" runat="server" class="btn btn-submit btnCardPhoto btnCardPhotoFront" onclick="file1F.click()">Capture Front</button>
                                                    <input type="file" id="file1F" runat="server" onchange="readURL(this);" class="custom-file-input" accept="image/*" capture="camera" aria-label="Get Photo" style="display:none;"/>
			                                        <br />
                                                    <img id="img1front" alt="" src="assets/img/Blank.jpg" style="height:auto;max-width:160px;display:none"/> 
                                                </div>
                                                <div class="col-sm-5">
                                                    <button type="button" id="btnUpload1B" class="btn btn-submit btnCardPhoto btnCardPhotoBack" onclick="file1B.click()">Capture Back</button>
                                                    <input type="file"  id="file1B" runat="server" onchange="readURL(this);" class="custom-file-input" accept="image/*" capture="camera" aria-label="Get Photo" style="display:none;"/>
			                                        <br />
                                                    <img id="img1back" alt="" src="assets/img/Blank.jpg" style="height:auto;max-width:160px;display:none"/> 
                                                </div>
                                            </div>
                                       </div>
                                    </div>
                                </div>



                                <hr />
								<div class="form-group">
                    			    <label class="lbl-ctl">Secondary Insurance Carrier: </label>
                                   <input type="text" id="txtInsCarrier2" name="Insurance Carrier2" runat="server" MaxLength="50" placeholder="Insurance Carrier2" class="form-control target"/>
                                </div>
                                <div class="form-group">
                    			    <label class="lbl-ctl">Policy#: </label>
                                    <input type="text" id="txtInsPolicy2" name="Policy2" runat="server" MaxLength="35" placeholder="Policy2" class="form-control target"/>
                                </div>
					 
                                <div class="form-group">
                    			    <label class="lbl-ctl">Group: </label>
                                    <input type="text" id="txtInsGroup2" name="Group2"  runat="server" MaxLength="50" placeholder="Group2" class="form-control target"/>
                                </div>
                                <label class="lbl-ctl">Include photo of insurance card: </label>
<%--                                <input id="cbInsPhoto2" type="checkbox" />--%>
                                <div id="divInsPhoto2">
                                    <div class="form-group">
                                       <div class="container" style="align-content:center;">

                                            <div class="row">
                                                <div class="col-sm-5">
                                                    <button type="button" id="btnUpload2F" class="btn btn-submit btnCardPhoto btnCardPhotoFront" onclick="file2F.click()">Capture Front</button>
                                                     <input type="file" id="file2F" onchange="readURL(this);"  runat="server" class="custom-file-input" accept="image/*" capture="camera" aria-label="Get Photo" style="display:none;"/>
			                                        <br />
                                                    <img id="img2front" alt="" src="assets/img/Blank.jpg" style="height:auto;max-width:160px;display:none"/> 
                                                </div>
                                                <div class="col-sm-5">
                                                    <button type="button" id="btnUpload2B" class="btn btn-submit btnCardPhoto btnCardPhotoBack" onclick="file2B.click()">Capture Back</button>
                                                     <input type="file" id="file2B" onchange="readURL(this);"  runat="server" class="custom-file-input" accept="image/*" capture="camera" aria-label="Get Photo" style="display:none;"/>
			                                        <br />
                                                    <img id="img2back" alt="" src="assets/img/Blank.jpg" style="height:auto;max-width:160px;display:none;"/> 
                                                </div>
                                            </div>

                                       </div>
                                   </div>

                                </div>

                                <div id="divHBS">
                                    
<%--                                    harcopy is available at front desk />--%>
                                    <img ID="imgDoc" src="assets/icons/download.png" onclick="openDoc('Notice of Beneficiary.pdf')" />
                                    <asp:Label ID="Label1" runat="server" Text="Label" Font-Size="Large" ForeColor="#0066CC">HBS Notice (for Medicare & Medicare Advantage)</asp:Label>
                                    <br />
                                    <label class="radio-inline" style="margin:0px;">a hardcopy is available at front desk.</label>
                                
                                    
                                </div>



								
                                <div class="form-wizard-buttons">
                                    <button type="button" class="btn btn-previous prev-btn">Previous</button>
                                    <button type="button" class="btn btn-next">Next</button>
                                </div>
                            </fieldset>
							<!-- Form Step 3 -->
<%-- =================================================================================================--%>
<!-- Form Step 4 -->
<%-- =================================================================================================--%>
							<fieldset>
    <div class="page">
        <h1>&nbsp;</h1>
        <table class="layout display responsive-table">
            <thead>
                <tr>
                    <th id="tbl-header" colspan="3">Verify Information</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td colspan="3" class="verif-section-label"><b>Personal Info</b></td>
                </tr>
                <tr>
                    <td class="tbl-col-1"><label id="lblVerif1"  class="verif-lbl-lbl" runat="server">Name:</label></td>
                    <td class="tbl-col-2"><label id="lblVerifName" class="verif-lbl-val" runat="server"></label></td>
                </tr>

                <tr>
                    <td class="tbl-col-1"><label id="Label9"  class="verif-lbl-lbl" runat="server">DOB:</label></td>
                    <td class="tbl-col-2"><label id="lblVerifDOB" class="verif-lbl-val" runat="server"></label></td>
                </tr>

                <tr>
                    <td class="tbl-col-1"><label id="Label2"  class="verif-lbl-lbl" runat="server">Address:</label></td>
                    <td class="tbl-col-2"><label id="lblVerifAddr1" class="verif-lbl-val" runat="server"></label><br />
                    <label id="lblVerifAddr2" class="verif-lbl-val" runat="server"></label>
                    </td>
                </tr>

                <tr>
                    <td class="tbl-col-1"><label id="Label5"  class="verif-lbl-lbl" runat="server">Phone:</label></td>
                    <td class="tbl-col-2"><label id="lblVerifPhone" class="verif-lbl-val" runat="server"></label></td>
                </tr>

                <tr>
                    <td class="tbl-col-1"><label id="Label7" class="verif-lbl-lbl" runat="server">Email:</label></td>
                    <td class="tbl-col-2"><label id="lblVerifEmail" class="verif-lbl-val" runat="server"></label></td>
                </tr>
                <tr>
                    <td colspan="3" class="verif-section-label"><b>Parent Info</b></td>
                </tr>
                <tr>
                    <td class="tbl-col-1"><label id="Label3"  class="verif-lbl-lbl" runat="server">Parent Name:</label></td>
                    <td class="tbl-col-2"><label id="lblVerifPName" class="verif-lbl-val" runat="server"></label></td>
                </tr>

                <tr>
                    <td class="tbl-col-1"><label id="Label6"  class="verif-lbl-lbl" runat="server">Parent DOB:</label></td>
                    <td class="tbl-col-2"><label id="lblVerifPdob" class="verif-lbl-val" runat="server"></label></td>
                </tr>
                <tr>
                    <td class="tbl-col-1"><label id="Label4"  class="verif-lbl-lbl" runat="server">Parent Phone:</label></td>
                    <td class="tbl-col-2"><label id="lblVerifPphone"   class="verif-lbl-val" runat="server"></label></td>
                </tr>
                <tr>
                    <td colspan="3" class="verif-section-label"><b>Emergency Contact</b></td>
                </tr>
                <tr>
                    <td class="tbl-col-1"><label id="Label11"  class="verif-lbl-lbl" runat="server">Name:</label></td>
                    <td class="tbl-col-2"><label id="lblVerifEcName" class="verif-lbl-val" runat="server"></label></td>
                </tr>
                <tr>
                    <td class="tbl-col-1"><label id="Label13"  class="verif-lbl-lbl" runat="server">Phone:</label></td>
                    <td class="tbl-col-2"> <label id="lblVerifEcPhone"   class="verif-lbl-val" runat="server"></label></td>
                </tr>

                <tr>
                    <td colspan="3" class="verif-section-label"><b>Next of Kin</b></td>
                </tr>
                <tr>
                    <td class="tbl-col-1"><label id="Label18"  class="verif-lbl-lbl" runat="server">Name:</label></td>
                    <td class="tbl-col-2"><label id="lblVerifNokName" class="verif-lbl-val" runat="server"></label></td>
                </tr>

                <tr>
                    <td class="tbl-col-1"><label id="Label19"  class="verif-lbl-lbl" runat="server">Phone:</label></td>
                    <td class="tbl-col-2"><label id="lblVerifNokPhone"   class="verif-lbl-val" runat="server"></label></td>
                </tr>
                <tr>
                    <td colspan="3" class="verif-section-label"><b>Primary Insurance</b></td>
                </tr>
                <tr>
                    <td class="tbl-col-1"><label id="Label23"  class="verif-lbl-lbl" runat="server">Carrier:</label></td>
                    <td class="tbl-col-2"><label id="lblVerifIns1Carrier" class="verif-lbl-val" runat="server"></label></td>
                </tr>
                <tr>
                    <td class="tbl-col-1"><label id="Label25"  class="verif-lbl-lbl" runat="server">Policy#:</label></td>
                    <td class="tbl-col-2"><label id="lblVerifIns1Policy"   class="verif-lbl-val" runat="server"></label></td>
                </tr>

                <tr>
                    <td class="tbl-col-1"><label id="Label27"  class="verif-lbl-lbl" runat="server">Group:</label></td>
                    <td class="tbl-col-2"><label id="lblVerifIns1Group"   class="verif-lbl-val" runat="server"></label></td>
                </tr>
                <tr>
                    <td class="tbl-col-1"><label id="Label37"  class="verif-lbl-lbl" runat="server">Card Photo:</label></td>
                    <td class="tbl-col-2"><label id="lblVeridcardPhoto1"   class="verif-lbl-val" runat="server"></label></td>
                </tr>
                <tr>
                    <td colspan="3" class="verif-section-label"><b>Secondary Insurance</b></td>
                </tr>
                <tr>
                    <td class="tbl-col-1"><label id="lblVerifIns2Carrierlbl" class="verif-lbl-lbl" runat="server">Carrier:</label></td>
                    <td class="tbl-col-2"><label id="lblVerifIns2Carrier" class="verif-lbl-val" runat="server"></label></td>
                </tr>
                <tr>
                    <td class="tbl-col-1"><label id="Label33"  class="verif-lbl-lbl" runat="server">Policy#:</label></td>
                    <td class="tbl-col-2"><label id="lblVerifIns2Policy" class="verif-lbl-val" runat="server"></label></td>
                </tr>

                <tr>
                    <td class="tbl-col-1"><label id="Label35"  class="verif-lbl-lbl" runat="server">Group:</label></td>
                    <td class="tbl-col-2"><label id="lblVerifIns2Group" class="verif-lbl-val" runat="server"></label></td>
                </tr>
                <tr>
                    <td class="tbl-col-1"><label id="Label8"  class="verif-lbl-lbl" runat="server">Card Photo:</label></td>
                    <td class="tbl-col-2"><label id="lblVeridcardPhoto2"   class="verif-lbl-val" runat="server"></label></td>
                </tr>
                
            </tbody>
        </table>
        <button id="btnPrev" type="button" class="btn btn-previous prev-btn">Previous</button>
        <button id="btnSubmit" type="button" runat="server" onclick="checkFormBeforeSubmit();" class="btn btn-submit" disabled="disabled">Submit</button>
    </div>
  </fieldset>
					<!-- Form Step 4 -->
   <input id="hidSID" type="hidden" runat="server"/>
   <input id="hidFacilID" runat="server" type="hidden" />
                </form>
				<!-- Form Wizard -->
            </div>
        </div>
                    
    </div>
</section>
        
        <div class="overlay"></div>
</body>

<script>

    // "use strict";
    function seeRegist() {
        alert('Please proceed inside to see a registrar.');
    }

    function openModal() {
        $("#modal").css("display", "block");
    }
    function closeModal() {
        $("#modal").css("display", "none");
    }

    $('#closeBtn').on('click', function () {
        closeModal();
    });


    $(".target").change(function () {
        //  alert("Handler for .change() called.");
        var fname = $('#txtFName').val() + ' ' + $('#txtLName').val();
        $('#lblVerifName').text(fname);
        var dob = $('#ddlMonth').val() + ' ' + $('#ddlDay').val() + ', ' + $('#ddlYear').val();
        $('#lblVerifDOB').text(dob);
        $('#lblVerifPhone').text($('#txtCell').val());
        $('#lblVerifEmail').text($('#txtEmail').val());
        /*  $('#lblVerifEmail').val();*/

        var addr1 = $('#txtStreet').val()
        if ($('#txtApt').val().length > 0) {
            addr1 += ', #' + $('#txtApt').val();
        }
        var addr2 = $('#txtCity').val() + ', ' + $('#txtState').val() + ' ' + $('#txtZip').val();
        $('#lblVerifAddr1').text(addr1);
        $('#lblVerifAddr2').text(addr2);

        var pName = $('#txtPName').val();
       // alert('pName.length = ' + pName.length);
        if (pName.length > 0) {
            $('#lblVerifPName').text(pName);
            $('#lblVerifPphone').text($('#txtPCell').val());
            var dobP = $('#ddlMonthP').val() + ' ' + $('#ddlDayP').val() + ', ' + $('#ddlYearP').val();
            $('#lblVerifPdob').text(dobP);
        }
        else {
            $('#lblVerifPName').text('');
            $('#lblVerifPphone').text('');
            $('#lblVerifPdob').text('');
        }


        var ecName = $('#txtEcFirstName').val() + ' ' + $('#txtEcLastName').val();
        $('#lblVerifEcName').text(ecName);
        //$('#lblVerifEcEmail').text($('#txtEcEmail').val());
        $('#lblVerifEcPhone').text($('#txtEcCell').val());

        var ecName = $('#txtNokFirstName').val() + ' ' + $('#txtNokLastName').val();
        $('#lblVerifNokName').text(ecName);
        $('#lblVerifNokPhone').text($('#txtNokCell').val());
        //$('#lblVerifNokEmail').text($('#txtNokEmail').val());


        $('#lblVerifIns1Carrier').text($('#txtInsCarrier').val());
        $('#lblVerifIns1Policy').text($('#txtInsPolicy').val());
        $('#lblVerifIns1Group').text($('#txtInsGroup').val());

        $('#lblVerifIns2Carrier').text($('#txtInsCarrier2').val());
        $('#lblVerifIns2Policy').text($('#txtInsPolicy2').val());
        $('#lblVerifIns2Group').text($('#txtInsGroup2').val());

    });



    function copyPhone() {
        //   alert($("#txtCell").val());
        let isChecked = $('#cbPhoneSame').is(':checked');
        //  alert("IsChecked = " + isChecked);
        if (isChecked == true) {

            //     alert("checked");
            $("#txtPCell").val($("#txtCell").val());
        }
        else {
            //     alert("not checked");
            $("#txtPCell").val('');
        }

    }
    function readURL(input) {
        // alert('readURL:' + input.id);
        var inputID = input.id;
        var strImgID = '';
        switch (inputID) {
            case "file1F":
                strImgID = '#img1front';
                //$("btnUpload1B").backcolor(rgb(109, 57, 108)
                // $("btnUpload1B").css("background-color", "green");
                $("#btnUpload1F").removeClass('btnCardPhoto');
                $("#btnUpload1F").addClass('btnCardPhotoOK');

                //alert('1F.length  ' + $('#file1F')[0].files.length);
                if ($('#file1F')[0].files.length === 0) {
                    $('#lblVeridcardPhoto1').text("No photo");
                } else {
                    $('#lblVeridcardPhoto1').text("Photo attached");
                }

                break;
            case "file1B":
                strImgID = '#img1back';
                $("#btnUpload1B").removeClass('btnCardPhoto');
                $("#btnUpload1B").addClass('btnCardPhotoOK');
                //btnCardPhotoOK
                //btnCardPhoto
                break;
            case "file2F":
                strImgID = '#img2front';
               // alert(inputID);
                $("#btnUpload2F").removeClass('btnCardPhoto');
                $("#btnUpload2F").addClass('btnCardPhotoOK');
               // alert('2F.length  ' + $('#file2F')[0].files.length);
                if ($('#file2F')[0].files.length === 0) {
                    $('#lblVeridcardPhoto2').text("No photo");
                } else {
                    $('#lblVeridcardPhoto2').text("Photo attached");
                }
                break;
            case "file2B":
                strImgID = '#img2back';
               // alert(inputID);
                $("#btnUpload2B").addClass('btnCardPhotoOK');
                $("#btnUpload2B").removeClass('btnCardPhoto');
                break;
        }

        if (input.files && input.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                $(strImgID)
                    .attr('src', e.target.result)
                    .width(200)
                    .height(100);
            };
            reader.readAsDataURL(input.files[0]);
        }


    }

    //=======================//  niu  //===============================================================================
    function checkFormBeforeSubmit() {
        $("#hidSubmitFlag").val("submit");

        $("#btnSubmit").html("Please wait...");
        $("#btnSubmit").prop('disabled', true);
        $("#btnSubmit").css("background-color", "green");
        $("#form1").submit();


    }
    //    =======================//  niu  //===============================================================================

    function scroll_to_class(element_class, removed_height) {
        var scroll_to = $(element_class).offset().top - removed_height;
        if ($(window).scrollTop() != scroll_to) {
            $('.form-wizard').stop().animate({ scrollTop: scroll_to }, 0);
        }
    }

    function bar_progress(progress_line_object, direction) {
        var number_of_steps = progress_line_object.data('number-of-steps');
        var now_value = progress_line_object.data('now-value');
        var new_value = 0;
        if (direction == 'right') {
            new_value = now_value + (100 / number_of_steps);
        }
        else if (direction == 'left') {
            new_value = now_value - (100 / number_of_steps);
        }
        progress_line_object.attr('style', 'width: ' + new_value + '%;').data('now-value', new_value);
    }




    $(document).ready(function () {
        // Prevent Enter key from submitting form
        disableSubmit();
        $(window).keydown(function (e) {
            if (e.keyCode == 13) {
                e.preventDefault();
                return false;
            }
        });

        $("consent-agree").hide;
        showHideParent();
        showHideQ();
        $("#divHBS").hide();

        $("#txtInsCarrier").change(function () {
           // alert('checking medicare');
            if (isMedicare($("#txtInsCarrier").val())) {
                //  alert('IS medicare = true');
                $("#divHBS").show();
            }
            else {
                $("#divHBS").hide();
                //    alert('NOT medicare1');
            }
        });

        $("#txtInsCarrier2").change(function () {
            if (isMedicare($("#txtInsCarrier2").val())) {
                $("#divHBS").show();
            }
            else {
                $("#divHBS").hide();
            }
        });


        /*        $("#btnSubmit").prop('disabled', true);*/
        $('.custom-file-input input[type="file"]').change(function (e) {
            $(this).siblings('input[type="text"]').val(e.target.files[0].name);
        });
        /*
            Form
        */
        $('.form-wizard fieldset:first').fadeIn('slow');

        $('.form-wizard .required').on('focus', function () {
            $(this).removeClass('input-error');
        });

        $('input:radio').change(function () {
            validate();
        });




        var z = document.getElementById("divUnder18");
        $(document).on('change', 'input[Id="cbUnder18"]', function (e) {
            var checkbox = $(this), // Selected or current checkbox
                value = checkbox.val(); // Value of checkbox

            if (checkbox.is(':checked')) {
                z.style.display = "block";
            } else {
                z.style.display = "none";
            }
        });


        $(document).on('change', 'input[Id="rad5N"]', function (e) {
            var checkbox = $(this), // Selected or current checkbox
                value = checkbox.val(); // Value of checkbox
            if (value == 'No') {
                w.style.display = "none";
            }
        });


        $("#ddlYear").change(function () {
            // Updates eeach year ddl value to the correspodning hid control so hid ctl alsways shows ddl value after change
            // --- needed becuase the year ddls are populated dynamically and for some reason don't show a value in code behind aafter postback
            $("#hidddlYear").val($("#ddlYear").val());
        });
        $("#ddlYearP").change(function () {
            // Updates eeach year ddl value to the correspodning hid control so hid ctl alsways shows ddl value after change
            // --- needed becuase the year ddls are populated dynamically and for some reason don't show a value in code behind aafter postback
            $("#hidddlYearP").val($("#ddlYearP").val());
        });

        $("body").on("click", "#btnSubmit", function () {
            $("#hidSubmit").val("submit");
            $("#form1").submit();
        });


        $("body").on("click", "#btnSaveImgFront", function () {
            var formData = new FormData;
            var strSeessionID = $('#hidSID').val();
            var imgFront = $('#File1F')[0].files;
            var imgBack = $('#File1B')[0].files;


            if (imgFront.length > 0) {

                var strImgFname = imgFront.name + '|' + 'FRONT';
                formData.append(strImgFname, imgFront);
            }

            if (imgBack.length > 0) {
                var strImgBname = imgBack.name + '|' + 'BACK';

                formData.append(strImgBname, imgBack);
                formData.append(sessionID, strSeessionID);
            }

            $.ajax({
                url: 'HandlerVB.ashx?sid=' + strSeessionID,
                type: 'POST',
                data: formData,
                cache: false,
                contentType: false,
                processData: false,
                success: function (file) {
                    $("#fileProgress").hide();
                    $("#lblMessage").html("<b>" + file.name + "</b> has been uploaded.");
                    location.reload();
                },
                xhr: function () {
                    var fileXhr = $.ajaxSettings.xhr();
                    if (fileXhr.upload) {
                        $("progress").show();
                        fileXhr.upload.addEventListener("progress", function (e) {
                            if (e.lengthComputable) {
                                $("#fileProgress").attr({
                                    value: e.loaded,
                                    max: e.total
                                });
                            }
                        }, false);
                    }
                    return fileXhr;
                }
            });
        });



        // next step
        $('.form-wizard .btn-next').on('click', function () {
            var parent_fieldset = $(this).parents('fieldset');
            var next_step = true;
            // navigation steps / progress steps
            var current_active_step = $(this).parents('.form-wizard').find('.form-wizard-step.active');
            var progress_line = $(this).parents('.form-wizard').find('.form-wizard-progress-line');


            // fields validation
            parent_fieldset.find('.required').each(function () {
                if ($(this).val() == "") {
                    $(this).addClass('input-error');
                    next_step = false;
                   
                }
                else {
                    $(this).removeClass('input-error');
                }

                //$("input[type='radio'][name='rad1']:checked").val()
                var ConsTreat = $("input[type='radio'][name='radConsTreat']:checked").val()
                var ConsFin = $("input[type='radio'][name='radConsFin']:checked").val()
                if (ConsTreat == 'No') {
                    next_step = false;
                }
                if (ConsTreat == undefined) {
                    next_step = false;
                }

                if (ConsFin == 'No') {
                    next_step = false;
                }
                if (ConsFin == undefined) {
                    next_step = false;
                }
            });
            // fields validation

            if (next_step) {
                parent_fieldset.fadeOut(400, function () {
                    // change icons
                    current_active_step.removeClass('active').addClass('activated').next().addClass('active');
                    // progress bar
                    bar_progress(progress_line, 'right');
                    // show next step
                    $(this).next().fadeIn();
                    // scroll window to beginning of the form
                    scroll_to_class($('.form-wizard'), 20);
                });
            }

        });

        // previous step
        $('.form-wizard .btn-previous').on('click', function () {
            // navigation steps / progress steps
            var current_active_step = $(this).parents('.form-wizard').find('.form-wizard-step.active');
            var progress_line = $(this).parents('.form-wizard').find('.form-wizard-progress-line');

            $(this).parents('fieldset').fadeOut(400, function () {
                // change icons
                current_active_step.removeClass('active').prev().removeClass('activated').addClass('active');
                // progress bar
                bar_progress(progress_line, 'left');
                // show previous step
                $(this).prev().fadeIn();
                // scroll window to beginning of the form
                scroll_to_class($('.form-wizard'), 20);
            });
        });

        // submit
        $('.form-wizard').on('submit', function (e) {

            // fields validation
            $(this).find('.required').each(function () {
                if ($(this).val() == "") {
                    e.preventDefault();
                    $(this).addClass('input-error');
                }
                else {
                    $(this).removeClass('input-error');
                }
            });
            // fields validation

        });

        var phoneInput = document.getElementById('txtCell');
        var phoneInputP = document.getElementById('txtPCell');
        var phoneInputEc = document.getElementById('txtEcCell');
        var phoneInputNok = document.getElementById('txtNokCell');

        phoneInput.addEventListener('input', function (e) {
            var x = e.target.value.replace(/\D/g, '').match(/(\d{0,3})(\d{0,3})(\d{0,4})/);
            e.target.value = !x[2] ? x[1] : '(' + x[1] + ') ' + x[2] + (x[3] ? '-' + x[3] : '');
        });

        phoneInputP.addEventListener('input', function (e) {
            var x1 = e.target.value.replace(/\D/g, '').match(/(\d{0,3})(\d{0,3})(\d{0,4})/);
            e.target.value = !x1[2] ? x1[1] : '(' + x1[1] + ') ' + x1[2] + (x1[3] ? '-' + x1[3] : '');
        });

        phoneInputEc.addEventListener('input', function (e) {
            var x2 = e.target.value.replace(/\D/g, '').match(/(\d{0,3})(\d{0,3})(\d{0,4})/);
            e.target.value = !x2[2] ? x2[1] : '(' + x2[1] + ') ' + x2[2] + (x2[3] ? '-' + x2[3] : '');
        });

        phoneInputNok.addEventListener('input', function (e) {
            var x3 = e.target.value.replace(/\D/g, '').match(/(\d{0,3})(\d{0,3})(\d{0,4})/);
            e.target.value = !x3[2] ? x3[1] : '(' + x3[1] + ') ' + x3[2] + (x3[3] ? '-' + x3[3] : '');
        });

    });
    function showHideIns1() {
        var x = document.getElementById("divInsPhoto1");
        if (x.style.display === "none") {
            x.style.display = "block";
        } else {
            x.style.display = "none";
        }
    }

    function showHideIns2() {
        var x = document.getElementById("divInsPhoto2");
        if (x.style.display === "none") {
            x.style.display = "block";
        } else {
            x.style.display = "none";
        }
    }

    function showHideParent() {
        var x = document.getElementById("divUnder18");
        if (x.style.display === "none") {
            x.style.display = "block";
        } else {
            x.style.display = "none";
        }
    }

    function showHideQ() {
        //    var x = document.getElementById("divHidQ");
        //    if (x.style.display === "none") {
        //        x.style.display = "block";
        //    } else {
        //        x.style.display = "none";
        //    }
    }

    function isMedicare(s) {
        var insCarrier1 = $("#txtInsCarrier").val();
        var insCarrier2 = $("#txtInsCarrier2").val();

        var Carrier = insCarrier1.toLowerCase();
        var result = Carrier.indexOf("medicare");
       // alert('index of medicare = ' + result);
        switch (result) {
            case -1:
         //       alert('NOT Medicare');
                return false;
                break;
            default:
        //        alert('Is Medicare');
                return true
                break;

        }

        //txtInsCarrier2
    }

    function nsZoomZoom() {
        htmlWidth = $('html').innerWidth();
        bodyWidth = 1000;

        if (htmlWidth < bodyWidth)
            scale = 1
        else {
            scale = htmlWidth / bodyWidth;
        }

        // Req for IE9
        $("body").css('-ms-transform', 'scale(' + scale + ')');
        $("body").css('transform', 'scale(' + scale + ')');
    }

    function validateQs() {

        var validCount = 2;
        var validQs = true;
        var iChecked = 0;
        var threeChecked = ($("input[type='radio'][name='rad5']:checked").val() == 'Yes');
        var ConsTreat = $("input[type='radio'][name='radConsTreat']:checked").val();
        var ConsFin = $("input[type='radio'][name='radConsFin']:checked").val();

        if (ConsTreat == undefined) {
            validQs = false;
        }
        else {
            iChecked++;

        }
        if (ConsFin == undefined) {
            validQs = false;
        }
        else {
            iChecked++;
        }

        validQs = (iChecked == validCount);
        if (ConsTreat == 'No') {
            validQs = false;
        }
        if (ConsFin == 'No') {
            validQs = false;
        }

        if (validQs == true) {
        }
        else {
            disableSubmit();
        }
        return validQs;

    }

    $(".required").keydown(function () {
        validate();
    });

    $('.required').on('blur input', function () {
        validate();
    });

    function validate() {
        var valid = true;
        var fname = $('#txtFName').val();
        var lname = $('#txtLName').val();
        var cell = $('#txtCell').val();
        // var email = $('#txtEmail').val();
        var addr1 = $('#txtStreet').val();
        var city = $('#txtCity').val();
        var state = $('#txtState').val();
        var zip = $('#txtZip').val();
        if (fname == "") { valid = false; }
        if (lname == "") { valid = false; }
        if (cell == "") { valid = false; }
        /*        if (email == "") { valid = false; }*/
        if (addr1 == "") { valid = false; }
        if (city == "") { valid = false; }
        if (state == "") { valid = false; }
        if (zip == "") { valid = false; }

        if (valid == false) { disableSubmit(); }
        if (valid == true) { enableSubmit(); }
        validateQs();
    }

    function enableSubmit() {
        $("#btnSubmit").prop('disabled', false);
        $("#btnSubmit").css("background-color", "green");
        $("#btnNext1").prop('disabled', false);
        $("#btnNext1").css("background-color", "#0f216b");

  
    }

    function disableSubmit() {
        $("#btnSubmit").css("background-color", "grey");
        $("#btnSubmit").prop('disabled', true);
        $("#btnNext1").prop('disabled', true);
        $("#btnNext1").css("background-color", "grey");
    }
    function populateYears() {

        var dateDropdown = document.getElementById('ddlYearP');

        var currentYear = new Date().getFullYear();
        var earliestYear = 1900;
        while (currentYear >= earliestYear) {
            let dateOption = document.createElement('option');
            dateOption.text = currentYear;
            dateOption.value = currentYear;
            dateDropdown.add(dateOption);
            currentYear -= 1;
        }
        var strYear = '<option>' + currentYear + '</option>';
        dateDropdown = document.getElementById('ddlYear');
        currentYear = new Date().getFullYear();
        earliestYear = 1900;
        while (currentYear >= earliestYear) {
            let dateOption = document.createElement('option');
            dateOption.text = currentYear;
            dateOption.value = currentYear;
            dateDropdown.add(dateOption);

            currentYear -= 1;
        }
        strYear += ' ';
    }

    function openDoc(doc) {
        var sdoc;
        var popUpObj;
        popUpObj = window.open("Docs/" + doc, "_blank,toolbar=no,location=no,menubar=no,resizable=0,scrollbars=yes,width=100,height=100,left = 490,top=300");
    }

    function toggleFullScreen() {
        var doc = window.document;
        var docEl = doc.documentElement;

        var requestFullScreen = docEl.requestFullscreen || docEl.mozRequestFullScreen || docEl.webkitRequestFullScreen || docEl.msRequestFullscreen;
        var cancelFullScreen = doc.exitFullscreen || doc.mozCancelFullScreen || doc.webkitExitFullscreen || doc.msExitFullscreen;

        if (!doc.fullscreenElement && !doc.mozFullScreenElement && !doc.webkitFullscreenElement && !doc.msFullscreenElement) {
            requestFullScreen.call(docEl);
        }
        else {
            cancelFullScreen.call(doc);
        }
    }

    $(document).on({
        ajaxStart: function () {
            $("body").addClass("loading");
        },
        ajaxStop: function () {
            $("body").removeClass("loading");
        }
    });

</script>

</html>
