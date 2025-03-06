using app.Data;
using app.Services;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.Configure<MongoDbContext>(builder.Configuration);
builder.Services.AddSingleton<MongoDbContext>();
builder.Services.AddScoped<GameData>();
builder.Services.AddScoped<LeaderboardData>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
