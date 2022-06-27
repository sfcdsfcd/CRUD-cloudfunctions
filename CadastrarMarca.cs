using desafio.Entidades;
using Google.Cloud.Functions.Framework;
using Google.Cloud.Functions.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Net;
using System.Net.Mime;
using System.Text.Json;
using System.Threading.Tasks;

namespace desafio
{
    public class Startup2 : FunctionsStartup
    {
        public override void ConfigureServices(WebHostBuilderContext context, IServiceCollection services)
        {
            services.AddDbContext<Context>(options => options.UseNpgsql("Host=motty.db.elephantsql.com;Username=ykqhegsf;Password=q6Dixqs3-teKVaJk9mf5ZtMX0PWIp7ki;Database=ykqhegsf"));
        }
    }
    [FunctionsStartup(typeof(Startup2))]
    public class CadastrarMarca : IHttpFunction
    {
        private readonly ILogger _logger;
        private readonly Context _databaseContext;
        public CadastrarMarca(ILogger<CadastrarMarca> logger, Context context)
        {
            _databaseContext = context;
            _logger = logger;
        }
        public async Task HandleAsync(HttpContext context)
        {
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;

            ContentType contentType = new ContentType(request.ContentType);


            if (contentType.MediaType != "application/json" || request.Method != "PUT")
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await context.Response.WriteAsync("Wrong Method");
                return;
            }

            using TextReader reader = new StreamReader(request.Body);
            string json = await reader.ReadToEndAsync();
            JsonElement body = JsonSerializer.Deserialize<JsonElement>(json.ToLower());

            if (body.TryGetProperty("descricao", out JsonElement property) && property.ValueKind == JsonValueKind.String)
            {
                Marca newMarcaObj = new Marca() { Descricao = property.GetString() };
                await _databaseContext.Marcas.AddAsync(newMarcaObj);
                await _databaseContext.SaveChangesAsync();

                await context.Response.WriteAsync("Marca criada com sucesso");
            }
            else
            {
                response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
        }
    }
}
