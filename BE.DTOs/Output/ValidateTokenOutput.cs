using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.DTOs.Output
{
    public class ValidateTokenOutput
    {
        public Guid AccountId { get; set; } = Guid.Empty;
        public Guid UserId { get; set; } = Guid.Empty;
        public string Username { get; set; }
        public string Email { get; set; }
        public bool isValid { get; set; }
    }
}
