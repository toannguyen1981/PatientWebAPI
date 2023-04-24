using PatientWebAPI.Models.DB;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace PatientWebAPI.DAL
{
    public interface IPatient
    {
        public Task SaveChanges();
        public Task AddPatientLabVist(List<PatientLabVisit> patientLabVisitList);
        public Task<bool> IsExistingPatientAsync(PatientDetails patientDetails);
        public Task<PatientDetails> GetPatientDataAsync(int patientId);
        public Task AddPatientAsync(PatientDetails patientDetails);
        public Task DeletePatientAsync(int patientId);
    }
}
