using NewtouchHIS.Domain.Entity.RemoteTreated;
using NewtouchHIS.Domain.InterfaceObjets.CIS;
using NewtouchHIS.Lib.Base;

namespace NewtouchHIS.Domain.IDomainService.CIS
{
    public interface IRemoteTreatedDmnService : IScopedDependency
    {
        /// <summary>
        /// Thêm hội chẩnĐăng ký
        /// </summary>
        /// <param name="applyVO"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<string> AddTreatedApply(TreatedApplyVO applyVO, string user);
        /// <summary>
        /// Cập nhật hội chẩnĐăng kýTrạng thái
        /// Yêu cầuApplyId,sqlsh không được để trống
        /// </summary>
        /// <param name="applyEntity"></param>
        /// <returns></returns>
        Task<bool> ModifiedApplyStu(TreatedApplyEntity applyEntity, string user);
        /// <summary>
        /// Truy vấn hội chẩnĐăng kýTrạng thái
        /// </summary>
        /// <param name="applyId"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        Task<bool> TreatedApplyStu(string applyId, string orgId);
        /// <summary>
        /// Hội chẩnĐăng kýTruy vấn thông tin by ApplyId
        /// </summary>
        /// <param name="applyId"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        Task<TreatedApplyVO> TreatedApplyInfo(string applyId, string orgId, string? sqlsh = null);
        /// <summary>
        /// Hội chẩnĐăng kýTruy vấn thông tin chi tiết  by ApplyId
        /// Chứa thông tin dư thừa
        /// </summary>
        /// <param name="applyId"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        Task<TreatedApplyExtendVO> TreatedApplyExtendInfo(string applyId, string orgId);
    }
}
