using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.EMR.Domain.DTO.OutputDto.MRUpload
{
    /// <summary>
    /// Thông tin trang đầu hồ sơ bệnh án nội trú
    /// </summary>
    public class YB_7600
    {
        /// <summary>
        /// Số thứ tự khám bệnh
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("就诊流水号")]
        public string AKC190 { get; set; }

        /// <summary>
        /// Số thứ tự hồ sơ bệnh án
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("病案流水号")]
        public string BKF303 { get; set; }

        /// <summary>
        /// Số thứ tự nhập viện
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("住院流水号")]
        public string BKC191 { get; set; }

        /// <summary>
        /// Tên cơ sở y tế
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("医疗机构名称")]
        public string AKB021 { get; set; }

        /// <summary>
        /// Mã tổ chức của cơ sở y tế
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("医疗机构组织机构代码")]
        public string BKF388 { get; set; }

        /// <summary>
        /// Mã phương thức thanh toán chi phí y tế
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("医疗费用支付方式代码")]
        public string BKF872 { get; set; }

        /// <summary>
        /// Số lần nhập viện
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("住院次数")]
        public string BKE076 { get; set; }

        /// <summary>
        /// Số thẻ sức khỏe cư dân
        /// </summary>
        /// <returns></returns>
        public string BKF636 { get; set; }

        /// <summary>
        /// Số thẻ bảo hiểm y tế
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("医保卡号")]
        public string AAZ500 { get; set; }

        /// <summary>
        /// Số hồ sơ bệnh án
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("病案号")]
        public string BKF888 { get; set; }

        /// <summary>
        /// Họ tên bệnh nhân
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("患者姓名")]
        public string AAC003 { get; set; }

        /// <summary>
        /// Mã giới tính
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("性别代码")]
        public string AAC004 { get; set; }

        /// <summary>
        /// Ngày sinh
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("出生日期")]
        public string AAC006 { get; set; }

        /// <summary>
        /// Tuổi (tuổi)
        /// </summary>
        /// <returns></returns>
        public string BKF811 { get; set; }

        /// <summary>
        /// Mã quốc tịch
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("国籍代码")]
        public string BKF638 { get; set; }

        /// <summary>
        /// Số tháng tuổi (tháng)
        /// </summary>
        /// <returns></returns>
        public string BKF411 { get; set; }

        /// <summary>
        /// Cân nặng lúc sinh của trẻ sơ sinh (g)
        /// </summary>
        /// <returns></returns>
        public string BKF640 { get; set; }

        /// <summary>
        /// Cân nặng lúc vào viện của trẻ sơ sinh (g)
        /// </summary>
        /// <returns></returns>
        public string BKF641 { get; set; }

        /// <summary>
        /// Nơi sinh _ tỉnh (khu, thành phố)
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("出生地_省")]
        public string BKF132 { get; set; }

        /// <summary>
        /// Nơi sinh _ thành phố
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("出生地_市")]
        public string BKF133 { get; set; }

        /// <summary>
        /// Nơi sinh _ huyện
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("出生地_县")]
        public string BKF134 { get; set; }

        /// <summary>
        /// Quê quán _ tỉnh (khu, thành phố)
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("籍贯_省(区、市)")]
        public string BKF643 { get; set; }

        /// <summary>
        /// Quê quán _ thành phố
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("籍贯_市")]
        public string BKF190 { get; set; }

        /// <summary>
        /// Mã dân tộc
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("民族编码")]
        public string AAC005 { get; set; }

        /// <summary>
        /// Mã loại giấy tờ tùy thân của bệnh nhân
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("患者身份证件类别代码")]
        public string AAC058 { get; set; }

        /// <summary>
        /// Số giấy tờ tùy thân của bệnh nhân
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("患者身份证件号码")]
        public string AAC002 { get; set; }

        /// <summary>
        /// Mã loại nghề nghiệp
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("职业类别代码")]
        public string BKF637 { get; set; }

        /// <summary>
        /// Mã tình trạng hôn nhân
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("婚姻状况代码")]
        public string AAC017 { get; set; }

        /// <summary>
        /// Địa chỉ hiện tại _ tỉnh (khu, thành phố)
        /// </summary>
        /// <returns></returns>
        public string BKF366 { get; set; }

        /// <summary>
        /// Địa chỉ hiện tại - thành phố (địa khu, châu)
        /// </summary>
        /// <returns></returns>
        public string BKF363 { get; set; }

        /// <summary>
        /// Địa chỉ hiện tại - huyện (quận)
        /// </summary>
        /// <returns></returns>
        public string BKF364 { get; set; }

        /// <summary>
        /// Địa chỉ hiện tại - xã (trấn, văn phòng đường phố)
        /// </summary>
        /// <returns></returns>
        public string BKF365 { get; set; }

        /// <summary>
        /// Địa chỉ hiện tại - thôn (phố, đường, ngõ...)
        /// </summary>
        /// <returns></returns>
        public string BKF361 { get; set; }

        /// <summary>
        /// Địa chỉ hiện tại - số nhà
        /// </summary>
        /// <returns></returns>
        public string BKF362 { get; set; }

        /// <summary>
        /// Địa chỉ hiện tại _ mã bưu chính
        /// </summary>
        /// <returns></returns>
        public string BKF649 { get; set; }

        /// <summary>
        /// Số điện thoại của bệnh nhân
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("患者电话号码")]
        public string BKC280 { get; set; }

        /// <summary>
        /// Địa chỉ hộ khẩu - tỉnh (khu tự trị, thành phố trực thuộc trung ương)
        /// </summary>
        /// <returns></returns>
        public string BKF180 { get; set; }

        /// <summary>
        /// Địa chỉ hộ khẩu - thành phố (địa khu, châu)
        /// </summary>
        /// <returns></returns>
        public string BKF181 { get; set; }

        /// <summary>
        /// Địa chỉ hộ khẩu - huyện (quận)
        /// </summary>
        /// <returns></returns>
        public string BKF182 { get; set; }

        /// <summary>
        /// Địa chỉ hộ khẩu - xã (trấn, văn phòng đường phố)
        /// </summary>
        /// <returns></returns>
        public string BKF183 { get; set; }

        /// <summary>
        /// Địa chỉ hộ khẩu - thôn (phố, đường, ngõ...)
        /// </summary>
        /// <returns></returns>
        public string BKF178 { get; set; }

        /// <summary>
        /// Địa chỉ hộ khẩu - số nhà
        /// </summary>
        /// <returns></returns>
        public string BKF179 { get; set; }

        /// <summary>
        /// Địa chỉ hộ khẩu _ mã bưu chính
        /// </summary>
        /// <returns></returns>
        public string BKF184 { get; set; }

        /// <summary>
        /// Tên đơn vị công tác
        /// </summary>
        /// <returns></returns>
        public string AAB004 { get; set; }

        /// <summary>
        /// Địa chỉ đơn vị công tác _ tỉnh (khu tự trị, thành phố trực thuộc trung ương)
        /// </summary>
        /// <returns></returns>
        public string BKF163 { get; set; }

        /// <summary>
        /// Địa chỉ đơn vị công tác _ thành phố (địa khu, châu)
        /// </summary>
        /// <returns></returns>
        public string BKF164 { get; set; }

        /// <summary>
        /// Địa chỉ đơn vị công tác _ huyện (quận)
        /// </summary>
        /// <returns></returns>
        public string BKF165 { get; set; }

        /// <summary>
        /// Địa chỉ đơn vị công tác _ xã (trấn, văn phòng đường phố)
        /// </summary>
        /// <returns></returns>
        public string BKF166 { get; set; }

        /// <summary>
        /// Địa chỉ đơn vị công tác _ thôn (phố, đường, ngõ...)
        /// </summary>
        /// <returns></returns>
        public string BKF161 { get; set; }

        /// <summary>
        /// Địa chỉ đơn vị công tác _ số nhà
        /// </summary>
        /// <returns></returns>
        public string BKF162 { get; set; }

        /// <summary>
        /// Địa chỉ đơn vị công tác _ mã bưu chính
        /// </summary>
        /// <returns></returns>
        public string BKF652 { get; set; }

        /// <summary>
        /// Số điện thoại đơn vị công tác
        /// </summary>
        /// <returns></returns>
        public string BKF651 { get; set; }

        /// <summary>
        /// Họ tên người liên hệ
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("联系人姓名")]
        public string AAE004 { get; set; }

        /// <summary>
        /// Mã quan hệ giữa người liên hệ và bệnh nhân
        /// </summary>
        /// <returns></returns>
        public string BKF654 { get; set; }

        /// <summary>
        /// Địa chỉ người liên hệ _ tỉnh (khu tự trị, thành phố trực thuộc trung ương)
        /// </summary>
        /// <returns></returns>
        public string BKF219 { get; set; }

        /// <summary>
        /// Địa chỉ người liên hệ _ thành phố (địa khu, châu)
        /// </summary>
        /// <returns></returns>
        public string BKF220 { get; set; }

        /// <summary>
        /// Địa chỉ người liên hệ _ huyện (quận)
        /// </summary>
        /// <returns></returns>
        public string BKF221 { get; set; }

        /// <summary>
        /// Địa chỉ người liên hệ _ xã (trấn, văn phòng đường phố)
        /// </summary>
        /// <returns></returns>
        public string BKF222 { get; set; }

        /// <summary>
        /// Địa chỉ người liên hệ _ thôn (phố, đường, ngõ...)
        /// </summary>
        /// <returns></returns>
        public string BKF217 { get; set; }

        /// <summary>
        /// Địa chỉ người liên hệ _ số nhà
        /// </summary>
        /// <returns></returns>
        public string BKF218 { get; set; }

        /// <summary>
        /// Số điện thoại người liên hệ
        /// </summary>
        /// <returns></returns>
        public string AAE005 { get; set; }

        /// <summary>
        /// Đường vào viện
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("入院途径")]
        public string BKF818 { get; set; }

        /// <summary>
        /// Ngày giờ vào viện
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("入院日期时间")]
        public string BKC192 { get; set; }

        /// <summary>
        /// Mã khoa vào viện
        /// </summary>
        /// <returns></returns>
        public string BKF659 { get; set; }

        /// <summary>
        /// Tên khoa vào viện
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("入院科室名称")]
        public string BKF660 { get; set; }

        /// <summary>
        /// Phòng bệnh lúc vào viện
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("入院病房")]
        public string BKF261 { get; set; }

        /// <summary>
        /// Mã khoa chuyển đến
        /// </summary>
        /// <returns></returns>
        public string BKF551 { get; set; }

        /// <summary>
        /// Tên khoa chuyển đến
        /// </summary>
        /// <returns></returns>
        public string BKF552 { get; set; }

        /// <summary>
        /// Ngày giờ ra viện
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("出院日期时间")]
        public string AKC194 { get; set; }

        /// <summary>
        /// Mã khoa ra viện
        /// </summary>
        /// <returns></returns>
        public string BKF669 { get; set; }

        /// <summary>
        /// Tên khoa ra viện
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("出院科室名称")]
        public string BKF670 { get; set; }

        /// <summary>
        /// Phòng bệnh lúc ra viện
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("出院病房")]
        public string BKF853 { get; set; }

        /// <summary>
        /// Số ngày nằm viện thực tế
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("实际住院天数")]
        public string BKF668 { get; set; }

        /// <summary>
        /// Chẩn đoán ngoại trú (cấp cứu) (chẩn đoán Đông y) mã tên bệnh
        /// </summary>
        /// <returns></returns>
        public string BKF855 { get; set; }

        /// <summary>
        /// Chẩn đoán ngoại trú (cấp cứu) (chẩn đoán Đông y) tên
        /// </summary>
        /// <returns></returns>
        public string BKF854 { get; set; }

        /// <summary>
        /// Chẩn đoán ngoại trú (cấp cứu) (chứng hậu Đông y) mã chứng hậu
        /// </summary>
        /// <returns></returns>
        public string BKF241 { get; set; }

        /// <summary>
        /// Chẩn đoán ngoại trú (cấp cứu) (chứng hậu Đông y) tên
        /// </summary>
        /// <returns></returns>
        public string BKF240 { get; set; }

        /// <summary>
        /// Chẩn đoán ngoại trú (cấp cứu) (chẩn đoán Tây y) mã bệnh
        /// </summary>
        /// <returns></returns>
        public string BKF857 { get; set; }

        /// <summary>
        /// Chẩn đoán ngoại trú (cấp cứu) (chẩn đoán Tây y) tên
        /// </summary>
        /// <returns></returns>
        public string BKF856 { get; set; }

        /// <summary>
        /// Mã cờ thực hiện lộ trình lâm sàng
        /// </summary>
        /// <returns></returns>
        public string BKF858 { get; set; }

        /// <summary>
        /// Cờ sử dụng chế phẩm thuốc Đông y của cơ sở y tế
        /// </summary>
        /// <returns></returns>
        public string BKF859 { get; set; }

        /// <summary>
        /// Cờ sử dụng thiết bị chẩn trị Đông y
        /// </summary>
        /// <returns></returns>
        public string BKF860 { get; set; }

        /// <summary>
        /// Cờ sử dụng kỹ thuật Đông y
        /// </summary>
        /// <returns></returns>
        public string BKF861 { get; set; }

        /// <summary>
        /// Cờ điều dưỡng theo biện chứng
        /// </summary>
        /// <returns></returns>
        public string BKF862 { get; set; }

        /// <summary>
        /// Mã nguyên nhân bên ngoài gây tổn thương, ngộ độc
        /// </summary>
        /// <returns></returns>
        public string BKF863 { get; set; }

        /// <summary>
        /// Mã chẩn đoán bệnh lý
        /// </summary>
        /// <returns></returns>
        public string BKF865 { get; set; }

        /// <summary>
        /// Tên chẩn đoán bệnh lý
        /// </summary>
        /// <returns></returns>
        public string BKF675 { get; set; }

        /// <summary>
        /// Số bệnh lý
        /// </summary>
        /// <returns></returns>
        public string BKF676 { get; set; }

        /// <summary>
        /// Cờ dị ứng thuốc: 1 Có; 2 Không
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("药物过敏标志")]
        public string BKF677 { get; set; }

        /// <summary>
        /// Thuốc gây dị ứng 
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("过敏药物")]
        public string AKE001 { get; set; }

        /// <summary>
        /// Cờ khám nghiệm tử thi bệnh nhân tử vong: 1 Có; 2 Không;
        /// </summary>
        /// <returns></returns>
        public string BKF324 { get; set; }

        /// <summary>
        /// Mã nhóm máu ABO
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("ABO血型代码")]
        public string BKF867 { get; set; }

        /// <summary>
        /// Mã nhóm máu Rh(D)
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("Rh(D)血型代码")]
        public string BKF868 { get; set; }

        /// <summary>
        /// Họ tên trưởng khoa
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("科主任姓名")]
        public string BKF684 { get; set; }

        /// <summary>
        /// Họ tên bác sĩ trưởng khoa (phó trưởng khoa)
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("主任(副主任)医师姓名")]
        public string BKF696 { get; set; }

        /// <summary>
        /// Mã bác sĩ điều trị chính
        /// </summary>
        /// <returns></returns>
        public string BKF050 { get; set; }

        /// <summary>
        /// Họ tên bác sĩ điều trị chính
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("主治医师姓名")]
        public string AKC273 { get; set; }

        /// <summary>
        /// Họ tên bác sĩ nội trú
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("住院医师姓名")]
        public string BKF692 { get; set; }

        /// <summary>
        /// Họ tên điều dưỡng phụ trách
        /// </summary>
        /// <returns></returns>
        public string BKF412 { get; set; }

        /// <summary>
        /// Họ tên bác sĩ tu nghiệp
        /// </summary>
        /// <returns></returns>
        public string BKF211 { get; set; }

        /// <summary>
        /// Họ tên bác sĩ thực tập
        /// </summary>
        /// <returns></returns>
        public string BKF267 { get; set; }

        /// <summary>
        /// Họ tên nhân viên mã hóa hồ sơ bệnh án
        /// </summary>
        /// <returns></returns>
        public string BKF105 { get; set; }

        /// <summary>
        /// Chất lượng hồ sơ bệnh án
        /// </summary>
        /// <returns></returns>
        public string BKF106 { get; set; }

        /// <summary>
        /// Họ tên bác sĩ kiểm soát chất lượng
        /// </summary>
        /// <returns></returns>
        public string BKF431 { get; set; }

        /// <summary>
        /// Họ tên điều dưỡng kiểm soát chất lượng
        /// </summary>
        /// <returns></returns>
        public string BKF429 { get; set; }

        /// <summary>
        /// Ngày kiểm soát chất lượng
        /// </summary>
        /// <returns></returns>
        public string BKF430 { get; set; }

        /// <summary>
        /// Hình thức rời viện
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("离院方式")]
        public string BKF705 { get; set; }

        /// <summary>
        /// Tên cơ sở y tế dự kiến tiếp nhận
        /// </summary>
        /// <returns></returns>
        public string BKF706 { get; set; }

        /// <summary>
        /// Cờ tái nhập viện trong 31 ngày sau ra viện
        /// </summary>
        /// <returns></returns>
        public string BKF137 { get; set; }

        /// <summary>
        /// Mục đích tái nhập viện trong 31 ngày sau ra viện
        /// </summary>
        /// <returns></returns>
        public string BKF138 { get; set; }

        /// <summary>
        /// Thời gian hôn mê trước khi vào viện của bệnh nhân chấn thương sọ não - ngày
        /// </summary>
        /// <returns></returns>
        public string BKF226 { get; set; }

        /// <summary>
        /// Thời gian hôn mê trước khi vào viện của bệnh nhân chấn thương sọ não - giờ
        /// </summary>
        /// <returns></returns>
        public string BKF227 { get; set; }

        /// <summary>
        /// Thời gian hôn mê trước khi vào viện của bệnh nhân chấn thương sọ não - phút
        /// </summary>
        /// <returns></returns>
        public string BKF228 { get; set; }

        /// <summary>
        /// Thời gian hôn mê sau khi vào viện của bệnh nhân chấn thương sọ não - ngày
        /// </summary>
        /// <returns></returns>
        public string BKF223 { get; set; }

        /// <summary>
        /// Thời gian hôn mê sau khi vào viện của bệnh nhân chấn thương sọ não - giờ
        /// </summary>
        /// <returns></returns>
        public string BKF224 { get; set; }

        /// <summary>
        /// Thời gian hôn mê sau khi vào viện của bệnh nhân chấn thương sọ não - phút
        /// </summary>
        /// <returns></returns>
        public string BKF225 { get; set; }

        /// <summary>
        /// Tình hình ra viện (diễn biến bệnh)
        /// </summary>
        /// <returns></returns>
        public string BKF758 { get; set; }

        /// <summary>
        /// Tình hình phù hợp chẩn đoán - ngoại trú và nội trú
        /// </summary>
        /// <returns></returns>
        public string BKF880 { get; set; }

        /// <summary>
        /// Tình hình phù hợp chẩn đoán - vào viện và ra viện
        /// </summary>
        /// <returns></returns>
        public string BKF882 { get; set; }

        /// <summary>
        /// Tình hình phù hợp chẩn đoán - trước mổ và sau mổ
        /// </summary>
        /// <returns></returns>
        public string BKF877 { get; set; }

        /// <summary>
        /// Tình hình phù hợp chẩn đoán - lâm sàng và bệnh lý
        /// </summary>
        /// <returns></returns>
        public string BKF878 { get; set; }

        /// <summary>
        /// Tình hình phù hợp chẩn đoán - chẩn đoán hình ảnh và bệnh lý
        /// </summary>
        /// <returns></returns>
        public string BKF879 { get; set; }

        /// <summary>
        /// Số lần cấp cứu
        /// </summary>
        /// <returns></returns>
        public string BKF890 { get; set; }

        /// <summary>
        /// Số lần cấp cứu thành công
        /// </summary>
        /// <returns></returns>
        public string BKF891 { get; set; }

        /// <summary>
        /// Tổng chi phí nằm viện (nhân dân tệ)
        /// </summary>
        /// <returns></returns>
        public decimal? BKF478 { get; set; }

        /// <summary>
        /// Tổng chi phí nằm viện _ số tiền tự trả (nhân dân tệ)
        /// </summary>
        /// <returns></returns>
        public decimal? BKF479 { get; set; }

        /// <summary>
        /// Phí dịch vụ y tế tổng hợp _ phí dịch vụ y tế thông thường
        /// </summary>
        /// <returns></returns>
        public decimal? BKF563 { get; set; }

        /// <summary>
        /// Phí dịch vụ y tế tổng hợp _ phí thủ thuật điều trị thông thường
        /// </summary>
        /// <returns></returns>
        public decimal? BKF564 { get; set; }

        /// <summary>
        /// Phí dịch vụ y tế thông thường _ phí biện chứng luận trị Đông y
        /// </summary>
        /// <returns></returns>
        public decimal? BKF386 { get; set; }

        /// <summary>
        /// Phí dịch vụ y tế thông thường _ phí hội chẩn biện chứng luận trị Đông y
        /// </summary>
        /// <returns></returns>
        public decimal? BKF387 { get; set; }

        /// <summary>
        /// Phí dịch vụ y tế tổng hợp _ phí điều dưỡng
        /// </summary>
        /// <returns></returns>
        public decimal? BKF561 { get; set; }

        /// <summary>
        /// Phí dịch vụ y tế tổng hợp _ chi phí khác
        /// </summary>
        /// <returns></returns>
        public decimal? BKF562 { get; set; }

        /// <summary>
        /// Chẩn đoán _ phí chẩn đoán bệnh lý
        /// </summary>
        /// <returns></returns>
        public decimal? BKF413 { get; set; }

        /// <summary>
        /// Chẩn đoán _ phí chẩn đoán xét nghiệm
        /// </summary>
        /// <returns></returns>
        public decimal? BKF415 { get; set; }

        /// <summary>
        /// Chẩn đoán _ phí chẩn đoán hình ảnh
        /// </summary>
        /// <returns></returns>
        public decimal? BKF416 { get; set; }

        /// <summary>
        /// Chẩn đoán _ phí hạng mục chẩn đoán lâm sàng
        /// </summary>
        /// <returns></returns>
        public decimal? BKF414 { get; set; }

        /// <summary>
        /// Điều trị _ phí hạng mục điều trị không phẫu thuật
        /// </summary>
        /// <returns></returns>
        public decimal? BKF432 { get; set; }

        /// <summary>
        /// Điều trị _ phí hạng mục điều trị không phẫu thuật _ phí vật lý trị liệu lâm sàng
        /// </summary>
        /// <returns></returns>
        public decimal? BKF433 { get; set; }

        /// <summary>
        /// Điều trị _ phí điều trị phẫu thuật
        /// </summary>
        /// <returns></returns>
        public decimal? BKF434 { get; set; }

        /// <summary>
        /// Điều trị _ phí điều trị phẫu thuật _ phí gây mê
        /// </summary>
        /// <returns></returns>
        public decimal? BKF435 { get; set; }

        /// <summary>
        /// Điều trị _ phí điều trị phẫu thuật _ phí phẫu thuật
        /// </summary>
        /// <returns></returns>
        public decimal? BKF436 { get; set; }

        /// <summary>
        /// Loại phục hồi chức năng - phí phục hồi chức năng
        /// </summary>
        /// <returns></returns>
        public decimal? AAE019 { get; set; }

        /// <summary>
        /// Loại Đông y _ phí chẩn đoán Đông y
        /// </summary>
        /// <returns></returns>
        public decimal? BKF465 { get; set; }

        /// <summary>
        /// Loại Đông y _ phí điều trị Đông y
        /// </summary>
        /// <returns></returns>
        public decimal? BKF461 { get; set; }

        /// <summary>
        /// Loại Đông y _ phí điều trị Đông y _ phí điều trị ngoài Đông y
        /// </summary>
        /// <returns></returns>
        public decimal? BKF472 { get; set; }

        /// <summary>
        /// Loại Đông y _ phí điều trị Đông y _ phí chỉnh hình Đông y
        /// </summary>
        /// <returns></returns>
        public decimal? BKF469 { get; set; }

        /// <summary>
        /// Loại Đông y _ phí điều trị Đông y _ phí châm cứu và cứu ngải
        /// </summary>
        /// <returns></returns>
        public decimal? BKF467 { get; set; }

        /// <summary>
        /// Loại Đông y _ phí điều trị Đông y _ phí xoa bóp Đông y
        /// </summary>
        /// <returns></returns>
        public decimal? BKF471 { get; set; }

        /// <summary>
        /// Loại Đông y _ phí điều trị Đông y _ phí điều trị hậu môn - trực tràng Đông y
        /// </summary>
        /// <returns></returns>
        public decimal? BKF468 { get; set; }

        /// <summary>
        /// Loại Đông y _ phí điều trị Đông y _ phí điều trị đặc biệt Đông y
        /// </summary>
        /// <returns></returns>
        public decimal? BKF470 { get; set; }

        /// <summary>
        /// Loại Đông y _ phí Đông y khác
        /// </summary>
        /// <returns></returns>
        public decimal? BKF462 { get; set; }

        /// <summary>
        /// Loại Đông y _ phí Đông y khác _ phí pha chế gia công đặc biệt Đông y
        /// </summary>
        /// <returns></returns>
        public decimal? BKF464 { get; set; }

        /// <summary>
        /// Loại Đông y _ phí Đông y khác _ phí ăn uống theo biện chứng
        /// </summary>
        /// <returns></returns>
        public decimal? BKF463 { get; set; }

        /// <summary>
        /// Loại Tây y - phí thuốc Tây
        /// </summary>
        /// <returns></returns>
        public decimal? BKF356 { get; set; }

        /// <summary>
        /// Loại Tây y - phí thuốc Tây _ chi phí thuốc kháng khuẩn
        /// </summary>
        /// <returns></returns>
        public decimal? BKF357 { get; set; }

        /// <summary>
        /// Loại thuốc Đông y _ phí thuốc thành phẩm Đông y
        /// </summary>
        /// <returns></returns>
        public decimal? BKF453 { get; set; }

        /// <summary>
        /// Phí thuốc thành phẩm Đông y - phí chế phẩm thuốc Đông y của cơ sở y tế
        /// </summary>
        /// <returns></returns>
        public decimal? BKF450 { get; set; }

        /// <summary>
        /// Loại thuốc Đông y _ phí thảo dược Đông y
        /// </summary>
        /// <returns></returns>
        public decimal? BKF452 { get; set; }

        /// <summary>
        /// Loại máu và chế phẩm máu - phí máu
        /// </summary>
        /// <returns></returns>
        public decimal? BKF376 { get; set; }

        /// <summary>
        /// Loại máu và chế phẩm máu - phí chế phẩm albumin
        /// </summary>
        /// <returns></returns>
        public decimal? BKF372 { get; set; }

        /// <summary>
        /// Loại máu và chế phẩm máu - phí chế phẩm globulin
        /// </summary>
        /// <returns></returns>
        public decimal? BKF374 { get; set; }

        /// <summary>
        /// Loại máu và chế phẩm máu - phí chế phẩm yếu tố đông máu
        /// </summary>
        /// <returns></returns>
        public decimal? BKF373 { get; set; }

        /// <summary>
        /// Loại máu và chế phẩm máu - phí chế phẩm cytokine
        /// </summary>
        /// <returns></returns>
        public decimal? BKF375 { get; set; }

        /// <summary>
        /// Loại vật tư tiêu hao - phí vật tư y tế dùng một lần cho kiểm tra
        /// </summary>
        /// <returns></returns>
        public decimal? BKF171 { get; set; }

        /// <summary>
        /// Loại vật tư tiêu hao - phí vật tư y tế dùng một lần cho điều trị
        /// </summary>
        /// <returns></returns>
        public decimal? BKF173 { get; set; }

        /// <summary>
        /// Loại vật tư tiêu hao - phí vật tư y tế dùng một lần cho phẫu thuật
        /// </summary>
        /// <returns></returns>
        public decimal? BKF172 { get; set; }

        /// <summary>
        /// Loại khác - phí khác
        /// </summary>
        /// <returns></returns>
        public decimal? BKF250 { get; set; }

        /// <summary>
        /// Cân nặng (kg)
        /// </summary>
        /// <returns></returns>
        public string BKF353 { get; set; }
    }
}
