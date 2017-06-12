using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;

namespace MiniBiblioteka
{
    public partial class Search : System.Web.UI.Page
    {
        //Pobieramy z konfiguracji string zawierający dane połączenia z bazą danych.
        string strSqlCon = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            //Przy uruchomieniu strony wywołujemy metodę wypełniającą ListView
            if (!IsPostBack)
            {
                wypelnijListView(string.Empty);
            }
        }

        //Zmieniamy rozmiar strony DataPager po wybraniu opcji na DropDownList.
        protected void dplElements_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataPager1.PageSize = Convert.ToInt16(dplElements.SelectedValue);
            sortujListView();
        }

        //Po wybraniu kolumny po której będziemy sortować wywołujemy metodę sortującą ListView.
        protected void dplSorting_SelectedIndexChanged(object sender, EventArgs e)
        {
            sortujListView();
        }

        //W zależności od przekazanych parametrów konstruujemy odpowiednie zapytanie do bazy danych.
        //Jeżeli przekazany parametr jest nullem lub jest pusty to inicjalizujemy domyślnie sortowanie.
        //Jeżeli żadne z pól wyszukiwania zaawansowanego nie zostały wpisane to inicjalizujemy standardowe zapytanie wyszukujące.
        //Liczbę znalezionych książek obliczamy i zapisujemy do labelki.
        //Dane znalezione w bazie przypisujemy do ListView.
        private void wypelnijListView(string str)
        {
            if (String.IsNullOrEmpty(str))
            {
                str = "TYTUL ASC";
            }
            string query = null;
            if (!(String.IsNullOrEmpty(txbTytul.Text)))
            {
                query += "TYTUL LIKE '%" + txbTytul.Text + "%'";
            }
            if (!(String.IsNullOrEmpty(txbAutor.Text)))
            {
                if (query != null)
                {
                    if (DropDownList1.SelectedIndex == 0) query += " AND ";
                    else query += " OR ";
                }
                query += "AUTOR LIKE '%" + txbAutor.Text + "%'";
            }
            if (!(String.IsNullOrEmpty(txbOpis.Text)))
            {
                if (query != null)
                {
                    if (DropDownList2.SelectedIndex == 0) query += " AND ";
                    else query += " OR ";
                }
                query += "OPIS LIKE '%" + txbOpis.Text + "%'";
            }
            if (!(String.IsNullOrEmpty(txbWydawnictwo.Text)))
            {
                if (query != null)
                {
                    if (DropDownList3.SelectedIndex == 0) query += " AND ";
                    else query += " OR ";
                }
                query += "WYDAWNICTWO LIKE '%" + txbWydawnictwo.Text + "%'";
            }

            if (query == null) query = "TYTUL Like '%" + Session["Search"] + "%' OR AUTOR Like '%" + Session["Search"] + "%' OR OPIS Like '%" + Session["Search"] + "%' OR Wydawnictwo Like '%" + Session["Search"] + "%'";
            SqlDataSource source = new SqlDataSource(strSqlCon, "SELECT * FROM Ksiazki WHERE " + query + " ORDER BY " + str);
            DataView dataView = (DataView)source.Select(DataSourceSelectArguments.Empty);
            string numberOfPositions = dataView.Count.ToString();
            lblNumberOfPositions.Text = numberOfPositions;
            lvBooks.DataSource = source;
            lvBooks.DataBind();
        }

        //Sczytujemy wartości wybrane w DropDownList i używamy ich do posortowania ListView.
        //Wywołujemy metodę wypelnijListView() z parametrami sortowania.
        private void sortujListView()
        {
            int dp1 = Convert.ToInt16(dplSorting.SelectedValue);
            int dp2 = Convert.ToInt16(dplSortingDirection.SelectedValue);
            string sortColumn;
            string direction;
            if (dp1 == 0) sortColumn = "AUTOR";
            else if (dp1 == 1) sortColumn = "ROK_WYDANIA";
            else if (dp1 == 2) sortColumn = "TYTUL";
            else sortColumn = "WYDAWNICTWO";
            if (dp2 == 0) direction = "ASC";
            else direction = "DESC";
            wypelnijListView(sortColumn + " " + direction);
        }

        //Po wybraniu kierunku sortowania wywołujemy metodę sortującą ListView.
        protected void dplSortingDirection_SelectedIndexChanged(object sender, EventArgs e)
        {
            sortujListView();
        }

        //Jeżeli pole szukaj nie jest puste lub domyślne (Wpisz...) to zapisujemy jego zawartość do sesji i przekierowujemy na stronę Search.aspx.
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (!(String.IsNullOrEmpty(txbSearch.Text)) && !(txbSearch.Text.Equals("Wpisz...")))
            {
                Session["Search"] = txbSearch.Text;
                Response.Redirect("Search.aspx");
            }
        }

        //Po wciśnięciu przycisku wyszukiwania zaawansowanego wywołujemy metodę sortująca ListView.
        protected void btnAdvancedSearch_Click(object sender, EventArgs e)
        {
            sortujListView();
        }

        //Po wciśnięciu strzałek danej książki przekierowujemy na stronę BookDetails.aspx z odpowiednim id.
        protected void lbBookDetails_Command(object sender, CommandEventArgs e)
        {
            Response.Redirect("BookDetails.aspx?id=" + e.CommandArgument);
        }

        //Przed wyrenderowaniem każdej strony DataPager od nowa wywołujemy metodę sortująca ListView.
        protected void DataPager1_PreRender(object sender, EventArgs e)
        {
            sortujListView();
        }
    }
}