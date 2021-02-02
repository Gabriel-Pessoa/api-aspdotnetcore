# API Web ASP.NET Core

Criamos uma API de cursos que realiza o cadastro de usuário e cursos. Cada curso está relacionado a um usuário, e este precisa está registrado e logado para adicionar novos cursos. Após o login um token é gerado permitindo a inclusão de novos cursos; esse token expira em um dia, para continuar com a inclusão de cursos  o usuário precisará logar novamente. A API persiste os dados num Banco SQL-Server. Utilizamos alguns Design Patterns para tornar a aplicação escalável e de fácil manutenção, por exemplo: Decorator nas classes do Controller; uma classe "ErrorsList" genérica que captura falhas de validação do Data Annotations e envia junto com a BadRequest; Injeção de Dependência e Inversão de Controle facilitando testes e aproveitamento de código.  



* **Passos para rodar a aplicação:**

1. Instalar o Sql-Server: [SQL SERVER 2019 EXPRESS](https://go.microsoft.com/fwlink/?linkid=866658).

2. Se quiser, pode definir o Secret do JwtConfigurations no arquivo: Course.Api\appsettings.json. Ou, pode utilizar o padrão do repositório. **Obs: Esta chave deve ser uma String que só a API conhece, é uma Chave Privada, portanto seria  uma falha grave de segurança expô-la, principalmente em um repositório público.**

   ```
   //...
   "JwtConfigurations": {
       "Secret": "MzfsT&d9gprP>!9$Es(X!5g@;ef!5sbk:jh\\2.}8ZP'qY#7" //Defina uma string complexa
     },
    //...
   ```

3. Definir o DefaultConnection do ConnectionStrings no arquivo: Course.Api\appsettings.json. **Obs: Também é uma chave privada e não deve ser exposta.**

``` 
 "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=course;user=sa;password=<yourpassword>"
  },
  //...
```

4. Copiar o mesmo valor do DefaultConnection acima no arquivo: Course.Api\Configurations\DbFactoryDbContext.cs. **Obs: Por se tratar da mesma chave privada acima, baseado na arquitetura da API, a melhor abordagem aqui seria utilizar uma variável de ambiente.**

   ````
   //...
   var optionsBuilder = new DbContextOptionsBuilder<CourseDbContext>();
   optionsBuilder.UseSqlServer("Server=localhost;Database=course;user=sa;password=<yourpassword>"); //Aqui
   CourseDbContext context = new CourseDbContext(optionsBuilder.Options);
   //...
   ````

5. Restaurar os pacotes dentro do diretório raiz: **./Course.Api**:

   > dotnet restore

6. Rodar as migrations dentro do diretório raiz: **./Course.Api** :

   > dotnet ef migrations add InitialBase

7. Enfim, rodar a aplicação:

   > dotnet run

8. Acessar o Swagger:

   > https://localhost:5001/