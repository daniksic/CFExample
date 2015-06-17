using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claudia.Domain.Models.v2
{
    //[ComplexType]
    public class Comments
    {
        public int EntityGroup { get; set; }
        public int Id { get; set; }
        public string Comment { get; set; }
    }
}
