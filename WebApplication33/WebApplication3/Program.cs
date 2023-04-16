using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using WebApplication3.Interfaces;
using WebApplication3.Models;
using Microsoft.Extensions.Configuration;
using WebApplication3.DB_Settings;
using Microsoft.EntityFrameworkCore;
using WebApplication3.DB;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);
/*string connection = builder.Configuration.GetConnectionString("DefaultConnection");*/

// Add services to the container.

builder.Services.AddRazorPages();
builder.Services.AddMvc(options => options.EnableEndpointRouting = false);
builder.Services.AddTransient<IGoods, Goods_DB>();
builder.Services.AddTransient<IGoodsCategories, Categories_DB>();

builder.Services.AddDbContext<DB_Content>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options => 
                                                                                                          {
                                                                                                              options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/AccountController/Login");
                                                                                                              options.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
                                                                                                          });

var app = builder.Build();

using var service = app.Services.CreateScope();
var content = service.ServiceProvider.GetService<DB_Content>();
Objects_DB.Initial(content);
content?.Database.Migrate();

// Configure the HTTP request pipeline.



if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseDeveloperExceptionPage();
app.UseStatusCodePages();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Goods}/{action=GoodsList}/{id?}"
    );

app.Run();
