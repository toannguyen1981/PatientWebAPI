using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatientWebAPI.Models.DB
{
    public class PatientVaccinationData
    {
        public int Id { get; set; }

        [ForeignKey("PatientDetails_Id")]
        public PatientDetails PatientDetails { get; set; }
        public DateTime VaccineDate { get; set; }
        public string VaccineValidity { get; set; }
        public string AdministeredBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
