using NUnit.Framework;
using System;
using VechiclesTraffic.Production;
using VechiclesTrafficFee.Models.Vehicles;

namespace TestVechicleTrafficFee
{
    public class Tests
    {
        private ITollCalculator _tollCalculator;

        [SetUp]
        public void Setup()
        {
            _tollCalculator = new TollCalculator();
        }

        [Test(Description = "Check 1 time fee")]
        public void OneTimeFee()
        {
            VehicleBase vehicle = new Car();

            DateTime[] dateTimes = new DateTime[]
            {
                new DateTime(2021,5, 25, 14, 20, 0)
            };

            var result = _tollCalculator.GetTollFee(vehicle, dateTimes);

            Assert.AreEqual(9, result);
        }

        [Test(Description = "Check total of 3 times fee")]
        public void TotalFee()
        {
            VehicleBase vehicle = new Car();

            DateTime[] dateTimes = new DateTime[]
            {
                new DateTime(2021,5, 25, 14, 20, 0),
                new DateTime(2021,5, 25, 16, 20, 0),
                new DateTime(2021,5, 25, 17, 34, 0)
            };

            var result = _tollCalculator.GetTollFee(vehicle, dateTimes);

            Assert.AreEqual(47, result);
        }

        [Test(Description = "Check 3, but 2 time fee (less than hour)")]
        public void LessThanHour()
        {
            VehicleBase vehicle = new Car();

            DateTime[] dateTimes = new DateTime[]
            {
                new DateTime(2021,5, 25, 14, 20, 0),
                new DateTime(2021,5, 25, 16, 20, 0),
                new DateTime(2021,5, 25, 17, 0, 0)
            };

            var result = _tollCalculator.GetTollFee(vehicle, dateTimes);

            Assert.AreEqual(31, result);
        }

        [Test(Description = "Check fee for all time intervals")]
        [TestCase("2021-06-02 06:00:00", ExpectedResult = 9)]
        [TestCase("2021-06-02 06:30:00", ExpectedResult = 16)]
        [TestCase("2021-06-02 07:59:00", ExpectedResult = 22)]
        [TestCase("2021-06-02 08:29:00", ExpectedResult = 16)]
        [TestCase("2021-06-02 08:30:00", ExpectedResult = 9)]
        [TestCase("2021-06-02 15:00:00", ExpectedResult = 16)]
        [TestCase("2021-06-02 15:30:00", ExpectedResult = 22)]
        [TestCase("2021-06-02 17:00:00", ExpectedResult = 16)]
        [TestCase("2021-06-02 18:00:00", ExpectedResult = 9)]
        [TestCase("2021-06-02 18:30:00", ExpectedResult = 0)]
        public int AllIntervals(string time)
        {
            VehicleBase vehicle = new Car();

            DateTime dateTime = DateTime.Parse(time);

            var result = _tollCalculator.GetTollFee(vehicle, new DateTime[] { dateTime });

            return result;
        }

        [Test(Description = "Check max fee")]
        public void MaxFee()
        {
            VehicleBase vehicle = new Car();

            DateTime[] dateTimes = new DateTime[]
            {
                new DateTime(2021, 5, 25, 7, 20, 0), // 22
                new DateTime(2021, 5, 25, 8, 21, 0), // 9
                new DateTime(2021, 5, 25, 15, 20, 0), // 16
                new DateTime(2021, 5, 25, 17, 10, 0), // 16
                new DateTime(2021, 5, 25, 18, 20, 0) // 9
            };

            var result = _tollCalculator.GetTollFee(vehicle, dateTimes);

            Assert.AreEqual(60, result);
        }

        [Test(Description = "Toll-free vehicle types"), TestCaseSource("TypesOfVehicles")]
        public void FreeVehicleTypes(VehicleBase tollFreeVehicle)
        {
            DateTime[] dateTimes = new DateTime[]
            {
                new DateTime(2013, 6, 7, 14, 20, 0)
            };

            var result = _tollCalculator.GetTollFee(tollFreeVehicle, dateTimes);

            Assert.AreEqual(0, result);
        }

        static VehicleBase[] TypesOfVehicles =
        {
            new Diplomat(),
            new Emergency(),
            new Foreign(),
            new Military(),
            new Motorbike(),
            new Tractor()
        };
    }
}