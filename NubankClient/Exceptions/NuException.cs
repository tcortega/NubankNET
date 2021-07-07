using System;
using System.Collections.Generic;
using System.Text;

namespace tcortega.NubankClient.Exceptions
{
    class NuException : Exception
    {
        public NuException(string Message) 
            : base(Message)
        {

        }
    }
}
