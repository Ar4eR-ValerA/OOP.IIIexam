using System;

namespace Taksi.Server.DAL.Exceptions
{
    public class DriverExceptions : Exception
    {
        public DriverExceptions()
        {
        }

        public DriverExceptions(string message)
            : base(message)
        {
        }

        public DriverExceptions(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}