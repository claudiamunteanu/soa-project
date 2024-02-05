using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public class ProductNotFound : Exception
    {
        public ProductNotFound() { }

        public ProductNotFound(string message) : base(message) { }

        public ProductNotFound(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
