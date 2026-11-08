

using Newtouch.CIS.Compat;
using Newtouch.Infrastructure.EF;
using Newtouch.Domain.DTO.InputDto.Inpatient;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.IRepository;
using Newtouch.Domain.ValueObjects.Inpatient;
using NewtouchHIS.Lib.Base.Extension;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Newtouch.DomainServices.Inpatient
{
    public class DietaryAdviceDmnService : DmnServiceBase, IDietaryAdviceDmnService
    {
        private readonly IInpatientDietBaseRepo _inpatientDietBaseRepo;
        /// <summary>
        /// Khởi tạo bằng constructor injection (NX-001, mẫu BA-003 DoctorserviceDmnService).
        /// Trước đây ctor rỗng, field readonly _inpatientDietBaseRepo không bao giờ
        /// được gán, nên SubmitService chạm tới _inpatientDietBaseRepo.IQueryable()
        /// là NullReferenceException ngay khi có dữ liệu (reqdietaryservices.Count > 0).
        /// Không giữ ctor không tham số: DI sẽ fail-fast lúc khởi động nếu thiếu
        /// đăng ký, thay vì NRE lúc chạy. Dependency đã đăng ký AddScoped trong
        /// CIS.Web/Program.cs:125 và CIS.API/Program.cs:140.
        /// </summary>
        public DietaryAdviceDmnService(IInpatientDietBaseRepo inpatientDietBaseRepo)
            : base()
        {
            _inpatientDietBaseRepo = inpatientDietBaseRepo;
        }
        public DAFormVO GetFormJson(string keyvalue)
        {
            DAFormVO vo = new DAFormVO();
            return vo;
        }

        public void SubmitService(string orgId,List<DAMXFormVO> reqdietaryservices, List<string> delData)
        {
            try
            {
                var dto = new DietaryServiceRequestDto
                {
                    DelIds = new List<string>(),
                    EditmxList = new List<InpatientDietSfxmdyEntity>()
                };
                if (delData != null && delData.Count>0)
                {
                    dto.DelIds.AddRange(delData);
                }
                if (reqdietaryservices != null && reqdietaryservices.Count > 0)
                {
                    var Id = reqdietaryservices[0].baseId;
                    var patientobj = _inpatientDietBaseRepo.IQueryable().FirstOrDefault(p => p.Id == Id && p.OrganizeId == orgId && p.zt == "1");
                    if (patientobj == null)
                    {
                        throw new FailedException("ăn uống không tồn tại ở");
                    }

                    foreach (var item in reqdietaryservices)
                    {
                        var entity = new InpatientDietSfxmdyEntity();
                        entity = item.MapperTo(entity);
                        entity.OrganizeId = orgId;
                        entity.zt = "1";
                        entity.sl = item.sl.ToInt();
                        entity.Create(true);
                        dto.EditmxList.Add(entity);
                    }
                }
                using (var db = new EFDbTransaction().BeginTrans())
                {
                    if (dto != null)
                    {
                        if (dto.EditmxList != null && dto.EditmxList.Count > 0)
                        {
                            foreach (var item in dto.EditmxList)
                            {
                                db.Insert(item);
                            }
                        }
                        //vật lý Xóa
                        if (dto.DelIds != null && dto.DelIds.Count() > 0)
                        {
                            var sql = @"UPDATE  dbo.zy_Dietsfxmdy
                                SET     zt = 0
                                WHERE   Id IN ( SELECT  *
                                                FROM    dbo.f_split(@str, ',') )
                                        AND OrganizeId = @orgId;";

                            db.ExecuteSqlCommand(sql, new[] { new SqlParameter("@orgId", orgId),
                            new SqlParameter("@str", string.Join(",", dto.DelIds))});
                        }
                    }
                    db.Commit();
                }
            }
            catch (Exception e)
            {
                // NX2-003: không ép kiểu vô điều kiện sang FailedException — e có thể
                // là NullReferenceException (vd field repo null) hay lỗi DB, khi đó
                // phép ép kiểu cũ ném InvalidCastException và che mất lỗi gốc.
                // FailedException thật giữ nguyên error shape cũ (tiền tố + Msg gốc);
                // exception khác trả đúng business error với Message gốc.
                if (e is Newtouch.CIS.Compat.FailedException failedException)
                {
                    throw new FailedException("Mục thu phí Lưu thất bại，" + failedException.Msg);
                }
                throw new FailedException("Mục thu phí Lưu thất bại，" + e.Message);
            }
        }

        public List<DAmxGridList> GetmxList(string Id, string orgId)
        {
            var sqlstr = new StringBuilder();
            var par = new List<SqlParameter>();
            sqlstr = sqlstr.Append(@"SELECT  Id ,
                                    xmdy.sfxmCode ,
                                    xmdy.sfxmmc ,
                                    xmdy.dw ,
                                    sfxm.dj price ,
                                    sfxm.gg,
		                            xmdy.sl
                            FROM    dbo.zy_Dietsfxmdy xmdy
                                    LEFT JOIN NewtouchHIS_Base..V_S_xt_sfxm sfxm ON sfxm.sfxmCode = xmdy.sfxmCode
                                                                                    AND sfxm.zt = '1'
                                                                                    AND xmdy.OrganizeId = sfxm.OrganizeId
                            WHERE   xmdy.baseId = @Id
                                    AND xmdy.zt = '1'
                                    AND xmdy.OrganizeId = @orgId;");
            par.Add(new SqlParameter("@Id", Id));
            par.Add(new SqlParameter("@orgId", orgId));
            return this.FindList<DAmxGridList>(sqlstr.ToString(), par.ToArray());
        }
    }
}
