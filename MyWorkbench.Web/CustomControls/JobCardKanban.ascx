﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="JobCardKanban.ascx.cs" Inherits="MyWorkbench.Web.CustomControls.JobCardKanban" %>
<%@ Register assembly="Syncfusion.EJ.Web, Version=17.1460.0.38, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89" namespace="Syncfusion.JavaScript.Web" tagprefix="ej" %>
<%@ Register assembly="Syncfusion.EJ, Version=17.1460.0.38, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89" namespace="Syncfusion.JavaScript.Models" tagprefix="ej" %>

<ej:Kanban ID="KanbanBoard" runat="server" AllowSelection="false" AllowTitle="true" KeyField="Status">
    <Columns>
        <ej:KanbanColumn HeaderText="Backlog" Key="Open" />
        <ej:KanbanColumn HeaderText="In Progress" Key="InProgress" />
        <ej:KanbanColumn HeaderText="Testing" Key="Testing" />
        <ej:KanbanColumn HeaderText="Done" Key="Close" />
    </Columns>
    <Fields Content="Summary" ImageUrl="ImgUrl" PrimaryKey="Id" />
</ej:Kanban>

