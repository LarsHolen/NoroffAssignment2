using NoroffAssignment2.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace NoroffAssignment2.DataAccess
{
    public class CustomerRepository : ICustomerRepository
    {
        public SqlConnectionStringBuilder Builder { get; set; }
        public CustomerRepository()
        {
            Builder = new() { DataSource = "DESKTOP-UD2KPSV\\SQLEXPRESS", InitialCatalog = "Chinook", IntegratedSecurity = true };
        }
        /// <summary>
        /// Getting all Customer enteries from datasource/Catalog from Builder/SqlConnectionStringBuilder
        /// </summary>
        /// <returns> List/IEnumerable<CustomerModel> ></returns>
        public IEnumerable<CustomerModel> GetAll()
        {
            List<CustomerModel> returnList = new();
            try
            {
                using SqlConnection connection = new(Builder.ConnectionString);
                connection.Open();

                string sql = "SELECT * FROM Customer";

                using SqlCommand command = new(sql, connection);
                using SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    CustomerModel customer = new()
                    {
                        CustomerId = reader.Get<int>("CustomerId"),
                        FirstName = reader.Get<string>("FirstName"),
                        LastName = reader.Get<string>("LastName"),
                        Company = reader.Get<string>("Company"),
                        Address = reader.Get<string>("Address"),
                        City = reader.Get<string>("City"),
                        State = reader.Get<string>("State"),
                        Country = reader.Get<string>("Country"),
                        PostalCode = reader.Get<string>("PostalCode"),
                        Phone = reader.Get<string>("Phone"),
                        Fax = reader.Get<string>("Fax"),
                        Email = reader.Get<string>("Email"),
                        SupportRepId = reader.Get<int>("SupportRepId")
                    };
                    returnList.Add(customer);
                }
            }
            catch (SqlException exception)
            { 
                Console.WriteLine("Sql exception msg:" + exception.Message);
            }
            return returnList;
        }

        public CustomerModel GetById(int id)
        {
            CustomerModel returnCustomer = new();
            try
            {
                using SqlConnection connection = new(Builder.ConnectionString);
                connection.Open();

                string sql = "SELECT * FROM Customer where CustomerId = @id";


                using SqlCommand command = new(sql, connection);
                command.Parameters.AddWithValue("@id", id);
                using SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    returnCustomer = new()
                    {
                        CustomerId = reader.Get<int>("CustomerId"),
                        FirstName = reader.Get<string>("FirstName"),
                        LastName = reader.Get<string>("LastName"),
                        Company = reader.Get<string>("Company"),
                        Address = reader.Get<string>("Address"),
                        City = reader.Get<string>("City"),
                        State = reader.Get<string>("State"),
                        Country = reader.Get<string>("Country"),
                        PostalCode = reader.Get<string>("PostalCode"),
                        Phone = reader.Get<string>("Phone"),
                        Fax = reader.Get<string>("Fax"),
                        Email = reader.Get<string>("Email"),
                        SupportRepId = reader.Get<int>("SupportRepId")
                    };
                   
                }
                
            }
            catch (SqlException exception)
            {
                Console.WriteLine("Sql exception msg:" + exception.Message);
            }
            return returnCustomer;
        }
    }
}