using Microsoft.EntityFrameworkCore;
using OnboardingSIGDB1.Domain._Base.Entidades;
using OnboardingSIGDB1.Domain._Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Data._Base
{
    public class RepositorioBase<TId, TEntidade> :
        IRepositorioBase<TId, TEntidade>
        where TId : struct
        where TEntidade : Entidade<TId, TEntidade>
    {
        private readonly DbSet<TEntidade> _dbSet;
        public RepositorioBase(DbContext context)
        {
            _dbSet = context.Set<TEntidade>();
        }

        public async Task AdicionarAsync(TEntidade entidade) => await _dbSet.AddAsync(entidade);

        public async Task<IEnumerable<TEntidade>> BuscarAsync(Expression<Func<TEntidade, bool>> predicate) =>
            await _dbSet.Where(predicate).ToListAsync();

        public async Task<IEnumerable<TEntidade>> ListarAsync() => await _dbSet.ToListAsync();
        

        public async Task<TEntidade> ObterPorIdAsync(TId id) => await _dbSet.FirstOrDefaultAsync(e => EqualityComparer<TId>.Default.Equals(e.Id, id));

        public void Remover(TEntidade entidade) => _dbSet.Remove(entidade);
    }
}
