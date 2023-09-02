using EDriveRent.Core.Contracts;
using EDriveRent.Models;
using EDriveRent.Models.Contracts;
using EDriveRent.Repositories;
using EDriveRent.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace EDriveRent.Core
{
    public class Controller : IController
    {
        private UserRepository users;
        private RouteRepository routes;
        private VehicleRepository vehicles;
        public Controller() 
        {
            users = new UserRepository();
            routes = new RouteRepository();
            vehicles = new VehicleRepository();
        }
        public string AllowRoute(string startPoint, string endPoint, double length)
        {
            var routeOne = routes.GetAll().FirstOrDefault(x => x.StartPoint == startPoint
            && x.EndPoint == endPoint && x.Length == length);
            if (routeOne != null)
            {
                return string.Format(OutputMessages.RouteExisting, startPoint, endPoint, length);
            }
            var routeTwo = routes.GetAll().FirstOrDefault(x => x.StartPoint == startPoint
            && x.EndPoint == endPoint && x.Length < length);
            if (routeTwo != null)
            {
                return string.Format(OutputMessages.RouteIsTooLong, startPoint, endPoint);
            }
            var routeThree = routes.GetAll().FirstOrDefault(x => x.StartPoint == startPoint
            && x.EndPoint == endPoint && x.Length > length);
            if (routeThree != null)
            {
                routeThree.LockRoute();
            }
            routes.AddModel(new Route(startPoint, endPoint, length, routes.GetAll().Count + 1));
            return string.Format(OutputMessages.NewRouteAdded, startPoint, endPoint, length);
        }

        public string MakeTrip(string drivingLicenseNumber, string licensePlateNumber, string routeId, bool isAccidentHappened)
        {
            var user = users.FindById(drivingLicenseNumber);
            var car = vehicles.FindById(licensePlateNumber);
            var route = routes.FindById(routeId);
            if (user.IsBlocked == true)
            {
                return string.Format(OutputMessages.UserBlocked, drivingLicenseNumber);
            }
            if (car.IsDamaged == true)
            {
                return string.Format(OutputMessages.VehicleDamaged, licensePlateNumber);
            }
            if (route.IsLocked == true)
            {
                return string.Format(OutputMessages.RouteLocked, routeId);
            }
            car.Drive(route.Length);
            if (isAccidentHappened)
            {
                user.DecreaseRating();
                car.ChangeStatus();
            }
            else
            {
                user.IncreaseRating();
            }
            return car.ToString();
        }

        public string RegisterUser(string firstName, string lastName, string drivingLicenseNumber)
        {
            var driver = users.FindById(drivingLicenseNumber);
            if (driver != null)
            {
                return string.Format(OutputMessages.UserWithSameLicenseAlreadyAdded, drivingLicenseNumber);
            }
            users.AddModel(new User(firstName, lastName, drivingLicenseNumber));
            return string.Format(OutputMessages.UserSuccessfullyAdded, firstName, lastName, drivingLicenseNumber);
        }

        public string RepairVehicles(int count)
        {
            var orderedVehicles = vehicles.GetAll().Where(x => x.IsDamaged == true)
                .OrderBy(x => x.Brand).ThenBy(x => x.Model).ToList();
            int repairedVehicles = 0;
            if (count < orderedVehicles.Count)
            {
                for (int i = 0; i < count; i++)
                {
                    orderedVehicles[i].ChangeStatus();
                    orderedVehicles[i].Recharge();
                    repairedVehicles++;
                }
            }
            else
            {
                foreach (var vehicle in orderedVehicles)
                {
                    vehicle.ChangeStatus();
                    vehicle.Recharge();
                    repairedVehicles++;
                }
            }
            return string.Format(OutputMessages.RepairedVehicles, repairedVehicles);
        }

        public string UploadVehicle(string vehicleType, string brand, string model, string licensePlateNumber)
        {
            if (vehicleType != nameof(CargoVan) && vehicleType != nameof(PassengerCar))
            {
                return string.Format(OutputMessages.VehicleTypeNotAccessible, vehicleType);
            }
            var car = vehicles.FindById(licensePlateNumber);
            if (car != null)
            {
                return string.Format(OutputMessages.LicensePlateExists, licensePlateNumber);
            }
            IVehicle vehicle = null;
            if (vehicleType == nameof(CargoVan))
            {
                vehicle = new CargoVan(brand, model, licensePlateNumber);
            }
            else if (vehicleType == nameof(PassengerCar))
            {
                vehicle = new PassengerCar(brand, model, licensePlateNumber);
            }
            vehicles.AddModel(vehicle);
            return string.Format(OutputMessages.VehicleAddedSuccessfully, brand, model, licensePlateNumber);
        }

        public string UsersReport()
        {
            var orderedUsers = users.GetAll().OrderByDescending(x => x.Rating).ThenBy(x => x.LastName).ThenBy(x => x.FirstName)
                .ToList();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("*** E-Drive-Rent ***");
            foreach (var users in orderedUsers)
            {
                sb.AppendLine(users.ToString());
            }
            return sb.ToString().Trim();
        }
    }
}
