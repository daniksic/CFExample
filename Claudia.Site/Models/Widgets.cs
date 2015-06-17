using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Claudia.Site.Models
{
    public class Widget : IWidget
    {
        public int SortOrder { get; set; }

        public string ClassName { get; set; }

        public string FooterText { get; set; }

        public string HeaderText { get; set; }

        public string WidgetName { get; set; }

        public ISubWidget SubWidget { get; set; }
    }

    public class SubWidget : ISubWidget {

        public string LinkAddress { get; set; }

        public string Description { get; set; }

        public string WidgetName { get; set; }
    }
}