using api.Services.Numeration;
using api.Services.Validation;
using api.Services.Validation.NumberToWordsValidation;
using api.Utils.EnvLoader;

EnvLoader.Load(".env");
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped<IInputValidator, NumberToWordsValidator>();
builder.Services.AddScoped<INumberToWordsService, NumberToWordsService>();

string frontendUrl = Environment.GetEnvironmentVariable("CLIENT_URL") ?? "http://localhost:5173";


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactClient", policy => policy.WithOrigins(frontendUrl)
            .AllowAnyMethod()
            .AllowAnyHeader());
});

var app = builder.Build();


app.UseHttpsRedirection();
app.UseCors("AllowReactClient");
app.MapControllers();
app.Run();

