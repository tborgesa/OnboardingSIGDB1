using Microsoft.AspNetCore.Mvc;
using OnboardingSIGDB1.Api._Base.Controllers;
using OnboardingSIGDB1.Domain.Empresas.Dto;
using OnboardingSIGDB1.Domain.Empresas.Interfaces;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Api.Controllers
{
    public class EmpresaController : OnboardingSIGDB1Controller
    {
        private readonly IArmazenadorDeEmpresa _armazenadorDeEmpresa;

        public EmpresaController(IArmazenadorDeEmpresa armazenadorDeEmpresa)
        {
            _armazenadorDeEmpresa = armazenadorDeEmpresa;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            await Task.CompletedTask;
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            await Task.CompletedTask;
            return Ok();
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
            await Task.CompletedTask;
            return Ok();
        }
    }
}
