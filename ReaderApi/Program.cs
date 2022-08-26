using CommonDbLayer.DatabaseEntity;
using Microsoft.EntityFrameworkCore;
using ReaderApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddTransient<IPaymentService, PaymentService>();
builder.Services.AddTransient<IReaderService, ReaderService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors((setup) =>
{
    setup.AddPolicy("default", (options) =>
    {
        options.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
    });
});
var ConnectionStrings = builder.Configuration.GetConnectionString("DefaultConnectionString");
builder.Services.AddDbContext<MyDigitalBookDbContext>(options => options.UseSqlServer(ConnectionStrings));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("default");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
