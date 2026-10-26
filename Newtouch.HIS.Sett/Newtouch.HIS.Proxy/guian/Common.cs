using System;
using Newtouch.HIS.Proxy.guian.DTO.S25;

namespace Newtouch.HIS.Proxy.guian
{
    /// <summary>
    /// Quý An BHYT nông thôn mới côngtổng cộng Phư ơng thức
    /// </summary>
    public static class Common
    {

        /// <summary>
        /// Tính BHYT nông thôn mới BHYTchi phí tự trả
        /// </summary>
        /// <param name="s25ResponseDto"></param>
        /// <returns></returns>
        public static decimal CalculationXnhZffy(S25ResponseDTO s25ResponseDto)
        {
            decimal res = 0;
            if (s25ResponseDto == null) return res;
            var qtfy = CalculationXnhQtbcfy(s25ResponseDto);
            return s25ResponseDto.totalCost - Convert.ToDecimal(s25ResponseDto.compensateCost) - qtfy;
        }

        /// <summary>
        /// Tính BHYT nông thôn mới BHYTtài khoản thanh toán số tiền
        /// </summary>
        /// <param name="s25ResponseDto"></param>
        /// <returns></returns>
        public static decimal CalculationXnhZhzf(S25ResponseDTO s25ResponseDto)
        {
            decimal res = 0;
            if (s25ResponseDto == null) return res;
            var qtfy = CalculationXnhQtbcfy(s25ResponseDto);
            return Convert.ToDecimal(s25ResponseDto.compensateCost) + qtfy;
        }

        /// <summary>
        /// Tính BHYT nông thôn mới khácchi phí bồi thư ờng
        /// </summary>
        /// <param name="s25ResponseDto"></param>
        /// <returns></returns>
        public static decimal CalculationXnhQtbcfy(S25ResponseDTO s25ResponseDto)
        {
            decimal res = 0;
            if (s25ResponseDto == null) return res;
            return Convert.ToDecimal(s25ResponseDto.civilCost) +
            Convert.ToDecimal(s25ResponseDto.insureCost) +
            Convert.ToDecimal(s25ResponseDto.salvaJSCost) +
            Convert.ToDecimal(s25ResponseDto.bottomRedeem) +
            Convert.ToDecimal(s25ResponseDto.bottomSecondRedeem) +
            Convert.ToDecimal(s25ResponseDto.medicineCost) +
            Convert.ToDecimal(s25ResponseDto.salvaYFCost) +
            Convert.ToDecimal(s25ResponseDto.salvaCLCost) +
            Convert.ToDecimal(s25ResponseDto.salvaFPCost) +
            Convert.ToDecimal(s25ResponseDto.salvaJKCost) +
            Convert.ToDecimal(s25ResponseDto.continentInsuranceCost) +
            Convert.ToDecimal(s25ResponseDto.nurseCost);
        }
    }
}
