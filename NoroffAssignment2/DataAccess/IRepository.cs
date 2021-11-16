using System.Data.SqlClient;

namespace NoroffAssignment2.DataAccess
{
    public interface IRepository
    {
        SqlConnectionStringBuilder Builder { get; }
    }
}
