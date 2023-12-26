using UnityEngine;
using ONEAdMax.Samples.Common;

namespace ONEAdMax.Samples
{
    /// <summary>
    /// Demonstrates how to use ONEAdMax interstitial video ads.
    /// </summary>
    [AddComponentMenu("ONEAdMax/Samples/InterstitialVideoAdController")]
    public class InterstitialVideoAdController : MonoBehaviour
    {
        // These ad units are configured to always serve test ads.
        private readonly string  _placementId = "ONESTORE_VIDEO";
        private OAMInterstitialVideo _interstitialVideoAd;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadPreviousScene();
            }
        }

        void OnDestroy() => Destroy();

        /// <summary>
        /// Creates the ad.
        /// </summary>
        public void CreateInterstitialVideoAd()
        {
            Debug.Log("Creating Interstitial video ad.");
            
            if (_interstitialVideoAd != null)
            {
                Debug.LogWarning("Already have a Interstitial video ad.");
                return;
            }

            _interstitialVideoAd = new OAMInterstitialVideo();
            _interstitialVideoAd.SetNetworkScheduleTimeout(10);

            // Register to ad events to extend functionality.
            RegisterEvents(_interstitialVideoAd);

            _interstitialVideoAd.Create(_placementId);

            Debug.Log("Interstitial video ad created.");
        }

        /// <summary>
        /// Register to events the interstital video ad may raise.
        /// </summary>
        private void RegisterEvents(OAMInterstitialVideo ad)
        {
            // Raised when an ad is loaded into the interstitial ad.
            ad.OnLoaded += () =>
            {
                Debug.Log("Interstitial video ad loaded.");
            };

            // Raised when an ad fails to load into the interstitial ad.
            ad.OnLoadFailed += (OAMError error) =>
            {
                Debug.LogError("Interstitial video ad failed to load an ad with error : " + error);
            };

            // Raised when an ad opened full screen content.
            ad.OnOpened += () =>
            {
                Debug.Log("Interstitial video ad has been opened.");
            };

            // Raised when the ad failed to open full screen content.
            ad.OnOpenFailed += (OAMError error) =>
            {
                Debug.LogError("Interstitial video ad failed to open an ad with error : " + error);
            };

            // Raised when the ad closed full screen content.
            ad.OnClosed += () =>
            {
                Debug.LogError("Interstitial video ad is closed.");
            };
        }


        /// <summary>
        /// Loads the ad.
        /// </summary>
        public void Load()
        {
            if (_interstitialVideoAd == null)
            {
                Debug.LogError("The Interstitial video ad is null.");
                return;
            }
            _interstitialVideoAd.Load();
        }

        /// <summary>
        /// Shows the ad.
        /// </summary>
        public void Show()
        {
            if (_interstitialVideoAd != null & _interstitialVideoAd.IsLoaded())
            {
                Debug.Log("Showing interstital video ad.");
                _interstitialVideoAd.Show();
            }
            else
            {
                Debug.LogError("The Interstitial video ad hasn't loaded yet.");
            }
        }

        /// <summary>
        /// Destroys the ad.
        /// </summary>
        public void Destroy()
        {
            if (_interstitialVideoAd != null)
            {
                Debug.Log("Destroying interstitial video ad.");
                _interstitialVideoAd.Destroy();
                _interstitialVideoAd = null;
            }
        }

        public void LoadPreviousScene()
        {
            SceneManager.LoadPreviousScene();
        }
    }
}
