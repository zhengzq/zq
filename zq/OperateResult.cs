using System;

namespace Zq
{
    public enum ResultState
    {
        Success = 1,
        Error = 2,
        Unauthorized = 3
    }
    [Serializable]
    public class OperateResult
    {
        public OperateResult()
        { }
        public OperateResult(ResultState state, string msg = "", int code = 0, dynamic data = null)
        {
            this.State = state;
            this.Msg = msg;
            this.Data = data;
            this.Code = code;
        }
        public int Code { get; set; }
        public string Msg { get; set; }
        public ResultState State { get; set; }
        public string JsonData { get; set; }
        public dynamic Data { get; set; }
    }
}
