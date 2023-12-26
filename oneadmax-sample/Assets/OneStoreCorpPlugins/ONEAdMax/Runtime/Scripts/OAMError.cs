using System;

namespace ONEAdMax
{
    [Serializable]
    public class OAMError
    {
        private readonly int _code;
        private readonly string _message;

        public int Code { get { return _code; } }
        public string Message { get { return _message; } }

        public OAMError(int code, string message)
        {
            _code = code;
            _message = message;
        }

        public override string ToString()
        {
            return "OAMError(code=" + _code + ", message=" + _message + ")";
        }
    }
}
