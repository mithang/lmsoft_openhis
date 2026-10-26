using System.Collections.Generic;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects.PatientManage;
using System;
using Newtouch.HIS.Domain.ValueObjects.HospitalizationManage;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.BusinessObjects;
using Newtouch.HIS.Domain.DTO.InputDto.OutpatientAccounting;
using Newtouch.HIS.Domain.DTO.OutputDto;
using Newtouch.HIS.Domain.ValueObjects.DaySettleManage;

namespace Newtouch.HIS.Domain.IDomainServices
{
	/// <summary>
	/// 
	/// </summary>
	public interface IDaySettleDmnService
	{
		/// <summary>
		/// Tra cứu quyết toán ngày ngoại trú
		/// </summary>
		/// <param name="pagination"></param>
		/// <param name="keyword"></param>
		/// <param name="kssj"></param>
		/// <param name="jssj"></param>
		/// <param name="orgId"></param>
		/// <returns></returns>
		IList<DaySettleDetailVO> GetOutPatientSettListJson(Pagination pagination, DateTime? kssj, DateTime? jssj, string orgId, string mzzybz);

		/// <summary>
		/// Lưu quyết toán ngày ngoại trú
		/// </summary>
		/// <param name="kssj">Thời gian bắt đầu</param>
		void SaveOutpatientDaySettleInfo(DateTime? kssj, DateTime jssj, string CreatorCode, string orgId);

		/// <summary>
		/// Lưu quyết toán ngày nội trú
		/// </summary>
		/// <param name="kssj">Thời gian bắt đầu</param>
		void SaveInpatientDaySettleInfo(DateTime? kssj, DateTime jssj, string CreatorCode, string orgId);

		/// <summary>
		/// Lấy thời gian quyết toán ngày lần trước
		/// </summary>
		/// <returns></returns>
		string getLastSettDate(string mzzybz);

		/// <summary>
		/// Hủy quyết toán ngày ngoại trú
		/// </summary>
		/// <param name="Id"></param>
		void CancleDaySettleInfo(string Id, string UserCode);


		IList<ybzdinfo> Getclr_type();
		IList<ybzdinfo> Getinsutype();

		/// <summary>
		/// Lấy thanh toán đối chiếu sổ tổng
		/// </summary>
		/// <param name="organizeId">Mã cơ sở</param>
		/// <param name="ksrq">Ngày bắt đầu</param>
		/// <param name="jsrq">Ngày kết thúc</param>
		/// <returns></returns>
		IList<Qsdzz> GetQsdzzs(string organizeId, string ksrq, string jsrq, string qslx, string xz);

		int inserqssq(System.Xml.XmlDocument ybqssq, string orgid);

		/// <summary>
		/// Lấy chi tiết thanh toán(Nửa trên)
		/// </summary>
		/// <param name="organizeId">Mã cơ sở</param>
		/// <param name="ksrq">Ngày bắt đầu</param>
		/// <param name="jsrq">Ngày kết thúc</param>
		/// <param name="jslx">Loại quyết toán</param>
		/// <returns></returns>
		IList<Qsmx> GetQsmx(string organizeId, string ksrq, string jsrq, string jslx, string xzlx);

		/// <summary>
		/// Lấy chi tiết thanh toán(Nửa dưới)
		/// </summary>
		/// <param name="organizeId">Mã cơ sở</param>
		/// <param name="ksrq">Ngày bắt đầu</param>
		/// <param name="jsrq">Ngày kết thúc</param>
		/// <param name="jslx">Loại quyết toán</param>
		/// <returns></returns>
		IList<Qsmx_1> GetQsmx_1(string organizeId, string ksrq, string jsrq, string jslx, string xzlx);


		/// <summary>
		/// Lấy đơn thanh toán
		/// </summary>
		/// <param name="organizeId">Mã cơ sở</param>
		/// <param name="ksrq">Ngày bắt đầu</param>
		/// <param name="jsrq">Ngày kết thúc</param>
		/// <param name="sfyd">Có phải khác địa phương hay không</param>
		/// <returns></returns>
		IList<Qssq> GetQssq(string organizeId, string ksrq, string jsrq, string sfyd);

		///// <summary>
		///// Quay lại thanh toán
		///// </summary>
		///// <param name="organizeId">Mã cơ sở</param>
		///// <param name="ksrq">Ngày bắt đầu</param>
		///// <param name="jsrq">Ngày kết thúc</param>
		///// <returns></returns>
		IList<Qsht> GetQsht(string organizeId, string ksrq, string jsrq);


		/// <summary>
		/// Dữ liệu lịch sử đối soát ngày
		/// </summary>
		/// <param name="orgid"></param>
		/// <param name="ksrq"></param>
		/// <param name="jsrq"></param>
		/// <returns></returns>
		IList<RdrlsList> lsdzList(string orgid, string ksrq, string jsrq);

		IList<RdrNewList> Newdzfysj(string orgid, string rq);
        /// <summary>
        /// Lấy dữ liệu chi tiết đối soát cục bộ
        /// </summary>
        /// <param name="orgid"></param>
        /// <param name="ksrq"></param>
        /// <param name="jsrq"></param>
        /// <returns></returns>
        IList<HistoryCheckVO> GetHistoryCheckList(string orgid, string ksrq, string jsrq);
        
    }
}
