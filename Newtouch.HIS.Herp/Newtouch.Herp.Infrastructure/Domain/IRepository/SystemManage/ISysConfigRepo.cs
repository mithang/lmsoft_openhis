using System.Collections.Generic;
using Newtouch.Herp.Domain.Entity;

namespace Newtouch.Herp.Domain.IRepository
{
    /// <summary>
    /// Repo cấu hình hệ thống — bản port mỏng của FrameworkBase.MultiOrg
    /// ISysConfigRepo (chỉ có dạng binary). Chỉ khai báo các thành viên mà
    /// code Herp thực sự dùng: IQueryable (API Default/Query), GetList và
    /// Insert (HomeController.SyncSysConfigParams), cộng GetIntValue phục vụ
    /// getmesquery (thay cho SysConfigReader.Int của FrameworkBase).
    /// Dữ liệu đọc từ bảng Sys_Config của CSDL Herp (xem SysConfigEntity).
    /// </summary>
    public interface ISysConfigRepo : IRepositoryBase<SysConfigEntity>
    {
        /// <summary>
        /// Lấy danh sách cấu hình của một tổ chức, lọc theo từ khóa
        /// (mã hoặc tên). keyWord rỗng = lấy tất cả của tổ chức đó.
        /// organizeId "*" = dữ liệu gốc dùng chung cho mọi tổ chức.
        /// </summary>
        IList<SysConfigEntity> GetList(string keyWord, string organizeId);

        /// <summary>
        /// Đọc giá trị số nguyên của một mã cấu hình: ưu tiên dòng của tổ
        /// chức hiện tại, sau đó dòng gốc "*", không có hoặc không parse
        /// được thì trả về 0 (đúng hành vi mặc định của SysConfigReader.Int).
        /// </summary>
        int GetIntValue(string code, string organizeId);
    }
}
