using BE.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BE.DTOs.Input
{
    public class LoginInput
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

        public bool UsernameIsEmail
        {
            get
            {
                return !string.IsNullOrEmpty(Username) && Regex.Match(Username, ConstantValue.REGEX_EMAIL).Success;
            }
        }
    }
}
