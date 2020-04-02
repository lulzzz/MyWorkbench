<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WorkflowTaskKanban.ascx.cs" Inherits="MyWorkbench.Web.CustomControls.WorkflowTaskKanban" %>
<%@ Register assembly="Syncfusion.EJ.Web, Version=17.1460.0.38, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89" namespace="Syncfusion.JavaScript.Web" tagprefix="ej" %>
<%@ Register assembly="Syncfusion.EJ, Version=17.1460.0.38, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89" namespace="Syncfusion.JavaScript.Models" tagprefix="ej" %>

<ej:Kanban ID="Kanban" runat="server" AllowSelection="false" AllowTitle="true" KeyField="Status">
    <Columns></Columns>
    <Fields Content="Summary" ImageUrl="Image" PrimaryKey="Oid" />
</ej:Kanban>

