using Newtouch.Infrastructure.EF;
using Newtouch.Core.Common;
using NewtouchHIS.Lib.Base.Model;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Common;
using SqlSugar;
using System.Linq;
using System.Text;
using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Infrastructure;
using NewtouchHIS.Lib.Base.Exceptions;


namespace Newtouch.HIS.DomainServices
{
    /// <summary>
    /// Lĩnh nội bộ
    /// </summary>
    public class RequisitionDmnService : DmnServiceBase, IRequisitionDmnService
    {
        private readonly ISysPharmacyDepartmentMedicineRepo _sysPharmacyDepartmentMedicineRepo;

        
        /// <summary>
        /// Lĩnh mới Gửi và lưu
        /// </summary>
        /// <param name="sldh">Số đơn lĩnh</param>
        /// <param name="fybmCode">Bộ phận phát thuốc</param>
        /// <param name="slbmCode">Bộ phận lĩnh（Bộ phận nhà thuốc người dùng đăng nhập hiện tại）</param>
        /// <param name="mxList">Chi tiết thuốc lĩnh</param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public bool SubmitRequisition(string sldh, string fybmCode, string slbmCode, IList<RequisitionDepartmentMedicineSubmitItemVO> mxList, string orgId)
        {
            using (var db = new EFDbTransaction().BeginTrans())
            {
                var mainEntity = new SysMedicineRequisitionEntity
                {
                    OrganizeId = orgId,
                    Sldh = sldh,
                    Slbm = slbmCode,
                    Ckbm = fybmCode,
                    ffzt = (int)EnumSLDDeliveryStatus.None,
                };
                mainEntity.Create(true);
                db.Insert(mainEntity);

                foreach (var mx in mxList)
                {
                    if (!db.IQueryable<SysPharmacyDepartmentMedicineEntity>().Any(p => p.yfbmCode == slbmCode && p.Ypdm == mx.ypCode))
                    {
                        throw new FailedException("Không thể lĩnh，Vui lòng hoàn thiện thông tin thuốc bộ phận này trước，Mã thuốc：" + mx.ypCode);
                    }
                    var mxEntity = new SysMedicineRequisitionDetailEntity
                    {
                        sldId = mainEntity.sldId,
                        ypCode = mx.ypCode,
                        //Yxrq = mx.Yxq,
                        Zhyz = mx.bbmzhyz,  //Hệ số chuyển đổi của bộ phận phát thuốc
                        //ph = "",
                        //pc = "",
                        slsl = mx.slsl,    //Chuyển thành đơn vị nhỏ nhất，lưu nó
                        yfsl = 0,   //Số lượng đã phát
                    };
                    mxEntity.Create(true);
                    db.Insert(mxEntity);
                }
                db.Commit();
            }
            return true;
        }

