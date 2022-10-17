
using Entities;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// CORS Eklenecek
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddFluentValidation(rgv=>rgv.RegisterValidatorsFromAssemblyContaining<Program>());
builder.Services.AddSubService(builder.Configuration);

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(opts =>
    {
        opts.Cookie.Name = "AuthCookie";
        //opts.SlidingExpiration = false;
        opts.LoginPath = "/Login/Login";
        opts.LogoutPath = "/Login/Logout";
        opts.AccessDeniedPath = "/Home/AccessDenied";      
        opts.Cookie.HttpOnly = true;
        opts.SlidingExpiration = false;
        //opts.Events = new CookieAuthenticationEvents
        //{
            
        // pro   
        //    OnSigningIn = CookieValidator.AddAuthenticationInstantClaimAsync,
        //    OnValidatePrincipal= 
        //    = CookieValidator.ValidateAsync
        //};

    });



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();
app.UseStaticFiles();





app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
// pattern: "{controller=Activities}/{action=All}/{id?}");
app.Run();
