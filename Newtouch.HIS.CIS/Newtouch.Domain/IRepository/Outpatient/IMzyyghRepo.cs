using Newtouch.Domain.Entity;
using Newtouch.Infrastructure.EF;
using System;

namespace Newtouch.Domain.IRepository
{
    /// <summary>
    /// Đăng ký khám hẹn ngoại trú
    /// </summary>
    public interface IMzyyghRepo : IRepositoryBase<MzyyghEntity>
    {
        /// <summary>
        /// TheoIDXóa đăng ký khám đã hẹn
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int DeleteById(string id);

        /// <summary>
        /// Đến hẹn
        /// </summary>
        /// <param name="id"></param>
        /// <param name="arrivalDate"></param>
        /// <param name="arrivalOpereater"></param>
        /// <returns></returns>
        int KeepAnAppointment(string id, DateTime arrivalDate, string arrivalOpereater);
    }
}
