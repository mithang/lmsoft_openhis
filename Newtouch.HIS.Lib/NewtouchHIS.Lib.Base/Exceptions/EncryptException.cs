namespace NewtouchHIS.Lib.Base.Exceptions
{
    public class EncryptException
    {
    }
    /// <summary>
    /// Lớp ngoại lệ - ngoại lệ mã hóa Rj (giúp phân biệt loại ngoại lệ)
    /// </summary>
    public class RijndaelEncryptException : Exception
    {
        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        public RijndaelEncryptException()
        {

        }
    }
    /// <summary>
    /// Lớp ngoại lệ - ngoại lệ giải mã Rj (giúp phân biệt loại ngoại lệ)
    /// </summary>
    public class RijndaelDecrptyException : Exception
    {
        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        public RijndaelDecrptyException()
        {

        }
    }
}
