using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Configuration;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Data;
using System.Collections;
using System.Text;

public partial class index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Array urls = getUrl();
        Array messages = getMessage();
        Array latitudes = getLatitude();
        Array longitudes = getLongitude();

        StringBuilder sb = new StringBuilder();
        sb.Append("<script>");
        sb.Append("var sites = new Array;");
        sb.Append("var messages = new Array;");
        sb.Append("var latitudes = new Array;");
        sb.Append("var longitudes = new Array;");
        foreach (string str in urls)
        {
            sb.Append("sites.push('" + str + "');");
        }
        foreach (string str in messages)
        {
            sb.Append("messages.push('" + str + "');");
        }
        foreach (double num in latitudes)
        {
            sb.Append("latitudes.push(" + num + ");");
        }
        foreach (double num in longitudes)
        {
            sb.Append("longitudes.push(" + num + ");");
        }
        sb.Append("</script>");
        ClientScript.RegisterStartupScript(this.GetType(), "TestArrayScript", sb.ToString());

    }
    [WebMethod]
    public static Array getUrl()
    {
        ArrayList answer = new ArrayList();
        System.Data.SqlClient.SqlConnection sc = new System.Data.SqlClient.SqlConnection();
        sc.ConnectionString = ConfigurationManager.ConnectionStrings["myRDSinstance"].ConnectionString;

        String getHash = "select url from device";
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(getHash, sc);
        //String getDate = "select dateStamp from recovery where userID = @userID";
        //System.Data.SqlClient.SqlCommand cmd2 = new System.Data.SqlClient.SqlCommand(getDate, sc);
        //cmd2.Parameters.AddWithValue("@userID", userID);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        sc.Open();
        foreach (DataRow dr in dt.Rows)
        {
            answer.Add(dr["url"].ToString());
        }
        return answer.ToArray();
    }
    protected Array getMessage()
    {
        ArrayList answer = new ArrayList();
        System.Data.SqlClient.SqlConnection sc = new System.Data.SqlClient.SqlConnection();
        sc.ConnectionString = ConfigurationManager.ConnectionStrings["myRDSinstance"].ConnectionString;

        String getHash = "select message from device";
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(getHash, sc);
        //String getDate = "select dateStamp from recovery where userID = @userID";
        //System.Data.SqlClient.SqlCommand cmd2 = new System.Data.SqlClient.SqlCommand(getDate, sc);
        //cmd2.Parameters.AddWithValue("@userID", userID);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        sc.Open();
        foreach (DataRow dr in dt.Rows)
        {
            answer.Add(dr["message"].ToString());
        }
        return answer.ToArray();
    }
    protected Array getLatitude()
    {
        ArrayList answer = new ArrayList();
        System.Data.SqlClient.SqlConnection sc = new System.Data.SqlClient.SqlConnection();
        sc.ConnectionString = ConfigurationManager.ConnectionStrings["myRDSinstance"].ConnectionString;

        String getHash = "select latitude from device";
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(getHash, sc);
        //String getDate = "select dateStamp from recovery where userID = @userID";
        //System.Data.SqlClient.SqlCommand cmd2 = new System.Data.SqlClient.SqlCommand(getDate, sc);
        //cmd2.Parameters.AddWithValue("@userID", userID);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        sc.Open();
        foreach (DataRow dr in dt.Rows)
        {
            answer.Add(double.Parse(dr["latitude"].ToString()));
        }
        return answer.ToArray();
    }
    protected Array getLongitude()
    {
        ArrayList answer = new ArrayList();
        System.Data.SqlClient.SqlConnection sc = new System.Data.SqlClient.SqlConnection();
        sc.ConnectionString = ConfigurationManager.ConnectionStrings["myRDSinstance"].ConnectionString;

        String getHash = "select longitude from device";
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(getHash, sc);
        //String getDate = "select dateStamp from recovery where userID = @userID";
        //System.Data.SqlClient.SqlCommand cmd2 = new System.Data.SqlClient.SqlCommand(getDate, sc);
        //cmd2.Parameters.AddWithValue("@userID", userID);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        sc.Open();
        foreach (DataRow dr in dt.Rows)
        {
            answer.Add(double.Parse(dr["longitude"].ToString()));
        }
        return answer.ToArray();
    }
}