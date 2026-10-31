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
        /// Cách sắp xếpascCần máy chủ của bạn sắp xếp theo thứ tự,descSắp xếp ngược
        /// </summary>
        public string order { get; set; }
        /// <summary>
        /// Dòng bắt đầu,Ví dụ CSDL của bạn có100bản ghi,offsetbằng10nghĩa là máy chủ cần trả từ bản ghi thứ10bản ghi để trả kết quả
        /// </summary>
        public int offset { get; set; }
        /// <summary>
        /// Số bản ghi đọc mỗi lần
        /// </summary>
        public int limit { get; set; }
        /// <summary>
        /// Từ khóa người dùng tìm trong ô nhập
        /// </summary>
        public string search { get; set; }
        /// <summary>
        /// Khi phân trang bạn có thể truyền tham số tùy chỉnh,Ví dụ bạn muốn mỗi lần yêu cầu máy chủ kèmaction="getlist"tham sốfunction(params) { return params }
        /// </summary>
        public T queryParams { get; set; }
    }
}
