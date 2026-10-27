using Newtouch.Sett.Domain;
using Newtouch.Sett.Infrastructure;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Newtouch.Sett.Repository
{
    // HospitalizationManage repos. Real (read-only) implementations follow the
    // Framework source SQL; write/update helpers throw explicitly (no DB writes
    // in this phase, and no fake success).

    public class HospSettlementRepo : RepositoryBase<HospSettlementEntity>, IHospSettlementRepo
    {
        public List<HospSettlementEntity> GetValidList(string zyh, string orgId)
        {
            var sql = @"SELECT * FROM zy_js a WITH (NOLOCK)
                WHERE NOT EXISTS (SELECT * FROM zy_js b WITH (NOLOCK)
                    WHERE b.OrganizeId = @orgId AND a.jsnm = b.cxjsnm)
                AND zyh = @zyh AND jszt = @jszt AND a.OrganizeId = @orgId AND zt = '1'
                ORDER BY CreateTime";
            return SettDb.GetConnection().SqlQueryable<HospSettlementEntity>(sql)
                .AddParameters(new SugarParameter[] { new SugarParameter("zyh", zyh), new SugarParameter("jszt", ((int)EnumJieSuanZT.YJ).ToString()), new SugarParameter("orgId", orgId) })
                .ToList();
        }
    }

    public class HospItemBillingRepo : RepositoryBase<HospItemBillingEntity>, IHospItemBillingRepo
    {
        public IList<HospItemBillingEntity> GetItemFeeEntityListByTime(string zyh, string orgId, DateTime startTime, DateTime endTime)
        {
            var query1 = IQueryable().Where(p => p.zyh == zyh && p.OrganizeId == orgId && p.zt == "1" && p.CreateTime > startTime).ToList();
            var query2 = query1.GroupBy(p => p.cxzyjfbbh != 0 ? p.cxzyjfbbh : p.jfbbh)
                .Select(p => new HospItemBillingEntity { jfbbh = p.Key, sl = p.Sum(i => i.sl) }).ToList();
            if (query2.Count == query1.Count)
            {
                return query1.Where(p => p.CreateTime <= endTime).ToList();
            }
            query1 = query1.Where(p => p.CreateTime <= endTime).ToList();
            var query = from q2 in query2
                        join q1 in query1 on q2.jfbbh equals q1.jfbbh
                        where q1.CreateTime <= endTime && q2.sl > 0
                        select new HospItemBillingEntity
                        {
                            jfbbh = q1.jfbbh, OrganizeId = q1.OrganizeId, zyh = q1.zyh,
                            tdrq = q1.tdrq, sfxm = q1.sfxm, dl = q1.dl, ys = q1.ys,
                            ysmc = q1.ysmc, ks = q1.ks, ksmc = q1.ksmc, cw = q1.cw,
                            dj = q1.dj, sl = q2.sl, jfdw = q1.jfdw, zfbl = q1.zfbl,
                            zfxz = q1.zfxz, ssbz = q1.ssbz, ssry = q1.ssry, ssrq = q1.ssrq,
                            yzxz = q1.yzxz, yzzt = q1.yzzt, CreatorCode = q1.CreatorCode,
                            CreateTime = q1.CreateTime, LastModifyTime = q1.LastModifyTime,
                        };
            return query.ToList();
        }

        public void Updatezyaddfee(string orgId, string jfbhs, string yfbm, string userCode)
        {
            throw new NotSupportedException("Cập nhật phíghi sổ (Updatezyaddfee) chưa được chuyển đổi trong giai đoạn này.");
        }
    }

    public class HospDrugBillingRepo : RepositoryBase<HospDrugBillingEntity>, IHospDrugBillingRepo
    {
        public void ExecPartialSettleFeeDetail(string zyh, string jsnm, string czlx)
        {
            throw new NotSupportedException("Xử lý chi tiếtquyết toán giữa kỳ (ExecPartialSettleFeeDetail) chưa được chuyển đổi trong giai đoạn này.");
        }

        public void Updatezy_brxxexpand(string orgId, string zyh)
        {
            throw new NotSupportedException("Cập nhật thông tin mở rộng bệnh nhân (Updatezy_brxxexpand) chưa được chuyển đổi trong giai đoạn này.");
        }

        public void Updatezyaddfee(string orgId, decimal sl, string yfbm, string ypdm)
        {
            throw new NotSupportedException("Cập nhật phí thuốc (Updatezyaddfee) chưa được chuyển đổi trong giai đoạn này.");
        }

        public void Updatezy_wzkcReturn(string orgId, string jfbbhs, string userCode)
        {
            throw new NotSupportedException("Hoàn trả tồn kho vật tư (Updatezy_wzkcReturn) chưa được chuyển đổi trong giai đoạn này.");
        }

        public void Updatezyyz_wzkcReturn(List<HospItemBillingEntity> entityList, string orgId, string userCode)
        {
            throw new NotSupportedException("Hoàn trả tồn kho vật tư theo y lệnh (Updatezyyz_wzkcReturn) chưa được chuyển đổi trong giai đoạn này.");
        }

        public int getHckcWith(string orgId)
        {
            throw new NotSupportedException("Kiểm tra cấu hình tồn khovật tư tiêu hao (getHckcWith) chưa được chuyển đổi trong giai đoạn này.");
        }
    }

    public class HospAccountingPlanDetailRepo : RepositoryBase<HospAccountingPlanDetailEntity>, IHospAccountingPlanDetailRepo
    {
    }

    public class HospPatientBasicInfoRepo : RepositoryBase<HospPatientBasicInfoEntity>, IHospPatientBasicInfoRepo
    {
            public string GetBlhByZyh(object p0, object p1)
        {
            return null;
        }
        public object GetInpatientList(object p0, object p1, object p2)
        {
            return null;
        }
        public object IQueryable()
        {
            return null;
        }
}

    public class CqybSett05Repo : RepositoryBase<CqybSett05Entity>, ICqybSett05Repo
    {
    
        public object SaveCqybSett(object p0, object p1)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (giai đoạn 1 chỉ xác minh đọc).");
        }
    }

    public class HospMultiDiagnosisRepo : RepositoryBase<HospMultiDiagnosisEntity>, IHospMultiDiagnosisRepo
    {
        public List<HospMultiDiagnosisEntity> SelectData(string zyh, string organizeId)
        {
            const string sql = "SELECT * FROM dbo.zy_rydzd(NOLOCK) WHERE zyh=@zyh AND OrganizeId=@OrganizeId AND zt='1'";
            return SettDb.GetConnection().SqlQueryable<HospMultiDiagnosisEntity>(sql)
                .AddParameters(new SugarParameter[] { new SugarParameter("zyh", zyh), new SugarParameter("OrganizeId", organizeId) })
                .ToList();
        }
    }

    public class SysChargeTemplateItemMappRepo : RepositoryBase<SysChargeTemplateItemMappEntity>, ISysChargeTemplateItemMappRepo
    {
        public IList<MbmxsfxmVO> getmbmx(string sfmb, string orgId)
        {
            throw new NotSupportedException("Tra cứu chi tiết mẫuthu phí (getmbmx) chưa được chuyển đổi trong giai đoạn này.");
        }
    }

    public class HospSettlementGAYBZYMXXRFeeRepo : RepositoryBase<HospSettlementGAYBZYMXXRFeeEntity>, IHospSettlementGAYBZYMXXRFeeRepo
    {
        public new void Insert(HospSettlementGAYBZYMXXRFeeEntity entity)
        {
            throw new NotSupportedException("Ghi dữ liệu hoàn trả BHYT GuiAn chưa được chuyển đổi trong giai đoạn này (không ghi DB).");
        }
    }

    public class HospSimulateSettlementGAYBFeeRepo : RepositoryBase<HospSimulateSettlementGAYBFeeEntity>, IHospSimulateSettlementGAYBFeeRepo
    {
        public new void Insert(HospSimulateSettlementGAYBFeeEntity entity)
        {
            throw new NotSupportedException("Ghi kết quảquyết toán mô phỏng BHYT chưa được chuyển đổi trong giai đoạn này (không ghi DB).");
        }
    }
}
