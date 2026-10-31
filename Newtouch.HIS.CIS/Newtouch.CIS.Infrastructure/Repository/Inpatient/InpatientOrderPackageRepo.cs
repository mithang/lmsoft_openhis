using Newtouch.Domain.Entity;
using Newtouch.Domain.IRepository;

using System.Collections.Generic;
using Newtouch.CIS.Compat;
using NewtouchHIS.Lib.Base.Model;
using System.Data.SqlClient;
using System.Text;
using Newtouch.CIS.Repository;
using System.Linq;

namespace Newtouch.Repository
{
    /// <summary>
    /// Tác giả: 冬林
    /// Ngày:2018-06-28 13:27
    /// Mô tả:Nhập việnY lệnhgói
    /// </summary>
    public class InpatientOrderPackageRepo : RepositoryBase<InpatientOrderPackageEntity>, IInpatientOrderPackageRepo
    {
        /// <summary>
        /// Mặc địnhHàm khởi tạosố
        /// </summary>
        /// <param name="databaseFactory">EFtrêndướivănNhà máy</param>
        public InpatientOrderPackageRepo()
            : base()
        {
            
        }

        /// <summary>
        /// Lưubảngphiếu(Thêm mới,Sửa)
        /// </summary>
        /// <param name="entity">thựcthểđối tượng</param>
        /// <param name="keyValue">Khóa chínhGiá trị</param>
        public void SubmitForm(InpatientOrderPackageEntity entity, string keyValue)
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
        /// XóaDữ liệu
        /// </summary>
        /// <param name="keyValue">Khóa chínhGiá trị</param>
        public void DeleteForm(string keyValue)
        {
            this.Delete(p => p.Id == keyValue);
        }

        public List<InpatientOrderPackageEntity> GettcTree(int tcfw,string yzlx,string orgId)
        {
            var alldata = this.IQueryable().Where(p => p.tcfw == tcfw && p.tclx == int.Parse(yzlx) && p.OrganizeId == orgId && p.zt == "1").ToList();//_inspectionTemplateDmnService.GetTemplateListByType(this.OrganizeId, jyjcmbLx);
            return alldata;
        }

    }
}