using System;

namespace ChatFrontend.Classes.Exceptions
{
    public class AuthException : Exception
    {
        public AuthException(string message) : base(message) { }
    }
}
