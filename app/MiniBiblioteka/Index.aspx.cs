using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace MiniBiblioteka
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        //Zmieniamy rozmiar strony DataPager po wybraniu opcji na DropDownList.
        protected void dplElements_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataPager1.PageSize = Convert.ToInt16(dplElements.SelectedValue);
        }

        //Po wybraniu kolumny po której będziemy sortować wywołujemy metodę sortującą ListView.
        protected void dplSorting_SelectedIndexChanged(object sender, EventArgs e)
        {
            sortujListView();
        }

        private void sortujListView()
        {
            //Sczytujemy wartości wybrane w DropDownList i używamy ich do posortowania ListView.
            int dp1 = Convert.ToInt16(dplSorting.SelectedValue);
            int dp2 = Convert.ToInt16(dplSortingDirection.SelectedValue);
            string sortColumn;
            SortDirection direction;
            if (dp1 == 0) sortColumn = "AUTOR";
            else if (dp1 == 1) sortColumn = "ROK_WYDANIA";
            else if (dp1 == 2) sortColumn = "TYTUL";
            else sortColumn = "WYDAWNICTWO";
            if (dp2 == 0) direction = SortDirection.Ascending;
            else direction = SortDirection.Descending;
            lvBooks.Sort(sortColumn, direction);
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

        //Po wciśnięciu strzałek danej książki przekierowujemy na stronę BookDetails.aspx z odpowiednim id.
        protected void lbBookDetails_Command(object sender, CommandEventArgs e)
        {
            Response.Redirect("BookDetails.aspx?id=" + e.CommandArgument);
        }
    }
}