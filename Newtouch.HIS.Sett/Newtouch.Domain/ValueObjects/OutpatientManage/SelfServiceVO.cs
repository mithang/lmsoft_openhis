using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects.OutpatientManage
{
	public class SelfServiceVO
	{
	}

	public class zzjRegScheduleVO
	{
		/// <summary>
		/// Khoa
		/// </summary>
		public string ks { get; set; }
		/// <summary>
		/// Tên khoa
		/// </summary>
		public string ksmc { get; set; }
		/// <summary>
		/// Lịch trựcid
		/// </summary>
		public string ghpbId { get; set; }
		/// <summary>
		/// Cờ khám ngoại trú/cấp cứu(1Khám ngoại trú 2Cấp cứu 3Chuyên gia)
		/// </summary>
		public string mjzbz { get; set; }
	}

	public class zzjCardInfoVO
	{
		/// <summary>
		/// Số CMND/CCCD
		/// </summary>
		public string ID_NO { get; set; }
		/// <summary>
		/// Số thẻ khám bệnh
		/// </summary>
		public string CARD_NO { get; set; }
		/// <summary>
		/// Bệnh nhânID
		/// </summary>
		public string PATIENT_ID { get; set; }
		/// <summary>
		/// Tên bệnh nhân
		/// </summary>
		public string PATIENT_NAME { get; set; }
		/// <summary>
		/// Loại thẻ khám bệnh
		/// </summary>
		public string CARD_TYPE { get; set; }
		/// <summary>
		/// Tên loại thẻ khám bệnh
		/// </summary>
		public string CARD_TYPENAME { get; set; }
		/// <summary>
		/// Số điện thoại di động
		/// </summary>
		public string PHONE_NUMBER { get; set; }
		/// <summary>
		/// Trạng thái thẻ khám bệnh
		/// </summary>
		public string STATUS { get; set; }
		/// <summary>
		/// Số dư thẻ khám bệnh
		/// </summary>
		public string TOTAL_FEE { get; set; }
		/// <summary>
		/// Thời gian tra cứu
		/// </summary>
		public string QUERY_TIME { get; set; }
	}

	public class OutpfeeMasterInfoVO: OutpfeeMasterPatInfo
	{
		/// <summary>
		/// Danh sách quyết toán
		/// </summary>
		public List<OutpfeeMasterInfoList> ITEMS { get; set; }
		
	}
	public class OutpfeeMasterPatInfo
	{
		/// <summary>
		/// Tên bệnh nhân
		/// </summary>
		public string PATIENT_NAME { get; set; }
		/// <summary>
		/// Mã bệnh nhân
		/// </summary>
		public string PATIENT_ID { get; set; }
		/// <summary>
		/// Số CMND/CCCD
		/// </summary>
		public string ID_NO { get; set; }
		/// <summary>
		/// Số điện thoại
		/// </summary>
		public string PHONE_NUMBER { get; set; }
		/// <summary>
		/// Giới tính
		/// </summary>
		public string GENDER { get; set; }
		/// <summary>
		/// Tuổi
		/// </summary>
		public string AGE { get; set; }
		/// <summary>
		/// Ngày sinh
		/// </summary>
		public string BIRTHDAY { get; set; }

	}
	public class OutpfeeMasterInfoList
	{
		/// <summary>
		/// Ngày khám
		/// </summary>
		public string VISIT_DATE { get; set; }
		/// <summary>
		/// Số thứ tự khám
		/// </summary>
		public string VISIT_NO { get; set; }
		/// <summary>
		/// Khoa khám bệnh
		/// </summary>
		public string DEPT_NAME { get; set; }
		/// <summary>
		/// Số thẻ khám bệnh
		/// </summary>
		public string CARD_NO { get; set; }
		/// <summary>
		/// Số khám ngoại trú
		/// </summary>
		public string OUTPATIENT_NO { get; set; }
		/// <summary>
		/// Số phiếu
		/// </summary>
		public string RECEIPT_NO { get; set; }
		/// <summary>
		/// Số giao dịch nghiệp vụ
		/// </summary>
		public string BUSSINESS_NO { get; set; }
		/// <summary>
		/// Số hóa đơn
		/// </summary>
		public string INVOICE_NO { get; set; }
		/// <summary>
		/// Loại hóa đơn
		/// </summary>
		public string INVOICE_TYPE { get; set; }
		/// <summary>
		/// Loại chi phí
		/// </summary>
		public string INSUR_TYPE { get; set; }
		/// <summary>
		/// Loại thẻ hoàn trả
		/// </summary>
		public string CARD_TYPE { get; set; }
		/// <summary>
		/// Số thẻ hoàn trả
		/// </summary>
		public string INSUR_CARD_NO { get; set; }
		/// <summary>
		/// Số giao dịch thẻ hoàn trả
		/// </summary>
		public string INSUR_RCPT_NO { get; set; }
		/// <summary>
		/// Tổng số tiền
		/// </summary>
		public string ACCOUNT_SUM { get; set; }
		/// <summary>
		/// Số tiền chi trả từ quỹ chung
		/// </summary>
		public string INSUR_PAY { get; set; }
		/// <summary>
		/// Số tiền thanh toán từ tài khoản
		/// </summary>
		public string ACCOUNT_PAY { get; set; }
		/// <summary>
		/// Số tiền mặt cá nhân thanh toán
		/// </summary>
		public string PERSONAL_PAY { get; set; }
		/// <summary>
		/// Quỹ bệnh nặng
		/// </summary>
		public string DISEASE_FUND { get; set; }
		/// <summary>
		/// Trợ cấp công chức
		/// </summary>
		public string INJURY_FUND { get; set; }
		/// <summary>
		/// Quỹ sinh đẻ
		/// </summary>
		public string FERTILITY_FUND { get; set; }
		/// <summary>
		/// Số tiền tự chi trả
		/// </summary>
		public string MYSELF_PAY { get; set; }
		/// <summary>
		/// Số tiền tự chi trả theo phân loại
		/// </summary>
		public string CLASS_SELF_PAY { get; set; }
		/// <summary>
		/// Số dư tài khoản
		/// </summary>
		public string INSUR_ACCOUNT { get; set; }
		/// <summary>
		/// Ngày quyết toán
		/// </summary>
		public string SETTLEMENT_DATE { get; set; }
		/// <summary>
		/// Số đơn hàng
		/// </summary>
		public string ORDER_NO { get; set; }
		/// <summary>
		/// Kênh thanh toán
		/// </summary>
		public string PAY_CHANNEL { get; set; }
	}

	public class OutpfeeDetailVO: OutpfeeDetailPatInfo
	{
		public IList<OutpfeeDetailList> ITEMS { get; set; }
    }

	public class OutpfeeDetailPatInfo
	{
		/// <summary>
		/// Tên bệnh nhân
		/// </summary>
		public string PATIENT_NAME { get; set; }
		/// <summary>
		/// Mã bệnh nhân
		/// </summary>
		public string PATIENT_ID { get; set; }
		/// <summary>
		/// Số CMND/CCCD
		/// </summary>
		public string ID_NO { get; set; }
		/// <summary>
		/// Số điện thoại
		/// </summary>
		public string PHONE_NUMBER { get; set; }
		/// <summary>
		/// Ngày khám
		/// </summary>
		public string VISIT_DATE { get; set; }
		/// <summary>
		/// Số thứ tự khám
		/// </summary>
		public string VISIT_NO { get; set; }
		/// <summary>
		/// Thời gian thanh toán
		/// </summary>
		public string PAY_DATE { get; set; }
	}

	public class OutpfeeDetailList
	{
		/// <summary>
		/// Định danh duy nhất của chi phí này
		/// </summary>
		public string M_FEIYONGID { get; set; }
		/// <summary>
		/// Số thứ tự hạng mục
		/// </summary>
		public string ITEM_NO { get; set; }
		/// <summary>
		/// Mã hạng mục
		/// </summary>
		public string ITEM_CODE { get; set; }
		/// <summary>
		/// Tên hạng mục
		/// </summary>
		public string ITEM_NAME { get; set; }
		/// <summary>
		/// Quy cách hạng mục
		/// </summary>
		public string ITEM_SPEC { get; set; }
		/// <summary>
		/// Đơn vị hạng mục
		/// </summary>
		public string ITEM_UNITS { get; set; }
		/// <summary>
		/// Dạng bào chế hạng mục
		/// </summary>
		public string ITEM_FORM { get; set; }
		/// <summary>
		/// Số lượng hạng mục
		/// </summary>
		public string ITEM_AMOUNT { get; set; }
		/// <summary>
		/// Đơn giá hạng mục
		/// </summary>
		public string ITEM_PRICE { get; set; }
		/// <summary>
		/// Tổng số tiền hạng mục
		/// </summary>
		public string ITEM_COSTS { get; set; }
		/// <summary>
		/// Loại hạng mục
		/// </summary>
		public string ITEM_CLASS { get; set; }
		/// <summary>
		/// Loại BHYT
		/// </summary>
		public string INSUR_CLASS { get; set; }
		/// <summary>
		/// Tỷ lệ tự chi trả
		/// </summary>
		public string MYSELF_SCALE { get; set; }
		/// <summary>
		/// Số tiền tự chi trả
		/// </summary>
		public string SELF_PAY { get; set; }
		/// <summary>
		/// Số tiền tự chi trả theo phân loại
		/// </summary>
		public string CLASS_SELF_PAY { get; set; }
		/// <summary>
		/// Mã hạng mục BHYT
		/// </summary>
		public string MEDICAL_INSURANCE_ITEM_CODE { get; set; }
		/// <summary>
		/// Tên hạng mục BHYT
		/// </summary>
		public string MEDICAL_INSURANCE_ITEM_NAME { get; set; }
		/// <summary>
		/// Bác sĩ kê đơn
		/// </summary>
		public string DOCTOR_NAME { get; set; }
		/// <summary>
		/// Khoa thực hiện
		/// </summary>
		public string PERFORM_DEPT { get; set; }
	}

	public class InPatientInfoVO : InPatientInfoPatInfo
	{
		public IList<InPatientInfoList> ITEMS { get; set; }
	}

	public class InPatientInfoPatInfo
	{
		/// <summary>
		/// Mã bệnh nhân
		/// </summary>
		public string PATIENT_ID { get; set; }
		/// <summary>
		/// Số CMND/CCCD
		/// </summary>
		public string ID_NO { get; set; }
		/// <summary>
		/// Khoa đang nằm viện
		/// </summary>
		public string DeptName { get; set; }
		/// <summary>
		/// Khu bệnh đang nằm viện
		/// </summary>
		public string WardName { get; set; }
		/// <summary>
		/// Số giường bệnh
		/// </summary>
		public string BedNo { get; set; }
		/// <summary>
		/// Trạng thái đang nằm viện
		/// </summary>
		public string PatStatus { get; set; }
		/// <summary>
		/// Thời gian nhập viện
		/// </summary>
		public string InHospitalDate { get; set; }
		/// <summary>
		/// Thời gian vào khu
		/// </summary>
		public string InDate { get; set; }
		/// <summary>
		/// Thời gian ra khỏi khu
		/// </summary>
		public string OutDate { get; set; }
		/// <summary>
		/// Giới tính
		/// </summary>
		public string SexName { get; set; }
		/// <summary>
		/// Tổng số tiền tạm ứng
		/// </summary>
		public string AccBalance { get; set; }
		/// <summary>
		/// Tổng chi phí
		/// </summary>
		public string TotalMoney { get; set; }
		/// <summary>
		/// Số nhập viện
		/// </summary>
		public string zyh { get; set; }
	}

	public class InPatientInfoList
	{
		/// <summary>
		/// Tên hạng mục
		/// </summary>
		public string ITEM_NAME { get; set; }
		/// <summary>
		/// Dạng bào chế hạng mục
		/// </summary>
		public string ITEM_FORM { get; set; }
		/// <summary>
		/// Số lượng hạng mục
		/// </summary>
		public string ITEM_AMOUNT { get; set; }
		/// <summary>
		/// Đơn giá hạng mục
		/// </summary>
		public string ITEM_PRICE { get; set; }
		/// <summary>
		/// Tổng số tiền hạng mục
		/// </summary>
		public string ITEM_COSTS { get; set; }
		/// <summary>
		/// Loại hạng mục
		/// </summary>
		public string ITEM_CLASS { get; set; }
		/// <summary>
		/// Đơn vị hạng mục
		/// </summary>
		public string ITEM_UNITS { get; set; }
		/// <summary>
		/// Ngày phát sinh
		/// </summary>
		public string ITEM_DATE { get; set; }
	}
}
