using System.Collections.Generic;

namespace Newtouch.Infrastructure.Model
{
    /// <summary>
    /// đ ăng nhập ngư ời dùng,khi trư ớc nhà thuốc bộ phận thông tin
    /// </summary>
    public class LoginUserCurrentYfbmModel
    {
        /// <summary>
        /// nhà thuốc bộ phậnCode
        /// </summary>
        public string yfbmCode { get; set; }

        /// <summary>
        /// nhà thuốc bộ phận cấp bậc (1 cấp kho thuốc,2 cấp nhà thuốc)
        /// </summary>
        public string yfbmjb { get; set; }

        /// <summary>
        /// Khi trư ớc đ ăng nhập thân phận của nhà thuốc bộ phận:0 kho thuốc;1 nhà thuốc ngoại trú;2 nhập viện nhà thuốc kho thuốc trực tiếp thông qua yjbmjb phán đoán thì có thể 
        /// </summary>
        public string mzzybz { get; set; }

        /// <summary>
        /// trên cấp kho thuốc Code List(nhà thuốc có thể tới 目标 kho thuốc xin 领)
        /// </summary>
        public IList<string> TheUpperYfbmCodeList { get; set; }

        /// <summary>
        /// dư ới cấp nhà thuốc Code List(kho thuốc có thể tới 目标 nhà thuốc phát thuốc)
        /// </summary>
        public IList<string> TheLowerYfbmCodeList { get; set; }


    }
}
