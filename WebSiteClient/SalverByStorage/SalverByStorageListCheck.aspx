<%@ Page Title="" Language="C#" MasterPageFile="~/MyMaster.master" AutoEventWireup="true"
    CodeFile="SalverByStorageListCheck.aspx.cs" Inherits="SalverByStorage_SalverByStorageListCheck" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content3" ContentPlaceHolderID="titleName" runat="Server">
    <%=PageTitle %>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table cellpadding="10" cellspacing="0">
                <tr>
                    <td>
                        选择核对仓库
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlStorage" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:ImageButton ID="btnSearch" OnClick="btnSearch_Click" ImageUrl="~/Images/search.gif"
                            runat="server" />
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlStorage"
                            Display="Dynamic" ErrorMessage="请选择需要核对的仓库！"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                            <ProgressTemplate>
                                <div style="color: #FF6600">
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <img src="../images/loading.gif" alt="" />
                                            </td>
                                            <td>
                                                &nbsp;正在处理请稍候...
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div class="divtdcontent">
                <asp:GridView ID="GridView1" OnRowDataBound="GridView1_OnRowDataBound" runat="server">
                    <Columns>
                        <%--  <asp:BoundField DataField="serialnum" HeaderText="序号" HeaderStyle-Width="50px"></asp:BoundField>--%>
                        <asp:BoundField DataField="WHName" HeaderText="仓库名称" HeaderStyle-Width="150px"></asp:BoundField>
                        <asp:BoundField DataField="Floor" HeaderText="楼层" HeaderStyle-Width="60px"></asp:BoundField>
                        <asp:BoundField DataField="WPNumber" HeaderText="仓位编号" HeaderStyle-Width="90px">
                        </asp:BoundField>
                        <asp:BoundField DataField="WPName" HeaderText="仓位名称" HeaderStyle-Width="90px"></asp:BoundField>
                        <asp:TemplateField HeaderText="本系统粮食情况(物料号 物料名 目前存量)">
                            <ItemTemplate>
                                <asp:Label ID="lblWPGBaseQty" runat="server" Text=""></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="EAS粮食情况(物料号 物料名 目前存量)">
                            <ItemTemplate>
                                <asp:Label ID="lblWPGBaseQtyEAS" runat="server" Text=""></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
