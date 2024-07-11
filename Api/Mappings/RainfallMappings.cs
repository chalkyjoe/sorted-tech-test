using AutoMapper;
using Domain.Models;
using RainfallApi.Models;

namespace Api.Mappings;

public class RainfallMapping : Profile
{
    public RainfallMapping( )
    {
        CreateMap<Item, RainfallReading>()
            .ForMember( m => m.AmountMeasured, m => m.MapFrom( x => x.Value ) )
            .ForMember( m => m.DateMeasured, m => m.MapFrom( x => x.DateTime ) );
    }
}