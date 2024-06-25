# Documentação da Aplicação em C# usando .NET Core e Visual Studio Community

Bem-vindo à documentação da aplicação [Nome da Aplicação]! Esta documentação fornecerá instruções sobre como configurar, compilar e executar a aplicação usando o .NET Core e o Visual Studio Community.

## Requisitos do Sistema

Antes de começar, verifique se o seu sistema atende aos seguintes requisitos:

- Sistema Operacional: Windows 10, macOS, Linux
- [.NET Core SDK](https://dotnet.microsoft.com/download): Versão 3.1 ou superior
- [Visual Studio Community](https://visualstudio.microsoft.com/vs/community/): Versão mais recente

## Instalação do .NET Core SDK

1. Baixe e instale o [.NET Core SDK](https://dotnet.microsoft.com/download) a partir do site oficial da Microsoft.
2. Siga as instruções de instalação fornecidas pelo instalador.

## Instalação do Visual Studio Community

1. Baixe e instale o [Visual Studio Community](https://visualstudio.microsoft.com/vs/community/) a partir do site oficial da Microsoft.
2. Siga as instruções de instalação fornecidas pelo instalador.

## Clonando o Repositório

1. Abra o Visual Studio Community.
2. Selecione "Clone or checkout code" na tela inicial ou no menu "File".
3. Insira a URL do repositório da aplicação.
4. Selecione o diretório de destino e clique em "Clone".

## Configuração e Compilação

1. Abra o arquivo de solução (.sln) da aplicação no Visual Studio Community.
2. Aguarde o Visual Studio restaurar os pacotes NuGet.
3. Selecione a configuração de compilação desejada (Debug ou Release) no menu suspenso.
4. Clique em "Build Solution" no menu "Build" ou pressione Ctrl + Shift + B para compilar a solução.

## Executando a Aplicação

1. Selecione o projeto de inicialização correto no Visual Studio Community.
2. Pressione F5 para iniciar a depuração ou Ctrl + F5 para iniciar sem depuração.

## Testando a Aplicação

### Postman

1. Baixe e instale o [Postman](https://www.postman.com/downloads/).
2. Abra o Postman e importe a coleção de requisições da aplicação.
3. Execute as requisições da coleção para testar os endpoints.

### Swagger

1. Abra a URL da aplicação no navegador.
2. Navegue até a documentação Swagger da API.
3. Explore os endpoints disponíveis e teste-os diretamente na interface Swagger.

## Endpoints da API

Aqui estão os endpoints disponíveis na API:

### Cliente

- **GET** `/api/Cliente`: Retorna todos os clientes cadastrados no sistema.
- **GET** `/api/Cliente/{id}`: Retorna os detalhes de um cliente específico com base no ID fornecido.
- **POST** `/api/Cliente`: Cria um novo cliente no sistema.
- **PUT** `/api/Cliente/{id}`: Atualiza os dados de um cliente existente com base no ID fornecido.
- **DELETE** `/api/Cliente/{id}`: Remove um cliente do sistema com base no ID fornecido.

### Investimento

- **GET** `/api/Investimento`: Retorna todos os investimentos registrados no sistema.
- **GET** `/api/Investimento/{id}`: Retorna os detalhes de um investimento específico com base no ID fornecido.
- **GET** `/api/Investimento/cliente/{clienteId}`: Retorna todos os investimentos de um cliente específico com base no ID do cliente fornecido.
- **GET** `/api/Investimento/extrato/{clienteId}`: Retorna o extrato de investimentos de um cliente específico com base no ID do cliente fornecido.
- **GET** `/api/Investimento/extrato-pdf/{clienteId}`: Retorna o extrato de investimentos de um cliente específico em formato PDF.
- **GET** `/api/Investimento/extrato-produto`: Retorna os investimentos agrupados por produto financeiro.
- **GET** `/api/Investimento/extrato-produto-pdf`: Retorna os investimentos agrupados por produto financeiro em formato PDF.
- **POST** `/api/Investimento/compra`: Registra uma nova transação de compra de investimento no sistema.
- **PUT** `/api/Investimento/venda/{id}`: Atualiza uma transação de venda de investimento existente com base no ID fornecido.
- **DELETE** `/api/Investimento/{id}`: Remove um investimento do sistema com base no ID fornecido.

### Produto Financeiro

- **GET** `/api/ProdutoFinanceiro`: Retorna todos os produtos financeiros disponíveis no sistema.
- **GET** `/api/ProdutoFinanceiro/{id}`: Retorna os detalhes de um produto financeiro específico com base no ID fornecido.
- **POST** `/api/ProdutoFinanceiro`: Adiciona um novo produto financeiro ao sistema.
- **PUT** `/api/ProdutoFinanceiro/{id}`: Atualiza os detalhes de um produto financeiro existente com base no ID fornecido.
- **DELETE** `/api/ProdutoFinanceiro/{id}`: Remove um produto financeiro do sistema com base no ID fornecido.

### Transação

- **GET** `/api/Transacao`: Retorna todas as transações de investimento registradas no sistema.
- **GET** `/api/Transacao/{id}`: Retorna os detalhes de uma transação de investimento específica com base no ID fornecido.

## Testando os Endpoints

Certifique-se de testar cada endpoint para garantir que a aplicação esteja funcionando corretamente. Utilize o Postman ou a interface Swagger para enviar requisições e verificar as respostas.

## Conclusão

Parabéns! Você concluiu a documentação da aplicação Sistema de Gestão de Portfólio de Investimentos. Esperamos que esta documentação seja útil para configurar, compilar, executar e testar a aplicação. Se encontrar algum problema ou tiver dúvidas, não hesite em entrar em contato conosco.
