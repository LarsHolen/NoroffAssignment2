using NoroffAssignment2.DataAccess;
using NoroffAssignment2.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace NoroffAssignment2
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Hello World!");

            ICustomerRepository customerRepository = new CustomerRepository();



            List<CustomerModel> customerList = new();

            //Getall:
            //List<CustomerModel> customerList = (List<CustomerModel>)customerRepository.GetAll();
            //customerList.Add(customerRepository.GetById(1));
            customerList = (List<CustomerModel>)customerRepository.GetPage(10,10);

            foreach (var customer  in customerList)
            {
                Console.WriteLine(customer.FirstName);
            }
        }

        /*
        private static List<CustomerModel> GetCustomers()
        {
            SqlConnectionStringBuilder builder = new();

            builder.DataSource = "DESKTOP-UD2KPSV\\SQLEXPRESS";
            builder.InitialCatalog = "Chinook";
            builder.IntegratedSecurity = true;

            List<CustomerModel> customers = new();

            try
            {
                using SqlConnection connection = new(builder.ConnectionString);
                connection.Open();

                string sql = "SELECT * FROM Customer";

                using SqlCommand command = new(sql, connection);
                using var reader = command.ExecuteReader();

                while(reader.Read())
                {
                    CustomerModel customer = new()
                    {
                        CustomerId = reader.Get<int>("CustomerId"),
                        FirstName = reader.Get<String>("FirstName"),
                        LastName = reader.Get<String>("LastName"),
                        Company = reader.Get<String>("Company"),
                        Address = reader.Get<String>("Address"),
                        City = reader.Get<String>("City"),
                        State = reader.Get<String>("State"),
                        Country = reader.Get<String>("Country"),
                        PostalCode = reader.Get<String>("PostalCode"),
                        Phone = reader.Get<String>("Phone"),
                        Fax = reader.Get<String>("Fax"),
                        Email = reader.Get<String>("Email"),
                        SupportRepId = reader.Get<int>("SupportRepId")

                    };
                    customers.Add(customer);
                }
                return customers;
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            
        }
        */
    
        
    }
    public static class SqlDataReaderExtensions
    {
        public static T Get<T>(this SqlDataReader reader, string columnName)
        {
            if (reader.IsDBNull(columnName))
                return default;
            return reader.GetFieldValue<T>(columnName);
        }
    }
}
