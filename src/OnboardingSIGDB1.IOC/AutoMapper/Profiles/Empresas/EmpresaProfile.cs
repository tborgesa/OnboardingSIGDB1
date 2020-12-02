using AutoMapper;
using OnboardingSIGDB1.Domain._Base.Helpers;
using OnboardingSIGDB1.Domain.Empresas.Dto;
using OnboardingSIGDB1.Domain.Empresas.Entidades;

namespace OnboardingSIGDB1.IOC.AutoMapper.Profiles.Empresas
{
    public class EmpresaProfile : Profile
    {
        public EmpresaProfile()
        {
            CreateMap<Empresa, EmpresaDto>().
                ForMember(dto => dto.Id, model => model.MapFrom(_ => _.Id)).
                ForMember(dto => dto.Nome, model => model.MapFrom(_ => _.Nome)).
                ForMember(dto => dto.Cnpj, model => model.MapFrom(_ => _.Cnpj.FormatarMascaraDoCnpj())).
                ForMember(dto => dto.DataDeFundacao, model => model.MapFrom(_ => _.DataDeFundacao));
        }
    }
}
