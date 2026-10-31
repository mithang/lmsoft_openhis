using Newtouch.EMR.Domain.Entity;
using Newtouch.EMR.Domain.IRepository;
using System;
using NewtouchHIS.Lib.Base.Exceptions;
using SqlSugar;

namespace Newtouch.EMR.Repository
{
    /// <summary>
    /// Người tạo:chl
    /// Ngày:2021-03-01 15:24
    /// Mô tả:Tóm tắt ra viện
    /// </summary>
    public class YBInpOutHosSummariesRepo : RepositoryBase<YBInpOutHosSummariesEntity>, IYBInpOutHosSummariesRepo
    {
        /// <summary>
        /// Hàm khởi tạo mặc định
        /// </summary>
        /// <param name="databaseFactory">EFFactory ngữ cảnh EF</param>

        public void Submit(YBInpOutHosSummariesEntity ety)
        {
            if (ety != null)
            {
                try
                {
                    if (!string.IsNullOrWhiteSpace(ety.BKC192))
                    {
                        ety.BKC192 = ety.BKC192 == "00010101000000" ? null : Convert.ToDateTime(ety.BKC192).ToString("yyyyMMddHHmmss");
                    }
                    if (!string.IsNullOrWhiteSpace(ety.AKC194))
                    {
                        ety.AKC194 = ety.AKC194 == "00010101000000" ? null : Convert.ToDateTime(ety.AKC194).ToString("yyyyMMddHHmmss");
                    }
                    var ext = this.FindEntity(p => p.Id == ety.Id && p.OrganizeId == ety.OrganizeId && p.zt == "1");
                    if (ext != null && !string.IsNullOrWhiteSpace(ety.Id))
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
