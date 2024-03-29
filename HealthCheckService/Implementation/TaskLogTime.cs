﻿using HealthCheckService.Contract;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace HealthCheckService.Implementation
{
    public class TaskLogTime : ITaskLogTime
    {
        public async Task DoWork(CancellationToken cancellationToken)
        {
            await Execute();
        }

        public async static void WriteFile(string response)
        {
            string path = @$"E:\logs\service-log-{DateTime.Now.ToString("yyyy-MM-dd")}.txt";

            await using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine(response);
                writer.Close();
            }
        }

        public async Task Execute()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:8077");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.GetAsync("/health");
                    if (response.IsSuccessStatusCode)
                    {
                        var res = await response.Content.ReadAsStringAsync();
                        //var jsonResponse = JsonConvert.DeserializeObject<JsonResponse>(res);

                        WriteFile(res);
                    }
                    else
                    {
                        Console.WriteLine("Internal server Error");
                    }
                }
            }
            catch (Exception ex)
            {
                WriteFile(ex.Message);
            }
        }
    }
}
