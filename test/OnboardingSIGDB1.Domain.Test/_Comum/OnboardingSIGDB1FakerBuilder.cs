using Bogus;

namespace OnboardingSIGDB1.Domain.Test._Comum
{
    public class OnboardingSIGDB1FakerBuilder
    {
        private static string _linguagem;

        public static OnboardingSIGDB1FakerBuilder Novo()
        {
            _linguagem = "pt_BR";

            return new OnboardingSIGDB1FakerBuilder();
        }

        public OnboardingSIGDB1Faker Build()
        {
            return new OnboardingSIGDB1Faker(_linguagem);
        }
    }
}
