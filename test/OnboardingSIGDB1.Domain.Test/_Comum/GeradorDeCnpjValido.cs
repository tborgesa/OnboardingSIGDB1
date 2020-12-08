using System.Collections;
using System.Collections.Generic;

namespace OnboardingSIGDB1.Domain.Test._Comum
{
    public class GeradorDeCnpjValido : IEnumerable<object[]>
    {
        private List<object[]> GerarNovosCnpjs()
        {
            var onboardingSIGDB1faker = OnboardingSIGDB1FakerBuilder.Novo().Build();
            var listaDeCpfValidos = new List<object[]>();
            for (int i = 0; i < 5; i++)
            {
                listaDeCpfValidos.Add(new object[] { onboardingSIGDB1faker.Cnpj() });
            }

            return listaDeCpfValidos;
        }

        public IEnumerator<object[]> GetEnumerator() => GerarNovosCnpjs().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GerarNovosCnpjs().GetEnumerator();
    }
}
