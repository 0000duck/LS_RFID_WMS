<%@ Page Title="" Language="C#" MasterPageFile="~/MyMasterSecond.master" AutoEventWireup="true"
    CodeFile="GroupStorageInfo.aspx.cs" Inherits="SalverByStorage_GroupStorageInfo" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" src="../Js/WindowDouble.js"></script>

    <%=PageTitle %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="divcontentmenu">
                <div class="barheader">
                    <div style="width: 20px; float: left;">
                        <img src="../images/builing.png" alt="" /></div>
                    <div class="titlename">
                        粮库结构图
                    </div>
                </div>
                <div class="content">
                    <asp:TreeView ID="TreeView1" OnSelectedNodeChanged="TreeView1_OnSelectedNodeChanged"
                        runat="server">
                    </asp:TreeView>
                    <br />
                </div>
            </div>
            <div id="comment1" class="comment">
                <div id="windowtitle1" class="windowtitle" onmousedown="Down(this)" onmousemove="Remove(this)"
                    onmouseup="Up(this)">
                    <div style="width: 20px; float: left;">
                        <img src="../images/ie.png" alt="" /></div>
                    <div class="titlename">
                        粮食情况
                    </div>
                    <div class="windowclose" onclick="Close('comment1',220,75);">
                        关闭</div>
                </div>
                <div class="divwindowscontent">
                    <asp:Panel ID="Panel4" runat="server">
                        <div class="barheader" style="cursor: pointer;">
                            <div style="float: left;">
                                目前存粮情况</div>
                            <div style="float: right; vertical-align: middle;">
                                <asp:ImageButton ID="Image2" runat="server" ImageUrl="../images/expand_blue.jpg"
                                    AlternateText="(展开...)" />&nbsp;&nbsp;&nbsp;
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="Panel3" runat="server">
                        <div class="content">
                            <asp:Repeater ID="Repeater3" runat="server">
                                <HeaderTemplate>
                                    <table class="linetable">
                                        <tr>
                                            <%--    <td style="width: 120px">
                                                批号
                                            </td>--%>
                                            <td style="width: 180px">
                                                物料编码
                                            </td>
                                            <td style="width: 220px">
                                                物料名称
                                            </td>
                                            <td style="width: 160px">
                                                重量(吨)
                                            </td>
                                        </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <%-- <td>
                                            <%# Eval("WPGLot").ToString()%>
                                        </td>--%>
                                        <td>
                                            <%# Eval("MaterialNumber").ToString()%>
                                        </td>
                                        <td>
                                            <%# Eval("MaterialName").ToString()%>
                                        </td>
                                        <td>
                                            <%# Math.Round(Convert.ToDouble(Eval("WPGBaseQty") is DBNull ? 0 : Eval("WPGBaseQty"))/1000, 3)%>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </table>
                                </FooterTemplate>
                            </asp:Repeater>
                        </div>
                    </asp:Panel>
                    <ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender2" runat="Server"
                        TargetControlID="Panel3" ExpandControlID="Panel4" CollapseControlID="Panel4"
                        Collapsed="false" ImageControlID="Image2" ExpandedText="(隐藏...)" CollapsedText="(展开...)"
                        ExpandedImage="../images/collapse_blue.jpg" CollapsedImage="../images/expand_blue.jpg"
                        SuppressPostBack="true" />
                    <br />
                    <asp:Panel ID="Panel2" runat="server">
                        <div class="barheader" style="cursor: pointer;">
                            <div style="float: left;">
                                近期出入库情况</div>
                            <div style="float: right; vertical-align: middle;">
                                <asp:ImageButton ID="Image1" runat="server" ImageUrl="../images/expand_blue.jpg"
                                    AlternateText="(展开...)" />&nbsp;&nbsp;&nbsp;
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="Panel1" runat="server">                    </asp:Panel>
                        <div class="content">
                            <asp:Chart ID="chart1" runat="server" BackGradientStyle="TopBottom" BackSecondaryColor="White"
                                EnableTheming="False" EnableViewState="True" Height="360px" Width="720px">
                                <Legends>
                                    <asp:Legend Alignment="Center" Docking="Bottom" Name="Legend1" Title="入库情况">
                                    </asp:Legend>
                                    <asp:Legend Alignment="Center" Docking="Bottom" Name="Legend2" Title="出库情况">
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
                        </div>

                    <br />
                    <ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="Server"
                        TargetControlID="Panel1" ExpandControlID="Panel2" CollapseControlID="Panel2"
                        Collapsed="false" ImageControlID="Image1" ExpandedText="(隐藏...)" CollapsedText="(展开...)"
                        ExpandedImage="../images/collapse_blue.jpg" CollapsedImage="../images/expand_blue.jpg"
                        SuppressPostBack="true" />
                    <asp:Panel ID="Panel5" runat="server">
                        <div class="barheader" style="cursor: pointer;">
                            <div style="float: left;">
                                基本信息</div>
                            <div style="float: right; vertical-align: middle;">
                                <asp:ImageButton ID="Image3" runat="server" ImageUrl="../images/collapse_blue.jpg"
                                    AlternateText="(隐藏...)" />&nbsp;&nbsp;&nbsp;
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="Panel6" runat="server">
                        <div class="content">
                            <asp:Literal ID="lilInfo" runat="server"></asp:Literal>
                        </div>
                    </asp:Panel>
                    <ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender3" runat="Server"
                        TargetControlID="Panel6" ExpandControlID="Panel5" CollapseControlID="Panel5"
                        Collapsed="true" ImageControlID="Image3" ExpandedText="(隐藏...)" CollapsedText="(展开...)"
                        ExpandedImage="../images/collapse_blue.jpg" CollapsedImage="../images/expand_blue.jpg"
                        SuppressPostBack="true" />
                    <br />
                </div>
            </div>
            <div id="comment2" class="comment">
                <div id="windowtitle2" class="windowtitle" onmousedown="Down(this)" onmousemove="Remove(this)"
                    onmouseup="Up(this)">
                    <div style="width: 20px; float: left;">
                        <img src="../images/ie.png" alt="" /></div>
                    <div class="titlename">
                        平面布局
                    </div>
                    <div class="windowclose" onclick="Close('comment2',220,75);">
                        关闭</div>
                </div>
                <div class="divwindowscontent">
                    <div style="width: 45%; float: left;">
                        <div class="barheader">
                            A区</div>
                        <div class="content">
                            <asp:Repeater ID="Repeater1" OnItemCommand="Repeater_OnItemCommand" runat="server">
                                <HeaderTemplate>
                                    <table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td onmouseover="Show('<%#Eval("WPID").ToString()%>')" onmouseout="Hide('<%#Eval("WPID").ToString()%>')">
                                            <asp:ImageButton ID="iLbtShow" CommandName="Show" CommandArgument='<%#Eval("WPID").ToString()%>'
                                                ImageUrl='<%#Convert.ToDouble(Eval("totalWeight") is DBNull ? 0 : Eval("totalWeight"))==0?"~/Images/grain.jpg":"~/Images/grainon.jpg" %>'
                                                runat="server" /><br />
                                            <%#Eval("WPName").ToString()%><br />
                                            <br />
                                        </td>
                                        <td style="width: 5px;">
                                        </td>
                                        <td>
                                            <div id='<%#Eval("WPID").ToString()%>' style="display: none;">
                                                仓位编码：<%#Eval("WPNumber").ToString()%><br />
                                                仓位名称：<%#Eval("WPName").ToString()%><br />
                                                堆位号：<%#Eval("Number").ToString()%><br />
                                                目前存放粮食总量(吨):<%# Math.Round(Convert.ToDouble(Eval("totalWeight") is DBNull ? 0 : Eval("totalWeight"))/1000,2)%>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr style="height: 15px;">
                                        <td colspan="2">
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </table></FooterTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                    <div style="width: 45%; float: right;">
                        <div class="barheader">
                            B区</div>
                        <div class="content">
                            <asp:Repeater ID="Repeater2" OnItemCommand="Repeater_OnItemCommand" runat="server">
                                <HeaderTemplate>
                                    <table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td onmouseover="Show('<%#Eval("WPID").ToString()%>')" onmouseout="Hide('<%#Eval("WPID").ToString()%>')">
                                            <asp:ImageButton ID="iLbtShow" CommandName="Show" CommandArgument='<%#Eval("WPID").ToString()%>'
                                                ImageUrl='<%#Convert.ToDouble(Eval("totalWeight") is DBNull ? 0 : Eval("totalWeight"))==0?"~/Images/grain.jpg":"~/Images/grainon.jpg" %>'
                                                runat="server" /><br />
                                            <%#Eval("WPName").ToString()%><br />
                                            <br />
                                        </td>
                                        <td style="width: 5px;">
                                        </td>
                                        <td>
                                            <div id='<%#Eval("WPID").ToString()%>' style="display: none;">
                                                仓位编码：<%#Eval("WPNumber").ToString()%><br />
                                                仓位名称：<%#Eval("WPName").ToString()%><br />
                                                堆位号：<%#Eval("Number").ToString()%><br />
                                                目前存放粮食总量(吨):<%# Math.Round(Convert.ToDouble(Eval("totalWeight") is DBNull ? 0 : Eval("totalWeight"))/1000,2)%>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr style="height: 15px;">
                                        <td colspan="2">
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </table></FooterTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
