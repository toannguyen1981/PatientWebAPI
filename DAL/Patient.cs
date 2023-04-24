using Microsoft.EntityFrameworkCore;
using PatientWebAPI.Context;
using PatientWebAPI.Models.DB;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PatientWebAPI.DAL
{
    public class Patient : IPatient
    {
        private readonly AtlasContext _atlasContext;
        public Patient(AtlasContext atlasContext)
        {
            _atlasContext = atlasContext;
        }

        public async Task SaveChanges()
        {
            await _atlasContext.SaveChangesAsync();
        }
        public async Task AddPatientLabVist(List<PatientLabVisit> patientLabVisitList)
        {
            await _atlasContext.PatientLabVisit.AddRangeAsync(patientLabVisitList);
        }

        /// <summary>
        /// Check if the patient is already exist
        /// </summary>
        /// <param name="patientDetails"></param>
        /// <returns>true if the patient exists, false if the patient does not exist</returns>
        public async Task<bool> IsExistingPatientAsync(PatientDetails patientDetails)
        {
            var result = await _atlasContext.PatientDetails.Where(x => x.Ssn == patientDetails.Ssn).FirstOrDefaultAsync();
            if(result != null)
                return true;

            return false;
        }
        public async Task<PatientDetails> GetPatientDataAsync(int patientId)
        {
            var result = await _atlasContext.PatientDetails.Where(x => x.Id == patientId).FirstOrDefaultAsync();
            return result;
        }
        public async Task AddPatientAsync(PatientDetails patientDetails)
        {
           await _atlasContext.PatientDetails.AddAsync(patientDetails);
        }

        public async Task DeletePatientAsync(int patientId)
        {
            var result = await _atlasContext.PatientDetails.Where(x => x.Id == patientId).FirstOrDefaultAsync();
             _atlasContext.Remove(result);
        }

    }
}
