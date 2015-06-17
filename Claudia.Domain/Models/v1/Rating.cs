using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Claudia.Domain.Models.v1
{
    public class Rating : BaseEntity
    {
        [Column(Order = 1), ForeignKey("ObjectCategory")]
        public int CategoryId { get; set; }

        [Column(Order = 2), Key]
        public int ObjectId { get; set; }

        public int RatingScore { get; set; }
        public int TotalCount { get; set; }

        public virtual ObjectCategory ObjectCategory { get; set; }
    }
}
