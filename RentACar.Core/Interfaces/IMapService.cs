using RentACar.Services.Infrastructure.AutoMapper;
namespace RentACar.Core.Interfaces
{
    public interface IMapService
    {
        TDestination Map<TSource, TDestination>(TSource source)
            where TSource : IMapTo<TDestination>;

    }
}
