using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Domain._Base.Interfaces
{
    public interface ICadastroCompletoRepositorio<TId, TEntidade>
    {
        Task AdicionarAsync(TEntidade entidade);
        Task<IEnumerable<TEntidade>> BuscarAsync(Expression<Func<TEntidade, bool>> predicate);
        Task<IEnumerable<TEntidade>> BuscarAsync(IEnumerable<Expression<Func<TEntidade, bool>>> predicate);
        Task<IEnumerable<TEntidade>> ListarAsync();
        Task<TEntidade> ObterPorIdAsync(TId id);
        void Remover(TEntidade entidade);
    }
}
