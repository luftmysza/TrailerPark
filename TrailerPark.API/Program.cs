using TrailerPark.Intrastructure.Config;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Custom
builder.InjectInfrastructure();

WebApplication app = builder.Build();

if (!app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Custom
await app.InjectSeedAsync();

app.UseHttpsRedirection();
app.MapControllers();
app.Run();