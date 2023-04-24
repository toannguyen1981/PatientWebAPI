using PatientWebAPI.Models.DTO;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatientWebAPI.Models.DB
{
    public class PatientLabVisit
    {
        public int? Id { get; set; }

        [ForeignKey("PatientDetails_Id")]
        public PatientDetails PatientDetails { get; set; }
        public string LabName { get; set; }
        public string LabTestRequest { get; set; }
        public string ResultDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
