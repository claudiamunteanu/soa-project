﻿namespace IdentityProvider.Exceptions
{
    public class ExistingEmailException : Exception
    {
        public ExistingEmailException() { }

        public ExistingEmailException(string? message) : base(message) { }

        public ExistingEmailException(string? message, Exception? innerException) : base(message, innerException) { }
    }
}
