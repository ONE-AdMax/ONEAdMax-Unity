using UnityEngine;
using ONEAdMax.Samples.Common;

namespace ONEAdMax.Samples
{
    /// <summary>
    /// Demonstrates how to use ONEAdMax rewarded ads.
    /// </summary>
    [AddComponentMenu("ONEAdMax/Samples/RewardVideoAdController")]
    public class RewardVideoAdController : MonoBehaviour
    {
        // These ad units are configured to always serve test ads.
        private readonly string  _placementId = "ONESTORE_REWARD_VIDEO";
        private OAMRewardVideo _rewardVideoAd;

        void OnApplicationPause(bool pauseStatus)
        {
            // You should call events for pause and resume in the application lifecycle.
            _rewardVideoAd?.OnPause(pauseStatus);
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
        /// Creates the ad.
        /// </summary>
        public void CreateRewardVideoAd()
        {
            Debug.Log("Creating Reward video ad.");
            
            if (_rewardVideoAd != null)
            {
                Debug.LogWarning("Already have a Reward video ad.");
                return;
            }

            _rewardVideoAd = new OAMRewardVideo();
            _rewardVideoAd.SetNetworkScheduleTimeout(10);

            // Register to ad events to extend functionality.
            RegisterEvents(_rewardVideoAd);

            _rewardVideoAd.Create(_placementId);

            Debug.Log("Reward video ad created.");
        }

        /// <summary>
        /// Register to events the reward video ad may raise.
        /// </summary>
        private void RegisterEvents(OAMRewardVideo ad)
        {
            // Raised when an ad is loaded into the reward video ad.
            ad.OnLoaded += () =>
            {
                Debug.Log("Reward video ad loaded.");
            };

            // Raised when an ad fails to load into the reward video ad.
            ad.OnLoadFailed += (OAMError error) =>
            {
                Debug.LogError("Reward video ad failed to load an ad with error : " + error);
            };

            // Raised when an ad opened full screen content.
            ad.OnOpened += () =>
            {
                Debug.Log("Reward video ad has been opened.");
            };

            // Raised when the ad failed to open full screen content.
            ad.OnOpenFailed += (OAMError error) =>
            {
                Debug.LogError("Reward video ad failed to open an ad with error : " + error);
            };

            // Raised when the ad closed full screen content.
            ad.OnClosed += () =>
            {
                Debug.LogError("Reward video ad is closed.");
            };

            // Raised when the ad completed full screen content.
            ad.OnCompleted += (int adNetworkNo, bool compledted, string bidId, bool enablePostback) =>
            {
                Debug.Log("Reward video ad completed : " + "adNetworkNo=" + adNetworkNo + ", compledted=" + compledted + ", bidId=" + bidId + ", enablePostback=" + enablePostback);
            };

            // Raised when a click is recorded for an ad.
            ad.OnClicked += () =>
            {
                Debug.Log("Reward video ad was clicked.");
            };
        }

        /// <summary>
        /// Loads the ad.
        /// </summary>
        public void Load()
        {
            if (_rewardVideoAd == null)
            {
                Debug.LogError("The Reward video ad is null.");
                return;
            }
            _rewardVideoAd.Load();
        }

        /// <summary>
        /// Shows the ad.
        /// </summary>
        public void Show()
        {
            if (_rewardVideoAd != null && _rewardVideoAd.IsLoaded())
            {
                Debug.Log("Showing reward video ad.");
                _rewardVideoAd.Show();
            }
            else
            {
                Debug.LogError("The Reward video ad hasn't loaded yet.");
            }
        }

        /// <summary>
        /// Destroys the ad.
        /// </summary>
        public void Destroy()
        {
            if (_rewardVideoAd != null)
            {
                Debug.Log("Destroying Reward video ad.");
                _rewardVideoAd.Destroy();
                _rewardVideoAd = null;
            }
        }

        public void LoadPreviousScene()
        {
            SceneManager.LoadPreviousScene();
        }
    }
}
