namespace VechiclesTrafficFee.Models.Vehicles
{
    public class Foreign : VehicleBase
    {
        public override bool IsTollFreeVehicle()
        {
            return true;
        }
    }
}
