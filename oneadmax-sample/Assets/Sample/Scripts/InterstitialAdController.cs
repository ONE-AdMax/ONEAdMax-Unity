using UnityEngine;
using ONEAdMax.Samples.Common;
using System.Collections.Generic;

namespace ONEAdMax.Samples
{
    /// <summary>
    /// Demonstrates how to use ONEAdMax interstitial ads.
    /// </summary>
    [AddComponentMenu("ONEAdMax/Samples/InterstitialAdController")]
    public class InterstitialAdController : MonoBehaviour
    {
        // These ad units are configured to always serve test ads.
        private readonly string  _placementId = "ONESTORE_INTERSTITIAL";
        private OAMInterstitial _interstitialAd;

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
        public void CreateInterstitialAd()
        {
            Debug.Log("Creating Interstitial ad.");
            
            if (_interstitialAd != null)
            {
                Debug.LogWarning("Already have a Interstitial ad.");
                return;
            }

            _interstitialAd = new OAMInterstitial();

            // Customization of the ads screen.
            SetCustomExtras(_interstitialAd);

            // Register to ad events to extend functionality.
            RegisterEvents(_interstitialAd);

            _interstitialAd.Create(_placementId);

            Debug.Log("Interstitial ad created.");
        }

        /// <summary>
        /// Customization of the ads screen.
        /// </summary>
        /// <seealso cref="CustomExtraKey"/>
        /// <param name="ad"><see cref="OAMInterstitial"/></param>
        private void SetCustomExtras(OAMInterstitial ad)
        {
            var customExtras = new Dictionary<CustomExtraKey, object>
            {
                { CustomExtraKey.BACKGROUND_COLOR, "#FF000000" },
                { CustomExtraKey.HIDE_CLOSE_BTN, false },

                { CustomExtraKey.CLOSE_BTN_MARGIN_FROM_EDGE, true },
                { CustomExtraKey.CLOSE_BTN_LEFT_MARGIN, -28 },
                { CustomExtraKey.CLOSE_BTN_TOP_MARGIN, 20 },
                { CustomExtraKey.CLOSE_BTN_RIGHT_MARGIN, 20 },
                { CustomExtraKey.CLOSE_BTN_BOTTOM_MARGIN, 0 },
                
                { CustomExtraKey.DISABLE_BACK_BTN, false },

                { CustomExtraKey.IS_ENDING_AD, true },
                { CustomExtraKey.ENDING_TEXT, "To exit, press the Back button one more time." },
                { CustomExtraKey.ENDING_TEXT_SIZE, 14 },
                { CustomExtraKey.ENDING_TEXT_COLOR, "#FFFFFFFF" },
                { CustomExtraKey.ENDING_TEXT_GRAVITY, OAMGravity.RIGHT },
            };

            ad.SetCustomExtras(customExtras);
        }

        /// <summary>
        /// Register to events the interstital ad may raise.
        /// </summary>
        private void RegisterEvents(OAMInterstitial ad)
        {
            // Raised when an ad is loaded into the interstitial ad.
            ad.OnLoaded += () =>
            {
                Debug.Log("Interstitial ad loaded.");
            };

            // Raised when the interstitial ad fails to load.
            ad.OnLoadFailed += (OAMError error) =>
            {
                Debug.LogError("Interstitial ad failed to load an ad with error : " + error);
            };

            // Raised when an ad opened full screen content.
            ad.OnOpened += () =>
            {
                Debug.Log("Interstitial ad has been opened.");
            };

            // Raised when the ad failed to open full screen content.
            ad.OnOpenFailed += (OAMError error) =>
            {
                Debug.LogError("Interstitial ad failed to open an ad with error : " + error);
            };

            // Raised when the ad closed full screen content.
            ad.OnClosed += (CloseEventType closeEventType) =>
            {
                Debug.LogError("Interstitial ad closed with type : " + closeEventType);
            };

            // Raised when a click is recorded for an ad.
            ad.OnClicked += () =>
            {
                Debug.Log("Interstitial ad was clicked.");
            };
        }

        /// <summary>
        /// Loads the ad.
        /// </summary>
        public void Load()
        {
            if (_interstitialAd == null)
            {
                Debug.LogError("The Interstitial ad is null.");
                return;
            }
            _interstitialAd.Load();
        }

        /// <summary>
        /// Shows the ad.
        /// </summary>
        public void Show()
        {
            if (_interstitialAd != null && _interstitialAd.IsLoaded())
            {
                Debug.Log("Showing interstital ad.");
                _interstitialAd.Show();
            }
            else
            {
                Debug.LogError("The Interstitial ad hasn't loaded yet.");
            }
        }

        /// <summary>
        /// Destroys the ad.
        /// </summary>
        public void Destroy()
        {
            if (_interstitialAd != null)
            {
                Debug.Log("Destroying interstitial ad.");
                _interstitialAd.Destroy();
                _interstitialAd = null;
            }
        }

        public void LoadPreviousScene()
        {
            SceneManager.LoadPreviousScene();
        }
    }
}
