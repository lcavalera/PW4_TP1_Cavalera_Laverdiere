using AutoMapper;
using Events.Api.BusinessLogic;
using Events.Api.Data;
using Events.Api.Entites;
using Events.Api.Extensions;
using Events.Api.Filters.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
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
    options.IncludeXmlComments(xmlPath);
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<EventsContext>(options => options.UseNpgsql(connectionString));


builder.Services.AddAutoMapper(c => c.AddProfile<MappingProfile>());

builder.Services.AddScoped(typeof(IAsyncRepository<>), typeof(AsyncRepository<>));
builder.Services.AddScoped(typeof(AsyncRepositoryEvenements<Evenement>), typeof(AsyncRepositoryEvenements<Evenement>));
builder.Services.AddScoped<IAsyncParticipationRepository, ParticipationAsyncRepository>();

builder.Services.AddScoped<IVillesBL, VillesBL>();
builder.Services.AddScoped<ICategorieBL, CategorieBL>();
builder.Services.AddScoped<IEvenementsBL, EvenementsBL>();
builder.Services.AddScoped<IParticipationBL, ParticipationBL>();


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

app.MapControllers();

app.CreateDbIfNotExists();

app.Run();

