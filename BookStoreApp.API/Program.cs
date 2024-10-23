using BookStoreApp.API.Configurations;
using BookStoreApp.API.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;
using Microsoft.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);

// Configure services and add them to the IoC container.

// Database Configuration - SQL Server
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<BookStoreDbContext>(options =>
    options.UseSqlServer(connectionString));

//                          Identity Core
// Add IdentityCore services for user authentication and authorization
// `ApiUser` as the user type and `IdentityRole` for roles.
//
// Store Identity-related data in the SQL Server database via Entity Framework.
builder.Services.AddIdentityCore<ApiUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<BookStoreDbContext>();

// AutoMapper Configuration
// Register AutoMapper with the DI container, passing the `MapperConfig` class for mapping configurations.
builder.Services.AddAutoMapper(typeof(MapperConfig));

// Add Controller Services
// Register the controllers with the DI container so that the API endpoints can be used to handle HTTP requests.
builder.Services.AddControllers();

// Swagger/OpenAPI Configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//                   Logging - Serilog Configuration
// Set up Serilog for logging to the console. The logger configuration is read from the app's configuration file (e.g., appsettings.json).
// This allows for structured logging throughout the application.
builder.Host.UseSerilog((context, loggerConfig) =>
    loggerConfig.WriteTo.Console()
    .ReadFrom.Configuration(context.Configuration));

//                          CORS Policy Setup
// Configure Cross-Origin Resource Sharing (CORS) to allow requests from any origin, method, or header.
// The "AllowAll" policy ensures that the API is accessible from any client.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", b =>
        b.AllowAnyMethod()
         .AllowAnyHeader()
         .AllowAnyOrigin());
});

// Authentication Configuration (JWT & Google OAuth)
// Configure the application to use JWT (JSON Web Token) for authentication.
// The options include validation of the token's signing key, issuer, audience, and expiration time.
builder.Services.AddAuthentication(options =>
{
    // Set JWT Bearer authentication as the default authentication and challenge scheme.
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,  // Ensure that the token's signature is valid.
        ValidateIssuer = true,  // Validate the token's issuer.
        ValidateAudience = true,  // Validate the audience to ensure it's the intended recipient.
        ValidateLifetime = true,  // Validate the token's expiration time.
        ClockSkew = TimeSpan.Zero,  // Disable the default time drift buffer to ensure strict expiration checking.
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],  // Get the valid issuer from configuration.
        ValidAudience = builder.Configuration["JwtSettings:Audience"],  // Get the valid audience from configuration.
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]))  // Use a symmetric key for signing.
    };
});

// Build the application (finalize the service configurations and prepare to handle HTTP requests).
var app = builder.Build();

// Configure the HTTP request pipeline based on the environment.

// Enable Swagger in Development Mode
if (app.Environment.IsDevelopment())
{

}

app.UseSwagger();
app.UseSwaggerUI();

// HTTPS Redirection
// Enforce the use of HTTPS for all incoming requests.
app.UseHttpsRedirection();

// Static Files
// Serve static files like images, CSS, or JavaScript from the wwwroot folder.
app.UseStaticFiles();

// CORS Middleware
// Apply the configured "AllowAll" CORS policy to the application so that it handles cross-origin requests.
app.UseCors("AllowAll");

// Authentication Middleware
// Enable authentication so that the app can recognize and validate incoming user credentials (e.g., JWT tokens).
app.UseAuthentication();

// Authorization Middleware
// Ensure that authorization policies are applied based on the user's identity and roles.
app.UseAuthorization();

// Map Controllers to Endpoints
// Map the routes of the controllers so that incoming requests are directed to the appropriate controller actions.
app.MapControllers();

// Run the application and start listening for incoming HTTP requests.
app.Run();
