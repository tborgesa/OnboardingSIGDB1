using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace OnboardingSIGDB1.Domain._Base.Helpers
{
    public static class CnpjHelper
    {
        static readonly int[] mt1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        static readonly int[] mt2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        static readonly string pattern = @"(?i)[^0-9]";

        public static string FormatarMascaraDoCnpj(this string cnpj)
        {
            if (string.IsNullOrEmpty(cnpj)) return null;

            return Convert.ToUInt64(cnpj).ToString(@"00\.000\.000\/0000\-00");
        }

        public static string RemoverMascaraDoCnpj(this string cnpj)
        {
            if (String.IsNullOrEmpty(cnpj))
                return cnpj;

            string replacement = "";

            Regex rgx = new Regex(pattern);
            cnpj = rgx.Replace(cnpj.Trim(), replacement);

            return cnpj;
        }

        public static bool ValidarCnpj(this string cnpj)
        {
            if (String.IsNullOrEmpty(cnpj))
                return false;

            int soma, resto;

            string replacement = "";

            Regex rgx = new Regex(pattern);
            cnpj = rgx.Replace(cnpj.Trim(), replacement);

            if (cnpj.Length != 14)
                return false;

            if (cnpj == "00000000000000" || cnpj == "11111111111111" ||
                cnpj == "22222222222222" || cnpj == "33333333333333" ||
                cnpj == "44444444444444" || cnpj == "55555555555555" ||
                cnpj == "66666666666666" || cnpj == "77777777777777" ||
                cnpj == "88888888888888" || cnpj == "99999999999999")
                return false;

            string tempCnpj = cnpj.Substring(0, 12);
            soma = 0;

            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * mt1[i];

            resto = (soma % 11);

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            string digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;

            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * mt2[i];

            resto = (soma % 11);

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto;

            return cnpj.EndsWith(digito);
        }
    }
}
