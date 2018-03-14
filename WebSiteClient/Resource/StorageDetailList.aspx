<%@ Page Title="" Language="C#" MasterPageFile="~/MyMaster.master" AutoEventWireup="true"
    CodeFile="StorageDetailList.aspx.cs" Inherits="Resource_StorageDetail_StorageDetailList" %>

<asp:Content ID="Content3" ContentPlaceHolderID="titleName" runat="Server">
   <%=PageTitle %>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table cellpadding="10" cellspacing="0">
                <tr>
                    <td>
                        仓位编码
                    </td>
                    <td>
                        <asp:TextBox ID="txtStorageCode" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        所属仓库
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
                <asp:GridView ID="GridView1" OnRowDataBound="GridView1_OnRowDataBound" DataKeyNames="Id"
                    runat="server">
                    <Columns>
                        <asp:BoundField DataField="serialnum" HeaderText="序号" HeaderStyle-Width="50px"></asp:BoundField>
                        <asp:BoundField DataField="WPNumber" HeaderText="仓位编码"></asp:BoundField>
                        <asp:BoundField DataField="WPName" HeaderText="仓位名称"></asp:BoundField>
                        <asp:BoundField DataField="WHNumber" HeaderText="所属仓库编码"></asp:BoundField>
                        <asp:BoundField DataField="WHName" HeaderText="所属仓库名称"></asp:BoundField>
                        <asp:BoundField DataField="Floor" HeaderText="所属楼层" HeaderStyle-Width="10%"></asp:BoundField>
                        <asp:BoundField DataField="Area" HeaderText="所属区域" HeaderStyle-Width="10%"></asp:BoundField>
                        <asp:BoundField DataField="Number" HeaderText="堆位号" HeaderStyle-Width="10%"></asp:BoundField>
                        <asp:TemplateField HeaderText="状况" HeaderStyle-Width="8%">
                            <ItemTemplate>
                                <%# Eval("WPUsedStaus").ToString() == "0" ? "停用" : "正常"%>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="目前存粮(吨)" HeaderStyle-Width="10%">
                            <ItemTemplate>
                                <%# Eval("WPGBaseQty") is DBNull ? 0 : Math.Round(Convert.ToDouble(Eval("WPGBaseQty"))/1000,3)%>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <div>
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
