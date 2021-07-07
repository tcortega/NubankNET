using System;
using System.Collections.Generic;
using System.Text;

namespace tcortega.NubankClient.Exceptions
{
    class NuRequestException : NuException
    {
        public NuRequestException(int responseCode)
            : base($"The request made failed with HTTP status {responseCode}")
        {

        }
    }
}
