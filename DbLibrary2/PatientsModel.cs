namespace DbLibrary2
{
    using System;
    using System.Collections.Generic;

    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Data.Entity;
    using System.Data.Entity.Validation;
    using System.Data.SqlServerCe;
    using System.ComponentModel.DataAnnotations;
    using System.Drawing;
    using System.Drawing.Imaging;

    public partial class PatientsModel : DbContext
    {
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Point> Points { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<PointType> PointTypes { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<HistoryNote> HistoryNotes { get; set; }
        public DbSet<Diagnosis> Diagnosis { get; set; }
        public DbSet<Conclusion> Conclusions { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Complaints> Complaints { get; set; }
        public DbSet<Methodology> Methodologies { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>().HasKey(patient => patient.Id);
            modelBuilder.Entity<Patient>().Property(patient => patient.FirstName)
                .IsRequired();
            modelBuilder.Entity<Patient>().Property(patient => patient.LastName).IsRequired();
            modelBuilder.Entity<Patient>().Property(patient => patient.Gender).IsRequired();
            modelBuilder.Entity<Patient>().Property(patient => patient.DateOfBirth).IsRequired();

            modelBuilder.Entity<Point>().HasKey(point => point.Id);
            modelBuilder.Entity<Point>().Property(point => point.X).IsRequired();
            modelBuilder.Entity<Point>().Property(point => point.Y).IsRequired();
            modelBuilder.Entity<Point>().Property(point => point.PointName).IsRequired();
            modelBuilder.Entity<Point>().Property(point => point.PictureId).IsRequired();

            modelBuilder.Entity<HistoryNote>()
                .HasOptional(historyNote => historyNote.Comments)
                .WithRequired(comment => comment.HistoryNote);
            modelBuilder.Entity<HistoryNote>()
                .HasOptional(historyNote => historyNote.Complaints)
                .WithRequired(complaint => complaint.HistoryNote);
            modelBuilder.Entity<HistoryNote>()
                .HasMany(historyNote => historyNote.Doctors)
                .WithMany(doctor => doctor.HistoryNotes)
                .Map(doctorHistoryNote =>
                {
                    doctorHistoryNote.MapLeftKey("HistoryNoteRefId");
                    doctorHistoryNote.MapRightKey("DoctorRefId");
                    doctorHistoryNote.ToTable("NoteDoctorHistory");
                });
        }
        public PatientsModel()
            : base("name=PatientsModel")
        {
        }
    }
    public class Patient
    {
        public long Id { get; set; }
        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Отчество
        /// </summary>
        public string MiddleName { get; set; }
        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Пол. Равно true, если мужской.
        /// </summary>
        public bool Gender { get; set; }
        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateTime DateOfBirth { get; set; }
        /// <summary>
        /// Адрес
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// Мобильный телефон
        /// </summary>
        public string Phone { get; set; }
        public string Email { get; set; }
        public virtual ICollection<HistoryNote> HistoryNotes { get; set; }

    }
    public class HistoryNote
    {
        public long Id { get; set; }

        public string Complaints { get; set; }
        public string Comments { get; set; }
        public string Conclusion { get; set; }
        public virtual ICollection<Diagnosis> Diagnosis { get; set; }       

        public virtual ICollection<Picture> Pictures { get; set; }

        public virtual ICollection<Doctor> Doctors { get; set; }
        public DateTime Date { get; set; }
        public long PatientId { get; set; }
        public virtual Patient Patient { get; set; }
    }
    public class Picture
    {
        public long Id { get; set; }
        [MaxLength]
        public byte[] Data { get; set; }
        public virtual ICollection<Point> Points { get; set; }
        public string Type { get; set; }
        public long HistoryNoteId { get; set; }
        public virtual HistoryNote HistoryNote { get; set; }
    }
    public class Diagnosis
    {
        public long Id { get; set; }
        public string BasicDiagnosis { get; set; }
        public string ConcomitantDiagnosis { get; set; }
        public string Icd { get; set; }
        public long HistoryNoteId { get; set; }
        public virtual HistoryNote HistoryNote { get; set; }
    }
    public class Doctor
    {
        public long Id { get; set; }
        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Отчество
        /// </summary>
        public string MiddleName { get; set; }
        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get; set; }
        public virtual ICollection<HistoryNote> HistoryNotes { get; set; }
    }


    public class Point
    {
        public long Id { get; set; }
        public long PointTypeId { get; set; }
        public virtual PointType Type { get; set; }
        public string PointName { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public long PictureId { get; set; }
        public virtual Picture Picture { get; set; }
    }

    public class Methodology
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<PointType> PointNames { get; set; }
    }

    public class PointType
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long MethodologyId { get; set; }
        public virtual Methodology Methodology { get; set; }
    }

}
