using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Model.Entities.Base
{
    public class BaseModel
    {
        public Guid Id { get; set; }
        public bool isActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public BaseModel()
        {
            CreatedDate = new DateTime();
            ModifiedDate = new DateTime();
            Id = Guid.NewGuid();
        }
    }
}
