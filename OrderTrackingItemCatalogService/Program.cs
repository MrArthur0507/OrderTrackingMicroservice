using MassTransit;
using Microsoft.EntityFrameworkCore;
using OrderTrackingItemCatalogService.Data;
using OrderTrackingItemCatalogService.Services.Implementations;
using OrderTrackingItemCatalogService.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ItemCatalogContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("CatalogConnection"));
});
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ConfigureEndpoints(context);
    });
});

builder.Services.AddMassTransitHostedService();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
