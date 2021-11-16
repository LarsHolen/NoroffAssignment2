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

        /// <summary>
        /// Getting a limit ammount of Customer enteries from datasource/Catalog from Builder/SqlConnectionStringBuilder,
        /// selected ordered by ID and with an offset of offset
        /// </summary>
        /// <returns> List/IEnumerable<CustomerModel> ></returns>
        public IEnumerable<CustomerModel> GetPage(int limit, int offset)
        {
            List<CustomerModel> returnList = new();
            try
            {
                using SqlConnection connection = new(Builder.ConnectionString);
                connection.Open();

                string sql = "SELECT * FROM Customer ORDER BY CustomerId OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY";

                using SqlCommand command = new(sql, connection);
                command.Parameters.AddWithValue("@offset", offset);
                command.Parameters.AddWithValue("@limit", limit);
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

        /// <summary>
        /// Get a single customer where ID == id
        /// </summary>
        /// <param name="id"></param>
        /// <returns> CustomerModel </returns>
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

        /// <summary>
        /// Get a single(the first if multiple) customer by first and last name
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <returns> CustomerModel </returns>
        public CustomerModel GetByName(string firstName, string lastName)
        {
            CustomerModel returnCustomer = new();
            try
            {
                using SqlConnection connection = new(Builder.ConnectionString);
                connection.Open();

                string sql = "SELECT * FROM Customer where (FirstName LIKE @firstName AND LastName LIKE @lastName) FETCH FIRST";


                using SqlCommand command = new(sql, connection);
                command.Parameters.AddWithValue("@firstName", "%" + firstName + "%");
                command.Parameters.AddWithValue("@lastName", "%" + lastName + "%");
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