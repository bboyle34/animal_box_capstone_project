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
        errorMessage.Text = "" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + "";
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
            string dateStamp = "" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + "";
            string[] userCreds = new string[2];
            userCreds[0] = getEmail(userID);
            userCreds[1] = getPassword(userID);
            MailMessage mm = new MailMessage("sender@gmail.com", userCreds[0]);
            mm.Subject = "Servitate Password Recovery";
            //mm.Body = string.Format("Hi " + userCreds[0] + ",<br /><br />Your password is " + userCreds[1] + ".<br /><br />Thank you");
            //http://localhost:60998/passwordRecovery.aspx
            mm.Body = string.Format("Hi " + userCreds[0] + ",<br /><br />Please click <a href=\"" + "http://localhost:60998/passwordReset.aspx?variable=" + userID + "&dateVar=" + dateStamp + "\">here</a>." +
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
}