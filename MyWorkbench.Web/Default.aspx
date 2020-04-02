<%@ Page Language="C#" AutoEventWireup="true" Inherits="Default" EnableViewState="false"
    ValidateRequest="false" CodeBehind="Default.aspx.cs" %>
<%@ Register Assembly="DevExpress.ExpressApp.Web.v19.2, Version=19.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" 
    Namespace="DevExpress.ExpressApp.Web.Templates" TagPrefix="cc3" %>
<%@ Register Assembly="DevExpress.ExpressApp.Web.v19.2, Version=19.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.ExpressApp.Web.Controls" TagPrefix="cc4" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>Main Page</title>
    <meta http-equiv="Expires" content="0" />
    <link rel="stylesheet" type="text/css" href="http://cdn.syncfusion.com/17.2.0.46/js/web/flat-azure/ej.web.all.min.css" />    
    <script type="text/javascript" src="https://code.jquery.com/jquery-3.3.1.min.js"></script>
    <script type="text/javascript" src="http://cdn.syncfusion.com/js/assets/external/jsrender.min.js"></script>
    <script type="text/javascript" src="http://cdn.syncfusion.com/17.2.0.46/js/web/ej.web.all.min.js"></script>
    <style>
        @media (max-width: 550px) {
            .color-selector-wrapper {
                display: none;
            }
        }
    </style>
</head>
<body class="VerticalTemplate">
    <form id="form2" runat="server">
    <cc4:ASPxProgressControl ID="ProgressControl" runat="server" />
    <div runat="server" id="Content" />
    </form>
    <script>                
        OldOnIFrameLoad = OnIFrameLoad;
        OnIFrameLoad = function (evt) {
            var iframe = evt.currentTarget;
            if (iframe.contentWindow.location != iframe.src) {
                iframe.contentWindow.location = iframe.src;
            } else {
                OldOnIFrameLoad(evt);
            }
        }
    </script>
</body>
</html>
