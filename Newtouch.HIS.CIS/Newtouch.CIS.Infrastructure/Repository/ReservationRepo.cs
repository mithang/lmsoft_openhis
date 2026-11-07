
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
        /// <param name="databaseFactory">EF trên dưới văn Nhà máy</param>
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
            // C130: bo catch (Exception) { } — loi Insert truoc day bi nuot, caller tra
            // Content("") gia trong khi khong ghi duoc gi; de exception toi global handler.
            entity.Create(true);
            this.Insert(entity);
        }
    }
}
