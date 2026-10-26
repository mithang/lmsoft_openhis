using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Herp.Infrastructure.ExException
{
    /// <summary>
    /// Ngoại lệ phát sinh trong quá trình xử lý
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
