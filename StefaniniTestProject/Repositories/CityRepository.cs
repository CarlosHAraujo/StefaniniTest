using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using StefaniniTestProject.Models;
using StefaniniTestProject.Helpers;

namespace StefaniniTestProject.Repositories
{
    public class CityRepository
    {
        public IEnumerable<CityViewModel> GetCities()
        {
            try
            {
                using (var conn = new DbConnection())
                {
                    var cmd = new SqlCommand("SELECT [CityId], [CityName] FROM [dbo].[City]", conn.Connection);
                    conn.Connection.Open();
                    var result = cmd.ExecuteReader().Select(r => new CityViewModel() { Id = (r["CityId"] is DBNull ? null : r["CityId"].ToString()), Name = (r["CityName"] as string) });
                    return new List<CityViewModel>(result);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}