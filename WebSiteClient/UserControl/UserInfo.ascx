<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UserInfo.ascx.cs" Inherits="UserControl_UserInfo" %>
<table>
    <tr>
        <td>
            <asp:Image ID="Image1" ImageUrl="~/Images/user.png" runat="server" />
        </td>
        <td>
            您好:<asp:Label ID="lblUser" runat="server" Text=""></asp:Label>
            &nbsp;欢迎登陆！ &nbsp;&nbsp;
        </td>
        <td>
            <asp:ImageButton ID="ibtnHome" OnClick="ibtnHome_Click"
                CausesValidation="false" ImageUrl="~/Images/home.gif" runat="server" />
        </td>
         <td>
            <asp:ImageButton ID="ibtnPassword" OnClick="ibtnPassword_Click"
                CausesValidation="false" ImageUrl="~/Images/password.gif" runat="server" />
        </td>
        <td>
            <asp:ImageButton ID="ibtnExit" CausesValidation="false" OnClick="ibtnExit_Click"
                ImageUrl="~/Images/exit.gif" runat="server" />
        </td>
    </tr>
</table>
