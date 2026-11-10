using NewtouchHIS.Lib.Framework.Operator;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Exceptions;
using Newtouch.Domain.ValueObjects;
using Newtouch.EMR.Domain.BusinessObjects;
using Newtouch.EMR.Domain.DTO.OutputDto.MRUpload;
using Newtouch.EMR.Domain.Entity;
using Newtouch.EMR.Domain.IDomainServices;
using Newtouch.EMR.Domain.IRepository;
using Newtouch.EMR.Domain.ValueObjects;
using Newtouch.EMR.Domain.ValueObjects.MedicalRecord;
using Newtouch.EMR.Infrastructure;
using Newtouch.Infrastructure;
using NewtouchHIS.Lib.Base.Extension;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using SqlSugar;
using Newtouch.EMR.Repository;

namespace Newtouch.EMR.DomainServices
{
    public class NursingRecordDmnService : RepositoryBase<bl_hljl_ybEntity>, INursingRecordDmnService
    {

        

        public NursingRecordDmnService()
        {
        }
        /// <summary>
        /// Lấy thông tin cây tất cả bệnh nhân
        /// </summary>
        /// <param name="staffId"></param>
        /// <returns></returns>
        public IList<InpWardPatTreeVO> GetPatTree(string orgId, string zyzt, string keyword)
        {
            string sql = @"select  distinct b.zyh,b.xm hzxm,b.wardCode bqCode,c.bqmc,b.sex,b.zybz,
CAST(FLOOR(DATEDIFF(DY, b.birth, GETDATE()) / 365.25) AS VARCHAR(5)) nl,convert(varchar(50),isnull(b.rqrq,getdate()),120) rqrq,convert(varchar(50),isnull(b.cqrq,getdate()),120) cqrq
from [Newtouch_CIS]..zy_brxxk b with(nolock)  
left join [NewtouchHIS_Base].[dbo].[V_S_xt_bq] c with(nolock) on c.bqcode=b.wardCode and c.OrganizeId=@orgId and c.zt='1'
                where 1=1 and b.zt='1' ";
            var par = new List<SugarParameter>();
            par.Add(new SugarParameter("@orgId", orgId));
            if (!string.IsNullOrWhiteSpace(zyzt))
            {
                switch (zyzt)
                {
                    case "zy"://Đang nằm viện:không phải đã ra viện,không phải bản ghi đã hủy,không phải hủy nhập viện
                        sql += " and b.zybz in (SELECT * FROM dbo.f_split(@zybz, ','))";
                        par.Add(new SugarParameter("@zybz", (int)EnumZYBZ.Bqz));
                        break;
                    case "cy"://Ra viện:Trạng thái đã ra viện
                        sql += " and b.zybz in (SELECT * FROM dbo.f_split(@zybz, ','))";
                        par.Add(new SugarParameter("@zybz", (int)EnumZYBZ.Ycy + "," + (int)EnumZYBZ.Djz));
                        break;
                }
            }
            else
            {
                sql += " and b.zybz<>'9'";
            }
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                sql += " and (zyh like @keyword or xm like @keyword)";
                par.Add(new SugarParameter("@keyword", "%" + keyword + "%"));
            }
            return this.FindList<InpWardPatTreeVO>(sql, par.ToArray());
        }
        public InpWardPatTreeVO GetPatList(string orgId, string zyh)
        {
            string sql = @"select  distinct b.zyh,b.xm hzxm,b.wardCode bqCode,c.bqmc,b.sex,b.zybz,
CAST(FLOOR(DATEDIFF(DY, b.birth, GETDATE()) / 365.25) AS VARCHAR(5)) nl,convert(varchar(50),isnull(b.rqrq,getdate()),120) rqrq,convert(varchar(50),isnull(b.cqrq,getdate()),120) cqrq
from [Newtouch_CIS]..zy_brxxk b with(nolock)  
left join [NewtouchHIS_Base].[dbo].[V_S_xt_bq] c with(nolock) on c.bqcode=b.wardCode and c.OrganizeId=@orgId and c.zt='1'
                where 1=1 and b.zt='1' ";
            var par = new List<SugarParameter>();
            par.Add(new SugarParameter("@orgId", orgId));
            if (!string.IsNullOrWhiteSpace(zyh))
            {
                sql += " and (zyh = @zyh)";
                par.Add(new SugarParameter("@zyh",zyh));
            }
            return this.FirstOrDefault<InpWardPatTreeVO>(sql, par.ToArray());
        }
        /// <summary>
        /// Lưu biểu mẫu(Thêm mới,Sửa)
        /// </summary>
        /// <param name="entity">Đối tượng thực thể</param>
        /// <param name="keyValue">Giá trị khóa chính</param>
        public void SubmitForm(bl_hljl_ybEntity entity, string keyValue)
        {
            // G18: entity null (binder không tạo được instance) / thiếu
            // zyh/rq/sj trước đây chạy tới Insert rồi lỗi SQL thô.
            if (entity == null)
            {
                throw new FailedException("Dữ liệu phiếu điều dưỡng không được để trống.");
            }
            if (string.IsNullOrWhiteSpace(entity.zyh) || string.IsNullOrWhiteSpace(entity.rq) || string.IsNullOrWhiteSpace(entity.sj))
            {
                throw new FailedException("Số nhập viện, ngày và giờ không được để trống.");
            }
            if (!string.IsNullOrEmpty(keyValue))
            {
                if (this.IQueryable(p => p.zyh == entity.zyh && p.rq == entity.rq && p.sj == entity.sj && p.Id != keyValue && p.zt == "1").Any())
                {
                    throw new FailedException("InpatientVitalSigns_Repeated_Error", "Nhập trùng lặp");
                }
                var dbEntity = this.FindEntity(keyValue);
                // G18: keyValue bogus trước đây NRE ở dbEntity.OrganizeId.
                if (dbEntity == null)
                {
                    throw new FailedException("Không tìm thấy phiếu điều dưỡng.");
                }
                if (dbEntity.OrganizeId != entity.OrganizeId)
                {
                    throw new FailedException("Error", "Thao tác bất thường");
                }
                //properties
                var ignoreProps = new List<string>() { "Id", "CreateTime", "CreatorCode" };
                // G18: MapperTo thường copy cả null — form điều dưỡng không
                // post gms/dghl/jybq/hlgcjg/hlczjg/hsqmrqsj nên mọi lần sửa
                // đều wipe 6 cột này về NULL (đã chứng minh live ở base).
                // Merge bỏ-null: field không post giữ giá trị cũ.
                entity.MapperToSkipNull(dbEntity, ignoreProps);
                dbEntity.LastModifyTime = DateTime.Now;
                try { dbEntity.LastModifierCode = OperatorProvider.GetCurrent()?.UserCode ?? ""; } catch { dbEntity.LastModifierCode = ""; }
                this.Update(dbEntity);
            }
            else
            {
                if (this.IQueryable(p => p.zyh == entity.zyh && p.rq == entity.rq && p.sj == entity.sj && p.zt == "1").Any())
                {
                    throw new FailedException("InpatientVitalSigns_Repeated_Error", "Nhập trùng lặp");
                }
                // G18: Insert trước đây không set Id/CreateTime/CreatorCode
                // nên tạo mới LUÔN lỗi SQL thô "Cannot insert NULL into
                // column 'CreatorCode'" (đã chứng minh live ở base — cả 3
                // form yb/wz/ss đều không tạo được dòng mới). Set đủ như
                // mẫu MedicalRulesDmnService.SubmitForm.
                entity.Id = Guid.NewGuid().ToString();
                entity.CreateTime = DateTime.Now;
                try { entity.CreatorCode = OperatorProvider.GetCurrent()?.UserCode ?? ""; } catch { entity.CreatorCode = ""; }
                this.Insert(entity);
            }
        }
        /// <summary>
        /// Truy vấn phân trang điều dưỡng
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="orgId"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public IList<bl_hljl_ybEntity> GetPaginationList(Pagination pagination, string orgId, DateTime? kssj, DateTime? jssj, string zyh, string wardCode, string isShowDelete)
        {
            string sql = @"select * from bl_hljl_yb where createtime>=@kssj  and createtime<=@jssj and OrganizeId=@orgId and zt='1'";
            // FIX-2 C2-B5: khong co kssj/jssj thi .Value nem 'Nullable object
            // must have a value'. UI luon gui ngay o luong chinh; khong ngay
            // thi tra luoi rong dung dang thay vi crash.
            if (!kssj.HasValue || !jssj.HasValue)
                return new List<bl_hljl_ybEntity>();
            var pars = new List<SugarParameter>();
            pars.Add(new SugarParameter("@orgId", orgId));
            pars.Add(new SugarParameter("@kssj", kssj.Value.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)));
            pars.Add(new SugarParameter("@jssj", jssj.Value.AddDays(1).ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)));
            if (!string.IsNullOrWhiteSpace(zyh))
            {
                sql += " and zyh=@zyh";
                pars.Add(new SugarParameter("@zyh", zyh.Trim()));
            }
            
            return this.FindList<bl_hljl_ybEntity>(sql, pars.ToArray());

        }
        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="keyValue">Giá trị khóa chính</param>
        public void DeleteForm(string keyValue, string orgId)
        {
            var dbEntity = this.FindEntity(keyValue);
            // G18: keyValue bogus/rỗng trước đây NRE ở dbEntity.zt; không
            // filter OrganizeId nên xóa (zt=0) được dòng của org khác
            // (đã chứng minh live ở base: dòng XORG bị flip + Success giả).
            if (dbEntity == null)
            {
                throw new FailedException("Không tìm thấy phiếu điều dưỡng.");
            }
            if (dbEntity.OrganizeId != orgId)
            {
                throw new FailedException("Error", "Thao tác bất thường");
            }
            dbEntity.zt = "0";  //Không hợp lệ
            dbEntity.LastModifyTime = DateTime.Now;
            try { dbEntity.LastModifierCode = OperatorProvider.GetCurrent()?.UserCode ?? ""; } catch { dbEntity.LastModifierCode = ""; }
            this.Update(dbEntity);
        }
        /// <summary>
        /// Ghi lại toàn bộ lượng đưa vào (srsclx='0') cho một lượt điều dưỡng:
        /// xóa (zt=0) các dòng cũ rồi chèn các dòng mới TRONG MỘT transaction.
        /// G18: trước đây DeleteSrl chạy ngoài transaction, mỗi SubmitSrl có
        /// transaction riêng — lỗi ở dòng sau (vd tên quá dài) để lại dữ liệu
        /// dở dang (dòng cũ đã xóa + dòng mới chèn dở); và Insert không set
        /// Id/CreateTime/CreatorCode/zt nên KHÔNG chèn được dòng nào (lỗi SQL
        /// thô "Cannot insert NULL into column 'Id'" — đã chứng minh live ở
        /// base, cả SubmitSrl lẫn SubmitScl đều hỏng hoàn toàn).
        /// </summary>
        public void SaveSrlBatch(IList<string[]> segments, string orgId, string zyh, string rq, string sj, string bllx, string user)
        {
            // G18: RepositoryBase của EMR không có IQueryable<T> generic
            // nên check bằng COUNT thô (cùng EmrDb, tham số hóa).
            var pat = this.FirstOrDefault<ZybrjbxxEntity>("SELECT TOP 1 Id FROM zy_brjbxx WHERE zyh=@zyh AND OrganizeId=@orgId AND zt='1'",
                new SugarParameter[] { new SugarParameter("@zyh", zyh), new SugarParameter("@orgId", orgId) });
            if (pat == null)
            {
                throw new FailedException("Không tìm thấy bệnh nhân.");
            }
            using (var db = new EmrTransaction().BeginTrans())
            {
                string sql = @"update bl_hljl_srscl set zt='0',LastModifyTime=@date,LastModifierCode=@user where OrganizeId=@orgId and srsclx='0' and zyh=@zyh and rq=@rq and sj=@sj and bllx=@bllx";
                var pars = new List<SugarParameter>();
                pars.Add(new SugarParameter("@orgId", orgId));
                pars.Add(new SugarParameter("@zyh", zyh));
                pars.Add(new SugarParameter("@rq", rq));
                pars.Add(new SugarParameter("@sj", sj));
                pars.Add(new SugarParameter("@bllx", bllx));
                pars.Add(new SugarParameter("@date", DateTime.Now));
                pars.Add(new SugarParameter("@user", user ?? ""));
                db.ExecuteCommand(sql, pars.ToArray());
                foreach (var parts in segments)
                {
                    bl_hljl_srsclEntity srscl = new bl_hljl_srsclEntity();
                    srscl.Id = Guid.NewGuid().ToString();
                    srscl.mc = parts[0];
                    srscl.ml = parts[1];
                    srscl.dw = parts[2];
                    srscl.tj = parts[3];
                    srscl.zyh = zyh;
                    srscl.rq = rq;
                    srscl.sj = sj;
                    srscl.bllx = bllx; //0 Thông thường 1 Nguy kịch 2 Phẫu thuật
                    srscl.srsclx = "0";//Lượng đưa vào 0 Lượng thải ra 1
                    srscl.OrganizeId = orgId;
                    srscl.CreateTime = DateTime.Now;
                    srscl.CreatorCode = user ?? "";
                    srscl.zt = "1";
                    db.Insert(srscl);
                }
                db.Commit();
            }
        }

        /// <summary>
        /// Như SaveSrlBatch nhưng cho lượng thải ra (srsclx='1').
        /// </summary>
        public void SaveSclBatch(IList<string[]> segments, string orgId, string zyh, string rq, string sj, string bllx, string user)
        {
            // G18: RepositoryBase của EMR không có IQueryable<T> generic
            // nên check bằng COUNT thô (cùng EmrDb, tham số hóa).
            var pat = this.FirstOrDefault<ZybrjbxxEntity>("SELECT TOP 1 Id FROM zy_brjbxx WHERE zyh=@zyh AND OrganizeId=@orgId AND zt='1'",
                new SugarParameter[] { new SugarParameter("@zyh", zyh), new SugarParameter("@orgId", orgId) });
            if (pat == null)
            {
                throw new FailedException("Không tìm thấy bệnh nhân.");
            }
            using (var db = new EmrTransaction().BeginTrans())
            {
                string sql = @"update bl_hljl_srscl set zt='0',LastModifyTime=@date,LastModifierCode=@user where OrganizeId=@orgId and srsclx='1' and zyh=@zyh and rq=@rq and sj=@sj and bllx=@bllx";
                var pars = new List<SugarParameter>();
                pars.Add(new SugarParameter("@orgId", orgId));
                pars.Add(new SugarParameter("@zyh", zyh));
                pars.Add(new SugarParameter("@rq", rq));
                pars.Add(new SugarParameter("@sj", sj));
                pars.Add(new SugarParameter("@bllx", bllx));
                pars.Add(new SugarParameter("@date", DateTime.Now));
                pars.Add(new SugarParameter("@user", user ?? ""));
                db.ExecuteCommand(sql, pars.ToArray());
                foreach (var parts in segments)
                {
                    bl_hljl_srsclEntity srscl = new bl_hljl_srsclEntity();
                    srscl.Id = Guid.NewGuid().ToString();
                    srscl.mc = parts[0];
                    srscl.ml = parts[1];
                    srscl.dw = parts[2];
                    srscl.ysxz = parts[3];
                    srscl.zyh = zyh;
                    srscl.rq = rq;
                    srscl.sj = sj;
                    srscl.bllx = bllx; //0 Thông thường 1 Nguy kịch 2 Phẫu thuật
                    srscl.srsclx = "1";//Lượng đưa vào 0 Lượng thải ra 1
                    srscl.OrganizeId = orgId;
                    srscl.CreateTime = DateTime.Now;
                    srscl.CreatorCode = user ?? "";
                    srscl.zt = "1";
                    db.Insert(srscl);
                }
                db.Commit();
            }
        }
        public IList<bl_hljl_srsclEntity> GetSrlScl(string orgId, string zyh, string rq, string sj, string bllx)
        {
            string sql = @"select * from bl_hljl_srscl where OrganizeId=@orgId and zt='1' and zyh=@zyh and rq=@rq and sj=@sj and bllx=@bllx and srsclx='0'";
            var pars = new List<SugarParameter>();
            pars.Add(new SugarParameter("@orgId", orgId));
            pars.Add(new SugarParameter("@zyh", zyh));
            pars.Add(new SugarParameter("@rq", rq));
            pars.Add(new SugarParameter("@sj", sj));
            pars.Add(new SugarParameter("@bllx", bllx));
            return this.FindList<bl_hljl_srsclEntity>(sql, pars.ToArray());

        }
        public IList<bl_hljl_srsclEntity> GetScl(string orgId, string zyh, string rq, string sj, string bllx)
        {
            string sql = @"select * from bl_hljl_srscl where OrganizeId=@orgId and zt='1' and zyh=@zyh and rq=@rq and sj=@sj and bllx=@bllx and srsclx='1'";
            var pars = new List<SugarParameter>();
            pars.Add(new SugarParameter("@orgId", orgId));
            pars.Add(new SugarParameter("@zyh", zyh));
            pars.Add(new SugarParameter("@rq", rq));
            pars.Add(new SugarParameter("@sj", sj));
            pars.Add(new SugarParameter("@bllx", bllx));
            return this.FindList<bl_hljl_srsclEntity>(sql, pars.ToArray());
        }
        public patientInfoDto GetInfoByZyh(string zyh, string orgId)
        {
            var sqlstr = new StringBuilder();
            var par = new List<SugarParameter>();
            sqlstr = sqlstr.Append(@"SELECT  zyh ,  
                        xx.blh,
                        xm ,
                        sex ,
                        CAST(FLOOR(DATEDIFF(DY, birth, GETDATE()) / 365.25) AS INT) age ,
                        brxzmc ,
                        bq.bqmc ,
                        cw.cwmc ,
                         CONVERT(DATETIME, xx.rqrq, 23) ryrq,
                        xx.cqrq,
                        xx.rqrq,
                        xx.zybz
                FROM    [Newtouch_CIS]..zy_brxxk xx
                                LEFT JOIN NewtouchHIS_Base..V_S_xt_bq bq ON bq.bqCode = xx.WardCode
                                                    AND bq.OrganizeId = xx.OrganizeId
                                                    AND bq.zt = '1'
                LEFT JOIN NewtouchHIS_Base..V_S_xt_cw cw ON cw.cwCode = xx.BedCode
                                                    AND cw.OrganizeId = xx.OrganizeId
                                                    AND cw.zt = '1'
            where xx.OrganizeId=@orgId and xx.zyh=@zyh and xx.zt='1'");
            par.Add(new SugarParameter("@orgId", orgId));
            par.Add(new SugarParameter("@zyh", zyh));
            return this.FirstOrDefault<patientInfoDto>(sqlstr.ToString(), par.ToArray());
        }
    }
}
