using System;
using UnityEngine;

namespace ONEAdMax.Internal
{
    public class OAMRewardVideoEventListener : AndroidJavaProxy
    {
        public event Action OnLoaded = delegate { };
        public event Action<OAMError> OnLoadFailed = delegate { };
        public event Action OnOpened = delegate { };
        public event Action<OAMError> OnOpenFailed = delegate { };
        public event Action OnClosed = delegate { };
        public event Action<int, bool> OnCompleted = delegate { };
        public event Action OnClicked = delegate { };
        
        public OAMRewardVideoEventListener() : base(Constants.IOAMRewardVideoEventListener) {}

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

        void onClosed() => OnClosed.Invoke();

        void onCompleted(int adNetworkNo, bool completed) => OnCompleted.Invoke(adNetworkNo, completed);

        void onClicked() => OnClicked.Invoke();
    }  
}
