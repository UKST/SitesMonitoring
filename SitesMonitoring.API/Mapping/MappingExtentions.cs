using System;
using AutoMapper;

namespace SitesMonitoring.API.Mapping
{
    public static class MappingExtentions
    {
        public static TDestination Map<TDestination>(this object source, IMapper mapper)
        {
            return mapper.Map<TDestination>(source);
        }
        
        public static TDestination Map<TSource, TDestination>(this TSource source, IMapper mapper, Action<TDestination> afterMap)
        {
            return mapper.Map<TSource, TDestination>(source,
                options => options.AfterMap(
                    (s, d) => { afterMap(d); }));
        }
    }
}