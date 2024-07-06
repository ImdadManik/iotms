using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iotms.Emqx_UserAuth
{
    internal class cEmqxUsers
    {
        public bool is_superuser { get; set; }
        public string password { get; set; }
        public string user_id { get; set; }
    }
}
