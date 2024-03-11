using AutoMapper;
using Events.Api.BusinessLogic.Classes;
using Events.Api.BusinessLogic.Interfaces;
using Events.Api.Data;
using Events.Api.Data.Classes;
using Events.Api.Data.Interfaces;
using Events.Api.Entites.Profiles;
using Events.Api.Extensions;
using Events.Api.Filters.Swagger;
using Events.Api.Filters.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Security.Claims;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows
        {
            AuthorizationCode = new OpenApiOAuthFlow
            {
                AuthorizationUrl = new Uri("https://localhost:5001/connect/authorize"),
                TokenUrl = new Uri("https://localhost:5001/connect/token"),
                Scopes = new Dictionary<string, string>
                {
                    {"web2ApiScope", "web2Api"} //Demo API - scope 
                }
            }
        }
    });
    options.OperationFilter<AuthorizeCheckOperationFilter>();
    options.SwaggerDoc("v1", new OpenApiInfo
    {

        Version = "v1",
        Title = "API Web 2",
        Description = "API pour la gestion des covoiturages",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Luca Cavalera et Frederic Lavardiere",
            Email = "helloWorld@gmail.com",
            Url = new Uri("https://google.com/")
        },
        License = new OpenApiLicense
        {
            Name = "Apache 2.0",
            Url = new Uri("http://www.apache.org")
        }
    });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.EnableAnnotations();
    options.SchemaFilter<SwaggerSkipPropertyFilter>();
    options.IncludeXmlComments(xmlPath);
    
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<EventsContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdmin", policy => policy.RequireAssertion(context => context.User.IsInRole("admin")));
    options.AddPolicy("RequireManager", policy => policy.RequireRole("manager"));
    options.AddPolicy("RequireScope", policy => policy.RequireAssertion(co => co.User.HasClaim(cl => cl.Type.Equals("scope") && cl.Value.Equals("web2ApiScope"))));

    options.DefaultPolicy = options.GetPolicy("RequireScope");
});

//auth
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "Bearer";

}).AddJwtBearer("Bearer", options =>
{
    options.Authority = "https://localhost:5001";
    options.Audience = "Web2Api";
    options.TokenValidationParameters.ValidTypes = new[] { "at+jwt" };
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = true
    };
});

builder.Services.AddAutoMapper(c => c.AddProfile<MappingProfile>());

builder.Services.AddScoped(typeof(IAsyncRepository<>), typeof(AsyncRepository<>));
builder.Services.AddScoped<IAsyncRepositoryEvenements, AsyncRepositoryEvenements>();
builder.Services.AddScoped<IAsyncParticipationRepository, ParticipationAsyncRepository>();
builder.Services.AddScoped<IAsyncRepositoryVilles, AsyncRepositoryVilles>();

builder.Services.AddScoped<IVillesBL, VillesBL>();
builder.Services.AddScoped<ICategorieBL, CategorieBL>();
builder.Services.AddScoped<IEvenementsBL, EvenementsBL>();
builder.Services.AddScoped<IParticipationBL, ParticipationBL>();
builder.Services.AddScoped<IStatistiquesBL, StatistiquesBL>();


builder.Services.AddControllers(options =>
{
    options.AllowEmptyInputInBodyModelBinding = true;
    options.Filters.Add<ExceptionsFilters>();
})
    .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true).
    AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.CreateDbIfNotExists();

app.Run();

