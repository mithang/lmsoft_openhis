using Newtouch.Domain.Entity;
using Newtouch.CIS.Repository;
using System;

namespace Newtouch.Domain.IRepository
{
    /// <summary>
    /// Ngoại trúđặt hẹnđăng ký khám
    /// </summary>
    public interface IMzyyghRepo : IRepositoryBase<MzyyghEntity>
    {
        /// <summary>
        /// TheoIDXóađãđặt hẹnđăng ký khám
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
