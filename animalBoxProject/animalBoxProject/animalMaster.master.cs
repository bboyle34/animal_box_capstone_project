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
        //this is code that tried to keep the Modal open when incorrect user info was presented
        //if (!IsPostBack)
        //{
        //    Session["correctLogin"] = "true";
        //}
        //else
        //{
        //    if (Session["correctLogin"].ToString().Equals("false", StringComparison.OrdinalIgnoreCase))
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        //    }
        //}
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
                        Session["userEmail"] = getUserFromEmail(txtUserEmail.Text.ToString());
                        Response.Redirect("admin.aspx");
                    }
                    else
                    {
                        errorMessage.Text = "Invalid Password";
                        failedLogin("Password is wrong.");
                    }
                }
            }
            else // if the username doesn't exist, it will show failure
            {
                errorMessage.Text = "Invalid User";
                failedLogin("Login failed.");
            }

            sc.Close();
        }
        catch (Exception g)
        {
            errorMessage.Text = g.ToString();
            failedLogin(g.ToString());
        }
    }
    protected String getUserFromEmail(String email)
    {
        String answer = "";
        char[] charArray = email.ToCharArray();
        for (int i = 0; i < email.Length; i++)
        {
            if (charArray[i].ToString().Equals("@", StringComparison.OrdinalIgnoreCase))
            {
                break;
            }
            else
            {
                answer += charArray[i];
            }
        }
        return answer;
    }
    protected void failedLogin(String error)
    {
        //Page.Response.Redirect(Page.Request.Url.ToString(), true);
        //Session["correctLogin"] = "false";
    }
}
