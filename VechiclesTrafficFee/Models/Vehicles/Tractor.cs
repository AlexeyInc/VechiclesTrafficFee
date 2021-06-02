namespace VechiclesTrafficFee.Models.Vehicles
{
    public class Tractor : VehicleBase
    {
        public override bool IsTollFreeVehicle()
        {
            return true;
        }
    }
}
