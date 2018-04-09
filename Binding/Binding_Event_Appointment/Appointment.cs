using System;

namespace Binding.Event.Appointment
{
    public class Appointment
    {
        string _eventDescription = String.Empty;
        DateTime _eventDate = DateTime.MinValue;

        public String EventDescription
        {
            get { return _eventDescription; }
            set 
            { 
                _eventDescription = value; 

            }
        }

        public DateTime EventDate
        {
            get { return _eventDate; }
            set 
            {
                if ((value.DayOfWeek == DayOfWeek.Sunday) || (value.DayOfWeek == DayOfWeek.Saturday))
                {
                    throw new ArgumentException("The date must be on a weekday");
                }
                _eventDate = value; 
            }
        }

    }
}
