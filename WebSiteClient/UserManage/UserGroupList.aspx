<%@ Page Title="" Language="C#" MasterPageFile="~/MyMaster.master" AutoEventWireup="true"
    CodeFile="UserGroupList.aspx.cs" Inherits="UserManage_UserGroup_UserGroupList" %>

<asp:Content ID="Content3" ContentPlaceHolderID="titleName" runat="Server">
     <%=PageTitle %>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table cellpadding="10" cellspacing="0">
                <tr>
                    <td>
                        用户组名称
                    </td>
                    <td>
                        <asp:TextBox ID="txtUserGroupName" runat="server"></asp:TextBox>
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
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div class="divtdcontent">
                <div style="float: right; padding-right: 10px;">
                    <a href="UserGroupEdit.aspx?Type=0">新增用户组</a></div>
                <br />
                <asp:GridView ID="GridView1" OnRowCommand="GridView1_OnRowCommand" OnRowDataBound="GridView1_OnRowDataBound"
                    DataKeyNames="UserGroupID" runat="server">
                    <Columns>
                        <asp:TemplateField HeaderText="序号" HeaderStyle-Width="50px">
                            <ItemStyle Width="50px" />
                            <ItemTemplate>
                                <%# Container.DataItemIndex+1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="userGroupname" HeaderText="用户组名称" HeaderStyle-Width="100px">
                            <ItemStyle Width="100px" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="手持机终端权限" HeaderStyle-Width="15%">
                            <ItemTemplate>
                                <%# Convert.ToBoolean(Eval("hasHandsetRole"))==true?"具备":"不具备" %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="叉车终端权限" HeaderStyle-Width="15%">
                            <ItemTemplate>
                                <%# Convert.ToBoolean(Eval("hasLoadCarRole")) == true ? "具备" : "不具备"%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="门口管理系统终端权限" HeaderStyle-Width="20%">
                            <ItemTemplate>
                                <%# Convert.ToBoolean(Eval("hasGateSysRole")) == true ? "具备" : "不具备"%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="网站终端权限" HeaderStyle-Width="15%">
                            <ItemTemplate>
                                <%# Convert.ToBoolean(Eval("hasWebsiteRole")) == true ? "具备" : "不具备"%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="操作">
                            <ItemTemplate>
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
