using Example.WebAPI.Middleware;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

using System.Text;

using Example.WebAPI.Utils;
using Example.Application.Interfaces;
using Example.Infrastructure;
using Example.Application;

namespace Example.WebAPI
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
            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IStorageDocumentsService, StorageDocumentsService>();

            services.AddDatabase(Configuration);
            services.AddEmailService(options =>
            {
                options.Address = Configuration["EmailSettings:Address"]!;
                options.Name = Configuration["EmailSettings:Name"]!;
                options.Password = Configuration["EmailSettings:Password"]!;
                options.Login = Configuration["EmailSettings:Login"]!;
                options.SmtpServer = Configuration["EmailSettings:SmtpServer"]!;
                options.Port = int.Parse(Configuration["EmailSettings:Port"]!);
                options.UseSSL = bool.Parse(Configuration["EmailSettings:UseSSL"]!);
            });

            services.AddApplication();
            services.AddControllers().AddNewtonsoftJson(o =>
            {
                o.SerializerSettings.Converters.Add(new StringEnumConverter
                {
                    NamingStrategy = new CamelCaseNamingStrategy(true, true),
                    //  CamelCaseText = true
                });
            });

            services.AddSwaggerGenNewtonsoftSupport();
            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Name = "Auth",
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Description = "Insert token"
                });
                c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
                {
                    {
                        new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                        {
                            Reference = new Microsoft.OpenApi.Models.OpenApiReference
                            {
                                Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },

                        Array.Empty<string>()
                    }
                });

                List<string> xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.xml", SearchOption.TopDirectoryOnly).ToList();

                foreach (string xmlFile in xmlFiles)
                {
                    c.IncludeXmlComments(xmlFile, includeControllerXmlComments: true);
                    c.SchemaFilter<EnumTypesSchemaFilter>(xmlFile);
                }

                c.DocumentFilter<EnumTypesDocumentFilter>();

            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudience = Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]!))
                    };
                });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Example.WebAPI v1");
                c.DocumentTitle = "Example.WebAPI v1";
                c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
            });

            app.UseCustomExceptionHandler();
            //app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
