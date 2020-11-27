namespace OnboardingSIGDB1.Domain._Base.Resources
{
    public static class Resource
    {
        public const string MensagemJaExisteCadastrada = "Já existe uma {0} cadastrada com o {1} informado.";
        public const string MensagemJaExisteCadastrado = "Já existe um {0} cadastrado com o {1} informado.";
        public const string MensagemDeCampoInvalido = "O campo {0} deve ser um valor válido.";

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
