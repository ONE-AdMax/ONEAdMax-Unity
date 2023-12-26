
namespace ONEAdMax
{
    public sealed class MediationKey
    {
        /// <summary>
        /// Set to true to show Couly ads on the lock screen.
        /// The value is a boolean. (Defaults to false)
        /// </summary>
        public static readonly MediationKey CAULY_ENABLE_LOCK = new MediationKey("CAULY_ENABLE_LOCK");

        /// <summary>
        /// Control ad serving intervals on Cauly's side.
        /// The value is a boolean. (Defaults to true)
        /// </summary>
        public static readonly MediationKey CAULY_ENABLE_DYNAMIC_RELOAD_INTERVAL = new MediationKey("CAULY_DYNAMIC_RELOAD_INTERVAL");

        /// <summary>
        /// Controls how often ads appear in media. (available after changing CAULY_DAYNAMIC_RELOAD_INTERVAL to False)
        /// The value is an integer. (Defaults to 20s / Setting range 10 to 120)
        /// </summary>
        public static readonly MediationKey CAULY_RELOAD_INTERVAL = new MediationKey ("CAULY_RELOAD_INTERVAL");

        /// <summary>
        /// Set thread priority.
        /// The value is an inteeger. (default: 5 / range: 1-10)
        /// </summary>
        public static readonly MediationKey CAULY_THREAD_PRIORITY = new MediationKey("CAULY_THREAD_PRIORITY");

        /// <summary>
        /// Options to set age information for meso ads.
        /// Unknown = -1; Children(under 13) = 0; Teens and adults(over 13) = 1
        /// The value is an inteeger. (Defaults to -1)
        /// </summary>
        public static readonly MediationKey MEZZO_AGE_LEVEL = new MediationKey("MEZZO_AGE_LEVEL");

        /// <summary>
        /// Option to allow the banner to run in the background. (true | false)
        /// The value is a boolean.
        /// </summary>
        public static readonly MediationKey MEZZO_ENABLE_BACKGROUND_CHECK = new MediationKey("MEZZO_IS_USED_BACKGROUND_CHECK");

        /// <summary>
        /// The app's Store URL
        /// The value is a string.
        /// </summary>
        public static readonly MediationKey MEZZO_STORE_URL = new MediationKey("MEZZO_STORE_URL");

        private readonly string _key;
        
        private MediationKey(string key)
        {
            _key = key;
        }

        public override string ToString()
        {
            return _key;
        }
    }
}
