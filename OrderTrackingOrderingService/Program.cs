using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.IdentityModel.Tokens;
using OrderTrackingOrderingService.Consumers;
using OrderTrackingOrderingService.DataAccess.Context;
using OrderTrackingOrderingService.Services.Contracts;
using OrderTrackingOrderingService.Services.Implementation;

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
        cfg.Host("rabbitmq", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
        cfg.ConfigureEndpoints(context);
    }
    );
});
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = "http://identityserver:5000";  
        options.RequireHttpsMetadata = false;        
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,                     
            ValidateLifetime = true,
            ValidateAudience = false,
            NameClaimType = "username",
    };
    });
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderVerificationService, OrderVerificationService>();
var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<OrderingDbContext>();
    db.Database.Migrate();
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
