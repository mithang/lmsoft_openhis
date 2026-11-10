using System.Collections.Generic;
using System.Text;
using Newtouch.MR.ManageSystem.Domain.Entity;
using Newtouch.MR.ManageSystem.Domain.IRepository;
using System;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SqlSugar;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base.Extension;
using DbConfigHelper;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Utilities;
using NewtouchHIS.Lib.Framework.Filter;
using NewtouchHIS.Lib.Framework.Operator;
using Newtouch.MR.ManageSystem.Infrastructure;

namespace Newtouch.MR.ManageSystem.Repository
{
    /// <summary>
    /// Tạo: chl
    /// Ngày: 2019-11-20 11:05
    /// Mô tả: Trang đầu bệnh án
    /// </summary>
    public class MrbasyRepo : RepositoryBase<MrbasyEntity>, IMrbasyRepo
    {
        /// <summary>
        /// Hàm tạo mặc định
        /// </summary>
        /// <param name="databaseFactory">Factory ngữ cảnh EF</param>
        public MrbasyRepo()
            
        {
            
        }

        /// <summary>
        /// Lưu biểu mẫu(Thêm mới, Sửa)
        /// </summary>
        /// <param name="entity">Đối tượng thực thể</param>
        /// <param name="keyValue">Giá trị khóa chính</param>
        public int SubmitForm(MrbasyEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                var dbEntity = this.FindEntity(keyValue);
                if (dbEntity == null)
                {
                    throw new FailedException("Không tìm thấy thông tin trang đầu bệnh án.");
                }
                var curOrg = NewtouchHIS.Lib.Framework.Operator.OperatorProvider.GetCurrent()?.OrganizeId ?? "";
                if (!string.IsNullOrWhiteSpace(curOrg) && dbEntity.OrganizeId != curOrg)
                {
                    throw new FailedException("Không có quyền sửa dữ liệu của tổ chức khác.");
                }
                // G22: edit truoc day bo qua entity da post (chi cham Modify)
                // nen sua khong doi du lieu; nay merge cac field da post,
                // bo qua field chua post (tien le G2/G18).
                MrmsMergeHelper.MergeSkipNull(entity, dbEntity);
                dbEntity.Modify(keyValue);
                return Update(dbEntity);
            }
            // G22: create truoc day khong kiem tra gi — thieu field NOT NULL
            // thi loi SQL tho "Cannot insert the value NULL...".
            var missing = new System.Collections.Generic.List<string>();
            CheckRequired(entity.BAH, "Số bệnh án", 100, missing);
            CheckRequired(entity.ZYH, "Số nội trú", 50, missing);
            CheckRequired(entity.XM, "Họ tên bệnh nhân", 100, missing);
            CheckRequired(entity.YLFKFS, "Cách thức nhập viện (YLFKFS)", 100, missing);
            CheckRequired(entity.ZYCS, "Số lần nhập viện", 100, missing);
            CheckRequired(entity.XB, "Giới tính", 100, missing);
            CheckRequired(entity.SFZH, "Số chứng minh thư", 100, missing);
            CheckRequired(entity.ZY, "Dân tộc", 100, missing);
            CheckRequired(entity.HY, "Hôn nhân", 100, missing);
            CheckRequired(entity.RYTJ, "Đường vào viện (RYTJ)", 100, missing);
            CheckRequired(entity.MZZD, "Chẩn đoán lúc vào viện", 200, missing);
            CheckRequired(entity.MZZDDM, "Mã chẩn đoán lúc vào viện", 100, missing);
            CheckRequired(entity.BRLY, "Nguồn bệnh nhân", 50, missing);
            CheckRequired(entity.YWGM, "Dị ứng thuốc", 100, missing);
            CheckRequired(entity.BAZL, "Loại bệnh án", 100, missing);
            CheckRequired(entity.LYFS, "Cách thức ra viện", 100, missing);
            if (entity.CSRQ == DateTime.MinValue)
            {
                missing.Add("Ngày sinh không hợp lệ");
            }
            if (entity.RYSJ == DateTime.MinValue)
            {
                missing.Add("Ngày nhập viện không hợp lệ");
            }
            if (missing.Count > 0)
            {
                throw new FailedException("Thông tin trang đầu bệnh án chưa đầy đủ: " + string.Join("; ", missing) + ".");
            }
            entity.Create(true);
            return Insert(entity);
        }

