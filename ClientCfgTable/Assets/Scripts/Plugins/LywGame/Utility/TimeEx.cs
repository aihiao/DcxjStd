using System;
using UnityEngine;

namespace LywGames
{
    // <summary>
    /// UnityEngin.Time.realtimeSinceStartup was not updated when system locked on iOS, use System.DateTime instead
    /// </summary>
    public static class TimeEx
    {
        private static bool initialized = false;
        private static DateTime startUpTime;

        public static void Initialize()
        {
            if (!initialized)
            {
                startUpTime = DateTime.Now.AddMilliseconds(-Time.realtimeSinceStartup * 1000);
            }
            initialized = true;

            LoggerManager.Instance.Info("TimeZoneOffset ", LocalZoneOffset);
        }

        public static float RealTimeSinceStartUp
        {
            get
            {
                if (initialized == false)
                {
                    LoggerManager.Instance.Error("TimeEx has not been initialized");
                    return 0;
                }
                return (float)((DateTime.Now - startUpTime).TotalSeconds);
            }
        }

        public static int UnityTime()
        {
            return (int)(Time.time * 1000);
        }

        public static int LocalZoneOffset
        {
            get { return TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now).Hours; }
        }

        public static DateTime ToUTCDateTime(long time)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            DateTime utcDate = origin.AddMilliseconds(time);
            return utcDate;
        }

        public static DateTime ToLocalDataTime(long time)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            DateTime utcDate = origin.AddMilliseconds(time);
            return utcDate.ToLocalTime();
        }

        public static long DateTimeToInt64(DateTime dateTime)
        {
            DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            return (dateTime.Ticks - epoch.Ticks) / TimeSpan.TicksPerMillisecond;
        }

        public const int cMillisecondInSecend = 1000;
        public const int cSecondInMinute = 60;
        public const int cMinuteInHour = 60;
        public const int cHourInDay = 24;
        public const int cMillisecondInMinute = cMillisecondInSecend * cSecondInMinute;
        public const int cMillisecondInHour = cMillisecondInMinute * cMinuteInHour;
        public const int cMillisecondInDay = cMillisecondInHour * cHourInDay;

        // Should always be same with ClientServerCommon._TimeDurationType
        public class TimeDurationType
        {
            public const int Unknown = 0;
            public const int Era = 1;
            public const int Year = 2;
            public const int Month = 3;
            public const int Day = 4;
            public const int Hour = 5;
            public const int Minute = 6;
            public const int Second = 7;
            public const int Week = 8;
        }

        /// <summary>
		/// Check if time is between times
		/// </summary>
		public static bool IsInTimeSpan(DateTime time, DateTime from, DateTime to, int timeDurationType)
        {
            long ticks, fromTicks, toTicks;
            switch (timeDurationType)
            {
                case TimeDurationType.Year:
                    ticks = ((int)time.DayOfYear) * cMillisecondInDay + time.TimeOfDay.Ticks;
                    fromTicks = ((int)from.DayOfYear) * cMillisecondInDay + from.TimeOfDay.Ticks;
                    toTicks = ((int)to.DayOfYear) * cMillisecondInDay + to.TimeOfDay.Ticks;
                    break;

                case TimeDurationType.Month:
                    ticks = ((int)time.Day) * cMillisecondInDay + time.TimeOfDay.Ticks;
                    fromTicks = ((int)from.Day) * cMillisecondInDay + from.TimeOfDay.Ticks;
                    toTicks = ((int)to.Day) * cMillisecondInDay + to.TimeOfDay.Ticks;
                    break;

                case TimeDurationType.Day:
                    ticks = time.TimeOfDay.Ticks;
                    fromTicks = from.TimeOfDay.Ticks;
                    toTicks = to.TimeOfDay.Ticks;
                    break;

                case TimeDurationType.Week:
                    ticks = ((int)time.DayOfWeek) * cMillisecondInDay + time.TimeOfDay.Ticks;
                    fromTicks = ((int)from.DayOfWeek) * cMillisecondInDay + from.TimeOfDay.Ticks;
                    toTicks = ((int)to.DayOfWeek) * cMillisecondInDay + to.TimeOfDay.Ticks;
                    break;

                default:
                    ticks = time.Ticks;
                    fromTicks = from.Ticks;
                    toTicks = to.Ticks;
                    break;
            }

            return ticks > fromTicks && ticks <= toTicks;
        }

        public static bool IsInSameTimeSpan(DateTime time1, DateTime time2, DateTime from, DateTime to, int timeDurationType)
        {
            if (IsInTimeSpan(time1, from, to, timeDurationType) == false || IsInTimeSpan(time2, from, to, timeDurationType) == false)
                return false;

            var fromTime1 = GetTimeBeforeTime(from, time1, timeDurationType);
            var toTime1 = GetTimeAfterTime(to, time1, timeDurationType);
            return time2 > fromTime1 && time2 <= toTime1;
        }

        /// <summary>
        /// Get previous time by duration.
        /// </summary>
        public static DateTime GetTimeBeforeTime(DateTime time, DateTime before, int timeDurationType)
        {
            switch (timeDurationType)
            {
                case TimeDurationType.Year:
                    time = new DateTime(before.Year, before.Month, before.Day).AddTicks(time.TimeOfDay.Ticks).AddDays(time.DayOfYear - before.DayOfYear);
                    if (time.Ticks > before.Ticks)
                        time = time.AddYears(-1);
                    break;

                case TimeDurationType.Month:
                    time = new DateTime(before.Year, before.Month, before.Day).AddTicks(time.TimeOfDay.Ticks).AddDays(time.Day - before.Day);
                    if (time.Ticks > before.Ticks)
                        time = time.AddMonths(-1);
                    break;

                case TimeDurationType.Day:
                    time = new DateTime(before.Year, before.Month, before.Day).AddTicks(time.TimeOfDay.Ticks);
                    if (time.Ticks > before.Ticks)
                        time = time.AddDays(-1);
                    break;

                case TimeDurationType.Week:
                    time = new DateTime(before.Year, before.Month, before.Day).AddTicks(time.TimeOfDay.Ticks).AddDays(time.DayOfWeek - before.DayOfWeek);
                    if (time.Ticks > before.Ticks)
                        time = time.AddDays(-7);
                    break;
            }

            return time;
        }

        /// <summary>
        /// Get next time by duration.
        /// </summary>
        public static DateTime GetTimeAfterTime(DateTime time, DateTime after, int timeDurationType)
        {
            switch (timeDurationType)
            {
                case TimeDurationType.Year:
                    time = new DateTime(after.Year, after.Month, after.Day).AddTicks(time.TimeOfDay.Ticks).AddDays(time.DayOfYear - after.DayOfYear);
                    if (time.Ticks <= after.Ticks)
                        time = time.AddYears(1);
                    break;

                case TimeDurationType.Month:
                    time = new DateTime(after.Year, after.Month, after.Day).AddTicks(time.TimeOfDay.Ticks).AddDays(time.Day - after.Day);
                    if (time.Ticks <= after.Ticks)
                        time = time.AddMonths(1);
                    break;

                case TimeDurationType.Day:
                    time = new DateTime(after.Year, after.Month, after.Day).AddTicks(time.TimeOfDay.Ticks);
                    if (time.Ticks <= after.Ticks)
                        time = time.AddDays(1);
                    break;

                case TimeDurationType.Week:
                    time = new DateTime(after.Year, after.Month, after.Day).AddTicks(time.TimeOfDay.Ticks).AddDays(time.DayOfWeek - after.DayOfWeek);
                    if (time.Ticks <= after.Ticks)
                        time = time.AddDays(7);
                    break;
            }

            return time;
        }

    }
}
