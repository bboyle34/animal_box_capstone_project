using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Data;

public partial class passwordReset : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int userID = int.Parse(Request.QueryString["variable"]);
        user.Text = getUserFromEmail(getEmail(userID));
        String foobar = Convert.ToString(Request.QueryString["hash"]);
        String nowDate = correctDate();


        if (!checkLinkValidity(userID, foobar, nowDate))
        {
            txtConfirmNewPassword.Enabled = false;
            txtNewPassword.Enabled = false;
            btnNewPassword.Enabled = false;
            //errorMessage.Text = "Link has expired.";
        }
    }
    protected int getID()
    {
        int userID = int.Parse(Request.QueryString["variable"]);
        return userID;
    }
    protected String correctDate()
    {
        String answer = "";
        answer += DateTime.Now.Year;
        if (int.Parse(DateTime.Now.Month.ToString()) < 10)
        {
            answer += "0" + DateTime.Now.Month.ToString();
        }
        else
        {
            answer += DateTime.Now.Month.ToString();
        }
        if (int.Parse(DateTime.Now.Day.ToString()) < 10)
        {
            answer += "0" + DateTime.Now.Day.ToString();
        }
        else
        {
            answer += DateTime.Now.Day.ToString();
        }
        if (int.Parse(DateTime.Now.Hour.ToString()) < 10)
        {
            answer += "0" + DateTime.Now.Hour.ToString();
        }
        else
        {
            answer += DateTime.Now.Hour.ToString();
        }
        if (int.Parse(DateTime.Now.Minute.ToString()) < 10)
        {
            answer += "0" + DateTime.Now.Minute.ToString();
        }
        else
        {
            answer += DateTime.Now.Minute.ToString();
        }
        return answer;
    }
    protected Boolean checkLinkValidity(int userID, String foobar, String nowDate)
    {
        Boolean answer = false;
        //String randomHash = "";
        //String dateStamp = "";
        //dateStamp + 10 < double.Parse(nowDate)
        System.Data.SqlClient.SqlConnection sc = new System.Data.SqlClient.SqlConnection();
        sc.ConnectionString = ConfigurationManager.ConnectionStrings["myRDSinstance"].ConnectionString;

        String getHash = "select * from recovery where userID = @userID;";
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(getHash, sc);
        cmd.Parameters.AddWithValue("@userID", userID);
        //String getDate = "select dateStamp from recovery where userID = @userID";
        //System.Data.SqlClient.SqlCommand cmd2 = new System.Data.SqlClient.SqlCommand(getDate, sc);
        //cmd2.Parameters.AddWithValue("@userID", userID);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        sc.Open();
        foreach(DataRow dr in dt.Rows)
        {
            answer = HashAndDateCheck(foobar, nowDate, dr["randomHash"].ToString(), dr["dateStamp"].ToString());
            if (answer)
            {
                break;
            }
        }
        //using (SqlDataReader rdr = cmd.ExecuteReader())
        //{
        //    while (rdr.Read() && !answer)
        //    {
        //        randomHash = rdr[1].ToString();
        //        dateStamp = rdr[2].ToString();
        //        if (foobar.Equals(randomHash, StringComparison.Ordinal))
        //        {
        //            if (double.Parse(dateStamp) + 10 < double.Parse(nowDate))
        //            {
        //                errorMessage.Text = "Link has valid hash and datestamp";
        //                answer = true;
        //                break;
        //            }
        //            else
        //            {
        //                errorMessage.Text = "Link has expired";
        //            }
        //        }
        //        else
        //        {
        //            errorMessage.Text = "Hash is invalid";
        //        }
        //    }
        //}
        sc.Close();
        return answer;
    }
    protected Boolean HashAndDateCheck(String foobar, String nowDate, String randomHash, String dateStamp)
    {
        Boolean answer = false;
        int count = 0;
        //time.Text = "" + double.Parse(dateStamp) + " vs " + double.Parse(nowDate) + "";
        if (foobar.Equals(randomHash, StringComparison.Ordinal) && !answer)
        {
            count++;
            if (double.Parse(dateStamp) > (double.Parse(nowDate)))
            {
                //errorMessage.Text = "Link has valid hash and datestamp";
                answer = true;
            }
            else
            {
                errorMessage.Text = "Link has expired";
            }
        }
        else if (!answer && count > 0)
        {
            errorMessage.Text = "Hash is invalid";
        }
        return answer;
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
        // add in functionality that checks if this password is the same as the last one
        int userID = int.Parse(Request.QueryString["variable"]);
        if (isPasswordValid() && isPasswordMatch())
        {
            if (checkLastPassword())
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
    protected Boolean isPasswordMatch()
    {
        Boolean answer = false;
        if (!txtNewPassword.Text.ToString().Equals(txtConfirmNewPassword.Text.ToString(), StringComparison.OrdinalIgnoreCase))
        {
            //errorMessage.Text = "Please completely fill out the form";
            passwordMatch.Text = "Passwords do not match.";
        }
        else
        {
            answer = true;
        }
        return answer;
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
    protected Boolean checkLastPassword()
    {
        Boolean answer = true;
        //String newPasswordHash = PasswordHash.HashPassword(txtConfirmNewPassword.Text.ToString());
        //int oldPasswordHash = 0;
        //int userID = getID();
        //System.Data.SqlClient.SqlConnection sc = new System.Data.SqlClient.SqlConnection();
        //sc.ConnectionString = ConfigurationManager.ConnectionStrings["myRDSinstance"].ConnectionString;

        //string getHash = "select PasswordHash from Pass where userID = @userID;";
        //System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(getHash, sc);
        //cmd.Parameters.AddWithValue("@userID", ID);

        //sc.Open();
        //oldPasswordHash = Convert.ToInt32(cmd.ExecuteScalar());
        //sc.Close();

        //if (newPasswordHash.Equals(oldPasswordHash.ToString(), StringComparison.Ordinal))
        //{
        //    answer = false;
        //    errorMessage.Text = "New Password cannot be the same as the Old Password.";
        //}
        //else
        //{
        //    answer = true;
        //}

        return answer;
    }

    protected void txtNewPassword_Changed(object sender, EventArgs e)
    {
        if(isPasswordValid())
        {
            passwordReqs.Text = "Password Meets all requirements.";
        }
    }

    protected void confirmNewPassword_Changed(object sender, EventArgs e)
    {
        if(isPasswordMatch())
        {
            passwordMatch.Text = "Passwords match";
        }
        else
        {
            passwordMatch.Text = "Passwords do not match";
        }
    }
}