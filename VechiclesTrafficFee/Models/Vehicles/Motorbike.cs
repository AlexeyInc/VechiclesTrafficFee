namespace VechiclesTrafficFee.Models.Vehicles
{
    public class Motorbike : VehicleBase
    {
        public override bool IsTollFreeVehicle()
        {
            return true;
        }
    }
}
