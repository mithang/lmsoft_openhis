using Newtouch.HIS.Domain.Entity;
using Newtouch.Infrastructure.EF;
using System;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// Tạo:chl
    /// Ngày tháng:2019-12-16 17:20
    /// Mô tả:Hẹn khám
    /// </summary>
    public interface IMzghbookRepo : IRepositoryBase<MzghbookEntity>
    {
        /// <summary>
        /// Lưu biểu mẫu(Thêm mới,Sửa đổi)
        /// </summary>
        /// <param name="entity">Đối tượng thực thể</param>
        /// <param name="keyValue">Giá trị khóa chính</param>
        int SubmitForm(MzghbookEntity entity, decimal? keyValue);

        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="keyValue">Giá trị khóa chính</param>
        int DeleteForm(decimal keyValue);

        /// <summary>
        /// Sửa trạng thái đăng ký khám hẹn trước ngoại trú
        /// </summary>
        /// <param name="jsnm"></param>
        /// <param name="mzh"></param>
        /// <param name="patid"></param>
        /// <param name="ks"></param>
        /// <param name="zfje"></param>
        /// <param name="Appid"></param>
        void UpdateMzGhAppointment(string jsnm,string mzh,int patid,string ks,string zfje,string Appid,string orgId,string outdate,string mzlx,string ghf);

        void SigninAppointment(string mzh,string orgId);

		int FZsjTB(string ghrq, string rygh, string orgid);

	}
}