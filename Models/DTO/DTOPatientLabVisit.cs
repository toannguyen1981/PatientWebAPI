using Newtonsoft.Json;

namespace PatientWebAPI.Models.DTO
{
    public class DTOPatientLabVisit
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("SSN")]
        public string Ssn { get; set; }

        [JsonProperty("lab_name")]
        public string LabName { get; set; }

        [JsonProperty("lab_test_request")]
        public string LabTestRequest { get; set; }

        [JsonProperty("collection_date")]
        public string CollectionDate { get; set; }

        [JsonProperty("result_date")]
        public string ResultDate { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }
    }
}
