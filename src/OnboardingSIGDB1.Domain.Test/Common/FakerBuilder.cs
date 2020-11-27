using Bogus;

namespace OnboardingSIGDB1.Domain.Test.Common
{
    public class FakerBuilder
    {
        private static string _linguagem;

        public static FakerBuilder Novo()
        {
            _linguagem = "pt_BR";

            return new FakerBuilder();
        }

        public Faker Build()
        {
            return new Faker(_linguagem);
        }
    }
}
