using Newtouch.HIS.Domain.Entity.DeanInquiryManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.IDomainServices.DeanInquiryManage
{
    public interface IDeanInquiryDmnService
    {
        /// <summary>
        /// Tin tức trong ngày_Tổng quan thu nhập toàn viện 
        /// </summary>
        List<DailyUpdatesEntiy> DailyUpdates_GetQysr();
        /// <summary>
        /// Tra cứu của viện trưởng-Tin tức hôm nay-banner 
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        JRDT_Banner GetJRDTBanner(string orgId);
        /// <summary>
        /// Tin tức trong ngày_Tin tức ngoại trú hôm nay
        /// </summary>
        List<DailyUpdates_GetJrdtmz> DailyUpdates_GetJrdt(string orgid);
        /// <summary>
        /// Tin tức trong ngày_Đơn thuốc ngoại trú
        /// </summary>
        List<DailyUpdates_GetMzcf> DailyUpdates_GetMzcf(string orgId);
        /// <summary>
        /// Tin tức trong ngày_Chi phí ngoại trú
        /// </summary>
        List<DailyUpdates_GetMzfy> DailyUpdates_GetMzfy(string orgId);
        /// <summary>
        /// Tin tức trong ngày_Tỷ lệ chiếm giường nội trú
        /// </summary>
        List<DailyUpdates_GetZyzcl> DailyUpdates_GetZyzcl(string orgId);
        /// <summary>
        /// Tin tức trong ngày_Thống kê đăng ký khám ngoại trú
        /// </summary>
        List<DailyUpdates_GetMzghtj> DailyUpdates_GetMzghtj(string orgId);
        /// <summary>
        /// Tin tức trong ngày_Thống kê bệnh nhân nội trú
        /// </summary>
        List<DailyUpdates_GetZyhztj> DailyUpdates_GetZyhztj(string orgId);
        /// <summary>
        /// Xếp hạng nghiệp vụ_Khối lượng công việc bác sĩ ngoại trú
        /// </summary>
        List<BusinessRankingEntiy> BusinessRankingEntiy_mzysgzl(string kssj, string jssj,string OrganizeId);
        /// <summary>
        /// Xếp hạng nghiệp vụ_Xếp hạng thu nhập khoa ngoại trú
        /// </summary>
        List<BusinessRankingEntiy_mzkssr> BusinessRankingEntiy_mzkssr(string kssj, string jssj, string OrganizeId);
        /// <summary>
        /// Xếp hạng nghiệp vụ_Xếp hạng khoa nghiệp vụ nội trú
        /// </summary>
        List<BusinessRankingEntiy_zyywks>  BusinessRankingEntiy_zyywks(string kssj, string jssj, string orgId);
        /// <summary>
        /// Xếp hạng nghiệp vụ_Xếp hạng nghiệp vụ bác sĩ
        /// </summary>
        List<BusinessRankingEntiy_ysyw> BusinessRankingEntiy_ysyw(string kssj, string jssj, string orgId, string bqcode);
        /// <summary>
        /// Phân tích tổng hợp ngoại trú_Dữ liệu tiêu đề
        /// </summary>
        OutpatientComprehensiveAnalysisEntiy OutpatientComprehensiveAnalysisEntiy_TitleData(string orgId, string ksrq, string jsrq, string rtype);
        /// <summary>
        /// Phân tích tổng hợp ngoại trú_Phân tích bệnh nhân ngoại trú
        /// </summary>
        OutpatientComprehensiveAnalysisEntiy_Mzhzfx OutpatientComprehensiveAnalysisEntiy_Mzhzfx(string orgId, string ksrq, string jsrq);
        /// <summary>
        /// Khối lượng công việc ngoại trú_Khối lượng công việc khoa ngoại trú
        /// </summary>
        List<OutpatientWorkloadEntiy> OutpatientWorkloadEntiy_Mzksgzl(string ksrq, string jsrq, string OrganizeId);
        /// <summary>
        /// Khối lượng công việc ngoại trú_Khối lượng công việc khoa ngoại trú
        /// </summary>
        List<OutpatientWorkloadEntiy_ysgzl> OutpatientWorkloadEntiy_Ysgzl(string ksrq, string jsrq, string OrganizeId, string ks);
        /// <summary>
        /// Phân tích chi phí ngoại trú_Phân tích phân loại chi phí ngoại trú
        /// </summary>
        List<OutpatientCostEntiy> OutpatientCostEntiy_Mzfyflfx(string orgId, string ksrq, string jsrq);
        /// <summary>
        /// Phân tích phân loại chi phí ngoại trú_Phân tích phân loại chi phí ngoại trú_Biểu đồ(Biểu đồ phân tích chi phí bình quân đầu người của bệnh nhân cũng dùng trường này, đều giống nhau)
        /// </summary>
        List<OutpatientCostEntiy_Mzfyflfx_tb> OutpatientCostEntiy_Mzfyflfx_tb(string orgId, string ksrq, string jsrq, string rtype);
        /// <summary>
        /// Phân tích chi phí ngoại trú_Phân tích chi phí bình quân đầu người của bệnh nhân
        /// </summary>
        List<OutpatientCostEntiy_Hzrjfyfx> OutpatientCostEntiy_Hzrjfyfx(string orgId, string ksrq, string jsrq);
        /// <summary>
        /// Phân tích chi phí ngoại trú_Phân tích chi phí bình quân đầu người của bệnh nhân_Biểu tượng
        /// </summary>
        List<OutpatientCostEntiy_Mzfyflfx_tb> OutpatientCostEntiy_Hzrjfyfx_tb(string orgId, string ksrq, string jsrq, string rtype);
        /// <summary>
        /// Hiệu quả ngoại trú_Chi tiết hiệu quả ngoại trú
        /// </summary>
        List<OutpatientBenefitsEntiy> OutpatientCostEntiy_Mzxymx(string orgId, string ksrq, string jsrq);
        /// <summary>
        /// Hiệu quả ngoại trú_Hiệu quả bác sĩ
        /// </summary>
        List<OutpatientBenefitsEntiy_Ysxy> OutpatientCostEntiy_Ysxy(string orgId, string ksrq, string jsrq, string code);
        /// <summary>
        /// 
        /// </summary>
        List<KSXYPMBySJSRDTO> OutpatientCostEntiy_Lsqs(string orgId, string ksrq, string jsrq,string rType,string type);
        List<KSXYPMDTO> GetKSXYPM(string orgId, string ksrq, string jsrq);
        List<KSXYPMBySJSRDTO> GetKSXYPMBySJSY(string orgId, string ksrq, string jsrq);
        /// <summary>
        /// Thuốctop 50 Xếp hạng
        /// </summary>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="OrganizeId"></param>
        /// <param name="yptype"></param>
        /// <returns></returns>
        List<DrugTrackingEntity> DrugTrackingEntity_Lsqs(string kssj, string jssj, string OrganizeId, string yptype);

        /// <summary>
        /// Xếp hạng lãi lỗ
        /// </summary>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="OrganizeId"></param>
        /// <returns></returns>
        List<Profitandlossranking> Profitandlossranking_Lsqs(string kssj, string jssj, string OrganizeId);

        /// <summary>
        /// Xếp hạng kê đơn của bác sĩ
        /// </summary>
        /// <param name="ypcode"></param>
        /// <param name="OrganizeId"></param>
        /// <returns></returns>
        List<DoctorBillingRanking> DoctorBillingRanking_Lsqs(string kssj, string jssj, string ypcode, string OrganizeId);

        /// <summary>
        /// Xếp hạng doanh số vật tư tiêu haoTOP50
        /// </summary>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="OrganizeId"></param>
        /// <returns></returns>
        List<ConsumablestatisticsEntity> Consumablestatistics_Lsqs(string kssj, string jssj, string OrganizeId);

        /// <summary>
        /// Xếp hạng bác sĩ vật tư tiêu hao
        /// </summary>
        /// <param name="sfxmdm"></param>
        /// <param name="OrganizeId"></param>
        /// <returns></returns>
        List<DoctorBillingRanking> DoctorBillingRankingHC_Lsqs(string kssj, string jssj, string sfxmdm, string OrganizeId);

        /// <summary>
        /// Thống kê vật tư tiêu hao
        /// </summary>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="OrganizeId"></param>
        /// <returns></returns>
        List<Profitandlossranking> ProfitandlossrankingHC_Lsqs(string kssj, string jssj, string OrganizeId);

        List<NumberTrendEntity> ryqs_Lsqs(string time, string OrganizeId);
        List<NumberTrendEntity> cyqs_Lsqs(string time, string OrganizeId);
        List<NumberTrendEntity> zzqs_Lsqs(string time, string OrganizeId);

        List<NumberPersonEntity> Rstj_Lsqs(string kssj,string jssj, string OrganizeId);

        List<DeparNumberEntity> Ksgzl_Lsqs(string kssj, string jssj, string OrganizeId);

        List<StaffNumberEntity> YSgzl_Lsqs(string kssj, string jssj, string bq, string OrganizeId);
        #region 住院费用分析
        /// <summary>
        /// Phân tích chi phí nội trú_Dữ liệu tiêu đề
        /// </summary>
        ZYFYFXDTO ZYFYFX_TitleData(string orgId, string ksrq, string jsrq, string rtype);
        List<ZYFYFX_KSFYFXDTO> ZYFYFX_KSFYFXDTO(string orgId, string ksrq, string jsrq, string rtype);
        List<ZYFYFX_CYHZFYFXDTO> GetCYHZFYFData(string orgId, string ksrq, string jsrq, string rtype);
        List<CYHZFYFTJTDTO> GetCYHZFYFTJData(string orgId, string ksrq, string jsrq, string rtype);
        #endregion
    }
}
