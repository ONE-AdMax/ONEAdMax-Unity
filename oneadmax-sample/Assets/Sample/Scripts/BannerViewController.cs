using System.Collections.Generic;
using UnityEngine;
using ONEAdMax.Samples.Common;

namespace ONEAdMax.Samples
{
    /// <summary>
    /// Demonstrates how to use ONEAdMax banner views.
    /// </summary>
    [AddComponentMenu("ONEAdMax/Samples/BannerViewController")]
    public class BannerViewController : MonoBehaviour
    {
        private OAMBannerView _bannerView;

        // These ad units are configured to always serve test ads.
        private readonly string _placementId1 = "ONESTORE_BANNER_320x50";  // 320 * 50
        private readonly string _placementId2 = "ONESTORE_BANNER_320x100";  // 320 * 100
        private readonly string _placementId3 = "ONESTORE_BANNER_300x250";  // 300 * 250

        void OnApplicationPause(bool pauseStatus)
        {
            // You should call events for pause and resume in the application lifecycle.
            _bannerView?.OnPause(pauseStatus);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadPreviousScene();
            }
        }

        void OnDestroy() => Destroy();

        /// <summary>
        /// Creates a 320x50 banner at top of the screen.
        /// </summary>
        public void CreateBannerView()
        {
            Debug.Log("Creating banner view.");

            // If we already have a banner.
            if (_bannerView != null) {
                Debug.LogWarning("Already have a banner.");
                return;
            }

            // Create a 320x50 banner view at top of the screen.
            _bannerView = new OAMBannerView(AdSize.BANNER_320x50, AdPosition.Top);
            _bannerView.SetAnimType(AnimType.SLIDE_LEFT);
            _bannerView.SetNetworkScheduleTimeout(5);
            _bannerView.SetRefreshTime(60);
            _bannerView.SetAutoBgColor(true);

            // Extend specific mediation capabilities.
            SetMediationExtras(_bannerView);

            // Register to ad events to extend functionality.
            RegisterEvents(_bannerView);

            _bannerView.Create(_placementId1);

            Debug.Log("Banner view created.");
        }

        /// <summary>
        /// Extend the functionality of the mediation that appears in your ads.
        /// </summary>
        /// <seealso cref="MediationKey" />
        /// <param name="ad"><see cref="OAMBannerView"/></param>
        private void SetMediationExtras(OAMBannerView ad)
        {
            var extras = new Dictionary<MediationKey, object>
            {
                { MediationKey.CAULY_ENABLE_LOCK, false },
                { MediationKey.CAULY_ENABLE_DYNAMIC_RELOAD_INTERVAL, true },
                { MediationKey.CAULY_RELOAD_INTERVAL, 20 },
                { MediationKey.CAULY_THREAD_PRIORITY, 5 },

                { MediationKey.MEZZO_AGE_LEVEL, -1 },
                { MediationKey.MEZZO_ENABLE_BACKGROUND_CHECK, false },
                { MediationKey.MEZZO_STORE_URL, "https://m.onestore.co.kr" }
            };

            ad.SetMediationExtras(extras);
        }

        /// <summary>
        /// Register to events the banner may raise.
        /// </summary>
        private void RegisterEvents(OAMBannerView ad)
        {
            // Raised when an ad is loaded into the banner view.
            ad.OnLoaded += () =>
            {
                Debug.Log("Banner view loaded.");
            };

            // Raised when an ad fails to load into the banner view.
            ad.OnLoadFailed += (OAMError error) =>
            {
                Debug.LogError("Banner view failed to load an ad with error : " + error);
            };

            // Raised when a click is recorded for an ad.
            ad.OnClicked += () =>
            {
                Debug.Log("Banner view was clicked.");
            };
        }

        /// <summary>
        /// Creates the banner view and loads a banner ad.
        /// </summary>
        public void Load()
        {
            if (_bannerView == null)
            {
                Debug.LogWarning("The banner view is null.");
                return;
            }

            _bannerView.Load();
        }

        /// <summary>
        /// Destroys the ad.
        /// When you are finished with a OAMBannerView, make sure to call
        /// the Destroy() method before dropping your reference to it.
        /// </summary>
        public void Destroy()
        {
            if (_bannerView != null)
            {
                Debug.Log("Destroying banner view.");
                _bannerView.Destroy();
                _bannerView = null;
            }
        }

        public void LoadPreviousScene()
        {
            SceneManager.LoadPreviousScene();
        }
    }
}
