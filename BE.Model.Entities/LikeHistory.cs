using BE.Model.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Model.Entities
{
    public class LikeHistory : BaseModel
    {
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        [ForeignKey(nameof(Movie))]
        public Guid MovieId { get; set; }

        public virtual User User { get; set; }
        public virtual Movie Movie { get; set; }

        public LikeHistory()
        {

        }
    }
}
