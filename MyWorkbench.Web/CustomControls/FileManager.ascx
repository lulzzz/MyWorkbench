<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FileManager.ascx.cs" Inherits="MyWorkbench.Web.CustomControls.FileManager" %>
<%@ Register assembly="DevExpress.Web.v19.2, Version=19.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>

<dx:ASPxFileManager ID="ASPxFileManager1" runat="server">
    <Settings RootFolder="~\" ThumbnailFolder="~\Thumb\" AllowedFileExtensions=".jpg,.jpeg,.gif,.rtf,.txt,.avi,.png,.mp3,.xml,.doc,.pdf" />
        <SettingsEditing AllowCreate="true" AllowDelete="true" AllowMove="true" AllowRename="true" AllowCopy="true" AllowDownload="true" />
        <SettingsPermissions>
            <AccessRules>
                <dx:FileManagerFolderAccessRule Path="System" Edit="Deny" />
                <dx:FileManagerFileAccessRule Path="System\*" Download="Deny" />
            </AccessRules>
        </SettingsPermissions>
        <SettingsFileList ShowFolders="true" ShowParentFolder="true" />
        <SettingsBreadcrumbs Visible="true" ShowParentFolderButton="true" Position="Top" />
        <SettingsUpload UseAdvancedUploadMode="true">
            <AdvancedModeSettings EnableMultiSelect="true" />
        </SettingsUpload>
        <SettingsAdaptivity Enabled="true" />
</dx:ASPxFileManager>

