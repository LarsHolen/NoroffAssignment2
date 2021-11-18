using NoroffAssignment2.DataAccess.Exceptions;
using NoroffAssignment2.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace NoroffAssignment2.DataAccess
{
    public class CustomerRepository : ICustomerRepository
    {
        public SqlConnectionStringBuilder Builder { get; init; }
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

                string sql = "SELECT * FROM Customer WHERE FirstName LIKE @firstName AND LastName LIKE @lastName";

                string firstN = "%" + firstName + "%";
                string lastN = "%" + lastName + "%";
                Console.WriteLine(firstN + "--" + lastN );
                Console.ReadLine();
                using SqlCommand command = new(sql, connection);
                command.Parameters.AddWithValue("@firstName", firstN);
                command.Parameters.AddWithValue("@lastName",  lastN);
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
        /// Adds a customer of type CustomerModel into the database
        /// </summary>
        /// <param name="customer"></param>
        public void AddCustomer(CustomerModel customer)
        {
            try
            {
                using SqlConnection connection = new(Builder.ConnectionString);
                connection.Open();

                string sql = "INSERT INTO Customer (FirstName, LastName, Company, Address, City, State, Country, PostalCode, Phone, Fax, Email,SupportRepId)" +
                    " VALUES(@firstName, @lastname, @company, @address, @city, @state, @country, @postalCode, @phone, @fax, @email, @supportRepId)";


                using SqlCommand command = new(sql, connection);
                command.Parameters.AddWithValue("@firstName", customer.FirstName);
                command.Parameters.AddWithValue("@LastName", customer.LastName);
                command.Parameters.AddWithValue("@company", customer.Company);
                command.Parameters.AddWithValue("@address", customer.Address);
                command.Parameters.AddWithValue("@city", customer.City);
                command.Parameters.AddWithValue("@state", customer.State);
                command.Parameters.AddWithValue("@country", customer.Country);
                command.Parameters.AddWithValue("@postalCode", customer.PostalCode);
                command.Parameters.AddWithValue("@phone", customer.Phone);
                command.Parameters.AddWithValue("@fax", customer.Fax);
                command.Parameters.AddWithValue("@email", customer.Email);
                command.Parameters.AddWithValue("@supportRepId", customer.SupportRepId);


                int result = command.ExecuteNonQuery();
                if (result != 1)
                {
                    throw new CustomSqlException("Inserting a customer does not return 1");
                }
                
            }
            catch (SqlException exception)
            {
                Console.WriteLine("Sql exception msg:" + exception.Message);
            }
        }

        /// <summary>
        /// Updates a row in the Customer table with CustomerID equal to the parameter CustomerModel CustomerId value 
        /// </summary>
        /// <param name="customer"></param>
        public void UpdateCustomer(CustomerModel customer)
        {
            try
            {
                using SqlConnection connection = new(Builder.ConnectionString);
                connection.Open();

                string sql = "UPDATE Customer SET FirstName = @firstName, LastName = @lastname, Company = @company, " +
                    "Address = @address, City = @city, State = @state, Country = @country, PostalCode = @postalCode, " +
                    "Phone = @phone, Fax = @fax, Email = @email,SupportRepId = @supportRepId " +
                    "WHERE CustomerId = @customerId";


                using SqlCommand command = new(sql, connection);
                command.Parameters.AddWithValue("@customerId", customer.CustomerId);
                command.Parameters.AddWithValue("@firstName", customer.FirstName);
                command.Parameters.AddWithValue("@LastName", customer.LastName);
                command.Parameters.AddWithValue("@company", customer.Company);
                command.Parameters.AddWithValue("@address", customer.Address);
                command.Parameters.AddWithValue("@city", customer.City);
                command.Parameters.AddWithValue("@state", customer.State);
                command.Parameters.AddWithValue("@country", customer.Country);
                command.Parameters.AddWithValue("@postalCode", customer.PostalCode);
                command.Parameters.AddWithValue("@phone", customer.Phone);
                command.Parameters.AddWithValue("@fax", customer.Fax);
                command.Parameters.AddWithValue("@email", customer.Email);
                command.Parameters.AddWithValue("@supportRepId", customer.SupportRepId);


                int result = command.ExecuteNonQuery();
                if (result != 1)
                {
                    throw new CustomSqlException("Updating a customer does not return 1");
                }

            }
            catch (SqlException exception)
            {
                Console.WriteLine("Sql exception msg:" + exception.Message);
            }
        }

        /// <summary>
        /// Returns true/false if the input id exist as CustomerId in Customer Db
        /// </summary>
        /// <param name="id"></param>
        /// <returns> bool </returns>
        public bool GetCustomerIDExists(int id)
        {
            bool returnValue = false;
            try
            {
                using SqlConnection connection = new(Builder.ConnectionString);
                connection.Open();

                string sql = "SELECT CASE WHEN EXISTS (SELECT * FROM Customer WHERE CustomerId = @id) THEN 'TRUE' ELSE 'FALSE' END";


                using SqlCommand command = new(sql, connection);
                command.Parameters.AddWithValue("@id", id);
                using SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    if(reader.GetString(0) == "TRUE")
                    {
                        returnValue = true;
                    } else
                    {
                        returnValue = false;
                    }

                }
            }
            catch (SqlException exception)
            {
                Console.WriteLine("Sql exception msg:" + exception.Message);
            }
            return returnValue;
        }
    }
}