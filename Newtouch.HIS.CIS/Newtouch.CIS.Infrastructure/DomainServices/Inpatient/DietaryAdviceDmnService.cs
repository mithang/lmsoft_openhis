

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
        /// Mặc địnhHàm khởi tạosố
        /// </summary>
        /// <param name="databaseFactory">EFtrêndướivănNhà máy</param>
        public DietaryAdviceDmnService()
            : base()
        {

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
                        throw new FailedException("ăn uốngkhôngtồn tạiở");
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
                        //vậtlýXóa
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
                throw new FailedException("Mục thu phíLưu thất bại，" + ((Newtouch.CIS.Compat.FailedException)e).Msg);
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
