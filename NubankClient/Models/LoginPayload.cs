using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tcortega.NubankClient.Models
{
    class LoginPayload
    {
        public string login { get; set; }
        public string password { get; set; }
        public string public_key { get; set; }
        public string public_key_crypto { get; set; }
        public string model { get; set; }
        public string device_id { get; set; }
    }
}
