using BookingApp.Domain.Model.BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.UseCases
{
    public class VoucherService
    {
        private IVoucherRepository _voucherRepository;

        public VoucherService()
        {
            _voucherRepository = Injector.CreateInstance<IVoucherRepository>();
        }

        public List<Voucher> GetAll()
        {
            return _voucherRepository.GetAll();
        }

        public Voucher GetById(int id)
        {
            return _voucherRepository.GetById(id);
        }

        public Voucher Save(Voucher voucher)
        {
            return _voucherRepository.Save(voucher);
        }

        public void Delete(int id)
        {
            _voucherRepository.Delete(id);
        }

        public Voucher Update(Voucher voucher)
        {
            return _voucherRepository.Update(voucher);
        }

        public List<Voucher> GetVouchersByTouristId(int id)
        {
            return _voucherRepository.GetVouchersByTouristId(id);
        }
    }
}
