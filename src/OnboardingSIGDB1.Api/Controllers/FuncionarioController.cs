using Microsoft.AspNetCore.Mvc;
using OnboardingSIGDB1.Api._Base.Controllers;
using OnboardingSIGDB1.Api.Models.Funcionarios;
using OnboardingSIGDB1.Domain.Funcionarios.Dto;
using OnboardingSIGDB1.Domain.Funcionarios.Interfaces;
using OnboardingSIGDB1.Domain.Funcionarios.Specifications;
using OnboardingSIGDB1.IOC.AutoMapper.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Api.Controllers
{
    public class FuncionarioController : OnboardingSIGDB1Controller
    {
        private readonly IArmazenadorDeFuncionario _armazenadorDeFuncionario;
        private readonly IFuncionarioRepositorio _funcionarioRepositorio;
        private readonly IVinculadorDeFuncionarioNaEmpresa _vinculadorDeFuncionarioNaEmpresa;
        private readonly IVinculadorDeFuncionarioNoCargo _vinculadorDeFuncionarioNoCargo;
        private readonly IExclusaoDeFuncionario _exclusaoDeFuncionario;

        public FuncionarioController(IArmazenadorDeFuncionario armazenadorDeFuncionario,
            IFuncionarioRepositorio funcionarioRepositorio,
            IVinculadorDeFuncionarioNaEmpresa vinculadorDeFuncionarioNaEmpresa,
            IVinculadorDeFuncionarioNoCargo vinculadorDeFuncionarioNoCargo,
            IExclusaoDeFuncionario exclusaoDeFuncionario)
        {
            _armazenadorDeFuncionario = armazenadorDeFuncionario;
            _funcionarioRepositorio = funcionarioRepositorio;
            _vinculadorDeFuncionarioNaEmpresa = vinculadorDeFuncionarioNaEmpresa;
            _vinculadorDeFuncionarioNoCargo = vinculadorDeFuncionarioNoCargo;
            _exclusaoDeFuncionario = exclusaoDeFuncionario;
        }

        [HttpPost("ObterComFiltro")]
        public async Task<IActionResult> ObterComFiltro(FuncionarioFiltro funcionarioFiltro)
        {
            var funcionarios = await _funcionarioRepositorio.BuscarAsync(ObterOsFuncionariosSpecification.
                Novo().
                ComNome(funcionarioFiltro.Nome).
                ComCpf(funcionarioFiltro.Cpf).
                ComIntervaloDeDataDeContratacao(funcionarioFiltro.DataDeContratacaoInicial, funcionarioFiltro.DataDeContratacaoFinal).
                Build());

            return Ok(funcionarios.MapTo<List<FuncionarioDto>>());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var funcionario = await _funcionarioRepositorio.ObterPorIdAsync(id);

            if (funcionario == null)
                return Ok();

            return Ok(funcionario.MapTo<FuncionarioDto>());
        }

        [HttpPost()]
        public async Task<IActionResult> Post(FuncionarioDto funcionarioDto)
        {
            await _armazenadorDeFuncionario.ArmazenarAsync(funcionarioDto);

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, FuncionarioDto funcionarioDto)
        {
            funcionarioDto.Id = id;
            await _armazenadorDeFuncionario.ArmazenarAsync(funcionarioDto);

            return Ok();
        }

        [HttpPut("VincularNaEmpresa/{funcionarioId}/{empresaId}")]
        public async Task<IActionResult> Put(int funcionarioId, int empresaId)
        {
            await _vinculadorDeFuncionarioNaEmpresa.Vincular(funcionarioId, empresaId);

            return Ok();
        }

        [HttpPut("VincularNoCargo")]
        public async Task<IActionResult> Put(CargoDoFuncionarioDto cargoDoFuncionarioDto)
        {
            await _vinculadorDeFuncionarioNoCargo.Vincular(cargoDoFuncionarioDto);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _exclusaoDeFuncionario.ExcluirAsync(id);

            return Ok();
        }
    }
}
