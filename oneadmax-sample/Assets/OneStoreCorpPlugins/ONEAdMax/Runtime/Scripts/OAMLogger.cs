using UnityEngine;

namespace ONEAdMax
{
    public class OAMLogger
    {
        private const string TAG = "ONEAdMaxUnity: ";

        /// <summary>
        /// Logs a formatted message with ILogger.
        /// </summary>
        public void Log(string format, params object[] args)
        {
            Debug.unityLogger.LogFormat(LogType.Log, TAG + format, args);
        }

        /// <summary>
        /// Logs a formatted warning message with ILogger.
        /// </summary>
        public void Warning(string format, params object[] args)
        {
            Debug.unityLogger.LogFormat(LogType.Warning, TAG + format, args);
        }

        /// <summary>
        /// Logs a formatted error message with ILogger.
        /// </summary>
        public void Error(string format, params object[] args)
        {
            Debug.unityLogger.LogFormat(LogType.Error, TAG + format, args);
        }

    }
}
