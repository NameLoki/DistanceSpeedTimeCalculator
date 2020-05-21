using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistanceSpeedTimeCalculator
{
    public class TimeCalculator : ITimeCalculator
    {
        private readonly byte startBusHours;
        private readonly byte startBusMinutes;
        private readonly byte endBusHouers ;
        private readonly byte endBusMinutes;

        public TimeCalculator(byte startBusHours, byte startBusMinutes, byte endBusHouers, byte endBusMinutes)
        {
            this.startBusHours = startBusHours;
            this.startBusMinutes = startBusMinutes;
            this.endBusHouers = endBusHouers;
            this.endBusMinutes = endBusMinutes;
        }

        public TimeCalculator() : this(6, 0, 21, 56) { }

        public DateTime AddTime(DateTime dateTime, double time, EUnitOfTime eUnitOfTime = EUnitOfTime.HOURS)
        {
            DateTime result = dateTime;

            try
            {
                if(eUnitOfTime == EUnitOfTime.HOURS)
                {
                    result += TimeSpan.FromHours(time);
                }
                else if(eUnitOfTime == EUnitOfTime.MINUTES)
                {
                    result += TimeSpan.FromMinutes(time);
                }
                else if(eUnitOfTime == EUnitOfTime.SECONDS)
                {
                    result += TimeSpan.FromSeconds(time);
                }
            }
            catch (OverflowException)
            {

            }
            catch (ArgumentOutOfRangeException)
            {

            }
            catch (ArgumentException)
            {

            }

            return result;
        }

        public DateTime GetDistanceTime(double distanceTime, EUnitOfTime eUnitOfTime = EUnitOfTime.HOURS)
        {
            DateTime nowTime = DateTime.Now;

            AddTime(nowTime, distanceTime, eUnitOfTime);
            DateTime? busTime = null;

            try
            {
                busTime = new DateTime(nowTime.Year, nowTime.Month, nowTime.Day, startBusHours, startBusMinutes, 0);

                if(endBusHouers < nowTime.Hour)
                {
                    if(endBusMinutes < nowTime.Minute)
                    {
                        return busTime.GetValueOrDefault();
                    }
                }

                while(true)
                {
                    if(nowTime < busTime)
                    {
                        break;
                    }

                    busTime += TimeSpan.FromMinutes(21);
                }
            } 
            catch (ArgumentOutOfRangeException)
            {

            }
            return busTime.GetValueOrDefault();
        }
    }

    public interface ITimeCalculator
    {
        DateTime AddTime(DateTime dateTime, double time, EUnitOfTime eUnitOfTime = EUnitOfTime.HOURS);
        DateTime GetDistanceTime(double distanceTime, EUnitOfTime eUnitOfTime = EUnitOfTime.HOURS);
    }
}
