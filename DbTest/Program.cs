using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbLibrary2;
namespace DbTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var patientService = new PatientService();
            //регистрируем пациента.
            //Для примера указываем не все параметры
            Console.WriteLine(patientService.TestConfig);
            patientService.Registrate(new Patient()
            {
                FirstName = "Пётр",
                MiddleName = "Петрович",
                LastName = "Петров",
                Gender = true,
                DateOfBirth = new DateTime(1991, 1, 18)
            });
            /*var patients = patientService.FindPatients("Пётр", "Петрович", "Петров");
            foreach (var patient in patients)
            {
                Console.WriteLine($"{patient.FirstName} {patient.LastName}");
            }*/
            Console.ReadKey();
        }
    }
}
