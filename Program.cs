using LoopApi.Models;
using LoopApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        RequireExpirationTime = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),        
    };
});

// Add services to the container.
builder.Services.Configure<UserDatabaseSettings>(builder.Configuration.GetSection("MongoDB"));
builder.Services.AddSingleton<UserService>();

builder.Services.Configure<TicketDatabaseSettings>(builder.Configuration.GetSection("MongoDB"));
builder.Services.AddSingleton<TicketService>();

builder.Services.Configure<StoreCategoriesDatabaseSettings>(builder.Configuration.GetSection("MongoDB"));
builder.Services.AddSingleton<StoreCategorieService>();

builder.Services.Configure<AuthorityCategoriesDatabaseSettings>(builder.Configuration.GetSection("MongoDB"));
builder.Services.AddSingleton<AuthorityCategoriesService>();

builder.Services.Configure<CodeDatabaseSettings>(builder.Configuration.GetSection("MongoDB"));
builder.Services.AddSingleton<CodeService>();

builder.Services.Configure<CouponDatabaseSettings>(builder.Configuration.GetSection("MongoDB"));
builder.Services.AddSingleton<CouponService>();

builder.Services.Configure<NotificationDatabaseSettings>(builder.Configuration.GetSection("MongoDB"));
builder.Services.AddSingleton<NotificationService>();

builder.Services.Configure<FrequentlyQuestionDatabaseSettings>(builder.Configuration.GetSection("MongoDB"));
builder.Services.AddSingleton<FrequentlyQuestionService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsProduction()) //isDevelopment o isProduction
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.UseAuthorization();

app.MapControllers();

app.Run();
