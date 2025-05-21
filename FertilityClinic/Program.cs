using FertilityClinic.BLL.Services.Implementations;
using FertilityClinic.BLL.Services.Interfaces;
using FertilityClinic.DAL;
using FertilityClinic.DAL.Repositories.Implementations;
using FertilityClinic.DAL.Repositories.Interfaces;
using FertilityClinic.DAL.UnitOfWork;
using FertilityClinic.DTO.Config;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Đăng ký DbContext
builder.Services.AddDbContext<FertilityClinicDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions => sqlOptions.EnableRetryOnFailure());
});

// Đăng ký các repository
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Đăng ký UnitOfWork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Đăng ký các service
builder.Services.AddScoped<IAuthService, AuthService>();

// Cấu hình JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
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
});

// Cấu hình JwtSettings
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtS"));

// Thêm các controller
builder.Services.AddControllers();

// Cấu hình Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("Auth", new OpenApiInfo { Title = "Auth APIs", Version = "v1" });
    opt.SwaggerDoc("Users", new OpenApiInfo { Title = "User APIs", Version = "v1" });
    opt.SwaggerDoc("Debug", new OpenApiInfo { Title = "Debug APIs", Version = "v1" });

    // XML comments
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        opt.IncludeXmlComments(xmlPath);
    }

    // JWT Bearer trong Swagger
    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Nhập JWT Bearer token **_only_**",

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

app.UseAuthentication(); // Phải gọi trước UseAuthorization
app.UseAuthorization();

app.MapControllers();

app.Run();
