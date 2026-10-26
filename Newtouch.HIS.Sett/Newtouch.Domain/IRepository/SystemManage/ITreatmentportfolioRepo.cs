using Newtouch.Core.Common;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects.TherapeutistCompleteManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.IRepository.SystemManage
{
    public interface ITreatmentportfolioRepo : IRepositoryBase<TreatmentportfolioEntity>
    {

        /// <summary>
        /// Thêm
        /// </summary>
        /// <param name="entity"></param>
        void ADDInsert(TreatmentportfolioEntity entity, string keyValue);

        /// <summary>
        /// Xóa tổ hợp hạng mục thu phí
        /// </summary>
        /// <param name="keyValue"></param>
        void deleteid(string keyValue, string orgId);

        /// <summary>
        /// Xóa chi tiết trong tổ hợp thu phí
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="zhcodemc"></param>
        void deletemc(string keyValue, string zhcodemc, string orgId);


        /// <summary>
        /// Lưu tổ hợp hạng mục thu phí
        /// </summary>
        /// <param name="entity"></param>
        void ADDrowdata(TreatmentportfolioEntity entity);
        /// <summary>
        /// Đã tồn tại tổ hợp hạng mục thu phí hay chưa
        /// </summary>
        /// <param name="zhcodetj"></param>
        /// <param name="sfxmmc123"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<TreatmentportfolioEntity> TJchaxun(string zhcodetj, string sfxmmc123, string orgId);
        /// <summary>
        /// Lấy tổ hợp hạng mục thu phí đã sửa
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="zhcode"></param>
        /// <returns></returns>
        IList<TreatmentportfolioEntity> Codecx(string orgId, string zhcode);
        /// <summary>
        /// Tra cứu tổ hợp hạng mục thu phí
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<TreamentviewVO> Keyword(Pagination pagination, string keyword, string orgId);
    }
}
