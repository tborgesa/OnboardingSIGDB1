using AutoMapper;
using OnboardingSIGDB1.Domain._Base.Helpers;
using OnboardingSIGDB1.Domain.Funcionarios.Dto;
using OnboardingSIGDB1.Domain.Funcionarios.Entidades;

namespace OnboardingSIGDB1.IOC.AutoMapper.Profiles.Funcionarios
{
    public class FuncionarioProfile : Profile
    {
        public FuncionarioProfile()
        {
            CreateMap<Funcionario, FuncionarioDto>().
                ForMember(dto => dto.Id, model => model.MapFrom(_ => _.Id)).
                ForMember(dto => dto.Nome, model => model.MapFrom(_ => _.Nome)).
                ForMember(dto => dto.Cpf, model => model.MapFrom(_ => _.Cpf.RemoverMascaraDoCpf())).
                ForMember(dto => dto.DataDeContratacao, model => model.MapFrom(_ => _.DataDeContratacao));
        }
    }
}
