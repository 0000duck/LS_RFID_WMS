<%@ Page Title="" Language="C#" MasterPageFile="~/MyMaster.master" AutoEventWireup="true"
    CodeFile="TruckEirList.aspx.cs" Inherits="TruckEir_TruckEirList" %>

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
                    <td colspan="5">
                        <asp:RadioButtonList ID="rdlTETaskType" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="" Selected="True">所有</asp:ListItem>
                            <asp:ListItem Value="1">出库任务</asp:ListItem>
                            <asp:ListItem Value="2">入库任务</asp:ListItem>
                            <asp:ListItem Value="3">库内移库</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td>
                        批号
                    </td>
                    <td>
                        <asp:TextBox ID="txtPRMBLot" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        物料编码
                    </td>
                    <td>
                        <asp:TextBox ID="txtMaterialNumber" runat="server"></asp:TextBox>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        入厂时间
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
                            <asp:BoundField DataField="PRMBNumber" HeaderText="通知单编号"></asp:BoundField>
                            <asp:BoundField DataField="PRMBLot" HeaderText="批号"></asp:BoundField>
                            <asp:BoundField DataField="MaterialNumber" HeaderText="物料编码"></asp:BoundField>
                            <asp:BoundField DataField="MaterialName" HeaderText="物料名称"></asp:BoundField>
                            <asp:TemplateField HeaderText="单据关系">
                                <ItemTemplate>
                                    <%# Common.GetPRMBRelation(Eval("PRMBRelation").ToString().Trim())%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="单据序号">
                                <ItemTemplate>
                                    <%# Eval("PRMBRelation").ToString() == "3" ? Eval("PRMBSerial").ToString() : ""%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="单据属性">
                                <ItemTemplate>
                                    <%# Eval("TEAttribute").ToString().Trim() == "1" ? "出入库" : "移库"%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="任务类型">
                                <ItemTemplate>
                                    <%# Common.GetTETaskType(Eval("TETaskType").ToString().Trim())%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="TEInTime" HeaderText="入厂时间" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}">
                            </asp:BoundField>
                            <asp:BoundField DataField="TEInWeight" HeaderText="入厂称重(KG)" DataFormatString="{0:#.##}">
                            </asp:BoundField>
                            <asp:BoundField DataField="TEInMan" HeaderText="入厂人员"></asp:BoundField>
                            <asp:BoundField DataField="TEOutTime" HeaderText="出厂时间" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}">
                            </asp:BoundField>
                            <asp:BoundField DataField="TEOutWeight" HeaderText="出厂称重(KG)" DataFormatString="{0:#.##}">
                            </asp:BoundField>
                            <asp:BoundField DataField="TEOutMan" HeaderText="出厂人员"></asp:BoundField>
                            <asp:BoundField DataField="TEWHTime" HeaderText="手持机操作时间" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}"
                                HeaderStyle-Width="7%"></asp:BoundField>
                            <asp:BoundField DataField="TEWHMan" HeaderText="手持机操作人员"></asp:BoundField>
                            <asp:BoundField DataField="WHNumber" HeaderText="仓库编码"></asp:BoundField>
                            <asp:BoundField DataField="WHName" HeaderText="仓库名称"></asp:BoundField>
                            <asp:BoundField DataField="WPNumber" HeaderText="仓位编码"></asp:BoundField>
                            <asp:BoundField DataField="WPName" HeaderText="仓位名称"></asp:BoundField>
                            <asp:BoundField DataField="OutWPNumber" HeaderText="移出仓位编码"></asp:BoundField>
                            <asp:BoundField DataField="OutWPName" HeaderText="移出仓位名称"></asp:BoundField>
                            <asp:BoundField DataField="TESalverQty" HeaderText="托盘数量"></asp:BoundField>
                            <asp:BoundField DataField="TEFullQty" HeaderText="满盘包数"></asp:BoundField>
                            <asp:BoundField DataField="TETrailQty" HeaderText="尾盘数量"></asp:BoundField>
                            <asp:BoundField DataField="TECurSalverQty" HeaderText="已执行托盘数量"></asp:BoundField>
                            <asp:TemplateField HeaderText="状态">
                                <ItemTemplate>
                                    <%# Common.GetPRMBState(Eval("TEStatus").ToString().Trim())%>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Fields>
                    </asp:DetailsView>
                </div>
            </div>
            <div class="divtdcontent">
                <asp:GridView ID="GridView1" OnRowCommand="GridView1_OnRowCommand" OnRowDataBound="GridView1_OnRowDataBound"
                    DataKeyNames="TEID" runat="server">
                    <Columns>
                        <asp:BoundField DataField="serialnum" HeaderText="序号" HeaderStyle-Width="50px"></asp:BoundField>
                        <asp:BoundField DataField="PRMBNumber" HeaderText="通知单编号"></asp:BoundField>
                        <asp:BoundField DataField="PRMBLot" HeaderText="批号"></asp:BoundField>
                        <asp:BoundField DataField="MaterialNumber" HeaderText="物料编码"></asp:BoundField>
                        <asp:BoundField DataField="MaterialName" HeaderText="物料名称"></asp:BoundField>
                        <asp:TemplateField HeaderText="单据关系" HeaderStyle-Width="6%">
                            <ItemTemplate>
                                <%# Common.GetPRMBRelation(Eval("PRMBRelation").ToString().Trim())%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="任务类型" HeaderStyle-Width="6%">
                            <ItemTemplate>
                                <%# Common.GetTETaskType(Eval("TETaskType").ToString().Trim())%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="TEInTime" HeaderText="入厂时间" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}"
                            HeaderStyle-Width="12%"></asp:BoundField>
                        <asp:BoundField DataField="TEInWeight" HeaderText="入厂称重(KG)" DataFormatString="{0:#.##}"
                            HeaderStyle-Width="8%"></asp:BoundField>
                        <asp:BoundField DataField="TEOutTime" HeaderText="出厂时间" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}"
                            HeaderStyle-Width="12%"></asp:BoundField>
                        <asp:BoundField DataField="TEOutWeight" HeaderText="出厂称重(KG)" DataFormatString="{0:#.##}"
                            HeaderStyle-Width="8%"></asp:BoundField>
                        <asp:TemplateField HeaderText="状态" HeaderStyle-Width="6%">
                            <ItemTemplate>
                                <%# Common.GetPRMBState(Eval("TEStatus").ToString().Trim())%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="查看" HeaderStyle-Width="50px">
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
