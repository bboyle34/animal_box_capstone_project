﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Text.RegularExpressions;

public partial class passwordReset : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int userID = int.Parse(Request.QueryString["variable"]);
        user.Text = getUserFromEmail(getEmail(userID));
        double dateStamp = double.Parse(Request.QueryString["dateVar"]);
        String nowDate = "" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + "";
        if (dateStamp + 10 < double.Parse(nowDate))
        {
            txtConfirmNewPassword.Enabled = false;
            txtNewPassword.Enabled = false;
            btnNewPassword.Enabled = false;
            errorMessage.Text = "Link has expired.";
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

    protected void btnNewPass_Clicked(object sender, EventArgs e)
    {
        int userID = int.Parse(Request.QueryString["variable"]);
        if (isPasswordValid())
        {
            if (!txtNewPassword.Text.ToString().Equals(txtConfirmNewPassword.Text.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                //errorMessage.Text = "Please completely fill out the form";
                passwordMatch.Text = "Passwords do not match.";
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
                    //insert.CommandText = "update Pass values(@Email, @Address, @Address2, @City, @State, @Zip)";
                    //insert.Parameters.AddWithValue("@Email", txtEmail.Text);
                    //insert.Parameters.AddWithValue("@Address", txtAddress.Text);
                    //if (txtAddress2.Text.ToString().Equals("", StringComparison.OrdinalIgnoreCase))
                    //{
                    //    insert.Parameters.AddWithValue("@Address2", DBNull.Value);
                    //}
                    //else
                    //{
                    //    insert.Parameters.AddWithValue("Address2", txtAddress2);
                    //}
                    //insert.Parameters.AddWithValue("@City", txtCity.Text);
                    //if (txtState.Text.ToString().Equals("", StringComparison.OrdinalIgnoreCase))
                    //{
                    //    insert.Parameters.AddWithValue("@State", DBNull.Value);
                    //}
                    //else
                    //{
                    //    insert.Parameters.AddWithValue("@State", txtState.Text);
                    //}
                    //insert.Parameters.AddWithValue("@Zip", txtZip.Text);
                    //insert.ExecuteNonQuery();

                    System.Data.SqlClient.SqlCommand setPass = new System.Data.SqlClient.SqlCommand();
                    setPass.Connection = sc;
                    setPass.CommandText = "update Pass set PasswordHash = @Password where UserID = @UserID";
                    setPass.Parameters.AddWithValue("@Password", PasswordHash.HashPassword(txtNewPassword.Text));
                    setPass.Parameters.AddWithValue("@UserID", userID);
                    setPass.ExecuteNonQuery();

                    sc.Close();
                    //errorMessage.Text = "Commit Successful";
                    Response.Redirect("index.aspx");
                }
                catch (Exception g)
                {
                    passwordMatch.Text = g.ToString();
                }
            }
        }
    }
    protected Boolean isPasswordValid()
    {
        var input = txtNewPassword.Text;
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
            passwordReqs.Text = "Password meets all requirements!";
        }
        else if (!validated)
        {
            passwordReqs.Text = "Please make sure your password meest the requirements: \n" +
                    "- At least one lowercase letter\n" +
                    "- At least one number\n" +
                    "- At least one Special character\n" +
                    "- And a length of at least 6";
        }
        return validated;
    }
    protected string getEmail(int userID)
    {
        int ID = userID;
        System.Data.SqlClient.SqlConnection sc = new System.Data.SqlClient.SqlConnection();
        sc.ConnectionString = ConfigurationManager.ConnectionStrings["myRDSinstance"].ConnectionString;

        string getEmail = "select email from person where userID = @userID;";
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(getEmail, sc);
        cmd.Parameters.AddWithValue("@userID", ID);

        sc.Open();
        String email = Convert.ToString(cmd.ExecuteScalar());
        sc.Close();

        return email;
    }

    protected void txtNewPassword_Changed(object sender, EventArgs e)
    {
        if(isPasswordValid())
        {
            passwordReqs.Text = "Password Meets all requirements.";
        }
    }
}