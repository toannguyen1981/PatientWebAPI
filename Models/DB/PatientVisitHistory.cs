using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatientWebAPI.Models.DB
{
    public class PatientVisitHistory
    {
        public int Id { get; set; }

        [ForeignKey("PatientDetails_Id")]
        public PatientDetails PatientDetails { get; set; }
        public DateTime VisitDate { get; set; }
        public string DoctorName { get; set; }
        public string NurseName1 { get; set; }
        public string NurseName2 { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
