using NoroffAssignment2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoroffAssignment2.DataAccess
{
    public interface ICustomerCountryRepository : IRepository
    {
        IEnumerable<CountryModel> GetAll();
    }
}
