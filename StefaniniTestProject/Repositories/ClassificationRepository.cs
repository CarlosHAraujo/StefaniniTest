using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using StefaniniTestProject.Models;
using StefaniniTestProject.Helpers;

namespace StefaniniTestProject.Repositories
{
    public class ClassificationRepository
    {
        public IEnumerable<ClassificationViewModel> GetClassifications()
        {
            try
            {
                using (var conn = new DbConnection())
                {
                    var cmd = new SqlCommand("SELECT [ClassificationId], [ClassificationName] FROM [dbo].[Classification]", conn.Connection);
                    conn.Connection.Open();
                    var result = cmd.ExecuteReader().Select(r => new ClassificationViewModel() { Id = (r["ClassificationId"] is DBNull ? null : r["ClassificationId"].ToString()), Name = (r["ClassificationName"] as string) });
                    return new List<ClassificationViewModel>(result);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}