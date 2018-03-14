<%@ Page Title="" Language="C#" MasterPageFile="~/MyMaster.master" AutoEventWireup="true"
    CodeFile="SalverByStorageList.aspx.cs" Inherits="SalverByStorage_SalverByStorageList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content3" ContentPlaceHolderID="titleName" runat="Server">
    <%=PageTitle %>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table cellpadding="5" cellspacing="0">
                <tr>
                    <td>
                        仓位编码
                    </td>
                    <td>
                        <asp:TextBox ID="txtWPNumber" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        选择仓库
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlStorage" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td colspan="2"></td>
                </tr>
                <tr>
                    <td>
                        批号
                    </td>
                    <td>
                        <asp:TextBox ID="txtWPGLot" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        物料编号
                    </td>
                    <td>
                        <asp:TextBox ID="txtMaterailNumber" runat="server"></asp:TextBox>
                    </td>
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
            <div class="divtdcontent">
                <asp:GridView ID="GridView1" OnRowDataBound="GridView1_OnRowDataBound" runat="server">
                    <Columns>
                        <asp:BoundField DataField="serialnum" HeaderText="序号" HeaderStyle-Width="50px"></asp:BoundField>
                        <asp:BoundField DataField="WHName" HeaderText="仓库名称" HeaderStyle-Width="100px"></asp:BoundField>
                        <asp:BoundField DataField="Floor" HeaderText="楼层" HeaderStyle-Width="100px"></asp:BoundField>
                        <asp:BoundField DataField="WPNumber" HeaderText="仓位编号" HeaderStyle-Width="90px">
                        </asp:BoundField>
                        <asp:BoundField DataField="WPName" HeaderText="仓位名称" HeaderStyle-Width="120px"></asp:BoundField>
                        <asp:BoundField DataField="WPGLot" HeaderText="批号" HeaderStyle-Width="120px"></asp:BoundField>
                        <asp:BoundField DataField="MaterialNumber" HeaderText="物料编号" HeaderStyle-Width="100px">
                        </asp:BoundField>
                        <asp:BoundField DataField="MaterialName" HeaderText="物料名称"></asp:BoundField>
                        <asp:BoundField DataField="WPGBaseQty" HeaderText="粮食重量(KG)" DataFormatString="{0:#.##}"
                            HeaderStyle-Width="200px"></asp:BoundField>
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
