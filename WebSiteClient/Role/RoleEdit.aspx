<%@ Page Title="" Language="C#" MasterPageFile="~/MyMasterSecond.master" AutoEventWireup="true"
    CodeFile="RoleEdit.aspx.cs" Inherits="Role_RoleEdit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script language="javascript" type="text/javascript">
        function rsCancel_onclick() {
            window.close();
            window.location.href = "RoleList.aspx";
        }
    </script>

    <%=PageTitle %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="diveditall">
        <div style="width: 680px">
            <asp:Panel ID="Panel1" runat="server" CssClass="dropShadowPanel">
                <div class="barheader">
                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label></div>
                <hr />
                <div class="content">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <table style="width: 640px;" cellpadding="5">
                                <tr style="height: 10px;">
                                    <td colspan="2">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; width: 25%">
                                        角色名称：
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:TextBox ID="txtRoleName" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvRoleName" runat="server" ErrorMessage="请输入角色名称"
                                            ControlToValidate="txtRoleName"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; width: 25%">
                                        备注：
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" Height="120px" Width="307px"></asp:TextBox>
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
