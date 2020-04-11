using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSource.dtos
{
    public class AccountDTO
    {
        public string UserID { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }
        public string Phone { get; set; }
        public string Account_Role { get; set; }
        public string Account_Status { get; set; }
    }
}
