using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

public partial class animalMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        try
        {
            System.Data.SqlClient.SqlConnection sc = new System.Data.SqlClient.SqlConnection();
            sc.ConnectionString = ConfigurationManager.ConnectionStrings["myRDSinstance"].ConnectionString;

            sc.Open();
            System.Data.SqlClient.SqlCommand findPass = new System.Data.SqlClient.SqlCommand();
            findPass.Connection = sc;
            // SELECT PASSWORD STRING WHERE THE ENTERED USERNAME MATCHES
            findPass.CommandText = "select PasswordHash from Pass where Email = @Email";
            findPass.Parameters.AddWithValue("@Email", txtUserEmail.Text);
            SqlDataReader reader = findPass.ExecuteReader(); // create a reader

            if (reader.HasRows) // if the username exists, it will continue
            {
                while (reader.Read()) // this will read the single record that matches the entered username
                {
                    string storedHash = reader["PasswordHash"].ToString(); // store the database password into this variable

                    if (PasswordHash.ValidatePassword(txtUserPass.Text, storedHash)) // if the entered password matches what is stored, it will show success
                    {
                        errorMessage.Text = "Success!";
                        Response.Redirect("admin.aspx");
                    }
                    else
                    {
                        errorMessage.Text = "Password is wrong.";
                    }
                }
            }
            else // if the username doesn't exist, it will show failure
            {
                errorMessage.Text = "Login failed.";
            }

            sc.Close();
        }
        catch (Exception g)
        {
            errorMessage.Text = g.ToString();
        }
    }
}
