using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Claudia.Domain.Models.v1
{
    public class LinksUrl:BaseEntity
    {
        [ForeignKey("Link")]
        public int LinkId { get; set; }

        public string ServerFileName { get; set; }

        public string ClientLocalFileName { get; set; }

        public virtual Link Link { get; set; }
    }
}
