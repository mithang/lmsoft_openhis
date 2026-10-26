using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewtouchHIS.Lib.Common.EnumExternal
{
    public class ClinicEnum
    {
        public enum EnumRecipelType
        {
            [Description("西药处方")]
            recipelType_0 = 0,
            [Description("中药处方")]
            recipelType_1 = 1,
        }

        /// <summary>
        /// tần suất - liều lượng
        /// </summary>
        public enum EnumChineseMedicineRecipelFrequency
        {
            [Description("一日一剂")]
            chineseMedicineRecipelFrequency_0 = 0,
            [Description("一日两剂")]
            chineseMedicineRecipelFrequency_1 = 1,
            [Description("一日三剂")]
            chineseMedicineRecipelFrequency_2 = 2,
        }
        /// <summary>
        /// đơn vị liều lượng
        /// </summary>
        public enum EnumMedicalDosisUnit
        {
            [Description("g")]
            medicalDosisUnit_0 = 593,
            [Description("ml")]
            medicalDosisUnit_1 = 608,
            [Description("l")]
            medicalDosisUnit_2 = 607,
            [Description("mg")]
            medicalDosisUnit_3 = 594,
            [Description("m")]
            medicalDosisUnit_4 = 603,
            [Description("%")]
            medicalDosisUnit_5 = -1,
            [Description("ug")]
            medicalDosisUnit_6 = 590,
            [Description("片")]
            medicalDosisUnit_7 = 527
        }
    }
    /// <summary>
    /// đơn vị bào chế 
    /// tương ứng HIS:[xt_ypdw]
    /// </summary>
    public enum EnumMedicalPreparationUnit
    {
        [Description("片")]
        medicalPreparationUnit_0 = 527,
        [Description("粒")]
        medicalPreparationUnit_1 = 530,
        [Description("袋")]
        medicalPreparationUnit_2 = 540,
        [Description("g")]
        medicalPreparationUnit_3 = 593,
        [Description("个")]
        medicalPreparationUnit_4 = 535,
        [Description("支")]
        medicalPreparationUnit_5 = 512,
        [Description("ml")]
        medicalPreparationUnit_6 = 608,
        [Description("mm")]
        medicalPreparationUnit_7 = 605,
        [Description("mg")]
        medicalPreparationUnit_8 = 594,
        [Description("瓶")]
        medicalPreparationUnit_9 = 526,
        [Description("本")]
        medicalPreparationUnit_10 = 513, //tờ
        [Description("ug")]
        medicalPreparationUnit_11 = 590,
        [Description("丸")]
        medicalPreparationUnit_12 = 516
    }
    /// <summary>
    /// đơn vị đóng gói
    /// </summary>
    public enum EnumMedicalPackUnit
    {
        [Description("包")]
        medicalPackUnit_0 = 542,
        [Description("袋")]
        medicalPackUnit_1 = 540,
        [Description("盒")]
        medicalPackUnit_2 = 532,
        [Description("箱")]
        medicalPackUnit_3 = -1, //HIS chưa được định nghĩa
        [Description("板")]
        medicalPackUnit_5 = -2, //HIS chưa được định nghĩa
        [Description("罐")]
        medicalPackUnit_6 = -3, //HIS chưa được định nghĩa
        [Description("支")]
        medicalPackUnit_7= 512,
        [Description("个")]
        medicalPackUnit_8= 535,
        [Description("瓶")]
        medicalPackUnit_9= 526,
        [Description("g")]
        medicalPackUnit_10= 593,
        [Description("本")]
        medicalPackUnit_11= 513
    }
    /// <summary>
    /// cách dùng thuốc Tây y
    /// </summary>
    public enum EnumWesternMedicineUse
    {
        [Description("口服")]
        westernMedicineUse_0 = 1,
        [Description("静脉注射")]
        westernMedicineUse_1 = 404,
        [Description("饭前")]
        westernMedicineUse_2 = -2, //HIS chưa được định nghĩa
        [Description("饭后")]
        westernMedicineUse_3 = -3, //HIS chưa được định nghĩa
        [Description("皮下注射")]
        westernMedicineUse_4 = 401,
        [Description("含化")]
        westernMedicineUse_5 = 3, //his dùng thuốc dưới lưỡi
        [Description("外敷")]
        westernMedicineUse_6 = 8,
        [Description("静脉滴注")]
        westernMedicineUse_7 = 405
    }
    /// <summary>
    /// cách dùng thuốc Đông y
    /// </summary>
    public enum EnumChineseMedicineRecipelUse
    {
        [Description("煎服")]
        chineseMedicineRecipelUse_0 = 1,
        [Description("熬服")]
        chineseMedicineRecipelUse_1 = -1, //HIS chưa được định nghĩa
        [Description("水冲服")]
        chineseMedicineRecipelUse_2 = -2,  //HIS chưa được định nghĩa
        [Description("泡服")]
        chineseMedicineRecipelUse_3 = 4,//his dương hóa
        [Description("冲服")]
        chineseMedicineRecipelUse_4 = 700,
        [Description("穴位注射")]
        chineseMedicineRecipelUse_5 = 620,
        [Description("水煎服")]
        chineseMedicineRecipelUse_6 = 11
    }    
    
    public enum EnumChineseMedicineRecipelTakeFrequency
    {
        [Description("每日四次")]
        chineseMedicineRecipelTakeFrequency_4 = 0,
        [Description("熬服")]
        chineseMedicineRecipelUse_1 = 1,
        [Description("水冲服")]
        chineseMedicineRecipelUse_2 = 2,
        [Description("泡服")]
        chineseMedicineRecipelUse_3 = 3,
        [Description("冲服")]
        chineseMedicineRecipelUse_4 = 5,
        [Description("穴位注射")]
        chineseMedicineRecipelUse_5 = 6,
        [Description("水煎服")]
        chineseMedicineRecipelUse_6 = 7
    }

}
