using Newtouch.Common.Operator;
using Newtouch.Core.Redis;
using Newtouch.Infrastructure.Model;
using System;

namespace Newtouch.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    public class Constants
    {
        /// <summary>
        /// ID tổ chức(cao nhất cấp,cấu hình hệ thống)
        /// </summary>
        public static string TopOrganizeId
        {
            get
            {
                return FrameworkBase.MultiOrg.Infrastructure.ConstantsBase.TopOrganizeId;
            }
        }

        /// <summary>
        /// ID ứng dụng
        /// </summary>
        public static string AppId
        {
            get
            {
                return FrameworkBase.MultiOrg.Infrastructure.ConstantsBase.AppId;
            }
        }

        #region 登录用户当前的药房部门信息 永不过期 不同系统之间会共享

        /// <summary>
        /// Lấykhi trư ớc củadượcbộ phận(nhà thuốc hoặc kho thuốc)
        /// </summary>
        public static LoginUserCurrentYfbmModel CurrentYfbm
        {
            get
            {
                return GetCurrentYfbm(OperatorProvider.GetCurrent().UserId);
            }
        }

        /// <summary>
        /// Lấykhi trư ớc củadượcbộ phận(nhà thuốc hoặc kho thuốc)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static LoginUserCurrentYfbmModel GetCurrentYfbm(string userId)
        {
            return RedisHelper.Get<LoginUserCurrentYfbmModel>(string.Format(CacheKey.CurrentYfbmInfoEntityKey, userId))
                ?? new LoginUserCurrentYfbmModel();
        }

        /// <summary>
        /// Đ ặtkhi trư ớc củadượcbộ phận(nhà thuốc hoặc kho thuốc)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SetCurrentYfbm(string userId, LoginUserCurrentYfbmModel value)
        {
            if (value == null)
            {
                RedisHelper.Remove(string.Format(CacheKey.CurrentYfbmInfoEntityKey, userId));
            }
            else
            {
                RedisHelper.Set(string.Format(CacheKey.CurrentYfbmInfoEntityKey, userId), value);
            }
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        public struct xtzypz
        {
            /// <summary>
            /// Nhập viện không tải lênBHYT loại lớn 
            /// </summary>
            public const string DL_BSCYB = "zy.DL_BSCYB";
            /// <summary>
            /// một lần vật tư tiêu hao loại lớn 
            /// </summary>
            public const string YCXCLDM = "zy.YCXCLDM";
            /// <summary>
            /// dự kiến tính thiếu phí
            /// </summary>
            public const string ZY_BRXZ_FYBL = "ZY_BRXZ_FYBL";
            /// <summary>
            /// 
            /// </summary>
            public const string ZYJFKSDM = "ZYJFKSDM";
            /// <summary>
            /// Xác minh mã BHYTcủa tính chất bệnh nhân;nhập viện hạng mục thu phí trong phảichứacó mã BHYTmới cho phépthu phí của tính chất bệnh nhân
            /// </summary>
            public const string ZYYBSFBRXZ = "ZYYBSFBRXZ";
            /// <summary>
            /// giảm gánh nặng bệnh nặng
            /// </summary>
            public const string ZYYZJFXM = "ZYYZJFXM";
            /// <summary>
            /// Nhập viện kết hợp tính chất trong tham giavà BHYT giao dịch củatính chất
            /// </summary>
            public const string ZYZHXZYBJYXZ = "ZYZHXZYBJYXZ";

            /// <summary>
            /// theo tính chất bệnh nhân và chẩn đoán phán đoán, có thể bảo hiểmchi phí của giới hạn trên,như quỹ thiếu nhi,thứ một chẩn đoán làcấp tínhxxruột thừa, thanh toán BHYT giới hạn trên là1100
            /// </summary>
            public const string ZY_BRXZ_ZD_BXSX = "zy.ZY_BRXZ_ZD_BXSX";

            ///// <summary>
            ///// Cán bộ nghỉ hưu tính chất bệnh nhân nhập viện giư ờng bệnh phí cố định thanh toán BHYT số tiền
            ///// </summary>
            //public const string ZYCWFTSXZBX = "zy.ZYCWFTSXZBX";


        }

        // gì phạm vi trong củachi phítham giavà tỷ lệ tự trả của tính
        //0 có thể ghi sổ một phầntham giavà tính(tạm thời không có)
        //1 có thể ghi sổ＋tự trả theo phân loại(nghỉ hưu, từ thiệnV.v.đa số……)
        //2 có thể ghi sổ＋tự trả theo phân loại＋tự chi trả－tuyệt đối đ ối với tự chi trả(nghỉ hưu phí đ ăng ký khám0.1 giờ,tuyệt đối đ ối với tự chi trả xử lý nghỉ hưu đ ặc biệt Đ ăng ký khámV.v. không có thể thanh toán một phần－xemhệ thống hạng mục thu phí.Tính chất tự trả)
        //"cùng loại lớn "Của" tất cả các cấp bậc "Của có thể thanh toán phạm vi phải nhất quán 

        /// <summary>
        /// Hệ thống bệnh nhânthu phí thuật toán của chi phí phạm vi
        /// gì phạm vi trong củachi phítham giavà tỷ lệ tự trả của tính
        /// </summary>
        public struct xtbrsfsffyfw
        {
            /// <summary>
            /// 
            /// </summary>
            public const string fyfw0 = "0";
            /// <summary>
            /// 
            /// </summary>
            public const string fyfw1 = "1";
            /// <summary>
            /// 
            /// </summary>
            public const string fyfw2 = "2";
            /// <summary>
            /// 
            /// </summary>
            public const string fyfw3 = "3";
        }

        public static string SiteBaseUrl = AppDomain.CurrentDomain.BaseDirectory;

        public static string ReportTemplateDirUrl = SiteBaseUrl + @"\ReportTemplate\";



        /// <summary>
        /// Hệ thống phư ơng thức thanh toán 
        /// </summary>
        public struct xtzffs
        {
            /// <summary>
            /// Thanh toán tiền mặt
            /// </summary>
            public const string XJZF = "0";
            /// <summary>
            /// Nhập việntạm ứngtài khoản thanh toán (2018 tài khoảntạm ứng thanh toán tiếp tục dùng dùng 3)
            /// </summary>
            public const string ZYYJZHZF = "3";
            /// <summary>
            /// POS máy thanh toán 
            /// </summary>
            public const string POS = "1";
            /// <summary>
            /// ngân hàng chuyển khoản thanh toán 
            /// </summary>
            public const string YHZZ = "2";


        }

        //Cấu hìnhNgoại trú
        public struct xtmzpz
        {
            public const string ZYTFPZ_INCLUDEYP = "mz.ZYTFPZ_INCLUDEYP";//Nhập viện hoàn phí cấu hình:Có hay không bao gồm thuốc 0-- không bao gồm 

            //add by HLF
            public const string BRXZ_ZFJC = "BRXZ_ZFJC";//Tính chất bệnh nhân tự trả giư ờng gia đ ình 
            public const string BRXZ_YBJC = "BRXZ_YBJC";//Tính chất bệnh nhân BHYT giư ờng gia đ ình 
            public const string SFXM_GHGHF = "mz.SFXM_GHGHF"; //Hạng mục thu phí không thuphí đ ăng ký khám khác
                                                              //Ngoại trú hiệu lực thời lư ợng(giờ):Khoa(ks) -1 biểu thị tất cả các khoa;ngoại trú/Cấp cứu(mz) -1 biểu thị khi ngày hiệu lực,Đ ơn vị giờ
                                                              // ví dụ:[{"ks":"-1","mz":"-1","jz":"24"},{"ks":"03.H1","mz":"-1","jz":"24"}]
            public const string MZ_ACTIVE_DURATION = "mz.MZ_ACTIVE_DURATION";
            public const string MZSFCX_DL = "mz.MZSFCX_DL"; // dùng cho ngoại trú thu phí tra cứutrang của loại lớn tổng cộng 
            //public const string GH_ZL_GROUP = "mz.GH_ZL_GROUP"; //Đ ăng ký khám nhóm 
            public const string SFXM_GHCKF = "mz.SFXM_GHCKF"; //Phí thẻ từ
            public const string SFXM_GHGBF = "mz.SFXM_GHGBF"; //Phí làm thẻ
            public const string SFXM_ZKD = "SFXM_ZKD";
            public const string DL_BSCYB = "mz.DL_BSCYB";
            public const string SFXM_FWF = "mz.SFXM_FWF";//Hạng mục thu phí phí dịch vụ 
            public const string DL_NOPRINTMX = "mz.DL_NOPRINTMX";// không inchi tiết loại lớn 
            public const string GHLX_ZJGH = "mz.GHLX_ZJGH"; //Chuyên giaĐ ăng ký khám
            public const string S20100 = "mz.S20100"; //Lịch trực ngoại trúlịch làm việcthời gian
        }

        #region 门诊退费
        public struct ybjylx
        {
            public const string ybjylx0 = "0";
            public const string ybjylx1 = "1";
            public const string ybjylx2 = "2";
            public const string ybjylx3 = "3";
            public const string ybjylx4 = "4";
            public const string ybjylx5 = "5";
            public const string ybjylx6 = "6";
        }
        public enum ybDealLB
        {
            yb_deal_wjy = 0,
            yb_deal_mzgh = 1,
            yb_deal_mzsf = 2,
            yb_deal_dbgh = 3,
            yb_deal_dbsf = 4,
            yb_deal_jcsf = 5,
            yb_deal_zysf = 6,
            yb_deal_gsgh = 7,
            yb_deal_gsmz = 8,
            yb_deal_sssf = 9,
            nb_yb_fyjs_mz_T1 = 10,
            nb_yb_bc_ztjs = 11,
            nb_yb_fyjs_zy_T1 = 12,
            yb_deal_gszy = 13,
            yb_deal_jcjz = 14,
        }
        public enum jsztEnum
        {
            YJ = 1,
            YT = 2,
            BS = 3,
            BSYT = 4,
        }
        public enum zfxzEnum
        {
            KB = 0,
            ZF = 1,
            FLZF = 2,
            JDZF = 3,
        }
        public struct fyfw
        {
            public const string fyfw0 = "0";
            public const string fyfw1 = "1";
            public const string fyfw2 = "2";
            public const string fyfw3 = "3";
        }
        public enum sfZfxzEnum
        {
            ZF = 0,
            ZL = 1,
        }
        #endregion

        #region 系统收费分类
        public struct xtsffl
        {
            public const string fzlfl = "FZLFL"; //Lịch trực ngoại trúlịch làm việcthời gian
        }
        #endregion

        public static DateTime MinDate = new DateTime(1970, 01, 01);
        public static DateTime MaxDate = new DateTime(2099, 12, 31);
    }
}
