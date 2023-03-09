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
    public class NewAccountInput
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }

        public bool isMatchedPassword
        {
            get
            {
                return !string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(ConfirmPassword) && Password == ConfirmPassword;
            }
        }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
