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
    public partial class Login : System.Web.UI.Page
    {
        //Pobieramy z konfiguracji string zawierający dane połączenia z bazą danych.
        string strSqlCon = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            //Jeżeli w sesji jest już zapisany login (znaczy to, że jesteśmy zalogowani) to przekierowujemy na stronę panelu administracyjnego.
            if (!(String.IsNullOrEmpty(Session["Login"] as string)))
                Response.Redirect("AdminPanel.aspx");
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

        //Zapisujemy wpisane dane do zmiennych.
        //Próbujemy znaleźć użytkownika o takich danych w bazie. 
        //Jeżeli go nie znajdziemy (NullReferenceException) - wyświetlamy błąd.
        //Jeżeli go znajdziemy - zapisujemy login do sesji oraz przekierowujemy na stronę panelu administracyjnego.
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string login = txbLogin.Text.ToString();
            string password = txbPassword.Text.ToString();
            SqlConnection connection = new SqlConnection(strSqlCon);
            try
            {
                lblError.Visible = false;
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT LOGIN FROM Uzytkownicy WHERE LOGIN = '" + login + "' AND Password = '" + password + "'", connection);
                string user = command.ExecuteScalar().ToString();
                Session["Login"] = user.ToString();
                Response.Redirect("AdminPanel.aspx");
            }
            catch (NullReferenceException)
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