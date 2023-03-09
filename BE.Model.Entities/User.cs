using BE.Model.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Model.Entities
{
    public class User : BaseModel
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }

        [ForeignKey(nameof(Account))]
        public Guid? AccountId { get; set; }
        public virtual Account Account { get; set; }

        public User()
        {
        }
    }
}
