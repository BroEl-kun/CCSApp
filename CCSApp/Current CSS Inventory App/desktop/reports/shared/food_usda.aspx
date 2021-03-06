﻿<%@ Page Title="" Language="C#" MasterPageFile="~/mobile.master" AutoEventWireup="true" CodeFile="food_usda.aspx.cs" Inherits="desktop_reports_incoming_Food_USDA"  %>

<%@ Register Src="~/NumericKeypad.ascx" TagPrefix="uc1" TagName="NumericKeypad" %>

<%@ Register src="ListSelectionControl.ascx" tagname="ListSelectionControl" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolder_PageTitle" Runat="Server">
    &nbsp;Grocery Rescue
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolder_Content" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    <h1>Categories</h1>
    <div id="Title">
       <asp:Label ID="lblQuest" runat="server" Text="Please Select the Categories you would like to show up on the Report"></asp:Label>
</div>

<style>
   #Title {text-align:center; margin: 0px auto;}
</style>
    
    <br />
    <div id="InputForm">
    
        <asp:Label ID="Label1" runat="server" Text="Regular Categories" Font-Bold="True" Font-Underline="True"></asp:Label>
        &nbsp;<br />
        <uc2:ListSelectionControl ID="lstRegularFood" runat="server" FoodCategories="true"  AllowNone="true"/>
        <br />
        <asp:Label ID="Label2" runat="server" Text="USDA Categories" Font-Bold="True" Font-Underline="True" ></asp:Label>
        <br />
        <uc2:ListSelectionControl ID="lstUSDA" runat="server" AllowNone="true" />
    </div>
    <style>
   #InputForm {text-align:center; margin: 0px auto;}
</style>
    <asp:Label ID="lblError" runat="server" ForeColor="Red" />
    <br />
    <div id="Button">
    <asp:Button ID="btnBack" CssClass="cancel" runat="server" Text="Back" OnClientClick="JavaScript: window.history.go(-1); return false;" />
         &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    <asp:Button ID="btnNext" runat="server" CssClass="submit" Text="Next" OnClick="btnNext_Click" />
     </div>
    <style>
        #Button{ text-align:center; margin: 0px auto;}
    </style>
</asp:Content>



