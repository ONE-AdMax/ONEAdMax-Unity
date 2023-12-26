using System;
using System.Collections.Generic;
using System.Linq;
using ONEAdMax.Internal;
using UnityEngine;

namespace ONEAdMax
{
    public class OAMInterstitial
    {
        private AndroidJavaObject _interstitialAd;

        public event Action OnLoaded;
        public event Action<OAMError> OnLoadFailed;
        public event Action OnOpened;
        public event Action<OAMError> OnOpenFailed;
        public event Action<CloseEventType> OnClosed;
        public event Action OnClicked;

        public OAMInterstitial()
        {
            _interstitialAd = new AndroidJavaObject(
                Constants.UnityInterstitialAd,
                JniHelper.GetUnityAndroidActivity()
            );

            ConfigureInterstitialEvents();
        }

        ~OAMInterstitial()
        {
            _interstitialAd = null;
        }

        private void ConfigureInterstitialEvents()
        {
            var listener = new OAMInterstitialEventListener();
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

            listener.OnClosed += (CloseEventType closeEventType) =>
            {
                if (OnClosed != null)
                {
                    OAMEventDispatcher.RunOnMainThread(() => OnClosed(closeEventType));
                }
            };

            listener.OnClicked += () =>
            {
                if (OnClicked != null)
                {
                    OAMEventDispatcher.RunOnMainThread(() => OnClicked());
                }
            };

            _interstitialAd.Call("setEventListener", listener);
        }

        public void Create(string placementId)
        {
            _interstitialAd.Call("create", placementId);
        }

        public bool IsLoaded() => _interstitialAd.Call<bool>("isLoaded");

        public void Load() => _interstitialAd.Call("load");

        public void Destroy() => _interstitialAd.Call("destroy");

        public void Show() => _interstitialAd.Call("show");

        /// <summary>
        /// Supports the following settings for some screens in interstitials.
        /// </summary>
        /// <seealso cref="CustomExtraKey"/>
        /// <param name="customExtras"></param>
        public void SetCustomExtras(Dictionary<CustomExtraKey, object> customExtras)
        {
            var extras = customExtras.ToDictionary(pair => pair.Key.ToString(), pair => pair.Value);
            var javaHashMap = JniHelper.CreateJavaHashMap(extras);
            _interstitialAd.Call("setCustomExtras", javaHashMap);
        }
    }
}
