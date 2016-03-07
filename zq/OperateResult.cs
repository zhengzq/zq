namespace Zq
{
    public enum ResultState
    {
        Success = 1,
        Error = 2,
        Unauthorized = 3
    }
    public class OperateResult
    {
        public OperateResult(){ }
        public OperateResult(ResultState state, string msg = "", dynamic data = null)
        {
            this.State = state;
            this.Msg = msg;
            this.Data = data;
        }
        public string Msg { get; set; }
        public ResultState State { get; set; }
        public dynamic Data { get; set; }
    }
}
