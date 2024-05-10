using DeskBooker.Core.Tests.Utils;

namespace DeskBooker.Core.Tests.Exceptions
{
    public class InvalidEmailException: Exception
    {
        public InvalidEmailException() : base(Constants.InvalidEmailMessage) { }

        public InvalidEmailException(string message) : base(message) { }

        public InvalidEmailException(string message, Exception innerException) : base(message, innerException) { }
    }
}

