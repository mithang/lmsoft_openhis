using System.Collections.Generic;
using Newtouch.Domain.Entity;
using Newtouch.Domain.ValueObjects;
using Newtouch.CIS.Repository;

namespace Newtouch.Domain.IRepository
{
    /// <summary>
    /// Tác giả: 冬林
    /// Ngày: 2018-06-28 13:27
    /// Mô tả: Nhập viện bệnh nhân Chẩn đoán thông tin
    /// </summary>
    public interface IInpatientPatientDiagnosisRepo : IRepositoryBase<InpatientPatientDiagnosisEntity>
    {
        /// <summary>
        /// Lưu bảng phiếu (Thêm mới, Sửa)
        /// </summary>
        /// <param name="entity">thực thể đối tượng</param>
        /// <param name="keyValue">Khóa chính Giá trị</param>
        void SubmitForm(InpatientPatientDiagnosisEntity entity, string keyValue);

        /// <summary>
        /// Xóa Dữ liệu
        /// </summary>
        /// <param name="keyValue">Khóa chính Giá trị</param>
        void DeleteForm(string keyValue);

        /// <summary>
        /// Lấy Chẩn đoán thông tin
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="organizeId"></param>
        /// <param name="zdlb">1 Chẩn đoán nhập viện 2 Xuất viện Chẩn đoán</param>
        /// <param name="zt">1-Hiệu lực 0-Vô hiệu</param>
        /// <returns></returns>
        List<InpatientPatientDiagnosisEntity> SelectData(string zyh, string organizeId, string zdlb,
           string zt = "1");
        /// <summary>
        /// Lấy Chẩn đoán thông tin
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="organizeId"></param>
        /// <param name="zdlb">1 Chẩn đoán nhập viện 2 Xuất viện Chẩn đoán</param>
        /// <param name="zt">1-Hiệu lực 0-Vô hiệu</param>
        /// <returns></returns>
        List<WMDiagnosisHtmlVO> SelectDiagData(string zyh, string organizeId, string zdlb,
           string zt = "1");

    }
}
