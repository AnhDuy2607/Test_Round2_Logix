using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.DTOs.Output
{
    public class LoginOutput
    {
        public string Token { get; set; }
        public DateTime ExpiredDate { get; set; }
        public UserOutput User { get; set; }
    }
}
