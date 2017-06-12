using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace MiniBiblioteka
{
    public partial class BookDetails : System.Web.UI.Page
    {
        //Pobieramy z konfiguracji string zawierający dane połączenia z bazą danych.
        string strSqlCon = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            //Zapisujemy id książki z Requesta do zmiennej.
            string id = Request.QueryString["id"];
            //Jeżeli id w adresie jest puste to przekierowujemy na stronę główną.
            if (String.IsNullOrEmpty(id))
                Response.Redirect("Index.aspx");

            //Przy uruchomieniu strony wywołujemy metodę znajdującą w bazie danę książkę.
            if (!IsPostBack)
            {
                findBook(id);
            }
            
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

        //Znajdujemy w bazie książkę o danym id i wszystkie jej dane przypisujemy do odpowiednich labelek.
        //Pobieramy z bazy zdjęcie i wyświetlamy je w kontrolce Image.
        //Jeżeli id jest niepoprawne to przekierowujemy na stronę 404.aspx.
        private void findBook(string i)
        {
            SqlDataSource source = new SqlDataSource(strSqlCon, "SELECT *  FROM [Ksiazki] WHERE [ID_KSIAZKI] = " + i);
            DataView dataView = (DataView)source.Select(DataSourceSelectArguments.Empty);
            try
            {
                DataRowView drv = dataView[0];
                lblAutor.Text = drv["AUTOR"].ToString();
                lblIloscStron.Text = drv["ILOSC_STRON"].ToString();
                lblWydawnictwo.Text = drv["WYDAWNICTWO"].ToString();
                lblMiejsceWydania.Text = drv["MIEJSCE_WYDANIA"].ToString();
                lblRokWydania.Text = drv["ROK_WYDANIA"].ToString();
                lblTitle.Text = drv["TYTUL"].ToString();
                lblOpis.Text = drv["OPIS"].ToString();
                lblOpis.Text = lblOpis.Text.Replace("\r\n", "<br/>");
                object imgSql = drv["ZDJECIE"];
                byte[] encode = (byte[])imgSql;
                string encodeString = Convert.ToBase64String(encode);
                Image1.ImageUrl = "data:image/jpg;base64," + encodeString;
            }
            catch(IndexOutOfRangeException)
            {
                Response.Redirect("404.aspx");
            }
            
            
        }

    }
}