<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Main.aspx.cs" Inherits="Main" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register Src="UserControl/UserInfo.ascx" TagName="UserInfo" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link type="text/css" href="Css/Common.css" rel="Stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="divcontenttop">
        <div id="divuser">
            <uc1:UserInfo ID="UserInfo1" runat="server" />
        </div>
    </div>
    <div id="divcontent">
        <table>
            <tr>
                <td>
                    <asp:Chart ID="chart1" runat="server" BackColor="LightSteelBlue" BackGradientStyle="TopBottom"
                        BackSecondaryColor="White" EnableTheming="False" EnableViewState="True" Height="360px"
                        Width="1030px">
                        <Legends>
                            <asp:Legend Alignment="Center" Docking="Right" Name="Legend1" Title="入库情况">
                            </asp:Legend>
                            <asp:Legend Alignment="Center" Docking="Right" Name="Legend2" Title="出库情况">
                            </asp:Legend>
                        </Legends>
                        <Titles>
                            <asp:Title Font="微软雅黑, 16pt" Name="Title1" Text="近期粮食出入库情况">
                            </asp:Title>
                        </Titles>
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
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td style="width: 25px;">
                </td>
                <td>
                    <asp:Chart ID="chart2" runat="server" BackColor="LightSteelBlue" BackGradientStyle="TopBottom"
                        BackSecondaryColor="White" EnableTheming="False" EnableViewState="True" Height="240px"
                        Width="320px">
                        <Legends>
                            <asp:Legend IsTextAutoFit="False" BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold">
                            </asp:Legend>
                        </Legends>
                        <Titles>
                            <asp:Title Font="微软雅黑, 14pt" Name="Title1" Text="目前托盘使用情况">
                            </asp:Title>
                        </Titles>
                        <Series>
                            <asp:Series ChartArea="ChartArea1" ChartType="Pie" IsValueShownAsLabel="true" Legend="Legend1"
                                Name="使用情况">
                            </asp:Series>
                        </Series>
                        <ChartAreas>
                            <asp:ChartArea BackColor="224, 224, 224" BackGradientStyle="LeftRight" Name="ChartArea1">
                            </asp:ChartArea>
                        </ChartAreas>
                    </asp:Chart>
                </td>
                <td>
                    <asp:Chart ID="chart3" runat="server" BackColor="LightSteelBlue" BackGradientStyle="TopBottom"
                        BackSecondaryColor="White" EnableTheming="False" EnableViewState="True" Height="240px"
                        Width="320px">
                        <Legends>
                            <asp:Legend IsTextAutoFit="False" BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold">
                            </asp:Legend>
                        </Legends>
                        <Titles>
                            <asp:Title Font="微软雅黑, 14pt" Name="Title1" Text="目前装卸点使用情况">
                            </asp:Title>
                        </Titles>
                        <Series>
                            <asp:Series ChartArea="ChartArea1" ChartType="Pie" IsValueShownAsLabel="true" Legend="Legend1"
                                Name="使用情况">
                            </asp:Series>
                        </Series>
                        <ChartAreas>
                            <asp:ChartArea BackColor="224, 224, 224" BackGradientStyle="LeftRight" Name="ChartArea1">
                            </asp:ChartArea>
                        </ChartAreas>
                    </asp:Chart>
                </td>
                <td>
                    <asp:Chart ID="chart4" runat="server" BackColor="LightSteelBlue" BackGradientStyle="TopBottom"
                        BackSecondaryColor="White" EnableTheming="False" EnableViewState="True" Height="240px"
                        Width="320px">
                        <Legends>
                            <asp:Legend IsTextAutoFit="False" BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold">
                            </asp:Legend>
                        </Legends>
                        <Titles>
                            <asp:Title Font="微软雅黑, 14pt" Name="Title1" Text="目前叉车使用情况">
                            </asp:Title>
                        </Titles>
                        <Series>
                            <asp:Series ChartArea="ChartArea1" ChartType="Pie" IsValueShownAsLabel="true" Legend="Legend1"
                                Name="使用情况">
                            </asp:Series>
                        </Series>
                        <ChartAreas>
                            <asp:ChartArea BackColor="224, 224, 224" BackGradientStyle="LeftRight" Name="ChartArea1">
                            </asp:ChartArea>
                        </ChartAreas>
                    </asp:Chart>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
