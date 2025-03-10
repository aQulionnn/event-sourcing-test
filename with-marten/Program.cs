using Marten;
using Marten.Events.Projections;
using Weasel.Core;
using with_marten;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMarten(options =>
{
    options.Connection("Host=localhost;Port=5432;Database=MartenTest;Username=postgres;Password=31415;");
    options.UseSystemTextJsonForSerialization();
    options.Projections.Add<OrderProjection>(ProjectionLifecycle.Inline);
    
    if (builder.Environment.IsDevelopment())
        options.AutoCreateSchemaObjects = AutoCreate.All;
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();