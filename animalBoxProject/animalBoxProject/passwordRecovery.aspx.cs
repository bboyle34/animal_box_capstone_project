using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Net.Mail;
using System.Drawing;
using System.Configuration;
using System.Data.SqlClient;

public partial class passwordRecovery : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //errorMessage.Text = "" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + "";
        //errorMessage.Text = generateRandomHash();
        errorMessage.Text = correctDate();
    }

    protected void btnRecoverPass_Clicked(object sender, EventArgs e)
    {
        Boolean validEmail = false;
        int userID = 0;
        String userEmail = txtUserEmail.Text.ToString();
        System.Data.SqlClient.SqlConnection sc = new System.Data.SqlClient.SqlConnection();
        sc.ConnectionString = ConfigurationManager.ConnectionStrings["myRDSinstance"].ConnectionString;
        string checkForEmail = "select UserID from person where email like @email;";
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(checkForEmail, sc);
        cmd.Parameters.AddWithValue("@email", userEmail);
        try
        {
            sc.Open();
            userID = int.Parse(Convert.ToString(cmd.ExecuteScalar()));
            sc.Close();
            //errorMessage.Text = "Email was correct";
            //Session["userIDPassReset"] = userID;
            //Session["userEmailPassReset"] = getEmail(userID).ToString();
            validEmail = true;
        }
        catch(Exception g)
        {
            errorMessage.ForeColor = Color.Red;
            errorMessage.Text = "Please provide a valid email";
        }
        if (validEmail)
        {
            //client.U
            //for (int i = 0; i < )
            string foobar = generateRandomHash(userID);
            string[] userCreds = new string[2];
            userCreds[0] = getEmail(userID);
            userCreds[1] = getPassword(userID);
            MailMessage mm = new MailMessage("sender@gmail.com", userCreds[0]);
            mm.Subject = "Servitae Password Recovery";
            //mm.Body = string.Format("Hi " + userCreds[0] + ",<br /><br />Your password is " + userCreds[1] + ".<br /><br />Thank you");
            //http://localhost:60998/passwordRecovery.aspx
            mm.Body = string.Format("Hi " + userCreds[0] + ",<br /><br />Please click <a href=\"" + "http://localhost:60998/passwordReset.aspx?variable=" + userID + "&hash=" + foobar + "\">here</a>." +
                "to reset your Password.<br /><br />If you did not request a new password, please ignore this email.<br /><br /> Thank you,<br /><br /> Servitae, by Evergreen Consulting");
            mm.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            NetworkCredential NetworkCred = new NetworkCredential();
            NetworkCred.UserName = "servitaePassRecovery@gmail.com";
            NetworkCred.Password = "arde321!";
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = NetworkCred;
            smtp.Port = 587;
            smtp.Send(mm);
            errorMessage.ForeColor = Color.Green;
            errorMessage.Text = "Password has been sent to your email address.";
            btnRecoverPass.Enabled = false;
        }
    }
    protected string getEmail(int userID)
    {
        //string[] userCreds = new string[2];
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
    protected string getPassword(int userID)
    {
        int ID = userID;
        System.Data.SqlClient.SqlConnection sc = new System.Data.SqlClient.SqlConnection();
        sc.ConnectionString = ConfigurationManager.ConnectionStrings["myRDSinstance"].ConnectionString;

        string getPassword = "select PasswordHash from pass where userID = @userID;";
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(getPassword, sc);
        cmd.Parameters.AddWithValue("@userID", ID);

        sc.Open();
        String password = Convert.ToString(cmd.ExecuteScalar());
        sc.Close();

        return password;
    }
    protected string generateRandomHash(int userID)
    {
        //const string characters = "0123456789abcdefghijklmnpqrstuvwxyzABCDEFGHIJKLMNPQRSTUVWXYZ!@#$%^&*()";
        //string answer = "";
        //var rand = new Random();
        //for (int i = 0; i < characters.Length; i++)
        //{
        //    answer += characters[Math.Floor(rand.Next() * characters.Length)];
        //}
        //return answer;
        Guid obj = Guid.NewGuid();
        String answer = obj.ToString();
        addHashToDB(answer, userID);
        return answer;
    }
    protected void addHashToDB(String hash, int userID)
    {
        string dateStamp = correctDate();
        try
        {
            System.Data.SqlClient.SqlConnection sc = new System.Data.SqlClient.SqlConnection();
            sc.ConnectionString = ConfigurationManager.ConnectionStrings["myRDSinstance"].ConnectionString;

            string insert = "insert into recovery values(@userID, @randomHash, @dateStamp)";
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(insert, sc);
            cmd.Parameters.AddWithValue("@userID", userID);
            cmd.Parameters.AddWithValue("@randomHash", hash);
            cmd.Parameters.AddWithValue("@dateStamp", dateStamp);

            sc.Open();
            cmd.ExecuteNonQuery();
            sc.Close();
            errorMessage.Text = "recovery insert successful";
        }
        catch (Exception g)
        {
            errorMessage.Text = g.ToString();
        }
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
            answer += int.Parse(DateTime.Now.Minute.ToString()) + 10;
        }
        else
        {
            answer += int.Parse(DateTime.Now.Minute.ToString()) + 10;
        }
        return answer;
    }
}