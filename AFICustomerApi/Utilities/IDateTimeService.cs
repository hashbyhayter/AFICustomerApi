namespace AFICustomerApi.Utilities
{
    public interface IDateTimeService
    {
        /// <summary>
        /// Returns DateTime.Now
        /// </summary>
        public DateTime Now { get; }

        /// <summary>
        /// Returns DateTime.Today
        /// </summary>
        public DateTime Today { get; }
    }
}
