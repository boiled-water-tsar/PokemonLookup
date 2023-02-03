using PokeLookup;

var builder = WebApplication.CreateBuilder(args);

// Inject dependencies.
builder.Services.AddScoped<IPokemonService, PokemonService>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();

// Below line is for testing usage only with xunit and minimal api
public partial class Program { }
