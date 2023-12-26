
using UnityEngine;

namespace ONEAdMax
{
    /// <summary>
    /// For interstitial ads, you have the following customization options.
    /// </summary>
    public sealed class CustomExtraKey
    {
        /// <summary>
        /// Change interstitial ad background color and transparency 
        /// The value is a color. (Defaults to "#FF000000")
        /// </summary>
        public static readonly CustomExtraKey BACKGROUND_COLOR = new CustomExtraKey("backgroundColor");

        /// <summary>
        /// Set whether to show a close button in the top right corner of the interstitial ad.
        /// The value is a boolean. (Defaults to false)
        /// </summary>
        public static readonly CustomExtraKey HIDE_CLOSE_BTN = new CustomExtraKey("hideCloseBtn");

        /// <summary>
        /// Change to False to provide a margin based on the ad image.
        /// The value is a boolean. (Defaults to true)
        /// </summary>
        public static readonly CustomExtraKey CLOSE_BTN_MARGIN_FROM_EDGE = new CustomExtraKey("closeBtnGravityFromEdge");

        /// <summary>
        /// Left margin of the interstitial ad close button.
        /// The value is an integer. (Defaults to -28)
        /// </summary>
        public static readonly CustomExtraKey CLOSE_BTN_LEFT_MARGIN = new CustomExtraKey("closeBtnLeftMargin");

        /// <summary>
        /// Top margin of the interstitial ad close button.
        /// The value is an integer. (Defaults to 20)
        /// </summary>
        public static readonly CustomExtraKey CLOSE_BTN_TOP_MARGIN = new CustomExtraKey("closeBtnTopMargin");

        /// <summary>
        /// Right margin of the interstitial ad close button.
        /// The value is an integer. (Defaults to 20)
        /// </summary>
        public static readonly CustomExtraKey CLOSE_BTN_RIGHT_MARGIN = new CustomExtraKey("closeBtnRightMargin");

        /// <summary>
        /// Bottom margin of the interstitial ad close button.
        /// The value is an integer. (Defaults to 0)
        /// </summary>
        public static readonly CustomExtraKey CLOSE_BTN_BOTTOM_MARGIN = new CustomExtraKey("closeBtnBottomMargin");

        /// <summary>
        /// Disable interstitial ad exit with a backkey.
        /// The value is a boolean. (Defaults to false)
        /// </summary>
        public static readonly CustomExtraKey DISABLE_BACK_BTN = new CustomExtraKey("disableBackBtn");

        /// <summary>
        /// Whether to show an exit message in interstitial ads.
        /// The value is a boolean. (Defaults to false)
        /// </summary>
        public static readonly CustomExtraKey IS_ENDING_AD = new CustomExtraKey("isEndingAd");

        /// <summary>
        /// Change the exit ad message.
        /// The value is a string. (Defaults to "To exit, press the Back button one more time.")
        /// </summary>
        public static readonly CustomExtraKey ENDING_TEXT = new CustomExtraKey("endingText");

        /// <summary>
        /// Change the text size of an exit ad message.
        /// The value is an integer. (Defaults to 11(sp))
        /// </summary>
        public static readonly CustomExtraKey ENDING_TEXT_SIZE = new CustomExtraKey("endingTextSize");

        /// <summary>
        /// Change the text color of the exit ad message.
        /// The value is a color. (Defaults to "#FFFFFFFF")
        /// </summary>
        public static readonly CustomExtraKey ENDING_TEXT_COLOR = new CustomExtraKey("endingTextColor");

        /// <summary>
        /// Change the gravity of exit ad message.
        /// The value is an integer. (Defaults to <see cref="OAMGravity.RIGHT"/>)
        /// </summary>
        public static readonly CustomExtraKey ENDING_TEXT_GRAVITY = new CustomExtraKey("endingTextGravity");

        private readonly string _key;

        private CustomExtraKey(string key)
        {
            _key = key;
        }

        public override string ToString()
        {
            return _key;
        }
    }
}
