
using Newtouch.CIS.Repository;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IRepository;
using System;
using Newtouch.CIS.Compat;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Repository
{
    public class AdmissionNoticeRepo : RepositoryBase<AdmissionNoticeEntity>, IAdmissionNoticeRepo
    {
        /// <summary>
        /// Mặc địnhHàm khởi tạosố
        /// </summary>
        /// <param name="databaseFactory">EFtrêndướivănNhà máy</param>
        public AdmissionNoticeRepo()
            : base()
        {

        }
        /// <summary>
        /// Lưu(Thêm mới,Sửa)
        /// </summary>
        /// <param name="entity">thựcthểđối tượng</param>
        /// <param name="keyValue">Khóa chínhGiá trị</param>
        public void SubmitForm(AdmissionNoticeEntity entity)
        {
            try
            {
                entity.Create(true);
                this.Insert(entity);
            }
            catch (Exception er) { }
        }
    }
}
