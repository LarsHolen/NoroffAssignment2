using NoroffAssignment2.Models;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace NoroffAssignment2.DataAccess
{
    public interface ICustomerRepository : IRepository
    {
        IEnumerable<CustomerModel> GetAll();
        IEnumerable<CustomerModel> GetPage(int limit, int offset);
        CustomerModel GetById(int id);
        CustomerModel GetByName(string firstName, string lastName);
        bool GetCustomerIDExists(int id);

        void AddCustomer(CustomerModel customer);
        void UpdateCustomer(CustomerModel customer);
    }
}