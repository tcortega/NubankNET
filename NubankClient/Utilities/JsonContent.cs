using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace tcortega.NubankClient.Utilities
{
    class JsonContent<T> : StringContent
    {
        public JsonContent(T content, JsonSerializerOptions options = null)
            : base(JsonSerializer.Serialize(content, options), Encoding.UTF8, "application/json")
        {
        }
    }
}
