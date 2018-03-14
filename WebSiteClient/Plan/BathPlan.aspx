<%@ Page Title="" Language="C#" MasterPageFile="~/MyMaster.master" AutoEventWireup="true"
    CodeFile="BathPlan.aspx.cs" Inherits="Plan_BathPlan" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content3" ContentPlaceHolderID="titleName" runat="Server">

    <script type="text/javascript" src="../Js/Window.js"></script>

    <%=PageTitle %>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table cellpadding="5" cellspacing="0">
                <tr>
                    <td>
                        <asp:RadioButtonList ID="rdlPlanType" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Selected="True" Value="">所有通知单</asp:ListItem>
                            <asp:ListItem Value="1">发货通知单</asp:ListItem>
                            <asp:ListItem Value="2">收货通知单</asp:ListItem>
                            <asp:ListItem Value="3">领料出库单</asp:ListItem>
                            <asp:ListItem Value="4">生产入库单</asp:ListItem>
                            <asp:ListItem Value="20">其他出库单</asp:ListItem>
                            <asp:ListItem Value="21">其他入库单</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
            </table>
            <table cellpadding="5" cellspacing="0">
                <tr>
                    <td>
                        单据编码
                    </td>
                    <td>
                        <asp:TextBox ID="txtPRNumber" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        批号
                    </td>
                    <td>
                        <asp:TextBox ID="txtPRLot" runat="server"></asp:TextBox>
                    </td>
                    <td colspan="2">
                    </td>
                </tr>
                <tr>
                    <td>
                        业务日期&nbsp;从
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
            <div id="shadow">
            </div>
            <div id="window" style="width: 420px; height: 560px; top: 80px; left: 360px" class="window"
                onmousedown="Focus(this)">
                <div id="windowtitle" class="windowtitle" onmousedown="Down(this)" onmousemove="Remove(this)"
                    onmouseup="Up(this)">
                    <div style="width: 20px; float: left;">
                        <img src="../images/ie.png" alt="" /></div>
                    <div class="titlename">
                        单据详细信息
                    </div>
                    <div class="windowclose" onclick="Close();">
                        关闭</div>
                </div>
                <div class="windowcontent" style="height: 500px;">
                    <asp:DetailsView ID="DetailsView1" runat="server">
                        <Fields>
                            <asp:BoundField DataField="PRNumber" HeaderText="单据编码"></asp:BoundField>
                            <asp:TemplateField HeaderText="单据类型" HeaderStyle-Width="8%">
                                <ItemTemplate>
                                    <%# Common.GetPRTypeName(Eval("PRType").ToString())%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="单据关系">
                                <ItemTemplate>
                                    <%# Common.GetPRMBRelation(Eval("PRRelation").ToString())%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="单据序号">
                                <ItemTemplate>
                                    <%# Eval("PRRelation").ToString() == "3" ? Eval("PRSerial").ToString() : ""%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="PRBizDate" HeaderText="业务日期" DataFormatString="{0:yyyy-MM-dd}">
                            </asp:BoundField>
                            <asp:BoundField DataField="PRFinishDate" HeaderText="业务完成日期" DataFormatString="{0:yyyy-MM-dd}">
                            </asp:BoundField>
                            <asp:BoundField DataField="PRConsignmentUnitName" HeaderText="发货组织名称"></asp:BoundField>
                            <asp:BoundField DataField="PRConsigneeUnitName" HeaderText="收货组织名称"></asp:BoundField>
                            <asp:BoundField DataField="PRLot" HeaderText="批号"></asp:BoundField>
                            <asp:BoundField DataField="PRCards" HeaderText="车号"></asp:BoundField>
                            <asp:BoundField DataField="PRSealNum" HeaderText="封条号"></asp:BoundField>
                            <asp:BoundField DataField="MaterialNumber" HeaderText="物料编码"></asp:BoundField>
                            <asp:BoundField DataField="MaterialName" HeaderText="物料名称"></asp:BoundField>
                            <asp:BoundField DataField="PRProductPlace" HeaderText="产地"></asp:BoundField>
                            <asp:BoundField DataField="PRFixedYear" HeaderText="年限" DataFormatString="{0:yyyy-MM-dd}">
                            </asp:BoundField>
                            <asp:BoundField DataField="WHNumber" HeaderText="仓库编码"></asp:BoundField>
                            <asp:BoundField DataField="WHName" HeaderText="仓库名称"></asp:BoundField>
                            <asp:BoundField DataField="WPNumber" HeaderText="仓位编码"></asp:BoundField>
                            <asp:BoundField DataField="WPName" HeaderText="仓位名称"></asp:BoundField>
                            <asp:TemplateField HeaderText="数量(KG)">
                                <ItemTemplate>
                                    <%# Math.Round(Convert.ToDouble(Eval("PRQty")), 2)%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="基本数量(KG)">
                                <ItemTemplate>
                                    <%# Math.Round(Convert.ToDouble(Eval("PRBaseQty")),2)%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="辅助数量(KG)">
                                <ItemTemplate>
                                    <%# Math.Round(Convert.ToDouble(Eval("PRAstantQty")), 2)%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="单价(元/KG)">
                                <ItemTemplate>
                                    <%# Math.Round(Convert.ToDouble(Eval("PRPrice")), 2)%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="金额(元)">
                                <ItemTemplate>
                                    <%# Math.Round(Convert.ToDouble(Eval("PRAmount")), 2)%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="PRDescription" HeaderText="描述"></asp:BoundField>
                            <asp:TemplateField HeaderText="状态">
                                <ItemTemplate>
                                    <%# Common.GetPRStatus(Eval("PRIsFinished").ToString())%>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Fields>
                    </asp:DetailsView>
                </div>
                <br />
            </div>
            <div class="divtdcontent">
                <asp:GridView ID="GridView1" OnRowCommand="GridView1_OnRowCommand" OnRowDataBound="GridView1_OnRowDataBound"
                    DataKeyNames="ID" runat="server">
                    <Columns>
                        <asp:TemplateField HeaderText="序号" HeaderStyle-Width="50px">
                            <ItemTemplate>
                                <%# Container.DataItemIndex+1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="PRNumber" HeaderText="单据编码"></asp:BoundField>
                        <asp:TemplateField HeaderText="单据类型" HeaderStyle-Width="8%">
                            <ItemTemplate>
                                <%# Common.GetPRTypeName(Eval("PRType").ToString())%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="PRBizDate" HeaderText="业务日期" HeaderStyle-Width="8%" DataFormatString="{0:yyyy-MM-dd}">
                        </asp:BoundField>
                        <asp:BoundField DataField="PRFinishDate" HeaderText="业务完成日期" HeaderStyle-Width="9%"
                            DataFormatString="{0:yyyy-MM-dd}"></asp:BoundField>
                        <asp:BoundField DataField="PRConsignmentUnitName" HeaderText="发货组织名称" HeaderStyle-Width="8%">
                        </asp:BoundField>
                        <asp:BoundField DataField="PRConsigneeUnitName" HeaderText="收货组织名称" HeaderStyle-Width="8%">
                        </asp:BoundField>
                        <asp:BoundField DataField="PRLot" HeaderText="批号" HeaderStyle-Width="8%"></asp:BoundField>
                        <asp:BoundField DataField="MaterialNumber" HeaderText="物料编码" HeaderStyle-Width="8%">
                        </asp:BoundField>
                        <asp:BoundField DataField="MaterialName" HeaderText="物料名称" HeaderStyle-Width="10%">
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="单据关系" HeaderStyle-Width="8%">
                            <ItemTemplate>
                                <asp:Label ID="lblViewRelation" Text='<%# Common.GetPRMBRelation(Eval("PRRelation").ToString())%>'
                                    runat="server"></asp:Label>
                                <asp:LinkButton ID="lnkViewRelation" CommandName="ViewRelation" Text="[查看]" runat="server"></asp:LinkButton>
                                <asp:Label ID="lblPRID" runat="server" Visible="false" Text='<%# Eval("PRID").ToString()%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="状态" HeaderStyle-Width="6%">
                            <ItemTemplate>
                                <%# Common.GetPRStatus(Eval("PRIsFinished").ToString())%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="查看" HeaderStyle-Width="60px">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkViewDetail" CommandName="ViewDetail" runat="server">详情</asp:LinkButton>
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
