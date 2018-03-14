<%@ Page Title="" Language="C#" MasterPageFile="~/MyMaster.master" AutoEventWireup="true"
    CodeFile="MovePlan.aspx.cs" Inherits="Plan_MovePlan" %>

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
                            <asp:ListItem Value="1">同仓移库</asp:ListItem>
                            <asp:ListItem Value="2">异仓移库</asp:ListItem>
                            <asp:ListItem Value="3">异库移库</asp:ListItem>
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
                            <asp:BoundField DataField="MBNumber" HeaderText="单据编码"></asp:BoundField>
                            <asp:TemplateField HeaderText="单据类型" HeaderStyle-Width="8%">
                                <ItemTemplate>
                                    <%# Common.GetMBTypeName(Eval("MBType").ToString())%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="单据关系">
                                <ItemTemplate>
                                    <%# Common.GetPRMBRelation(Eval("MBRelation").ToString())%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="单据序号">
                                <ItemTemplate>
                                    <%# Eval("MBRelation").ToString() == "3" ? Eval("MBSerial").ToString() : ""%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="MBBizDate" HeaderText="业务日期" DataFormatString="{0:yyyy-MM-dd}">
                            </asp:BoundField>
                            <asp:BoundField DataField="MBFinishDate" HeaderText="业务完成日期" DataFormatString="{0:yyyy-MM-dd}">
                            </asp:BoundField>
                            <asp:BoundField DataField="outSUNumber" HeaderText="调出库存组织编码"></asp:BoundField>
                            <asp:BoundField DataField="outSUName" HeaderText="调出库存组织名称"></asp:BoundField>
                            <asp:BoundField DataField="outWHNumber" HeaderText="调出仓库编码"></asp:BoundField>
                            <asp:BoundField DataField="outWHName" HeaderText="调出仓库名称"></asp:BoundField>
                            <asp:BoundField DataField="outWPNumber" HeaderText="调出仓位编码"></asp:BoundField>
                            <asp:BoundField DataField="outWPName" HeaderText="调出仓位名称"></asp:BoundField>
                            <asp:BoundField DataField="inSUNumber" HeaderText="调入库存组织编码"></asp:BoundField>
                            <asp:BoundField DataField="inSUName" HeaderText="调入库存组织名称"></asp:BoundField>
                            <asp:BoundField DataField="inWHNumber" HeaderText="调入仓库编码"></asp:BoundField>
                            <asp:BoundField DataField="inWHName" HeaderText="调入仓库名称"></asp:BoundField>
                            <asp:BoundField DataField="inWPNumber" HeaderText="调入仓位编码"></asp:BoundField>
                            <asp:BoundField DataField="inWPName" HeaderText="调入仓位名称"></asp:BoundField>
                            <asp:BoundField DataField="MBLot" HeaderText="批号"></asp:BoundField>
                            <asp:BoundField DataField="MBCards" HeaderText="车号"></asp:BoundField>
                            <asp:BoundField DataField="MBSealNum" HeaderText="封条号"></asp:BoundField>
                            <asp:BoundField DataField="MaterialNumber" HeaderText="物料编码"></asp:BoundField>
                            <asp:BoundField DataField="MaterialName" HeaderText="物料名称"></asp:BoundField>
                            <asp:BoundField DataField="MBProductPlace" HeaderText="产地"></asp:BoundField>
                            <asp:BoundField DataField="PRFixedYear" HeaderText="年限" DataFormatString="{0:yyyy-MM-dd}">
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="数量(KG)">
                                <ItemTemplate>
                                    <%# Math.Round(Convert.ToDouble(Eval("MBQty")), 2)%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="基本数量(KG)">
                                <ItemTemplate>
                                    <%# Math.Round(Convert.ToDouble(Eval("MBBaseQty")), 2)%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="辅助数量(KG)">
                                <ItemTemplate>
                                    <%# Math.Round(Convert.ToDouble(Eval("MBAstantQty")), 2)%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="MBReason" HeaderText="移库原因"></asp:BoundField>
                            <asp:BoundField DataField="MBDescription" HeaderText="描述"></asp:BoundField>
                            <asp:TemplateField HeaderText="状态">
                                <ItemTemplate>
                                    <%# Common.GetPRStatus(Eval("MBIsFinished").ToString())%>
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
                        <asp:BoundField DataField="MBNumber" HeaderText="单据编码"></asp:BoundField>
                        <asp:TemplateField HeaderText="单据类型" HeaderStyle-Width="8%">
                            <ItemTemplate>
                                <%# Common.GetMBTypeName(Eval("MBType").ToString())%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="MBBizDate" HeaderText="业务日期" HeaderStyle-Width="10%" DataFormatString="{0:yyyy-MM-dd}">
                        </asp:BoundField>
                        <asp:BoundField DataField="MBFinishDate" HeaderText="业务完成日期" HeaderStyle-Width="10%"
                            DataFormatString="{0:yyyy-MM-dd}"></asp:BoundField>
                        <asp:BoundField DataField="MBLot" HeaderText="批号" HeaderStyle-Width="10%"></asp:BoundField>
                        <asp:BoundField DataField="MaterialNumber" HeaderText="物料编码" HeaderStyle-Width="10%">
                        </asp:BoundField>
                        <asp:BoundField DataField="MaterialName" HeaderText="物料名称" HeaderStyle-Width="15%">
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="单据关系">
                            <ItemTemplate>
                                <asp:Label ID="lblViewRelation" Text='<%# Common.GetPRMBRelation(Eval("MBRelation").ToString())%>'
                                    runat="server"></asp:Label>
                                <asp:LinkButton ID="lnkViewRelation" CommandName="ViewRelation" Text="[查看]" runat="server"></asp:LinkButton>
                                <asp:Label ID="lblMBID" runat="server" Visible="false" Text='<%# Eval("MBID").ToString()%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="状态" HeaderStyle-Width="6%">
                            <ItemTemplate>
                                <%# Common.GetPRStatus(Eval("MBIsFinished").ToString())%>
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
