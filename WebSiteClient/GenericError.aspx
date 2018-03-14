<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GenericError.aspx.cs" Inherits="GenericError" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Css/Common.css" rel="Stylesheet" />
    <script language="javascript" type="text/javascript">
         if (window != top) {                         
             top.location.href = window.location;
         }
     </script> 
</head>
<body style="text-align:center;">
    <form id="form1" runat="server">
    <div class="error">
        <div class="error_msg_div">
            <div class="error_msg">请联系网站管理员</div>
        </div>
    </div><asp:Label ID="lblErrMsg" runat="server" Visible="false"></asp:Label>
    </form>
</body>
</html>
