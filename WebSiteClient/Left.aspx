<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Left.aspx.cs" Inherits="Left" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link type="text/css" href="Css/Accordion.css" rel="Stylesheet" />
</head>
<body style="margin: 0px; background-color: #2c5d98; width: 140px; height: 100%;">
    <form id="form1" runat="server">
    <div>
        <ajaxToolkit:ToolkitScriptManager runat="server" ID="ScriptManager1" />
        <ajaxToolkit:Accordion ID="MyAccordion" runat="server" SelectedIndex="0" Width="140px"
            HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
            ContentCssClass="accordionContent" FadeTransitions="false" FramesPerSecond="40"
            TransitionDuration="150" AutoSize="None" RequireOpenedPane="false" SuppressHeaderPostbacks="true">
        </ajaxToolkit:Accordion>
    </div>
    </form>
</body>
</html>
