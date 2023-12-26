
namespace ONEAdMax
{
    public enum OAMErrorCode
    {
        // General errors
        EXCEPTION = 200,
        
        // Invalid parameters
        INVALID_PARAMETER = 1000,
        
        // Unknown server errors
        UNKNOWN_SERVER_ERROR = 9999,
        
        // Invalid app key
        INVALID_APP_KEY = 2000,
        
        // Invalid Spot Key
        INVALID_PLACEMENT_ID = 2030,
        
        // No ads
        EMPTY_CAMPAIGN = 2100,
        
        // Failed to load external network information
        INVALID_THIRDPARTY_NAME = 2200,
        
        // Error initializing native settings
        NATIVE_SPOT_DOES_NOT_INITIALIZED = 3200,
        
        // Server Timeouts
        SERVER_TIMEOUT = 5000,
        
        // Failed to load certain network ads
        LOAD_AD_FAILED = 5001,
        
        // Failure to load all network ads
        NO_AD = 5002
    }
}
