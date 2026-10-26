using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IRepository;
using Newtouch.Domain.ValueObjects;

namespace Newtouch.Repository
{
    /// <summary>
    /// Tạo bởi: 张冬林
    /// Ngày: 2018-06-28 13:27
    /// Mô tả: Thông tin chẩn đoán bệnh nhân nội trú
    /// </summary>
    public class InpatientPatientDiagnosisRepo : RepositoryBase<InpatientPatientDiagnosisEntity>, IInpatientPatientDiagnosisRepo
    {
        /// <summary>
        /// Hàm khởi tạo mặc định
        /// </summary>
        /// <param name="databaseFactory">Factory ngữ cảnh EF</param>
        public InpatientPatientDiagnosisRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        /// <summary>
        /// Lưu biểu mẫu (thêm mới, sửa đổi)
        /// </summary>
        /// <param name="entity">Đối tượng thực thể</param>
        /// <param name="keyValue">Giá trị khóa chính</param>
        public void SubmitForm(InpatientPatientDiagnosisEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                var dbEntity = this.FindEntity(keyValue);
                //properties

                dbEntity.Modify(keyValue);
                this.Update(dbEntity);
            }
            else
            {
                entity.Create(true);
                this.Insert(entity);
            }
        }

        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="keyValue">Giá trị khóa chính</param>
        public void DeleteForm(string keyValue)
        {
            this.Delete(p => p.Id == keyValue);
        }

        /// <summary>
        /// Lấy thông tin chẩn đoán
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="organizeId"></param>
        /// <param name="zdlb">1 Chẩn đoán nhập viện, 2 Chẩn đoán xuất viện</param>
        /// <param name="zt">1-Có hiệu lực 0-Không hiệu lực</param>
        /// <returns></returns>
        public List<InpatientPatientDiagnosisEntity> SelectData(string zyh, string organizeId, string zdlb, string zt = "1")
        {
            const string sql = @"
SELECT * FROM dbo.zy_PatDxInfo(NOLOCK) 
WHERE zyh=@zyh AND OrganizeId=@OrganizeId AND zt=@zt AND zdlb=@zdlb
";
            var param = new DbParameter[]
            {
                new SqlParameter("@zyh",zyh ),
                new SqlParameter("@OrganizeId",organizeId ),
                new SqlParameter("@zt",zt ),
                new SqlParameter("@zdlb", zdlb)
            };
            return FindList<InpatientPatientDiagnosisEntity>(sql, param);
        }

        public List<WMDiagnosisHtmlVO> SelectDiagData(string zyh, string organizeId, string zdlb, string zt = "1")
        {
            const string sql = @"
            SELECT a.zddm, a.zdmc, b.icd10
            FROM dbo.zy_PatDxInfo a
            left join NewtouchHIS_Base.dbo.xt_zd b
            on a.zddm = b.zdCode and (a.OrganizeId = b.OrganizeId or b.OrganizeId = '*')
            and a.zt = '1' and b.zt = '1' 
            WHERE a.zyh=@zyh AND a.OrganizeId=@OrganizeId AND a.zt=@zt AND a.zdlb=@zdlb and a.zdmc is not null
            ";
            var param = new DbParameter[]
            {
                new SqlParameter("@zyh",zyh ),
                new SqlParameter("@OrganizeId",organizeId ),
                new SqlParameter("@zt",zt ),
                new SqlParameter("@zdlb", zdlb)
            };
            return FindList<WMDiagnosisHtmlVO>(sql, param);
        }
    }
}