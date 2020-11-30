﻿using OnboardingSIGDB1.Domain._Base.Resources;
using OnboardingSIGDB1.Domain.Funcionarios.Entidades;
using OnboardingSIGDB1.Domain.Test._Comum;
using System;

namespace OnboardingSIGDB1.Domain.Test._Builders
{
    public class FuncionarioBuilder : BuilderBase
    {
        private int _id;
        private static string _nome;
        private static string _cpf;
        private static DateTime? _dataDeContratacao;

        public static FuncionarioBuilder Novo()
        {
            var faker = OnboardingSIGDB1FakerBuilder.Novo().Build();

            _nome = faker.FraseComQuantidadeExataDeCaracteres(Constantes.Numero150);
            _cpf = faker.Cpf();
            _dataDeContratacao = faker.QualquerDataDoUltimoAno();

            return new FuncionarioBuilder();
        }

        public FuncionarioBuilder ComNome(string nome)
        {
            _nome = nome;
            return this;
        }

        public FuncionarioBuilder ComId(int id)
        {
            _id = id;
            return this;
        }

        public FuncionarioBuilder ComCpf(string cpf)
        {
            _cpf = cpf;
            return this;
        }

        public FuncionarioBuilder ComDataDeContratacao(DateTime? dataDeContratacao)
        {
            _dataDeContratacao = dataDeContratacao;
            return this;
        }

        public Funcionario Build()
        {
            var funcionario = new Funcionario(_nome, _cpf, _dataDeContratacao);

            AtribuirId(_id, funcionario);

            return funcionario;
        }
    }
}