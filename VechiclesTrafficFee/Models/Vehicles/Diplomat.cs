namespace VechiclesTrafficFee.Models.Vehicles
{
    public class Diplomat : VehicleBase
    {
        public override bool IsTollFreeVehicle()
        {
            return true;
        }
    }
}
