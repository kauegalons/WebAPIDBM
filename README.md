WebAPIDBM
Descrição do Projeto
O WebAPIDBM é um projeto desenvolvido para gerenciar dados de uma aplicação baseada em APIs. Ele fornece funcionalidades para operar e gerenciar informações, permitindo uma integração eficiente com outras plataformas e serviços.

Configuração e Execução do Projeto Localmente
Pré-requisitos
Ferramentas Necessárias:

.NET SDK (versão 6.0 ou superior)
Banco de dados SQL Server ou outro configurado no projeto
IDE compatível, como Visual Studio ou Visual Studio Code
Clonar o Repositório:

bash
Copiar código
git clone <URL_DO_REPOSITORIO>
cd <NOME_DO_DIRETORIO>
Passos para Configuração
Restaurar Dependências: Navegue até o diretório do projeto e execute:

bash
Copiar código
dotnet restore
Configurar a String de Conexão:

Localize o arquivo appsettings.json no projeto principal.
Atualize a string de conexão com as credenciais e informações corretas do banco de dados.
Executar a Aplicação:

Execute o comando:
bash
Copiar código
dotnet run
A aplicação estará disponível no endereço padrão http://localhost:5000 (ou conforme configurado).
Executar Migrações do Banco de Dados
O projeto utiliza Entity Framework Core para gerenciar migrações. Para rodar as migrações:

Adicionar novas migrações:

bash
Copiar código
dotnet ef migrations add <NomeDaMigracao>
Aplicar as migrações ao banco de dados:

bash
Copiar código
dotnet ef database update
Nota: Certifique-se de que a ferramenta CLI do Entity Framework está instalada. Caso contrário, instale com:

bash
Copiar código
dotnet tool install --global dotnet-ef
Executar Testes Unitários
Para rodar os testes unitários do projeto:

Navegue até o diretório de testes, se aplicável.
Execute o comando:
bash
Copiar código
dotnet test
Os resultados dos testes serão exibidos no terminal.

Contribuição
Se desejar contribuir com o projeto, siga estas etapas:

Faça um fork do repositório.
Crie um branch para a sua feature ou correção:
bash
Copiar código
git checkout -b feature/nome-da-feature
Faça um commit das suas alterações:
bash
Copiar código
git commit -m "Descrição da alteração"
Envie o branch para o repositório remoto:
bash
Copiar código
git push origin feature/nome-da-feature
Abra um Pull Request. 
