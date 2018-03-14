<%@ Page Title="" Language="C#" MasterPageFile="~/MyMaster.master" AutoEventWireup="true"
    CodeFile="GroupWorkItem.aspx.cs" Inherits="Work_GroupWorkItem" %>

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
                                </td>
                                <td>
                                    <asp:ImageButton ID="btnSearch" OnCommand="btnSearch_Click" CommandName="date" ImageUrl="~/Images/search.gif"
                                        runat="server" />&nbsp;
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtPlanStartTime"
                                        Display="Dynamic" runat="server" ErrorMessage="请选择日期！"></asp:RequiredFieldValidator>
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
                                <td>
                                    选择月份
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlMonth" Width="130px" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:ImageButton ID="ibtnSearch1" OnCommand="btnSearch_Click" CommandName="month"
                                        CausesValidation="false" ImageUrl="~/Images/search.gif" runat="server" />
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
                                </td>
                                <td>
                                    <asp:ImageButton ID="ibtnSearch2" OnCommand="btnSearch_Click" CommandName="year"
                                        CausesValidation="false" ImageUrl="~/Images/search.gif" runat="server" />
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
        <div id="divtoexcel" style="display: none; float:right; padding: 10px;">
            <asp:LinkButton ID="ltnToExcle" OnClick="ltnToExcle_OnClick" CausesValidation="false"
                Text="导出Excel文档" runat="server"></asp:LinkButton></div><br /><br />
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <asp:GridView ID="GridView1" runat="server">
                    <Columns>
                        <asp:BoundField DataField="jobno" HeaderText="工号" HeaderStyle-Width="20%" />
                        <asp:BoundField DataField="realname" HeaderText="姓名" HeaderStyle-Width="20%" />
                        <asp:BoundField DataField="opernum" HeaderText="工作量(次)" />
                    </Columns>
                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
