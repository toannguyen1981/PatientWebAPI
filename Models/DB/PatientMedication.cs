using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace PatientWebAPI.Models.DB
{
    [Keyless]
    public class PatientMedication
    {
        [ForeignKey("PatientDetails_Id")]
        public PatientDetails PatientDetails { get; set; }

        [ForeignKey("PatientLabVisit_Id")]
        public PatientLabVisit PatientLabVisit { get; set; }
        public string MedicineName { get; set; }
        public string Dosage { get; set; }
        public string Frequency { get; set; }
        public string PrescribedBy { get; set; }
        public DateTime PrescriptionDate { get; set; }
        public string PrescriptionPeriod { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
