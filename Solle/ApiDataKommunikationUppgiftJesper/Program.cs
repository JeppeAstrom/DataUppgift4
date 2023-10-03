using ApiDataKommunkationUppgift;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSignalR();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add CORS services.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder
            .WithOrigins("https://localhost:7248/") // Replace with your actual client origin
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials(); // Allow credentials (cookies, authorization headers, etc.)
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

// Use CORS policy after routing and before endpoints
app.UseCors("AllowAll");  // This should match the policy name you've defined above

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<TemperatureHub>("/temperatureHub");
    endpoints.MapControllers();
});

app.Run();
