
using Microsoft.EntityFrameworkCore;
using PatientWebAPI.Models.DB;
using System;

namespace PatientWebAPI.Context
{
    public class AtlasContext : DbContext
    {
        public AtlasContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<PatientDetails> PatientDetails { get; set; }
        public DbSet<PatientLabResult> PatientLabResult { get; set; }
        public DbSet<PatientLabVisit> PatientLabVisit { get; set; }
        public DbSet<PatientMedication> PatientMedication { get; set; }
        public DbSet<PatientVaccinationData> PatientVaccinationData { get; set; }
        public DbSet<PatientVisitHistory> PatientVisitHistory { get; set; }
    }
}
