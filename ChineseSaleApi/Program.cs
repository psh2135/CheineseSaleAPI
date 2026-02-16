//using AutoMapper;
//using ChineseSaleApi.Data;
//using ChineseSaleApi.Mapping;
//using ChineseSaleApi.Repositories;
//using ChineseSaleApi.Services;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.IdentityModel.Tokens;
//using System.Text;
//using System.Security.Claims;
//using Microsoft.OpenApi.Models;


//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.

//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen(c =>
//{
//    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ChineseSaleApi", Version = "v1" });

//    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
//    {
//        Description = @"JWT Authorization header using the Bearer scheme. 
//                      Enter 'Bearer' [space] and then your token in the text input below.
//                      Example: 'Bearer 12345abcdef'",
//        Name = "Authorization",
//        In = ParameterLocation.Header,
//        Type = SecuritySchemeType.ApiKey,
//        Scheme = "Bearer"
//    });

//    c.AddSecurityRequirement(new OpenApiSecurityRequirement
//    {
//        {
//            new OpenApiSecurityScheme
//            {
//                Reference = new OpenApiReference
//                {
//                    Type = ReferenceType.SecurityScheme,
//                    Id = "Bearer"
//                }
//            },
//            new string[] {}
//        }
//    });
//});
//builder.Services.AddDbContext<AppDbContext>(options =>
//    options.UseSqlServer(
//        builder.Configuration.GetConnectionString("DefaultConnection")));
//builder.Services.AddAutoMapper(typeof(MappingProfile));
//builder.Services.AddCors(options => {
//    options.AddPolicy("AllowAngular", policy => {
//        policy.WithOrigins("http://localhost:4200") // הכתובת של אנגולר
//              .AllowAnyMethod()
//              .AllowAnyHeader();
//    });
//});
//builder.Services.AddScoped<IUserService, UserService>();
//builder.Services.AddScoped<IUserRepository, UserRepository>();

//builder.Services.AddScoped<ILotteryRepository, LotteryRepository>();
//builder.Services.AddScoped<ILotteryService, LotteryService>();

//// Gift
//builder.Services.AddScoped<IGiftRepository, GiftRepository>();
//builder.Services.AddScoped<IGiftService, GiftService>();

//// Purchase
//builder.Services.AddScoped<IPurchaseRepository, PurchaseRepository>();
//builder.Services.AddScoped<IPurchaseService, PurchaseService>();

//// Ticket
//builder.Services.AddScoped<ITicketRepository, TicketRepository>();
//builder.Services.AddScoped<ITicketService, TicketService>();

//builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
//builder.Services.AddScoped<ICategoryService, CategoryService>();

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(options =>
//    {
//        options.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateIssuer = true,
//            ValidateAudience = true,
//            ValidateLifetime = true,
//            ValidateIssuerSigningKey = true,
//            ValidIssuer = "ChineseSaleApi",
//            ValidAudience = "ChineseSaleApiUsers",
//            IssuerSigningKey = new SymmetricSecurityKey(
//                Encoding.UTF8.GetBytes("SUPER_SECRET_KEY_123456_SUPER_SECRET_KEY_123456")),
//            RoleClaimType = ClaimTypes.Role
//        };
//    });
//builder.Services.AddAuthorization();

//var app = builder.Build();


//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();
//app.UseCors("AllowAngular");

//app.UseAuthentication();
//app.UseAuthorization();

//app.MapControllers();

//app.Run();
using ChineseSaleApi.Data;
using ChineseSaleApi.Mapping;
using ChineseSaleApi.Repositories;
using ChineseSaleApi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using Microsoft.OpenApi.Models;
using Serilog;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ChineseSaleApi", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      Example: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddCors(options => {
    options.AddPolicy("AllowAngular", policy => {
        policy.WithOrigins("http://localhost:4200") // הכתובת של אנגולר
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
builder.Services.AddScoped<IRaffleStateService, RaffleStateService>();
builder.Services.AddScoped<RaffleRepository>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<ILotteryRepository, LotteryRepository>();
builder.Services.AddScoped<ILotteryService, LotteryService>();

// Gift
builder.Services.AddScoped<IGiftRepository, GiftRepository>();
builder.Services.AddScoped<IGiftService, GiftService>();

// Purchase
builder.Services.AddScoped<IPurchaseRepository, PurchaseRepository>();
builder.Services.AddScoped<IPurchaseService, PurchaseService>();

// Ticket
builder.Services.AddScoped<ITicketRepository, TicketRepository>();
builder.Services.AddScoped<ITicketService, TicketService>();

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "ChineseSaleApi",
            ValidAudience = "ChineseSaleApiUsers",
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("SUPER_SECRET_KEY_123456_SUPER_SECRET_KEY_123456")),
            RoleClaimType = ClaimTypes.Role
        };
    });
builder.Services.AddAuthorization();
// ----------------------------------------
// Serilog configuration
// ----------------------------------------
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information() // כל הלוגים מ-Information ומעלה
    .WriteTo.Console() // גם לקונסולה
    .WriteTo.File(
        path: "logs/app.log", // הקובץ בתיקייה logs
        rollingInterval: RollingInterval.Day, // קובץ חדש כל יום
        retainedFileCountLimit: 14, // שמירת 14 קבצים אחרונים
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}{Exception}"
    )
    .CreateLogger();

// החלפת Logging של ASP.NET Core ל-Serilog
//builder.Host.UseSerilog();


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAngular");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseExceptionHandler("/error");

app.Run();
