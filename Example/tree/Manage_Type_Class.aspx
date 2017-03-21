<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Manage_Type_Class.aspx.cs" Inherits="Manage_Type_Class" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>无限级分类</title>
    <style type="text/css">
    body, td { font-size:12px;}
	a { color:#000;}
	.main_table { width:100%; padding:2px; border:solid 1px #C4D8ED;}
	.td_title { height:25px; line-height:25px; background:#EEF7FD; color:#135294; font-weight:bold;}
	.td_on { background:#fafafa;}
	.td_off { background:#EEF7FD;}
    </style>
    <script type="text/javascript">
    function ChkInput()
    {
        if(document.getElementById("txtClassName").value=="")
        {
            alert("栏目名称不能为空！");
            document.getElementById("txtClassName").focus();
            return false;
        }
    }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanelRpt" runat="server">
    <contenttemplate>
        <div style="font-size:14px; font-weight:bold;">所有产品分类列表</div><asp:Label ID="LabError" runat="server" ForeColor="red"></asp:Label>
        <table cellpadding=0 cellspacing=1 border=0 align="center">  
        <tr>
            <td>
                <asp:UpdateProgress id="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanelRpt">
                        <ProgressTemplate>
                            <div id="div1" runat="server">  
                                <asp:Image ID="Image1" runat="server" ImageUrl="~/images/loading.gif" />请稍后……
                            </div> 
                        </ProgressTemplate>
                </asp:UpdateProgress>
            </td>
        </tr>  
        <tr>
            <td>
                <asp:Button ID="Button1" runat="server" Text=" 保存栏目顺序 " OnClick="BtnSave_Click" />
            </td>
        </tr>    
        <tr>
            <td valign="top">
                <asp:Repeater ID="rptMenuList" runat="server" OnItemDataBound="rptMenuList_ItemDataBound" OnItemCommand="rptMenuList_ItemCommand">
                <HeaderTemplate>
                 <table class="main_table" cellpadding="0" cellspacing="1" border="0">
                   <tr align="center" class="td_title">
                        <td align="left" width="75%">分类名称</td>
                        <td align="left" width="10%">栏目顺序</td>
                        <td align="center" width="15%">操作</td>
                   </tr>
                </HeaderTemplate>
                <ItemTemplate>
                 <tr class="td_on" onmouseover="this.className='td_off'" onmouseout="this.className='td_on'">
                    <td>
                        <asp:HiddenField ID="txtClassId" runat="server" Value='<%#DataBinder.Eval(Container.DataItem,"ClassId") %>' />
                        <asp:Literal ID="LitFirst" runat="server"></asp:Literal><asp:Label ID="LabClassNm" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ClassName") %>'></asp:Label>
                       <%-- <asp:Label ID="Label1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ClassList") %>'></asp:Label>--%>
                    </td>
                    <td>
                        <asp:TextBox Width="50" ID="txtOrder" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ClassOrder") %>'></asp:TextBox>
                    </td>
                    <td align="center"><asp:LinkButton ID="BtnEdit" CommandName="BtnEdit" runat="server">编辑</asp:LinkButton>&nbsp;&nbsp;|&nbsp;&nbsp;<asp:LinkButton ID="BtnDelete" CommandName="BtnDelete" runat="server" OnClientClick="return confirm('你确定要删除吗')">删除</asp:LinkButton></td>
                 </tr>
                </ItemTemplate>
                <FooterTemplate>
                </table>
                </FooterTemplate>
                </asp:Repeater>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="BtnSave" runat="server" Text=" 保存 " OnClick="BtnSave_Click" Visible="false" />
            </td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td>
                            分类列表：
                        </td>
                        <td>
                        <asp:DropDownList ID="DdlMenu" runat="server" CssClass="select">
                <asp:ListItem Value="0" Text="添加为一级分类"></asp:ListItem>
                </asp:DropDownList>
                <asp:HiddenField ID="HidClassId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            分类名称：
                        </td>
                        <td>
                            <asp:TextBox ID="txtClassName" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            分类标签：
                        </td>
                        <td>
                            <asp:TextBox ID="txtClassTitle" runat="server" MaxLength="255" Columns="60"></asp:TextBox>
                            (可用，隔开)</td>
                    </tr>
                    <tr>
                        <td>
                            分类介绍：
                        </td>
                        <td>
                            <asp:TextBox ID="txtRemark" runat="server" Columns="60" Rows="4" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <a name="M"></a>
                        </td>
                        <td>
                        
                        </td>
                    </tr>
                </table>
                
                
                <asp:Button ID="btnAdd" runat="server" Text=" 添加 " OnClick="btnAdd_Click" />
                <asp:Button ID="btnEdit" runat="server" Text=" 保存 " Visible="false"  OnClick="btnEdit_Click" />
          </td>
        </tr>
    </table>
    </contenttemplate>
    </asp:UpdatePanel> 
    </form>

</body>
</html>
