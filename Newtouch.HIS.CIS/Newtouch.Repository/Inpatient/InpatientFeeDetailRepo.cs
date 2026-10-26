using Newtouch.Domain.Entity;
using Newtouch.Domain.IRepository;
using FrameworkBase.MultiOrg.Infrastructure;
using System.Collections.Generic;
using Newtouch.Core.Common;
using System.Data.SqlClient;
using System.Text;
using FrameworkBase.MultiOrg.Repository;

namespace Newtouch.Repository
{
    /// <summary>
    /// Tạo bởi: 张冬林
    /// Ngày: 2018-06-28 13:27
    /// Mô tả: Kho chi tiết phí nội trú
    /// </summary>
    public class InpatientFeeDetailRepo : RepositoryBase<InpatientFeeDetailEntity>, IInpatientFeeDetailRepo
    {
        /// <summary>
        /// Hàm khởi tạo mặc định
        /// </summary>
        /// <param name="databaseFactory">Factory ngữ cảnh EF</param>
        public InpatientFeeDetailRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        /// <summary>
        /// Lưu biểu mẫu (thêm mới, sửa đổi)
        /// </summary>
        /// <param name="entity">Đối tượng thực thể</param>
        /// <param name="keyValue">Giá trị khóa chính</param>
        public void SubmitForm(InpatientFeeDetailEntity entity, string keyValue)
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
        /// Lấy thông tin chi phí theo số nhập viện, tính chất y lệnh
        /// </summary>
        /// <param name="OrganizeId"></param>
        /// <param name="zyh"></param>
        /// <param name="yzxz"></param>
        /// <returns></returns>
        public List<InpatientFeeDetailEntity> GetListByZyhYzxz(string OrganizeId, string zyh, string yzxz)
        {
            List<SqlParameter> pars = new List<SqlParameter>();
            string lsSQL = @" SELECT * FROM zy_fymxk WITH(NOLOCK) WHERE zyh=@zyh AND OrganizeId = @orgId AND zt ='1' ";
            pars.Add(new SqlParameter("@zyh", zyh));
            pars.Add(new SqlParameter("@orgId", OrganizeId));
            if (!string.IsNullOrWhiteSpace(yzxz))
            {
                lsSQL += " AND yzxz = @yzxz";
                pars.Add(new SqlParameter("@yzxz", yzxz));
            }
            return this.FindList<InpatientFeeDetailEntity>(lsSQL, pars.ToArray());
        }

    }
}