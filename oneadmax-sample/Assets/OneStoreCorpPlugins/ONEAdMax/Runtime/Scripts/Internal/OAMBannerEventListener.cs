using System;
using UnityEngine;

namespace ONEAdMax.Internal
{
    public class OAMBannerEventListener : AndroidJavaProxy
    {
        public event Action OnLoaded = delegate { };
        public event Action<OAMError> OnLoadFailed = delegate { };
        public event Action OnClicked = delegate { };
        
        public OAMBannerEventListener() : base(Constants.IOAMBannerEventListener) {}

        void onLoaded() => OnLoaded.Invoke();

        void onLoadFailed(AndroidJavaObject oamError) {
            OnLoadFailed.Invoke(OAMHelper.ParseJavaOAMError(oamError));
        } 

        void onClicked() => OnClicked.Invoke();
    }  
}
