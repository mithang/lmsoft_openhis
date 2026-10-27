using Newtouch.EMR.Domain.Entity;
using System.Collections.Generic;
using NewtouchHIS.Lib.Base.Model;

namespace Newtouch.EMR.Domain.IRepository
{
    /// <summary>
    /// Người tạo：chl
    /// Ngày：2018-08-30 11:31
    /// Mô tả：Thông tin bệnh nhân nội trú
    /// </summary>
    public interface IZybrjbxxRepo : IRepositoryBase<ZybrjbxxEntity>
    {

        ZybrjbxxEntity GetZybrjbxx(string zyh, string OrganizeId);
        /// <summary>
        /// Lưu biểu mẫu（Thêm mới、Sửa）
        /// </summary>
        /// <param name="entity">Đối tượng thực thể</param>
        /// <param name="keyValue">Giá trị khóa chính</param>
        void SubmitForm(ZybrjbxxEntity entity, string keyValue);

        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="keyValue">Giá trị khóa chính</param>
        void DeleteForm(string keyValue);
        /// <summary>
        /// Nộp hồ sơ bệnh án
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="OrgId"></param>
        /// <param name="user"></param>
        void CommitRecord(string zyh, string OrgId, string user);
        /// <summary>
        /// Trả lại bệnh án
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="OrgId"></param>
        /// <param name="user"></param>
        void BackRecord(string zyh, string OrgId, string user);

    }
}
