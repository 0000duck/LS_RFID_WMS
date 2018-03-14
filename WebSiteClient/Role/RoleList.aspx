<%@ Page Title="" Language="C#" MasterPageFile="~/MyMaster.master" AutoEventWireup="true"
    CodeFile="RoleList.aspx.cs" Inherits="Role_RoleList" %>

<asp:Content ID="Content3" ContentPlaceHolderID="titleName" runat="Server">
    <%=PageTitle %>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Always" runat="server">
        <ContentTemplate>
            <table cellpadding="10" cellspacing="0">
                <tr>
                    <td>
                        角色名称：
                    </td>
                    <td>
                        <asp:TextBox ID="txtRoleName" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:ImageButton ID="btnSearch" OnClick="btnSearch_Click" ImageUrl="~/Images/search.gif"
                            runat="server" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Always" runat="server">
        <ContentTemplate>
            <div class="divtdcontent">
                <div style="float: right; padding-right: 10px;">
                    <a href="RoleEdit.aspx?Type=0">新增角色</a></div>
                <br />
                <asp:GridView ID="GridView1" OnRowCommand="GridView1_OnRowCommand" OnRowDataBound="GridView1_OnRowDataBound"
                    DataKeyNames="RoleID" runat="server">
                    <Columns>
                        <asp:TemplateField HeaderText="序号" HeaderStyle-Width="50px">
                            <ItemTemplate>
                                <%# Container.DataItemIndex+1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="RoleName" HeaderText="角色名称" HeaderStyle-Width="15%"></asp:BoundField>
                        <asp:BoundField DataField="Remark" HeaderText="备注"></asp:BoundField>
                        <asp:TemplateField HeaderText="操作" HeaderStyle-Width="160px">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtEnter" CommandName="Enter" runat="server">点击进行授权</asp:LinkButton>
                                <asp:LinkButton ID="lbtEdit" CommandName="Edit" runat="server">编辑</asp:LinkButton>
                                <asp:LinkButton ID="lbtDel" CommandName="Del" OnClientClick="javascript:return confirm('确定删除？');"
                                    runat="server">删除</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <div class="divpage">
                <table>
                    <tr>
                        <td>
                            总记录数：<asp:Label ID="lblRecordNum" runat="server" Text="0"></asp:Label>&nbsp; 目前页/总页数：<asp:Label
                                ID="lblPresentPageNum" runat="server" Text="0"></asp:Label>/<asp:Label ID="lblTotalPageNum"
                                    runat="server" Text="0"></asp:Label>&nbsp;
                            <asp:LinkButton ID="lbtFirstPage" CommandArgument="first" OnCommand="ChangePage_Click"
                                runat="server">首页</asp:LinkButton>&nbsp;
                            <asp:LinkButton ID="lbtPreviousPage" CommandArgument="previous" OnCommand="ChangePage_Click"
                                runat="server">上一页</asp:LinkButton>&nbsp;
                            <asp:LinkButton ID="lbtNextPage" CommandArgument="next" OnCommand="ChangePage_Click"
                                runat="server">下一页</asp:LinkButton>&nbsp;
                            <asp:LinkButton ID="lbtLastPage" CommandArgument="last" OnCommand="ChangePage_Click"
                                runat="server">尾页</asp:LinkButton>&nbsp; 跳转到第<asp:TextBox ID="txtPageNum" CssClass="page_nav_textbox"
                                    runat="server"></asp:TextBox>页
                        </td>
                        <td>
                            <asp:ImageButton ID="btJump" ImageUrl="~/Images/go.jpg" OnClick="btJump_Click" runat="server" />
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
