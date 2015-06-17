using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claudia.Domain.Models.v1
{
    public class Widget: BaseEntity
    {
        public string Description { get; set; }
        public int SortOrder { get; set; }

        public string WidgetName { get; set; }

        public string SubWidgetName { get; set; }

        public string ReferenceType { get; set; }

        public int ReferenceId { get; set; }
    }
}
