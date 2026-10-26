using Newtouch.Common.Operator;
using Newtouch.Core.Common;
using Newtouch.Domain.ValueObjects;
using Newtouch.EMR.Domain.BusinessObjects;
using Newtouch.EMR.Domain.Entity;
using Newtouch.EMR.Domain.ValueObjects.MedicalRecord;
using System.Collections.Generic;

namespace Newtouch.EMR.Domain.IDomainServices
{
    public interface IMedicalRecordDmnService
    {
        medicalRecordVO GetMedicalRecord(string blid, string bllx);

        void MedicalRecordSave(medicalRecordVO medicalRecord, ZymeddocsrelationEntity Entity);
        void LockRecord(string blid, string bllx, string OrganizeId, string UserCode, int isLock);
        /// <summary>
        /// Lưu trữ thống nhất bệnh án
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="bllx"></param>
        /// <param name="mbbh"></param>
        /// <param name="path"></param>
        /// <param name="BLMC"></param>
        /// <param name="user"></param>
        /// <param name="zyh"></param>
        /// <param name="mzh"></param>
        /// <returns></returns>
        string BL_Save(string orgId, string bllx, string mbbh, string path, string BLMC, OperatorModel user, string zyh, string mzh = null, string doctype = null);
        /// <summary>
        /// Lấy bệnh án lưu trữ thống nhất
        /// </summary>
        /// <param name="blid"></param>
        /// <param name="bllx"></param>
        /// <returns></returns>
        medicalRecordVO GetMedicalRecordbyId(string blid, string bllx);
        /// <summary>
        /// Lưu bệnh án ngoại trú
        /// </summary>
        /// <param name="medicalRecord"></param>
        /// <param name="Entity"></param>
        void MedicalRecordSaveMz(medicalRecordVO medicalRecord, MzmeddocsrelationEntity Entity);

        void TbDataSavebyWriter(IList<BlHljlData> data, string ksrq, string jsrq);

        List<LisReportSqdhValueVo> GetLisSqdhData(string orgId, string zymzh, string type, string ztmc, string kssj, string jssj);//Hạng mục xét nghiệm
        List<PacsReportSqdhValueVo> GetPacsSqdhData(string orgId, string zymzh, string type, string ztmc, string kssj, string jssj);//Hạng mục kiểm tra
        List<PacsReportSqdhValueMxVo> GetPacsSqdhMxData(string sqdh, string OrganizeId);//Chi tiết hạng mục kiểm tra
        List<LisReportSqdhValueMxVo> GetLisSqdhMxData(string zyh, string lissqdh, string organizeId);//Chi tiết hạng mục xét nghiệm

        List<AdviceListGridVO> AdviceGridView(Pagination pagination, AdviceListRequestVO req);//Tra cứu y lệnh
        blzybrjbxxVO GetBlZybrjbxx(string OrgId, string zyh,string user,bool newbz);//Lấy giá trị phần tử bệnh án
        int updateLock(string OrgId, string blid,string user);//Sửa trạng thái khóa
        void BLJG_Save(List<bl_ysjgnrEntity> bljgnr);
        string BLJG_Delete(string blid, string orgId);
    }
}