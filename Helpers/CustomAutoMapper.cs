using AutoMapper;
using PatientWebAPI.Models.DB;
using PatientWebAPI.Models.DTO;

namespace PatientWebAPI.Helpers
{
    public class CustomAutoMapper : Profile
    {
        public CustomAutoMapper()
        {
            CreateMap<DTOPatientDetails, PatientDetails>();
            CreateMap<DTOPatientLabVisit, PatientLabVisit>();
        }
    }
}
