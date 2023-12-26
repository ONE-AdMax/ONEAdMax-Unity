using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace ONEAdMax
{
    public class OAMEventDispatcher : MonoBehaviour
    {
        public static OAMEventDispatcher Instance = null;

        private static List<Action> _adEventQueue = new List<Action>();
        private volatile static bool _adEventsQueueEmpty = true;

        public static void RunOnMainThread(Action action)
        {
            lock(_adEventQueue)
            {
                _adEventQueue.Add(action);
                _adEventsQueueEmpty = false;
            }
        }

        public static void RunAsync(Action action) {
            ThreadPool.QueueUserWorkItem(o => action());
        }
 
        public static void RunAsync(Action<object> action, object state) {
            ThreadPool.QueueUserWorkItem(o => action(o), state);
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            if (IsActive()) return;

            Instance = new GameObject("OAMEventDispatcher").AddComponent<OAMEventDispatcher>();
            Instance.hideFlags = HideFlags.HideAndDontSave;
            DontDestroyOnLoad(Instance.gameObject);
            
        }

        public static bool IsActive()
        {
            return Instance != null;
        }

        public void Update()
        {
            if (_adEventsQueueEmpty) return;

            var stagedAdEventsQueue = new List<Action>();

            lock(_adEventQueue)
            {
                stagedAdEventsQueue.AddRange(_adEventQueue);
                _adEventQueue.Clear();
                _adEventsQueueEmpty = true;
            }

            foreach (Action stagedAction in stagedAdEventsQueue)
            {
                if (stagedAction.Target != null)
                {
                    stagedAction.Invoke();
                }
            }
        }

        public void OnDisable()
        {
            Instance = null;
        }
    }
}
