﻿using BookingApp.Domain.Model.BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class VoucherRepository : IVoucherRepository
    {
        private const string FilePath = "../../../Resources/Data/vouchers.csv";
        private readonly Serializer<Voucher> _serializer;
        private List<Voucher> _vouchers;

        public VoucherRepository()
        {
            _serializer = new Serializer<Voucher>();
            _vouchers = _serializer.FromCSV(FilePath);

            bool hasExpiredVouchers = _vouchers.Any(voucher => voucher.ExpiryDate < DateTime.Now);
            if (hasExpiredVouchers)
            {
                _vouchers = _vouchers.Where(voucher => voucher.ExpiryDate >= DateTime.Now).ToList();
                _serializer.ToCSV(FilePath, _vouchers); 
            }
        }

        public List<Voucher> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public Voucher GetById(int voucherId)
        {
            _vouchers = _serializer.FromCSV(FilePath);
            return _vouchers.FirstOrDefault(voucher => voucher.Id == voucherId);
        }

        public Voucher Save(Voucher voucher)
        {
            voucher.Id = NextId();
            _vouchers = _serializer.FromCSV(FilePath);
            _vouchers.Add(voucher);
            _serializer.ToCSV(FilePath, _vouchers);
            return voucher;
        }

        public void Delete(int id)
        {
            _vouchers = _serializer.FromCSV(FilePath);
            Voucher found = _vouchers.Find(v => v.Id == id);
            if (found != null)
            {
                _vouchers.Remove(found);
                _serializer.ToCSV(FilePath, _vouchers);
            }
        }

        public Voucher Update(Voucher voucher)
        {
            _vouchers = _serializer.FromCSV(FilePath);
            Voucher current = _vouchers.Find(v => v.Id == voucher.Id);
            if (current != null)
            {
                int index = _vouchers.IndexOf(current);
                _vouchers[index] = voucher;
                _serializer.ToCSV(FilePath, _vouchers);
            }
            return voucher;
        }

        private int NextId()
        {
            _vouchers = _serializer.FromCSV(FilePath);
            if (_vouchers.Count < 1)
            {
                return 1;
            }
            return _vouchers.Max(v => v.Id) + 1;
        }

        public List<Voucher> GetVouchersByTouristId(int touristId)
        {
            return _vouchers.Where(voucher => voucher.TouristId == touristId).ToList();
        }
    }
}
