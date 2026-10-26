using Newtouch.Domain.Entity;
using System.Collections.Generic;
using Newtouch.Core.Common;
using Newtouch.Infrastructure.EF;
using Newtouch.Domain.ValueObjects.Outpatient;

namespace Newtouch.Domain.IRepository
{
    /// <summary>
    /// Tạo bởi:张冬林
    /// Ngày:2018-06-28 13:27
    /// Mô tả:Thông tin bệnh nhân nội trú
    /// </summary>
    public interface IInpatientPatientInfoRepo : IRepositoryBase<InpatientPatientInfoEntity>
    {
        /// <summary>
        /// Lưu biểu mẫu(Thêm mới,Sửa đổi)
        /// </summary>
        /// <param name="entity">Đối tượng thực thể</param>
        /// <param name="keyValue">Giá trị khóa chính</param>
        void SubmitForm(InpatientPatientInfoEntity entity, string keyValue);

        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="keyValue">Giá trị khóa chính</param>
        void DeleteForm(string keyValue);
        /// <summary>
        /// Cập nhật cờ đang ở viện của bệnh nhân nội trú
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="zyh"></param>
        /// <param name="zybz"></param>
        void UpdateInpatientStatus(string orgId, string zyh, string zybz, string ryzd, string ryzdmc, string brxz, string brxzmc, string lxr, string lxrgx, string lxrdh);

        /// <summary>
        /// Lấy thông tin bệnh nhân nội trú qua số nội trú
        /// </summary>
        /// <param name="OrganizeId"></param>
        /// <param name="zyh"></param>
        /// <returns></returns>
        InpatientPatientInfoEntity GetByZyh(string OrganizeId, string zyh);

        /// <summary>
        /// Lấy thông tin cơ bản bệnh nhân
        /// </summary>
        /// <param name="zyh">Số nội trú</param>
        /// <param name="xm">Họ tên</param>
        /// <param name="organizeId">Tổ chứcID</param>
        /// <returns></returns>
        InpatientPatientInfoEntity SelectData(string zyh, string xm, string organizeId);


        List<InpatientAreaVO> TreeViewdata(string orgId);


    }
}