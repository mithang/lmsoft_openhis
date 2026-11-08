using Newtouch.Domain.Entity;
using Newtouch.Domain.IRepository;

using System.Collections.Generic;
using Newtouch.CIS.Compat;
using NewtouchHIS.Lib.Base.Model;
using System.Data.SqlClient;
using System.Text;
using Newtouch.CIS.Repository;

namespace Newtouch.Repository
{
    /// <summary>
    /// Tác giả: 冬林
    /// Ngày:2018-06-28 13:27
    /// Mô tả: Nhập viện chi phí chi tiết kho
    /// </summary>
    public class InpatientFeeDetailRepo : RepositoryBase<InpatientFeeDetailEntity>, IInpatientFeeDetailRepo
    {
        /// <summary>
        /// Hàm khởi tạo mặc định
        /// </summary>
        /// <param name="databaseFactory">Nhà máy ngữ cảnh EF</param>
        public InpatientFeeDetailRepo()
            : base()
        {

        }

        /// <summary>
        /// Lưu bảng phiếu (Thêm mới, Sửa)
        /// </summary>
        /// <param name="entity">thực thể đối tượng</param>
        /// <param name="keyValue">Khóa chính giá trị</param>
        public void SubmitForm(InpatientFeeDetailEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                var dbEntity = this.FindEntity(keyValue);
                var ignoreProps = new List<string>() { "Id", "OrganizeId", "CreateTime", "CreatorCode", "LastModifyTime", "LastModifierCode" };
                entity.MapperTo(dbEntity, ignoreProps);
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
        /// <param name="keyValue">Khóa chính giá trị</param>
        public void DeleteForm(string keyValue)
        {
            this.Delete(p => p.Id == keyValue);
        }

        /// <summary>
        /// thông qua Số nhập viện, Y lệnh Tính chất Lấy Chi phí thông tin
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