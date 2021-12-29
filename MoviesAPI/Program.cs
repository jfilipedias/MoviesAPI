using MoviesAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MoviesAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using MoviesAPI.Authorization;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("default");
builder.Services.AddDbContext<AppDbContext>(options => 
    options
    .UseLazyLoadingProxies()
    .UseNpgsql(connectionString, options => options.EnableRetryOnFailure())
);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes("0asdjas09djsa09djdsadjsadjsadjsd09asjd09sajcnzxn")),
        ValidateIssuer = false,
        ValidateActor = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("MinimumAge", policy =>
    {
        policy.Requirements.Add(new MinimumAgeRequirement(18));
    });
});

builder.Services.AddSingleton<IAuthorizationHandler, MinimumAgeHandler>();
builder.Services.AddScoped<AddressService, AddressService>();
builder.Services.AddScoped<ManagerService, ManagerService>();
builder.Services.AddScoped<MovieService, MovieService>();
builder.Services.AddScoped<SessionService, SessionService>();
builder.Services.AddScoped<TheaterService, TheaterService>();
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddSwaggerGen(options => {
    options.EnableAnnotations(); 
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Movies API",
        Description = "An API designed to manage Theaters.",
        Version = "v1",
        Contact = new OpenApiContact
        {
            Name = "Filipe Dias",
            Email = "filipediascontato@gmail.com"
        }
    });
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
