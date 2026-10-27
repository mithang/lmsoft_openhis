using Mapster;
using NewtouchHIS.Base.Domain.Entity;
using NewtouchHIS.Base.Domain.Organize;
using NewtouchHIS.Base.DomainService;
using NewtouchHIS.Domain.Entity.RemoteTreated;
using NewtouchHIS.Domain.IDomainService.CIS;
using NewtouchHIS.Domain.InterfaceObjets.CIS;
using NewtouchHIS.Lib.Base.Exceptions;
using static NewtouchHIS.Lib.Common.HisEnum;

namespace NewtouchHIS.DomainService.CIS
{
    public class RemoteTreatedDmnService : BaseServices<TreatedApplyEntity>, IRemoteTreatedDmnService
    {
        private readonly ISysStaffDmnService _sysStaffDmn;
        private readonly ISysDepartmentDmnService _sysDepartmentDmn;
        public RemoteTreatedDmnService(ISysDepartmentDmnService sysDepartment, ISysStaffDmnService sysStaff)
        {
            _sysStaffDmn = sysStaff;
            _sysDepartmentDmn = sysDepartment;
        }
        #region IDU Thao tác
        public async Task<string> AddTreatedApply(TreatedApplyVO applyVO, string user)
        {
            applyVO.sqzt = (int)EmunRemoteTreatedStu.dqr;
            if (string.IsNullOrWhiteSpace(applyVO.OrganizeId))
            {
                throw new FailedException("Thông tin tổ chức không được để trống");
            }
            if (string.IsNullOrWhiteSpace(applyVO.sqlsh))
            {
                throw new FailedException("Mã duy nhất của đăng ký khám chữa bệnh từ xa không được để trống");
            }
            if (DateTime.Compare(applyVO.sqsj ?? DateTime.Now, DateTime.Now.Date) < 0)
            {
                throw new FailedException("Thời gian khám chữa bệnh đăng ký bất thường");
            }
            var lshcheck = await baseDal.GetByWhere(p => p.sqlsh == applyVO.sqlsh && p.OrganizeId == applyVO.OrganizeId && p.zt == "1");
            if (lshcheck.Count > 0)
            {
                throw new FailedException("Đăng ký này đã được gửi, vui lòng không thao tác trùng lặp");
            }
            applyVO.Id = Guid.NewGuid().ToString();
            applyVO.NewEntity(applyVO.OrganizeId, user);
            string errorMsg = string.Empty;
            if (!ValidRequired(applyVO, out errorMsg))
            {
                throw new FailedException(errorMsg);
            }
            TreatedApplyEntity entity = applyVO.Adapt<TreatedApplyEntity>();
            var result = await baseDal.Add(entity);
            if (result <= 0)
            {
                throw new FailedException("Nhận thông tin đăng ký bất thường, vui lòng thử lại");
            }
            return entity.Id;
        }
        /// <summary>
        /// Sửa đổiĐăng kýTrạng thái phiếu,Cuộc họpSố(Chỉ khi đang khám&khi chờ xác nhận)
        /// </summary>
        /// <param name="applyEntity"></param>
        /// <returns></returns>
        /// <exception cref="FailedException"></exception>
        public async Task<bool> ModifiedApplyStu(TreatedApplyEntity applyEntity, string user)
        {
            if (string.IsNullOrWhiteSpace(applyEntity.Id) || string.IsNullOrWhiteSpace(applyEntity.sqlsh))
            {
                throw new FailedException("Đăng ký quan trọngId không được để trống");
            }
            var entity = await baseDal.GetFirstOrDefault(p => p.Id == applyEntity.Id && p.OrganizeId == applyEntity.OrganizeId && p.sqlsh == applyEntity.sqlsh && p.zt == "1");
            if (entity == null || string.IsNullOrWhiteSpace(entity.Id))
            {
                throw new FailedException("Thông tin đăng ký khám chữa bệnh bất thường, xác nhận đăng ký Id và số thứ tự đăng ký có khớp hay không");
            }
            switch (entity.sqzt)
            {
                case (int)EmunRemoteTreatedStu.dqr:
                    entity.sqzt = applyEntity.sqzt;
                    if (applyEntity.sqzt == (int)EmunRemoteTreatedStu.dqr || applyEntity.sqzt == (int)EmunRemoteTreatedStu.jzz)
                    {
                        entity.mzh = applyEntity.mzh;
                        entity.mettingId = string.IsNullOrWhiteSpace(applyEntity.mettingId) ? entity.mettingId : applyEntity.mettingId;
                        entity.jzsj = DateTime.Now;
                    }
                    break;
                case (int)EmunRemoteTreatedStu.jzz:
                    //Cập nhậtCuộc họpSố
                    if (applyEntity.sqzt == (int)EmunRemoteTreatedStu.jzz)
                    {
                        entity.mzh = applyEntity.mzh;
                        entity.mettingId = string.IsNullOrWhiteSpace(applyEntity.mettingId) ? entity.mettingId : applyEntity.mettingId;
                    }
                    //Kết thúc khám
                    else if (applyEntity.sqzt == (int)EmunRemoteTreatedStu.yjs)
                    {
                        entity.sqzt = applyEntity.sqzt;
                    }
                    else
                    {
                        throw new FailedException($"Đang khám chữa bệnh");
                    }
                    break;
                case (int)EmunRemoteTreatedStu.yjs:
                    if (applyEntity.sqzt == (int)EmunRemoteTreatedStu.yfy)
                    {
                        entity.sqzt = applyEntity.sqzt;
                    }
                    else
                    {
                        throw new FailedException($"Cập nhật trạng thái đăng ký bất thường, lần khám chữa bệnh này đã kết thúc");
                    }
                    break;
                case (int)EmunRemoteTreatedStu.yth:
                    throw new FailedException($"Cập nhật trạng thái đăng ký bất thường, đăng ký khám chữa bệnh lần này bị hủy do bị từ chối");
                case (int)EmunRemoteTreatedStu.ycx:
                    throw new FailedException($"Cập nhật trạng thái đăng ký bất thường, đăng ký khám chữa bệnh lần này đã bị hủy");
                case (int)EmunRemoteTreatedStu.yfy:
                    throw new FailedException($"Cập nhật trạng thái đăng ký bất thường, quá trình khám chữa bệnh lần này đã kết thúc, nhà thuốc đã phát thuốc");
            }
            entity.ModifiedEntity(applyEntity.OrganizeId, user);
            return await baseDal.UpdateAsync(entity);
        }


