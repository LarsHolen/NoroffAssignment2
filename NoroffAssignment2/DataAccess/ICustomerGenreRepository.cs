using NoroffAssignment2.Models;
using System.Collections.Generic;

namespace NoroffAssignment2.DataAccess
{
    public interface ICustomerGenreRepository : IRepository
    {
        IEnumerable<CustomerGenreModel> GetAll(int id);
    }
}
