using Microsoft.AspNetCore.Mvc;
using OnboardingSIGDB1.Api._Base.Controllers;
using OnboardingSIGDB1.Domain.Cargos.Dto;
using OnboardingSIGDB1.Domain.Cargos.Interfaces;
using OnboardingSIGDB1.IOC.AutoMapper.Extensions;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Api.Controllers
{
    public class CargoController : OnboardingSIGDB1Controller
    {
        private readonly IArmazenadorDeCargo _armazenadorDeCargo;
        private readonly ICargoRepositorio _cargoRepositorio;

        public CargoController(IArmazenadorDeCargo armazenadorDeCargo,
            ICargoRepositorio cargoRepositorio)
        {
            _armazenadorDeCargo = armazenadorDeCargo;
            _cargoRepositorio = cargoRepositorio;
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
            var cargo = await _cargoRepositorio.ObterPorIdAsync(id);

            if (cargo == null)
                return Ok();

            return Ok(cargo.MapTo<CargoDto>());
        }

        [HttpPost()]
        public async Task<IActionResult> Post(CargoDto cargoDto)
        {
            await _armazenadorDeCargo.ArmazenarAsync(cargoDto);

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, CargoDto cargoDto)
        {
            cargoDto.Id = id;
            await _armazenadorDeCargo.ArmazenarAsync(cargoDto);

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
