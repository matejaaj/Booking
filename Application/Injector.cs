﻿using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application
{
    public class Injector
    {
        private static Dictionary<Type, object> _implementations = new Dictionary<Type, object>
    {
        { typeof(IUserRepository), new UserRepository() },
        { typeof(ITourRepository), new TourRepository() },
        { typeof(IVehicleRepository), new VehicleRepository() },
        { typeof(ITourInstanceRepository), new TourInstanceRepository() },
        { typeof(ITourReservationRepository), new TourReservationRepository() },
        { typeof(ITourGuestRepository), new TourGuestRepository() },
        { typeof(IDriveReservationRepository), new DriveReservationRepository() },
        { typeof(ILanguageRepository), new LanguageRepository() },
        { typeof(ILocationRepository), new LocationRepository() },
        { typeof(IImageRepository), new ImageRepository() },
        { typeof(ICheckpointRepository), new CheckpointRepository() },
        { typeof(IDetailedLocationRepository), new DetailedLocationRepository() },
        { typeof(IAccommodationRepository), new AccommodationRepository() },
        { typeof(IAccommodationReservationRepository), new AccommodationReservationRepository() },
        { typeof(IGuestRatingRepository), new GuestRatingRepository() },
        { typeof(IAccommodationAndOwnerRatingRepository), new AccommodationAndOwnerRatingRepository() },
        { typeof(IOwnerRepository), new OwnerRepository() },
        { typeof(IVoucherRepository), new VoucherRepository() },
        { typeof(ITourReviewRepository), new TourReviewRepository() },
        { typeof(IReservationModificationRequestRepository), new ReservationModificationRequestRepository() },
        { typeof(IGroupDriveReservationRepository), new GroupDriveReservationRepository() },
        { typeof(ISuperDriverStateRepository), new SuperDriverStateRepository() },
        { typeof(ITourRequestSegmentRepository), new TourRequestSegmentRepository()},
        { typeof(IPrivateTourGuestRepository), new PrivateTourGuestRepository()},
        { typeof(ITourRequestRepository), new TourRequestRepository() },
        { typeof(IDriverUnreliableReportRepository), new DriverUnreliableReportRepository() },
        { typeof(IRenovationRepository), new RenovationRepository() },
        { typeof(IDriverOnVacationRepository), new DriverOnVacationReposiroty() },
        { typeof(IVacationStatusRepository), new VacationStatusRepository() },
        { typeof(IVacationTypeRepository), new VacationTypeRepository() },
        { typeof(ILocationStateRepository), new LocationStateRepository() },
        { typeof(IRenovationRecommendationRepository), new RenovationRecommendationRepository()},
        { typeof(ISuperGuestRepository), new SuperGuestRepository()},
        { typeof(INotificationRepository), new NotificationRepository()},
        { typeof(IForumRepository), new ForumRepository()},
        { typeof(IForumCommentRepository), new ForumCommentRepository()},
        { typeof(ISuperGuideRepository), new SuperGuideRepository()}
    };
        
        public static T CreateInstance<T>()
        {
            Type type = typeof(T);

            if (_implementations.ContainsKey(type))
            {
                return (T)_implementations[type];
            }

            throw new ArgumentException($"No implementation found for type {type}");
        }

        public static void init()
        {
            foreach (var item in _implementations)
            {
                Console.WriteLine(item);
            }
        }
    }
}
