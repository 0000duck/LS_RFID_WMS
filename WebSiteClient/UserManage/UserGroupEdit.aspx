<%@ Page Title="" Language="C#" MasterPageFile="~/MyMasterSecond.master" AutoEventWireup="true"
    CodeFile="UserGroupEdit.aspx.cs" Inherits="UserManage_UserGroup_UserGroupEdit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script language="javascript" type="text/javascript">


        function rsCancel_onclick() {
            window.close();
            window.location.href = "UserGroupList.aspx";
        }
    </script>

    <%=PageTitle %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="diveditall">
        <div style="width: 680px">
            <asp:Panel ID="Panel1" runat="server" CssClass="dropShadowPanel">
                <div class="barheader">
                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label></div>
                <hr />
                <div class="content">
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <table style="width: 670px;" cellpadding="5">
                                <tr style="height: 10px;">
                                    <td colspan="2">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; width: 40%">
                                        用户组名称：
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:TextBox ID="txtUserGroupName" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvUserGroupName" runat="server" ErrorMessage="请输入用户组名称"
                                            ControlToValidate="txtUserGroupName"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">
                                        是否具有手持机终端权限：
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:RadioButtonList ID="rblHasHangsetRole" RepeatDirection="Horizontal" runat="server">
                                            <asp:ListItem Value="True" Selected="True">是</asp:ListItem>
                                            <asp:ListItem Value="False">否</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">
                                        是否具有叉车终端权限：
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:RadioButtonList ID="rblHasLoadCarRole" RepeatDirection="Horizontal" runat="server">
                                            <asp:ListItem Value="true" Selected="True">是</asp:ListItem>
                                            <asp:ListItem Value="false">否</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">
                                        是否具有门口管理系统终端权限：
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:RadioButtonList ID="rblHasGateSysRole" RepeatDirection="Horizontal" runat="server">
                                            <asp:ListItem Value="true" Selected="True">是</asp:ListItem>
                                            <asp:ListItem Value="false">否</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">
                                        是否具有网站终端权限：
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:RadioButtonList ID="rblHasWebsiteRole" RepeatDirection="Horizontal" runat="server">
                                            <asp:ListItem Value="true" Selected="True">是</asp:ListItem>
                                            <asp:ListItem Value="false">否</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr style="height: 15px;">
                                    <td colspan="2">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="text-align: center;">
                                        <asp:ImageButton ID="btnConfrim" OnClick="btnConfrim_Click" ImageUrl="~/Images/confirm.gif"
                                            runat="server" />&nbsp;&nbsp;&nbsp;
                                        <asp:ImageButton ID="btnCannel" OnClientClick="return rsCancel_onclick()" CausesValidation="false"
                                            ImageUrl="~/Images/cannel.gif" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnConfrim" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </asp:Panel>
        </div>
        <ajaxToolkit:DropShadowExtender ID="DropShadowExtender1" runat="server" TargetControlID="Panel1"
            Width="6" Rounded="true" Radius="6" Opacity=".75" TrackPosition="true" />
    </div>
</asp:Content>
