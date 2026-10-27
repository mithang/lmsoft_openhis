using System;

namespace Newtouch.Sett.Web.Helpers
{
    /// <summary>Core replacement for the Framework DateTimeHelper (binary-only).
    /// GetInHospDays: whole calendar days between admission date and end date.</summary>
    public static class DateTimeHelper
    {
        public static int GetInHospDays(DateTime start, DateTime end)
        {
            return (end.Date - start.Date).Days;
        }
    }
}
