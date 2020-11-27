using OnboardingSIGDB1.Domain.Cargos;
using OnboardingSIGDB1.Domain.Test.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnboardingSIGDB1.Domain.Test.Builders
{
    public class CargoBuilder : BuilderBase
    {
        private int _id;
        private static string _descricao;

        public static CargoBuilder Novo()
        {
            var fake = FakerBuilder.Novo().Build();

            _descricao = fake.Lorem.Random.AlphaNumeric(250);

            return new CargoBuilder();
        }

        public CargoBuilder ComDescricao(string descricao)
        {
            _descricao = descricao;
            return this;
        }

        public CargoBuilder ComId(int id)
        {
            _id = id;
            return this;
        }

        public Cargo Build()
        {
            var cargo = new Cargo(_descricao);

            AtribuirId(_id, cargo);

            return cargo;
        }
    }
}
