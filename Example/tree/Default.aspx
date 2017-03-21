<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无限级分类</title>
    <style type="text/css">
        body
        {
            font-size:12px
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <div>
            <asp:TreeView ID="TreeView1" runat="server" ShowLines="True">
            <NodeStyle Font-Size="12px" />
            </asp:TreeView>
        </div>
    </form>    
</body>
</html>
