namespace VechiclesTrafficFee.Models.Vehicles
{
    public class Emergency : VehicleBase
    {
        public override bool IsTollFreeVehicle()
        {
            return true;
        }
    }
}
