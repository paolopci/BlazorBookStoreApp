using AutoMapper;
using BookStoreApp.API.Configurations;
using BookStoreApp.API.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// registra tutti i profili AutoMapper presenti nell'assembly
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MapperConfig>());

builder.Host.UseSerilog((ctx, lc) =>
{
    lc.WriteTo.Console();
    lc.ReadFrom.Configuration(ctx.Configuration);
});

builder.Services.AddDataProtection();
// configuro Identity
builder.Services
    .AddIdentityCore<ApiUser>(options =>
    {
        options.User.RequireUniqueEmail = true;
        options.Password.RequiredLength = 6;
        // aggiungi qui le altre policy che ti servono
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<BookStoreDbContext>()
    .AddDefaultTokenProviders();

// configuro CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll ", b =>
        b.AllowAnyMethod()
            .AllowAnyHeader()
            .AllowAnyOrigin());
});

var connectionString = builder.Configuration.GetConnectionString("BookStoreAppDbConnection");
builder.Services.AddDbContext<BookStoreDbContext>(options => options.UseSqlServer(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// uso Cors
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
