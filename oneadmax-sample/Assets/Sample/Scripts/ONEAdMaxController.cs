using UnityEngine;
using ONEAdMax.Samples.Common;

namespace ONEAdMax.Samples
{
    /// <summary>
    /// Demonstrates how to use the ONEAdMax Unity plugin.
    /// </summary>
    [AddComponentMenu("ONEAdMax/Samples/ONEAdMaxController")]
    public class ONEAdMaxController : MonoBehaviour
    {
        // The ONEAdMax Unity plugin needs to be run only once.
        private static bool _initialized = false;
        
        /// <summary>
        /// Demonstrates how to configure ONEAdMax Unity plugin.
        /// </summary>
        void Start()
        {
            if (!_initialized)
            {
                Debug.Log("ONEAdMaxController initialize.");
                // Expose the detailed logs of the ONEAdMax SDK for development.
                ONEAdMaxClient.SetLogEnable(true);
                
                ONEAdMaxClient.Initialize(() => { _initialized = true; });
            }
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }

        /// <summary>
        /// When you terminate your application, you need to free up memory
        /// for resources allocated to the OneAdMax SDK.
        /// </summary>
        void OnApplicationQuit()
        {
            ONEAdMaxClient.Destroy();
        }

        /// <summary>
        /// Open the Banner view scene.
        /// </summary>
        public void OpenBannerView()
        {
            SceneManager.LoadScene("ONEAdMaxBannerView");
        }

        /// <summary>
        /// Open the interstitial ad scene.
        /// </summary>
        public void OpenInterstitial()
        {
            SceneManager.LoadScene("ONEAdMaxInterstitial");
        }

        /// <summary>
        /// Open the interstitial video ad scene.
        /// </summary>
        public void OpenInterstitialVideo()
        {
            SceneManager.LoadScene("ONEAdMaxInterstitialVideo");
        }

        /// <summary>
        /// Open the reward video ad scene.
        /// </summary>
        public void OpenRewardVideo()
        {
            SceneManager.LoadScene("ONEAdMaxRewardVideo");
        }
    }

}
