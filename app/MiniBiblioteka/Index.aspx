<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="MiniBiblioteka.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Mini biblioteka - strona główna</title>
    <link href="~/style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div id="wrapper">

            <div class="clr"></div>
            <div class="content">
                <div id="menu">
                    <ul id="ulMenu">
                        <li><a class="current" href="Index.aspx">Strona główna</a></li>
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
                <div class="col full">
                    <h3>Lista książek</h3>
                    <asp:ListView ID="lvBooks" runat="server" DataSourceID="SqlDataSource1" EnableModelValidation="True">
                        <ItemTemplate>
                            <div class="book">
                                <p class="short">
                                    <%# Eval("TYTUL") + ", " + Eval("AUTOR") + ", " + Eval("Wydawnictwo") + " " + Eval("ROK_WYDANIA")%>
                                    <asp:LinkButton ID="lbBookDetails" CommandName="view" CommandArgument='<%# Eval("ID_KSIAZKI") %>' runat="server" OnCommand="lbBookDetails_Command">&raquo;</asp:LinkButton>
                                </p>
                            </div>


                        </ItemTemplate>

                    </asp:ListView>
                    <asp:DataPager ID="DataPager1" runat="server" PageSize="10" PagedControlID="lvBooks">
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
                <div class="clr"></div>
            </div>
            <div id="footer">
                <p>Copyright &copy; by Szymon Gajewski 2014. Wszelkie prawa zastrzeżone.</p>
            </div>
        </div>

        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:SqlConnection %>" SelectCommand="SELECT * FROM Ksiazki ORDER BY TYTUL"></asp:SqlDataSource>
    </form>
</body>
</html>
