
using TrailerPark.Intrastructure.Config;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.InjectCustom();

WebApplication app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();
