using Newtouch.HIS.Domain.Entity;
using System;
using Newtonsoft.Json;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.BusinessObjects

{
    public class InterfaceBase
    { }
    public class MZJS
    {
        public int patid { get; set; }
        public string BRXZ { get; set; }
        public string BRXZMC { get; set; }
        public string KS { get; set; }
        public string KSMC { get; set; }
        public string YS { get; set; }
        public string RYMC { get; set; }
        public int GHNM { get; set; }
    }
    [NotMapped]

    public class MZJSInfo
    {
        public string xm { get; set; }
        public string kh { get; set; }
        public int jsnm { get; set; }
        public int patid { get; set; }
        public decimal flzffy { get; set; }
        public decimal zffy { get; set; }
        public decimal jzfy { get; set; }
        public decimal zlfy { get; set; }
        public decimal xjzf { get; set; }
        // public string xm { get; set; }

        public string fph { get; set; }
        public string fpdm { get; set; }
        public int zh { get; set; }
        public string oldFPH { get; set; }
        public DateTime? CreateTime { get; set; }
        public decimal zje { get; set; }
        public decimal zhjz { get; set; }

        public int cxjsnm { get; set; }
        public int brxzbh { get; set; }
        public int jszt { get; set; }
        public string brxz { get; set; }
        public decimal jmje { get; set; }
        public string jslx { get; set; }
        public string jylx { get; set; }
        public decimal xjwc { get; set; }
        public string xjzffs { get; set; }
        public decimal zl { get; set; }
        public decimal ysk { get; set; }
        public string czr { get; set; }
        public string blh { get; set; }

    }
    /// <summary>
    /// Bao gồm BHYT và không BHYT
    /// </summary>
    public class Brsfsf_ReturnValue_YBANDFYB
    {
        public Brsfsf_ReturnValue_YBANDFYB()
        {
            YB = new Brsfsf_ReturnValue();
            FYB = new Brsfsf_ReturnValue();
        }

        /// <summary>
        /// Tính chất bệnh nhân
        /// </summary>
        public string brxz { get; set; }

        /// <summary>
        /// Cần giao dịch BHYT
        /// </summary>
        public Brsfsf_ReturnValue YB { get; set; }

        /// <summary>
        /// Không cần giao dịch BHYT
        /// </summary>
        public Brsfsf_ReturnValue FYB { get; set; }

        /// <summary>
        /// Tổng chi phí giao dịch
        /// </summary>
        public decimal jyze
        {
            get
            {
                return YB.jyze;
            }
        }

        /// <summary>
        /// Tổng chi phí trong phạm vi quyết toán BHYT
        /// </summary>
        public decimal ybjsfwze
        {
            get
            {
                return YB.ybjsfwze;
            }
        }

        /// <summary>
        /// Cá nhân tự chi trả ngoài phạm vi quyết toán BHYT
        /// </summary>
        public decimal fybjsfwgrzf
        {
            get
            {
                return YB.fybjsfwgrzf;
            }
        }

        /// <summary>
        /// Tự chi trả theo phân loại
        /// </summary>
        public decimal flzfHj
        {
            get
            {
                return YB.flzfHj + FYB.flzfHj;
            }
        }

        /// <summary>
        /// Tự lo
        /// </summary>
        public decimal zlHj
        {
            get
            {
                return YB.zlHj + FYB.zlHj;
            }
        }

        /// <summary>
        /// Chi phí ghi sổ(Nếu là bệnh nhân BHYT,Kết quả BHYT trả về)
        /// </summary>
        public decimal jzfyHj
        {
            get
            {
                return YB.jzfyHj + FYB.jzfyHj;
            }
        }

        /// <summary>
        /// Tiền mặt(Chưa bao gồm　Số tiền tự chi trả BHYT trả về)
        /// </summary>
        public decimal xjHj
        {
            get
            {
                return YB.xjHj + FYB.xjHj;
            }
        }

        /// <summary>
        /// Số tiền giảm miễn
        /// </summary>
        public decimal jmjeHj
        {
            get
            {
                return YB.jmjeHj + FYB.jmjeHj;
            }
        }

        /// <summary>
        /// Tự chi trả theo thuật toán(Số tiền tự chi trả tính được qua thuật toán)
        /// </summary>
        public decimal sfzfHj
        {
            get
            {
                return YB.sfzfHj + FYB.sfzfHj;
            }
        }


        /// <summary>
        /// Tự lo theo thuật toán(Số tiền tự lo tính được qua thuật toán)
        /// </summary>
        public decimal sfzlHj
        {
            get
            {
                return YB.sfzlHj + FYB.sfzlHj;
            }
        }

        /// <summary>
        /// Tổng chi phí(Tiền mặt bệnh nhân tự chi trả cần phải trả)
        /// </summary>
        public decimal totalHj
        {
            get
            {
                return YB.totalHj + FYB.totalHj;
            }
        }

        /// <summary>
        /// Chi tiết giao dịch
        /// </summary>
        public List<HospTransactionSettlementDetailEntity> jsmxList { get; set; }

    }
    public class Brsfsf_ReturnValue
    {
        public Brsfsf_ReturnValue()
        {
            this.dlFymxList = new List<Brsfsf_Dl_Fymx>();
        }

        /// <summary>
        /// Tổng chi phí giao dịch
        /// </summary>
        public decimal jyze
        {
            get
            {
                if (dlFymxList == null)
                    return 0m;

                Dictionary<string, decimal> dlfy = new Dictionary<string, decimal>();

                decimal fy = 0m;
                foreach (Brsfsf_Dl_Fymx fymx in this.dlFymxList)
                {
                    string key = fymx.dl;
                    if (!dlfy.ContainsKey(key))
                        dlfy.Add(key, 0);

                    dlfy[key] = dlfy[key] + fymx.jzfy;
                }

                foreach (KeyValuePair<string, decimal> pair in dlfy)
                {
                    //Làm tròn
                    fy += Math.Round(pair.Value, 2, MidpointRounding.AwayFromZero);
                }

                return fy;
            }
        }

        /// <summary>
        /// Tổng chi phí trong phạm vi quyết toán BHYT
        /// </summary>
        public decimal ybjsfwze
        {
            get
            {
                if (dlFymxList == null)
                    return 0m;

                Dictionary<string, decimal> dlfy = new Dictionary<string, decimal>();

                decimal fy = 0m;
                foreach (Brsfsf_Dl_Fymx fymx in this.dlFymxList)
                {
                    string key = fymx.dl;
                    if (!dlfy.ContainsKey(key))
                        dlfy.Add(key, 0);

                    dlfy[key] = dlfy[key] + (fymx.jzfy + fymx.flzf);
                }

                foreach (KeyValuePair<string, decimal> pair in dlfy)
                {
                    //Làm tròn
                    fy += Math.Round(pair.Value, 2, MidpointRounding.AwayFromZero);
                }

                return fy;
            }
        }

        /// <summary>
        /// Cá nhân tự chi trả ngoài phạm vi quyết toán BHYT
        /// </summary>
        public decimal fybjsfwgrzf
        {
            get
            {
                if (dlFymxList == null)
                    return 0m;

                Dictionary<string, decimal> dlfy = new Dictionary<string, decimal>();

                decimal fy = 0m;
                foreach (Brsfsf_Dl_Fymx fymx in this.dlFymxList)
                {
                    string key = fymx.dl;
                    if (!dlfy.ContainsKey(key))
                        dlfy.Add(key, 0);

                    dlfy[key] = dlfy[key] + fymx.zl;
                }

                foreach (KeyValuePair<string, decimal> pair in dlfy)
                {
                    //Làm tròn
                    fy += Math.Round(pair.Value, 2, MidpointRounding.AwayFromZero);
                }

                return fy;
            }
        }

        /// <summary>
        /// Tự chi trả theo phân loại
        /// </summary>
        public decimal flzfHj
        {
            get
            {
                if (dlFymxList == null)
                    return 0m;

                decimal fy = 0m;
                foreach (Brsfsf_Dl_Fymx fymx in this.dlFymxList)
                {
                    fy += fymx.flzf;
                }

                return fy;
            }
        }

        /// <summary>
        /// Tự lo
        /// </summary>
        public decimal zlHj
        {
            get
            {
                if (dlFymxList == null)
                    return 0m;

                decimal fy = 0m;
                foreach (Brsfsf_Dl_Fymx fymx in this.dlFymxList)
                {
                    fy += fymx.zl;
                }

                return fy;
            }
        }

        /// <summary>
        /// Chi phí ghi sổ(Nếu là bệnh nhân BHYT,Kết quả BHYT trả về)
        /// </summary>
        public decimal jzfyHj
        {
            get
            {
                if (dlFymxList == null)
                    return 0m;

                decimal fy = 0m;
                foreach (Brsfsf_Dl_Fymx fymx in this.dlFymxList)
                {
                    fy += fymx.jzfy;
                }

                //Chi phí được ghi sổ trừ phần vượt mức
                fy = fy - jzce;
                //Số tiền khởi điểm sau khi tính toán
                if (fy > qfje)
                {
                    fy = fy - qfje;
                }
                else
                {
                    fy = 0;
                }

                return fy;
            }
        }

        /// <summary>
        /// Tiền mặt(Chưa bao gồm　Số tiền tự chi trả BHYT trả về)
        /// </summary>
        public decimal xjHj
        {
            get
            {
                if (dlFymxList == null)
                    return 0m;

                decimal fy = 0m;
                foreach (Brsfsf_Dl_Fymx fymx in this.dlFymxList)
                {
                    fy += fymx.xj;
                }

                return fy;
            }
        }

        /// <summary>
        /// Số tiền giảm miễn
        /// </summary>
        public decimal jmjeHj
        {
            get
            {
                if (dlFymxList == null)
                    return 0m;

                decimal fy = 0m;
                foreach (Brsfsf_Dl_Fymx fymx in this.dlFymxList)
                {
                    fy += fymx.jmje;
                }

                return fy;
            }
        }

        /// <summary>
        /// Tự chi trả theo thuật toán(Số tiền tự chi trả tính được qua thuật toán)
        /// </summary>
        public decimal sfzfHj
        {
            get
            {
                if (dlFymxList == null)
                    return 0m;

                decimal fy = 0m;
                foreach (Brsfsf_Dl_Fymx fymx in this.dlFymxList)
                {
                    fy += fymx.sfzf;
                }

                //Tự chi trả cộng phần vượt mức ghi sổ
                fy = fy + jzce;
                //Số tiền khởi điểm sau khi tính toán
                if (jzfyHj > qfje)
                {
                    fy = fy + qfje;
                }

                return fy;
            }
        }

        /// <summary>
        /// Tự lo theo thuật toán(Số tiền tự lo tính được qua thuật toán)
        /// </summary>
        public decimal sfzlHj
        {
            get
            {
                if (dlFymxList == null)
                    return 0m;

                decimal fy = 0m;
                foreach (Brsfsf_Dl_Fymx fymx in this.dlFymxList)
                {
                    fy += fymx.sfzl;
                }

                return fy;
            }
        }


        /// <summary>
        /// Tổng chi phí(Tiền mặt bệnh nhân tự chi trả cần phải trả)
        /// </summary>
        public decimal totalHj
        {
            get
            {
                if (dlFymxList == null)
                    return 0m;

                decimal fy = 0m;
                foreach (Brsfsf_Dl_Fymx fymx in this.dlFymxList)
                {
                    fy += fymx.total;
                }

                //Tiền mặt cộng phần vượt mức ghi sổ
                fy = fy + jzce;
                //Số tiền khởi điểm sau khi tính toán
                if (jzfyHj > qfje)
                {
                    fy = fy + qfje;
                }

                return fy;
            }
        }

        /// <summary>
        /// Chi tiết chi phí loại lớn
        /// </summary>
        public List<Brsfsf_Dl_Fymx> dlFymxList { get; set; }

        /// <summary>
        /// Chi tiết chi phí hạng mục  ----fangyi
        /// </summary>
        public List<SFXM_FYMX> fymxList { get; set; }


        /// <summary>
        /// Lấy tổng chi phí trong phạm vi quyết toán BHYT của loại lớn thu phí
        /// </summary>
        /// <returns></returns>
        public decimal getDlJyje(string dl, bool flag)
        {
            decimal amount = 0m;
            foreach (Brsfsf_Dl_Fymx fymx in dlFymxList)
            {
                if (fymx.dl == dl)
                {
                    if (flag == true)
                    {
                        amount += (fymx.total - fymx.sfzl) + (fymx.jzfy + fymx.flzf);
                    }
                    else
                    {
                        amount += (fymx.jzfy + fymx.flzf);
                    }
                    break;
                }
            }

            //Làm tròn
            amount = Math.Round(amount, 2, MidpointRounding.AwayFromZero);

            return amount;
        }


        /// <summary>
        /// Lấy tổng chi phí trong phạm vi quyết toán BHYT của loại lớn thu phí
        /// </summary>
        /// <returns></returns>
        public decimal getDlJyje(string dl)
        {
            decimal amount = 0m;
            foreach (Brsfsf_Dl_Fymx fymx in dlFymxList)
            {
                if (fymx.dl == dl)
                {
                    amount += (fymx.jzfy + fymx.flzf);
                    break;
                }
            }

            //Làm tròn
            amount = Math.Round(amount, 2, MidpointRounding.AwayFromZero);

            return amount;
        }

        /// <summary>
        /// Lấy tổng chi phí trong phạm vi quyết toán BHYT của loại lớn thu phí
        /// </summary>
        /// <param name="yydlList">Loại lớn bệnh viện</param>
        /// <returns></returns>
        public decimal getDlJyje(List<string> yydlList)
        {
            decimal amount = 0m;
            foreach (Brsfsf_Dl_Fymx fymx in dlFymxList)
            {
                if (yydlList.Contains(fymx.dl))
                {
                    amount += (fymx.jzfy + fymx.flzf);
                }
            }

            //Làm tròn
            amount = Math.Round(amount, 2, MidpointRounding.AwayFromZero);

            return amount;
        }

        /// <summary>
        /// Phần vượt mức ghi sổ(Cần chuyển sang tự chi trả)
        /// </summary>
        public decimal jzce { get; set; }

        /// <summary>
        /// Số tiền khởi điểm sau khi tính toán(Cần chuyển sang tự chi trả)
        /// </summary>
        public decimal qfje { get; set; }
    }
    /// <summary>
    /// Chi tiết chi phí loại lớn do thuật toán tính phí bệnh nhân tính ra
    /// </summary>
    public class Brsfsf_Dl_Fymx
    {
        /// <summary>
        /// Loại lớn
        /// </summary>
        public string dl { get; set; }

        /// <summary>
        /// Mã chi tiết(Tạm thời chỉ dùng cho Bảo hiểm nông thôn mới)
        /// </summary>
        //public string mxbm { get; set; }

        /// <summary>
        /// Hạng mục thu phí(Tạm thời chỉ dùng cho Bảo hiểm nông thôn mới)
        /// </summary>
        public string sfxm { get; set; }

        /// <summary>
        /// Mã nội bộ chi tiết(Tạm thời chỉ dùng cho Bảo hiểm nông thôn mới)
        /// </summary>
        public int mxnm { get; set; }

        /// <summary>
        /// Mã nội bộ chi tiết đơn thuốc(Tạm thời chỉ dùng cho Bảo hiểm nông thôn mới)
        /// </summary>
        public int cf_mxnm { get; set; }

        /// <summary>
        /// Số nhóm(Tạm thời chỉ dùng cho Bảo hiểm nông thôn mới)
        /// </summary>
        public string czh { get; set; }

        /// <summary>
        /// Tự chi trả theo phân loại
        /// </summary>
        public decimal flzf { get; set; }

        /// <summary>
        /// Tự lo
        /// </summary>
        public decimal zl { get; set; }

        /// <summary>
        /// Chi phí được ghi sổ
        /// </summary>
        public decimal jzfy { get; set; }

        /// <summary>
        /// Tiền mặt
        /// </summary>
        public decimal xj { get; set; }

        /// <summary>
        /// Số tiền giảm miễn
        /// </summary>
        public decimal jmje { get; set; }

        /// <summary>
        /// Tự chi trả theo thuật toán(Số tiền tự chi trả tính được qua thuật toán)
        /// </summary>
        public decimal sfzf { get; set; }

        /// <summary>
        /// Tự lo theo thuật toán(Số tiền tự chi trả tính được qua thuật toán)
        /// </summary>
        public decimal sfzl { get; set; }

        /// <summary>
        /// Tổng chi phí(Tiền mặt bệnh nhân tự chi trả cần phải trả)
        /// </summary>
        public decimal total { get; set; }

        /// <summary>
        /// Số bảng tính phí hạng mục nội trú
        /// </summary>
        public int xmjfbbh { get; set; }

        /// <summary>
        /// Số bảng tính phí thuốc nội trú
        /// </summary>
        public int ypjfbbh { get; set; }

        /// <summary>
        /// Số tiền giao dịch BHYT(Dữ liệu thu được sau khi tính toán)
        /// </summary>
        public decimal jyje { get; set; }

        /// <summary>
        /// Số tiền trong phạm vi giao dịch BHYT(Dữ liệu thu được sau khi tính toán)
        /// </summary>
        public decimal jyfwje { get; set; }

        /// <summary>
        /// Loại y lệnh1 Thuốc 2 Hạng mục
        /// </summary>
        public string yzlx { get; set; }
    }
    public partial class SFXM_FYMX
    {
        public SFXM_FYMX() { }

        #region model
        #region property
        private decimal _kbje;
        private decimal _flzf;
        private decimal _zlfy;
        private decimal _jmje;
        #endregion property

        /// <summary>
        /// Loại lớn
        /// </summary>
        public string dl { get; set; }

        /// <summary>
        /// Mã chi tiết
        /// </summary>
        //public string mxbm { get; set; }

        /// <summary>
        /// Hạng mục thu phí
        /// </summary>
        public string sfxm { get; set; }

        /// <summary>
        /// Mã nội bộ chi tiết
        /// </summary>
        public int mxnm { get; set; }

        /// <summary>
        /// Mã nội bộ chi tiết đơn thuốc
        /// </summary>
        public int cf_mxnm { get; set; }

        /// <summary>
        /// Số nhóm
        /// </summary>
        public string czh { get; set; }

        /// <summary>
        /// Số bảng tính phí hạng mục nội trú
        /// </summary>
        public int xmjfbbh { get; set; }

        /// <summary>
        /// Số bảng tính phí thuốc nội trú
        /// </summary>
        public int ypjfbbh { get; set; }

        /// <summary>
        /// Loại y lệnh1 Thuốc 2 Hạng mục
        /// </summary>
        public string yzlx { get; set; }

        /// <summary>
        /// Số tiền giao dịch BHYT(Dữ liệu thu được sau khi tính toán)
        /// </summary>
        public decimal jyje { get; set; }

        /// <summary>
        /// Số tiền trong phạm vi giao dịch BHYT(Dữ liệu thu được sau khi tính toán)
        /// </summary>
        public decimal jyfwje { get; set; }

        /// <summary>
        /// Số tiền được thanh toán
        /// </summary>
        public decimal Kbje
        {
            get { return _kbje; }
            set { _kbje = value; }
        }

        /// <summary>
        /// Tự chi trả theo phân loại
        /// </summary>
        public decimal Flzf
        {
            get { return _flzf; }
            set { _flzf = value; }
        }

        /// <summary>
        /// Chi phí tự lo
        /// </summary>
        public decimal Zlfy
        {
            get { return _zlfy; }
            set { _zlfy = value; }
        }

        /// <summary>
        /// Số tiền giảm miễn
        /// </summary>
        public decimal Jmje
        {
            get { return _jmje; }
            set { _jmje = value; }
        }

        #endregion model
    }
    public partial class CMSEntity
    {
        public CMSEntity() { }

        #region model
        #region property
        private string _dl;
        private string _sfxm;
        private decimal _sl;
        private decimal _kbje;
        private decimal _flzf;
        private decimal _zlfy;
        private decimal _jmje;
        #endregion property

        /// <summary>
        /// Loại lớn
        /// </summary>
        public string Dl
        {
            get { return _dl; }
            set { _dl = value; }
        }

        /// <summary>
        /// Hạng mục thu phí
        /// </summary>
        public string Sfxm
        {
            get { return _sfxm; }
            set { _sfxm = value; }
        }

        /// <summary>
        /// Số lượng,Số lượng loại lớn là1,Hạng mục thu phí là số lượng thực tế
        /// </summary>
        public decimal Sl
        {
            get { return _sl; }
            set { _sl = value; }
        }

        /// <summary>
        /// Số tiền được thanh toán
        /// </summary>
        public decimal Kbje
        {
            get { return _kbje; }
            set { _kbje = value; }
        }

        /// <summary>
        /// Tự chi trả theo phân loại
        /// </summary>
        public decimal Flzf
        {
            get { return _flzf; }
            set { _flzf = value; }
        }

        /// <summary>
        /// Chi phí tự lo
        /// </summary>
        public decimal Zlfy
        {
            get { return _zlfy; }
            set { _zlfy = value; }
        }

        /// <summary>
        /// Số tiền giảm miễn
        /// </summary>
        public decimal Jmje
        {
            get { return _jmje; }
            set { _jmje = value; }
        }

        #endregion model
    }
    [NotMapped]
    public class tbl_mz_jszffs_Ex : OutpatientSettlementPaymentModelEntity
    {
        /// <summary>
        /// Tên phương thức thanh toán tiền mặt
        /// </summary>
     //   public string xjzffsmc { get; set; }


    }
    #region 获取处方状态
    [Serializable]
    public class GET_CF_STATUS : InterfaceBase
    {
        /// <summary>
        /// Số đơn thuốc
        /// </summary>
        public string prescriptionNo { get; set; }

        /// <summary>
        /// Trạng thái đơn thuốc
        /// </summary>
        public string status { get; set; }

        /// <summary>
        /// Thông tin trả thuốc của đơn thuốc
        /// </summary>
        public List<returnDrugInfo> returnDrugInfo { get; set; }
    }
    [Serializable]
    public class returnDrugInfo : InterfaceBase
    {
        /// <summary>
        /// Mã thuốc
        /// </summary>
        public string drugCode { get; set; }

        /// <summary>
        /// Số lượng trả thuốc cấp 3
        /// </summary>
        public int thirdLevelPkgQty { get; set; }

        /// <summary>
        /// Số lượng trả thuốc cấp 4
        /// </summary>
        public int fourthLevelPkgQty { get; set; }

    }
    #endregion
    public class InterfaceRepParaBase<T>
    {
        /// <summary>
        /// Số bản ghi thao tác thành công
        /// </summary>
        [JsonProperty]
        public int successCount { get; set; }

        /// <summary>
        /// Số bản ghi thao tác thất bại
        /// </summary>
        [JsonProperty]
        public int failureCount { get; set; }

        /// <summary>
        /// Kết quả trả về khi thao tác thành công
        /// </summary>
        [JsonProperty]
        public List<T> result { get; set; }

        /// <summary>
        /// Danh sách thông báo thao tác thất bại
        /// </summary>
        [JsonProperty]
        public List<failureMessage> failureMessage { get; set; }

        /// <summary>
        /// Kết quả trả về khi thao tác thành công
        /// </summary>
        [JsonIgnore]
        public string failureMessageShow
        {
            get
            {
                string strMsg = string.Empty;
                foreach (failureMessage m in failureMessage)
                {
                    strMsg += m.id + ":" + m.errorMessage + ";";
                }

                return strMsg;
            }
        }
    }
    [Serializable]
    [JsonObject(MemberSerialization.OptIn)]
    public class failureMessage
    {
        /// <summary>
        /// Định danh duy nhất của đối tượng nghiệp vụ phát sinh ngoại lệ
        /// </summary>
        [JsonProperty]
        public string id { get; set; }

        /// <summary>
        /// Mã bất thường
        /// </summary>
        [JsonProperty]
        public string errorCode { get; set; }

        /// <summary>
        /// Mô tả bất thường
        /// </summary>
        [JsonProperty]
        public string errorMessage { get; set; }

    }

    #region 处方退费通知

    [Serializable]
    public class ADD_CF_TFTZ : InterfaceBase
    {
        /// <summary>
        /// Số đơn thuốc
        /// </summary>
        public string pn { get; set; }

        /// <summary>
        /// Thông tin trả thuốc của đơn thuốc
        /// </summary>
        public List<pd> pd { get; set; }
    }

    [Serializable]
    public class pd : InterfaceBase
    {
        /// <summary>
        /// Mã thuốc
        /// </summary>
        public string dc { get; set; }

        /// <summary>
        /// Số lượng hoàn phí đóng gói cấp 3
        /// </summary>
        public int tq { get; set; }

        /// <summary>
        /// Số lượng hoàn phí đóng gói cấp 4
        /// </summary>
        public int fq { get; set; }
    }

    #endregion
}
