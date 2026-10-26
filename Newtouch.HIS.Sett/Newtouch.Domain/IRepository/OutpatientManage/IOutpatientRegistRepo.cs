
using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// Đăng ký khám ngoại trú
    /// </summary>
    public interface IOutpatientRegistRepo : IRepositoryBase<OutpatientRegistEntity>
    {
        OutpatientRegistEntity SelectOutPatientReg(int ghnm, string orgId);
        OutpatientRegistEntity SelectOutPatientReg(string mzh, string orgId);

        /// <summary>
        /// TheomzhLấy thực thể đăng ký khám
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        OutpatientRegistEntity SelectData(string mzh, string orgId);

        List<OutpatientRegistEntity> SelectOutPatientRegList(List<int> ghnmList, string orgId);

        /// <summary>
        /// Lấy thông tin đăng ký khám theo số thẻ và tổ chức
        /// </summary>
        /// <param name="kh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        OutpatientRegistEntity SelectCFZ(string kh, string orgId);

        /// <summary>
        /// Lấy thông tin đăng ký khám trong ngày của số thẻ hiện tại
        /// add HLF
        /// </summary>
        /// <param name="kh">Số thẻ</param>
        /// <returns></returns>
        List<OutpatientRegistEntity> GetOutPatRegEntityList(string kh, string orgId);

        #region GRS门诊挂号
        /// <summary>
        /// Lưu
        /// </summary>
        /// <param name="hospItemFeeEntity"></param>
        /// <param name="keyValue"></param>
        string SubmitForm(OutpatientRegistEntity OutpatientRegistEntity, int? keyValue, string pzzy, string orgId, string curUserCode, out string mzh);

        /// <summary>
        /// Hôm nay bệnh nhân đã đăng ký khám hay chưa
        /// </summary>
        /// <param name="patid"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        bool AllowRegh(string blh, string orgId);
        int UpdatePatPhone(string patid, string phone, string userCode, string organizeId);
        /// <summary>
        /// Hủy đăng ký khám
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="ghnm"></param>
        void SaveCancelRegister(string orgId, int ghnm);

        #endregion

        /// <summary>
        /// Cập nhật đăng ký khám ngoại trú Trạng thái khám bệnh,Bác sĩ khám bệnh
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="mzh"></param>
        void UpdateConsultationStatus(string orgId, string mzh, string jzbz, string jzys);

        /// <summary>
        /// Cập nhật đăng ký khám ngoại trú Thông tin chẩn đoán
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="mzh"></param>
        /// <param name="zdicd10"></param>
        /// <param name="zdmc"></param>
        void UpdateDiagnosis(string orgId, string mzh, string zdicd10, string zdmc);
        /// <summary>
        /// Cập nhật tính chất bệnh nhân
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="mzh"></param>
        /// <param name="brxz"></param>
        void updatePatBrxzInfo(string orgId, string mzh, string brxz);
        /// <summary>
        /// Đăng ký khám ngoại trú không đăng ký BHYT Khi thu phí thì đăng ký cập nhật thẻ tự chi trả thành thẻ BHYT
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="mzh"></param>
        /// <param name="kh"></param>
        /// <param name="cardtype"></param>
        /// <param name="brxz"></param>
        void UpdateChargeMzghInfo(string orgId, string mzh, string kh,string cardtype, string brxz,string zjh,string status);
        /// <summary>
        /// Cập nhật khám BHYT đăng ký khámidSố giao dịch
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="mzh"></param>
        /// <param name="jzId"></param>
        /// <param name="jzpzlx"></param>
        void updateYbghjzId(string orgId, string mzh, string jzId, string jzpzlx);

        /// <summary>
        /// Ghi số thứ tự bồi thường
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="outpId">Số thứ tự bồi thường</param>
        /// <param name="userCode">Người sửa đổi</param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        int RecordOutpId(string mzh, string outpId, string userCode, string organizeId);
    }
}
