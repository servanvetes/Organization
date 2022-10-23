
using Entities;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// CORS Eklenecek
builder.Services.AddControllersWithViews();
builder.Services.AddFluentValidation(rgv=>rgv.RegisterValidatorsFromAssemblyContaining<Program>());
builder.Services.AddSubService(builder.Configuration);
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(opts =>
    {
        opts.Cookie.Name = "AuthCookie";
        opts.LoginPath = "/Login/Login";
        opts.LogoutPath = "/Login/Logout";
        opts.AccessDeniedPath = "/Home/AccessDenied";      
        opts.Cookie.HttpOnly = true;
        opts.SlidingExpiration = false;
    });

//Cors kullan�labilir kendi site i�erisinden gelmesi i�in sadece
//builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

//app.UseCors(crs => 
//// Ayn� sitedeki (subdomain de olur) - Client
//crs.AllowAnyOrigin().
//// get,set gibi methodlar i�in
//AllowAnyMethod()
//);
app.UseHttpsRedirection();
app.UseStaticFiles();



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
