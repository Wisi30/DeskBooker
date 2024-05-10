using DeskBookerApp.Utils;

namespace DeskBookerApp.Exceptions
{
    public class InvalidEmailException : Exception
    {
        public InvalidEmailException() : base(Constants.InvalidEmailMessage) { }

        public InvalidEmailException(string message) : base(message) { }

        public InvalidEmailException(string message, Exception innerException) : base(message, innerException) { }
    }
}