using System.IdentityModel.Tokens.Jwt;
using Netfirebase.Api;
using Netfirebase.Api.Authentication;
using Netfirebase.Api.Services.Permissions;
using FirebaseAdmin;
using Netfirebase.Api.Extensions;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Netfirebase.Api.Data;
using Netfirebase.Api.Services.Authentication;
using Netfirebase.Api.Services.Products;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var connectionString = builder.Configuration
    .GetConnectionString("ConnectionString") 
    ?? throw new ArgumentNullException("Connection string not found");

builder.Services.AddDbContext<DatabaseContext>(options => {
    options.UseNpgsql(connectionString);
});


builder.Services.AddSignalR();

builder.Services.AddHostedService<ServerNotifier>();



FirebaseApp.Create(new AppOptions
{
    Credential = GoogleCredential.FromFile("firebase.json")
});

//builder.Services.AddSingleton<IAuthenticationService, AuthenticationService>();
builder.Services.AddHttpClient<IAuthenticationService, AuthenticationService>((sp, httpClient) =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    httpClient.BaseAddress = new Uri(configuration["Authentication:TokenUri"]!);
});

builder.Services
    .AddAuthentication()
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, jwtOptions => {
        jwtOptions.Authority = builder.Configuration["Authentication:ValidIssuer"];
        jwtOptions.Audience = builder.Configuration["Authentication:Audience"];
        jwtOptions.TokenValidationParameters.ValidIssuer = builder.Configuration["Authentication:ValidIssuer"];
});

builder.Services.AddAuthorization();
builder.Services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
builder.Services.AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizacionPolicyProvider>();
builder.Services.AddScoped<IPermissionService, PermissionService>();

//builder.Services.AddDbContext<DatabaseContext>(opt =>
//{
//    opt.LogTo(Console.WriteLine, new[] {
//        DbLoggerCategory.Database.Command.Name
//    },
//    LogLevel.Information
//    ).EnableSensitiveDataLogging();

//    opt.UseSqlite(builder.Configuration.GetConnectionString("SqliteDatabase"));
//});

builder.Services.AddScoped<IProductService, ProductService>();




builder.Services.AddCors(options => {
    options.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
    
    );

});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");
app.MapControllers();

app.UseAuthentication();
app.UseAuthorization();

app.AddTestData();

app.MapHub<NotificationHub>("notifications");

app.Run();
