﻿using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;

namespace PreCompileEnvironmentVariables
{
    public class FunctionTrigger
    {
        public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, TraceWriter log)
        {
            log.Info($"Webhook was triggered!");

            var appKey = "FooKey";
            var appValue = Environment.GetEnvironmentVariable(appKey);
            log.Info($"App Setting. Key : {appKey}, Value : {appValue}");

            string jsonContent = await req.Content.ReadAsStringAsync();
            dynamic data = JsonConvert.DeserializeObject(jsonContent);

            if (data.first == null || data.last == null)
            {
                return req.CreateResponse(HttpStatusCode.BadRequest, new
                {
                    error = "Please pass first/last properties in the input object"
                });
            }

            log.Info("Hello world");
            return req.CreateResponse(HttpStatusCode.OK, new
            {
                hello = $"Hello World. {data.first} {data.last}!"
            });
        }
    }
}
