using Newtouch.EMR.Domain.Entity;
using Newtouch.EMR.Domain.IRepository;
using Newtouch.EMR.Infrastructure;
using System;
using NewtouchHIS.Lib.Base.Exceptions;
using SqlSugar;

namespace Newtouch.EMR.Repository
{
    /// <summary>
    /// Người tạo：chl
    /// Ngày：2021-03-01 15:24
    /// Mô tả：Ghi chép diễn biến bệnh
    /// </summary>
    public class YBInpCourseDiseaseRepo : RepositoryBase<YBInpCourseDiseaseEntity>, IYBInpCourseDiseaseRepo
    {
        /// <summary>
        /// Hàm khởi tạo mặc định
        /// </summary>
        /// <param name="databaseFactory">EFFactory ngữ cảnh EF</param>


        public void Submit(YBInpCourseDiseaseEntity ety)
        {
            if (ety != null)
            {
                try {
                    ety.BKF737 = ety.BKF737== "00010101000000"? null: Convert.ToDateTime(ety.BKF737).ToString("yyyyMMddHHmmss");
                    var ext = FindEntity(p => p.Id == ety.Id && p.OrganizeId == ety.OrganizeId && p.zt == "1");
                    if (ext != null)
                    {
                        this.Update(ety);
                    }
                    else
                    {
                        this.Insert(ety);
                    }
                }
                catch (Exception ex)
                {
                    throw new FailedException("Lưu thất bại," + ex.Message);
                }
            }
        }

    }
}
