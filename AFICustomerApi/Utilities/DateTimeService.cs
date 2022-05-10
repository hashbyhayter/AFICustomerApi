namespace AFICustomerApi.Utilities
{
    public class DateTimeService : IDateTimeService
    {
        /// <summary>
        /// Returns DateTime.Now
        /// </summary>
        public DateTime Now => DateTime.Now;

        /// <summary>
        /// Returns DateTime.Today
        /// </summary>
        public DateTime Today => DateTime.Today;
    }
}
