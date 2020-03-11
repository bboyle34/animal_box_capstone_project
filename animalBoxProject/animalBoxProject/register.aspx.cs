using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

public partial class register : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //github training
    }

    protected void btnCreate_Click(object sender, EventArgs e)
    {
        if (isPasswordValid())
        {
            if (txtEmail.Text.ToString().Equals("", StringComparison.OrdinalIgnoreCase)
                && txtPassword.Text.ToString().Equals("", StringComparison.OrdinalIgnoreCase)
                && txtAddress.Text.ToString().Equals("", StringComparison.OrdinalIgnoreCase)
                && txtCity.Text.ToString().Equals("", StringComparison.OrdinalIgnoreCase)
                && txtZip.Text.ToString().Equals("", StringComparison.OrdinalIgnoreCase))
            {
                errorMessage.Text = "Please completely fill out the form";
            }
            else
            {
                try
                {
                    System.Data.SqlClient.SqlConnection sc = new System.Data.SqlClient.SqlConnection();
                    sc.ConnectionString = ConfigurationManager.ConnectionStrings["myRDSinstance"].ConnectionString;
                    
                    sc.Open();
                    System.Data.SqlClient.SqlCommand insert = new System.Data.SqlClient.SqlCommand();
                    insert.Connection = sc;
                    insert.CommandText = "insert into Person values(@Email, @Address, @Address2, @City, @State, @Zip)";
                    insert.Parameters.AddWithValue("@Email", txtEmail.Text);
                    insert.Parameters.AddWithValue("@Address", txtAddress.Text);
                    if (txtAddress2.Text.ToString().Equals("", StringComparison.OrdinalIgnoreCase))
                    {
                        insert.Parameters.AddWithValue("@Address2", DBNull.Value);
                    }
                    else
                    {
                        insert.Parameters.AddWithValue("Address2", txtAddress2);
                    }
                    insert.Parameters.AddWithValue("@City", txtCity.Text);
                    if (txtState.Text.ToString().Equals("", StringComparison.OrdinalIgnoreCase))
                    {
                        insert.Parameters.AddWithValue("@State", DBNull.Value);
                    }
                    else
                    {
                        insert.Parameters.AddWithValue("@State", txtState.Text);
                    }
                    insert.Parameters.AddWithValue("@Zip", txtZip.Text);
                    insert.ExecuteNonQuery();

                    System.Data.SqlClient.SqlCommand setPass = new System.Data.SqlClient.SqlCommand();
                    setPass.Connection = sc;
                    setPass.CommandText = "insert into Pass values((select max(userid) from person), @Email, @Password)";
                    setPass.Parameters.AddWithValue("@Email", txtEmail.Text);
                    setPass.Parameters.AddWithValue("@Password", PasswordHash.HashPassword(txtPassword.Text));
                    setPass.ExecuteNonQuery();

                    sc.Close();
                    errorMessage.Text = "Commit Successful";
                }
                catch (Exception g)
                {
                    errorMessage.Text = g.ToString();
                }
            }
        }
    }

    protected Boolean isPasswordValid()
    {
        var input = txtPassword.Text;
        var hasNumber = new Regex(@"[0-9]+");
        var hasUpperChar = new Regex(@"[A-Z]+");
        var hasSpecialChar = new Regex(@"[!@#$&*]+");
        var hasMin6Char = new Regex(@".{6,}");

        Boolean validated = false;
        if (hasNumber.IsMatch(input) && hasUpperChar.IsMatch(input) && hasSpecialChar.IsMatch(input) && hasMin6Char.IsMatch(input))
        {
            validated = true;
        }
        if (validated)
        {
            errorMessage.Text = "Password meets all requirements!";
        }
        else if (!validated)
        {
            errorMessage.Text = "Please make sure your password meest the requirements: \n" +
                    "- At least one lowercase letter\n" +
                    "- At least one number\n" +
                    "- At least one Special character\n" +
                    "- And a length of at least 6";
        }
        return validated;
    }
}