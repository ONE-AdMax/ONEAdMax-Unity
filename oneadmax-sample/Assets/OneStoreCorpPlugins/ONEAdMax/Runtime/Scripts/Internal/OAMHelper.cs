using System;
using UnityEngine;

namespace ONEAdMax.Internal
{
    public class OAMHelper
    {
        public static OAMError ParseJavaOAMError(AndroidJavaObject javaOAMError)
        {
            var code = javaOAMError.Call<int>("getCode");
            var message = javaOAMError.Call<string>("getMessage");

            return new OAMError(code, message);
        }

        public static CloseEventType ParseJavaCloseEventType(AndroidJavaObject javaCloseEventType)
        {
            if (javaCloseEventType == null) return CloseEventType.UNKNOWN;

            var javaEnumName = javaCloseEventType.Call<string>("name");
            // Enum 이름과 Java Enum 이름이 일치하면 변환
            if (Enum.IsDefined(typeof(CloseEventType), javaEnumName))
                return (CloseEventType) Enum.Parse(typeof(CloseEventType), javaEnumName);

            return CloseEventType.UNKNOWN;
        }

        public static AndroidJavaObject ConvertToJavaAdSize(AdSize type)
        {
            if (Enum.IsDefined(typeof(AdSize), type)) {
                string cSharpEnumName = type.ToString();

                // AndroidJavaClass를 사용하여 Java Enum 클래스를 로드
                AndroidJavaClass javaEnumClass = new AndroidJavaClass(Constants.BannerAdSize);
                // Java Enum 값을 생성
                return javaEnumClass.GetStatic<AndroidJavaObject>(cSharpEnumName);
            }

            return null; // Enum 값이 정의되지 않았을 경우
        }

        public static AndroidJavaObject ConvertToJavaAnimType(AnimType type)
        {
            if (Enum.IsDefined(typeof(AnimType), type)) {
                string cSharpEnumName = type.ToString();

                // AndroidJavaClass를 사용하여 Java Enum 클래스를 로드
                AndroidJavaClass javaEnumClass = new AndroidJavaClass(Constants.BannerAnimType);
                // Java Enum 값을 생성
                return javaEnumClass.GetStatic<AndroidJavaObject>(cSharpEnumName);
            }

            return null; // Enum 값이 정의되지 않았을 경우
        }
    }
}
