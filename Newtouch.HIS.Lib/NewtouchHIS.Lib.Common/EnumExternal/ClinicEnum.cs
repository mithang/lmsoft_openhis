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
            [Description("Đơn thuốc tây")]
            recipelType_0 = 0,
            [Description("Đơn thuốc đông y")]
            recipelType_1 = 1,
        }

        /// <summary>
        /// Liều lượng tần suất
        /// </summary>
        public enum EnumChineseMedicineRecipelFrequency
        {
            [Description("Một ngày một thang")]
            chineseMedicineRecipelFrequency_0 = 0,
            [Description("Một ngày hai thang")]
            chineseMedicineRecipelFrequency_1 = 1,
            [Description("Một ngày ba thang")]
            chineseMedicineRecipelFrequency_2 = 2,
        }
        /// <summary>
        /// Đơn vị liều lượng
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
            [Description("Viên")]
            medicalDosisUnit_7 = 527
        }
    }
    /// <summary>
    /// Đơn vị bào chế 
    /// tương ứng HIS:[xt_ypdw]
    /// </summary>
    public enum EnumMedicalPreparationUnit
    {
        [Description("Viên")]
        medicalPreparationUnit_0 = 527,
        [Description("Hạt")]
        medicalPreparationUnit_1 = 530,
        [Description("Túi")]
        medicalPreparationUnit_2 = 540,
        [Description("g")]
        medicalPreparationUnit_3 = 593,
        [Description("Cái")]
        medicalPreparationUnit_4 = 535,
        [Description("Ống")]
        medicalPreparationUnit_5 = 512,
        [Description("ml")]
        medicalPreparationUnit_6 = 608,
        [Description("mm")]
        medicalPreparationUnit_7 = 605,
        [Description("mg")]
        medicalPreparationUnit_8 = 594,
        [Description("Lọ")]
        medicalPreparationUnit_9 = 526,
        [Description("Quyển")]
        medicalPreparationUnit_10 = 513, //Tờ
        [Description("ug")]
        medicalPreparationUnit_11 = 590,
        [Description("Hoàn")]
        medicalPreparationUnit_12 = 516
    }
    /// <summary>
    /// Đơn vị đóng gói
    /// </summary>
    public enum EnumMedicalPackUnit
    {
        [Description("Gói")]
        medicalPackUnit_0 = 542,
        [Description("Túi")]
        medicalPackUnit_1 = 540,
        [Description("Hộp")]
        medicalPackUnit_2 = 532,
        [Description("Thùng")]
        medicalPackUnit_3 = -1, //HIS chưa được định nghĩa
        [Description("Vỉ")]
        medicalPackUnit_5 = -2, //HIS chưa được định nghĩa
        [Description("Bình")]
        medicalPackUnit_6 = -3, //HIS chưa được định nghĩa
        [Description("Ống")]
        medicalPackUnit_7= 512,
        [Description("Cái")]
        medicalPackUnit_8= 535,
        [Description("Lọ")]
        medicalPackUnit_9= 526,
        [Description("g")]
        medicalPackUnit_10= 593,
        [Description("Quyển")]
        medicalPackUnit_11= 513
    }
    /// <summary>
    /// Cách dùng thuốc tây
    /// </summary>
    public enum EnumWesternMedicineUse
    {
        [Description("Uống")]
        westernMedicineUse_0 = 1,
        [Description("Tiêm tĩnh mạch")]
        westernMedicineUse_1 = 404,
        [Description("Trước bữa ăn")]
        westernMedicineUse_2 = -2, //HIS chưa được định nghĩa
        [Description("Sau bữa ăn")]
        westernMedicineUse_3 = -3, //HIS chưa được định nghĩa
        [Description("Tiêm dưới da")]
        westernMedicineUse_4 = 401,
        [Description("Ngậm tan")]
        westernMedicineUse_5 = 3, //his Thuốc đặt dưới lưỡi
        [Description("Đắp ngoài")]
        westernMedicineUse_6 = 8,
        [Description("Truyền tĩnh mạch")]
        westernMedicineUse_7 = 405
    }
    /// <summary>
    /// Cách dùng thuốc đông y
    /// </summary>
    public enum EnumChineseMedicineRecipelUse
    {
        [Description("Sắc uống")]
        chineseMedicineRecipelUse_0 = 1,
        [Description("Uống thuốc nấu")]
        chineseMedicineRecipelUse_1 = -1, //HIS chưa được định nghĩa
        [Description("Uống pha nước")]
        chineseMedicineRecipelUse_2 = -2,  //HIS chưa được định nghĩa
        [Description("Hãm uống")]
        chineseMedicineRecipelUse_3 = 4,//his Hòa tan
        [Description("Uống pha")]
        chineseMedicineRecipelUse_4 = 700,
        [Description("Tiêm huyệt vị")]
        chineseMedicineRecipelUse_5 = 620,
        [Description("Sắc uống")]
        chineseMedicineRecipelUse_6 = 11
    }    
    
    public enum EnumChineseMedicineRecipelTakeFrequency
    {
        [Description("Mỗi ngày bốn lần")]
        chineseMedicineRecipelTakeFrequency_4 = 0,
        [Description("Uống thuốc nấu")]
        chineseMedicineRecipelUse_1 = 1,
        [Description("Uống pha nước")]
        chineseMedicineRecipelUse_2 = 2,
        [Description("Hãm uống")]
        chineseMedicineRecipelUse_3 = 3,
        [Description("Uống pha")]
        chineseMedicineRecipelUse_4 = 5,
        [Description("Tiêm huyệt vị")]
        chineseMedicineRecipelUse_5 = 6,
        [Description("Sắc uống")]
        chineseMedicineRecipelUse_6 = 7
    }

}
