using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MiniSuperPF.Models;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();

        // Agg inf de conexion base de datos
        var CnnStrBuilder = new SqlConnectionStringBuilder(
            builder.Configuration.GetConnectionString("CNNSTR"));

        //Def variable local
        string cnnStr = CnnStrBuilder.ConnectionString;
      

        // def cnnstring al proyecto
        builder.Services.AddDbContext<BD_MiniSuperContext>(options => options.UseSqlServer(cnnStr));


        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseRouting();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}