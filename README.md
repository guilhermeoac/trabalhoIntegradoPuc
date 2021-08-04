# LocacaoVeiculo
Serviço de locação de veículos desenvolvido para fins fictícios utilizando de conhecimento técnico e teórico para avaliação de conhecimento. Os dados trafegados pelas APIs não serão disponibilizados nem poderão ser utilizados para insumo de qualquer meio produtivo. 

## Desafio
Construir um serviço que simule e realize locações de veículos, verificando previamente suas disponibilidades e exibindo histórico de locações.

### Requisitos
  __Back-end:__
  - Cadastro de perfis cliente e/ou operador;
  - Cadastro de marca, modelo e veículos;
  - Simulação e locação de veículos;
  - Disponibilização de modelo de contrato de locação em PDF;
  - Devolução do veículo com reajuste de valor com base em um check-list de vistoria;
  
  __Front-end:__
  - Cadastro e login de clientes retornando os dados do usuário logado;
  - Listagem de veículos por categoria;
  - Disponibilização de dados e valores totais do veículo para realizar a locação;
  - Autenticação do usuário para realizar a locação;
  - Histórico de locações por usuário por período;

## Arquitetura

Para a nossa arquitetura separamos as responsabilidades em três grandes grupos que se comunicam entre si e entregam um serviço mais escalavél e independente, sendo eles o *Front-end*, o *Back-end* e uma camada auxiliar central chamada de *BFF(Back-end for Front-end)*.

No Front-end onde ficam agrupado nossos recursos de interação com o usuários, foi utilizado um SPA(Single Page Application), levando em consideração a simplicidade e a quantidade de telas que seriam necessárias para entregar a experiência no serviço.

No Back-end, nosso maior grupo, ficaram concentrados nossos serviços que dão vida ao sistema. Neste caso, foram criados 3 microsserviços separados pelos contextos de Usuario, Veículo e Locação. Cada um destes microsserviços fazem persistência dos seus dados em um banco de dados SqlServer que apesar de utilizarem o mesmo banco, estão disponibilizados em schemas diferentes, aumentando assim ainda mais a independência entre cada elo do processo.

No microsserviço de Usuario ficaram concentradas as APIs referente aos Clientes e Operadores onde foram criados *Cruds* para ambos. Para o microsserviço de Veículo ficou a responsabilidade de cuidar das APIs de Marca, Modelo e Veiculo que também foram criadas em modo *Crud*. Já para o microsserviço de Locação, como a responsabilidade dele era um pouco maior, foi utilizado um conceito arquitetural chamado *Onion*, onde destribuímos também dentro do próprio serviço responsabilidades individuais entre cada camada. E é nele onde se concentram as regras de negócio e o *Crud* para a entidade locação.  

Para o *BFF*, a responsabilidade foi de orquestrar as chamadas da tela para o devido serviço requisitado.

Um desenho arquitetural também está disponibilizado na pasta "Arquitetura" (https://github.com/guilhermeoac/trabalhoIntegradoPuc/tree/master/Arquitetura) dentro da pasta raiz do projeto, para ilustração e melhor entendimento.

## Tecnologias utilizadas

Todos os serviços são ASP.NET Core Web Application, utilizando versão 3.1. Para a comuniação com o banco de dados foi utilizado o Microsoft.EntityFrameworkCore.SqlServer versão 2.1.1, junto do conceito *Code First*, onde criamos nossas classes e com comandos de *Migrations* um espelhamento dessas entidades já criam as tabelas no banco.

Testes unitários utilizando xUnit pela sua versatilidade e adaptabilidade com bibliotecas chaves como a *Moq*, facilidade de uso e conhecimento prévio. 

Para o Front-end HTML 5, CSS3, Javascript, Jquery e Bootstrap para as estilizações visuais.

## Como Executar

Os microsserviços estão configurados e documentados com o *Swagger*, então estão disponíveis para acesso rodando em conjunto ou individualmente pelas chamadas diretas nas APIs.

Para o funcionamento pleno do serviço web é necessário que todos os outros serviços estejam também em execução, apesar que se executado individualmente, ainda é possivel ter acesso à tela principal.

__Um ponto de atenção é__ que como os serviços fazem persistência dos dados em banco, será necessario que o avaliador tenha instalado o SQL Server Management Studio (disponível para download no link https://docs.microsoft.com/pt-br/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver15) e executar o seguinte comando para os serviços de Usuário, Veículo e Locação:
 - Abra o arquivo appsettings.json > Dentro da connectionStrings garanta que o *source=* seja o mesmo nome do servidor do seu SQL Server Management Studio. Ex.:
   - *"ConnectionStrings": { "LocacaoVeicDB": "data source=LOK-00499\\SQLEXPRESS;initial catalog=LocacaoVeicDB;integrated security=True"}*
 - Na barra superior navegue até *Tools* > NuGet Package Manager > Package Manager Console;
 - Abrirá um terminal. Nele adicione o comando *update-database*. As tabelas necessárias serão criadas em seu banco de dados e os serviços estarão prontos para uso.
