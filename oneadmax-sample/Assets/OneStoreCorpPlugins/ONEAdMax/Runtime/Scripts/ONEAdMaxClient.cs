using System;
using UnityEngine;
using ONEAdMax.Internal;

namespace ONEAdMax
{
    public class ONEAdMaxClient
    {
        private static ONEAdMaxClient _instance;
        public static ONEAdMaxClient Instance
        { 
            get
            {
                _instance ??= new ONEAdMaxClient(); 
                return _instance;
            }
        }

        private static readonly OAMLogger logger = new OAMLogger();
        private readonly AndroidJavaObject oneAdMax = new AndroidJavaObject(Constants.ONEAdMax);

        static ONEAdMaxClient()
        {
            if (Application.platform != RuntimePlatform.Android)
            {
                throw new PlatformNotSupportedException("Operation is not supported on this platform.");
            }
        }

        /// <summary>
        /// Initializes the ONEAdMax SDK.
        /// </summary>
        /// <remarks>
        /// Call this method before loading an ad and before interacting with
        /// the rest of the ONEAdMax SDK.
        /// </remarks>
        /// <param name="initCompleteAction">
        /// An action which is invoked after initialization is complete.
        /// </param>
        public static void Initialize(Action initCompleteAction)
        {
            if (IsInitialize() == true)
            {
                logger.Warning("ONEAdMaxClient has already been initialized.");
                return;
            }

            var listener = new OAMInitializeListener();
            listener.OnInitialized += () =>
            {
                logger.Log("ONEAdMax has been initialized successfully.");

                if (initCompleteAction != null)
                {
                    OAMEventDispatcher.RunOnMainThread(() => initCompleteAction());
                }
            };
            
            Instance.oneAdMax.CallStatic(
                "init",
                JniHelper.GetUnityAndroidActivity(),
                listener
            );
        }

        public static bool IsInitialize()
        {
            return Instance.oneAdMax.CallStatic<bool>(
                "isInit",
                JniHelper.GetUnityAndroidActivity()
            );
        }

        public static void Destroy()
        {
            Instance.oneAdMax.CallStatic("unInit");
        }

        /// <summary>
        /// Expose the detailed logs of the ONEAdMax SDK for development.
        /// </summary>
        /// <param name="flag"></param>
        public static void SetLogEnable(bool flag)
        {
            Instance.oneAdMax.CallStatic("setLogEnable", flag);
        }

        /// <summary>
        /// This API is in response to the EU's GDPR(General Data protection Regulation)
        /// legislation.
        /// </summary>
        /// <remarks>
        /// Only users who require GDPR consent should call the API.
        /// </remarks>
        /// <param name="flag">default: true</param>
        public static void GdprConsentAvailable(bool flag)
        {
            Instance.oneAdMax.CallStatic("gdprConsentAvailable", flag);
        }

        public static void OpenRewardVideoCSPage(string param)
        {
            Instance.oneAdMax.CallStatic("openRewardVideoCSPage", JniHelper.GetUnityAndroidActivity(), param);
        }

        public static void SetUserId(string userId)
        {
            Instance.oneAdMax.CallStatic("setUserId", JniHelper.GetApplicationContext(), userId);
        }
    }  
}
