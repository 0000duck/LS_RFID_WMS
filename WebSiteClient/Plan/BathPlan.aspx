<%@ Page Title="" Language="C#" MasterPageFile="~/MyMaster.master" AutoEventWireup="true"
    CodeFile="BathPlan.aspx.cs" Inherits="Plan_BathPlan" %>

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
                            <asp:ListItem Selected="True" Value="">����֪ͨ��</asp:ListItem>
                            <asp:ListItem Value="1">����֪ͨ��</asp:ListItem>
                            <asp:ListItem Value="2">�ջ�֪ͨ��</asp:ListItem>
                            <asp:ListItem Value="3">���ϳ��ⵥ</asp:ListItem>
                            <asp:ListItem Value="4">������ⵥ</asp:ListItem>
                            <asp:ListItem Value="20">�������ⵥ</asp:ListItem>
                            <asp:ListItem Value="21">������ⵥ</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
            </table>
            <table cellpadding="5" cellspacing="0">
                <tr>
                    <td>
                        ���ݱ���
                    </td>
                    <td>
                        <asp:TextBox ID="txtPRNumber" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        ����
                    </td>
                    <td>
                        <asp:TextBox ID="txtPRLot" runat="server"></asp:TextBox>
                    </td>
                    <td colspan="2">
                    </td>
                </tr>
                <tr>
                    <td>
                        ҵ������&nbsp;��
                    </td>
                    <td>
                        <asp:TextBox ID="txtPlanStartTime" SkinID="DataSkin" runat="server"></asp:TextBox>
                        <cc1:CalendarExtender ID="txtPlanStartTime_CalendarExtender" runat="server" Enabled="True"
                            CssClass="MyCalendar" TargetControlID="txtPlanStartTime" Format="yyyy-MM-dd">
                        </cc1:CalendarExtender>
                    </td>
                    <td>
                        ��
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
                                                &nbsp;���ڴ������Ժ�...
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
                        ������ϸ��Ϣ
                    </div>
                    <div class="windowclose" onclick="Close();">
                        �ر�</div>
                </div>
                <div class="windowcontent" style="height: 500px;">
                    <asp:DetailsView ID="DetailsView1" runat="server">
                        <Fields>
                            <asp:BoundField DataField="PRNumber" HeaderText="���ݱ���"></asp:BoundField>
                            <asp:TemplateField HeaderText="��������" HeaderStyle-Width="8%">
                                <ItemTemplate>
                                    <%# Common.GetPRTypeName(Eval("PRType").ToString())%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="���ݹ�ϵ">
                                <ItemTemplate>
                                    <%# Common.GetPRMBRelation(Eval("PRRelation").ToString())%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="�������">
                                <ItemTemplate>
                                    <%# Eval("PRRelation").ToString() == "3" ? Eval("PRSerial").ToString() : ""%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="PRBizDate" HeaderText="ҵ������" DataFormatString="{0:yyyy-MM-dd}">
                            </asp:BoundField>
                            <asp:BoundField DataField="PRFinishDate" HeaderText="ҵ���������" DataFormatString="{0:yyyy-MM-dd}">
                            </asp:BoundField>
                            <asp:BoundField DataField="PRConsignmentUnitName" HeaderText="������֯����"></asp:BoundField>
                            <asp:BoundField DataField="PRConsigneeUnitName" HeaderText="�ջ���֯����"></asp:BoundField>
                            <asp:BoundField DataField="PRLot" HeaderText="����"></asp:BoundField>
                            <asp:BoundField DataField="PRCards" HeaderText="����"></asp:BoundField>
                            <asp:BoundField DataField="PRSealNum" HeaderText="������"></asp:BoundField>
                            <asp:BoundField DataField="MaterialNumber" HeaderText="���ϱ���"></asp:BoundField>
                            <asp:BoundField DataField="MaterialName" HeaderText="��������"></asp:BoundField>
                            <asp:BoundField DataField="PRProductPlace" HeaderText="����"></asp:BoundField>
                            <asp:BoundField DataField="PRFixedYear" HeaderText="����" DataFormatString="{0:yyyy-MM-dd}">
                            </asp:BoundField>
                            <asp:BoundField DataField="WHNumber" HeaderText="�ֿ����"></asp:BoundField>
                            <asp:BoundField DataField="WHName" HeaderText="�ֿ�����"></asp:BoundField>
                            <asp:BoundField DataField="WPNumber" HeaderText="��λ����"></asp:BoundField>
                            <asp:BoundField DataField="WPName" HeaderText="��λ����"></asp:BoundField>
                            <asp:TemplateField HeaderText="����(KG)">
                                <ItemTemplate>
                                    <%# Math.Round(Convert.ToDouble(Eval("PRQty")), 2)%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="��������(KG)">
                                <ItemTemplate>
                                    <%# Math.Round(Convert.ToDouble(Eval("PRBaseQty")),2)%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="��������(KG)">
                                <ItemTemplate>
                                    <%# Math.Round(Convert.ToDouble(Eval("PRAstantQty")), 2)%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="����(Ԫ/KG)">
                                <ItemTemplate>
                                    <%# Math.Round(Convert.ToDouble(Eval("PRPrice")), 2)%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="���(Ԫ)">
                                <ItemTemplate>
                                    <%# Math.Round(Convert.ToDouble(Eval("PRAmount")), 2)%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="PRDescription" HeaderText="����"></asp:BoundField>
                            <asp:TemplateField HeaderText="״̬">
                                <ItemTemplate>
                                    <%# Common.GetPRStatus(Eval("PRIsFinished").ToString())%>
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
                        <asp:TemplateField HeaderText="���" HeaderStyle-Width="50px">
                            <ItemTemplate>
                                <%# Container.DataItemIndex+1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="PRNumber" HeaderText="���ݱ���"></asp:BoundField>
                        <asp:TemplateField HeaderText="��������" HeaderStyle-Width="8%">
                            <ItemTemplate>
                                <%# Common.GetPRTypeName(Eval("PRType").ToString())%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="PRBizDate" HeaderText="ҵ������" HeaderStyle-Width="8%" DataFormatString="{0:yyyy-MM-dd}">
                        </asp:BoundField>
                        <asp:BoundField DataField="PRFinishDate" HeaderText="ҵ���������" HeaderStyle-Width="9%"
                            DataFormatString="{0:yyyy-MM-dd}"></asp:BoundField>
                        <asp:BoundField DataField="PRConsignmentUnitName" HeaderText="������֯����" HeaderStyle-Width="8%">
                        </asp:BoundField>
                        <asp:BoundField DataField="PRConsigneeUnitName" HeaderText="�ջ���֯����" HeaderStyle-Width="8%">
                        </asp:BoundField>
                        <asp:BoundField DataField="PRLot" HeaderText="����" HeaderStyle-Width="8%"></asp:BoundField>
                        <asp:BoundField DataField="MaterialNumber" HeaderText="���ϱ���" HeaderStyle-Width="8%">
                        </asp:BoundField>
                        <asp:BoundField DataField="MaterialName" HeaderText="��������" HeaderStyle-Width="10%">
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="���ݹ�ϵ" HeaderStyle-Width="8%">
                            <ItemTemplate>
                                <asp:Label ID="lblViewRelation" Text='<%# Common.GetPRMBRelation(Eval("PRRelation").ToString())%>'
                                    runat="server"></asp:Label>
                                <asp:LinkButton ID="lnkViewRelation" CommandName="ViewRelation" Text="[�鿴]" runat="server"></asp:LinkButton>
                                <asp:Label ID="lblPRID" runat="server" Visible="false" Text='<%# Eval("PRID").ToString()%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="״̬" HeaderStyle-Width="6%">
                            <ItemTemplate>
                                <%# Common.GetPRStatus(Eval("PRIsFinished").ToString())%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="�鿴" HeaderStyle-Width="60px">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkViewDetail" CommandName="ViewDetail" runat="server">����</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <div class="divpage">
                <table>
                    <tr>
                        <td>
                            �ܼ�¼����<asp:Label ID="lblRecordNum" runat="server" Text="0"></asp:Label>&nbsp; Ŀǰҳ/��ҳ����<asp:Label
                                ID="lblPresentPageNum" runat="server" Text="0"></asp:Label>/<asp:Label ID="lblTotalPageNum"
                                    runat="server" Text="0"></asp:Label>&nbsp;
                            <asp:LinkButton ID="lbtFirstPage" CommandArgument="first" OnCommand="ChangePage_Click"
                                runat="server">��ҳ</asp:LinkButton>&nbsp;
                            <asp:LinkButton ID="lbtPreviousPage" CommandArgument="previous" OnCommand="ChangePage_Click"
                                runat="server">��һҳ</asp:LinkButton>&nbsp;
                            <asp:LinkButton ID="lbtNextPage" CommandArgument="next" OnCommand="ChangePage_Click"
                                runat="server">��һҳ</asp:LinkButton>&nbsp;
                            <asp:LinkButton ID="lbtLastPage" CommandArgument="last" OnCommand="ChangePage_Click"
                                runat="server">βҳ</asp:LinkButton>&nbsp; ��ת����<asp:TextBox ID="txtPageNum" CssClass="page_nav_textbox"
                                    runat="server"></asp:TextBox>ҳ
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
