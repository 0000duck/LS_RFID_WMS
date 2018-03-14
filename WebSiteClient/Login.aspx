<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="_Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" id="login_html">
<head runat="server">
    <title>粮库RFID自动化系统</title>

    <script language="javascript" type="text/javascript">
        if (window != top) {
            top.location.href = window.location;
        }

        window.onload = window.onresize = window.onscroll = function() {
            var doc = document,
                layer = doc.getElementById('divLogin'),
                clientWidth = window.innerWidth/*W3C*/ || doc.documentElement.clientWidth/*std IE*/ || doc.body.clientWidth/*IE quirk mode*/,
                clientHeight = window.innerHeight || doc.documentElement.clientHeight || doc.body.clientHeight;
            layer.style.left = Math.round((clientWidth - layer.offsetWidth) / 2) + 'px';
            layer.style.top = Math.round((clientHeight - layer.offsetHeight) / 2) + 'px';
        };

        function CheckLoginClick() {
            document.getElementById('lblErrMsg').innerText = "";

            var userName = document.getElementById('txtUserName').value;
            var password = document.getElementById('txtPassWord').value;

            if (userName == "") {
                var elevalue = document.createTextNode("请输入用户名!")
                document.getElementById('lblErrMsg').appendChild(elevalue);
                return false;
            }
            if (password == "") {
                var elevalue = document.createTextNode("请输入密码!")
                document.getElementById('lblErrMsg').appendChild(elevalue);
                return false;
            }
            return true;
        }

        function OnFocus(obj) {
            obj.style.backgroundColor = '#ffffcc';
        }

        function OnBlur(obj) {
            obj.style.backgroundColor = '#ffffff';
        }
    </script>

</head>
<body style="background-color: #336aaa;">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="divLogin" style="position: absolute; left: 0px; top: 0px; width: 373px;">
        <table border="0" cellpadding="0" cellspacing="0" width="373px">
            <tr>
                <td>
                    <img src="images/login1.jpg" width="373" height="206" alt="" />
                </td>
            </tr>
            <tr style="height: 74px;">
                <td style="width: 363px; padding-left: 10px; vertical-align: top; background-image: url(images/login2.jpg)">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <table cellpadding="0" cellspacing="0" border="0">
                                <tr style="height: 14px;">
                                    <td>
                                    </td>
                                </tr>
                                <tr style="height: 18px;">
                                    <td>
                                        <table border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <img src="images/username.jpg" width="68" height="18" alt="" />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtUserName" onfocus="OnFocus(this)" onblur="OnBlur(this)" runat="server"
                                                        Width="80px"></asp:TextBox>&nbsp;
                                                </td>
                                                <td>
                                                    <img src="images/password.jpg" width="50" height="18" alt="" />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtPassWord" runat="server" onfocus="OnFocus(this)" onblur="OnBlur(this)"
                                                        Width="80px" TextMode="Password"></asp:TextBox>&nbsp;&nbsp;
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="btnLogin" runat="server" ImageUrl="images/login.jpg" Width="39px"
                                                        Height="18px" OnClick="btnLogin_Click" OnClientClick="javascript:return CheckLoginClick();" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr style="height: 8px;">
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: center; font-family: 微软雅黑; font-weight: bold; font-size: 11pt;">
                                        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                                            <ProgressTemplate>
                                                <div style="float: right; font-family: 幼圆">
                                                    <img src="images/loginbar.gif" alt="" />正在登录...</div>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                        <asp:Label ID="lblErrMsg" runat="server" ForeColor="Red"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 5px;">
                                    <td>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnLogin" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
