using Microsoft.AspNetCore.Mvc;
using OnboardingSIGDB1.Api._Base.Controllers;
using OnboardingSIGDB1.Api.Models.Empresas;
using OnboardingSIGDB1.Domain.Empresas.Dto;
using OnboardingSIGDB1.Domain.Empresas.Interfaces;
using OnboardingSIGDB1.Domain.Empresas.Specifications;
using OnboardingSIGDB1.IOC.AutoMapper.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Api.Controllers
{
    public class EmpresaController : OnboardingSIGDB1Controller
    {
        private readonly IArmazenadorDeEmpresa _armazenadorDeEmpresa;
        private readonly IEmpresaRepositorio _empresaRepositorio;
        private readonly IExclusaoDeEmpresa _exclusaoDeEmpresa;

        public EmpresaController(IArmazenadorDeEmpresa armazenadorDeEmpresa,
            IEmpresaRepositorio empresaRepositorio, 
            IExclusaoDeEmpresa exclusaoDeEmpresa)
        {
            _armazenadorDeEmpresa = armazenadorDeEmpresa;
            _empresaRepositorio = empresaRepositorio;
            _exclusaoDeEmpresa = exclusaoDeEmpresa;
        }

        [HttpPost("ObterComFiltro")]
        public async Task<IActionResult> ObterComFiltro(EmpresaFiltro empresaFiltro)
        {
            var empresas = await _empresaRepositorio.BuscarAsync(ObterAsEmpresasSpecification.Novo().
                ComNome(empresaFiltro.Nome).
                ComCnpj(empresaFiltro.Cnpj).
                ComIntervaloDeDataDeFundacao(empresaFiltro.DataDeFundacaoFinal, empresaFiltro.DataDeFundacaoFinal)
                .Build());

            return Ok(empresas.MapTo<List<EmpresaDto>>());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var empresa = await _empresaRepositorio.ObterPorIdAsync(id);

            if (empresa == null)
                return Ok();

            return Ok(empresa.MapTo<EmpresaDto>());
        }

        [HttpPost()]
        public async Task<IActionResult> Post(EmpresaDto empresaDto)
        {
            await _armazenadorDeEmpresa.ArmazenarAsync(empresaDto);

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, EmpresaDto empresaDto)
        {
            empresaDto.Id = id;
            await _armazenadorDeEmpresa.ArmazenarAsync(empresaDto);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _exclusaoDeEmpresa.ExcluirAsync(id);

            return Ok();
        }
    }
}
