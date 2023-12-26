using System;
using ONEAdMax.Internal;
using UnityEngine;

namespace ONEAdMax
{
    public class OAMRewardVideo
    {
        private AndroidJavaObject _rewardVideo;

        public event Action OnLoaded;
        public event Action<OAMError> OnLoadFailed;
        public event Action OnOpened;
        public event Action<OAMError> OnOpenFailed;
        public event Action OnClosed;
        public event Action<int, bool> OnCompleted;
        public event Action OnClicked;
        
        public OAMRewardVideo()
        {
            _rewardVideo = new AndroidJavaObject(
                Constants.UnityRewardVideoAd,
                JniHelper.GetUnityAndroidActivity()
            );

            ConfigureRewardVideoEvents();
        }

        ~OAMRewardVideo()
        {
            _rewardVideo = null;
        }

        private void ConfigureRewardVideoEvents()
        {
            var listener = new OAMRewardVideoEventListener();
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

            listener.OnCompleted += (int adNetworkNo, bool completed) =>
            {
                if (OnCompleted != null)
                {
                    OAMEventDispatcher.RunOnMainThread(() => OnCompleted(adNetworkNo, completed));
                }
            };

            listener.OnClicked += () =>
            {
                if (OnClicked != null)
                {
                    OAMEventDispatcher.RunOnMainThread(() => OnClicked());
                }
            };

            _rewardVideo.Call("setEventListener", listener);
        }

        public void OnPause(bool pauseStatus)
        {
            var methodName = pauseStatus ? "onPause" : "onResume";
            _rewardVideo.Call(methodName);
        }

        public void Create(string placementId)
        {
            _rewardVideo.Call("create", placementId);
        }

        public bool IsLoaded() => _rewardVideo.Call<bool>("isLoaded");

        public void Load() => _rewardVideo.Call("load");

        public void Destroy() => _rewardVideo.Call("destroy");

        public void Show() => _rewardVideo.Call("show");

        /// <summary>
        /// Set network reservation timeouts for reward video ads.
        /// </summary>
        /// <remarks>
        /// When you load a reward video ad, set a timeout for each network so that
        /// if they don't receive the ad within that time,
        /// they move on to the next network.
        /// </remarks>
        /// <param name="seconds">Defaults to 5s.</param>
        public void SetNetworkScheduleTimeout(int timeout)
        {
            _rewardVideo.Call("setNetworkScheduleTimeout", timeout);
        }
    }
}
