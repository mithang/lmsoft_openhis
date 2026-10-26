namespace NewtouchHIS.Lib.Base.Model
{
    /// <summary>
    /// lớp đóng gói tham số phân trang
    /// </summary>
    public class Pagination
    {
        /// <summary>
        /// số dòng mỗi trang
        /// </summary>
        public int? rows { get; set; }

        /// <summary>
        /// trang hiện tại
        /// </summary>
        public int? page { get; set; }

        /// <summary>
        /// cột sắp xếp
        /// </summary>
        public string? sidx { get; set; }

        /// <summary>
        /// kiểu sắp xếp
        /// </summary>
        public string? sord { get; set; }

        /// <summary>
        /// tổng số bản ghi
        /// </summary>
        public int? records { get; set; }

        /// <summary>
        /// tổng số trang
        /// </summary>
        public int? total
        {
            get
            {
                if (records > 0)
                {
                    return records % this.rows == 0 ? records / this.rows : records / this.rows + 1;
                }
                else
                {
                    return 0;
                }
            }
        }

        public Pagination()
        {
            rows = 15;
            page = 1;
            sidx = string.Empty;
            sord = string.Empty;
            records = 0;
        }

        /// <summary>
        /// khởi tạo（nhân bản）
        /// </summary>
        /// <param name="pagination"></param>
        public Pagination(Pagination pagination)
        {
            rows = pagination.rows;
            page = pagination.page;
            sidx = pagination.sidx;
            sord = pagination.sord;
            records = pagination.records;
        }
    }

    /// <summary>
    /// yêu cầu phân trang
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Pagination<T> : Pagination
    {
        /// <summary>
        /// điều kiện phân trang
        /// </summary>
        public T? filter { get; set; }
    }

    public class PaginationResult<T> : Pagination
    {
        /// <summary>
        /// dữ liệu phân trang
        /// </summary>
        public T? pagedata { get; set; }
    }

    /// <summary>
    /// phân trang offset/limit
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class OLPagination<T>
    {
        /// <summary>
        /// phương thức sắp xếpasccần máy chủ của bạn sắp xếp theo thứ tự，descsắp xếp thứ tự ngược
        /// </summary>
        public string? order { get; set; }
        /// <summary>
        /// trường sắp xếp
        /// </summary>
        public string? sort { get; set; }
        /// <summary>
        /// dòng bắt đầu，ví dụ cơ sở dữ liệu của bạn có100dòng dữ liệu，offsetbằng10biểu thị máy chủ của bạn cần từ ... thứ10kết quả trả về của dòng dữ liệu
        /// </summary>
        public int offset { get; set; }
        /// <summary>
        /// mỗi lần đọc bao nhiêu dòng dữ liệu
        /// </summary>
        public int limit { get; set; }
        /// <summary>
        /// từ khóa người dùng tìm kiếm trong ô nhập
        /// </summary>
        public string? search { get; set; }

        /// <summary>
        /// khi phân trang bạn có thể truyền tham số tùy chỉnh của mình，ví dụ bạn muốn mỗi lần gửi yêu cầu tới máy chủ đều mang theoaction="getlist"tham sốfunction(params) { return params }
        /// </summary>
        public T? queryParams { get; set; }

        public int total { get; set; } = 0;
    }




}
