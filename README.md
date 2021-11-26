# TRABALHO DE CONCLUSÃO DE CURSO
Implementação gerada como resultado da monografia apresentada ao Curso de Sistemas de Informação da Pontifícia Universidade Católica de Minas Gerais, como requisito parcial para obtenção do título de Bacharel em Sistemas de Informação.

# BIBLIOTECA DE SOFTWARE - ApiMapping
Esta é uma biblioteca de software desenvolvida segundo a especificação Microsoft .NET Standard 2.0 que, ao ser utilizada em uma Web Api Microsoft .NET Core que suporte tal especificação, permite o mapeamento de um ambiente de serviços Web baseado em Web APIs. 
Dados são obtidos das requisições recebidas por estas Web APIs em tempo de execução, sendo armazenados em uma base de dados Microsoft SQL Server e, através da análise destes dados numa modelagem em um Grafo Direcionado, podem ser identificadas: 
- Web APIs que não possuem dependências com outras APIs (que provavelmente estão no fim da cadeia de requisições para acessar algum repositório de dados);
- Web APIs que dependem de si mesmas (que por algum motivo desconhecido fazem alguma requisição HTTP a si próprias, necessitando uma análise para o cenário);
- Web APIs que possuem muitas dependências;
- Web APIs com dependências desatualizadas (que podem ter sido descontinuadas);
- Dependências entre Web APIs que provavelmente não deveriam existir;
- E até mesmo a identificação da cadeia completa de uma requisição HTTP, partindo de uma Web API, passando por outras, até chegar na última Web API que processará a requisição antes de respondê-la ao seu cliente chamador.

# MODELAGEM DO BANCO DE DADOS
Modelagem da base de dados “ApiMappingDatabase” utilizada pela biblioteca de software desenvolvida (**Fonte: Elaborada pelo autor**)
![image](https://user-images.githubusercontent.com/13300754/143625970-187dc608-f0ec-4f72-831a-caee534b9d84.png)

# ESTRUTURA DA SOLUÇÃO
Estrutura das pastas e arquivos da biblioteca de software (**Fonte: Elaborada pelo autor**)
![image](https://user-images.githubusercontent.com/13300754/143626067-439fefd8-1dbf-4d80-90c8-cfd1e796f7cc.png)

# COMO UTILIZAR A BIBLIOTECA
- Executar o script "ApiMappingDatabaseScripts - Structure.sql" em seu servidor de banco de dados.
- Importar a biblioteca de software para sua solução.
- Incluir a seguinte ConnectionString em sua aplicação
- > "ApiMappingDatabaseConnectionString": "Server=YOUR_SERVER,YOUR_SERVER_PORT;Database=ApiMappingDatabase;User ID=YOUR_USER;Password=YOUR_PASSWORD"
Como configurar a comunicação com banco de dados da biblioteca para o projeto (**Fonte: Elaborada pelo autor**)
![image](https://user-images.githubusercontent.com/13300754/143626733-6a256811-a7fa-4ded-9c2f-3fb26db00ba8.png)
- Realizar as seguintes injeções de dependência:
Como realizar as injeções de dependência da biblioteca para o projeto (**Fonte: Elaborada pelo autor**)
![image](https://user-images.githubusercontent.com/13300754/143626290-c02b5d25-0dc2-4c1f-96a9-e64188a584ac.png)
- Incluir cabeçalhos específicos no envio de cada requisição HTTP:
Como configurar o envio de cabeçalhos HTTP da biblioteca para o projeto (**Fonte: Elaborada pelo autor**)
![image](https://user-images.githubusercontent.com/13300754/143626659-9e4ea630-38c4-4cd2-8fd6-a6dc520e9a77.png)
Como configurar o envio de cabeçalhos HTTP no consumo de recursos/endpoints (**Fonte: Elaborada pelo autor**)
![image](https://user-images.githubusercontent.com/13300754/143626692-f2426ea5-b742-44e2-bebe-b550dda55a18.png)

# RESULTADO DO USO DA BIBLIOTECA
- Executar o script "ApiMappingDatabaseScripts - Structure.sql" em seu servidor de banco de dados.
![image](https://user-images.githubusercontent.com/13300754/143626889-aab41bea-9a02-4076-9a50-932fd3ffb845.png)

# RELATÓRIOS GERADOS PELA BIBLIOTECA
- Exemplos de como obter relatórios gerados pela bibliteca, ou obter o grafo para gerar seus próprios relatórios:

Como utilizar a classe de geração de relatórios da biblioteca no projeto (**Fonte: Elaborada pelo autor**)
![image](https://user-images.githubusercontent.com/13300754/143626955-a718cc26-bdf7-474d-8331-8488b6c92a36.png)

Resultados apresentados num relatório (**Fonte: Elaborada pelo autor**)
![image](https://user-images.githubusercontent.com/13300754/143626861-110fb3b8-2bcc-4762-958c-bcaf017118e6.png)

