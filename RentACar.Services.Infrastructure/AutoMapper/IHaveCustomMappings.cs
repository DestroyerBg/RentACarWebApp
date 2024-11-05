using AutoMapper;

namespace RentACar.Services.Infrastructure.AutoMapper
{
    public interface IHaveCustomMappings
    {
        void CreateMappings(IProfileExpression configuration);
    }
}
