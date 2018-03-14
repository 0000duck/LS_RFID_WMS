<%@ Page Title="" Language="C#" MasterPageFile="~/MyMasterSecond.master" AutoEventWireup="true"
    CodeFile="GroupSalverByStorage.aspx.cs" Inherits="SalverByStorage_GroupSalverByStorage" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%=PageTitle %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <table id="table_main_outer">
            <tr style="height: 3px;">
                <td>
                </td>
            </tr>
            <tr>
                <td style="text-align: center;">
                    <table id="table_main_gridview">
                        <tr>
                            <td>
                                <asp:Chart ID="chart1" runat="server" BackColor="LightSteelBlue" BackGradientStyle="TopBottom"
                                    BackSecondaryColor="White" EnableTheming="False" EnableViewState="True" Height="360px"
                                    Width="500px">
                                    <Legends>
                                        <asp:Legend IsTextAutoFit="False" BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold">
                                        </asp:Legend>
                                    </Legends>
                                    <Titles>
                                        <asp:Title Font="微软雅黑, 16pt" Name="Title1" Text="目前粮食存量按物料分布（单位：吨）">
                                        </asp:Title>
                                    </Titles>
                                    <Series>
                                        <asp:Series ChartArea="ChartArea1" ChartType="Pie" IsValueShownAsLabel="true" Legend="Legend1"
                                            Name="使用数量">
                                        </asp:Series>
                                    </Series>
                                    <ChartAreas>
                                        <asp:ChartArea BackColor="224, 224, 224" BackGradientStyle="LeftRight" Name="ChartArea1">
                                        </asp:ChartArea>
                                    </ChartAreas>
                                </asp:Chart>
                            </td>
                            <td>
                                <asp:Chart ID="chart2" runat="server" BackColor="LightSteelBlue" BackGradientStyle="TopBottom"
                                    BackSecondaryColor="White" EnableTheming="False" EnableViewState="True" Height="360px"
                                    Width="500px">
                                    <Legends>
                                        <asp:Legend IsTextAutoFit="False" BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold">
                                        </asp:Legend>
                                    </Legends>
                                    <Titles>
                                        <asp:Title Font="微软雅黑, 16pt" Name="Title1" Text="目前粮食存量按仓库分布（单位：吨）">
                                        </asp:Title>
                                    </Titles>
                                    <Series>
                                        <asp:Series ChartArea="ChartArea1" ChartType="Pie" IsValueShownAsLabel="true" Legend="Legend1"
                                            IsVisibleInLegend="true" Name="使用数量">
                                        </asp:Series>
                                    </Series>
                                    <ChartAreas>
                                        <asp:ChartArea BackColor="224, 224, 224" BackGradientStyle="LeftRight" Name="ChartArea1">
                                        </asp:ChartArea>
                                    </ChartAreas>
                                </asp:Chart>
                            </td>
                        </tr>
                        <tr style="height: 3px;">
                            <td>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
