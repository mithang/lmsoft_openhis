using NewtouchHIS.Domain.Entity.RemoteTreated;
using NewtouchHIS.Domain.InterfaceObjets.CIS;
using NewtouchHIS.Lib.Base;

namespace NewtouchHIS.Domain.IDomainService.CIS
{
    public interface IRemoteTreatedDmnService : IScopedDependency
    {
        /// <summary>
        /// thêm mới đăng ký hội chẩn
        /// </summary>
        /// <param name="applyVO"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<string> AddTreatedApply(TreatedApplyVO applyVO, string user);
        /// <summary>
        /// cập nhật trạng thái đăng ký hội chẩn
        /// yêu cầuApplyId、sqlshkhông được để trống
        /// </summary>
        /// <param name="applyEntity"></param>
        /// <returns></returns>
        Task<bool> ModifiedApplyStu(TreatedApplyEntity applyEntity, string user);
        /// <summary>
        /// truy vấn trạng thái đăng ký hội chẩn
        /// </summary>
        /// <param name="applyId"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        Task<bool> TreatedApplyStu(string applyId, string orgId);
        /// <summary>
        /// truy vấn thông tin đăng ký hội chẩn by ApplyId
        /// </summary>
        /// <param name="applyId"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        Task<TreatedApplyVO> TreatedApplyInfo(string applyId, string orgId, string? sqlsh = null);
        /// <summary>
        /// truy vấn thông tin chi tiết đăng ký hội chẩn  by ApplyId
        /// bao gồm thông tin dự phòng
        /// </summary>
        /// <param name="applyId"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        Task<TreatedApplyExtendVO> TreatedApplyExtendInfo(string applyId, string orgId);
    }
}
