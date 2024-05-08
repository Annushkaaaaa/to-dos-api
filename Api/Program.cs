using DataAccess;
using Application;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
//using TourmalineCore.AspNetCore.JwtAuthentication.Core;
//using TourmalineCore.AspNetCore.JwtAuthentication.Core.Options;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddControllers(opt => { opt.OutputFormatters.RemoveType<HttpNoContentOutputFormatter>(); });

builder.Services.AddCors();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddPersistence(configuration);
builder.Services.AddApplication();
//var authenticationOptions = configuration.GetSection(nameof(AuthenticationOptions)).Get<AuthenticationOptions>();
//builder.Services.AddJwtAuthentication(authenticationOptions)
//    .WithUserClaimsProvider<UserClaimsProvider>(UserClaimsProvider.PermissionClaimType);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .CreateLogger();

builder.Host.ConfigureLogging(logging =>
    {
        logging.AddSerilog();
        logging.SetMinimumLevel(LogLevel.Information);
    })
    .UseSerilog();

builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1",
            new OpenApiInfo
            {
                Version = "0.0.1",
            }
        );

        //c.AddSecurityDefinition("Bearer",
        //    new OpenApiSecurityScheme
        //    {
        //        Name = "Authorization",
        //        In = ParameterLocation.Header,
        //        Type = SecuritySchemeType.ApiKey,
        //        Scheme = "Bearer"
        //    }
        //);

        //c.AddSecurityRequirement(new OpenApiSecurityRequirement
        //    {
        //        {
        //            new OpenApiSecurityScheme
        //            {
        //                Reference = new OpenApiReference
        //                {
        //                    Type = ReferenceType.SecurityScheme,
        //                    Id = "Bearer"
        //                },
        //                Scheme = "oauth2",
        //                Name = "Bearer",
        //                In = ParameterLocation.Header
        //            },
        //            new List<string>()
        //        }
        //    }
        //);
    }
);

var app = builder.Build();

app.UseCors(
    corsPolicyBuilder => corsPolicyBuilder
        .AllowAnyHeader()
        .SetIsOriginAllowed(_ => true)
        .AllowAnyMethod()
        .AllowAnyOrigin()
);

app.UseStaticFiles();

app.UseSwagger();

app.UseSwaggerUI();

using (var serviceScope = app.Services.CreateScope())
{
    var context = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
    await context.Database.MigrateAsync();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
//app.UseJwtAuthentication();

app.MapControllers();

app.Run();