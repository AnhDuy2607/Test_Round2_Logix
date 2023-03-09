using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.DTOs.Output
{
    public class MovieOutput
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public int LikeNo { get; set; }
        public bool isLiked { get; set; }
    }
}
