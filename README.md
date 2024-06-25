# Documentação do Sistema de Gestão de Portfólio de Investimentos

<p align="center">
      <img src="https://img.shields.io/badge/Status-Em%20Desenvolvimento-yellowgreen"/>
      <a href="https://github.com/laismanieri">
        <img alt="Feito por Lais Manieri" src="https://img.shields.io/badge/feito%20por-laismanieri-yellow">
      </a>
      <img alt="GitHub last commit" src="https://img.shields.io/badge/project%20-%20Backend-yellowgreen">
</p>


  ## 💻 Sobre o projeto
  
  <p align="justify"> O Sistema de Gestão de Portfólio de Investimentos é uma aplicação desenvolvida para permitir que uma empresa de consultoria financeira gerencie os investimentos disponíveis e os clientes comprem, vendam e acompanhem seus investimentos. O sistema foi desenvolvido em C# e utiliza o ASP.NET Core para fornecer serviços no backend. Este documento fornece informações detalhadas sobre como instalar, configurar e utilizar o sistema.
  
  ---
 ### Pré-requisitos 
  Antes de começar, você vai precisar ter instalado em sua máquina as seguintes ferramentas:
  -   **SDK .NET Core:** Certifique-se de ter o SDK .NET Core instalado em sua máquina. Você pode baixá-lo **[aqui](https://dotnet.microsoft.com/pt-br/download)** 
  -   **Ferramenta de Desenvolvimento:** Recomenda-se o uso de um editor de código como [VSCode](https://code.visualstudio.com/) ou [Visual Studio Community](https://nodejs.org/en/download/)
  -   **Postman (Opcional):** Para testar as APIs, você pode usar o Postman. Ele pode ser baixado **[aqui](https://www.postman.com/downloads/)**
 ---

 ## 🛠 Configuração do Ambiente de Desenvolvimento:
  
  1. Instale o Visual Studio ou Visual Studio Code.
  
  2. Clone o Repositório:      
      Clone o repositório para o seu ambiente de desenvolvimento local usando o seguinte comando:
     ```sh
     git clone https://github.com/laismanieri/gestao_portfolio_investimento.git
    
  3. Acesse o Diretório do Projeto:    
      Navegue até o diretório do projeto usando o terminal ou o prompt de comando:
     ```sh
     cd gestao_portfolio_investimento
    
  4. Restaurar Dependências:
      Use o comando dotnet restore para restaurar as dependências do projeto:
     ```sh
     dotnet restore
    
  5. Executar a Aplicação:
      SQL Server: Configure a string de conexão no arquivo appsettings.json:
     ```sh
     "ConnectionStrings": {
          "DefaultConnection": "Server=YOUR_SERVER;Database=YOUR_DATABASE;User Id=YOUR_USER;Password=YOUR_PASSWORD;"
      }
      ```
     MySQL: Configure a string de conexão no arquivo appsettings.json para MySQL se estiver usando Pomelo.EntityFrameworkCore.MySql:
     ```sh
     "ConnectionStrings": {
          "DefaultConnection": "Server=YOUR_SERVER;Database=YOUR_DATABASE;User Id=YOUR_USER;Password=YOUR_PASSWORD;"
      }
     
 6. Executar a Aplicação:
      Aplique as migrações para configurar o banco de dados:
     ```sh
     dotnet ef database update

 7. Executar a Aplicação:
      Aplique as migrações para configurar o banco de dados:
     ```sh
     dotnet ef database update    
    
  8. Testar as APIs:
      Para testar as APIS acesse [aqui](https://github.com/laismanieri/gestao_portfolio_investimento/blob/main/GETTING_STARTED.md) a documentação de utilização.

  9. Documentação Adicional:
      Se necessário, consulte a documentação oficial do .NET Core para obter mais informações sobre o desenvolvimento e execução de aplicativos .NET Core: Documentação do **[aqui](https://learn.microsoft.com/pt-br/dotnet/fundamentals/)**

---

<div align="justify"> 
## 🛠 Tecnologias
  
  -   Linguagem de Programação C#: A aplicação é desenvolvida principalmente em C#, que é uma linguagem de programação moderna, orientada a objetos e fortemente tipada, amplamente utilizada para desenvolvimento de aplicativos na plataforma .NET.

  -   ASP.NET Core: Framework para desenvolvimento de aplicativos web e APIs em C#, permitindo a criação de serviços RESTful e endpoints da API.

  -   Entity Framework Core (EF Core): Um ORM (Object-Relational Mapping) que simplifica o acesso e a manipulação de dados em bancos de dados relacionais, permitindo que as entidades do modelo de dados sejam mapeadas para tabelas do banco de dados.

  -   ASP.NET Core Identity: Fornece recursos para autenticação, autorização e gerenciamento de usuários.

  -   Swagger: Utilizado para documentar e testar APIs, permitindo aos desenvolvedores visualizar e interagir facilmente com os endpoints da API.

  -   JSON (JavaScript Object Notation): Formato de dados amplamente utilizado para troca de informações entre o cliente e o servidor.

  -   RESTful APIs: As funcionalidades da aplicação são expostas através de APIs RESTful, seguindo os princípios e padrões de design REST para comunicação entre clientes e servidores.

  -   Visual Studio / Visual Studio Code: IDEs (Integrated Development Environments) populares utilizadas para desenvolvimento em C#, oferecendo recursos avançados de edição, depuração e compilação.
    
  -   Swagger: Uma ferramenta para documentar, testar e visualizar APIs REST de forma amigável para os desenvolvedores. Integra-se facilmente a aplicativos ASP.NET Core, gerando automaticamente uma documentação interativa para a API. O Swagger simplifica o desenvolvimento, teste e integração de APIs, tornando o processo mais eficiente e colaborativo.
    
  -   MySQL: Um sistema de gerenciamento de banco de dados relacional open-source amplamente utilizado para armazenar e gerenciar dados. O MySQL é conhecido por sua confiabilidade, escalabilidade e desempenho, sendo uma escolha popular para aplicativos web e empresariais.

  -   Git: Um sistema de controle de versão distribuído amplamente utilizado para gerenciamento de código-fonte. Permite que os desenvolvedores trabalhem colaborativamente em projetos de software, acompanhem alterações no código, revertam para versões anteriores e integrem alterações de forma eficiente. Comumente utilizado em conjunto com plataformas de hospedagem de código, como GitHub, GitLab e Bitbucket, para facilitar a colaboração e o compartilhamento de código entre equipes.

---
  ## Modelagem de banco de dados
<div align="center">  
      <img alt="Modelo de banco de dados" src=https://github.com/laismanieri/gestao_portfolio_investimento/assets/82177551/f6f6dfaf-1044-44a6-b956-94f91e5e4cde">
</div>
  
---  


## Documentação de Dependências do Projeto
É dependência é um componente externo ou biblioteca que um projeto de software precisa para funcionar corretamente. Essas dependências fornecem funcionalidades adicionais que não estão incluídas no código principal do projeto.
Este documento explica cada dependência utilizada no projeto e como instalá-las através do NuGet no Visual Studio Code.

## Dependências
### iTextSharp.LGPLv2.Core
- **Descrição:** Biblioteca para manipulação de documentos PDF.
- **Instalação:**
  ```bash
  dotnet add package iTextSharp.LGPLv2.Core --version 3.4.20

### Microsoft.EntityFrameworkCore
- **Descrição:** Provedor de acesso a dados ORM (Object-Relational Mapping) para o Entity Framework Core.
- **Instalação:**
  ```bash
  dotnet add package Microsoft.EntityFrameworkCore --version 6.0.29

### Microsoft.EntityFrameworkCore.Design
- **Descrição:** Ferramentas de design para Entity Framework Core, incluindo scaffolding de migrações.
- **Instalação:**
  ```bash
  dotnet add package Microsoft.EntityFrameworkCore.Design --version 6.0.29

### Microsoft.EntityFrameworkCore.SqlServer
- **Descrição:** Provedor de banco de dados SQL Server para Entity Framework Core.
- **Instalação:**
  ```bash
  dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 6.0.29

### Microsoft.EntityFrameworkCore.Tools
- **Descrição:** Provedor de banco de dados SQL Server para Entity Framework Core.
- **Instalação:**
  ```bash
  dotnet add package Microsoft.EntityFrameworkCore.Tools --version 6.0.29

### Microsoft.VisualStudio.Web.CodeGeneration.Design
- **Descrição:** Ferramentas de geração de código para ASP.NET Core.
- **Instalação:**
  ```bash
  dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design --version 6.0.16

### Pomelo.EntityFrameworkCore.MySql
- **Descrição:** Provedor de banco de dados MySQL para Entity Framework Core.
- **Instalação:**
  ```bash
  dotnet add package Pomelo.EntityFrameworkCore.MySql --version 6.0.2

### Quartz
- **Descrição:** Biblioteca de agendamento de tarefas (job scheduling).
- **Instalação:**
  ```bash
  dotnet add package Quartz --version 3.9.0

### SendGrid
- **Descrição:** Cliente para a API do SendGrid, utilizado para envio de e-mails.
- **Instalação:**
  ```bash
  dotnet add package SendGrid --version 9.29.3

### Swashbuckle.AspNetCore
- **Descrição:** Gera documentação Swagger para APIs ASP.NET Core.
- **Instalação:**
  ```bash
  dotnet add package Swashbuckle.AspNetCore --version 6.5.0

## Instruções de Instalação pelo NuGet no Visual Studio Community
Passo a Passo
1. Abra o Visual Studio Community:
            Inicie o Visual Studio Community e abra seu projeto
2. Gerenciador de Pacotes NuGet:
            Clique com o botão direito do mouse na solução ou no projeto em que deseja adicionar as dependências.
            Selecione Gerenciar Pacotes NuGet... no menu de contexto.
3. Procurar Pacotes:
            No Gerenciador de Pacotes NuGet, vá até a aba Procurar.
            No campo de pesquisa, digite o nome do pacote que deseja instalar (por exemplo, iTextSharp.LGPLv2.Core).
4. Instalar Pacotes:      
            Selecione o pacote correto na lista de resultados da pesquisa.
            Clique no botão Instalar.
5. Aceite os termos de licença, se solicitado:
            Repita os passos de 3 a 4 para cada dependência listada abaixo.

---

  ## ⚙️ Configuração das Variáveis de Ambiente para o SendGrid

Passo a Passo
1. Crie uma Conta no SendGrid:
            Acesse o site do SendGrid e crie uma conta se você ainda não tiver uma.
2. Obtenha sua Chave de API do SendGrid:
            Faça login na sua conta do SendGrid.
            No painel do SendGrid, navegue até as configurações da API ou chaves de API.
            Crie uma nova chave de API ou copie uma chave existente.
3. Procurar Pacotes:
            Faça login na sua conta do SendGrid.
            No painel do SendGrid, navegue até as configurações da API ou chaves de API.
            Crie uma nova chave de API ou copie uma chave existente.
4. Defina a Variável de Ambiente no seu Sistema:     
            No código da sua aplicação, você pode acessar essa variável de ambiente para obter a chave de API do SendGrid.
            Dependendo da linguagem de programação e do framework que você está utilizando, pode haver diferentes maneiras de acessar variáveis de ambiente. No entanto, a maioria das linguagens oferece uma maneira de fazer isso de forma simples e direta.
5. Teste a Configuração:
            Depois de configurar a variável de ambiente, teste sua aplicação para garantir que ela esteja usando a chave de API do SendGrid corretamente.
            Você pode enviar e-mails de teste para verificar se o envio está funcionando conforme esperado.
   
---

  ## ⚙️ Funcionalidades
 
      
:heavy_check_mark: **Cadastro de Clientes:** Permite adicionar novos clientes ao sistema. Para cada cliente, são fornecidos detalhes como nome, endereço de e-mail, data de nascimento e endereço.

:heavy_check_mark: **Cadastro de Investimentos:** Permite adicionar novos investimentos ao sistema. Cada investimento está associado a um cliente e a um produto financeiro específico. Detalhes como quantidade, valor de compra e data de vencimento são fornecidos durante o cadastro.

:heavy_check_mark: **Cadastro de Produto Financeiro:** Permite adicionar novos produtos financeiros ao sistema. Os produtos financeiros representam os ativos disponíveis para investimento e podem incluir ações, títulos, fundos mútuos, etc. Para cada produto financeiro, são fornecidos detalhes como tipo, nome, valor, etc.

:heavy_check_mark: **Cadastro de Transação:** Permite adicionar novas transações ao sistema. As transações representam as operações de compra e venda de produtos financeiros. Cada transação está associada a um investimento específico e inclui detalhes como quantidade, valor unitário e tipo de transação (compra ou venda).

:heavy_check_mark: **Listagem de Clientes:** Permite visualizar uma lista de todos os clientes cadastrados no sistema, incluindo detalhes como nome, e-mail, data de nascimento e endereço.

:heavy_check_mark: **Listagem de Investimentos:** Permite visualizar uma lista de todos os investimentos cadastrados no sistema, incluindo detalhes como cliente associado, produto financeiro, quantidade, valor de compra e data de vencimento.

:heavy_check_mark: **Listagem de Produtos Financeiros:** Permite visualizar uma lista de todos os produtos financeiros cadastrados no sistema, incluindo detalhes como tipo, nome, valor, etc.

:heavy_check_mark: **Listagem de Transações:** Permite visualizar uma lista de todas as transações cadastradas no sistema, incluindo detalhes como investimento associado, quantidade, valor unitário e tipo de transação.

:heavy_check_mark: **Atualização de Clientes:** Permite atualizar os detalhes de um cliente existente no sistema, como nome, endereço de e-mail, data de nascimento, etc.

:heavy_check_mark: **Atualização de Investimentos:** Permite atualizar os detalhes de um investimento existente no sistema, como quantidade, valor de compra, data de vencimento, etc.

:heavy_check_mark: **Atualização de Produto Financeiro:** Permite atualizar os detalhes de um produto financeiro existente no sistema, como tipo, nome, valor, etc.

:heavy_check_mark: **Deleção de Clientes:** Permite excluir um cliente existente do sistema juntamente com todos os seus investimentos associados.

:heavy_check_mark: **Deleção de Investimentos:** Permite excluir um investimento existente do sistema.

:heavy_check_mark: **Deleção de Produto Financeiro:** Permite excluir um produto financeiro existente do sistema.

:heavy_check_mark: **Negociar Produto Financeiro (Compra e Venda):** Permite aos clientes comprar ou vender produtos financeiros disponíveis no sistema.

:heavy_check_mark: **Extrato do Produto:** Permite visualizar um extrato detalhado de um produto financeiro específico, incluindo todas as transações associadas a ele, como compras, vendas, etc.

:heavy_check_mark: **Extrato do Produto (PDF):** Permite gerar um extrato detalhado de um produto financeiro específico no formato PDF, incluindo todas as transações associadas a ele, como compras, vendas, etc.

:heavy_check_mark: **Extrato dos Investimentos por Cliente (PDF):** Permite gerar um extrato detalhado de um produto financeiro específico no formato PDF, incluindo todas as transações associadas a ele, como compras, vendas, etc.

:heavy_check_mark: **Disparo de e-mail automatico:** Disparo de e-mail diario com a relação dos investimentos que estão próximos do vencimento.

## 👨‍💻 Contribuidores 
</div>
Backend

 [<img src="https://avatars.githubusercontent.com/u/82177551?v=4" width=115><br><sub>Lais Manieri</sub>](https://github.com/laismanieri) 

  


