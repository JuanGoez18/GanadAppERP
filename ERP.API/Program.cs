var builder = WebApplication.CreateBuilder(args);

// Controllers (para que se habiliten tus controladores)
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Habilitar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazor",
        policy =>
        {
            policy.WithOrigins("http://localhost:5064")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

// Activar CORS
app.UseCors("AllowBlazor");

app.UseHttpsRedirection();

app.UseAuthorization();




if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



// Mapea controladores (¡clave!)
app.MapControllers();

app.Run();