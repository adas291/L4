<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Forma1.aspx.cs" Inherits="L4.Forma1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="Stilius.css"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" />
            <asp:FileUpload ID="FileUpload1" runat="server" AllowMultiple="True" />
            <br />
            <asp:Button CssClass="ButtonStyle" ID="Button1" runat="server" OnClick="Button1_Click" Text="Įvesti duomenis" />
            <br />
        <asp:Table CssClass="TableStyle" ID="Table1" runat="server" GridLines="Both" ForeColor="Red" Height="16px">
        </asp:Table>
            <br />
            <asp:Label CssClass="LabelStyle" ID="Label1" runat="server" Text="Label" Visible="False"></asp:Label>
        </div>
        <asp:Table CssClass="TableStyle" ID="Table2" runat="server" GridLines="Both">
        </asp:Table>
        <br />
        <asp:Label CssClass="LabelStyle" ID="Label2" runat="server" Text="Label" Visible="False"></asp:Label>
        <asp:Table CssClass="TableStyle" ID="Table3" runat="server" GridLines="Both">
        </asp:Table>
        <br />
        <asp:Label CssClass="LabelStyle" ID="Label3" runat="server" Text="Label" Visible="False"></asp:Label>
        <br />
        <asp:Table CssClass="TableStyle" ID="Table4" runat="server" GridLines="Both">
        </asp:Table>
        <br />
        <asp:Label CssClass="LabelStyle" ID="Label7" runat="server" Text="Label" Visible="False"></asp:Label>
        <br />
        <br />
        <asp:Label CssClass="LabelStyle" ID="Label4" runat="server" Text="Label" Visible="False"></asp:Label>
        <br />
        <asp:Table CssClass="TableStyle" ID="Table5" runat="server" GridLines="Both">
        </asp:Table>
        <br />
        <asp:Label CssClass="LabelStyle" ID="Label5" runat="server" Text="Label" Visible="False"></asp:Label>
        <br />
        <asp:Table CssClass="TableStyle" ID="Table6" runat="server" GridLines="Both" Height="16px">
        </asp:Table>
        <br />
        <asp:Label CssClass="LabelStyle" ID="Label6" runat="server" Text="Label" Visible="False"></asp:Label>
        <br />
        <asp:Table CssClass="TableStyle" ID="Table7" runat="server" GridLines="Both" Height="16px">
        </asp:Table>
        <br />
        <asp:Label ID="Label8" runat="server" Text="Label" Visible="False"></asp:Label>
        <br />
        <asp:Table CssClass="TableStyle" ID="Table8" runat="server" GridLines="Both" Height="16px">
        </asp:Table>
        <br />
        <br />
        <br />
        <asp:Label ID="Label9" runat="server" Text="Label" Visible="False"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <br />
        <asp:Label ID="Label10" runat="server" Text="Label" Visible="False"></asp:Label>
    </form>
</body>
</html>
