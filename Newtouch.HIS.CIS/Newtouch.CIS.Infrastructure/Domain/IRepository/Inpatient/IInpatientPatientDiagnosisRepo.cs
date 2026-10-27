using System.Collections.Generic;
using Newtouch.Domain.Entity;
using Newtouch.Domain.ValueObjects;
using Newtouch.CIS.Repository;

namespace Newtouch.Domain.IRepository
{
    /// <summary>
    /// Tác giả: 冬林
    /// Ngày：2018-06-28 13:27
    /// Mô tả：Nhập việnbệnh nhânChẩn đoánthông tin
    /// </summary>
    public interface IInpatientPatientDiagnosisRepo : IRepositoryBase<InpatientPatientDiagnosisEntity>
    {
        /// <summary>
        /// Lưubảngphiếu（Thêm mới、Sửa）
        /// </summary>
        /// <param name="entity">thựcthểđối tượng</param>
        /// <param name="keyValue">Khóa chínhGiá trị</param>
        void SubmitForm(InpatientPatientDiagnosisEntity entity, string keyValue);

        /// <summary>
        /// XóaDữ liệu
        /// </summary>
        /// <param name="keyValue">Khóa chínhGiá trị</param>
        void DeleteForm(string keyValue);

        /// <summary>
        /// LấyChẩn đoánthông tin
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="organizeId"></param>
        /// <param name="zdlb">1Chẩn đoán nhập viện2Xuất việnChẩn đoán</param>
        /// <param name="zt">1-Hiệu lực  0-Vô hiệu</param>
        /// <returns></returns>
        List<InpatientPatientDiagnosisEntity> SelectData(string zyh, string organizeId, string zdlb,
           string zt = "1");
        /// <summary>
        /// LấyChẩn đoánthông tin
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="organizeId"></param>
        /// <param name="zdlb">1Chẩn đoán nhập viện2Xuất việnChẩn đoán</param>
        /// <param name="zt">1-Hiệu lực  0-Vô hiệu</param>
        /// <returns></returns>
        List<WMDiagnosisHtmlVO> SelectDiagData(string zyh, string organizeId, string zdlb,
           string zt = "1");

    }
}
