using Newtouch.EMR.Domain.Entity;
using System.Collections.Generic;
using Newtouch.Core.Common;
using Newtouch.Infrastructure.EF;
using Newtouch.Common.Operator;
using Newtouch.EMR.Domain.ValueObjects.MedicalRecord;

namespace Newtouch.EMR.Domain.IRepository
{
    /// <summary>
    /// Người tạo: chl
    /// Ngày: 2018-09-05 11:34
    /// Mô tả: Danh sách mẫu bệnh án
    /// </summary>
    public interface IBlmblbRepo : IRepositoryBase<BlmblbEntity>
    {
        /// <summary>
        /// Lưu biểu mẫu (thêm mới, sửa đổi)
        /// </summary>
        /// <param name="entity">Đối tượng thực thể</param>
        /// <param name="keyValue">Giá trị khóa chính</param>
        void SubmitForm(BlmblbEntity entity, string keyValue);

        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="keyValue">Giá trị khóa chính</param>
        void DeleteForm(string keyValue);
        BlmblbEntity GetTemplateById(string ID);
        BlmblbEntity GetTemplateByBllxId(string ID, string OrganizeId);
        
        List<BlmblbEntity> GetParentTemplate(string OrganizeId);
        List<BlmblbEntity> GetPagintionList(Pagination pagination, string OrganizeId, string keyword, OperatorModel user);


        #region 护理记录
        void BtnSubmit(OperatorModel user, IList<HljldataVO> data);
        IList<HljldataVO> HljlLoadData(Pagination pagination, string blId,string zyh, string orgId);
        #endregion
        



    }


}