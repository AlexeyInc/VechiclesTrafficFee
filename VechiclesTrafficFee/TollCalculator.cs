using System;
using VechiclesTrafficFee.Helpers;
using VechiclesTrafficFee.Models.Vehicles;

namespace VechiclesTraffic.Production
{
    public class TollCalculator : ITollCalculator
    {

        private const int MAX_DAY_FEE = 60;

        private const int ONE_HOUR_IN_MINUTES = 60;

        /**
         * Calculate the total toll fee for one day
         *
         * @param vehicle - the vehicle
         * @param dates   - date and time of all passes on one day
         * @return - the total toll fee for that day
         */

        public int GetTollFee(VehicleBase vehicle, DateTime[] dates)
        {
            if (vehicle == null || vehicle.IsTollFreeVehicle())
                return 0;

            DateTime prevDateTime = dates[0];
            int totalFee = 0;

            foreach (DateTime curDateTime in dates)
            {
                int nextFee = GetTollFeeByDate(curDateTime);
                int tempFee = GetTollFeeByDate(prevDateTime);

                var differenceInMinutes = curDateTime.GetTimeInMinutes() - prevDateTime.GetTimeInMinutes();

                if (differenceInMinutes <= ONE_HOUR_IN_MINUTES)
                {
                    if (totalFee > 0) totalFee -= tempFee;
                    if (nextFee >= tempFee) tempFee = nextFee;
                    totalFee += tempFee;
                }
                else
                {
                    totalFee += nextFee;
                }

                prevDateTime = curDateTime;
            }

            if (totalFee > MAX_DAY_FEE) 
                totalFee = MAX_DAY_FEE;

            return totalFee;
        }

        private int GetTollFeeByDate(DateTime date)
        {
            if (date.IsTollFreeDate())
                return 0;

            int resultFee = GetTollFeeByTime(date.TimeOfDay);

            return resultFee;
        }

        private int GetTollFeeByTime(TimeSpan time)
        {
            switch (time)
            {
                case TimeSpan t when new TimeSpan(6, 0, 0) <= t && t <= new TimeSpan(6, 29, 0):
                    return 9;
                case TimeSpan t when new TimeSpan(6, 30, 0) <= t && t <= new TimeSpan(6, 59, 0):
                    return 16;
                case TimeSpan t when new TimeSpan(7, 0, 0) <= t && t <= new TimeSpan(7, 59, 0):
                    return 22;
                case TimeSpan t when new TimeSpan(8, 0, 0) <= t && t <= new TimeSpan(8, 29, 0):
                    return 16;
                case TimeSpan t when new TimeSpan(8, 30, 0) <= t && t <= new TimeSpan(14, 59, 0):
                    return 9;
                case TimeSpan t when new TimeSpan(15, 0, 0) <= t && t <= new TimeSpan(15, 29, 0):
                    return 16;
                case TimeSpan t when new TimeSpan(15, 30, 0) <= t && t <= new TimeSpan(16, 59, 0):
                    return 22;
                case TimeSpan t when new TimeSpan(17, 0, 0) <= t && t <= new TimeSpan(17, 59, 0):
                    return 16;
                case TimeSpan t when new TimeSpan(18, 0, 0) <= t && t <= new TimeSpan(18, 29, 0):
                    return 9;
                case TimeSpan t when new TimeSpan(18, 30, 0) <= t && t <= new TimeSpan(5, 59, 0):
                    return 0;
                default:
                    return 0;
            }
        }
    }
}
