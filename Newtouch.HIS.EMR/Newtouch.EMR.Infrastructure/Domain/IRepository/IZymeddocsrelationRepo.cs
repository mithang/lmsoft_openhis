using Newtouch.EMR.Domain.Entity;
using System.Collections.Generic;
using NewtouchHIS.Lib.Base.Model;

namespace Newtouch.EMR.Domain.IRepository
{
    /// <summary>
    /// Người tạo：chl
    /// Ngày：2018-09-11 10:46
    /// Mô tả：Bảng quan hệ văn bản bệnh án của bệnh nhân
    /// </summary>
    public interface IZymeddocsrelationRepo : IRepositoryBase<ZymeddocsrelationEntity>
    {
        /// <summary>
        /// Lưu biểu mẫu（Thêm mới、Sửa）
        /// </summary>
        /// <param name="entity">Đối tượng thực thể</param>
        /// <param name="keyValue">Giá trị khóa chính</param>
        void SubmitForm(ZymeddocsrelationEntity entity, string keyValue);
   
        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="keyValue">Giá trị khóa chính</param>
        void DeleteForm(string keyValue);
        /// <summary>
        /// Thêm mới/Sửa cây quan hệ bệnh án của bệnh nhân
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="zyh"></param>
        /// <param name="BlId"></param>
        /// <param name="BllxId"></param>
        /// <param name="OrgId"></param>
        void SubmitEntity(ZymeddocsrelationEntity entity);

        List<ZymeddocsrelationEntity> GetTreeList(string orgId, string zyh);

    }
}
