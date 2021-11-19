using NoroffAssignment2.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoroffAssignment2.DataAccess
{
    public class CustomerSpenderRepository : ICustomerSpenderRepository
    {
        public SqlConnectionStringBuilder Builder { get; init; }

        public CustomerSpenderRepository()
        {
            Builder = new() { DataSource = "DESKTOP-UD2KPSV\\SQLEXPRESS", InitialCatalog = "Chinook", IntegratedSecurity = true };
        }

        public IEnumerable<CustomerSpenderModel> GetAll()
        {
            List<CustomerSpenderModel> returnList = new();
            try
            {
                using SqlConnection connection = new(Builder.ConnectionString);
                connection.Open();

                string sql = "SELECT CustomerId, SUM(Total) AS tot FROM Invoice Group BY CustomerId Order BY ToT DESC";

                using SqlCommand command = new(sql, connection);
                using SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    CustomerSpenderModel customerSpender = new()
                    {
                        CustomerId = reader.Get<int>("CustomerId"),
                        TotalSpendt = reader.Get<decimal>("ToT")
                       // TotalSpendt = reader.Get<double>("ToT")
                    };
                    returnList.Add(customerSpender);
                }
            }
            catch (SqlException exception)
            {
                Console.WriteLine("Sql exception msg:" + exception.Message);
            }
            return returnList;
        }

        public IEnumerable<CustomerSpenderModel> GetPage(int limit, int offset)
        {
            List<CustomerSpenderModel> returnList = new();
            try
            {
                using SqlConnection connection = new(Builder.ConnectionString);
                connection.Open();

                string sql = "SELECT CustomerId, SUM(Total) AS ToT FROM Invoice GROUP BY CustomerId ORDER BY ToT DESC OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY";

                using SqlCommand command = new(sql, connection);
                command.Parameters.AddWithValue("@offset", offset);
                command.Parameters.AddWithValue("@limit", limit);
                using SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    CustomerSpenderModel customerSpender = new()
                    {
                        CustomerId = reader.Get<int>("CustomerId"),
                        TotalSpendt = reader.Get<decimal>("ToT")
                    };
                    returnList.Add(customerSpender);
                }
            }
            catch (SqlException exception)
            {
                Console.WriteLine("Sql exception msg:" + exception.Message);
            }
            return returnList;
        }
    }
}
