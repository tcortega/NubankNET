using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tcortega.NubankClient.Models
{
    class LoginPayload
    {
        public string grant_type { get; set; }
        public string client_id { get; set; }
        public string client_secret { get; set; }
        public string login { get; set; }
        public string password { get; set; }
    }
}
