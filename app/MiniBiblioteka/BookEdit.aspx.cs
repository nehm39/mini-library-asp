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
    public partial class BookEdit : System.Web.UI.Page
    {
        //Pobieramy z konfiguracji string zawierający dane połączenia z bazą danych.
        string strSqlCon = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
        //Zmienna trzymająca id edytowanej książki.
        string id;
        protected void Page_Load(object sender, EventArgs e)
        {
            //Jeżeli nie jesteśmy zalogowani to przekierowujemy na stronę logowania.
            if (String.IsNullOrEmpty(Session["Login"] as string))
            {
                Response.Redirect("Login.aspx");
            }

            //Zapisujemy id książki z Requesta do zmiennej.
            id = Request.QueryString["id"];
            //Jeżeli id w adresie jest puste to przekierowujemy na stronę główną.
            if (String.IsNullOrEmpty(id))
                Response.Redirect("Index.aspx");

            //Przy uruchomieniu strony wywołujemy metodę wczytujące dane książki.
            if (!IsPostBack)
            {
                wczytajDane();
            }
        }

        //Znajdujemy w bazie książkę o danym id i wszystkie jej dane przypisujemy do odpowiednich TextBoxów.
        //Pobieramy z bazy zdjęcie i wyświetlamy je w kontrolce Image.
        //Jeżeli id jest niepoprawne to przekierowujemy na stronę 404.aspx.
        private void wczytajDane()
        {
            SqlDataSource source = new SqlDataSource(strSqlCon, "SELECT *  FROM Ksiazki WHERE ID_KSIAZKI = " + id);
            DataView dataView = (DataView)source.Select(DataSourceSelectArguments.Empty);
            try
            {
                DataRowView drv = dataView[0];
                txbAutor.Text = drv["AUTOR"].ToString();
                txbIloscStron.Text = drv["ILOSC_STRON"].ToString();
                txbWydawnictwo.Text = drv["WYDAWNICTWO"].ToString();
                txbMiejsceWydania.Text = drv["MIEJSCE_WYDANIA"].ToString();
                txbRokWydania.Text = drv["ROK_WYDANIA"].ToString();
                txbTytul.Text = drv["TYTUL"].ToString();
                txbOpis.Text = drv["OPIS"].ToString();
                object imgSql = drv["ZDJECIE"];
                byte[] encode = (byte[])imgSql;
                string encodeString = Convert.ToBase64String(encode);
                Image1.ImageUrl = "data:image/jpg;base64," + encodeString;
            }
            catch (IndexOutOfRangeException)
            {
                Response.Redirect("404.aspx");
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

        //Po kliknięciu na przycisk łączymy się z bazą danych.
        //Tworzymy nowe zapytanie modyfikujące dane w bazie.
        //Wszystkie dane wpisane w TextBoxy dodajemy do zapytania, a zdjęcie (jeżeli zostało dodane) konwertujemy z pliku na strumień danych.
        //Jeżeli zapytanie zostanie wykonane - zapisujemy w sesji, że książka została zedytowana oraz przekierowujemy z powrotem do panelu administracyjnego.
        //Jeżeli wystąpi bład - wypisujemy w labelce komunikat o błędzie.
        protected void btnSave_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(strSqlCon);
            try
            {
                lblError.Visible = false;
                connection.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Ksiazki SET TYTUL = @tytul, AUTOR = @autor, OPIS = @opis, ILOSC_STRON = @iloscStron, MIEJSCE_WYDANIA = @miejsceWydania, WYDAWNICTWO = @wydawnictwo, ROK_WYDANIA = @rokWydania"
                    + (FileUpload1.HasFile ? ", ZDJECIE = @okladka" : "") + " WHERE ID_KSIAZKI = @id", connection);
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                cmd.Parameters.Add("@tytul", SqlDbType.VarChar).Value = txbTytul.Text;
                cmd.Parameters.Add("@autor", SqlDbType.VarChar).Value = txbAutor.Text;
                cmd.Parameters.Add("@opis", SqlDbType.VarChar).Value = txbOpis.Text;
                cmd.Parameters.Add("@iloscStron", SqlDbType.Int).Value = txbIloscStron.Text;
                cmd.Parameters.Add("@miejsceWydania", SqlDbType.VarChar).Value = txbMiejsceWydania.Text;
                cmd.Parameters.Add("@wydawnictwo", SqlDbType.VarChar).Value = txbWydawnictwo.Text;
                cmd.Parameters.Add("@rokWydania", SqlDbType.Int).Value = txbRokWydania.Text;
                if (FileUpload1.HasFile)
                {
                    HttpPostedFile file = FileUpload1.PostedFile;
                    byte[] img = new byte[file.ContentLength];
                    file.InputStream.Read(img, 0, file.ContentLength);
                    cmd.Parameters.Add("@okladka", SqlDbType.Image).Value = img;
                }
                cmd.ExecuteNonQuery();
                connection.Close();
                Session["Edit"] = "edited";
                Response.Redirect("AdminPanel.aspx");
            }
            catch
            {
                lblError.Visible = true;
            }
            finally
            {
                if (connection != null) connection.Close();
            }

        }


    }
}