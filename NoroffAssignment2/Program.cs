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
            // Initializing a new customer for testing

            CustomerModel testCustomer = new()
            {
                CustomerId = 61,
                FirstName = "UPDATED",
                LastName = "UPDATED",
                Company = "UPDATED",
                Address = "UPDATED",
                State = "UPDATED",
                City = "UPDATED",
                Country = "UPDATED",
                PostalCode = "UPDATED",
                Phone = "TesUPDATEDt",
                Fax = "UPDATED",
                Email = "UPDATED",
                SupportRepId = 1
            };
            ICustomerRepository customerRepository = new CustomerRepository();
            ICustomerCountryRepository countryRepository = new CustomerCountryRepository();
            ICustomerSpenderRepository customerSpenderRepository = new CustomerSpenderRepository();
            ICustomerGenreRepository customerGenreRepository = new CustomerGenreRepository();


            WriteHeader();
            string input = GetInput();
            while (input.ToLower() != "q")
            {
                List<CustomerModel> customerList = new();
                switch (input.ToLower())
                {
                    case "g":
                        int? id = GetId();
                        if (id is null) break;
                        List<CustomerGenreModel> genreList = (List<CustomerGenreModel>)customerGenreRepository.GetAll((int)id);
                        Console.SetCursorPosition(0, 15);
                        Console.Write("Top Genre(s) for customer with ID 3: ");
                        foreach (var item in genreList)
                        {
                            Console.Write(item.Name + "(" + item.Count + ")\t");
                        }
                        break;
                    case "q":
                        break;
                    case "a":
                        customerList.Clear();
                        customerList = (List<CustomerModel>)customerRepository.GetAll();
                        Console.SetCursorPosition(0, 12);
                        WriteCustomersToConsole(customerList);
                        break;
                    case "s":
                        id = GetId();
                        if (id is null) break;
                        CustomerModel customer = customerRepository.GetById((int)id);
                        customerList.Clear();
                        customerList.Add(customer);
                        WriteCustomersToConsole(customerList);
                        break;
                    case "n":
                        string firstName = GetNameString("Firstname(q to cancel): ");
                        if (firstName.ToLower() == "q") break;
                        string lastName = GetNameString("Lastname(q to cancel): ");
                        if (lastName.ToLower() == "q") break;
                        customer = null;
                        customer = customerRepository.GetByName(firstName, lastName);
                        Console.WriteLine(customer.FirstName);
                        if (customer == null)
                        {
                            Console.Clear();
                            Console.WriteLine("Customer not found");
                            Console.WriteLine("Press enter");
                            Console.ReadLine();
                        } else
                        {
                            customerList.Clear();
                            customerList.Add(customer);
                            Console.WriteLine(customerList.Count);
                            WriteCustomersToConsole(customerList);
                        }
                        break;

                    default:
                        break;

                }
                //Console.SetCursorPosition(0,22);
                Console.WriteLine("\nPress enter");
                Console.ReadLine();
                WriteHeader();
                input = GetInput();
            }





            //TOP GENRE
            /*
            IEnumerable<CustomerGenreModel> genreList = customerGenreRepository.GetAll(1);
            List<CustomerGenreModel> genList = (List<CustomerGenreModel>)genreList;
            List<CustomerGenreModel> topList = new();
            for (int i = 0; i < genList.Count; i++)
            {
               
                if (i == 0)
                {
                    topList.Add(genList[i]);
                } else if(i > 0 && genList[i].Count == genList[i-1].Count)
                {
                    Console.WriteLine("Hey");
                    topList.Add(genList[i]);
                } else
                {
                    break;
                }
               
            }
            foreach (var item in topList)
            {
                Console.WriteLine("Top Genre for customer with ID 3: " + item.Name);
            }
            //Console.WriteLine(" Top benre for customerId 1: " + genList[0].Name);
            */



            /* Get spenders test
            IEnumerable<CustomerSpenderModel> spenderList = customerSpenderRepository.GetPage(10,0);
            int spenders = 0;
            foreach (var spender in spenderList)
            {
                Console.WriteLine(customerRepository.GetById(spender.CustomerId).FirstName + " spent: " + spender.TotalSpendt.ToString());
                spenders++;
            }
            Console.WriteLine("Total spenders: " + spenders.ToString());
            */
            /*
            var countryList = countryRepository.GetAll();
            foreach (CountryModel country in countryList)
            {
                Console.WriteLine(country.Name + "\t \t" + country.NumberOfCustomers);
            }
            */




            // Updating customer
            //customerRepository.UpdateCustomer(testCustomer);

            // Adding customer
            //customerRepository.AddCustomer(testCustomer);
            //List<CustomerModel> customerList = new();


            //Getall:
            //List<CustomerModel> customerList = (List<CustomerModel>)customerRepository.GetAll();
            //customerList.Add(customerRepository.GetById(1));

            /* GetPage
            customerList = (List<CustomerModel>)customerRepository.GetPage(10,10);

            foreach (var customer  in customerList)
            {
                Console.WriteLine(customer.FirstName);
            }
            */
        }

        private static void WriteCustomersToConsole(List<CustomerModel> customerList)
        {
            for (int i = 0; i < customerList.Count; i++)
            {
                Console.SetCursorPosition(0, 12 + i);
                Console.Write(customerList[i].CustomerId);
                Console.SetCursorPosition(5, 12 + i);
                Console.Write(customerList[i].FirstName);
                Console.SetCursorPosition(25, 12 + i);
                Console.Write(customerList[i].LastName);
                Console.SetCursorPosition(40, 12 + i);
                Console.Write(customerList[i].City);
                Console.SetCursorPosition(65, 12 + i);
                Console.Write(customerList[i].Country);
            }
        }

        /// <summary>
        /// Clear screen and write the menu to console
        /// </summary>
        private static void WriteHeader()
        {
            Console.Clear();
            Console.WriteLine("Chinook DB Console UI");
            Console.WriteLine("");
            Console.WriteLine("Input Options:");
            Console.WriteLine("g = lists the most popular, or multiple equally popular genres for customer by ID");
            Console.WriteLine("a = list all customers in DB");
            Console.WriteLine("s = list a single customer in DB by ID");
            Console.WriteLine("n = list a customer from name input");
            Console.WriteLine("l = list a group of customers with input 'limit' and 'offset'");
            Console.WriteLine("q = quit");
        }

        /// <summary>
        /// Get input and testing if its a valid customer ID.  Both testing if input can be parsed as an int and if that number is an existing CustomerId in DB.
        /// </summary>
        /// <returns>integer on success, null on fail</returns>
        private static int? GetId()
        {
            ICustomerRepository cRepository = new CustomerRepository();
            while (true)
            {
                int? inputId = 0;
                Console.SetCursorPosition(0, 20);
                Console.Write("Enter vaild customer ID(q to cancel: ");
                string input = Console.ReadLine();
                if (input.ToLower() == "q") return null;
                try
                {
                    inputId = int.Parse(input);
                }
                catch (Exception)
                {
                    Console.WriteLine("Unable to parse input as integer.");
                    return null;
                }
                if (cRepository.GetCustomerIDExists((int)inputId))
                {
                    return inputId;
                }
                else
                {
                    Console.WriteLine("Unable to find customer with ID: " + inputId);
                    return null;
                }
            }
        }

        private static string GetNameString(string inputText)
        {
            Console.Clear();
            string inputName = "";
            Console.SetCursorPosition(0, 20);
            Console.Write(inputText);
            inputName = Console.ReadLine();
            return inputName;
        }


        /// <summary>
        /// Gets unput for the Input Options
        /// </summary>
        /// <returns></returns>
        private static string GetInput()
        {
            Console.SetCursorPosition(0, 9);
            Console.Write("Your input: ");
            return Console.ReadLine();
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

    /// <summary>
    /// Found an extension that handle null and retrive data through columnName
    /// Not my invention
    /// </summary>
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
