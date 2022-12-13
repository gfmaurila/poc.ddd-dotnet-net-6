# Estrutura da API
- .NET Core 6: Framework para desenvolvimento da Microsoft.
- AutoMapper: Biblioteca para realizar mapeamento entre objetos.
- Swagger: Documentação para a API.
- DDD: Domain Drive Design modelagem de desenvolvimento orientado a injeção de dependência.
- Entity FrameWork
- XUnit
- FluentValidator

# SqlServer Docker
- docker --version
- Server=localhost,1433;Database=UserAPI;User ID=sa;Password=@G23u10i1985@
- docker build -t gfmaurila/sqlserver .
- docker pull mcr.microsoft.com/mssql/server
- docker run -v ~/docker --name sqlserver -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=@G23u10i1985@" -p 1433:1433 -d gfmaurila/sqlserver

# Iniciar os segredos de usuários
- dotnet user-secrets init --project .\Demo.API.csproj 
- dotnet user-secrets list --project .\Demo.API.csproj

# Configurar a string de conexão ao banco de dados
- dotnet user-secrets set "ConnectionStrings:UserAPI" "Server=localhost,1433;Database=UserAPI;User=sa;Password=@C23l10a1985;MultipleActiveResultSets=true"
- dotnet user-secrets list --project .\Demo.API.csproj

# Configurar dados de autenticação (JWT)
- dotnet user-secrets set "Jwt:Key" "cHJqc2xuYmFjay1ndWlsaGVybWVtYXVyaWxh"
- dotnet user-secrets set "Jwt:Login" "login"
- dotnet user-secrets set "Jwt:Password" "@G23u10i1985@"

# Banco
- Add-Migration InitialCreate
- Update-Database

# Nuget Encrypt Decryp
- https://www.nuget.org/packages/PocNugetEncryptDecrypt/


## Autor

- Guilherme Figueiras Maurila

[![Linkedin Badge](https://img.shields.io/badge/-Guilherme_Figueiras_Maurila-blue?style=flat-square&logo=Linkedin&logoColor=white&link=https://www.linkedin.com/in/guilherme-maurila-58250026/)](https://www.linkedin.com/in/guilherme-maurila-58250026/)
[![Gmail Badge](https://img.shields.io/badge/-gfmaurila@gmail.com-c14438?style=flat-square&logo=Gmail&logoColor=white&link=mailto:gfmaurila@gmail.com)](mailto:gfmaurila@gmail.com)


