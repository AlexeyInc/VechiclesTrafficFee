namespace VechiclesTrafficFee.Models.Vehicles
{
    public class Car : VehicleBase
    {
        public override bool IsTollFreeVehicle()
        {
            return false;
        }
    }
}
