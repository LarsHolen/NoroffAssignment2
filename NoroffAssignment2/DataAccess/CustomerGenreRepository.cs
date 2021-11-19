using NoroffAssignment2.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoroffAssignment2.DataAccess
{
    class CustomerGenreRepository : ICustomerGenreRepository
    {
        public SqlConnectionStringBuilder Builder { get; init; }
        public CustomerGenreRepository()
        {
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["connString"];
            string connectString = settings.ConnectionString;
            Builder = new(connectString);
        }

        /// <summary>
        /// Gets the most populat genre for the CustomerID parameter.If multiple genres with the same count, multiple are returned
        /// </summary>
        /// <param name="id"></param>
        /// <returns>List<CustomerGenreModel></returns>
        public IEnumerable<CustomerGenreModel> GetAll(int id)
        {

            List<CustomerGenreModel> returnList = new();
            try
            {
                using SqlConnection connection = new(Builder.ConnectionString);
                connection.Open();

                string sql = "SELECT TOP(1) WITH TIES g.Name, COUNT(g.Name) AS Number FROM Genre as g " +
                    "INNER JOIN (SELECT GenreId AS GId FROM Track WHERE TrackId IN (SELECT TrackId FROM InvoiceLine WHERE InvoiceId IN (SELECT InvoiceId FROM Invoice " +
                    "WHERE CustomerId = @id))) as gi on g.GenreId = gi.GId " +
                    "GROUP BY Name ORDER BY Number DESC";

                using SqlCommand command = new(sql, connection);
                command.Parameters.AddWithValue("@id", id);
                using SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    CustomerGenreModel customerGenre = new()
                    {
                        Name = reader.Get<string>("Name"),
                        Count = reader.Get<int>("Number"),
                       
                    };
                    returnList.Add(customerGenre);
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
