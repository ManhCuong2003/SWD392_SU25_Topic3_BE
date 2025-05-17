using FertilityClinic.DAL;
using FertilityClinic.DAL.UnitOfWork;
using FertilityClinic.DTO.Config;
using MetroOne.Api.Middlewares;
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
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
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

    //options.Events = new JwtBearerEvents
    //   {
    //       OnChallenge = context =>
    //       {
    //           // Skip the default behavior
    //           context.HandleResponse();
    //           context.Response.StatusCode = 401;
    //           context.Response.ContentType = "application/json";
    //           return context.Response.WriteAsync("{\"message\": \"You must be logged in to access this resource.\"}");
    //       },
    //       OnForbidden = context =>
    //       {
    //           context.Response.StatusCode = 403;
    //           context.Response.ContentType = "application/json";
    //           return context.Response.WriteAsync("{\"message\": \"You do not have permission to access this resource.\"}");
    //       }
    //   };
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

#region CORS
//Unit of Work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Add services to the container.
builder.Services.AddControllers();
#endregion

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Register the DbContext with the connection string
builder.Services.AddDbContext<FertilityClinicDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/Auth/swagger.json", "Auth API");
        options.SwaggerEndpoint("/swagger/Users/swagger.json", "User APIs");
        options.SwaggerEndpoint("/swagger/Debug/swagger.json", "Debug APIs");
    });
}

app.UseHttpsRedirection();

app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseAuthentication(); // JWT must come before UseAuthorization

app.UseAuthorization();

app.MapControllers();

app.Run();
