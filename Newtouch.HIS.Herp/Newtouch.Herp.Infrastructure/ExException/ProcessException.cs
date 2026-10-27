using System;

namespace Newtouch.Herp.Infrastructure.ExException
{
    /// <summary>
    /// Port of the original Newtouch.Herp.Infrastructure.ExException.ProcessException:
    /// an exception raised by a business process step (distinct from the
    /// predictable FailedException validation failure).
    /// </summary>
    public class ProcessException : Exception
    {
        public string Msg { get; set; }

        public int ErrorCode { get; set; }

        public ProcessException() : this(0, "")
        {
        }

        public ProcessException(string msg) : this(0, msg)
        {
        }

        public ProcessException(int code) : this(code, "")
        {
        }

        public ProcessException(int code, string msg)
        {
            ErrorCode = code;
            Msg = msg;
        }
    }
}
