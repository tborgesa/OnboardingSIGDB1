namespace OnboardingSIGDB1.Domain._Base.Resources
{
    public static class Resource
    {
        public const string OnboardingSIGDB1 = "OnboardingSIGDB1";

        public const string MensagemNaoExisteNoBancoDeDadosFeminino = "{0} não encontrada.";
        public const string MensagemNaoExisteNoBancoDeDadosMasculino = "{0} não encontrado.";

        public const string MensagemJaExisteCadastradoFeminino = "Já existe uma {0} cadastrada com o {1} informado.";
        public const string MensagemJaExisteCadastradoMasculino = "Já existe um {0} cadastrado com o {1} informado.";

        public const string MensagemDeCampoInvalido = "O campo {0} deve ser um valor válido.";

        public const string MensagemDeErro500 = "Estamos passando por alguns problemas técnicos.";

        public const string Ambiente = "ASPNETCORE_ENVIRONMENT";
        public const string Desenvolvimento = "Development";

        public const string Post = "POST";
        public const string Put = "PUT";
        public const string Delete = "DELETE";

        public static string FormatarResource(string valor1, string valor2, string valor3)
        {
            return string.Format(valor1, valor2, valor3);
        }

        public static string FormatarResource(string valor1, string valor2)
        {
            return string.Format(valor1, valor2);
        }

        public static string FormatarResourceToLowerValor2(string valor1, string valor2)
        {
            return FormatarResource(valor1, valor2.ToLower());
        }

        public static string FormatarResourceToLowerValor2(string valor1, string valor2, string valor3)
        {
            return FormatarResource(valor1, valor2.ToLower(), valor3);
        }


    }
}
