﻿<%@ Page Title="" Language="C#" MasterPageFile="~/mobile.master" AutoEventWireup="true" CodeFile="displayoutgoingreport.aspx.cs" Inherits="desktop_reports_outgoing_displayoutgoingreport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolder_PageTitle" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolder_Content" Runat="Server">
    <style>
        .center
        {
            display:block;
            margin: 0px auto;
            border: 1px solid black;
        }
    </style>
       <asp:scriptmanager ID="Scriptmanager1" runat="server"></asp:scriptmanager>
        <h1>Outgoing Report</h1>
        <rsweb:ReportViewer  CssClass="center" Width="650px" Height="1000px" ID="reportViewer" ShowPrintButton="true" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
        <LocalReport ReportPath="desktop\reports\outgoing\outgoingreport.rdlc">
        </LocalReport>
        </rsweb:ReportViewer>
</asp:Content>

