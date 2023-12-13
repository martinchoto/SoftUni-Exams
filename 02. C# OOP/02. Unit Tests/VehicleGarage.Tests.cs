using NUnit.Framework;
using System.ComponentModel;
using System.Reflection;

namespace VehicleGarage.Tests
{
    public class Tests
    {
        Garage garage;
        Vehicle vehicle;
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestConstructorWorks()
        {
            string brand = "Tesla";
            string model = "GT";
            string license = "PA4490";
            double maxMileage = 130;
            vehicle = new Vehicle(brand, model, license, maxMileage);
            Assert.AreEqual(vehicle.Brand, "Tesla");
            Assert.AreEqual(vehicle.Model, "GT");
            Assert.AreEqual(vehicle.LicensePlateNumber, "PA4490");
            Assert.IsFalse(vehicle.IsDamaged);
            Assert.AreEqual(vehicle.BatteryLevel, 100);
        }
        [Test]
        public void TestGarageConstructor()
        {
            garage = new Garage(3);
            Assert.AreEqual(garage.Capacity, 3);
            Assert.AreEqual(garage.Vehicles.Count, 0);
        }
        [Test]
        public void TestAddVehicle()
        {
            garage = new Garage(3);
            vehicle = new Vehicle("BMW", "M5", "PA8888", 100);
            garage.AddVehicle(vehicle);
            Assert.AreEqual(garage.Vehicles.Count, 1);
            Assert.AreEqual(garage.Vehicles[0], vehicle);
            Assert.IsFalse(garage.AddVehicle(vehicle));
            Vehicle vehicletwo = new Vehicle("BMW", "M5", "1425", 100);
            Vehicle vehiclethree = new Vehicle("BMW", "M5", "1477", 100);
            garage.AddVehicle(vehicletwo);
            garage.AddVehicle(vehiclethree);
            Assert.AreEqual(garage.Capacity, garage.Vehicles.Count);
            Assert.IsFalse(garage.AddVehicle(new Vehicle("GOLF", "FT", "1447", 100)));
            Assert.IsFalse(garage.AddVehicle(new Vehicle("GOLF", "FT", "1477", 100)));
        }
        [Test] public void DriveVehicle()
        {
            garage = new Garage(100);
            string licensePlate = "144";
            vehicle = new Vehicle("BMW", "M3", licensePlate, 100);
            garage.AddVehicle(vehicle);
            garage.AddVehicle(new Vehicle("BMW", "M3", "63", 100));
            Assert.AreEqual(garage.Vehicles.Count, 2);
            garage.DriveVehicle(licensePlate, 40, false);
            garage.DriveVehicle("63", 56, true);
            Assert.IsFalse(garage.Vehicles[0].IsDamaged);
            Assert.IsTrue(garage.Vehicles[1].IsDamaged);
            Assert.AreEqual(garage.Vehicles[0].BatteryLevel, 60);
            Assert.AreEqual(garage.Vehicles[1].BatteryLevel, 44);
            garage.DriveVehicle(garage.Vehicles[1].LicensePlateNumber, 100, false);
            Assert.AreEqual(garage.Vehicles[1].BatteryLevel, 44);
        }
        [Test]
        public void TestRepairMethod()
        {
            garage = new Garage(100);
            garage.AddVehicle(new Vehicle("BMW", "M3", "174", 100));
            garage.AddVehicle(new Vehicle("BMW", "M3", "175", 100));
            garage.AddVehicle(new Vehicle("BMW", "M3", "176", 100));
            garage.Vehicles[0].IsDamaged = true;
            garage.Vehicles[1].IsDamaged = true;
            Assert.AreEqual(garage.Vehicles.Count, 3);
            int vehiclesRepaired = 2;
            Assert.AreEqual(garage.RepairVehicles(), $"Vehicles repaired: {vehiclesRepaired}");
            Assert.IsFalse(garage.Vehicles[0].IsDamaged);
            Assert.IsFalse(garage.Vehicles[1].IsDamaged);
            
        }
        [Test]
        public void ChargeVehicles()
        {
            garage = new Garage(100);
            garage.AddVehicle(new Vehicle("BMW", "M3", "174", 100));
            garage.AddVehicle(new Vehicle("BMW", "M3", "175", 100));
            garage.AddVehicle(new Vehicle("BMW", "M3", "176", 100));
            garage.AddVehicle(new Vehicle("BMW", "M3", "177", 100));
            garage.AddVehicle(new Vehicle("BMW", "M3", "178", 100));
            garage.DriveVehicle("174", 40, false);
            int vehiclesToBeCharged = 1;
            Assert.AreEqual(garage.ChargeVehicles(99), vehiclesToBeCharged);
            Assert.AreEqual(garage.Vehicles[0].BatteryLevel, 100);
            garage.DriveVehicle("174", 40, false);
            garage.DriveVehicle("174", 40, false);
            Assert.AreEqual(garage.ChargeVehicles(0), 0);
        }
    }
}