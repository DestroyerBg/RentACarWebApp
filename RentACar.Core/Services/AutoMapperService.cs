using RentACar.Core.Interfaces;
using RentACar.Services.Infrastructure.AutoMapper;

namespace RentACar.Core.Services
{
    public class AutoMapperService : IMapService
    { 
        public TDestination Map<TSource, TDestination>(TSource source) 
            where TSource : IMapTo<TDestination>
        {
            TDestination result = Activator.CreateInstance<TDestination>();
            AutoMapperConfig.MapperInstance.Map(source, result);

            return result;
        }

    }
}
