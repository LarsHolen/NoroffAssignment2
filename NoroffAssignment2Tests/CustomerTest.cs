using NoroffAssignment2.DataAccess;
using NoroffAssignment2.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace NoroffAssignment2Tests
{
    public class CustomerTest
    {
        #region Testing Get/GetByID
        [Fact]
        public static void GetAllReturnListOfCorrectCount()
        {
            // Arrange
            List<CustomerModel> customerList = new();
            ICustomerRepository customerRepository = new CustomerRepository();
            // Act
            customerList = (List<CustomerModel>)customerRepository.GetAll();
            // Assert
            int expected = 59;
            int actual = customerList.Count;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public static void GetByIdOneShouldReturnCustomerWithFirstNameLuis()
        {
            // Arrange
            CustomerModel customer = new();
            ICustomerRepository customerRepository = new CustomerRepository();
            // Act
            customer = customerRepository.GetById(1);
            // Assert
            string expected = "Luís";
            string actual = customer.FirstName;
            Assert.Equal(expected, actual);
        }

        #endregion
    }
}
