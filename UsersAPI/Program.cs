using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using UsersAPI.Data;
using UsersAPI.Providers;
using UsersAPI.Services;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("default");
builder.Services.AddDbContext<UserDbContext>(options => 
    options.UseLazyLoadingProxies()
    .UseNpgsql(connectionString, options => options.EnableRetryOnFailure())
);

// Add services to the container.
builder.Services.AddScoped<IEmailProvider, MailKitEmailProvider>();
builder.Services.AddScoped<LoginService, LoginService>();
builder.Services.AddScoped<LogoutService, LogoutService>();
builder.Services.AddScoped<RegisterService, RegisterService>();
builder.Services.AddScoped<TokenService, TokenService>();
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddIdentity<IdentityUser<int>, IdentityRole<int>>(
        options => options.SignIn.RequireConfirmedEmail = true)
    .AddEntityFrameworkStores<UserDbContext>()
    .AddDefaultTokenProviders();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddSwaggerGen(options => {
    options.EnableAnnotations();
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Users API",
        Description = "An API designed to manage Users.",
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
