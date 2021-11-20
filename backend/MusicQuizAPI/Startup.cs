using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicQuizAPI.Services;
using MusicQuizAPI.Middleware;
using MusicQuizAPI.Filters;
using MusicQuizAPI.Database;
using AutoMapper;

namespace MusicQuizAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public void ConfigureServices(IServiceCollection services)
        {
            // Injecting MySql Database class to the services
            services.AddDbContext<MusicQuizDbContext>(opts => 
            {
                opts.UseMySql(Configuration["ConnectionString"], ServerVersion.AutoDetect(Configuration["ConnectionString"]));
            });
            
            // Adding CORS so that only specified origin can use the API
            services.AddCors(options =>
            {
                options.AddPolicy("MusicQuizPolicy", builder =>
                {
                    builder.WithOrigins(Configuration["AllowedOrigin"]);
                });
            });

            // Adding filters
            services.AddControllers(config =>
            {
                config.Filters.Add(new LogFilter());
            });

            // Adding AutoMapper to help transform one model to another
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Adding DI services
            services.AddSingleton<IConfiguration>(Configuration);

            services.AddScoped<AnimeRepository>();
            services.AddScoped<FavoriteAnimeRepository>();
            services.AddScoped<FavoriteSongRepository>();
            services.AddScoped<FriendshipRepository>();
            services.AddScoped<SongRepository>();
            services.AddScoped<TopAnimeRepository>();
            services.AddScoped<UserRepository>();

            services.AddScoped<AnimeService>();
            services.AddScoped<FavoriteAnimeService>();
            services.AddScoped<FavoriteSongService>();
            services.AddScoped<FriendshipService>();
            services.AddScoped<SongService>();
            services.AddScoped<UserService>();
            services.AddScoped<InitialDatabaseService>();

            // Adding MVC Routing
            services.AddMvc(opts =>
            {
                opts.EnableEndpointRouting = false;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseRouting();
            app.UseCors();
            app.UseAuthorization();

            // Using custom middleware
            app.UseMiddleware<ControllersCheckerMiddleware>();
            app.UseMiddleware<JwtMiddleware>();
            app.UseMiddleware<ExceptionMiddleware>();

            // Using specified route patern
            app.UseMvc(builder =>
            {
                builder.MapRoute("default", "api/{controller}/{action}/{id?}");
            });
        }
    }
}