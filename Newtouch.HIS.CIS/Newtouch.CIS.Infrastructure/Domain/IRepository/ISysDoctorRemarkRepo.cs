using Newtouch.Domain.Entity;
using Newtouch.Domain.ValueObjects;
using Newtouch.CIS.Repository;
using System.Collections.Generic;

namespace Newtouch.Domain.IRepository
{
    public interface ISysDoctorRemarkRepo : IRepositoryBase<SysDoctorRemarkEntity>
    {
        /// <summary>
        /// Lấydanh sách
        /// </summary>
        /// <returns></returns>
        List<SysDoctorRemarkVO> GetListByOrg(string orgId);

        /// <summary>
        /// Lưu
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        void SubmitForm(SysDoctorRemarkEntity entity, string keyValue);

        /// <summary>
        /// Xóa
        /// </summary>
        /// <param name="keyValue"></param>
        void DeleteForm(string keyValue);



        #region Bác sĩtrạm--》văn bảnnhập--》chỉhiển thịvăn bảnkhungNút

        //List<SyssurgeryTextVO> surgery_record(string OrganizeId);

        #endregion




    }
}
