using BE.Model.Entities.Base;
using BE.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Model.Entities
{
    public class Account : BaseModel
    {
        public string Username { get; set; }
        public string SaltPwd { get; set; }
        public string HashPwd { get; set; }
        public string Token { get; set; }
        public DateTime? ExpiredTime { get; set; }

        [ForeignKey(nameof(User))]
        public Guid? UserId { get; set; }
        public virtual User User { get; set; }

        public Account()
        {
            SaltPwd = ConstantValue.PASSWORD_SALT;
            ExpiredTime = null;
        }
    }
}
