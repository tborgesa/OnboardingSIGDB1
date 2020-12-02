using Microsoft.AspNetCore.Mvc;
using OnboardingSIGDB1.Api._Base.Controllers;
using OnboardingSIGDB1.Domain.Funcionarios.Dto;
using OnboardingSIGDB1.Domain.Funcionarios.Interfaces;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Api.Controllers
{
    public class FuncionarioController : OnboardingSIGDB1Controller
    {
        private readonly IArmazenadorDeFuncionario _armazenadorDeFuncionario;

        public FuncionarioController(IArmazenadorDeFuncionario armazenadorDeFuncionario)
        {
            _armazenadorDeFuncionario = armazenadorDeFuncionario;
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await Task.CompletedTask;
            return Ok();
        }
    }
}
