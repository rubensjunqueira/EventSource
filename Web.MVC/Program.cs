using MassTransit;
using MassTransit.SignalR;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Web.MVC.EventConsumers;
using Web.MVC.Hubs;

namespace Web.MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddStackExchangeRedisCache(cache =>
            {
                cache.Configuration = "redis";
            });

            builder.Services.AddSignalR();
            builder.Services.AddMassTransit(x =>
            {
                x.AddSignalRHub<EventHub>();
                x.AddConsumer<EventEmitterConsumer>();
                x.AddConsumer<EventStorageConsumer>();
                x.SetKebabCaseEndpointNameFormatter();
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("rabbitmq", "/", h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });
                    cfg.ConfigureEndpoints(context);
                });
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

            app.UseAuthorization();

            app.MapHub<EventHub>("/eventHub");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}