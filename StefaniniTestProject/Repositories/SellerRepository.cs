using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using StefaniniTestProject.Models;
using StefaniniTestProject.Helpers;

namespace StefaniniTestProject.Repositories
{
    public class SellerRepository
    {
        public string GetIdByEmail(string userEmail)
        {
            if (String.IsNullOrWhiteSpace(userEmail))
            {
                return null;
            }
            try
            {
                using (var conn = new DbConnection())
                {
                    var cmd = new SqlCommand("SELECT [UserId] FROM [dbo].[User] WHERE [Email] = @userEmail", conn.Connection);
                    cmd.Parameters.AddWithValue("userEmail", userEmail);
                    conn.Connection.Open();
                    object result = cmd.ExecuteScalar();
                    return result is DBNull ? null : result.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SellerViewModel> GetSellers()
        {
            try
            {
                using (var conn = new DbConnection())
                {
                    var cmd = new SqlCommand("SELECT [UserId], [Name] FROM [dbo].[User] WHERE [Role] = 'Seller'", conn.Connection);
                    conn.Connection.Open();
                    var result = cmd.ExecuteReader().Select(r => new SellerViewModel() { Id = (r["UserId"] is DBNull ? null : r["UserId"].ToString()), Name = (r["Name"] as string) });
                    return new List<SellerViewModel>(result);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}