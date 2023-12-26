using System;
using System.Collections.Generic;
using System.Linq;
using ONEAdMax.Internal;
using UnityEngine;

namespace ONEAdMax
{
    public class OAMBannerView
    {
        private AndroidJavaObject _bannerView;

        public event Action OnLoaded;
        public event Action<OAMError> OnLoadFailed;
        public event Action OnClicked;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="size">
        /// The banner size is passed to the AdSize enum class with three options,
        /// and the size can be set as follows 
        /// <see cref="AdSize.BANNER_320x50"/>,
        /// <see cref="AdSize.BANNER_320x250"/>,
        /// <see cref="AdSize.BANNER_320x100"/>
        /// </param>
        /// <param name="adPosition">
        /// Specify the gravity of the banner ad.
        /// <see cref="AdPosition"/>
        /// </param>
        public OAMBannerView(AdSize adSize, AdPosition adPosition)
        {
            var javaAdSize = OAMHelper.ConvertToJavaAdSize(adSize);

            _bannerView = new AndroidJavaObject(
                Constants.UnityBannerView,
                JniHelper.GetUnityAndroidActivity(),
                javaAdSize,
                (int) adPosition
            );
           
            ConfigureBannerEvents();
        }

        ~OAMBannerView()
        {
            _bannerView = null;
        }

        private void ConfigureBannerEvents()
        {
            var listener = new OAMBannerEventListener();
            listener.OnClicked += () =>
            {
                if (OnClicked != null)
                {
                    OAMEventDispatcher.RunOnMainThread(() => OnClicked());
                }
                
            };
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

            _bannerView.Call("setEventListener", listener);
        }

        public void Create(string placementId)
        {
            _bannerView.Call("create", placementId);
        }

        public void OnPause(bool pauseStatus)
        {
            var methodName = pauseStatus ? "onPause" : "onResume";
            _bannerView.Call(methodName);
        }

        public bool IsLoaded() => _bannerView.Call<bool>("isLoaded");

        public void Load() => _bannerView.Call("load");

        public void Destroy() => _bannerView.Call("destroy");

        /// <summary>
        /// Allows the background color to fill any empty space in the view
        /// where the banner ad is displayed.
        /// </summary>
        /// <param name="isAutoBgColor">Defaults to true</param>
        public void SetAutoBgColor(bool isAutoBgColor)
        {
            _bannerView.Call("setAutoBgColor", isAutoBgColor);
        }

        /// <summary>
        /// Set network reservation timeouts for banner ads.
        /// </summary>
        /// <remarks>
        /// When you load a banner ad, set a timeout for each network so that
        /// if they don't receive the ad within that time,
        /// they move on to the next network.
        /// </remarks>
        /// <param name="seconds">Defaults to 5s.</param>
        public void SetNetworkScheduleTimeout(int seconds)
        {
            _bannerView.Call("setNetworkScheduleTimeout", seconds);
        }

        /// <summary>
        /// Set the banner ad request renewal cycle.
        /// </summary>
        /// <param name="seconds">
        /// It can be set between 15 and 300 seconds, and if set to -1,
        /// it will not automatically renew.
        /// (Defaults to 60s.)
        /// </param>
        public void SetRefreshTime(int seconds)
        {
            _bannerView.Call("setRefreshTime", seconds);
        }

        /// <summary>
        /// Banner ad animation settings.
        /// </summary>
        /// <param name="type">
        /// <see cref="AnimType.NONE"/> : No animation (default).
        /// <see cref="AnimType.FADE_IN"/> : Fade-in animation.
        /// <see cref="AnimType.SLIDE_LEFT"/> : Animate the slide from the left.
        /// <see cref="AnimType.SLIDE_RIGHT"/> : Animate the slide from the right.
        /// <see cref="AnimType.TOP_SLIDE"/> : Animate the slide from the top.
        /// <see cref="AnimType.BOTTOM_SLIDE"/> : Animate the slide from the bottom.
        /// <see cref="AnimType.CIRCLE"/> : Banner Rotation Animation.
        /// </param>
        public void SetAnimType(AnimType type)
        {
            var animType = OAMHelper.ConvertToJavaAnimType(type);
            _bannerView.Call("setAnimType", animType);
        }

        /// <summary>
        /// Supports the following settings for some mediations of banner ads.
        /// </summary>
        /// <seealso cref="MediationKey"/>
        /// <param name="mediations"></param>
        public void SetMediationExtras(Dictionary<MediationKey, object> mediations)
        {
            var extras = mediations.ToDictionary(pair => pair.Key.ToString(), pair => pair.Value);
            var javaHashMap = JniHelper.CreateJavaHashMap(extras);
            _bannerView.Call("setMediationExtras", javaHashMap);
        }

        /// <summary>
        /// Specify the gravity of the banner ad.
        /// </summary>
        /// <param name="adPosition"><see cref="AdPosition"/></param>
        public void SetPosition(AdPosition adPosition)
        {
            _bannerView.Call("setPosition", (int) adPosition);
        }

    }
}
