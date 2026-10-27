using Newtouch.Domain.Entity;
using System.Collections.Generic;
using NewtouchHIS.Lib.Base.Model;
using Newtouch.CIS.Repository;
using Newtouch.Domain.ValueObjects.Outpatient;

namespace Newtouch.Domain.IRepository
{
    /// <summary>
    /// Tác giả: 冬林
    /// Ngày：2018-06-28 13:27
    /// Mô tả：Nhập việnThông tin bệnh nhân
    /// </summary>
    public interface IInpatientPatientInfoRepo : IRepositoryBase<InpatientPatientInfoEntity>
    {
        /// <summary>
        /// Lưubảngphiếu（Thêm mới、Sửa）
        /// </summary>
        /// <param name="entity">thựcthểđối tượng</param>
        /// <param name="keyValue">Khóa chínhGiá trị</param>
        void SubmitForm(InpatientPatientInfoEntity entity, string keyValue);

        /// <summary>
        /// XóaDữ liệu
        /// </summary>
        /// <param name="keyValue">Khóa chínhGiá trị</param>
        void DeleteForm(string keyValue);
        /// <summary>
        /// Cập nhậtNhập việnbệnh nhânởviệnCờ
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="zyh"></param>
        /// <param name="zybz"></param>
        void UpdateInpatientStatus(string orgId, string zyh, string zybz, string ryzd, string ryzdmc, string brxz, string brxzmc, string lxr, string lxrgx, string lxrdh);

        /// <summary>
        /// thông quaSố nhập việnLấyNhập việnThông tin bệnh nhân
        /// </summary>
        /// <param name="OrganizeId"></param>
        /// <param name="zyh"></param>
        /// <returns></returns>
        InpatientPatientInfoEntity GetByZyh(string OrganizeId, string zyh);

        /// <summary>
        /// Lấybệnh nhânThông tin cơ bản
        /// </summary>
        /// <param name="zyh">Số nhập viện</param>
        /// <param name="xm">Họ tên</param>
        /// <param name="organizeId">Tổ chứcID</param>
        /// <returns></returns>
        InpatientPatientInfoEntity SelectData(string zyh, string xm, string organizeId);


        List<InpatientAreaVO> TreeViewdata(string orgId);


    }
}
