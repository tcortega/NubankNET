using System;
using System.Collections.Generic;
using System.Text;

namespace tcortega.NubankClient.Exceptions
{
    class NuException : Exception
    {
        public NuException(string message) 
            : base(message)
        {

        }
    }
}
