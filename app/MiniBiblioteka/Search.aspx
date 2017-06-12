<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="MiniBiblioteka.Search" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Mini biblioteka - szukaj</title>
    <link href="~/style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form2" runat="server" defaultbutton="btnSearch">
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
                        onfocus="if (this.value == 'Wpisz...')  this.value = ''; "></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" Text="Szukaj" OnClick="btnSearch_Click" />
                </div>

                <div id="header">
                </div>
                <div class="clr"></div>
            </div>
            <div id="main">
                <div class="col main">
                    <h3>Znalezione pozycje (<asp:Label ID="lblNumberOfPositions" runat="server" Text=""></asp:Label>):</h3>
                    <asp:ListView ID="lvBooks" runat="server" EnableModelValidation="True">
                        <ItemTemplate>
                            <div class="book">
                                <p class="short"><%# Eval("TYTUL") + ", " + Eval("AUTOR") + ", " + Eval("Wydawnictwo") + " " + Eval("ROK_WYDANIA")%> <asp:LinkButton ID="lbBookDetails" CommandName="view" CommandArgument='<%# Eval("ID_KSIAZKI") %>' runat="server" OnCommand="lbBookDetails_Command" >&raquo;</asp:LinkButton></p>
                            </div>

                        </ItemTemplate>

                    </asp:ListView>
                    <asp:DataPager ID="DataPager1" runat="server" PageSize="10" PagedControlID="lvBooks" OnPreRender="DataPager1_PreRender">
                        <Fields>
                            <asp:NextPreviousPagerField ShowPreviousPageButton="true"
                                ShowLastPageButton="false" ShowNextPageButton="false" ButtonCssClass="dpbutton" PreviousPageText="Poprzednia" />

                            <asp:NumericPagerField ButtonType="Link" NumericButtonCssClass="dpbutton" />

                            <asp:NextPreviousPagerField ShowFirstPageButton="false" ShowPreviousPageButton="false"
                                ShowNextPageButton="true" ButtonCssClass="dpbutton" NextPageText="Następna" />
                        </Fields>
                    </asp:DataPager>
                    <br />
                    <br />
                    <div>
                        <asp:Label ID="Label1" runat="server" Text="Elementów na stronie: "></asp:Label>

                        <asp:DropDownList ID="dplElements" BackColor="#dfdfdf" runat="server" OnSelectedIndexChanged="dplElements_SelectedIndexChanged" AutoPostBack="true">
                            <asp:ListItem>5</asp:ListItem>
                            <asp:ListItem Selected="True">10</asp:ListItem>
                            <asp:ListItem>15</asp:ListItem>
                            <asp:ListItem>20</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <br />
                    <div>
                        <asp:Label ID="Label2" runat="server" Text="Sortuj według: "></asp:Label>

                        <asp:DropDownList ID="dplSorting" BackColor="#dfdfdf" runat="server" AutoPostBack="true" OnSelectedIndexChanged="dplSorting_SelectedIndexChanged">
                            <asp:ListItem Text="autor" Value="0" />
                            <asp:ListItem Text="rok wydania" Value="1" />
                            <asp:ListItem Selected="True" Text="tytuł" Value="2" />
                            <asp:ListItem Text="wydawnictwo" Value="3" />

                        </asp:DropDownList>
                        <asp:DropDownList ID="dplSortingDirection" BackColor="#dfdfdf" runat="server" AutoPostBack="true" OnSelectedIndexChanged="dplSortingDirection_SelectedIndexChanged">
                            <asp:ListItem Selected="True" Text="rosnąco" Value="0" />
                            <asp:ListItem Text="malejąco" Value="1" />

                        </asp:DropDownList>
                    </div>

                </div>




                <div class="col last">
                    <h3>Wyszukiwanie zaawansowane</h3>
                    <div class="panel">
                        <asp:Panel ID="Panel1" runat="server" DefaultButton="btnAdvancedSearch">
                            Tytuł:
                        <asp:TextBox BackColor="#dfdfdf" ID="txbTytul" float="right" runat="server" Style="float: right; width: 59%"></asp:TextBox>
                            <br />
                            <br />
                            <br />

                            <asp:DropDownList BackColor="#dfdfdf" ID="DropDownList1" runat="server" Style="margin-left: 40%">
                                <asp:ListItem Text="i/oraz" Value="0" />
                                <asp:ListItem Text="lub" Value="1" />
                            </asp:DropDownList>
                            <br />
                            Autor:
                        <asp:TextBox BackColor="#dfdfdf" ID="txbAutor" float="right" runat="server" Style="float: right; width: 59%; margin-top: 2px"></asp:TextBox>
                            <br />
                            <br />
                            <br />

                            <asp:DropDownList BackColor="#dfdfdf" ID="DropDownList2" runat="server" Style="margin-left: 40%">
                                <asp:ListItem Text="i/oraz" Value="0" />
                                <asp:ListItem Text="lub" Value="1" />
                            </asp:DropDownList>
                            <br />
                            Opis:
                        <asp:TextBox BackColor="#dfdfdf" ID="txbOpis" float="right" runat="server" Style="float: right; width: 59%; margin-top: 2px"></asp:TextBox>
                            <br />
                            <br />
                            <br />

                            <asp:DropDownList BackColor="#dfdfdf" ID="DropDownList3" runat="server" Style="margin-left: 40%">
                                <asp:ListItem Text="i/oraz" Value="0" />
                                <asp:ListItem Text="lub" Value="1" />
                            </asp:DropDownList>
                            <br />
                            Wydawnictwo:
                        <asp:TextBox BackColor="#dfdfdf" ID="txbWydawnictwo" float="right" runat="server" Style="float: right; width: 59%; margin-top: 2px"></asp:TextBox>
                            <br />
                            <br />
                            <br />



                            <asp:Button ID="btnAdvancedSearch" runat="server" Text="Szukaj" Style="margin-left: 40%" OnClick="btnAdvancedSearch_Click" />

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
