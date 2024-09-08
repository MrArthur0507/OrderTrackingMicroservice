using MassTransit;
using Microsoft.EntityFrameworkCore;
using OrderTrackingOrderingService.Consumers;
using OrderTrackingOrderingService.DataAccess.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<OrderingDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("OrderingConnection"));
});
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<ItemCreatedConsumer>();
    x.AddConsumer<ItemUpdatedConsumer>();
    x.AddConsumer<ItemDeletedConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.ConfigureEndpoints(context);
    }
    );
});
builder.Services.AddAutoMapper(typeof(Program));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
