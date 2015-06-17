using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebGrease.Css.Extensions;

namespace Claudia.Site.Helpers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method,AllowMultiple = true,Inherited = true)]
    public class MenuAttribute:Attribute
    {
        /// <summary>
        /// Resolves in runtime and setup dynamic menu. Use with Html.MenuRender extension. Does not initialise controller.
        /// </summary>
        /// <param name="group">Name of groupe used to group items. example: admin, cms.</param>
        /// <param name="positionInGroup">Position in group</param>
        /// <param name="displayName">Display name in menu</param>
        /// <param name="htmlAttributes">KeyValue pair. example: class=inline</param>
        /// <param name="routeValues">KeyValue pair. example: id=1</param>
        /// <param name="innerHtml">Html inside a tag</param>
        /// <param name="routeName">Optional, use only if controllers is in different route map</param>
        public MenuAttribute(string group, int positionInGroup, string displayName, string htmlAttributes, string routeValues, string innerHtml, string routeName = null)
        {
        }
    }
}