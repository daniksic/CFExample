using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Claudia.Domain.Models.v1
{
    public class Comment : BaseEntity
    {
        [Column(Order = 1), ForeignKey("ObjectCategory")]
        public int CategoryId { get; set; }

        [Column(Order = 2), Key]
        public int ObjectId { get; set; }

        public string Text { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }


        public virtual User User { get; set; }

        public virtual ObjectCategory ObjectCategory { get; set; }
    }
}
