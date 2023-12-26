using System;
using UnityEngine;

namespace ONEAdMax.Internal
{
    public class OAMInitializeListener : AndroidJavaProxy
    {
        public event Action OnInitialized = delegate { };
        public OAMInitializeListener() : base(Constants.IOAMInitListener) {}

        void onInitialized() => OnInitialized.Invoke();
    }  
}
