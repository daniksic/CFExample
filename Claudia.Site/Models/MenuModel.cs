using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web.Routing;
using WebGrease.Css.Extensions;

namespace Claudia.Site.Models
{
    public class MenuModel
    {
        public string Group { get; set; }
        public int PositionInGroup { get; set; }
        public string DisplayName { get; set; }
        public string HtmlAttributes { get; set; }
        public string RouteValues { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string InnerHtml { get; set; }
        public string RouteName { get; set; }

        public MenuModel()
        {
        }

        public MenuModel(string group, int postionInGroup, string displayName, string controllername)
            : this(group, postionInGroup, displayName, controllername, null, null,null)
        {
        }

        public MenuModel(string group, int postionInGroup, string displayName, string controllername,
            string htmlAttributes)
            : this(group, postionInGroup, displayName, controllername, htmlAttributes, null,null)
        {

        }

        public MenuModel(string group, int postionInGroup, string displayName, string controllername,
            string htmlAttributes, string routeValues, string innerHtml)
        {
            this.Group = group;
            this.PositionInGroup = postionInGroup;
            this.DisplayName = displayName;
            this.ControllerName = controllername;
            this.HtmlAttributes = htmlAttributes;
            this.RouteValues = routeValues;
            this.InnerHtml = innerHtml;
        }

        private IDictionary<string, object> MapKeyValueString(string keyvalstr)
        {
            if (keyvalstr == null) return null;

            var keyval = new Dictionary<string, object>();

            if (keyvalstr.Contains('&'))
            {
                keyvalstr.Split('&').ForEach(p =>
                {
                    var tmp = keyvalstr.Split('=');
                    keyval.Add(tmp[0], tmp[1]);
                });
            }
            else if (keyvalstr.Contains('='))
            {
                var tmp = keyvalstr.Split('=');
                keyval.Add(tmp[0], tmp[1]);
            }

            return keyval;
        }

        public IDictionary<string,object> GetHtmlAttributes()
        {
            return MapKeyValueString(HtmlAttributes);
        }

        public RouteValueDictionary GetRouteValues()
        {
            return MapKeyValueString(RouteValues) as RouteValueDictionary;
            //return (RouteValueDictionary)MapKeyValueString(RouteValues) ?? new RouteValueDictionary();
            //return new RouteValueDictionary();
        }
    }
}