using System;
using System.Threading;
using Newtouch.HIS.Domain.DTO.OutputDto;
using Newtouch.HIS.Domain.ValueObjects.OutpatientManage;
using Newtouch.HIS.Repository;

namespace Newtouch.HIS.Application.Strategy.MzBespeakRegister
{
    /// <summary>
    /// Phòng khám thông thư ờng
    /// </summary>
    public class GeneralBespeakStrategy : ICommitStrategy
    {
        #region 单例
        private static GeneralBespeakStrategy _instance;
        private GeneralBespeakStrategy()
        {
        }
        public static GeneralBespeakStrategy Instance()
        {
            return _instance ?? (_instance = new GeneralBespeakStrategy());
        }

        #endregion

        private string mzyyghId;
        private string userCode;

        /// <summary>
        /// thực hiện 
        /// </summary>
        /// <param name="cts"></param>
        /// <param name="request"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public bool Process(CancellationTokenSource cts, dynamic request, ActResult result)
        {
            try
            {
                mzyyghId = "";
                userCode = "";
                var mzBespeakRegister = request as MzBespeakRegisterVO;
                //Đ ặt lịch đ ăng ký khám có hay không lịch trực chưa lịch trực củađặt qua 
                if (mzBespeakRegister.bespeakRegSchedulingDetail == null || mzBespeakRegister.bespeakRegSchedulingDetail.Count <= 0) return true; // này khoa không có lịch trực,có thể trực ti ếpĐ ăng ký khám
                //Có hay không đ ặt lịch có đ ặt lịch củađặt qua 
                var todayDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                if (mzBespeakRegister.bespeakRegDetail != null && mzBespeakRegister.bespeakRegDetail.Count > 0) //Có đ ặt lịch 
                {
                    var data = mzBespeakRegister.bespeakRegDetail.FindAll(p => p.regDate == todayDate && p.ksCode == mzBespeakRegister.registerInfo.ksCode && p.ysgh == "" && (p.arrivalDate == null || p.arrivalDate <= Convert.ToDateTime("1971-01-01")));
                    if (data != null && data.Count > 0)
                    {
                        mzyyghId = data[0].mzyyghId;
                        userCode = mzBespeakRegister.userCode;
                        return true; // đ ặt lịch qua 
                    }
                }

                // không có đ ặt lịch 
                if (mzBespeakRegister.alreadyBespeakRegisterCount >= mzBespeakRegister.allBespeakMaxCount)
                {
                    //Có lịch trực đ ặt lịch,khi trư ớc bệnh nhân chưa đ ặt lịch khi trư ớc khoa, và đ ặt lịch Đ ãđầy
                    result.IsSucceed = false;
                    result.ResultMsg = "该科室预约已满，确定继续挂号吗？";
                    return false;
                }
                if ((mzBespeakRegister.alreadyBespeakRegisterCount + mzBespeakRegister.alreadyRegisterCount) >= mzBespeakRegister.allBespeakMaxCount)
                {
                    //Có lịch trực đ ặt lịch,khi trư ớc bệnh nhân chưa đ ặt lịch khi trư ớc khoa, đ ặt lịch chưađầy,Nh ưngĐ ãĐ ăng ký kh ámĐ ăng ký khámsố+Đ ã đ ặt lịch số>= có thể đ ặt lịch tổngsố
                    result.IsSucceed = false;
                    result.ResultMsg = "该科室挂号已满，确定继续挂号吗？";
                    return false;
                }
                return true; // đ ặt lịch số ngư ời + thực tế Đ ãĐ ăng ký khám số ngư ời < hỗ trợ đ ặt lịch lớn nhất số ngư ời,có thể tiếp tục Đ ăng ký khám

            }
            catch (Exception ex)
            {
                cts.Cancel();
                result.IsSucceed = false;
                result.ResultMsg = ex.ToString();
                return false;
            }
        }

        /// <summary>
        /// Hậu xử lý
        /// </summary>
        /// <param name="cts"></param>
        /// <param name="request"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public bool After(CancellationTokenSource cts, dynamic request, ActResult result)
        {
            if (!string.IsNullOrWhiteSpace(mzyyghId))
            {
                result.Data = mzyyghId;
            }
            return true;
        }
    }
}
