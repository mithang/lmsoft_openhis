using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Core.Common;
using Newtouch.Core.Common.Exceptions;
using Newtouch.Domain.DTO;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.ValueObjects;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;

namespace Newtouch.DomainServices
{
    /// <summary>
    /// Liên quan đến kho trung gian
    /// </summary>
    public class IDBDmnService : DmnServiceBase, IIDBDmnService
    {
        public IDBDmnService(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        /// <summary>
        /// Tra cứu thông tin đặt lịch của bệnh nhân
        /// </summary>
        /// <param name="ksCode"></param>
        /// <param name="ysgh">//Lọc số chuyên gia, chỉ xem số của mình</param>
        /// <param name="mjzbz">//1 Khám ngoại trú thông thường 2 Cấp cứu 3 Khám chuyên gia</param>
        /// <param name="jiuzhenbz"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IList<TreatmentEntity> GetRegistrationInfoList(Pagination pagination, string orgId, string ksCode, string ysgh, string mjzbz, int jiuzhenbz, string keyword)
        {
            if (jiuzhenbz < 0)
            {
                throw new FailedException("就诊标志为空");
            }
            if (jiuzhenbz == (int)EnumJzzt.NotYetTreate)
            {
                jiuzhenbz = 0;  //Tương ứng với kho trung gian đang chờ khám 0
            }

            const  string sqlstr = @"
SELECT  --CAST(NEWID() AS VARCHAR(50)) jzId ,
	@orgId OrganizeId ,
	zjk.RegID mzh ,
	1 mjzbz,--需要修改确认
	xxk.blh ,
	zjk.PatName xm ,
		CASE zjk.PatSex
		WHEN 1 THEN '男'
		WHEN 2 THEN '女'
		ELSE '不详'
	END xb ,
	CONVERT(DATETIME, zjk.PatBirth, 112) csny,
	zjk.PatTypeName brxzmc ,
	zjk.PatTypeCode brxzCode,
	1 zjlx ,
	zjk.PatIDNum zjh ,
	zjk.RegDeptName ghksmc ,
	zjk.DoctorName ghys ,
	CONVERT(DATETIME, zjk.RegDateTime, 0) ghsj ,
	CONVERT(DATETIME, zjk.InsertDateTime, 0) ghczsj ,
	--待就诊 中间库jzzt 0，CIS 1   --else 异常
	case zjk.jzzt when 0 then 1 else -1 end jzzt
FROM NNHIS_INTERFACE_Basic.dbo.MZ_REGISTER_HIS(NOLOCK) zjk
     LEFT JOIN NNHIS_INTERFACE_Basic.dbo.ZY_BRXXK(NOLOCK) xxk ON zjk.PatID = xxk.patid
WHERE DATEDIFF(HOUR, CAST(zjk.RegDateTime AS DATETIME), GETDATE()) < 24 
AND ( zjk.RegDeptCode IN (select col from [dbo].f_split(@kscode, ',')) OR ISNULL(@kscode, '') = '')
AND ( zjk.DoctorCode = @ysgh OR ISNULL(@ysgh, '') = '')
AND ( zjk.jzzt = @jzbz)
AND ( ISNULL(@mjzbz, '') = ''OR '1' = @mjzbz)
AND (@keyword = '%%' OR zjk.PatName LIKE @keyword
	OR xxk.blh LIKE @keyword
	OR zjk.RegID LIKE @keyword
	OR zjk.PatID LIKE @keyword
	OR xxk.py LIKE @keyword
)
";
            var par = new DbParameter[]
            {
                new SqlParameter("@keyword", "%" + keyword.Trim() + "%"),
                new SqlParameter("@kscode", ksCode),
                new SqlParameter("@ysgh", ysgh),
                new SqlParameter("@orgId", orgId),
                new SqlParameter("@mjzbz", mjzbz),
                new SqlParameter("@jzbz", jiuzhenbz)
            };
            var voList = this.QueryWithPage<SysIDBVO>(sqlstr.ToString(), pagination, par);
            var resultList = new List<TreatmentEntity>();
            foreach (var t in voList)
            {
                var entity = new TreatmentEntity();
                t.MapperTo(entity);
                resultList.Add(entity);
            }
            return resultList;
        }

        /// <summary>
        /// Đồng bộ thông tin đơn thuốc chưa thu phí của mzh tới kho trung gian   
        /// (Và sau khi đồng bộ đơn thuốc thành công phải ghi lại trạng thái đồng bộ đơn thuốc)
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="mzh"></param>
        /// <returns></returns>
        public void SyncTo(string orgId, string mzh,string cfId= null)
        {
            if (string.IsNullOrWhiteSpace(orgId))
            {
                throw new FailedException("组织机构为空");
            }
            if (string.IsNullOrWhiteSpace(mzh))
            {
                throw new FailedException("门诊号为空");
            }
            var par = new List<SqlParameter>();
            par.Add(new SqlParameter("@orgId", orgId));
            par.Add(new SqlParameter("@mzh", mzh));
            _databaseFactory.Get().Database.ExecuteSqlCommand("EXEC dbo.skd_Syncmzcf @orgId=@orgId,@mzh=@mzh ", par.ToArray());
        }

        /// <summary>
        /// Hủy tất cả đơn thuốc của một lần khám trong his
        /// </summary>
        /// <param name="jzId"></param>
        /// <param name="orgId"></param>
        public void ObsoleteAllPresToHIS(string jzId, string orgId)
        {
            if (string.IsNullOrWhiteSpace(orgId))
            {
                throw new FailedException("组织机构为空");
            }
            if (string.IsNullOrWhiteSpace(jzId))
            {
                throw new FailedException("就诊号为空");
            }
            var par = new List<SqlParameter>();
            par.Add(new SqlParameter("@orgId", orgId));
            par.Add(new SqlParameter("@jzId", jzId));
            _databaseFactory.Get().Database.ExecuteSqlCommand("EXEC dbo.skd_SynAllPres @orgId=@orgId,@jzId=@jzId ", par.ToArray());
        }

        /// <summary>
        /// Hủy đơn thuốc đơn lẻ của HIS
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="mzh"></param>
        /// <param name="cfId"></param>
        /// <param name="apicflx"></param>
        /// <param name="apicfh"></param>
        public void cancelSinglePresToHIS(string orgId, string mzh, string cfId
            , int apicflx, string apicfh)
        {
            if (string.IsNullOrWhiteSpace(orgId))
            {
                throw new FailedException("组织机构为空");
            }
            if (string.IsNullOrWhiteSpace(cfId)&& string.IsNullOrWhiteSpace(apicfh))
            {
                throw new FailedException("缺少处方信息");
            }
            var par = new List<SqlParameter>();
            par.Add(new SqlParameter("@orgId", orgId));
            par.Add(new SqlParameter("@cfId", cfId));
            par.Add(new SqlParameter("@mzh", mzh));
            par.Add(new SqlParameter("@cfh", apicfh));
            _databaseFactory.Get().Database.ExecuteSqlCommand("EXEC dbo.skd_SynSinglePres @orgId=@orgId,@mzh=@mzh,@cfId=@cfId,@cfh=@cfh ", par.ToArray());
        }


        /// <summary>
        /// Dữ liệu đơn thuốc CIS
        /// </summary>
        /// <param name="Opt"></param>
        /// <param name="Brwym"></param>
        /// <param name="Zdlx"></param>
        /// <param name="Brxz"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<PrescriptionDto> GetCisPrescription(string Opt, string Brwym, string Zdlx, string Brxz, DateTime? startDate, DateTime? endDate)
        {
            var inParameters = new List<SqlParameter>();
            inParameters.Add(new SqlParameter("@Opt", Opt));
            inParameters.Add(new SqlParameter("@Brwym", Brwym));
            inParameters.Add(new SqlParameter("@Zdlx", Zdlx??""));
            inParameters.Add(new SqlParameter("@Brxz", Brxz));
            inParameters.Add(new SqlParameter("@startDate", startDate));
            inParameters.Add(new SqlParameter("@endDate", endDate));
           return this.FindList<PrescriptionDto>("exec skd_QhdSvscis @Opt,@Brwym,@Zdlx,@Brxz,@startDate,@endDate", inParameters.ToArray());

        }
        /// <summary>
        /// Kho trung gian đồng bộ trạng thái thu phí đơn thuốc
        /// </summary>
        /// <param name="blh"></param>
        /// <param name="orgId"></param>
        public void Refreshcfsfbz(string blh,string orgId)
        {
            if (string.IsNullOrWhiteSpace(blh))
            {
                throw new FailedException("缺少病历号");
            }
            var par = new List<SqlParameter>();
            par.Add(new SqlParameter("@orgId", orgId));
            par.Add(new SqlParameter("@blh", blh));
            _databaseFactory.Get().Database.ExecuteSqlCommand("EXEC dbo.skd_SyncRefreshcfsfbz @orgId=@orgId,@blh=@blh ", par.ToArray());

        }
    }
}
