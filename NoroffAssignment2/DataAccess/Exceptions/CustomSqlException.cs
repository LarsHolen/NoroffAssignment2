using System;


namespace NoroffAssignment2.DataAccess.Exceptions
{
    public class CustomSqlException : Exception
    {
        public CustomSqlException(string message) : base(message)
        { 
        }


    }

}

