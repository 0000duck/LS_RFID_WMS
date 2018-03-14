<%@ Page Title="" Language="C#" MasterPageFile="~/MyMasterSecond.master" AutoEventWireup="true"
    CodeFile="Password.aspx.cs" Inherits="UserManage_Password" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script language="javascript" type="text/javascript">

        function rsCancel_onclick() {
            window.close();
            window.location.href = "UserGroupList.aspx";
        }
    </script>

    密码修改
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="diveditall">
        <div style="width: 680px">
            <asp:Panel ID="Panel1" runat="server" CssClass="dropShadowPanel">
                <div class="barheader">
                    修改密码</div>
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
                                        原密码：
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:TextBox ID="txtOldPassword" TextMode="Password" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="请输入原密码！"
                                            ControlToValidate="txtOldPassword"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; width: 40%">
                                        新密码：
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:TextBox ID="txtNewPassword" TextMode="Password" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="请输入新密码！"
                                            ControlToValidate="txtNewPassword"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; width: 40%">
                                        确认新密码：
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:TextBox ID="txtNewPassordAgain" TextMode="Password" runat="server"></asp:TextBox>
                                        <asp:CompareValidator ID="CompareValidator1" ControlToValidate="txtNewPassword" ControlToCompare="txtNewPassordAgain"
                                            Operator="Equal" runat="server" ErrorMessage="两次密码不一致！"></asp:CompareValidator>
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
