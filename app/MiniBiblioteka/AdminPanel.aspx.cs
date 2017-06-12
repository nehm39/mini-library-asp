using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace MiniBiblioteka
{
    public partial class AdminPanel : System.Web.UI.Page
    {
        //Pobieramy z konfiguracji string zawierający dane połączenia z bazą danych.
        string strSqlCon = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;


        protected void Page_Load(object sender, EventArgs e)
        {

            //Jeżeli nie jesteśmy zalogowani to przekierowujemy na stronę logowania.
            if (String.IsNullOrEmpty(Session["Login"] as string))
            {
                Response.Redirect("Login.aspx");
            }

            //Jeżeli zostaliśmy przekierowani na stronę po dodaniu książki, wyświetlamy komunikat o sukcesie.
            if (!(String.IsNullOrEmpty(Session["Add"] as string)))
            {
                lblMessage.Text = "Książka została dodana pomyślnie.";
                lblMessage.Visible = true;
                Session["Add"] = null;
            }

            //Jeżeli zostaliśmy przekierowani na stronę po edycji książki, wyświetlamy komunikat o sukcesie.
            if (!(String.IsNullOrEmpty(Session["Edit"] as string)))
            {
                lblMessage.Text = "Książka została zedytowana pomyślnie.";
                lblMessage.Visible = true;
                Session["Edit"] = null;
            }

            //Przy uruchomieniu strony ustawiamy labelkę z loginem na login, z którego jesteśmy zalogowani.
            //Wywołujemy metodę sortująca/filtrująca i wypełniająca ListView.
            if (!IsPostBack)
            {
                lblLogged.Text = Session["Login"].ToString();
                wypelnijListView(dplFiltr.SelectedIndex, txbFiltr.Text.ToString());
            }
            else lblMessage.Visible = false;
        }

        //Zmieniamy rozmiar strony DataPager po wybraniu opcji na DropDownList i wywołujemy metodę wypełniająca ListView.
        protected void dplElements_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataPager1.PageSize = Convert.ToInt16(dplElements.SelectedValue);
            wypelnijListView(dplFiltr.SelectedIndex, txbFiltr.Text.ToString());
        }

        //Po wybraniu kolumny po której będziemy sortować wywołujemy metodę wypełniająca ListView.
        protected void dplSorting_SelectedIndexChanged(object sender, EventArgs e)
        {
            wypelnijListView(dplFiltr.SelectedIndex, txbFiltr.Text.ToString());
        }


        private void wypelnijListView(int dp3Index, string value)
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
            string str = sortColumn + " " + direction;
            string like;
            string query;
            if (!(String.IsNullOrEmpty(value)))
            {
                if (dp3Index == 0) like = "AUTOR";
                else if (dp3Index == 1) like = "ROK_WYDANIA";
                else if (dp3Index == 2) like = "TYTUL";
                else like = "WYDAWNICTWO";
                query = "SELECT * FROM Ksiazki WHERE " + like + " LIKE '%" + value + "%' ORDER BY " + str;
            }
            else query = "SELECT * FROM Ksiazki ORDER BY " + str;
            var source = new SqlDataSource(strSqlCon, query);
            var dataView = (DataView)source.Select(DataSourceSelectArguments.Empty);
            lvBooks.DataSource = source;
            lvBooks.DataBind();
        }

        //Po wybraniu kierunku sortowania wywołujemy metodę wypełniająca ListView.
        protected void dplSortingDirection_SelectedIndexChanged(object sender, EventArgs e)
        {
            wypelnijListView(dplFiltr.SelectedIndex, txbFiltr.Text.ToString());
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

        //Po wciśnięciu strzałek danej książki przekierowujemy na stronę BookDetails.aspx z odpowiednim id.
        protected void lbBookDetails_Command(object sender, CommandEventArgs e)
        {
            Response.Redirect("BookDetails.aspx?id=" + e.CommandArgument);
        }

        //Przed wyrenderowaniem każdej strony DataPager od nowa wywołujemy metodę sortująca ListView.
        protected void DataPager1_PreRender(object sender, EventArgs e)
        {
            wypelnijListView(dplFiltr.SelectedIndex, txbFiltr.Text.ToString());
        }

        //Po kliknięciu buttona Wyloguj czyścimy sesję logowania i przekierowujemy na stronę główną.
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session["Login"] = null;
            Response.Redirect("Index.aspx");
        }

        //Po kliknięciu na LinkButton "Usuń" przy danej pozycji nawiązujemy połączenie z bazą danych i
        //wywołujemy zapytanie usuwające z bazy wybrany rekord.
        //Następnie wyświetlamy komunikat o pomyślnym wykonaniu.
        //Jeżeli wystąpi wyjątek to wyświetlamy komunikat o błędzie.
        protected void lbDelete_Command(object sender, CommandEventArgs e)
        {
            SqlConnection connection = new SqlConnection(strSqlCon);
            connection.Open();
            SqlCommand command = new SqlCommand("DELETE from Ksiazki WHERE ID_KSIAZKI = " + e.CommandArgument, connection);
            command.ExecuteNonQuery();
            connection.Close();
            lblMessage.Text = "Usunięto pomyślnie.";
            lblMessage.Visible = true;
            wypelnijListView(dplFiltr.SelectedIndex, txbFiltr.Text.ToString());

        }

        //Po wciśnięciu LinkButtona "Usuń" danej książki przekierowujemy na stronę BookEdit.aspx z odpowiednim id.
        protected void lbEdit_Command(object sender, CommandEventArgs e)
        {
            Response.Redirect("BookEdit.aspx?id=" + e.CommandArgument);
        }

        //Po wciśnięciu Buttona "Dodaj książke" przekierowujemy na stronę z dodawaniem nowej książki.
        protected void btnAddBook_Click(object sender, EventArgs e)
        {
            Response.Redirect("BookAdd.aspx");
        }

        //Po zmianie tekstu w TextBoxie txbFiltr wywołujemy metodę wypełniającą ListView.
        protected void txbFiltr_TextChanged(object sender, EventArgs e)
        {
            wypelnijListView(dplFiltr.SelectedIndex, txbFiltr.Text.ToString());
        }
    }
}