<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BookDetails.aspx.cs" Inherits="MiniBiblioteka.BookDetails" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Mini biblioteka - informacje o książce</title>
    <link href="~/style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form3" runat="server">
        <div id="wrapper">

            <div class="clr"></div>
            <div class="content">
                <div id="menu">
                    <ul id="ulMenu">
                        <li><a href="Index.aspx">Strona główna</a></li>
                        <li><a href="Login.aspx">Panel administracyjny</a></li>
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
                <div class="col main">
                    <h3>
                        <asp:Label ID="lblTitle" runat="server" Text="Label"></asp:Label></h3>
                    <b>Autor:</b>
                    <asp:Label ID="lblAutor" runat="server" Text="Label"></asp:Label>
                    <br />
                    <br />
                    <b>Ilość stron:</b>
                    <asp:Label ID="lblIloscStron" runat="server" Text="Label"></asp:Label>
                    <br />
                    <br />
                    <b>Wydawnictwo:</b>
                    <asp:Label ID="lblWydawnictwo" runat="server" Text="Label"></asp:Label>
                    <br />
                    <br />
                    <b>Miejsce wydania:</b>
                    <asp:Label ID="lblMiejsceWydania" runat="server" Text="Label"></asp:Label>
                    <br />
                    <br />
                    <b>Rok wydania:</b>
                    <asp:Label ID="lblRokWydania" runat="server" Text="Label"></asp:Label>
                    <br />
                    <br />
                    <b>Opis:</b>
                    <asp:Label ID="lblOpis" runat="server" Text="Label"></asp:Label>
                    <br />
                    <br />
                </div>




                <div class="col last">
                    <h3>Okładka</h3>
                    <div class="panel">
                        <asp:Image ID="Image1" runat="server" Width="294 px" />
                    </div>
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
