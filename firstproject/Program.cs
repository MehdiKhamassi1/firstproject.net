using Serilog;
using firstproject.Data;
using Microsoft.EntityFrameworkCore;
using firstproject;
using firstproject.Repository.IRepository;
using firstproject.Repository;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
/*Log.Logger=new LoggerConfiguration().MinimumLevel.Debug()
    .WriteTo.File("log/Villalogs.txt",rollingInterval:RollingInterval.Day).CreateLogger();
builder.Host.UseSerilog();*/
builder.Services.AddDbContext<ApplicationDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLConnection"));
});
builder.Services.AddScoped<IVillaRepository,VillaRepository>();
builder.Services.AddScoped<IVillaNumberRepository, VillaNumberRepository>();
//builder.Services.AddAutoMapper(typeof(MappingConfig));
builder.Services.AddControllers(
    //option => option.ReturnHttpNotAcceptable=true
    ).AddNewtonsoftJson().AddXmlDataContractSerializerFormatters();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
