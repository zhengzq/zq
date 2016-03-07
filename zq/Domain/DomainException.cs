using System;

namespace Zq.Domain
{
    public class DomainException : Exception
    {
        public int Code { get; set; }
        public DomainException(string message, int code = 0)
            : base(message)
        {
            this.Code = code;
        }
        public DomainException()
        {
        }
    }
}
