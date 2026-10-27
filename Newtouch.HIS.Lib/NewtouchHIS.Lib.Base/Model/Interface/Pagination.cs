namespace NewtouchHIS.Lib.Base.Model
{
    /// <summary>
    /// Lớp đóng gói tham số phân trang
    /// </summary>
    public class Pagination
    {
        /// <summary>
        /// Số dòng mỗi trang
        /// </summary>
        public int? rows { get; set; }

        /// <summary>
        /// Trang hiện tại
        /// </summary>
        public int? page { get; set; }

        /// <summary>
        /// Cột sắp xếp
        /// </summary>
        public string? sidx { get; set; }

        /// <summary>
        /// Loại sắp xếp
        /// </summary>
        public string? sord { get; set; }

        /// <summary>
        /// Tổng số bản ghi
        /// </summary>
        public int? records { get; set; }

        /// <summary>
        /// Tổng số trang
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
        /// Khởi tạo (nhân bản)
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
    /// Yêu cầu phân trang
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Pagination<T> : Pagination
    {
        /// <summary>
        /// Điều kiện phân trang
        /// </summary>
        public T? filter { get; set; }
    }

    public class PaginationResult<T> : Pagination
    {
        /// <summary>
        /// Dữ liệu phân trang
        /// </summary>
        public T? pagedata { get; set; }
    }

    /// <summary>
    /// Phân trang offset/limit
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class OLPagination<T>
    {
        /// <summary>
        /// Phương thức sắp xếp asc cần máy chủ của bạn sắp xếp theo thứ tự, desc sắp xếp ngược
        /// </summary>
        public string? order { get; set; }
        /// <summary>
        /// Trường sắp xếp
        /// </summary>
        public string? sort { get; set; }
        /// <summary>
        /// Dòng bắt đầu, ví dụ cơ sở dữ liệu của bạn có 100 bản ghi, offset bằng 10 nghĩa là máy chủ của bạn cần trả về kết quả từ bản ghi thứ 10
        /// </summary>
        public int offset { get; set; }
        /// <summary>
        /// Mỗi lần đọc bao nhiêu bản ghi
        /// </summary>
        public int limit { get; set; }
        /// <summary>
        /// Từ khóa người dùng tìm kiếm trong ô nhập
        /// </summary>
        public string? search { get; set; }

        /// <summary>
        /// Khi phân trang bạn có thể truyền tham số tùy chỉnh của mình, ví dụ bạn muốn mỗi lần gửi yêu cầu đến máy chủ kèm theoaction="getlist"tham sốfunction(params) { return params }
        /// </summary>
        public T? queryParams { get; set; }

        public int total { get; set; } = 0;
    }




}
