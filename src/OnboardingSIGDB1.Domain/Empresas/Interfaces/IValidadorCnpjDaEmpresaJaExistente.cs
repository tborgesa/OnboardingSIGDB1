using System.Threading.Tasks;

namespace OnboardingSIGDB1.Domain.Empresas.Interfaces
{
    public interface IValidadorCnpjDaEmpresaJaExistente
    {
        Task<bool> ValidarAsync(string cnpj, int? id = null);
    }
}
