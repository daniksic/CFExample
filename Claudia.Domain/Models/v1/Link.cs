using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Claudia.Domain.Models.v1
{
    public class Link : BaseEntity
    {
        [Column(Order = 1), ForeignKey("ObjectCategory")]
        public int CategoryId { get; set; }

        [MaxLength(150)]
        public string Title { get; set; }

        [MaxLength]
        public string Description { get; set; }

        public string ServerFileName { get; set; }

        public string ClientLocalFileName { get; set; }

        public virtual ObjectCategory ObjectCategory { get; set; }
        public virtual ICollection<LinksUrl> LinksUrls { get; set; }

        public bool Equals(Link link)
        {
            if (this.Title != link.Title) return false;
            if (this.Description != link.Description) return false;
            if (this.CategoryId != link.CategoryId) return false;
            if (this.Id != link.Id) return false;
            if (this.ServerFileName != link.ServerFileName) return false;
            return true;
        }
    }

}
