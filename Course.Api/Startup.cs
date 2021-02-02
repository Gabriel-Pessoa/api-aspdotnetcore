using Course.Api.Business.Repositories;
using Course.Api.Configurations;
using Course.Api.Infraestruture.Data;
using Course.Api.Infraestruture.Data.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace Course.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .ConfigureApiBehaviorOptions(options =>
            {
                // Desabilito o padr�o para customiza��o
                options.SuppressModelStateInvalidFilter = true;
            });

            // Add Swagger. Informando e lendo arquivo de metadata na raiz do projeto "Course.Api.xml"
            services.AddSwaggerGen(c =>
            {
                //Definindo Bearer como nosso esquema de seguran�a, acessado atraves do objeto Header na propriedade Authorization
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });

                // Nessa linha abaixo, ele est� usando uma t�cnica de refection para capturar o nome do projeto (Course.Api) e concatena com o ".xml".
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile); // Define o diret�rio, no caso o diret�rio do projeto
                c.IncludeXmlComments(xmlPath); // inclui os coment�rios no Swagger
            });

            var secret = Encoding.ASCII.GetBytes(Configuration.GetSection("JwtConfigurations:Secret").Value); // Ler a chave e converte em bytes
            //Configurando middleware
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false; // Desativa https
                x.SaveToken = true; // "Cache" do token
                // Par�metros de configura��o
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secret),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddDbContext<CourseDbContext>(options => 
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddScoped<IUserRepository, UserRepository>(); // o framework invoca o new internamente
            services.AddScoped<ICourseRepository, CourseRepository>(); // o framework invoca o new internamente
            services.AddScoped<IAuthenticationService, JwtService>(); // o framework invoca o new internamente
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication(); // configura��o para conseguir mapear

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger(); // Incluindo o middleware do Swagger

            app.UseSwaggerUI(c =>
            {
                // Configura a rota do Swagger. Foi gerado anteriormente o arquivo no middleware e aqui configuramos para l�-lo
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Course APi");
                c.RoutePrefix = string.Empty; //swagger (exclui a necessidade de digita na routa swagger para acess�-lo)
            });
        }
    }
}
