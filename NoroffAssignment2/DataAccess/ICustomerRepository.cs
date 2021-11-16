using NoroffAssignment2.Models;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace NoroffAssignment2.DataAccess
{
    public interface ICustomerRepository : IRepository
    {
        IEnumerable<CustomerModel> GetAll();
        CustomerModel GetById(int id);
    }
}