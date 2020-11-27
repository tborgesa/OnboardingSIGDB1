using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace OnboardingSIGDB1.Domain._Base.Helpers
{
    public static class CpfHelper
    {
        static readonly int[] multiplicador1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        static readonly int[] multiplicador2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        static readonly string pattern = @"(?i)[^0-9]";

        public static string FormatarMascaraCpf(this string cpf)
        {
            var resultado = string.Empty;
            var retorno = string.Empty;

            for (var i = 0; i < 11; i++)
            {
                resultado = cpf[i].ToString();

                if (i == 2 || i == 5)
                    resultado += ".";

                if (i == 8)
                    resultado += "-";

                retorno += resultado;
            }

            return retorno;
        }

       public static string RemoverMascaraDoCpf(this string cpf)
        {
            if (string.IsNullOrEmpty(cpf))
                return cpf;

            var rgx = new Regex(pattern);
            cpf = rgx.Replace(cpf.Trim(), string.Empty);

            return cpf;
        }

        public static bool ValidarCpf(this string cpf)
        {
            if (string.IsNullOrEmpty(cpf))
                return false;

            var rgx = new Regex(pattern);
            cpf = rgx.Replace(cpf.Trim(), string.Empty);

            if (cpf.Length != 11)
                return false;

            if (cpf == "00000000000" || cpf == "11111111111" ||
                cpf == "22222222222" || cpf == "33333333333" ||
                cpf == "44444444444" || cpf == "55555555555" ||
                cpf == "66666666666" || cpf == "77777777777" ||
                cpf == "88888888888" || cpf == "99999999999")
                return false;

            var tempCpf = cpf.Substring(0, 9);
            var soma = 0;

            for (var i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            var resto = soma % 11;
            if (resto < 2)
            {
                resto = 0;
            }
            else
            {
                resto = 11 - resto;
            }

            var digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (var i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            if (resto < 2)
            {
                resto = 0;
            }
            else
            {
                resto = 11 - resto;
            }

            digito = digito + resto;
            return cpf.EndsWith(digito);
        }
    }
}
