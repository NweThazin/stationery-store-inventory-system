<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="PurchaseReport.aspx.cs" Inherits="ADProjectSA43_Team1.Reports.PurchaseReport" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <br />
    <asp:CheckBoxList ID="chklPurchase" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ckblPurchase_SelectedIndexChanged" RepeatDirection="Horizontal">
        <asp:ListItem>Clip</asp:ListItem>
        <asp:ListItem>Envelope</asp:ListItem>
        <asp:ListItem>Eraser</asp:ListItem>
        <asp:ListItem>Exercise</asp:ListItem>
        <asp:ListItem>File</asp:ListItem>
        <asp:ListItem>Pen</asp:ListItem>
        <asp:ListItem>Puncher</asp:ListItem>
        <asp:ListItem>Pad</asp:ListItem>
        <asp:ListItem>Paper</asp:ListItem>
        <asp:ListItem>Ruler</asp:ListItem>
        <asp:ListItem>Scissors</asp:ListItem>
        <asp:ListItem>Tape</asp:ListItem>
        <asp:ListItem>Sharpener</asp:ListItem>
        <asp:ListItem>Shorthand</asp:ListItem>
        <asp:ListItem>Stapler</asp:ListItem>
        <asp:ListItem>Tacks</asp:ListItem>
        <asp:ListItem>Transparency</asp:ListItem>
        <asp:ListItem>Tray</asp:ListItem>
    </asp:CheckBoxList>
    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" HasExportButton="False" HasPrintButton="False" Height="50px" SeparatePages="False" Width="350px" DisplayToolbar="False" HasToggleGroupTreeButton="False" HasToggleParameterPanelButton="False" HasZoomFactorList="False" />
</asp:Content>
