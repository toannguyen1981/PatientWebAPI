using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatientWebAPI.Models.DB
{
    public class PatientLabResult
    {
        public int Id { get; set; }

        [ForeignKey("PatientLabVisit_Id")]
        public PatientLabVisit PatientLabVisit { get; set; }
        public string TestName { get; set; }
        public string TestResult { get; set; }
        public string TestObservation { get; set; }
        public string Attachments { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
