using Newtouch.Domain.Entity;
using System.Collections.Generic;
using Newtouch.Core.Common;
using Newtouch.Infrastructure.EF;
using System;
using Newtouch.Domain.DTO.InputDto.Inpatient;
using Newtouch.Domain.ValueObjects.Inpatient;

namespace Newtouch.Domain.IRepository
{
    /// <summary>
    /// Tạo bởi:张冬林
    /// Ngày:2018-06-28 13:27
    /// Mô tả:Dấu hiệu sinh tồn bệnh nhân nội trú
    /// </summary>
    public interface IInpatientVitalSignsRepo : IRepositoryBase<InpatientVitalSignsEntity>
    {
        /// <summary>
        /// Tra cứu phân trang điều dưỡng
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="orgId"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="zyh"></param>
        /// <returns></returns>
        IList<InpatientVitalSignsDto> GetPaginationList(Pagination pagination, string orgId, DateTime? kssj, DateTime? jssj, string zyh,string wardCode, string isShowDelete);

        /// <summary>
        /// Tra cứu điều dưỡng
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="zyh"></param>
        /// <returns></returns>
        IList<InpatientVitalSignsEntity> GetValidList(string orgId, DateTime? kssj, DateTime? jssj, string zyh);


        /// <summary>
        /// Tra cứu điều dưỡng(Không phân trang)
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="orgId"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="zyh"></param>
        /// <returns></returns>
        IList<InpatientVitalSignsDto> GetList(string orgId, DateTime? kssj, DateTime? jssj, string zyh);
        /// <summary>
        /// Lưu biểu mẫu(Thêm mới,Sửa đổi)
        /// </summary>
        /// <param name="entity">Đối tượng thực thể</param>
        /// <param name="keyValue">Giá trị khóa chính</param>
        void SubmitForm(InpatientVitalSignsEntity entity, string keyValue);

        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="keyValue">Giá trị khóa chính</param>
        void DeleteForm(string keyValue);

        IList<TemperatureGraphData> GetTempratureData(string orgId, DateTime? kssj, DateTime? jssj, string zyh,int pagesize =6);

    }
}