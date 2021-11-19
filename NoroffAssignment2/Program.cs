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
          
            ICustomerRepository customerRepository = new CustomerRepository();
            ICustomerCountryRepository countryRepository = new CustomerCountryRepository();
            ICustomerSpenderRepository customerSpenderRepository = new CustomerSpenderRepository();
            ICustomerGenreRepository customerGenreRepository = new CustomerGenreRepository();

            UILogic(customerRepository, customerGenreRepository);

        }
        /// <summary>
        /// Logic for showing information on console and getting user input
        /// </summary>
        /// <param name="customerRepository"></param>
        /// <param name="customerGenreRepository"></param>
        private static void UILogic(ICustomerRepository customerRepository, ICustomerGenreRepository customerGenreRepository)
        {
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
                        if (firstName.ToLower() == "q" || firstName == string.Empty) break;
                        string lastName = GetNameString("Lastname(q to cancel): ");
                        if (lastName.ToLower() == "q" || lastName == string.Empty) break;
                        customer = null;
                        customer = customerRepository.GetByName(firstName, lastName);
                        if (customer == null || customer.CustomerId == 0)
                        {
                            Console.Clear();
                            Console.WriteLine("Customer not found");
                        }
                        else
                        {
                            customerList.Clear();
                            customerList.Add(customer);
                            WriteCustomersToConsole(customerList);
                        }
                        break;
                    case "p":
                        int? limit = GetIntInput("Enter a number for how many customers to load: ");
                        if (limit == null) break;
                        int? offset = GetIntInput("Enter a number for which customer ID to start from: ");
                        if (offset == null) break;
                        customerList.Clear();
                        customerList = (List<CustomerModel>)customerRepository.GetPage((int)limit, (int)offset);
                        WriteCustomersToConsole(customerList);

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
        }

        /// <summary>
        /// Gets an integer input from user
        /// </summary>
        /// <param name="inputText"></param>
        /// <returns> int? </returns>
        private static int? GetIntInput(string inputText)
        {
            Console.Clear();
            while (true)
            {
                int? inputInt = 0;
                Console.SetCursorPosition(0, 20);
                Console.Write(inputText);
                string input = Console.ReadLine();
                if (input.ToLower() == "q") return null;
                try
                {
                    inputInt = int.Parse(input);
                }
                catch (Exception)
                {
                    Console.WriteLine(inputText);
                    return null;
                }
                return inputInt;
            }
        }

        /// <summary>
        /// Write customer ID, firstName, LastName, City and country to screen for all customermodels in customerList to console
        /// </summary>
        /// <param name="customerList"></param>
        private static void WriteCustomersToConsole(List<CustomerModel> customerList)
        {
            Console.Clear();
            if(customerList.Count < 1)
            {
                Console.WriteLine("No customers found!");
                return;
            }
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
            Console.WriteLine("p = list a page of customers with input 'limit' and 'offset'");
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

        /// <summary>
        /// Clear screen and  write inputText at a set position and requests input from user  
        /// </summary>
        /// <param name="inputText"></param>
        /// <returns> string </returns>
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
        /// Gets input for the Input Options
        /// </summary>
        /// <returns></returns>
        private static string GetInput()
        {
            Console.SetCursorPosition(0, 9);
            Console.Write("Your input: ");
            return Console.ReadLine();
        }
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
