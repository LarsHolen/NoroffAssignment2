using NoroffAssignment2.DataAccess;
using NoroffAssignment2.Models;
using System.Collections.Generic;
using Xunit;

namespace NoroffAssignment2Tests
{
    public class CountryTest
    {
        #region Testing GetAll
        [Fact]
        public static void GetAllReturnListOfCorrectCount()
        {
            // Arrange
            List<CountryModel> countryList = new();
            ICustomerCountryRepository countryRepository = new CustomerCountryRepository();
            // Act
            countryList = (List<CountryModel>)countryRepository.GetAll();
            // Assert
            int expected = 24;
            int actual = countryList.Count;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public static void GetAllReturnUSAInFirstSlot()
        {
            // Arrange
            List<CountryModel> countryList = new();
            ICustomerCountryRepository countryRepository = new CustomerCountryRepository();
            // Act
            countryList = (List<CountryModel>)countryRepository.GetAll();
            // Assert
            string expected = "USA";
            string actual = countryList[0].Name;
            Assert.Equal(expected, actual);
        }

        #endregion
    }
}
