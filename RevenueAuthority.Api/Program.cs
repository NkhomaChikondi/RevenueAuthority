using Newtonsoft.Json;
using RevenueAuthority.Core;
using RevenueAuthority.Core.Repositories;
using RevenueAuthority.Core.Services;
using RevenueAuthority.Data;
using RevenueAuthority.Data.Repositories;
using RevenueAuthority.Services;

    var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(
            options => options.AllowEmptyInputInBodyModelBinding = true
        )
        .AddNewtonsoftJson(options => 
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
        );  
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<RevenueAuthorityDbContext>();

//Registration of services and repositories using dependency injection
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
    
builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();

    
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