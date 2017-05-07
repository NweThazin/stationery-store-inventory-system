<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="DestinationPage.aspx.cs" Inherits="ADProjectSA43_Team1.StoreClerk.DestinationPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid pg-content">
        <div class="row">
            <div class="col-md-12">
                <div class="row">
                    <div class="col-md-8">
                        <h2><i class="glyphicon glyphicon-map-marker"></i><%--Stationery Store--%> <asp:Label ID="collectionPoint" runat="server" Text="collectionPoint"></asp:Label></h2>
                    </div>
                </div>
            </div>
        </div>
        <br/>
        

    <div class="row">
         <div class="table-responsive">
             <asp:GridView ID="GVDestination" runat="server"  AutoGenerateColumns="False" CssClass="table" GridLines="Horizontal" Height="100%" Width="100%" CellPadding="3" BorderWidth="0px">
                 <Columns>
                     <asp:TemplateField HeaderText="#" >
                         <ItemTemplate>
                             <%# Container.DataItemIndex+1 %>
                         </ItemTemplate>
                     </asp:TemplateField>
                     <asp:BoundField DataField="DepartmentName" HeaderText="DepartmentName" />
                     <asp:TemplateField >
                           <ItemTemplate>
                               <asp:HiddenField ID="DepartmentID" value='<%#Eval("DepartmentID") %>'  runat="server" />
                           </ItemTemplate>
                       </asp:TemplateField>
                     <asp:BoundField HeaderText="Representative" DataField="EmployeeName" />
                     <asp:BoundField HeaderText="TotalItem" DataField="TotalItem" />
                     <asp:HyperLinkField DataNavigateUrlFields="DepartmentID,CollectionPointID" DataNavigateUrlFormatString="DisbursementListPage.aspx?departmentID={0}&&collectionPointID={1}" Text="View" />
                 </Columns>
             </asp:GridView>
         </div>
    </div>
          <div class="row">
                 <div class="col-md-12">
                     <div class="col-md-4"></div>
                     <div class="col-md-4">
                         <asp:Button ID="Button1" runat="server" Text="BACK" OnClick="Button1_Click" CssClass="btn btn-md btn-primary"/>
                     </div>
                     <div class="col-md-4"></div>
                 </div>
             </div>  
            </div>
        </div>
    </div>
</asp:Content>
