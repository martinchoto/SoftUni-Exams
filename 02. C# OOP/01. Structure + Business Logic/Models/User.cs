using EDriveRent.Models.Contracts;
using EDriveRent.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDriveRent.Models
{
    public class User : IUser
    {
        public User(string firstName, string lastName, string drivingLicenceNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            DrivingLicenseNumber = drivingLicenceNumber;
            Rating = 0;
            IsBlocked = false;
        }
        private string firstName;
        public string FirstName
        {
            get { return firstName; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.FirstNameNull);
                }
                firstName = value;
            }
        }
        private string lastName;
        public string LastName
        {
            get { return lastName; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.LastNameNull);
                }
                lastName = value;
            }
        }

        public double Rating {get; private set;}
        private string drivingLicenseNumber;
        public string DrivingLicenseNumber 
        {
            get { return drivingLicenseNumber; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.DrivingLicenseRequired);
                }
                drivingLicenseNumber = value;
            }
        }

        public bool IsBlocked {get; private set;}

        public void DecreaseRating()
        {
            Rating -= 2;
            if (Rating < 0)
            {
                Rating = 0;
                IsBlocked = true;
            }
        }

        public void IncreaseRating()
        {
            Rating += 0.5;
            if (Rating > 10)
            {
                Rating = 10;
            }
        }
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"{FirstName} {LastName} Driving license: {DrivingLicenseNumber} Rating: {Rating}");
            return stringBuilder.ToString().Trim();
        }
    }
}
