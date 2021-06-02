using System;
using VechiclesTrafficFee.Models.Vehicles;

namespace VechiclesTraffic.Production
{
    public interface ITollCalculator
    {
        int GetTollFee(VehicleBase vehicle, DateTime[] dates);
    }
}
