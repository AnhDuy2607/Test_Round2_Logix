using BE.Model.Entities.Base;

namespace BE.Model.Entities
{
    public class Movie : BaseModel
    {
        public string Title { get; set; }
        public int LikeNo { get; set; }
        public string ImageUrl { get; set; }
        public virtual IEnumerable<LikeHistory> LikeHistories { get; set; }

        public Movie()
        {
            LikeNo = 0;
            ImageUrl= string.Empty;
            LikeHistories = new List<LikeHistory>();
        }
    }
}