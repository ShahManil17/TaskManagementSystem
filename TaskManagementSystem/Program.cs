using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using System.Text;
using TaskManagementSystem.Core.Middlewares;
using TaskManagementSystem.Core.Repositories.AdminServices;
using TaskManagementSystem.Core.Repositories.Logins;
using TaskManagementSystem.Core.Repositories.ManagerServices;
using TaskManagementSystem.Data;

var builder = WebApplication.CreateBuilder(args);

var jwtKey = builder.Configuration.GetSection("Jwt:Key").Get<string>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
 .AddJwtBearer(options =>
 {
     options.TokenValidationParameters = new TokenValidationParameters
     {
         ValidateIssuer = true,
         ValidateAudience = true,
         ValidateLifetime = true,
         ValidateIssuerSigningKey = true,
         ValidIssuer = builder.Configuration.GetSection("Jwt:Issuer").Get<string>(),
         ValidAudience = builder.Configuration.GetSection("Jwt:Audience").Get<string>(),
         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
     };
 });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("addUsers", policy =>
    {
        policy.RequireClaim("addUsers");
    });
    options.AddPolicy("assignRoles", policy =>
    {
        policy.RequireClaim("assignRoles");
    });
    options.AddPolicy("assignPermissions", policy =>
    {
        policy.RequireClaim("assignPermissions");
    });
    options.AddPolicy("assignTasks", policy =>
    {
        policy.RequireClaim("assignTasks");
    });
    options.AddPolicy("editTask", policy =>
    {
        policy.RequireClaim("editTask");
    });
    options.AddPolicy("assignUsers", policy =>
    {
        policy.RequireClaim("assignUsers");
    });
});

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();

//Custom Services
builder.Services.AddTransient<ILogin, TaskManagementSystem.Core.Repositories.Logins.Login>();
builder.Services.AddTransient<IAdmin, Admin>();
builder.Services.AddTransient<IManager, Manager>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseCustomAuthMiddleware();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
