using System;
using System.Collections.Generic;
using NewtouchHIS.Lib.Base.Model;
using Newtouch.Domain.DTO.OutputDto;
using Newtouch.Domain.Entity;
using Newtouch.Domain.ValueObjects.API;
using Newtouch.CIS.Repository;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITreatmentRepo : IRepositoryBase<TreatmentEntity>
    {
        /// <summary>
        /// đãKhám bệnh Khám bệnhtrong danh sách( đãKhám bệnh: chỉcó thểXemtựtựtêndướicủaBệnh nhân)
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="orgId"></param>
        /// <param name="jzzt"></param>
        /// <param name="mjzbz"></param>
        /// <param name="rygh"></param>
        /// <param name="kzrq"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<TreatEntityObj> GetTreatingOrTreatedList(Pagination pagination, string orgId, int jzzt, int mjzbz, string rygh, DateTime? kzrq, string keyword, bool? zzhz);

        IList<PatientRegInfoDTO> GetdjzList(Pagination pagination, string keyword, int mjzbz);

        IList<RegisteredInfoRespVO> GetghList(string xm, string blh, string orgid);

        /// <summary>
        /// khôi phụclạiKhám bệnh
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="jzId"></param>
        void ResumeTreat(string orgId, string jzId);

        /// <summary>
        /// select data
        /// </summary>
        /// <param name="jzId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        TreatmentEntity SelectData(string jzId, string organizeId);

        /// <summary>
        /// select data
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<TreatmentEntity> SelectDataByMzh(string mzh, string organizeId);

        /// <summary>
        ///SửaCmmPatId
        /// </summary>
        /// <param name="cmmPatId"></param>
        /// <param name="jzId"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <param name="updateTime"></param>
        /// <returns></returns>
        int UpdateCmmPatId(string cmmPatId, string jzId, string organizeId, string userCode, DateTime updateTime);
    }
}
