namespace VechiclesTrafficFee.Models.Vehicles
{
    public class Military : VehicleBase
    {
        public override bool IsTollFreeVehicle()
        {
            return true;
        }
    }
}
