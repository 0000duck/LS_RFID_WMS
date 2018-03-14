<%@ Page Title="" Language="C#" MasterPageFile="~/MyMaster.master" AutoEventWireup="true"
    CodeFile="TruckEirGroup.aspx.cs" Inherits="TruckEir_TruckEirGroup" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content3" ContentPlaceHolderID="titleName" runat="Server">
    <%=PageTitle %>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <br />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <ajaxToolkit:TabContainer runat="server" ID="TabContainer1">
                <ajaxToolkit:TabPanel runat="server" ID="TabPanel1" HeaderText="按日期统计">
                    <ContentTemplate>
                        <table cellpadding="5" cellspacing="0">
                            <tr>
                                <td>
                                    选择日期
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPlanStartTime" SkinID="DataSkin" runat="server"></asp:TextBox>
                                    <cc1:CalendarExtender ID="txtPlanStartTime_CalendarExtender" runat="server" Enabled="True"
                                        CssClass="MyCalendar" TargetControlID="txtPlanStartTime" Format="yyyy-MM-dd">
                                    </cc1:CalendarExtender>
                                    到
                                    <asp:TextBox ID="txtPlanEndTime" SkinID="DataSkin" runat="server"></asp:TextBox>
                                    <cc1:CalendarExtender ID="txtPlanEndTime_CalendarExtender" runat="server" Enabled="True"
                                        CssClass="MyCalendar" TargetControlID="txtPlanEndTime" Format="yyyy-MM-dd">
                                    </cc1:CalendarExtender>
                                    &nbsp;
                                </td>
                                <td colspan="2">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtPlanStartTime"
                                        Display="Dynamic" runat="server" ErrorMessage="请选择开始日期！"></asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtPlanEndTime"
                                        Display="Dynamic" runat="server" ErrorMessage="请选择结束日期！"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtPlanStartTime"
                                        ControlToCompare="txtPlanEndTime" Display="Dynamic" Type="Date" Operator="LessThanEqual"
                                        ErrorMessage="开始日期不能大于结束日期！"></asp:CompareValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    选择粮食物料
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlMaterialName" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td colspan="2">
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    选择单位
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlStorage" AutoPostBack="true" OnSelectedIndexChanged="ddlStorage_OnSelectedIndexChanged"
                                        runat="server">
                                    </asp:DropDownList>
                                    &nbsp;
                                    <asp:DropDownList ID="ddlFloor" AutoPostBack="true" OnSelectedIndexChanged="ddlFloor_OnSelectedIndexChanged"
                                        runat="server">
                                    </asp:DropDownList>
                                    &nbsp;
                                    <asp:DropDownList ID="ddlStorageDetail" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:ImageButton ID="btnSearch" OnClick="btnSearch_Click" ImageUrl="~/Images/search.gif"
                                        runat="server" />
                                </td>
                                <td>
                                    <asp:UpdateProgress ID="UpdateProgress2" runat="server">
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
                </ajaxToolkit:TabPanel>
                <ajaxToolkit:TabPanel runat="server" ID="TabPanel2" HeaderText="按月份统计">
                    <ContentTemplate>
                        <table cellpadding="5" cellspacing="0">
                            <tr>
                                <td>
                                    选择年度
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlYear" Width="130px" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    选择粮食物料
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlMaterialNameM" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    选择单位
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlStorageM" AutoPostBack="true" OnSelectedIndexChanged="ddlStorageM_OnSelectedIndexChanged"
                                        runat="server">
                                    </asp:DropDownList>
                                    &nbsp;
                                    <asp:DropDownList ID="ddlFloorM" AutoPostBack="true" OnSelectedIndexChanged="ddlFloorM_OnSelectedIndexChanged"
                                        runat="server">
                                    </asp:DropDownList>
                                    &nbsp;
                                    <asp:DropDownList ID="ddlStorageDetailM" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:ImageButton ID="ibtnSearch1" OnCommand="ibtnSearch_Click" CommandName="month"
                                        CausesValidation="false" ImageUrl="~/Images/search.gif" runat="server" />
                                </td>
                                <td>
                                    <asp:UpdateProgress ID="UpdateProgress3" runat="server">
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
                </ajaxToolkit:TabPanel>
                <ajaxToolkit:TabPanel runat="server" ID="TabPanel3" HeaderText="按年份统计">
                    <ContentTemplate>
                        <table cellpadding="5" cellspacing="0">
                            <tr>
                                <td>
                                    选择年份
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlYearBegin" runat="server">
                                    </asp:DropDownList>
                                    到
                                    <asp:DropDownList ID="ddlYearEnd" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddlYearBegin"
                                        ControlToCompare="ddlYearEnd" Display="Dynamic" Type="Integer" Operator="LessThanEqual"
                                        ErrorMessage="开始年份不能大于结束年份！"></asp:CompareValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    选择粮食物料
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlMaterialNameY" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    选择单位
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlStorageY" AutoPostBack="true" OnSelectedIndexChanged="ddlStorageY_OnSelectedIndexChanged"
                                        runat="server">
                                    </asp:DropDownList>
                                    &nbsp;
                                    <asp:DropDownList ID="ddlFloorY" AutoPostBack="true" OnSelectedIndexChanged="ddlFloorY_OnSelectedIndexChanged"
                                        runat="server">
                                    </asp:DropDownList>
                                    &nbsp;
                                    <asp:DropDownList ID="ddlStorageDetailY" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:ImageButton ID="ibtnSearch2" OnCommand="ibtnSearch_Click" CommandName="year"
                                         ImageUrl="~/Images/search.gif" runat="server" />
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
                </ajaxToolkit:TabPanel>
            </ajaxToolkit:TabContainer>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="divtdcontent">
        <div id="divtoexcel" style="display: none; float: right; padding-left: 10px;">
            <asp:LinkButton ID="ltnToExcle" OnClick="ltnToExcle_OnClick" Text="导出Excel文档" runat="server"></asp:LinkButton></div>
        <br />
        <br />
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <asp:Panel ID="Panel1" runat="server">
                    <asp:GridView ID="GridView1" EmptyDataText="暂无记录!" Width="98%" runat="server">
                        <Columns>
                            <asp:BoundField DataField="opardate" HeaderText="日期" HeaderStyle-Width="20%" DataFormatString="{0:yyyy-MM-dd}" />
                            <asp:BoundField DataField="inTotalWeight" HeaderText="入库数量（单位：吨）" />
                            <asp:BoundField DataField="outTotalWeight" HeaderText="出库数量（单位：吨）" />
                        </Columns>
                    </asp:GridView>
                </asp:Panel>
                <asp:Panel ID="Panel2" Visible="false" runat="server">
                    <asp:Chart ID="chart2" runat="server" BackColor="LightSteelBlue" BackGradientStyle="TopBottom"
                        BackSecondaryColor="White" EnableTheming="False" EnableViewState="True" Height="360px"
                        Width="1010px">
                        <Legends>
                            <asp:Legend Alignment="Center" Docking="Bottom" Name="Legend1" Title="入库情况">
                            </asp:Legend>
                            <asp:Legend Alignment="Center" Docking="Bottom" Name="Legend2" Title="出库情况">
                            </asp:Legend>
                        </Legends>
                        <Series>
                            <asp:Series ChartArea="ChartArea1" ChartType="Column" IsValueShownAsLabel="true"
                                Legend="Legend1" Name="入库量">
                            </asp:Series>
                            <asp:Series ChartArea="ChartArea1" ChartType="Column" IsValueShownAsLabel="true"
                                Legend="Legend2" Name="出库量">
                            </asp:Series>
                        </Series>
                        <ChartAreas>
                            <asp:ChartArea BackColor="224, 224, 224" BackGradientStyle="LeftRight" Name="ChartArea1">
                            </asp:ChartArea>
                        </ChartAreas>
                    </asp:Chart>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
