<%@ Page Title="" Language="C#" MasterPageFile="~/MyMaster.master" AutoEventWireup="true"
    CodeFile="GroupSalver.aspx.cs" Inherits="Work_GroupSalver" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content3" ContentPlaceHolderID="titleName" runat="Server">
    <%=PageTitle %>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
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
                            runat="server" />&nbsp;
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtPlanStartTime"
                            Display="Dynamic" runat="server" ErrorMessage="请选择开始日期！"></asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtPlanEndTime"
                            Display="Dynamic" runat="server" ErrorMessage="请选择结束日期！"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtPlanStartTime"
                            ControlToCompare="txtPlanEndTime" Display="Dynamic" Type="Date" Operator="LessThanEqual"
                            ErrorMessage="开始日期不能大于结束日期！"></asp:CompareValidator>
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
                <asp:Chart ID="chart2" runat="server" BackColor="LightSteelBlue" BackGradientStyle="TopBottom"
                    BackSecondaryColor="White" EnableTheming="False" EnableViewState="True" Height="360px"
                    Width="1010px">
                    <Legends>
                        <asp:Legend Alignment="Center" Docking="Bottom" Name="Legend1" Title="使用">
                        </asp:Legend>
                        <asp:Legend Alignment="Center" Docking="Bottom" Name="Legend2" Title="闲置">
                        </asp:Legend>
                    </Legends>
                    <Titles>
                        <asp:Title Font="微软雅黑, 16pt" Name="Title1" Text="托盘使用情况">
                        </asp:Title>
                    </Titles>
                    <Series>
                        <asp:Series ChartArea="ChartArea1" ChartType="Line" IsValueShownAsLabel="true" Legend="Legend1"
                            Name="使用个数">
                        </asp:Series>
                        <asp:Series ChartArea="ChartArea1" ChartType="Line" IsValueShownAsLabel="true" Legend="Legend2"
                            Name="闲置个数">
                        </asp:Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea BackColor="224, 224, 224" BackGradientStyle="LeftRight" Name="ChartArea1">
                        </asp:ChartArea>
                    </ChartAreas>
                </asp:Chart>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
