using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Warehouse.DataAccess;
using Warehouse.DataAccess.UOW;
using Warehouse.DataAccess.Utils;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(policy =>
{
    policy.AddPolicy("CorsPolicy", opt => opt
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod());
});

// https://github.com/dotnet/efcore/issues/15218
builder.Services.AddDbContext<WarehouseDbContext>(options =>
{
    options
        .UseLazyLoadingProxies()
        .UseSqlite(builder.Configuration.GetConnectionString("SqLite"));
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>(
    // options =>
// {
    // var dbContext = options.GetRequiredService<WarehouseDbContext>();
    //
    // dbContext.Database.EnsureDeletedAsync().GetAwaiter().GetResult();
    // dbContext.Database.EnsureCreatedAsync().GetAwaiter().GetResult();
    //
    // var unitOfWork = new UnitOfWork(dbContext);
    //
    // unitOfWork.CreateAndFillWarehouseDatabaseWithUowAsync().GetAwaiter().GetResult();
    //
    // return unitOfWork;
// }
    );

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

//Fill database with test data
using var dbContext = new WarehouseDbContext();
dbContext?.CreateAndFillWarehouseDatabase();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors("CorsPolicy");
// app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().AllowCredentials());

app.Run();


