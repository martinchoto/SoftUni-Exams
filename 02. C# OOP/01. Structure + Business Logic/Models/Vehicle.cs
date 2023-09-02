using EDriveRent.Models.Contracts;
using EDriveRent.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDriveRent.Models
{
    public abstract class Vehicle : IVehicle
    {
        protected Vehicle(string brand, string model, double maxMileage, string licensePlateNumber) 
        { 
            Brand = brand;
            Model = model;
            MaxMileage = maxMileage;
            LicensePlateNumber = licensePlateNumber;
            BatteryLevel = 100;
            IsDamaged = false;
        }
        private string brand;
        public string Brand
        {
            get { return brand; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(ExceptionMessages.BrandNull);
                }
                brand = value;
            }
        }
        private string model;
        public string Model
        {
            get { return model; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(ExceptionMessages.ModelNull);
                }
                model = value;
            }
        }

        public double MaxMileage {get; private set;}
        private string licensePlateNumber;
        public string LicensePlateNumber 
        {
            get { return licensePlateNumber; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(ExceptionMessages.LicenceNumberRequired);
                }
                licensePlateNumber = value;
            }
        }

        public int BatteryLevel { get; private set;}

        public bool IsDamaged {get; private set;}

        public void ChangeStatus()
        {
            IsDamaged = !IsDamaged;
        }

        public void Drive(double mileage)
        {
            int calculateDistancePercent = (int)Math.Round(mileage / MaxMileage * 100);
            if (GetType().Name == nameof(CargoVan))
            {
                BatteryLevel -= calculateDistancePercent + 5;
            }
            else if (GetType().Name == nameof(PassengerCar)) 
            {
                BatteryLevel -= calculateDistancePercent;
            }
        }

        public void Recharge()
        {
            this.BatteryLevel = 100;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{Brand} {Model} License plate: {LicensePlateNumber} Battery: {BatteryLevel}% Status: ");
            if (IsDamaged)
            {
                sb.Append("damaged");
            }
            else
            {
                sb.Append("OK");
            }
            sb.AppendLine();
            return sb.ToString().Trim();
        }
    }
}
