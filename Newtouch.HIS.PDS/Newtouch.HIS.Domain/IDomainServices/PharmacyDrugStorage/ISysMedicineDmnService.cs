using Newtouch.Core.Common;
using Newtouch.HIS.Domain.ValueObjects.PharmacyDrugStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.IDomainServices.PharmacyDrugStorage
{
    public interface ISysMedicineDmnService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="medicineEntity"></param>
        /// <param name="medicinePropEntity"></param>
        /// <param name="ypId"></param>
        void SubmitMedicine(SysMedicineVO model, int? ypId);

        /// <summary>
        /// sửa BHYT đồng bộ thời gian
        /// </summary>
        /// <param name="ypId"></param>
        /// <returns></returns>
        //bool YibaoUpload(int ypId, out string error);

        /// <summary>
        /// gắn thuốc list tập hợp
        /// </summary>
        /// <param name="organizeId"></param>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<SysMedicineVO> GetPaginationList(string organizeId, Pagination pagination, string zt, string ypflCode, string keyword = null);



        /// <summary>
        /// sửa thông tin giờ,gắn sửa giá trị
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        SysMedicineVO GetFormJson(int keyValue);

        /// <summary>
        /// lấy hệ thống nhân viên thông tin
        /// </summary>
        /// <param name="ypCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        SysMedicineVO SelectMedicineInfo(string ypCode, string organizeId);

        /// <summary>
        /// lấy thuốc hệ số
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        string GetTradePricePlus(string name);

        /// <summary>
        /// lấy hiện tại tổ chức dưới của hệ thống thông tin thuốc
        /// </summary>
        /// <param name="organizeId"></param>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<SysMedicineVO> GetPaginationListdm(string organizeId, Pagination pagination, string type, string keyword = null);
        IList<SysMedicineMldzxxVO> GetPaginationListMldzxx(string organizeId, Pagination pagination, string type, string keyword = null);
        /// <summary>
        /// tra cứu BHYT thông tin thuốc
        /// </summary>
        /// <param name="organizeId"></param>
        /// <param name="ypmc"></param>
        /// <param name="py"></param>
        /// <param name="gjybdm"></param>
        /// <param name="pzwh"></param>
        /// <returns></returns>
        IList<G_yb_ypxxVO> GetYpypxxlist(string organizeId, string ypmc, string py, string gjybdm, string pzwh);

        /// <summary>
        /// lưu BHYT đối với mã thông tin
        /// </summary>
        /// <param name="ybxx"></param>
        /// <param name="ypid"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        //int SaveYpYb(G_yb_ypxxVO ybxx, int? ypid, string organizeId);


        /// <summary>
        /// Thông tin vật tư hệ thống
        /// </summary>
        /// <param name="organizeId"></param>
        /// <param name="pagination"></param>
        /// <param name="type"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<SysChargeItemVO> GetclxxList(string organizeId, Pagination pagination, string type, string keyword = null);

        /// <summary>
        /// Truy vấn thông tin vật tư BHYT theo thông tin vật tư hệ thống
        /// </summary>
        /// <param name="organizeId"></param>
        /// <param name="ypmc"></param>
        /// <param name="py"></param>
        /// <param name="gjybdm"></param>
        /// <param name="pzwh"></param>
        /// <returns></returns>
        IList<G_yb_clxxVO> GetYbclxxlist(string organizeId, string ypmc, string py, string gjybdm, string pzwh);

        /// <summary>
        /// Lưu mã đối chiếu vật tư
        /// </summary>
        /// <param name="ybxx"></param>
        /// <param name="ypid"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        //int SaveYpcl(G_yb_clxxVO ybxx, int? ypid, string organizeId);



        /// <summary>
        /// hệ thống hạng mục thông tin
        /// </summary>
        /// <param name="organizeId"></param>
        /// <param name="pagination"></param>
        /// <param name="type"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<SysChargeItemVO> GetxmxxList(string organizeId, Pagination pagination, string type, string keyword = null);


        /// <summary>
        /// tra cứu BHYT hạng mục thông tin theo hệ thống hạng mục thông tin
        /// </summary>
        /// <param name="organizeId"></param>
        /// <param name="ypmc"></param>
        /// <param name="py"></param>
        /// <param name="gjybdm"></param>
        /// <param name="pzwh"></param>
        /// <returns></returns>
        IList<G_yb_xmxxVO> GetYbxmxxlist(string organizeId, string ypmc, string py, string gjybdm, string pzwh);

        //int SaveYpxm(G_yb_xmxxVO ybxx, int? ypid, string organizeId);

        /// <summary>
        /// cách dùng thuốc
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<SysMedicineUsageVO> GetUsageFloat(string keyword);
        IList<GzybybItemCodeVO> GetYbMedicineList(string OrganizeId, string keyword = null);
        IList<GzybNameCodeVO> GetYbName(string OrganizeId, string lx, string keyword = null);
        IList<GzxnhybItemCodeVO> GetYbXNHMedicineList(string keyword = null);
    }
}
