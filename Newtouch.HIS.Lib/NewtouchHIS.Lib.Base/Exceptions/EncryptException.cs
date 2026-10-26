namespace NewtouchHIS.Lib.Base.Exceptions
{
    public class EncryptException
    {
    }
    /// <summary>
    /// lớp ngoại lệ-Rjngoại lệ mã hóa（giúp phân biệt loại ngoại lệ）
    /// </summary>
    public class RijndaelEncryptException : Exception
    {
        /// <summary>
        /// hàm khởi tạo
        /// </summary>
        public RijndaelEncryptException()
        {

        }
    }
    /// <summary>
    /// lớp ngoại lệ-Rjngoại lệ giải mã（giúp phân biệt loại ngoại lệ）
    /// </summary>
    public class RijndaelDecrptyException : Exception
    {
        /// <summary>
        /// hàm khởi tạo
        /// </summary>
        public RijndaelDecrptyException()
        {

        }
    }
}
