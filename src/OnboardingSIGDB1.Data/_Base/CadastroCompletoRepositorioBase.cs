using OnboardingSIGDB1.Domain._Base.Entidades;
using OnboardingSIGDB1.Domain._Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Data._Base
{
    public abstract class CadastroCompletoRepositorioBase<TId, TEntidade> : OnboardingSIGDB1Repositorio,
        ICadastroCompletoRepositorio<TId, TEntidade>
        where TId : struct
        where TEntidade : Entidade<TId, TEntidade>
    {
        //private readonly DbSet<TEntidade> _dbSet;
        //public CadastroCompletoRepositorioBase(DbContext context)
        //{
        //    _dbSet = context.Set<TEntidade>();
        //}

        public async Task AdicionarAsync(TEntidade entidade) => await Task.CompletedTask;  //await _dbSet.AddAsync(entidade); 

        public async Task<IEnumerable<TEntidade>> BuscarAsync(Expression<Func<TEntidade, bool>> predicate) //=> await _dbSet.Where(predicate).ToListAsync();
        {
            await Task.CompletedTask; return null;
        }

        public async Task<IEnumerable<TEntidade>> ListarAsync() //=> await _dbSet.ToListAsync();
        {
            await Task.CompletedTask; return null;
        }

        public async Task<TEntidade> ObterPorIdAsync(TId id) //=> await _dbSet.FirstOrDefaultAsync(e => EqualityComparer<TId>.Default.Equals(e.Id, id));
        {
            await Task.CompletedTask; return null;
        }

        public void Remover(TEntidade entidade) //=>  _dbSet.Remove(entidade);
        {

        }
    }
}
