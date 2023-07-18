using Microsoft.AspNetCore.Mvc;
using System.Net;
using MISA.WebFresher052023.CTM.Infrastructure;
using MISA.WebFresher052023.CTM.Domain;
using MISA.WebFresher052023.CTM.Application;
using MISA.WebFresher052023.CTM;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(p => p.AddPolicy("MyCors", build =>
{
    build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader().WithExposedHeaders("Content-Disposition"); ;
}));

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = actionContext =>
        {
            var modelState = actionContext.ModelState;
            var errors = new Dictionary<string, string>();

            foreach (var entry in modelState)
            {
                var key = entry.Key;
                var valueErrors = entry.Value.Errors.Select(error => error.ErrorMessage);
                var errorMessage = string.Join(", ", valueErrors);

                errors[key] = errorMessage;
            }

            return new BadRequestObjectResult(new BaseException
            {
                ErrorCode = (int)HttpStatusCode.BadRequest,
                DevMessage = ResourceVN.Validate_User_Input_Error,
                UserMessage = ResourceVN.Validate_User_Input_Error,
                TraceId = "",
                MoreInfo = "",
                Data = errors,
            });
        };
    })
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60);
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IExcelService<Employee, EmployeeDto, EmployeeCreateDto>, EmployeeExcelService>();

builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();

builder.Services.AddScoped<IEmployeeValidate, EmployeeValidate>();
builder.Services.AddScoped<IDepartmentValidate, DepartmentValidate>();

builder.Services.AddScoped<IExcelWorker<EmployeeDto, EmployeeCreateDto>, EmployeeExcelWorker>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseSession();

app.UseCors("MyCors");

app.MapControllers();

app.UseMiddleware<ExceptionMiddleware>();

app.Run();
