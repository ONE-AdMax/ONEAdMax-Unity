using System;
using UnityEngine;

namespace ONEAdMax.Internal
{
    public class OAMInterstitialEventListener : AndroidJavaProxy
    {
        public event Action OnLoaded = delegate { };
        public event Action<OAMError> OnLoadFailed = delegate { };
        public event Action OnOpened = delegate { };
        public event Action<OAMError> OnOpenFailed = delegate { };
        public event Action<CloseEventType> OnClosed = delegate { };
        public event Action OnClicked = delegate { };
        
        public OAMInterstitialEventListener() : base(Constants.IOAMInterstitialEventListener) {}

        void onLoaded() => OnLoaded.Invoke();

        void onLoadFailed(AndroidJavaObject oamError)
        {
            OnLoadFailed.Invoke(OAMHelper.ParseJavaOAMError(oamError));
        } 

        void onOpened() => OnOpened.Invoke();

        void onOpenFailed(AndroidJavaObject oamError)
        {
            OnOpenFailed.Invoke(OAMHelper.ParseJavaOAMError(oamError));
        }

        void onClosed(AndroidJavaObject closeEventType)
        {
            OnClosed.Invoke(OAMHelper.ParseJavaCloseEventType(closeEventType));
        }

        void onClicked() => OnClicked.Invoke();
    }  
}
