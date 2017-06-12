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
    public partial class BookAdd : System.Web.UI.Page
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
        //Tworzymy nowe zapytanie dodające dane do bazy.
        //Wszystkie dane wpisane w TextBoxy dodajemy do zapytania, a zdjęcie konwertujemy z pliku na strumień danych.
        //Jeżeli zapytanie zostanie wykonane - zapisujemy w sesji, że książka została dodana oraz przekierowujemy z powrotem do panelu administracyjnego.
        //Jeżeli wystąpi bład - wypisujemy w labelce komunikat o błędzie.
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(strSqlCon);
            try
            {
                lblError.Visible = false;             
                HttpPostedFile file = FileUpload1.PostedFile;
                byte[] img = new byte[file.ContentLength];
                file.InputStream.Read(img, 0, file.ContentLength);
                connection.Open();
                SqlCommand cmd = new SqlCommand("INSERT into Ksiazki VALUES (@tytul, @autor, @opis, @iloscStron, @miejsceWydania, @wydawnictwo, @rokWydania, @okladka)", connection);
                cmd.Parameters.Add("@tytul", SqlDbType.VarChar).Value = txbTytul.Text;
                cmd.Parameters.Add("@autor", SqlDbType.VarChar).Value = txbAutor.Text;
                cmd.Parameters.Add("@opis", SqlDbType.VarChar).Value = txbOpis.Text;
                cmd.Parameters.Add("@iloscStron", SqlDbType.Int).Value = txbIloscStron.Text;
                cmd.Parameters.Add("@miejsceWydania", SqlDbType.VarChar).Value = txbMiejsceWydania.Text;
                cmd.Parameters.Add("@wydawnictwo", SqlDbType.VarChar).Value = txbWydawnictwo.Text;
                cmd.Parameters.Add("@rokWydania", SqlDbType.Int).Value = txbRokWydania.Text;
                cmd.Parameters.Add("@okladka", SqlDbType.Image).Value = img;
                cmd.ExecuteNonQuery();
                Session["Add"] = "added";
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