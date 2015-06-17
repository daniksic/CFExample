using System.Linq;
using AutoMapper;
using Claudia.Domain.Models.v1;
using Claudia.Site.Areas.CMS.Models;

namespace Claudia.Site
{
    public static class MapperConfig
    {
        public static void Init()
        {
            Mapper.CreateMap<Link, LinkViewModel>()
                .ForMember(m => m.CategoryName, r => r.Ignore())
                .ForMember(m => m.Urls, c => c.ResolveUsing(r => r.LinksUrls));

            Mapper.CreateMap<LinkViewModel, Link>()
                .ForMember(m => m.LinksUrls, c => c.ResolveUsing(r => r.Urls))
                .ForMember(m=>m.CategoryId, c=> c.Condition(src=> src.CategoryId > 0))
                .IgnoreAllNonExisting();

            Mapper.AssertConfigurationIsValid();
        }

        public static IMappingExpression<TSource, TDestination> IgnoreAllNonExisting<TSource, TDestination>(this IMappingExpression<TSource, TDestination> expression)
        {
            var sourceType = typeof(TSource);
            var destinationType = typeof(TDestination);
            var existingMaps = Mapper.GetAllTypeMaps().First(x => x.SourceType.Equals(sourceType)
                && x.DestinationType.Equals(destinationType));
            foreach (var property in existingMaps.GetUnmappedPropertyNames())
            {
                expression.ForMember(property, opt => opt.Ignore());
            }
            return expression;
        }
    }
}