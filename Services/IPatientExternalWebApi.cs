using PatientWebAPI.Models;
using PatientWebAPI.Models.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PatientWebAPI.Services
{
    public interface IPatientExternalWebApi
    {
        public Task<string> GetJWTToken(Credential credential, string url);
        public Task<List<DTOPatientLabVisit>> GetPatientLabVisit(string ssn);
        public Task GetPatientLabResults(string ssn);
        public Task GetPatientMedication(string ssn);
        public Task GetPatientVaccination(string ssn);
    }
}
