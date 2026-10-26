using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.OR.ManageSystem.Domain
{
    public class PagingData<T>
    {
        /// <summary>
        /// Cách sắp xếpasccần máy chủ của bạn sắp xếp theo thứ tự，descSắp xếp ngược
        /// </summary>
        public string order { get; set; }
        /// <summary>
        /// Dòng bắt đầu，ví dụ cơ sở dữ liệu của bạn có100bản ghi，offsetbằng10có nghĩa là máy chủ của bạn cần bắt đầu từ10bản ghi để trả kết quả
        /// </summary>
        public int offset { get; set; }
        /// <summary>
        /// Số bản ghi đọc mỗi lần
        /// </summary>
        public int limit { get; set; }
        /// <summary>
        /// Từ khóa người dùng tìm kiếm trong ô nhập
        /// </summary>
        public string search { get; set; }
        /// <summary>
        /// Khi phân trang, bạn có thể truyền tham số tùy chỉnh của mình，ví dụ khi bạn muốn mỗi lần yêu cầu máy chủ đều kèm theoaction="getlist"Tham sốfunction(params) { return params }
        /// </summary>
        public T queryParams { get; set; }
    }
}
