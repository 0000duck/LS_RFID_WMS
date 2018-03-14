<%@ Page Title="" Language="C#" MasterPageFile="~/MyMaster.master" AutoEventWireup="true"
    CodeFile="WorkItemList.aspx.cs" Inherits="Work_WorkItemList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content3" ContentPlaceHolderID="titleName" runat="Server">
    <%=PageTitle %>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table cellpadding="5" cellspacing="0">
                <tr>
                    <td colspan="2">
                        <asp:RadioButtonList ID="rdlWorkType" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Selected="True" Text="所有" Value=""></asp:ListItem>
                            <asp:ListItem Selected="False" Text="入库" Value="2"></asp:ListItem>
                            <asp:ListItem Selected="False" Text="出库" Value="1"></asp:ListItem>
                            <asp:ListItem Selected="False" Text="移库" Value="3"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        批次号
                    </td>
                    <td>
                        <asp:TextBox ID="txtPRMBlot" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        用户工号
                    </td>
                    <td>
                        <asp:TextBox ID="txtJobNo" runat="server"></asp:TextBox>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        工作时间
                    </td>
                    <td>
                        <asp:TextBox ID="txtPlanStartTime" SkinID="DataSkin" runat="server"></asp:TextBox>
                        <cc1:CalendarExtender ID="txtPlanStartTime_CalendarExtender" runat="server" Enabled="True"
                            CssClass="MyCalendar" TargetControlID="txtPlanStartTime" Format="yyyy-MM-dd">
                        </cc1:CalendarExtender>
                    </td>
                    <td>
                        到
                    </td>
                    <td>
                        <asp:TextBox ID="txtPlanEndTime" SkinID="DataSkin" runat="server"></asp:TextBox>
                    </td>
                    <cc1:CalendarExtender ID="txtPlanEndTime_CalendarExtender" runat="server" Enabled="True"
                        CssClass="MyCalendar" TargetControlID="txtPlanEndTime" Format="yyyy-MM-dd">
                    </cc1:CalendarExtender>
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
                <asp:GridView ID="GridView1" OnRowDataBound="GridView1_OnRowDataBound" DataKeyNames="serialnum"
                    runat="server">
                    <Columns>
                        <asp:BoundField DataField="serialnum" HeaderText="序号" HeaderStyle-Width="50px"></asp:BoundField>
                        <asp:BoundField DataField="JobNo" HeaderText="用户工号" HeaderStyle-Width="8%"></asp:BoundField>
                        <asp:BoundField DataField="RealName" HeaderText="用户姓名" HeaderStyle-Width="8%"></asp:BoundField>
                        <asp:BoundField DataField="PRMBlot" HeaderText="任务批号" HeaderStyle-Width="15%"></asp:BoundField>
                        <asp:TemplateField HeaderText="任务类型" HeaderStyle-Width="10%">
                            <ItemTemplate>
                                <%# GetWorkType(Eval("TETaskType").ToString().Trim())%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="WHName" HeaderText="仓库" HeaderStyle-Width="10%"></asp:BoundField>
                        <asp:BoundField DataField="WPName" HeaderText="仓位" HeaderStyle-Width="10%"></asp:BoundField>
                        <asp:BoundField DataField="OperNum" HeaderText="叉送数量(次)"></asp:BoundField>
                        <asp:BoundField DataField="OpTime" HeaderText="叉送时间"></asp:BoundField>
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
