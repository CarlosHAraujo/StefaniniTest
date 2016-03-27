using System;
using System.Data.SqlClient;

namespace StefaniniTestProject.Repositories
{
    public class LoginRepository
    {
        public bool IsAdmin(string userEmail)
        {
            try
            {
                using (var conn = new DbConnection())
                {
                    var cmd = new SqlCommand("SELECT [Role] FROM [dbo].[User] WHERE [Email] = @userEmail", conn.Connection);
                    cmd.Parameters.AddWithValue("userEmail", userEmail);
                    conn.Connection.Open();
                    string role = cmd.ExecuteScalar() as string;
                    return role == "Administrator";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool CanLogin(string email, string password, out string error)
        {
            try
            {
                using (var conn = new DbConnection())
                {
                    var cmd = new SqlCommand("SELECT [Email], [Password] FROM [dbo].[User] WHERE [Email] = @email AND [Password] = @password", conn.Connection);
                    cmd.Parameters.AddWithValue("email", email);
                    cmd.Parameters.AddWithValue("password", password);
                    conn.Connection.Open();
                    SqlDataReader cmdExecuteReader = cmd.ExecuteReader();
                    if (cmdExecuteReader.HasRows)
                    {
                        error = null;
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
            error = "The email and/or password entered is invalid. Please try again.";
            return false;
        }
    }
}