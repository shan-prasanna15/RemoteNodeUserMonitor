using System;
using System.Collections.Generic;
using System.Text;

namespace BackendProcessor
{
    class UserDetail
    {
        public string UserName { get; set; }
        public string SessionName { get; set; }
        public int Id { get; set; }
        public string State { get; set; }
        public string IdleTime { get; set; }
        public string LogonTime { get; set; }
    }
}
