<%@ Page Title="" Language="C#" MasterPageFile="~/MyMasterSecond.master" AutoEventWireup="true"
    CodeFile="TaskView.aspx.cs" Inherits="TruckEir_TaskView" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%=PageTitle %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Timer ID="Timer1" runat="server" Interval="5000" OnTick="Timer1_Tick">
            </asp:Timer>
            <div class="divtdcontent">
                <div id="divtasttime">
                    <asp:Label ID="lblTime" runat="server" Text="请稍候..."></asp:Label>
                </div>
                <div style="height: 30px;">
                </div>
                <div>
                    <asp:GridView ID="GridView1" EmptyDataText="暂无执行中任务!" runat="server">
                        <Columns>
                            <asp:BoundField DataField="PRMBNumber" HeaderText="通知单编号"></asp:BoundField>
                            <asp:BoundField DataField="PRMBLot" HeaderText="批号"></asp:BoundField>
                            <asp:BoundField DataField="MaterialNumber" HeaderText="物料号" HeaderStyle-Width="8%">
                            </asp:BoundField>
                            <asp:BoundField DataField="MaterialName" HeaderText="物料名称" HeaderStyle-Width="12%">
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="任务类型" HeaderStyle-Width="6%">
                                <ItemTemplate>
                                    <%# Common.GetTETaskType(Eval("TETaskType").ToString().Trim())%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="基本重量(KG)" HeaderStyle-Width="8%">
                                <ItemTemplate>
                                    <%# Convert.ToInt32(Eval("TESalverQty") is DBNull ? 0 : Eval("TESalverQty"))*1000%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="进度" HeaderStyle-Width="360px">
                                <ItemStyle HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <table>
                                        <tr>
                                            <td>
                                                <div style="background-color: #FF6600; height: 20px; width: <%# GetProcess(Eval("TESalverQty"), Eval("TECurSalverQty"))*3%>px;">
                                                </div>
                                            </td>
                                            <td>
                                                <%# GetProcess(Eval("TESalverQty"), Eval("TECurSalverQty"))%>%
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