        private static void CheckRequired(string value, string label, int maxLen, System.Collections.Generic.List<string> missing)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                missing.Add(label + " không được để trống");
            }
            else if (value.Length > maxLen)
            {
                missing.Add(label + " tối đa " + maxLen + " ký tự");
            }
        }

        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="keyValue">Giá trị khóa chính</param>
        public int DeleteForm(string keyValue, string organizeId = null)
        {
            if (string.IsNullOrWhiteSpace(keyValue))
            {
                throw new FailedException("Không tìm thấy thông tin trang đầu bệnh án.");
            }
            var dbEntity = this.FindEntity(keyValue);
            if (dbEntity == null)
            {
                throw new FailedException("Không tìm thấy thông tin trang đầu bệnh án.");
            }
            var org = organizeId;
            if (string.IsNullOrWhiteSpace(org))
            {
                org = NewtouchHIS.Lib.Framework.Operator.OperatorProvider.GetCurrent()?.OrganizeId ?? "";
            }
            if (!string.IsNullOrWhiteSpace(org) && dbEntity.OrganizeId != org)
            {
                throw new FailedException("Không có quyền xóa dữ liệu của tổ chức khác.");
            }
            return Delete(p => p.Id == keyValue);
        }

        public IList<MrbasyEntity> GetPagintionHospitalList(Pagination pagination, string organizeId, string jkkh, string keyword)
        {
	        var sql = new StringBuilder();
	        sql.Append("select  * from  [dbo].[mr_basy]  where 1 = 1 and zt=1");

	        List<SugarParameter> pars = null;
	        if (!string.IsNullOrWhiteSpace(organizeId))
	        {
		        pars = pars ?? new List<SugarParameter>();
		        sql.Append(" and organizeid = @organizeId");
		        pars.Add(new SugarParameter("@organizeId", organizeId));
	        }
	        if (!string.IsNullOrWhiteSpace(jkkh))
	        {
		        pars = pars ?? new List<SugarParameter>();
		        sql.Append(" and jkkh like @jkkh");
		        pars.Add(new SugarParameter("@jkkh", "%" + jkkh.Trim() + "%"));
	        }
	        if (!string.IsNullOrWhiteSpace(keyword))
	        {
		        pars = pars ?? new List<SugarParameter>();
		        sql.Append(" and (1=2 ");
				sql.Append("or zyh like @keyword ");
				sql.Append(" or xm like @keyword )");
		        pars.Add(new SugarParameter("@keyword", "%" + keyword.Trim() + "%"));
	        }
	        return this.QueryWithPage<MrbasyEntity>(sql.ToString(), pagination, pars == null ? null : pars.ToArray());
        }




        // Ten DB lay tu DbNames (DbNames.cs, dung chung trong Infra).

        /// <summary>
        /// Sửa trạng thái lưu trữ
        /// </summary>
        /// <param name="dataId"></param>
        /// <param name="ZYH"></param>
        /// <returns></returns>
        public int Updatebazt(string dataId, string ZYH,string XM)
        {
            var GDRQ= DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var mrmsDb = DbNames.Name("MrmsDb", "Newtouch_MRMS");
            var emrDb = DbNames.Name("EmrDb", "Newtouch_EMR");
            var sql = "update  " + mrmsDb + ".[dbo].[mr_basy] set bazt='3',GDRQ=@GDRQ where Id=@dataId and ZYH=@ZYH and XM=@XM";
            SugarParameter[] para ={
                new SugarParameter("@GDRQ",GDRQ),
                new SugarParameter("@dataId",dataId),
                new SugarParameter("@ZYH",ZYH),
                new SugarParameter("@XM",XM)
                };
           var bazt1=  ExecuteSqlCommand(sql, para);
            var sql2 = "update  " + emrDb + ".[dbo].[mr_basy] set bazt='3',GDRQ=@GDRQ where  ZYH=@ZYH and XM=@XM";
            var bazt2= ExecuteSqlCommand(sql2, para);
            if (bazt1!=0 && bazt2 != 0)
            {
                return 1;
            }
            return 0;
        }
        //Hủy trạng thái bệnh án
        public int CXUpdatebazt(string dataId, string ZYH, string XM, string organizeId, string LastModifierCode, DateTime LastModifyTime)
        {
            var GDRQ = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            // Giai doan 7: cau nay cung hardcode [Newtouch_MRMS] (tren staging
            // update nham DB demo, tra 0 du dong EMR da doi) — dung ten cau hinh.
            var sql = "update  " + DbNames.Name("MrmsDb", "Newtouch_MRMS") + ".[dbo].[mr_basy] set bazt='2',GDRQ=null where Id=@dataId and ZYH=@ZYH and XM=@XM";
            
            SugarParameter[] para ={
                new SugarParameter("@dataId",dataId),
                new SugarParameter("@ZYH",ZYH),
                new SugarParameter("@XM",XM)
                };
            var data1= this.ExecuteSqlCommand(sql, para);

            // Giai doan 7: ban cu hardcode [Newtouch_EMR] (ghi sang demo tren
            // staging) VA khong loc zyh (1 lan goi doi TOAN BO dong zt='1').
            // Ban nay dung ten DB cau hinh + chi doi dung dong cua zyh do.
            string sql2 = "update " + DbNames.Name("EmrDb", "Newtouch_EMR") + ".[dbo].[zy_brjbxx] set RecordStu=2,LastModifyTime=@LastModifyTime,LastModifierCode=@LastModifierCode where zt='1' and zyh=@ZYH";
            SugarParameter[] para2 ={
                new SugarParameter("@LastModifyTime",Convert.ToDateTime(LastModifyTime)),
                new SugarParameter("@LastModifierCode",LastModifierCode ?? ""),
                new SugarParameter("@organizeId",organizeId ?? ""),
                new SugarParameter("@ZYH",ZYH)
                };
            var data2 = this.ExecuteSqlCommand(sql2, para2);
            if (data1 != 0 && data2 != 0)
            {
                return 1;
            }
            else {
                return 0;
            }
        }

        public int CXGDZT(string dataId, string ZYH, string XM, string organizeId, string LastModifierCode, DateTime LastModifyTime)
        {
            var GDRQ = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var sql = "update  " + DbNames.Name("MrmsDb", "Newtouch_MRMS") + ".[dbo].[mr_basy] set bazt='2',GDRQ=@GDRQ where Id=@dataId and ZYH=@ZYH and XM=@XM";

            SugarParameter[] para ={
                new SugarParameter("@GDRQ",GDRQ),
                new SugarParameter("@dataId",dataId),
                new SugarParameter("@ZYH",ZYH),
                new SugarParameter("@XM",XM)
                };
            var data1 = this.ExecuteSqlCommand(sql, para);
            if (data1 != 0 )
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}