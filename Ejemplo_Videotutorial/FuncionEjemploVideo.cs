using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Ejemplo_Videotutorial
{
    public static class FuncionEjemploVideo
    {
        [FunctionName("FuncionEjemploVideo")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            string numeroIntroducido = req.Query["numero"];
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            numeroIntroducido = numeroIntroducido ?? data?.name;
            string respuesta = "";
            respuesta = saberPar_Impar(numeroIntroducido, respuesta);
            return new OkObjectResult(respuesta);
        }

        private static string saberPar_Impar(string numeroIntroducido, string respuesta)
        {
            int numero = Convert.ToInt32(numeroIntroducido);
            if (numero == 0)
                respuesta += "Esta funcion HTTP trigger se ejecutó correctamente. " +
                    "Pásale un valor para que pueda procese la función, el número.";
            else if (numero % 2 == 0)
                respuesta += "El número " + numero + " introducido es par";
            else
                respuesta += "El número " + numero + " introducido es impar";
            return respuesta;
        }
    }
}
