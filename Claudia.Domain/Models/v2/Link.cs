using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Claudia.Domain.Models.v2
{
    public class Link : BaseEntity
    {
        [MaxLength(40)]
        public string Title { get; set; }

        [MaxLength]
        public string Description { get; set; }

        public string Url { get; set; }

        public ICollection<Comments> Comments { get; set; }
    }
}
