using System.Collections.Generic;
using NewtouchHIS.Lib.Base.Model;
using Newtouch.Herp.Domain.Entity.VEntity;

namespace Newtouch.Herp.Domain.IDomainServices
{
    /// <summary>
    /// Bảo trì chứng chỉ
    /// </summary>
    public interface ILicLicenceDmnService
    {
        /// <summary>
        /// Lấy loại chứng chỉ
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        IList<VLicLicenceTypeEntity> GetLicenceTypeList(Pagination pagination, string keyWord = "");

        /// <summary>
        /// Lấy danh sách chứng chỉ
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyWord"></param>
        /// <param name="belongedId"></param>
        /// <param name="licenceTypeId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        IList<VLicLicencesEntity> GetLicenceList(Pagination pagination, string keyWord, string belongedId, string licenceTypeId, string organizeId);
    }
}