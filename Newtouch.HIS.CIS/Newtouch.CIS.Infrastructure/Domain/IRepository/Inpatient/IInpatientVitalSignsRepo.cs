using Newtouch.Domain.Entity;
using System.Collections.Generic;
using NewtouchHIS.Lib.Base.Model;
using Newtouch.CIS.Repository;
using System;
using Newtouch.Domain.DTO.InputDto.Inpatient;
using Newtouch.Domain.ValueObjects.Inpatient;

namespace Newtouch.Domain.IRepository
{
    /// <summary>
    /// Tác giả: 冬林
    /// Ngày: 2018-06-28 13:27
    /// Mô tả: Nhập viện bệnh nhân dấu hiệu sinh tồn
    /// </summary>
    public interface IInpatientVitalSignsRepo : IRepositoryBase<InpatientVitalSignsEntity>
    {
        /// <summary>
        /// điều dưỡng phút trang Tra cứu
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="orgId"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="zyh"></param>
        /// <returns></returns>
        IList<InpatientVitalSignsDto> GetPaginationList(Pagination pagination, string orgId, DateTime? kssj, DateTime? jssj, string zyh,string wardCode, string isShowDelete);

        /// <summary>
        /// điều dưỡng Tra cứu
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="zyh"></param>
        /// <returns></returns>
        IList<InpatientVitalSignsEntity> GetValidList(string orgId, DateTime? kssj, DateTime? jssj, string zyh);


        /// <summary>
        /// điều dưỡng Tra cứu(không phút trang)
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="orgId"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="zyh"></param>
        /// <returns></returns>
        IList<InpatientVitalSignsDto> GetList(string orgId, DateTime? kssj, DateTime? jssj, string zyh);
        /// <summary>
        /// Lưu bảng phiếu (Thêm mới, Sửa)
        /// </summary>
        /// <param name="entity">thực thể đối tượng</param>
        /// <param name="keyValue">Khóa chính Giá trị</param>
        void SubmitForm(InpatientVitalSignsEntity entity, string keyValue);

        /// <summary>
        /// Xóa Dữ liệu
        /// </summary>
        /// <param name="keyValue">Khóa chính Giá trị</param>
        void DeleteForm(string keyValue);

        IList<TemperatureGraphData> GetTempratureData(string orgId, DateTime? kssj, DateTime? jssj, string zyh,int pagesize =6);

    }
}
