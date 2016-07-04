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
using System.Configuration;
namespace DbLibrary2
{
    public class PatientService
    {
        private PatientsModel _patientsContext = new PatientsModel();
        public string TestConfig = ConfigurationManager.ConnectionStrings["PatientsModel"].ConnectionString;
        public void Registrate(Patient patient)
        {
            _patientsContext.Patients.Add(patient);
            _patientsContext.SaveChanges();
        }
        public IQueryable<Patient> FindPatients(string firstName, string middleName, string lastName)
        {
            var patients = _patientsContext.Patients.Where(patient => (patient.FirstName.ToUpper() == firstName.ToUpper())
            && (patient.MiddleName.ToUpper() == middleName.ToUpper()) && (patient.LastName.ToUpper() == lastName.ToUpper()));
            return patients;
        }

    }
}