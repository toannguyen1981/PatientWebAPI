using Newtonsoft.Json;

namespace PatientWebAPI.Models
{
    public class Credential
    {
        [JsonProperty("identifier")]
        public string Identifier { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
