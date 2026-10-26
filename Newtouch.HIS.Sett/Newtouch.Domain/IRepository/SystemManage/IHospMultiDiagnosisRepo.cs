using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// Nhiều chẩn đoán nhập viện nội trú
    /// </summary>
    public interface IHospMultiDiagnosisRepo : IRepositoryBase<HospMultiDiagnosisEntity>
    {
        /// <summary>
        /// Lấy thông tin chẩn đoán qua số nhập viện đã dùng
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        List<HospMultiDiagnosisEntity> GetDiagnoseByZYH(string zyh);

        /// <summary>
        /// Tra cứu
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<HospMultiDiagnosisEntity> SelectData(string zyh, string organizeId);
    }
}
