using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claudia.Site
{
    public interface ISubWidget
    {
        string LinkAddress { get; set; }
        string Description { get; set; }
        string WidgetName { get; set; }
    }

    public interface IWidget
    {
        int SortOrder { get; set; }
        string ClassName { get; set; }
        string FooterText { get; set; }
        string HeaderText { get; set; }
        string WidgetName { get; set; }
        ISubWidget SubWidget { get; set; }
    }
}
