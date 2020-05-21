namespace DistanceSpeedTimeCalculator
{
    public class DistanceSpeedTime
    {
        public double DistanceSpeedCalculator(double distance, double speed, EUnitsOfLength eUnitsOfLength = EUnitsOfLength.M, EUnitOfTime eUnitOfTime = EUnitOfTime.SECONDS)
        {
            return (distance / ((speed * (double)eUnitsOfLength) / (double)eUnitOfTime));
        }
    }
}
