using System;

namespace tcortega.NubankClient.Exceptions
{
    class NuRequestException : NuException
    {
        public NuRequestException(int responseCode, string message)
            : base($"The request made failed with HTTP status {responseCode}." + Environment.NewLine + message)
        {

        }
    }
}