        #endregion

        public async Task<bool> TreatedApplyStu(string applyId, string orgId)
        {
            var entity = await baseDal.FindKey(applyId);
            if (entity == null || entity.zt != "1")
            {
                throw new FailedException("Thông tin đăng ký khám chữa bệnh bất thường, vui lòng gửi lại đăng ký");
            }
            if (entity.OrganizeId != orgId)
            {
                throw new FailedException("Thông tin tổ chức đăng ký khám chữa bệnh kiểm tra bất thường, vui lòng xác nhận lại đăng ký Id");
            }
            switch (entity.sqzt)
            {
                case (int)EmunRemoteTreatedStu.dqr:
                case (int)EmunRemoteTreatedStu.jzz:
                    return true;
            }
            return false;
        }


        public async Task<TreatedApplyVO> TreatedApplyInfo(string applyId, string orgId, string? sqlsh = null)
        {
            TreatedApplyEntity ety = new TreatedApplyEntity();
            if (!string.IsNullOrWhiteSpace(applyId))
            {
                ety = await baseDal.GetFirstOrDefault(p => p.Id == applyId && p.OrganizeId == orgId && p.zt == "1");
            }
            else if (!string.IsNullOrWhiteSpace(sqlsh))
            {
                ety = await baseDal.GetFirstOrDefault(p => p.sqlsh == sqlsh && p.OrganizeId == orgId && p.zt == "1");
            }
            if (ety == null)
            {
                throw new FailedException("Thông tin đăng ký khám chữa bệnh bất thường, vui lòng gửi lại đăng ký");
            }
            return ety.Adapt<TreatedApplyVO>();
        }
        public async Task<TreatedApplyExtendVO> TreatedApplyExtendInfo(string applyId, string orgId)
        {
            var entity = await baseDal.FindKey(applyId);
            if (entity == null || entity.zt != "1")
            {
                throw new FailedException("Thông tin đăng ký khám chữa bệnh bất thường, vui lòng gửi lại đăng ký");
            }
            if (entity.OrganizeId != orgId)
            {
                throw new FailedException("Thông tin tổ chức đăng ký khám chữa bệnh kiểm tra bất thường, vui lòng xác nhận lại đăng ký Id");
            }
            var extendVo = entity.Adapt<TreatedApplyExtendVO>();
            extendVo.ksmc = await _sysDepartmentDmn.GetNameByCode(entity.ks, entity.OrganizeId);
            extendVo.ysxm = (await _sysStaffDmn.GetStaffDeptByGh(entity.ysgh, entity.OrganizeId))?.FirstOrDefault()?.xm;
            return extendVo;
        }
    }
}
