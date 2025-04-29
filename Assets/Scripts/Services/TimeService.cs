using System;
using DuckFriend.Data;

namespace DuckFriend.Services
{
    /// <summary>
    /// Service for daily rollover and check-in logic.
    /// </summary>
    public static class TimeService
    {
        /// <summary>
        /// Checks if today is a new day compared to last check-in.
        /// </summary>
        public static bool IsNewDay(string lastCheckInISO)
        {
            if (!DateTime.TryParse(lastCheckInISO, out var lastCheckIn))
            {
                return true;
            }
            var lastDate = lastCheckIn.Date;
            var today = DateTime.Now.Date;
            return today > lastDate;
        }

        /// <summary>
        /// Checks if the user has already checked in today.
        /// </summary>
        public static bool HasCheckedInToday(string lastCheckInISO)
        {
            if (!DateTime.TryParse(lastCheckInISO, out var lastCheckIn))
            {
                return false;
            }
            return lastCheckIn.Date == DateTime.Now.Date;
        }

        /// <summary>
        /// Registers a daily check-in by updating the last check-in date in user data.
        /// </summary>
        public static void RegisterCheckIn(UserData data)
        {
            data.lastCheckInDate = DateTime.Now.ToString("o");
        }
    }
} 