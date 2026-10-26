using System.Collections.Generic;
using Newtouch.Domain.Entity;
using Newtouch.Domain.ValueObjects;
using Newtouch.Infrastructure.EF;

namespace Newtouch.Domain.IRepository
{
    /// <summary>
    /// Tạo bởi:张冬林
    /// Ngày:2018-06-28 13:27
    /// Mô tả:Thông tin chẩn đoán bệnh nhân nội trú
    /// </summary>
    public interface IInpatientPatientDiagnosisRepo : IRepositoryBase<InpatientPatientDiagnosisEntity>
    {
        /// <summary>
        /// Lưu biểu mẫu(Thêm mới,Sửa đổi)
        /// </summary>
        /// <param name="entity">Đối tượng thực thể</param>
        /// <param name="keyValue">Giá trị khóa chính</param>
        void SubmitForm(InpatientPatientDiagnosisEntity entity, string keyValue);

        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="keyValue">Giá trị khóa chính</param>
        void DeleteForm(string keyValue);

        /// <summary>
        /// Lấy thông tin chẩn đoán
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="organizeId"></param>
        /// <param name="zdlb">1Chẩn đoán nhập viện2Chẩn đoán xuất viện</param>
        /// <param name="zt">1-Có hiệu lực  0-Vô hiệu</param>
        /// <returns></returns>
        List<InpatientPatientDiagnosisEntity> SelectData(string zyh, string organizeId, string zdlb,
           string zt = "1");
        /// <summary>
        /// Lấy thông tin chẩn đoán
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="organizeId"></param>
        /// <param name="zdlb">1Chẩn đoán nhập viện2Chẩn đoán xuất viện</param>
        /// <param name="zt">1-Có hiệu lực  0-Vô hiệu</param>
        /// <returns></returns>
        List<WMDiagnosisHtmlVO> SelectDiagData(string zyh, string organizeId, string zdlb,
           string zt = "1");

    }
}