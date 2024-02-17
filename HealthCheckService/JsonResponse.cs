using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCheckService
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class JsonResponse
    {
        [JsonProperty("totalDuration")]
        public string totalDuration { get; set; }

        [JsonProperty("status")]
        public string status { get; set; }

        public Entries entries { get; set; }
    }

    public class Entries
    {
        public Api publicapi  { get; set; }
        public Api catapi { get; set; }
        public Api mongodb { get; set; }
        public Api sqlserver { get; set; }
    }

    //public class Mongodb
    //{
    //    public string duration { get; set; }
    //    public string status { get; set; }
    //}

    public class Api
    {
        [JsonProperty("duration")]
        public string duration { get; set; }
        [JsonProperty("status")]
        public string status { get; set; }
    }

    //public class Sqlserver
    //{
    //    public string duration { get; set; }
    //    public string status { get; set; }
    //}


}