using Newtouch.Core.Common;
using Newtouch.HIS.Domain.Entity.PatientManage;
using Newtouch.HIS.Domain.ReportTemplateVO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.IRepository.PatientManage
{
    public interface IG_yb_daysrg_trt_list_bRepo : IRepositoryBase<G_yb_daysrg_trt_list_bEntity>
    {
        /// <summary>
        /// Bảng danh mục điều trị phẫu thuật trong ngày của BHYT
        /// </summary>
        /// <param name="tbname"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<G_yb_daysrg_trt_list_bVO> G_yb_daysrg_trt_list_b(string tbname);
        /// <summary>
        /// Bảng danh mục chẩn đoán
        /// </summary>
        /// <param name="tbname"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<G_yb_diag_list_bVO> G_yb_diag_list_b(string tbname);
        /// <summary>
        /// Bảng danh mục quyết toán BHYT theo loại bệnh
        /// </summary>
        /// <param name="tbname"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<G_yb_dise_setl_list_bVO> G_yb_dise_setl_list_b(string tbname);
        /// <summary>
        /// Bảng thông tin vật tư tiêu hao
        /// </summary>
        /// <param name="tbname"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<G_yb_mcs_info_bVO> G_yb_mcs_info_b(string tbname);
        /// <summary>
        /// Bảng phân loại và mã phẫu thuật
        /// </summary>
        /// <param name="tbname"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<G_yb_oprn_std_bVO> G_yb_oprn_std_b(string tbname);
        /// <summary>
        /// Bảng danh mục bệnh mãn tính/bệnh đặc biệt ngoại trú của BHYT
        /// </summary>
        /// <param name="tbname"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<G_yb_opsp_dise_list_bVO> G_yb_opsp_dise_list_b(string tbname);
        /// <summary>
        /// Bảng thông tin chế phẩm tự chế
        /// </summary>
        /// <param name="tbname"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<G_yb_selfprep_info_bVO> G_yb_selfprep_info_b(string tbname);
        /// <summary>
        /// Bảng chẩn đoán Trung y
        /// </summary>
        /// <param name="tbname"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<G_yb_tcm_diag_bVO> G_yb_tcm_diag_b(string tbname);
        /// <summary>
        /// Bảng thông tin thảo dược Trung y
        /// </summary>
        /// <param name="tbname"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<G_yb_tcmherb_info_bVO> G_yb_tcmherb_info_b(string tbname);
        /// <summary>
        /// Bảng phân loại chứng hậu Trung y
        /// </summary>
        /// <param name="tbname"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<G_yb_tcmsymp_type_bVO> G_yb_tcmsymp_type_b(string tbname);
        /// <summary>
        /// Bảng hình thái học u bướu
        /// </summary>
        /// <param name="tbname"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<G_yb_tmor_mpy_bVO> G_yb_tmor_mpy_b(string tbname);
        /// <summary>
        /// Bảng hạng mục dịch vụ y tế
        /// </summary>
        /// <param name="tbname"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<G_yb_trt_serv_bVO> G_yb_trt_serv_b(string tbname);
        /// <summary>
        /// Bảng thông tin thuốc Tây y và thuốc thành phẩm Trung y
        /// </summary>
        /// <param name="tbname"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<G_yb_wm_tcmpat_info_bVO> G_yb_wm_tcmpat_info_b(Pagination pagination, string tbname);

        /// <summary>
        /// Phương pháp chung tra cứu danh mục
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="mlbh"></param>
        /// <returns></returns>
        IList<G_yb_mluCommon_Info> Get_G_yb_mluCommon_Info(Pagination pagination, string mlbh,string key);

        string Header(string tbname);

        void DataListSQl(string tbname, string path);
        List<MedinsDaySetlResult> DataListSQl_V2(string tbname, string path);

    }
}
