using FertilityClinic.BLL.Services.Implementations;
using FertilityClinic.BLL.Services.Interfaces;
using FertilityClinic.DAL;
using FertilityClinic.DAL.Repositories;
using FertilityClinic.DAL.Repositories.Implementations;
using FertilityClinic.DAL.Repositories.Interfaces;
using FertilityClinic.DAL.UnitOfWork;
using FertilityClinic.DTO.Config;
using FertilityClinic.Middlewares;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

#region JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
    .AddCookie()
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    })

.AddGoogle(options =>
{
    options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
    options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
});
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));

builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("Users", new OpenApiInfo { Title = "User APIs", Version = "v1" });
    opt.SwaggerDoc("Auth", new OpenApiInfo { Title = "User APIs", Version = "v1" });
    opt.SwaggerDoc("Debug", new OpenApiInfo { Title = "Debug APIs", Version = "v1" });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    opt.IncludeXmlComments(xmlPath);

    // Add JWT bearer to Swagger
    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter JWT Bearer token **_only_**",

        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
        }
    };

    opt.AddSecurityDefinition("Bearer", securityScheme);

    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { securityScheme, new string[] { } }
    });

});

builder.Services.AddAuthorization();

#endregion

builder.Services.AddHttpClient();

#region CORS

// Đăng ký UnitOfWork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddControllers();

// Đăng ký các repository
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
builder.Services.AddScoped<IAppoimentRepository, AppoimentRepository>();
builder.Services.AddScoped<IAppoimentHistoryRepository, AppoimentHistoryRepository>();
builder.Services.AddScoped<IPartnerRepository, PartnerRepository>();
builder.Services.AddScoped<ITreatmentMethodRepository, TreatmentMethodRepository>();
builder.Services.AddScoped<ITreatmentProcessRepository, TreatmentProcessRepository>();
//builder.Services.AddScoped<ILabTestScheduleRepository, LabTestScheduleRepository>();
builder.Services.AddScoped<ILabTestResultRepository, LabTestResultRepository>();
builder.Services.AddScoped<IInseminationScheduleRepository, InseminationScheduleRepository>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IInseminationResultRepository, InseminationResultRepository>();
builder.Services.AddScoped<IPillRepository, PillRepository>();
builder.Services.AddScoped<IPrescriptionRepository, PrescriptionRepository>();
// Đăng ký các service
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IDoctorService, DoctorService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAppoimentService, AppoimentService>();
builder.Services.AddScoped<IAppoimentHistoryService, AppoimentHistoryService>();
builder.Services.AddScoped<IPartnerService, PartnerService>();
builder.Services.AddScoped<ITreatmentMethodService, TreatmentMethodServicecs>();
builder.Services.AddScoped<ITreatmentProcessService, TreatmentProcessService>();
//builder.Services.AddScoped<ILabTestScheduleService, LabTestScheduleService>();
builder.Services.AddScoped<ILabTestResultService, LabTestResultService>();
builder.Services.AddScoped<IInseminationScheduleService, InseminationScheduleService>();
builder.Services.AddScoped<IInseminationResultService, InseminationResultService>();
builder.Services.AddScoped<IPillService, PillService>();
builder.Services.AddScoped<IPrescriptionService, PrescriptionService>();
// Add these lines in your Program.cs service configuration
builder.Services.Configure<PayOSSettings>(builder.Configuration.GetSection("PayOS"));
builder.Services.AddScoped<IPaymentService, PaymentService>();
#endregion
// Frontend Connection
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173") // React frontend
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Register the DbContext with the connection string
builder.Services.AddDbContext<FertilityClinicDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenLocalhost(5194); // HTTP
    options.ListenLocalhost(7194, listenOptions =>
    {
        listenOptions.UseHttps(); // HTTPS
    });
});

var app = builder.Build();

// Cấu hình middleware pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(opt =>
    {
        opt.SwaggerEndpoint("/swagger/Auth/swagger.json", "Auth APIs");
        opt.SwaggerEndpoint("/swagger/Users/swagger.json", "User APIs");
        opt.SwaggerEndpoint("/swagger/Debug/swagger.json", "Debug APIs");
    });
}

app.UseHttpsRedirection();
app.UseCors("AllowFrontend");

app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseAuthentication(); // JWT must come before UseAuthorization

app.UseAuthorization();

app.MapControllers();

app.Run();