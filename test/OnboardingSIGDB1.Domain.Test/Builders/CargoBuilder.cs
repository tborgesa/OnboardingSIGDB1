using OnboardingSIGDB1.Domain._Base.Resources;
using OnboardingSIGDB1.Domain.Cargos.Entidades;
using OnboardingSIGDB1.Domain.Test.Common;

namespace OnboardingSIGDB1.Domain.Test.Builders
{
    public class CargoBuilder : BuilderBase
    {
        private int _id;
        private static string _descricao;

        public static CargoBuilder Novo()
        {
            var faker = FakerBuilder.Novo().Build();

            _descricao = faker.Lorem.Random.AlphaNumeric(Constantes.QuantidadeDeCaracteres250);

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
