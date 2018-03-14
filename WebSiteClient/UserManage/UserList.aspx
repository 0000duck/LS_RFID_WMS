<%@ Page Title="" Language="C#" MasterPageFile="~/MyMaster.master" AutoEventWireup="true"
    CodeFile="UserList.aspx.cs" Inherits="UserManage_User_UserList" %>

<asp:Content ID="Content3" ContentPlaceHolderID="titleName" runat="Server">
     <%=PageTitle %>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table cellpadding="10" cellspacing="0">
                <tr>
                    <td>
                        用户工号
                    </td>
                    <td>
                        <asp:TextBox ID="txtJobNo" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:ImageButton ID="btnSearch" OnClick="btnSearch_Click" ImageUrl="~/Images/search.gif"
                            runat="server" />
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
                <div style="float: left; padding-left: 10px;">
                    备注：管理员可以点击“恢复密码”按钮将用户密码恢复到初始密码，在本系统中初始密码为用户工号</div>
                <div style="float: right; padding-right: 10px;">
                    <a href="UserEdit.aspx?Type=0">新增用户</a></div>
                <br />
                <asp:GridView ID="GridView1" OnRowCommand="GridView1_OnRowCommand" OnRowDataBound="GridView1_OnRowDataBound"
                    DataKeyNames="UserId" runat="server">
                    <Columns>
                        <asp:TemplateField HeaderText="序号" HeaderStyle-Width="60px">
                            <ItemTemplate>
                                <%# Container.DataItemIndex+1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="JobNo" HeaderText="工号"></asp:BoundField>
                        <asp:BoundField DataField="RealName" HeaderText="名字" HeaderStyle-Width="160px"></asp:BoundField>
                        <asp:BoundField DataField="JobDuty" HeaderText="职务" HeaderStyle-Width="140px"></asp:BoundField>
                        <asp:BoundField DataField="UserGroupName" HeaderText="用户组" HeaderStyle-Width="180px">
                        </asp:BoundField>
                        <asp:BoundField DataField="RoleName" HeaderText="网站角色" HeaderStyle-Width="180px">
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="操作" HeaderStyle-Width="150px">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton1" CommandName="ResumePassword" runat="server">恢复密码</asp:LinkButton>
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
