using UnityEngine;
using UnityEngine.UI;
using System.Threading;

namespace ONEAdMax.Samples.Common
{
    [AddComponentMenu("ONEAdMax/Samples/Common/LogMessageText")]
    public class LogMessageText : Text
    {
        private SynchronizationContext _synchronizationContext;

        protected override void Awake()
        {
            base.Awake();

            if (Application.isPlaying)
            {
                supportRichText = true;
                text = string.Empty;
                _synchronizationContext = SynchronizationContext.Current;
                Application.logMessageReceivedThreaded += OnLogMessageReceivedThreaded;
            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            Application.logMessageReceivedThreaded -= OnLogMessageReceivedThreaded;
        }

        private void OnLogMessageReceivedThreaded(string logString, string stackTrace, LogType type)
        {
            _synchronizationContext.Post((sender) =>
            {
                // Safeguard against race conditions from Unity disposed objects.
                if (this == null || !Application.isPlaying)
                {
                    return;
                }

                string color;
                switch (type)
                {
                    case LogType.Warning:
                        color = "#FFFF00";
                        break;
                    case LogType.Error:
                    case LogType.Exception:
                        color = "#FF4500";
                        break;
                    default:
                        color = "#FFFFFF";
                        break;
                }

                string message = $"<color={color}>{logString}</color>\n\r";
                text += message;
            }, this);
        }
    }
}