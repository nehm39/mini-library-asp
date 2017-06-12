<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BookAdd.aspx.cs" Inherits="MiniBiblioteka.BookAdd" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Mini biblioteka - dodaj książkę</title>
    <link href="~/style.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            width: 200px;
        }
        .auto-style3 {
            width: 310px;
        }
        .auto-style4 {
            width: 306px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
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
                <div class="col full">
                    <h3>Dodaj książkę</h3>
                    <div>
                                                <asp:Panel ID="Panel1" runat="server" DefaultButton="btnAdd">
                        <table class="auto-style1">
                            <tr>
                                <td class="auto-style2"><b>Tytuł:</b> </td>
                                <td class="auto-style4"> <asp:TextBox ID="txbTytul" BackColor="#dfdfdf" MaxLength="250" runat="server" Width="300px"></asp:TextBox>
                                </td>
                                <td>
                                <asp:RequiredFieldValidator ID="validTytul" runat="server" ValidationGroup="vgAdd" ControlToValidate="txbTytul" ErrorMessage="Pole z tytułem nie może być puste." ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                            </tr>
                            <tr>
                                <td class="auto-style2"><b>Autor:</b></td>
                                <td class="auto-style4"> <asp:TextBox ID="txbAutor" BackColor="#dfdfdf" MaxLength="250" runat="server" Width="300px"></asp:TextBox>
                                </td>
                                <td>
                                <asp:RequiredFieldValidator ID="validAutor" runat="server" ValidationGroup="vgAdd" ControlToValidate="txbAutor" ErrorMessage="Pole z autorem nie może być puste." ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                            </tr>
                            <tr>
                                <td class="auto-style2"><b>Ilość stron:</b> </td>
                                <td class="auto-style4"> <asp:TextBox ID="txbIloscStron" BackColor="#dfdfdf" runat="server" TextMode="Number" Width="300px"></asp:TextBox>
                                </td>
                                <td>
                                <asp:RequiredFieldValidator ID="validIloscStron" runat="server" ValidationGroup="vgAdd" ControlToValidate="txbIloscStron" ErrorMessage="Pole z ilością stron nie może być puste." ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                                    <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="txbIloscStron" ErrorMessage="Ilość stron może być tylko wartością całkowitą z zakresu 1-9999." Display="Dynamic" ForeColor="Red" Type="Integer"
                                        MinimumValue="1" MaximumValue="9999"></asp:RangeValidator>
                            </td>
                            </tr>
                            <tr>
                                <td class="auto-style2"><b>Wydawnictwo:</b> </td>
                                <td class="auto-style4"> <asp:TextBox ID="txbWydawnictwo" BackColor="#dfdfdf" MaxLength="30" runat="server" Width="300px"></asp:TextBox>
                                </td>
                                <td>
                                <asp:RequiredFieldValidator ID="validWydawnictwo" runat="server" ValidationGroup="vgAdd" ControlToValidate="txbWydawnictwo" ErrorMessage="Pole z wydawnictwem nie może być puste." ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                            </tr>
                            <tr>
                                <td class="auto-style2"><b>Miejsce wydania:</b></td>
                                <td class="auto-style4"> <asp:TextBox ID="txbMiejsceWydania" BackColor="#dfdfdf" MaxLength="30" runat="server" Width="300px"></asp:TextBox>
                                </td>
                                <td>
                                <asp:RequiredFieldValidator ID="validMiejsceWydania" runat="server" ValidationGroup="vgAdd" ControlToValidate="txbMiejsceWydania" ErrorMessage="Pole z miejscem wydania nie może być puste." ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                            </tr>
                            <tr>
                                <td class="auto-style2"><b>Rok wydania:</b> </td>
                                <td class="auto-style4"> <asp:TextBox ID="txbRokWydania" BackColor="#dfdfdf" TextMode="Number" runat="server" Width="300px"></asp:TextBox>
                                </td>
                                <td>
                                <asp:RequiredFieldValidator ID="validRokWydania" runat="server" ValidationGroup="vgAdd" ControlToValidate="txbRokWydania" ErrorMessage="Pole z rokiem wydania nie może być puste." ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                                    <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txbRokWydania" ErrorMessage="Rok wydania może być tylko wartością całkowitą z zakresu 1800-2014." ForeColor="Red" Type="Integer"
                                        MinimumValue="1800" MaximumValue="2014" Display="Dynamic"></asp:RangeValidator>
                                    </td>
                            </tr>
                            <tr>
                                <td class="auto-style2"><b>Opis:</b> </td>
                                <td class="auto-style4"> <asp:TextBox ID="txbOpis" BackColor="#dfdfdf" runat="server" TextMode="MultiLine" Width="300px"></asp:TextBox>
                                </td>
                                <td>
                                <asp:RequiredFieldValidator ID="validOpis" runat="server" ValidationGroup="vgAdd" ControlToValidate="txbOpis" ErrorMessage="Pole z opisem nie może być puste." ForeColor="Red"></asp:RequiredFieldValidator>
                                    
                                </td>
                            </tr>
                             <tr>
                                <td class="auto-style2"><b>Okładka:</b> </td>
                                <td class="auto-style4"> 
                                    <asp:FileUpload ID="FileUpload1" Width="300px" runat="server" />
                                </td>
                                 <td>
                                <asp:RequiredFieldValidator ID="validOkladka" runat="server" ValidationGroup="vgAdd" ControlToValidate="FileUpload1" ErrorMessage="Należy wybrać okładkę." ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                            </tr>
                        </table>
                    <br />
                            <asp:Button ID="btnAdd" runat="server" Style="margin-left: 200px" Text="Dodaj" ValidationGroup="vgAdd" OnClick="btnAdd_Click" />
<br /><br />
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ValidationGroup="vgAdd" ForeColor="Red" ErrorMessage="Okładka musi być w formacie jpg/png/bmp/gif." ValidationExpression=".*\.(jpg|JPG|png|PNG|bmp|BMP|jpeg|JPEG|gif|GIF)$" ControlToValidate="FileUpload1"></asp:RegularExpressionValidator><br />
                                                    <asp:Label ID="lblError" runat="server" Text="Wystąpił błąd przy dodawaniu danych. Sprawdź czy na pewno podałeś poprawne dane." ForeColor="Red" Visible="false"></asp:Label>
                        </asp:Panel>
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
