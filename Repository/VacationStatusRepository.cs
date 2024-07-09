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
    class VacationStatusRepository : IVacationStatusRepository
    {
        private const string FilePath = "../../../Resources/Data/vacationstatus.csv";
        private readonly Serializer<VacationStatus> serializer;
        private List<VacationStatus> vacations;

        public VacationStatusRepository()
        {
            serializer = new Serializer<VacationStatus>();
            vacations = serializer.FromCSV(FilePath);
        }
        public void Delete(int id)
        {
            vacations = serializer.FromCSV(FilePath);
            VacationStatus? founded = vacations.Find(v => v.Id == id);
            if (founded != null)
            {
                vacations.Remove(founded);
                serializer.ToCSV(FilePath, vacations);
            }
        }

        public List<VacationStatus> GetAll()
        {
            return serializer.FromCSV(FilePath);
        }

        public VacationStatus GetById(int id)
        {
            return serializer.FromCSV(FilePath).Where(v => v.Id == id).First();
        }

        public int NextId()
        {
            vacations = serializer.FromCSV(FilePath);
            if (vacations.Count < 1)
            {
                return 1;
            }
            return vacations.Max(t => t.Id) + 1;
        }

        public VacationStatus Save(VacationStatus vacationType)
        {
            vacationType.Id = NextId();
            vacations = serializer.FromCSV(FilePath);
            vacations.Add(vacationType);
            serializer.ToCSV(FilePath, vacations);
            return vacationType;

        }

        public VacationStatus Update(VacationStatus vacationType)
        {
            vacations = serializer.FromCSV(FilePath);
            VacationStatus? cur = vacations.Find(r => r.Id == vacationType.Id);
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
