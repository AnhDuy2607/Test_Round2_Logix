using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.DTOs.Output
{
    public class UserOutput
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get
            {
                return $"{LastName} {FirstName}";
            }
        }
    }
}
