using BookingApp.Application;
using BookingApp.Application.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Domain.Model;

namespace BookingApp.LogicServices.Driver
{
    internal class SuperDriverService
    {
        private static readonly SuperDriverStateService superDriverStateService = new SuperDriverStateService();

        public static bool UpdateStateForDriver(int DriverId)
        {
            SuperDriverState? curr = superDriverStateService.Get(DriverId);
            bool res = false;
            if (curr == null)
            {
                curr = new SuperDriverState
                {
                    DriverID = DriverId,
                    StateOfPoints = 0,
                    IsSuper = 0,
                    NumberOfDrives = 1,
                    DateOfGettingStatus = null,
                    DateOfEndingOfStatus = null,
                };
                superDriverStateService.Update(curr);
            }
            else
            {
                if (curr.IsSuper == 1)
                {
                    curr.StateOfPoints += 1;
                    res = false;
                    curr.NumberOfDrives += 1;
                }
                else
                {
                    curr.NumberOfDrives += 1;
                    res = CheckForStatusUpdate(curr);
                }
                superDriverStateService.Update(curr);
            }
            return res;
        }

        public static Boolean CheckForStatusUpdate(SuperDriverState superDriverState)
        {
            if (superDriverState != null && superDriverState.NumberOfDrives >= 15)
            {
                DateTime dt = System.DateTime.Today;
                superDriverState.IsSuper = 1;
                superDriverState.DateOfGettingStatus = DateOnly.FromDateTime(dt);
                superDriverState.DateOfEndingOfStatus = DateOnly.FromDateTime(dt).AddYears(1);
                superDriverStateService.Update(superDriverState);
                return true;
            }
            return false;
        }


        public static bool IsReadyForBonus(int DriverId)
        {
            SuperDriverState? superDriverState = superDriverStateService.Get(DriverId);
            if (superDriverState != null && superDriverState.IsSuper == 1 && superDriverState.StateOfPoints == 50)
                return true;
            return false;
        }

        public static bool CanceledResevationByDriver(int DriverId)
        {
            bool res = false;
            SuperDriverState? superDriverState = superDriverStateService.Get(DriverId);
            if (superDriverState != null)
            {
                superDriverState.StateOfPoints -= 5;
                if (superDriverState.StateOfPoints <= 0)
                {
                    superDriverState.NumberOfDrives = 0;
                    superDriverState.IsSuper = 0;
                    superDriverState.StateOfPoints = 0;
                    superDriverState.DateOfGettingStatus = null;
                    superDriverState.DateOfEndingOfStatus = null;
                    res = true;
                }
                superDriverStateService.Update(superDriverState);
            }
            return res;
        }
    }
}
