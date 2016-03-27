using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using StefaniniTestProject.Models;
using StefaniniTestProject.Helpers;

namespace StefaniniTestProject.Repositories
{
    public class RegionRepository
    {
        public IEnumerable<RegionViewModel> GetRegions(string cityId)
        {
            if (String.IsNullOrWhiteSpace(cityId))
            {
                return new List<RegionViewModel>();
            }
            try
            {
                using (var conn = new DbConnection())
                {
                    var cmd = new SqlCommand("SELECT [RegionId], [RegionName] FROM [dbo].[Region] WHERE [CityId] = @cityId", conn.Connection);
                    cmd.Parameters.AddWithValue("cityId", cityId);
                    conn.Connection.Open();
                    var result = cmd.ExecuteReader().Select(r => new RegionViewModel() { Id = (r["RegionId"] is DBNull ? null : r["RegionId"].ToString()), Name = (r["RegionName"] as string) });
                    return new List<RegionViewModel>(result);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}