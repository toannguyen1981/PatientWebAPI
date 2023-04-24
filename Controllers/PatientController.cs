using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PatientWebAPI.DAL;
using PatientWebAPI.Models.DB;
using PatientWebAPI.Models.DTO;
using PatientWebAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly IPatient _patient;
        private readonly IMapper _mapper;
        private readonly IPatientExternalWebApi _patientExternalWebApi;
        private readonly ILogger _logger;
        public PatientController(IPatient patient, IMapper mapper, ILogger<PatientController> logger, IPatientExternalWebApi patientExternalWebApi)
        {
            _patient = patient;
            _mapper = mapper;
            _patientExternalWebApi = patientExternalWebApi;
            _logger = logger;
        }

        [HttpGet("GetPatientData")]
        public async Task<IActionResult> Get(int patientId)
        {
            try
            {
                var result = await _patient.GetPatientDataAsync(patientId);
                if (result != null)
                    return Ok(result);
                else
                    return NotFound($"No patient info based on patient id: {patientId}");
            }
            catch(Exception ex)
            {
                _logger.LogCritical(ex, "Failed to retrieve patient information", new object[] { patientId });
                return StatusCode(500, "An issue has occured with this request.");
            }
        }

        [HttpPost("RegisterNewPatient")]
        public async Task<IActionResult> Post([FromBody] DTOPatientDetails dtoPatientDetails)
        {
            int newPatientId = 0;
            try
            {
                var mappedPatientDetails = _mapper.Map<PatientDetails>(dtoPatientDetails);
                if (await _patient.IsExistingPatientAsync(mappedPatientDetails))
                    return BadRequest("This patient is already exist");

                //assign a random # from 1-1000 as the new patient id
                var random = new Random();
                newPatientId = random.Next(1, 1000);
                mappedPatientDetails.Id = newPatientId;

                //run all three external apis concurrently and srubbing the data before saving to the database
                var taskPatientLabVisitList1 = Task.Run(async () =>
                {
                    var dtoPatientVisitList = await _patientExternalWebApi.GetPatientLabVisit(mappedPatientDetails.Ssn);
                    var patientVisitList = _mapper.Map<List<DTOPatientLabVisit>, List<PatientLabVisit>>(dtoPatientVisitList);

                    patientVisitList.ToList().ForEach(x =>
                    {
                        x.PatientDetails = mappedPatientDetails;
                        x.Id = null;
                    });

                    return patientVisitList;
                });

                var taskPatientLabVisitList2 = Task.Run(async () =>
                {
                    var dtoPatientVisitList = await _patientExternalWebApi.GetPatientLabVisit(mappedPatientDetails.Ssn);
                    var patientVisitList = _mapper.Map<List<DTOPatientLabVisit>, List<PatientLabVisit>>(dtoPatientVisitList);

                    patientVisitList.ToList().ForEach(x =>
                    {
                        x.PatientDetails = mappedPatientDetails;
                        x.Id = null;
                    });

                    return patientVisitList;
                });

                var taskPatientLabVisitList3 = Task.Run(async () =>
                {
                    var dtoPatientVisitList = await _patientExternalWebApi.GetPatientLabVisit(mappedPatientDetails.Ssn);
                    var patientVisitList = _mapper.Map<List<DTOPatientLabVisit>, List<PatientLabVisit>>(dtoPatientVisitList);

                    patientVisitList.ToList().ForEach(x =>
                    {
                        x.PatientDetails = mappedPatientDetails;
                        x.Id = null;
                    });

                    return patientVisitList;
                });

                Task.WaitAll(taskPatientLabVisitList1, taskPatientLabVisitList2, taskPatientLabVisitList3);

                //check if any of the results is emptied because no orphan data is allowed
                if (!taskPatientLabVisitList1.Result.Any() ||
                    !taskPatientLabVisitList2.Result.Any() ||
                    !taskPatientLabVisitList3.Result.Any() 
                    )
                {
                    _logger.LogCritical("There are no records of this patient visit", new object[] { taskPatientLabVisitList1 });
                    return BadRequest("Unable to register patient");
                }

                //save patient details first (header/master record) then its dependencies
                await _patient.AddPatientAsync(mappedPatientDetails);
                await _patient.AddPatientLabVist(taskPatientLabVisitList1.Result);
                await _patient.AddPatientLabVist(taskPatientLabVisitList2.Result);
                await _patient.AddPatientLabVist(taskPatientLabVisitList3.Result);
                await _patient.SaveChanges();
                
                return StatusCode(201, $"New patient registered. Patient's id: {newPatientId}");
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, ex.Message, new object[] { dtoPatientDetails });
                return StatusCode(500, "An issue has occured with this request.");
            }
        }

        /*
        [HttpGet("TestToken")]
        public async Task<IActionResult> Get()
        {
            var result = await _patientExternalWebApi.GetPatientLabVisit("2");
            foreach (var item in result)
            {
                var single = _mapper.Map<PatientLabVisit>(item);
                var a = "hello";
            }
            return null;
        }
        */
    }
}
