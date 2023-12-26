using System;
using ONEAdMax.Internal;
using UnityEngine;

namespace ONEAdMax
{
    public class OAMInterstitialVideo
    {
        private AndroidJavaObject _interstitialVideoAd;

        public event Action OnLoaded;
        public event Action<OAMError> OnLoadFailed;
        public event Action OnOpened;
        public event Action<OAMError> OnOpenFailed;
        public event Action OnClosed;

        public OAMInterstitialVideo()
        {
            _interstitialVideoAd = new AndroidJavaObject(
                Constants.UnityInterstitialVideoAd,
                JniHelper.GetUnityAndroidActivity()
            );

            ConfigureInterstitialVideoEvents();
        }

        ~OAMInterstitialVideo()
        {
            _interstitialVideoAd = null;
        }

        private void ConfigureInterstitialVideoEvents()
        {
            var listener = new OAMInterstitialVideoEventListener();
            listener.OnLoaded += () =>
            {
                if (OnLoaded != null)
                {
                    OAMEventDispatcher.RunOnMainThread(() => OnLoaded());
                }
            };

            listener.OnLoadFailed += (OAMError error) =>
            {
                if (OnLoadFailed != null)
                {
                    OAMEventDispatcher.RunOnMainThread(() => OnLoadFailed(error));
                }
            };

            listener.OnOpened += () =>
            {
                if (OnOpened != null)
                {
                    OAMEventDispatcher.RunOnMainThread(() => OnOpened());
                }
            };

            listener.OnOpenFailed += (OAMError error) =>
            {
                if (OnOpenFailed != null)
                {
                    OAMEventDispatcher.RunOnMainThread(() => OnOpenFailed(error));
                }
            };

            listener.OnClosed += () =>
            {
                if (OnClosed != null)
                {
                    OAMEventDispatcher.RunOnMainThread(() => OnClosed());
                }
            };

            _interstitialVideoAd.Call("setEventListener", listener);
        }

        public void Create(string placementId)
        {
            _interstitialVideoAd.Call("create", placementId);
        }

        public bool IsLoaded() => _interstitialVideoAd.Call<bool>("isLoaded");

        public void Load() => _interstitialVideoAd.Call("load");

        public void Destroy() => _interstitialVideoAd.Call("destroy");

        public void Show() => _interstitialVideoAd.Call("show");

        /// <summary>
        /// Set network reservation timeouts for interstitial video ads.
        /// </summary>
        /// <remarks>
        /// When you load a interstitial video ad, set a timeout for each network so that
        /// if they don't receive the ad within that time,
        /// they move on to the next network.
        /// </remarks>
        /// <param name="seconds">Defaults to 5s.</param>
        public void SetNetworkScheduleTimeout(int timeout)
        {
            _interstitialVideoAd.Call("setNetworkScheduleTimeout", timeout);
        }
    }
}
