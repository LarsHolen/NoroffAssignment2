using NoroffAssignment2.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoroffAssignment2.DataAccess
{
    public class CustomerCountryRepository : ICustomerCountryRepository
    {
        public SqlConnectionStringBuilder Builder { get; init; }
        public CustomerCountryRepository()
        {
            Builder = new() { DataSource = "DESKTOP-UD2KPSV\\SQLEXPRESS", InitialCatalog = "Chinook", IntegratedSecurity = true };
        }

        public IEnumerable<CountryModel> GetAll()
        {
            List<CountryModel> returnList = new();
            try
            {
                using SqlConnection connection = new(Builder.ConnectionString);
                connection.Open();

                string sql = "SELECT Country, count(CustomerId) AS Number FROM Customer GROUP BY Country ORDER BY Number DESC";

                using SqlCommand command = new(sql, connection);
                using SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    CountryModel country = new()
                    {
                        Name = reader.Get<string>("Country"),
                        NumberOfCustomers = reader.Get<int>("Number")
                    };
                    returnList.Add(country);
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
