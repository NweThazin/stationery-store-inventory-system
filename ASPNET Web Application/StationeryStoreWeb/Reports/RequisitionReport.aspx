<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="RequisitionReport.aspx.cs" Inherits="ADProjectSA43_Team1.Reports.RequisitionReport" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br/>
    <br/>
<asp:CheckBoxList ID="chklDepartment" runat="server" AutoPostBack="True" OnSelectedIndexChanged="chklDepartment_SelectedIndexChanged" RepeatDirection="Horizontal">
        <asp:ListItem>Management</asp:ListItem>
        <asp:ListItem>Medical</asp:ListItem>
        <asp:ListItem>Engineering</asp:ListItem>
        <asp:ListItem>Science</asp:ListItem>
        <asp:ListItem>Student Welfare</asp:ListItem>
        <asp:ListItem>Finance</asp:ListItem>
        <asp:ListItem>Registrar</asp:ListItem>
        <asp:ListItem>Facilities Managment</asp:ListItem>
        <asp:ListItem>Admission</asp:ListItem>
    </asp:CheckBoxList>
<CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" HasExportButton="False" HasPrintButton="False" Height="50px" SeparatePages="False" Width="350px" DisplayToolbar="False" EnableDatabaseLogonPrompt="False" HasToggleGroupTreeButton="False" HasToggleParameterPanelButton="False" HasZoomFactorList="False" />
</asp:Content>
