using System;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using StefaniniTestProject.Models;
using StefaniniTestProject.Helpers;
using System.Text;

namespace StefaniniTestProject.Repositories
{
    public class CustomerRepository
    {
        public List<CustomerViewModel> GetCustomers(SearchCustomerViewModel model, string userEmail)
        {
            model = model ?? new SearchCustomerViewModel();
            try
            {
                using (var conn = new DbConnection())
                {
                    var cmd = new SqlCommand() { Connection = conn.Connection };
                    StringBuilder query = new StringBuilder();
                    bool isAdmin = new LoginRepository().IsAdmin(userEmail);
                    string sellerId = isAdmin ? model.SellerId : new SellerRepository().GetIdByEmail(userEmail);
                    query.AppendLine(@"SELECT CLI.[Name], CLI.[Phone], CLI.[Gender], CLI.[LastPurchase], CLI.[SellerId], REG.[RegionName], CITY.[CityName], CLASS.[ClassificationName], U.[Name] UserName
                                        FROM [dbo].[Client] CLI
                                        INNER JOIN [dbo].[Classification] CLASS on CLASS.[ClassificationId] = CLI.[ClassificationId]
                                        INNER JOIN [dbo].[Region] REG on REG.[RegionId] = CLI.[REgionId]
                                        INNER JOIN [dbo].[City] CITY on CITY.[CityId] = REG.[CityId]
                                        INNER JOIN [dbo].[User] U on CLI.[SellerId] = U.[UserId]");
                    bool whereAdded = false;
                    if (!String.IsNullOrWhiteSpace(model.Name))
                    {
                        if (!whereAdded)
                        {
                            query.AppendLine(" WHERE ");
                            whereAdded = true;
                        }
                        else
                        {
                            query.AppendLine(" AND ");
                        }
                        query.AppendLine(" CLI.[Name] LIKE '%' + @name + '%'");
                        cmd.Parameters.AddWithValue("name", model.Name);
                    }
                    if (model.Gender.HasValue)
                    {
                        if (!whereAdded)
                        {
                            query.AppendLine(" WHERE ");
                            whereAdded = true;
                        }
                        else
                        {
                            query.AppendLine(" AND ");
                        }
                        query.AppendLine(@" CLI.[Gender] = @gender");
                        cmd.Parameters.AddWithValue("gender", model.Gender.Value == Gender.Female ? "F" : model.Gender.Value == Gender.Male ? "M" : "");
                    }
                    if (!String.IsNullOrWhiteSpace(model.RegionId))
                    {
                        if (!whereAdded)
                        {
                            query.AppendLine(" WHERE ");
                            whereAdded = true;
                        }
                        else
                        {
                            query.AppendLine(" AND ");
                        }
                        query.AppendLine(@" CLI.[RegionId] = @regionId");
                        cmd.Parameters.AddWithValue("regionId", model.RegionId);
                    }
                    if (!String.IsNullOrWhiteSpace(model.CityId))
                    {
                        if (!whereAdded)
                        {
                            query.AppendLine(" WHERE ");
                            whereAdded = true;
                        }
                        else
                        {
                            query.AppendLine(" AND ");
                        }
                        query.AppendLine(@" CITY.[CityId] = @cityId");
                        cmd.Parameters.AddWithValue("cityId", model.CityId);
                    }
                    if (model.LastPurchase.HasValue)
                    {
                        if (!whereAdded)
                        {
                            query.AppendLine(" WHERE ");
                            whereAdded = true;
                        }
                        else
                        {
                            query.AppendLine(" AND ");
                        }
                        query.AppendLine(@" CLI.[LastPurchase] BETWEEN @lastPurchase AND @until");
                        cmd.Parameters.AddWithValue("lastPurchase", model.LastPurchase);
                        cmd.Parameters.AddWithValue("until", model.Until.HasValue ? model.Until.Value : DateTime.Today);
                    }
                    if (!String.IsNullOrWhiteSpace(model.ClassificationId))
                    {
                        if (!whereAdded)
                        {
                            query.AppendLine(" WHERE ");
                            whereAdded = true;
                        }
                        else
                        {
                            query.AppendLine(" AND ");
                        }
                        query.AppendLine(@" CLASS.[ClassificationId] = @classificationId");
                        cmd.Parameters.AddWithValue("classificationId", model.ClassificationId);
                    }
                    if (!String.IsNullOrWhiteSpace(sellerId))
                    {
                        if (!whereAdded)
                        {
                            query.AppendLine(" WHERE ");
                            whereAdded = true;
                        }
                        else
                        {
                            query.AppendLine(" AND ");
                        }
                        query.AppendLine(@" [SellerId] = @sellerId");
                        cmd.Parameters.AddWithValue("sellerId", sellerId);
                    }
                    cmd.CommandText = query.ToString();
                    cmd.Connection.Open();
                    Gender result;
                    return new List<CustomerViewModel>(cmd.ExecuteReader().Select(r => new CustomerViewModel()
                    {
                        Name = (r["Name"] as string),
                        Phone = (r["Phone"] as string),
                        Gender = (r["Gender"] as string) == "M" ? "Male" : (r["Gender"] as string) == "F" ? "Female" : null,
                        LastPurchase = (r["LastPurchase"] as Nullable<DateTime>).HasValue ? (r["LastPurchase"] as Nullable<DateTime>).Value.ToShortDateString() : null,
                        Classification = (r["ClassificationName"] as string),
                        Region = (r["RegionName"] as string),
                        City = (r["CityName"] as string),
                        Seller = (r["UserName"] as string)
                    }));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}