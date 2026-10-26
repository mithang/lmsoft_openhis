using Newtouch.HIS.Domain.DTO;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects;
using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Newtouch.HIS.Application.Interface
{
    /// <summary>
    /// 
    /// </summary>
    public interface IOutpatientRegApp
    {
        /// <summary>
        /// Đ ăng ký khám lịch trực List
        /// </summary>
        /// <param name="keyword">Khoa/Bác sĩ</param>
        /// <param name="mzlx">Loại ngoại trú phòng khám thông thư ờng/cấp cứu/Phòng khám chuyên gia</param>
        /// <returns></returns>
        List<RegScheduleVO> GetRegScheduleList(string keyword, string mzlx);

        string GetInvoice();

        void CheckInvoice(string inputFph);

        /// <summary>
        /// Đ ăng ký khám ngoại trú lưu
        /// </summary>
        /// <param name="patid"></param>
        /// <param name="kh"></param>
        /// <param name="ghly"></param>
        /// <param name="mjzbz"></param>
        /// <param name="ks"></param>
        /// <param name="ys"></param>
        /// <param name="ksmc"></param>
        /// <param name="ysmc"></param>
        /// <param name="ghxm"></param>
        /// <param name="zlxm"></param>
        /// <param name="fph"></param>
        /// <param name="sfrq"></param>
        /// <param name="isCkf"></param>
        /// <param name="isGbf"></param>
        /// <param name="ghpbId"></param>
        /// <param name="feeRelated"></param>
        /// <param name="brxz">Tính chất bệnh nhândotrang truyền qua đến,vìlà BHYTbệnh nhân có thểđăng kýtự trả</param>
        /// <param name="notSett">true tạm thời không quyết toán(ngoại trú thu phí giờ cùng nhau quyết toán),nếu khôngquyết toán</param>
        /// <param name="ybjsh">BHYTphíasố quyết toán</param>
        /// <param name="qzjzxh"> trư ớc đặtsố thứ tự khám bệnh</param>
        /// <param name="qzmzh"> trư ớc đặtsố khám ngoại trú</param>
        /// <param name="mzyyghid">Đ ặt lịch đ ăng ký khám ngoại trú ID Tư ơng ứngNewtouch_CIS.dbo.mz_yygh.Id</param>
        /// <returns></returns>
        void Save(int patid, string kh, string ghly, string mjzbz,
            string ks, string ys, string ksmc, string ysmc, string ghxm, string zlxm
            , string fph, DateTime? sfrq, bool isCkf, bool isGbf, int ghpbId
            , OutpatientSettFeeRelatedDTO feeRelated, string brxz
            , string ybjsh, string mzyyghId, ref short? qzjzxh, ref string qzmzh
            , string jzyy,string jzid, string jzlx, string bzbm, string bzmc,string isjm, out object newJszbInfo);

        long Book(OutpatientRegistEntity gh, decimal ssk, string fph, bool isCkf, bool isGbf, string updateBrxz);

        void CheckBrxz(string kh, string updateBrxz);

        /// <summary>
        /// Đ ặt lịch đ ăng ký khám
        /// </summary>
        /// <param name="zjlx"></param>
        /// <param name="zjh"></param>
        /// <param name="blh"></param>
        /// <param name="ksCode"></param>
        /// <param name="mzlx"></param>
        /// <param name="ysgh"></param>
        /// <returns></returns>
        string BespeakRegister(int? zjlx, string zjh, string blh, string ksCode, int mzlx, string ysgh);
    }
}
