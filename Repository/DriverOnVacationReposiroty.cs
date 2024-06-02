using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Serializer;
using Syncfusion.Data.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BookingApp.Repository
{
    class DriverOnVacationReposiroty : IDriverOnVacationRepository
    {
        private const string FilePath = "../../../Resources/Data/driveronvacation.csv";
        private readonly Serializer<DriverOnVacation> serializer;
        private List<DriverOnVacation> vacations;

        public DriverOnVacationReposiroty()
        {
            serializer = new Serializer<DriverOnVacation>();
            vacations = serializer.FromCSV(FilePath);
        }
        public void Delete(int id)
        {
            vacations = serializer.FromCSV(FilePath);
            DriverOnVacation? founded = vacations.Find(v => v.DriverId == id);
            if (founded != null)
            {
                vacations.Remove(founded);
                serializer.ToCSV(FilePath, vacations);
            }
        }

        public List<DriverOnVacation> GetAll()
        {
            var lista = serializer.FromCSV(FilePath);
            lista.ForEach(d => { d.UpdateModel(); });
            return lista;
        }

        public DriverOnVacation GetById(int id)
        {
            var jedan =  serializer.FromCSV(FilePath).Where(v => v.DriverId == id).First();
            jedan.UpdateModel();
            return jedan;
        }

        public DriverOnVacation Save(DriverOnVacation vacationType)
        {
            vacations = serializer.FromCSV(FilePath);
            vacations.Add(vacationType);
            serializer.ToCSV(FilePath, vacations);
            return vacationType;

        }

        public DriverOnVacation Update(DriverOnVacation vacationType)
        {
            vacations = serializer.FromCSV(FilePath);
            DriverOnVacation? cur = vacations.Find(r => r.DriverId == vacationType.DriverId);
            if (cur != null)
            {
                int index = vacations.IndexOf(cur);
                vacations.Remove(cur);
                vacations.Insert(index, vacationType);
                serializer.ToCSV(FilePath, vacations);
            }
            return vacationType;
        }
    }
}
