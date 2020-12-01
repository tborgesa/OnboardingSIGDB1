﻿using OnboardingSIGDB1.Data._Base;
using OnboardingSIGDB1.Domain.Empresas.Entidades;
using OnboardingSIGDB1.Domain.Empresas.Interfaces;
using System;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Data.Empresas
{
    public class EmpresaRepositorio : CadastroCompletoRepositorioBase<int, Empresa>, IEmpresaRepositorio
    {
        public async Task<Empresa> ObterPorCnpjAsync(string cnpj)
        {
            var empresa = new Empresa("dgs", "455464", DateTime.Now);

            await Task.CompletedTask;

            return empresa;
        }
    }
}