using api.Services.Numeration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddControllers();
builder.Services.AddScoped<INumberToWordsService, NumberToWordsService>();
builder.Services.AddOpenApi();
builder.Services.AddCors(options =>
{
  options.AddPolicy("AllowReactClient", policy => policy.WithOrigins("https://localhost:5173").AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseCors("AllowReactClient");
app.MapControllers();
app.Run();

