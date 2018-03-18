# Intelipost: Teste Prático Desenvolvedor de Integrações

Foi criada para resolução do teste uma WebAPI a partir da linguagem de programação C#, utilizando-se da estrutura de desenvolvimento MVC 5. A escolha destas ferramentas para o desenvolvimento baseou-se na familiaridade e experiência com as mesmas.

O tempo gasto para o `desenvolvimento do código fonte` foi de aproximadamente **6h**, acrescentando mais algumas horas necessárias para `compreender o funcionamento do Git` e para a `formulação das instruções de utilização`.

## Sobre o código fonte ##

O código fonte desenvolvido resume-se à 3 classes principais, responsáveis por realizar todas as operações necessárias para integrar os dados recebidos do sistema de Rastreamento, em um formato totalmente compatível com o sistema de Vendas.

* **EnumStatus**

Nesta classe estão presentes os enumeradores para os diferentes status dos pedidos. Aqui, a partir de uma informação em comum entre os dois sistemas (que é o que o status representa), é possível extrair tanto seu valor numérico (sistema de rastreamento) quanto seu valor em texto correspondente (sistema de vendas). 

```` 
[Description("in_transit")]
EmTransito      = 1,
	    
[Description("to_be_delivered")]
SaiuParaEntrega = 2,

[Description("delivered")]
Entregue        = 3
```` 

* **EnumExtensions**

Esta classe é a responsável por fazer a tradução entre os status do pedido. A partir de um valor numérico (que representa o status do pedido) o método `GetEnumDescription` recupera o valor correspondente em forma de texto, buscando estas informações na classe EnumStatus.

* **IntegrationController**

Esta classe é a responsável por obter as requisições feitas pelo sistema de vendas, através do método `IntegrateTrackingSalesSystem`, que recebe como parâmetro as informações do pedido em forma de JSON.

Dentro deste método, os dados são deserializados em um objeto genérico para que em seguida, seja criado um novo objeto apenas com as informações relevantes para o sistema de vendas, e em seu formato esperado.

Dentro deste método, é possível identificar a chamada de um outro método interno chamado `GetEnumDescriptionByValue`, que recebe um tipo de enum genérico (ou seja, este método serve para qualquer enum existente na aplicação, e não apenas para o EnumStatus criado anteriormente. Para isso, basta que na chamada deste seja especificado qual Enum será utilizado para buscar as informações) e utiliza o método criado dentro da classe EnumExtensions para obter os dados.

````
this.GetEnumDescriptionByValue<EnumStatus.OrderStatus>("status_id")
````

Por fim, o método tem como response o novo objeto esperado pelo sistema de vendas, em formato JSON.

## Instruções de utilização

**A) Baixando a API**

1. Fazer download da [Api de Integração](https://github.com/DoVale-Daniel/job-integration-developer/archive/master.zip);
2. Extrair o conteúdo do download para uma pasta local;

&nbsp;

**B) Publicando a API**

Para a publicação da API desenvolvida, existem duas formas diferentes (uma delas requer utilização do **Visual Studio** para Build e Deploy, e a outra requer apenas publicação da api que se encontra na pasta **DeployAPI**).

&nbsp;

> **Importante:** Antes de publicar a API, é importante que o seu computador tenha instalado o .NET Framework 4.5.1 ou superior, e tenha os recursos do Serviços de Informação da Internet (IIS) habilitados.

&nbsp;

**Alternativa 1: Através do Visual Studio**

1. Abrir o Visual Studio como Administrador;
2. Abrir a solução do projeto contida na pasta "IntegracaoVendas.API";
3. Dar build na solution (Ctrl + Shift + B);
4. No windows, abrir o "Gerenciador de Serviços de Informações da Internet (IIS)";
5. Clicar com o botão direito em "Pool de Aplicativos" > "Adicionar Pool de Aplicativos";
6. Criar um Aplicativo com as configurações:
	
	- Nome = WebApiPool
	- Versão do .NET Framework = v4.0.30319
	- Modo de Pipeline gerenciado = Integrado
	- Iniciar pool de aplicativos imediatamente = sim

> **OBS:** Caso já possua um pool com estas configurações, o mesmo pode ser utilizado, sem a necessidade de criar um novo

7. Em "Sites > Default Web Site", clicar com o botão direito em "IntegracaoVendas.API" > "Gerenciar Aplicativo" > "Configurações Avançadas";

8. Na opção "Pool de Aplicativos", selecionar o novo pool que foi criado no passo 6, em seguida clicar em OK;

&nbsp;

**Alternativa 2: Sem Visual Studio**

1. Abrir a pasta "DeployAPI" e copiar a pasta "IntegracaoVendas.API" que está dentro dela;
2. Abrir a pasta "C:/inetpub/wwroot" e colar a pasta que foi copiada no passo anterior;
3. No windows, abrir o "Gerenciador de Serviços de Informações da Internet (IIS)";
4. Clicar com o botão direito em "Pool de Aplicativos" > "Adicionar Pool de Aplicativos";
5. Criar um Aplicativo com as configurações:
	
	- Nome = WebApiPool
	- Versão do .NET Framework = v4.0.30319
	- Modo de Pipeline gerenciado = Integrado
	- Iniciar pool de aplicativos imediatamente = sim

> **OBS:** Caso já possua um pool com estas configurações, o mesmo pode ser utilizado, sem a necessidade de criar um novo

6. Em "Sites > Default Web Site", clicar com o botão direito em "IntegracaoVendas.API" > "Converter para Aplicativo";

7. Na opção "Pool de Aplicativos", selecionar o pool criado no passo 5, e em seguida clicar em OK. Depois, clicar em OK novamente;

&nbsp;

**C) Acessando a API**

1. Via Postman, iniciar uma requisição do tipo POST, acessando a URL: 

````
http://localhost/IntegracaoVendas.Api/Integration/IntegrateTrackingSalesSystem?orderData={"order_id":123,"event":{"status_id":1,"date":"2018-02-02T10:45:32"},"package":{"package_id":1,"package_invoice":{"number":"9871236","key":"01234567890123456789012345678901234567891234","date":"2018-02-01T10:45:32" }}}
````

> **OBS:** O conteúdo do parâmetro "orderData" é o conteúdo no formato enviado pelo sistema de RASTREAMENTO (json). Os dados deste parâmetros são transformados dentro da API para o formato esperado pelo sistema de VENDAS.
