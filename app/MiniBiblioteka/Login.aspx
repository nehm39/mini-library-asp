<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="MiniBiblioteka.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Mini biblioteka - zaloguj</title>
    <link href="~/style.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            width: 218px;
        }
        .auto-style3 {
            width: 328px;
        }
    </style>
</head>
<body>
    <form id="form4" runat="server">
       <div id="wrapper">

                <div class="clr"></div>
            <div class="content">
                <div id="menu">
                                    <ul id="ulMenu">
                    <li><a href="Index.aspx">Strona główna</a></li>
                    <li><a class="current" href="Login.aspx">Panel administracyjny</a></li>
                </ul>

                </div>

                                                    <div id="search">
                    <asp:TextBox ID="txbSearch" Text="Wpisz..." runat="server" BackColor="#dfdfdf"
                        onfocus="if (this.value == 'Wpisz...')  this.value = ''; "
                        defaultbutton="btnSzukaj"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" Text="Szukaj" OnClick="btnSearch_Click" />
                </div>

                <div id="header">
                </div>
                <div class="clr"></div>
            </div>
            <div id="main">
                <div class="col full">
                    <h3>Zaloguj się</h3>
                    <br /><br />
                    <asp:Panel ID="Panel1" runat="server" DefaultButton="btnLogin">
                    <table class="auto-style1">
                        <tr>
                            <td style="text-align: right; padding-right: 5px" class="auto-style3">Login:</td>
                            <td class="auto-style2">
                                <asp:TextBox ID="txbLogin" BackColor="#dfdfdf" runat="server" Width="213px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="validLogin" runat="server" ValidationGroup="vgLogin" ControlToValidate="txbLogin" ErrorMessage="Pole z loginem nie może być puste." ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; padding-right: 5px" class="auto-style3">Hasło:</td>
                            <td class="auto-style2">
                                <asp:TextBox ID="txbPassword" BackColor="#dfdfdf" runat="server" Width="213px" TextMode="Password"></asp:TextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="validPassword" runat="server" ValidationGroup="vgLogin" ControlToValidate="txbPassword" ErrorMessage="Pole z hasłem nie może być puste." ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style3">&nbsp;</td>
                            <td class="auto-style2">
                                <asp:Button ID="btnLogin" runat="server" Text="Zaloguj" style="margin-left: 30%" OnClick="btnLogin_Click" ValidationGroup="vgLogin"/>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="auto-style3">&nbsp;</td>
                            <td class="auto-style2">
                                <asp:Label ID="lblError" runat="server" Style="text-align: center; margin-left: 30%" ForeColor="Red" Text="Błędne dane." visible="false"></asp:Label>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                        </asp:Panel>
                </div>
                <div class="clr"></div>
            </div>
            <div id="footer">
                <p>Copyright &copy; by Szymon Gajewski 2014. Wszelkie prawa zastrzeżone.</p>
            </div>
        </div>

    </form>
</body>
</html>
