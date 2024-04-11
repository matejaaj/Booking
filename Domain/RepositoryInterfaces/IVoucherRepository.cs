using BookingApp.Domain.Model.BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IVoucherRepository
    {
        List<Voucher> GetAll();


        Voucher GetById(int voucherId);


        Voucher Save(Voucher voucher);


        void Delete(Voucher voucher);


        Voucher Update(Voucher voucher);

        List<Voucher> GetVouchersByTouristId(int touristId);
    }
}
