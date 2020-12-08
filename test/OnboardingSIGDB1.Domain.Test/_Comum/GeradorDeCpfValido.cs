using System.Collections;
using System.Collections.Generic;

namespace OnboardingSIGDB1.Domain.Test._Comum
{
    public class GeradorDeCpfValido : IEnumerable<object[]>
    {
        private List<object[]> GerarNovosCpfs()
        {
            var onboardingSIGDB1faker = OnboardingSIGDB1FakerBuilder.Novo().Build();
            var listaDeCpfValidos = new List<object[]>();
            for (int i = 0; i < 5; i++)
            {
                listaDeCpfValidos.Add(new object[] { onboardingSIGDB1faker.Cpf() });
            }

            return listaDeCpfValidos;
        }

        public IEnumerator<object[]> GetEnumerator() => GerarNovosCpfs().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GerarNovosCpfs().GetEnumerator();
    }
}
