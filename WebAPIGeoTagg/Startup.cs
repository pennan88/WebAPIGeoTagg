using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using WebAPIGeoTagg.Data;
using WebAPIGeoTagg.Handler;

namespace WebAPIGeoTagg
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            services.AddApiVersioning(o =>
            {
                o.DefaultApiVersion = new ApiVersion(2, 0);
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.ReportApiVersions = true;
            });

            services.AddVersionedApiExplorer(o =>
            {
                o.GroupNameFormat = "'v'VVV";

                o.SubstituteApiVersionInUrl = true;
            });

            services.AddSwaggerGen(c =>
            {

                c.AddSecurityDefinition("basic", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "basic",
                    In = ParameterLocation.Header,
                    Description = "Basic Authorization header using the Bearer scheme."
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "basic"
                            }
                        },
                        Array.Empty<string>()
                    }
                });

                c.SwaggerDoc("v1", new OpenApiInfo
                { Title = "GeoTagging v1", Version = "v1.0" });

                c.SwaggerDoc("v2", new OpenApiInfo
                { Title = "GeoTagging v2", Version = "v2.0" });

                var path = Path.Combine(AppContext.BaseDirectory, "WebAPIGeoTaggDocument.xml");
                c.IncludeXmlComments(path);

                c.EnableAnnotations();
            });

            services.AddDbContext<GeoCommentDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("UserDbContextConnection")));

            services.AddIdentityCore<IdentityUser>()
                .AddEntityFrameworkStores<GeoCommentDbContext>();

            services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint($"/swagger/v1/swagger.json", "v1.0");
                    c.SwaggerEndpoint($"/swagger/v2/swagger.json", "v2.0");

                });
            }
            app.UseHttpsRedirection();


            app.UseCors(options => options.
            SetIsOriginAllowed(origin => true)
            .AllowCredentials()
            .AllowAnyMethod()
            .AllowAnyHeader());



            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
