using AutoMapper;
using Demo.Infrastruture.CrossCutting.IOC.AutoMapper;
using Demo.Infrastruture.CrossCutting.IOC.DependenceInjection;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region AutoMapper

var autoMapperConfig = new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new MappingProfile());
});

builder.Services.AddSingleton(autoMapperConfig.CreateMapper());

#endregion

#region DI
ConfigureDbContext.ConfigureDependenceDbContext(builder.Services, builder.Configuration);
ConfigureService.ConfigureDependenceService(builder.Services);
ConfigureRepository.ConfigureDependenceRepository(builder.Services);
#endregion

#region Swagger

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "User Api",
        Version = "v1",
        Description = "User Api",
        Contact = new OpenApiContact
        {
            Name = "Guilherme Figueiras Maurila",
            Email = "gfmaurila@gmail.com"
        },
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Por favor utilize Bearer <TOKEN>",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
                });
});

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "User Api v1"));

    app.UseHttpsRedirection();

    app.UseRouting();

    app.UseAuthentication();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });
}

app.UseAuthorization();

app.MapControllers();

app.Run();