        /// <summary>
        /// Đơn lĩnh nội bộ Truy vấn
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="orgId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="sldh"></param>
        /// <param name="slbm">Bộ phận lĩnh</param>
        /// <param name="ckbm">Bộ phận xuất kho</param>
        /// <param name="ffzt">Trạng thái phát</param>
        /// <param name="allUseableFfzt"></param>
        /// <returns></returns>
        public IList<RequisitionSelectVO> RequisitionSearch(Pagination pagination, string orgId, DateTime? startDate, DateTime? endDate
            , string sldh, string slbm, string ckbm, int? ffzt, string allUseableFfzt)
        {
            var sb = new StringBuilder(@"
select sld.sldId,sld.Sldh,sld.CreateTime,sld.ffzt ,slyfbm.yfbmmc slbmmc,ckyfbm.yfbmmc ckbmmc
from xt_yp_sld(nolock) sld
left join [NewtouchHIS_Base]..V_S_xt_yfbm(nolock) slyfbm on slyfbm.yfbmCode = sld.Slbm and slyfbm.OrganizeId = sld.OrganizeId 
left join [NewtouchHIS_Base]..V_S_xt_yfbm(nolock) ckyfbm on ckyfbm.yfbmCode = sld.Slbm and ckyfbm.OrganizeId = sld.OrganizeId
where sld.zt = '1' 
and sld.OrganizeId =@Organizeid
");
            var par = new List<SugarParameter>();
            if (startDate.HasValue)
            {
                sb.Append(" and sld.CreateTime >= @qsrj");
                startDate = startDate < Constants.MinDateTime ? Constants.MinDateTime : startDate;
                par.Add(new SugarParameter("@qsrj", startDate.Value));
            }
            if (endDate.HasValue)
            {
                sb.Append(" and sld.CreateTime <= @jsrj");
                endDate = endDate < Constants.MinDateTime ? Constants.MinDateTime : endDate;
                par.Add(new SugarParameter("@jsrj", endDate.Value));
            }
            if (!string.IsNullOrWhiteSpace(sldh))
            {
                sb.Append(" and sld.Sldh like @sldh");
                par.Add(new SugarParameter("@sldh", "%" + sldh + "%"));
            }
            if (!string.IsNullOrWhiteSpace(slbm))
            {
                sb.Append(" and sld.Slbm like @slbm");
                par.Add(new SugarParameter("@slbm", "%" + slbm + "%"));
            }
            if (!string.IsNullOrWhiteSpace(ckbm))
            {
                sb.Append(" and sld.Ckbm = @ckbm");
                par.Add(new SugarParameter("@ckbm", ckbm));
            }
            if (ffzt.HasValue)
            {
                sb.Append(" and sld.ffzt = @ffzt");
                par.Add(new SugarParameter("@ffzt", ffzt));
            }
            if (!string.IsNullOrWhiteSpace(allUseableFfzt))
            {
                sb.AppendFormat(" and sld.ffzt in ({0})", allUseableFfzt);
            }
            par.Add(new SugarParameter("@Organizeid", orgId));
            return QueryWithPage<RequisitionSelectVO>(sb.ToString(), pagination, par.ToArray());
        }

        /// <summary>
        /// Đơn lĩnh nội bộ Truy vấn chi tiết
        /// </summary>
        /// <param name="sldId"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public IList<RequisitionSelectDetailVO> RequisitionDetailListBySlId(string sldId, string orgId)
        {
            if (string.IsNullOrWhiteSpace(sldId))
            {
                throw new FailedException("Thiếu tham Số：sldId");
            }
            var strSql = new StringBuilder(@"
SELECT yp.ypmc ypmc, sldmx.ph, sldmx.pc, sldmx.Yxrq, 
FLOOR(sldmx.slsl) slsl, 
yp.bzdw slsldw,
FLOOR(sldmx.yfsl) yfsl,
yp.bzdw yfsldw
FROM XT_YP_SLDMX(nolock) sldmx
INNER JOIN dbo.xt_yp_sld(NOLOCK) sld ON sld.sldId=sldmx.sldId
INNER JOIN [NewtouchHIS_Base]..V_S_xt_yfbm yfbm ON yfbm.yfbmCode=sld.Slbm and  yfbm.OrganizeId = sld.OrganizeId
INNER JOIN NewtouchHIS_Base.dbo.xt_yp(nolock) yp on sldmx.ypCode = yp.ypCode and yp.OrganizeId = sld.OrganizeId AND yp.zt='1'
INNER JOIN NewtouchHIS_Base.dbo.xt_ypsx(NOLOCK) ypsx ON ypsx.ypId=yp.ypId AND ypsx.zt='1'
WHERE sldmx.SldId = @sldId
AND sld.OrganizeId=@OrganizeId
");
            SugarParameter[] par = {
                new SugarParameter("@sldId",sldId),
                new SugarParameter("@OrganizeId",orgId)
            };
            return FindList<RequisitionSelectDetailVO>(strSql.ToString(), par);
        }

    }
}
