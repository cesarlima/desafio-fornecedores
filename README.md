# Litagem de Fornecedores

Web API para cadastrar e listar empresas e seus fornecedores

## Como executar

1. No projeto *WebApi* configure a string de conexção que se encontra no arquivo *appsettings.json*.
2. Com o terminal navegue até a pasta *FORNECEDORES*.
3. Execute os seguintes comandos para criar o banco de dados e rodar a API:
```
dotnet ef database update -p ./src/Infra/Infra.csproj -s ./src/WebApi/WebApi.csproj

dotnet run -p ./src/WebApi/WebApi.csproj
```

### Pré-requisitos

* .NET Core 3.1
* SQL Server

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details