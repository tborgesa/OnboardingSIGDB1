using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Domain.Funcionarios.Interfaces
{
    interface IValidadorCpfDaFuncionarioJaExistente
    {
        Task<bool> ValidarAsync(string cpf, int? id = null);
    }
}
