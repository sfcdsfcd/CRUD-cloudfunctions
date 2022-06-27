using Google.Cloud.Functions.Framework;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Npgsql;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net;
using desafio.Services;
using Google.Cloud.Functions.Hosting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using desafio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace desafio
{
    public class Startup1 : FunctionsStartup
    {
        public override void ConfigureServices(WebHostBuilderContext context, IServiceCollection services)
        {
            services.AddDbContext<Context>(options => options.UseNpgsql("Host=motty.db.elephantsql.com;Username=ykqhegsf;Password=q6Dixqs3-teKVaJk9mf5ZtMX0PWIp7ki;Database=ykqhegsf"));
        }
    }
    [FunctionsStartup(typeof(Startup1))]
    public class PegarTodasMarcas : IHttpFunction
    {
        private readonly ILogger _logger;
        private readonly Context _databaseContext;
        public PegarTodasMarcas(Context context)
        {
            _databaseContext = context;
        }

        public async Task HandleAsync(HttpContext context)
        {

            if (context.Request.Method != "GET")
            {
                context.Response.StatusCode = (int) HttpStatusCode.MethodNotAllowed;
                await context.Response.WriteAsync("Method Not Allowed");
                return;
            }
            var descricao = context.Request.Query["descricao"].ToString();

            var marcas = _databaseContext.Marcas.Where(p=> p.Descricao == descricao).ToListAsync().Result;
            string jsonString = JsonConvert.SerializeObject(marcas);

            var teste = context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(jsonString);
        }
    }
}
