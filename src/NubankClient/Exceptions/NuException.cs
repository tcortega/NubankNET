using System;

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
