namespace NewtouchHIS.Lib.Base.Model
{
    public class PageResponse
    {
        public int total { get; set; }

    }
    /// <summary>
    /// phản hồi phân trang Model
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PageResponseRow<T> : PageResponse
    {

        public T? rows { get; set; }
    }

}
