﻿using Microsoft.EntityFrameworkCore;
using OnboardingSIGDB1.Domain._Base.Entidades;
using OnboardingSIGDB1.Domain._Base.Helpers;
using OnboardingSIGDB1.Domain._Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Data._Base
{
    public abstract class CadastroCompletoRepositorioBase<TId, TEntidade> : OnboardingSIGDB1Repositorio,
        ICadastroCompletoRepositorio<TId, TEntidade>
        where TId : struct
        where TEntidade : Entidade<TId, TEntidade>
    {
        public readonly DbSet<TEntidade> DbSet;
        public readonly DbContext Context;
        public CadastroCompletoRepositorioBase(DbContext context)
        {
            Context = context;
            DbSet = context.Set<TEntidade>();
        }

        public async Task AdicionarAsync(TEntidade entidade) => await DbSet.AddAsync(entidade);

        public async Task<IEnumerable<TEntidade>> BuscarAsync(Expression<Func<TEntidade, bool>> predicate) => await DbSet.Where(predicate).ToListAsync();

        public async Task<IEnumerable<TEntidade>> BuscarAsync(IEnumerable<Expression<Func<TEntidade, bool>>> predicate) => await DbSet.FiltrarUmaListaDeWhere(predicate).ToListAsync();

        public async Task<IEnumerable<TEntidade>> ListarAsync() => await DbSet.ToListAsync();

        public async Task<TEntidade> ObterPorIdAsync(TId id) => await DbSet.FindAsync(id);

        public void Remover(TEntidade entidade) => DbSet.Remove(entidade);
    }
}
