

using NewtouchHIS.Lib.Base.Model;
using Newtouch.CIS.Compat;
using Newtouch.Domain.DTO;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.ValueObjects;
using Newtouch.Infrastructure;
using NewtouchHIS.Lib.Base.Extension;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;

namespace Newtouch.DomainServices
{
    /// <summary>
    /// tronggiữakholiên quan
    /// </summary>
    public class IDBDmnService : DmnServiceBase, IIDBDmnService
    {
        public IDBDmnService() : base()
        {
        }

        /// <summary>
        /// Tra cứuBệnh nhânđăng ký khámthông tin
        /// </summary>
        /// <param name="ksCode"></param>
        /// <param name="ysgh">//Lọcchuyên giasố chỉ xemtreotựtựsốcủa</param>
        /// <param name="mjzbz">//1thông thườngNgoại trú 2gấpkhám 3chuyên giaNgoại trú</param>
        /// <param name="jiuzhenbz"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IList<TreatmentEntity> GetRegistrationInfoList(Pagination pagination, string orgId, string ksCode, string ysgh, string mjzbz, int jiuzhenbz, string keyword)
        {
            if (jiuzhenbz < 0)
            {
                throw new FailedException("Khám bệnhCờlàtrống");
            }
            if (jiuzhenbz == (int)EnumJzzt.NotYetTreate)
            {
                jiuzhenbz = 0;  //tương ứngtronggiữakhochờKhám bệnh0
            }

            const  string sqlstr = @"
SELECT  --CAST(NEWID() AS VARCHAR(50)) jzId ,
	@orgId OrganizeId ,
	zjk.RegID mzh ,
	1 mjzbz,--cầnsửaxác nhận
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
	--Chờ khám tronggiữakhojzzt 0，CIS 1   --else bất thường
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
        /// sẽmzhcủaChưa thu phíĐơn thuốcđợithông tin Đồng bộđếntronggiữakho   
        /// （vàĐồng bộĐơn thuốcthành côngsaucầnvềviếtĐơn thuốcTrạng thái đồng bộ）
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="mzh"></param>
        /// <returns></returns>
        public void SyncTo(string orgId, string mzh,string cfId= null)
        {
            if (string.IsNullOrWhiteSpace(orgId))
            {
                throw new FailedException("Tổ chứclàtrống");
            }
            if (string.IsNullOrWhiteSpace(mzh))
            {
                throw new FailedException("Số khám ngoại trúlàtrống");
            }
            var par = new List<SqlParameter>();
            par.Add(new SqlParameter("@orgId", orgId));
            par.Add(new SqlParameter("@mzh", mzh));
            ExecuteSqlCommand("EXEC dbo.skd_Syncmzcf @orgId=@orgId,@mzh=@mzh ", par.ToArray());
        }

        /// <summary>
        /// Hủy bỏhisMột lầnKhám bệnhcủatất cảĐơn thuốc
        /// </summary>
        /// <param name="jzId"></param>
        /// <param name="orgId"></param>
        public void ObsoleteAllPresToHIS(string jzId, string orgId)
        {
            if (string.IsNullOrWhiteSpace(orgId))
            {
                throw new FailedException("Tổ chứclàtrống");
            }
            if (string.IsNullOrWhiteSpace(jzId))
            {
                throw new FailedException("Khám bệnhsốlàtrống");
            }
            var par = new List<SqlParameter>();
            par.Add(new SqlParameter("@orgId", orgId));
            par.Add(new SqlParameter("@jzId", jzId));
            ExecuteSqlCommand("EXEC dbo.skd_SynAllPres @orgId=@orgId,@jzId=@jzId ", par.ToArray());
        }

        /// <summary>
        /// Hủy bỏHISphiếutờĐơn thuốc
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
                throw new FailedException("Tổ chứclàtrống");
            }
            if (string.IsNullOrWhiteSpace(cfId)&& string.IsNullOrWhiteSpace(apicfh))
            {
                throw new FailedException("thiếuĐơn thuốcthông tin");
            }
            var par = new List<SqlParameter>();
            par.Add(new SqlParameter("@orgId", orgId));
            par.Add(new SqlParameter("@cfId", cfId));
            par.Add(new SqlParameter("@mzh", mzh));
            par.Add(new SqlParameter("@cfh", apicfh));
            ExecuteSqlCommand("EXEC dbo.skd_SynSinglePres @orgId=@orgId,@mzh=@mzh,@cfId=@cfId,@cfh=@cfh ", par.ToArray());
        }

        /// <summary>
        /// CISĐơn thuốcDữ liệu
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
        /// tronggiữakho Đồng bộĐơn thuốcThu phíTrạng thái
        /// </summary>
        /// <param name="blh"></param>
        /// <param name="orgId"></param>
        public void Refreshcfsfbz(string blh,string orgId)
        {
            if (string.IsNullOrWhiteSpace(blh))
            {
                throw new FailedException("thiếuSố bệnh án");
            }
            var par = new List<SqlParameter>();
            par.Add(new SqlParameter("@orgId", orgId));
            par.Add(new SqlParameter("@blh", blh));
            ExecuteSqlCommand("EXEC dbo.skd_SyncRefreshcfsfbz @orgId=@orgId,@blh=@blh ", par.ToArray());

        }
    }
}
