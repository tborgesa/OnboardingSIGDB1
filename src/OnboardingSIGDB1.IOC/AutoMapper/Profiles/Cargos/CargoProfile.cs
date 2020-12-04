using AutoMapper;
using OnboardingSIGDB1.Domain.Cargos.Dto;
using OnboardingSIGDB1.Domain.Cargos.Entidades;

namespace OnboardingSIGDB1.IOC.AutoMapper.Profiles.Cargos
{
    public class CargoProfile : Profile
    {
        public CargoProfile()
        {
            CreateMap<Cargo, CargoDto>().
                ForMember(dto => dto.Id, model => model.MapFrom(_ => _.Id)).
                ForMember(dto => dto.Descricao, model => model.MapFrom(_ => _.Descricao));
        }
    }
}
