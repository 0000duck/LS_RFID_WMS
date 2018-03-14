<%@ Page Title="" Language="C#" MasterPageFile="~/MyMasterSecond.master" AutoEventWireup="true"
    CodeFile="UserEdit.aspx.cs" Inherits="UserManage_User_UserEdit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script language="javascript" type="text/javascript">

        function rsCancel_onclick() {
            window.close();
            window.location.href = "UserList.aspx";
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
                                        �û����ţ�
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:TextBox ID="txtJobNo" runat="server" Width="128px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvJobNo" runat="server" ErrorMessage="�������û�����" ControlToValidate="txtJobNo"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">
                                        ���֣�
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:TextBox ID="txtRealName" runat="server" Width="128px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">
                                        ְ��
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:TextBox ID="txtJobDuty" runat="server" Width="128px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">
                                        ��ѡ���û��飺
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:DropDownList ID="ddlUserGroup" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlUserGroup_SelectedIndexChanged"
                                            CausesValidation="false" Width="132px">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="ddlUserGroup"
                                            runat="server" ErrorMessage="��ѡ���û��飡"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <asp:Panel ID="panWebRole" Visible="false" runat="server">
                                    <tr>
                                        <td style="text-align: right;">
                                            ��ѡ����վ��ɫ��
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:DropDownList ID="ddlWebRole" runat="server" Width="128px">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="ddlWebRole"
                                                runat="server" ErrorMessage="��ѡ����վ��ɫ��"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                </asp:Panel>
                                <%--  <asp:Panel ID="panPassWord" Visible="false" runat="server">
                                    <tr>
                                        <td style="text-align: right;">
                                            ���룺
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:TextBox ID="txtPassWord" runat="server" TextMode="Password" Width="128px"></asp:TextBox>
                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtPassWord"
                                                runat="server" ErrorMessage="���벻��Ϊ�գ�"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right;">
                                            ȷ�����룺
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:TextBox ID="txtConfirmPwd" runat="server" TextMode="Password" Width="128px"></asp:TextBox>
                                            <asp:CompareValidator ID="CompareValidator1" ControlToValidate="txtPassWord" ControlToCompare="txtConfirmPwd" Operator="Equal" runat="server" ErrorMessage="�������벻һ�£�"></asp:CompareValidator>
                                        </td>
                                    </tr>
                                </asp:Panel>--%>
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
        <br />
        <br />
        <div>
            <asp:Label ID="lblRemark" runat="server" Visible="false">��ע�������û���ƾ��ʼ�����¼����¼�����û���ʱ�޸����룬��ϵͳ���û��ĳ�ʼ����Ϊ�û����ţ�</asp:Label></div>
        <ajaxToolkit:DropShadowExtender ID="DropShadowExtender1" runat="server" TargetControlID="Panel1"
            Width="6" Rounded="true" Radius="6" Opacity=".75" TrackPosition="true" />
    </div>
</asp:Content>
