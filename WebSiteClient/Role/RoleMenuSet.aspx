<%@ Page Title="" Language="C#" MasterPageFile="~/MyMasterSecond.master" AutoEventWireup="true"
    CodeFile="RoleMenuSet.aspx.cs" Inherits="Role_RoleMenuSet" %>

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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
                            <fieldset style="width: 600px;">
                                <table>
                                    <tr style="height: 10px;">
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left; padding-left: 15px;">
                                            <asp:TreeView ID="trvMenu" ShowLines="true" ShowCheckBoxes="Leaf" runat="server"
                                                ExpandDepth="1">
                                            </asp:TreeView>
                                        </td>
                                    </tr>
                                    <tr style="height: 15px;">
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: center;">
                                            <asp:Button ID="btnConfrim" runat="server" OnClick="btnConfrim_Click" Text="授权" Width="64px" />&nbsp;
                                            <input id="rsCancel" type="reset" value="取消" onclick="return rsCancel_onclick()" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnConfrim" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </asp:Panel>
            <br />
        </div>
        <%--  <ajaxToolkit:DropShadowExtender ID="DropShadowExtender1" runat="server" TargetControlID="Panel1"
            Width="6" Rounded="true" Radius="6" Opacity=".75" TrackPosition="true" />--%>
    </div>
</asp:Content>
