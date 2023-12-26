using System;
using System.Collections.Generic;
using UnityEngine;

namespace ONEAdMax.Internal
{
    public class JniHelper
    {
        /// <summary>
        /// Returns the Android activity context of the Unity app.
        /// </summary>
        public static AndroidJavaObject GetUnityAndroidActivity()
        {
            return new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>(
                "currentActivity");
        }

        /// <summary>
        /// Returns the Android application context of the Unity app.
        /// </summary>
        public static AndroidJavaObject GetApplicationContext()
        {
            return GetUnityAndroidActivity().Call<AndroidJavaObject>("getApplicationContext");
        }

        /// <summary>
        /// Create a Java ArrayList of strings.
        /// </summary>
        public static AndroidJavaObject CreateJavaArrayList(params string[] inputs)
        {
            var javaList = new AndroidJavaObject("java.util.ArrayList");
            foreach (var input in inputs)
            {
                javaList.Call<bool>("add", input);
            }

            return javaList;
        }

        /*public static AndroidJavaObject CreateJavaHashMap(Dictionary<string, string> inputs)
        {
            var javaHashMap = new AndroidJavaObject("java.util.HashMap");
            IntPtr methodId = AndroidJNIHelper.GetMethodID(javaHashMap.GetRawClass(), "put",
                "(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;");

            if (inputs != null)
            {
                object[] args = new object[2];
                foreach (KeyValuePair<string, string> kvp in inputs)
                {
                    using (AndroidJavaObject k = new AndroidJavaObject("java.lang.String", kvp.Key))
                    {
                        using (AndroidJavaObject v = new AndroidJavaObject("java.lang.String", kvp.Value))
                        {
                            args[0] = k;
                            args[1] = v;
                            AndroidJNI.CallObjectMethod(
                                javaHashMap.GetRawObject(),
                                methodId,
                                AndroidJNIHelper.CreateJNIArgArray(args)
                            );
                        }
                    }
                }
            }
            return javaHashMap;
        }*/

        
        public static AndroidJavaObject CreateJavaHashMap(Dictionary<string, object> inputs)
        {
            var javaHashMap = new AndroidJavaObject("java.util.HashMap");
            IntPtr methodId = AndroidJNIHelper.GetMethodID(javaHashMap.GetRawClass(), "put",
                "(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;");

            if (inputs != null)
            {
                object[] args = new object[2];
                foreach (KeyValuePair<string, object> kvp in inputs)
                {
                    using (AndroidJavaObject k = new AndroidJavaObject("java.lang.String", kvp.Key))
                    {
                        using (AndroidJavaObject v = ObjectToJavaObject(kvp.Value))
                        {
                            args[0] = k;
                            args[1] = v;
                            AndroidJNI.CallObjectMethod(
                                javaHashMap.GetRawObject(),
                                methodId,
                                AndroidJNIHelper.CreateJNIArgArray(args)
                            );
                        }
                    }
                }
            }
            return javaHashMap;
        }

        private static readonly Dictionary<Type, Func<object, AndroidJavaObject>> typeToJavaObjectConverter =
            new Dictionary<Type, Func<object, AndroidJavaObject>>
            {
                { typeof(int), obj => new AndroidJavaObject("java.lang.Integer", (int)obj) },
                { typeof(string), obj => new AndroidJavaObject("java.lang.String", (string)obj) },
                { typeof(bool), obj => new AndroidJavaObject("java.lang.Boolean", (bool)obj) },
                { typeof(float), obj => new AndroidJavaObject("java.lang.Float", (float)obj) },
                { typeof(double), obj => new AndroidJavaObject("java.lang.Double", (double)obj) },
                { typeof(char), obj => new AndroidJavaObject("java.lang.Character", (char)obj) },
                { typeof(byte), obj => new AndroidJavaObject("java.lang.Byte", (byte)obj) },
                { typeof(short), obj => new AndroidJavaObject("java.lang.Short", (short)obj) },
            };

        private static AndroidJavaObject ObjectToJavaObject(object obj)
        {
            if (obj == null) return null;

            if (typeToJavaObjectConverter.TryGetValue(obj.GetType(), out var converter))
                return converter(obj);

            throw new NotSupportedException("Unsupported type: " + obj.GetType().Name);
        }
    }   
}
