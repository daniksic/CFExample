using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using System.Web.UI.WebControls;
using System.Web.WebPages;
using Claudia.Domain.Configuration;

namespace Claudia.Site.Helpers
{
    public static class HtmlExtensions
    {
        public static MvcHtmlString GoogleAds(this HtmlHelper helper, string clinet, string slot, int width, int height)
        {
            var html =
            string.Format(
                "<div class=\"row text-center\"><script type=\"text/javascript\">google_ad_client = \"{0}\";" + 
                " google_ad_slot = \"{1}\"; google_ad_width = {2}; google_ad_height = {3};</script> " +
                "<script type=\"text/javascript\" src=\"http://pagead2.googlesyndication.com/pagead/show_ads.js\"></script></div>",
                clinet, slot, width, height);

            return MvcHtmlString.Create(html);
        }

        public static MvcHtmlString ThumbnailFor<TModel, TResult>(this HtmlHelper<TModel> html,
            Expression<Func<TModel, TResult>> expression, string entityName)
        {
            return ThumbnailFor(html, expression, entityName, null);
        }
        public static MvcHtmlString ThumbnailFor<TModel, TResult>(this HtmlHelper<TModel> html,
            Expression<Func<TModel, TResult>> expression, string entityName, IDictionary<string,string> attributeData)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            var value = metadata.Model;

            var imgurl = AppConfig.ImageData[entityName].GetThumnailUrl(value.ToString());

            var tag = new TagBuilder("img");
            tag.Attributes.Add("src", imgurl);

            if (attributeData != null)
            {
                foreach (var item in attributeData)
                {
                    tag.Attributes.Add(item.Key, item.Value);
                }
            }

            //left for demo
            //var fieldName = ExpressionHelper.GetExpressionText(expression);
            //var fullBindingName = html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(fieldName);
            //var fieldId = TagBuilder.CreateSanitizedId(fullBindingName);
            //tag.Attributes.Add("name", fullBindingName);
            //tag.Attributes.Add("id", fieldId);
            //tag.Attributes.Add("type", "text");
            //tag.Attributes.Add("value", value == null ? "" : value.ToString());

            //var validationAttributes = html.GetUnobtrusiveValidationAttributes(fullBindingName, metadata);
            //foreach (var key in validationAttributes.Keys)
            //{
            //    tag.Attributes.Add(key, validationAttributes[key].ToString());
            //}

            return new MvcHtmlString(tag.ToString(TagRenderMode.SelfClosing));
        }

        public static MvcHtmlString Thumbnail(this HtmlHelper html, string entityName, string filename, IDictionary<string, string> attributeData)
        {
            var imgurl = AppConfig.ImageData[entityName].GetThumnailUrl(filename);

            var tag = new TagBuilder("img");
            tag.Attributes.Add("src", imgurl);

            if (attributeData != null)
            {
                foreach (var item in attributeData)
                {
                    tag.Attributes.Add(item.Key, item.Value);
                }
            }

            return new MvcHtmlString(tag.ToString(TagRenderMode.SelfClosing));
        }

        public static MvcHtmlString Menu(this HtmlHelper helper, string groupName)
        {
            var data =
                QuickData.GetControllerNames().Where(g => g.Group.Equals(groupName)).OrderBy(o => o.PositionInGroup);
                //.Where(c => c.Value.StartsWith(nameSpace, StringComparison.InvariantCultureIgnoreCase));

            return helper.Partial("_Menu", data);
        }

        public static MvcHtmlString ActionLinkAdvance(this HtmlHelper helper,
            string actionName, string controllerName, RouteValueDictionary routeValues,
            IDictionary<string, object> htmlAttributes, string innerHtml, string routeName = null)
        {
            string link = ResolveUrl(routeName, actionName, controllerName, routeValues, helper);

            TagBuilder tagBuilder = new TagBuilder("a")
            {
                InnerHtml = !string.IsNullOrEmpty(innerHtml) ? innerHtml : string.Empty
            };
            tagBuilder.MergeAttributes(htmlAttributes);
            tagBuilder.MergeAttribute("href", link);
            return MvcHtmlString.Create(tagBuilder.ToString(TagRenderMode.Normal));
        }

        private static string ResolveUrl(string routeName, string actionName, string controllerName,
            RouteValueDictionary routeValues, HtmlHelper helper)
        {
            return UrlHelper.GenerateUrl(routeName, actionName, controllerName, routeValues,
                helper.RouteCollection, helper.ViewContext.RequestContext, false);
        }

        private static MvcHtmlString BootstrapIconButton(HtmlHelper helper, string text, string actionName, string controllerName, 
            RouteValueDictionary routeValues, string buttonCss, string iconCss)
        {
            //<button type="button" class="btn btn-default" aria-label="Left Align">
            //<span class="glyphicon glyphicon-align-left" aria-hidden="true"></span>
            //</button>
            string link = ResolveUrl(null, actionName, controllerName, routeValues, helper);

            string button = string.Concat(@"<a class=""", buttonCss ,@""" aria-label=""Left Align"" href=""", link,
                @"""><span class=""", iconCss ,@""" aria-hidden=""true""></span> ",
                text, "</a>");

            return MvcHtmlString.Create(button);
        }

        public static MvcHtmlString BootstrapDeleteButton(this HtmlHelper helper, string text, string actionName, string controllerName,
            RouteValueDictionary routeValues)
        {
            return BootstrapIconButton(helper, text, actionName, controllerName, routeValues, "btn btn-danger", "glyphicon glyphicon-trash");
        }

        public static MvcHtmlString BootstrapAddNewButton(this HtmlHelper helper, string text, string actionName, string controllerName,
            RouteValueDictionary routeValues)
        {
            return BootstrapIconButton(helper, text, actionName, controllerName, routeValues, "btn btn-default", "glyphicon glyphicon-plus");
        }

        public static MvcHtmlString BootstrapCancelButton(this HtmlHelper helper, string text, string actionName, string controllerName,
    RouteValueDictionary routeValues)
        {
            return BootstrapIconButton(helper, text, actionName, controllerName, routeValues, "btn btn-default", "glyphicon glyphicon-arrow-left");
        }


        public static MvcHtmlString BootstrapSaveButton(this HtmlHelper helper, string text)
        {
            //<input type="submit" value="Save changes" class="btn btn-success" />

            string button = string.Concat(@"<button class=""btn btn-success"" aria-label=""Left Align"" type=""submit"">
                                            <span class=""glyphicon glyphicon-floppy-save"" aria-hidden=""true""></span> ",
                                            text, "</button>");

            return MvcHtmlString.Create(button);
        }

    }
}