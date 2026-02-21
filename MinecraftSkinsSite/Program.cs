using MinecraftSkinsSite.Data;
using MinecraftSkinsSite.Interfaces;
using MinecraftSkinsSite.Repositories;
using MinecraftSkinsSite.Services;

namespace MinecraftSkinsSite
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddHttpClient<IBtcRateService, BtcRateService>();
            builder.Services.AddSingleton<IInMemoryDatabase, InMemoryDatabase>();
            builder.Services.AddSingleton<ISkinRepository, SkinRepository>();
            builder.Services.AddSingleton<IPurchaseRepository, PurchaseRepository>();
            builder.Services.AddScoped<ISkinsService, SkinsService>();
            builder.Services.AddScoped<IPurchasesService, PurchasesService>();
            builder.Services.AddScoped<IPriceService, PriceService>();
            builder.Services.AddMemoryCache();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
