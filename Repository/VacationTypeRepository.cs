using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    class VacationTypeRepository : IVacationTypeRepository
    {
        private const string FilePath = "../../../Resources/Data/vacationtype.csv";
        private readonly Serializer<VacationType> serializer;
        private List<VacationType> vacations;

        public VacationTypeRepository()
        {
            serializer = new Serializer<VacationType>();
            vacations = serializer.FromCSV(FilePath);
        }
        public void Delete(int id)
        {
            vacations = serializer.FromCSV(FilePath);
            VacationType? founded = vacations.Find(v => v.Id == id);
            if (founded != null)
            {
                vacations.Remove(founded);
                serializer.ToCSV(FilePath, vacations);
            }
        }

        public List<VacationType> GetAll()
        {
            return serializer.FromCSV(FilePath);
        }

        public VacationType GetById(int id)
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

        public VacationType Save(VacationType vacationType)
        {
            vacationType.Id = NextId();
            vacations = serializer.FromCSV(FilePath);
            vacations.Add(vacationType);
            serializer.ToCSV(FilePath, vacations);
            return vacationType;

        }

        public VacationType Update(VacationType vacationType)
        {
            vacations = serializer.FromCSV(FilePath);
            VacationType? cur = vacations.Find(r => r.Id == vacationType.Id);
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
