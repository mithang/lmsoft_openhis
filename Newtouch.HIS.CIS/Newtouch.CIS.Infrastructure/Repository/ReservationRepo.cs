
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
    public class ReservationRepo : RepositoryBase<ReservationEntity>, IReservationRepo
    {
        /// <summary>
        /// Mặc địnhHàm khởi tạosố
        /// </summary>
        /// <param name="databaseFactory">EFtrêndướivănNhà máy</param>
        public ReservationRepo()
            : base()
        {

        }
        /// <summary>
        /// Lưu(Thêm mới,Sửa)
        /// </summary>
        /// <param name="entity">thựcthểđối tượng</param>
        /// <param name="keyValue">Khóa chínhGiá trị</param>
        public void SubmitForm(ReservationEntity entity)
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
