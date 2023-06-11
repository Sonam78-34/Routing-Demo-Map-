using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PassTheDataFromModelMVC.Context;
using PassTheDataFromModelMVC.Controllers;
using PassTheDataFromModelMVC.Repository;

namespace PassTheDataFromModelMVC
{
    public class Program
    {
        
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<IStudentRepository, StudentRepository>();

            //for connection to sql
            var connectionString = builder.Configuration.GetConnectionString("StudentDbContext");
            builder.Services.AddDbContext<StudentDbContext>(options => options.UseSqlServer (connectionString));

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
    
            app.Use(async (context, next) =>
            {
                Endpoint endpoint = context.GetEndpoint();
                
                await next(context);
            });

            app.UseRouting();

            


            app.UseEndpoints(endpoint => 
            {
                endpoint.MapGet("/Student", async (context) =>
                {
                   
                    await context.Response.WriteAsync("This is Http Get Page");


                });
               endpoint.MapPost("/StudentDetails/{createPage}", async (context) =>
                {
                    await context.Response.WriteAsync("This http post is  created");


                }); 
              endpoint.MapDelete("/Student", async (context) =>
              {
                  await context.Response.WriteAsync("Delete operation succesfull");


              });
              endpoint.MapPut("/Student", async (context) =>
              {
                  await context.Response.WriteAsync("Http put for update the message");


              }); 
            });

          


            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}"); 

            app.Run();
        }
    }
}