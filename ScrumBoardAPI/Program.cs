using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ScrumBoardAPI.Configuration;
using ScrumBoardAPI.Contracts;
using ScrumBoardAPI.Data;
using ScrumBoardAPI.Repository;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connString = builder.Configuration.GetConnectionString("ScrumBoardAPIDbServerConnectionString");

builder.Services.AddDbContext<ScrumBoardDbContext>(options => {
    options.UseNpgsql(connString);
});

builder.Services.AddIdentityCore<AUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ScrumBoardDbContext>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// https://gavilan.blog/2021/05/19/fixing-the-error-a-possible-object-cycle-was-detected-in-different-versions-of-asp-net-core/
builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddCors(options => {
    options.AddPolicy("AllowAll",
    b =>
        b.AllowAnyHeader()
         .AllowAnyOrigin()
         .AllowAnyMethod());
});

builder.Host.UseSerilog((hostingContext, loggerConfiguration) => {
    loggerConfiguration.WriteTo.Console().ReadFrom.Configuration(hostingContext.Configuration);
});

builder.Services.AddAutoMapper(typeof(MapperConfig));

builder.Services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
builder.Services.AddScoped(typeof(IUserRepostory), typeof(UserRepository));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseSerilogRequestLogging();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
