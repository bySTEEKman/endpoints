using System;
using System.Text.Encodings;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using System.Web;
using System.Text.Json.Serialization;

namespace endpoints
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration {get;}
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/headers", async context =>
                {
                    var headers = context.Request.Headers;
                    string output = "";
                    foreach(var header in headers)
                    {
                        output += header.Key + ": " + header.Value + ";\n";
                    }
                    await context.Response.WriteAsync(output);
                });
                endpoints.MapGet("/plural", async context =>
                {
                    Plural plural = new Plural();
                    int number = Int32.Parse(context.Request.Query["number"]);
                    string[] forms = context.Request.Query["forms"].ToString().Split(",");

                    context.Response.Headers.Add("Number", $"{number}");
                    context.Response.Headers.Add("Word", $"{forms}");

                    string answer = $"{number} {plural.MakePlural(number, forms)}";
                    
                    await context.Response.WriteAsync(answer);
                });
                endpoints.MapPost("/frequency", async context =>
                {
                    string raw = await new StreamReader(context.Request.Body).ReadToEndAsync();

                    Frequency frequency = new Frequency();
                    frequency.GetFrequencyWords(raw);

                    context.Response.ContentType = "application/json";
                    context.Response.Headers.Add("Unquie-Words", $"{frequency.GetUniqueWords()}");
                    context.Response.Headers.Add("MostPopular-Word", $"{frequency.GetMostPopularWord()}");

                    string output = JsonSerializer.Serialize(frequency.GetDictionary());

                    await context.Response.WriteAsync(output);
                });
            });
        }
    }
}
