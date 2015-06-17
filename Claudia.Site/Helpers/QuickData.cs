using Claudia.Domain.EventManager;
using Claudia.Domain.Models.v1;
using Claudia.Repository;
using Claudia.Site.Models;
using Claudia.Site.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WebGrease.Css.Extensions;

namespace Claudia.Site.Helpers
{
    public class QuickData : ISubscriber
    {
        internal static IEnumerable<ObjectCategory> GetEntitesCategories(bool forceRefresh = false)
        {
            return InMemoryService.Get(QuickDataKey.EntityCategories.ToString(),
                () =>
                {
                    using (var uow = new UnitOfWork())
                    {
                        var data = uow.Category.GetAll().ToList();
                        InMemoryService.Set(QuickDataKey.EntityCategories.ToString(),data);
                        return data;
                    }
                }, forceRefresh);
        }
        internal static IEnumerable<Link> GetCarouselPictureLinks(bool forceRefresh = false)
        {
            return InMemoryService.Get("carousels",
                () =>
                {// calls this function only if data dont allready exists
                    using (var uow = new UnitOfWork())
                    {
                        var data = uow.ApplicationContext.Links.Where(c => c.IsDeleted == false && c.CategoryId == 6).OrderByDescending(c => c.DateTimeStamp).ToList();

                        InMemoryService.Set("carousels", data);
                        return data;
                    }
                }, forceRefresh);
        }

        internal static IEnumerable<Claudia.Site.Models.Widget> GetWidgets(bool forceRefresh = false)
        {
            return InMemoryService.Get("widgets",
                () =>
                {// calls this function only if data don't allready exists
                    using (var uow = new UnitOfWork())
                    {
                        var widgetData = uow.ApplicationContext.Widgets.Where(w => w.IsDeleted == false).OrderBy(o => o.SortOrder).ToList();
                        var widgets = new List<Claudia.Site.Models.Widget>();

                        foreach (var w in widgetData)
                        {
                            //get type and query for data
                            var data = uow.ApplicationContext.Links.FirstOrDefault(d => d.ObjectCategory.EntityName == w.ReferenceType && d.IsDeleted == false && d.Id == w.ReferenceId);

                            if (data != null)
                            {
                                var n = new Claudia.Site.Models.Widget
                                {
                                    SortOrder = w.SortOrder,
                                    WidgetName = w.WidgetName,
                                    HeaderText = data.Title,
                                    FooterText = data.ServerFileName,
                                    SubWidget = new SubWidget { WidgetName = w.SubWidgetName, Description = data.Description, LinkAddress = data.ServerFileName }
                                };

                                widgets.Add(n);
                            }
                        }

                        InMemoryService.Set("widgets", widgets);
                        return widgets;
                    }
                }, forceRefresh);


        }

        internal static Link GetAnnouncement(bool forceRefresh = false)
        {
            return InMemoryService.Get("announcement",
                () =>
                {// calls this function only if data dont allready exists
                    using (var uow = new UnitOfWork())
                    {
                        var data = uow.ApplicationContext.Links.Where(c => c.IsDeleted == false && c.CategoryId == 7).ToList();

                        InMemoryService.Set("announcement", data);
                        return data;
                    }
                }, forceRefresh).FirstOrDefault();
        }

        internal static void Refresh()
        {
            GetCarouselPictureLinks(true);
            GetWidgets(true);
        }

        /// <summary>
        /// Gets controllers for creating dynamic menu
        /// </summary>
        /// <param name="forceRefresh"></param>
        /// <returns></returns>
        internal static IEnumerable<MenuModel> GetControllerNames(bool forceRefresh = false)
        {
            return InMemoryService.Get(QuickDataKey.Controllers.ToString(),
                () =>
                {
                    var cons = Assembly.GetCallingAssembly().GetTypes()
                        .Where(type => Attribute.IsDefined(type, typeof (MenuAttribute))).ToList();
                    var list = new List<MenuModel>();

                    cons.ForEach(t =>
                    {
                        var attrData = t.GetCustomAttributesData()
                            .Where(a => a.AttributeType.Name == "MenuAttribute");
                        attrData.ForEach(att =>
                        {
                            var model = new MenuModel();
                            model.Group = att.ConstructorArguments[0].Value.ToString();
                            model.PositionInGroup = int.Parse(att.ConstructorArguments[1].Value.ToString());
                            model.DisplayName = att.ConstructorArguments[2].Value.ToString();
                            model.HtmlAttributes = att.ConstructorArguments[3].Value as string;
                            model.RouteValues = att.ConstructorArguments[4].Value as string;
                            model.ControllerName = t.Name.Replace("Controller", "");
                            model.ActionName = "Index";
                            model.InnerHtml = att.ConstructorArguments[5].Value as string;
                            model.RouteName = att.ConstructorArguments[6].Value as string;

                            list.Add(model);
                        });
                    });
                    InMemoryService.Set(QuickDataKey.Controllers.ToString(), list);

                    return list;
                }, forceRefresh);
        }

        public void SubscriptionUpdate(object message)
        {
            Refresh();
        }


        enum QuickDataKey
        {
            EntityCategories,
            Controllers
        }
    }
}