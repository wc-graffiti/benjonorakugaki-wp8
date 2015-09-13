using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wcg_client
{
    public class spotApiResult
    {
        public int id { get; set; }
        public string name { get; set; }
        public string lat { get; set; }
        public string lon { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
    }
}
