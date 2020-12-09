# On Boarding - DB1

Um projeto com [AspNetCore 2.2.0](https://www.nuget.org/packages/Microsoft.AspNetCore/2.2.0)

## Frameworks e bibliotecas utilizadas

##### Web
[AspNetCore 2.2.0](https://www.nuget.org/packages/Microsoft.AspNetCore/2.2.0)
[Swagger 5.6.3](https://www.nuget.org/packages/Swashbuckle.AspNetCore/5.6.3)

##### Dominio
[FluentValidation 7.2.0](https://www.nuget.org/packages/FluentValidation/7.2.0)

##### Mapeamento
[AutoMapper 8.0.0](https://www.nuget.org/packages/AutoMapper/8.0.0)

##### ORM Repositório
[Microsoft.EntityFrameworkCore 2.2.4](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore/2.2.4)

##### Testes de unidade
[xunit 2.4.3](https://www.nuget.org/packages/xunit/2.4.1)
[Moq 4.12.0](https://www.nuget.org/packages/Moq/4.12.0)
[Bogus 29.0.2](https://www.nuget.org/packages/Bogus/29.0.2)

## Base de Dados
Configure a tag `OnboardingSIGDB1.StringDeConexoes.OnboardingSIGDB1` do arquivo `src\OnboardingSIGDB1.Api\appsettings.Development.json` como acesso ao banco Sql Server de sua preferência.

Para criar e atualizar a base de dados execute
`
dotnet ef database update --startup-project .\src\OnboardingSIGDB1.Api\ -p .\src\OnboardingSIGDB1.Data\
`

Caso não tenha a ferramenta do Entity Framework instalada execute
`
dotnet tool install --global dotnet-ef
`

## Development server
Execute `dotnet run -p .\src\OnboardingSIGDB1.Api`. No browser acesse `http://localhost:5100/swagger` para ver a lista de endpoints disponíveis.

## Testes de Unidade

Para exeucutar os testes de unidade 
`
dotnet test .\test\OnboardingSIGDB1.Domain.Test\
`