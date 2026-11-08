using Newtouch.Domain.Entity;
using Newtouch.Domain.ValueObjects;
using Newtouch.CIS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.IRepository
{
    public interface IComDiagnosisRepo : IRepositoryBase<ComDiagnosisEntity>
    {
        /// <summary>
        /// Lấy thường dùng Chẩn đoán thông tin
        /// </summary>
        /// <param name="ys"></param>
        /// <param name="orgId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        IList<ComDiagnosisVO> GetComDiagnosis(string ys,string ks,string orgId,string type,string keyword,string lx);
        /// <summary>
        /// Lưu thường dùng Chẩn đoán
        /// </summary>
        /// <param name="entity"></param>
        void SubmitDiag(ComDiagnosisEntity entity);
        /// <summary>
        /// Lấy Chẩn đoán Pinyin
        /// </summary>
        /// <param name="zdmc"></param>
        /// <param name="zdbm"></param>
        /// <returns></returns>
        List<ComDiagnosisVO> Getzdpy(string zdmc, string zdbm);
        /// <summary>
        /// Lấy Hồ sơ chẩn đoán thông tin
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="type"></param>
        /// <param name="mzh"></param>
        /// <returns></returns>
        List<ComDiagnosisVO> GetDiagsLists(string orgId,string type, string mzh);
        void DelDiagnosticTemplate(string Id);
        /// <summary>
        /// Lấy thường dùng Chẩn đoán thông tin-Quản lý giới mặt
        /// </summary>
        /// <param name="ys"></param>
        /// <param name="orgId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        IList<ComDiagnosisVO> GetComDiagnosisAdmin(string ys, string ks, string orgId, string type, string keyword, string lx);
    }
}
