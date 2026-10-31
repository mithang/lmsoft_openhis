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
    /// Thông tin trang đầu bệnh án nội trú
    /// </summary>
    public class YB_7600
    {
        /// <summary>
        /// Số thứ tự khám
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("Số thứ tự khám")]
        public string AKC190 { get; set; }

        /// <summary>
        /// Số thứ tự bệnh án
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("Số thứ tự bệnh án")]
        public string BKF303 { get; set; }

        /// <summary>
        /// Số thứ tự nội trú
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("Số thứ tự nội trú")]
        public string BKC191 { get; set; }

        /// <summary>
        /// Tên cơ sở y tế
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("Tên cơ sở y tế")]
        public string AKB021 { get; set; }

        /// <summary>
        /// Mã tổ chức của cơ sở y tế
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("Mã tổ chức của cơ sở y tế")]
        public string BKF388 { get; set; }

        /// <summary>
        /// Mã phương thức thanh toán chi phí y tế
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("Mã phương thức thanh toán chi phí y tế")]
        public string BKF872 { get; set; }

        /// <summary>
        /// Số lần nhập viện
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("Số lần nhập viện")]
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
        [Description("Số thẻ bảo hiểm y tế")]
        public string AAZ500 { get; set; }

        /// <summary>
        /// Số hồ sơ bệnh án
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("Số hồ sơ bệnh án")]
        public string BKF888 { get; set; }

        /// <summary>
        /// Tên bệnh nhân
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("Tên bệnh nhân")]
        public string AAC003 { get; set; }

        /// <summary>
        /// Mã giới tính
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("Mã giới tính")]
        public string AAC004 { get; set; }

        /// <summary>
        /// Ngày sinh
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("Ngày sinh")]
        public string AAC006 { get; set; }

        /// <summary>
        /// Tuổi( tuổi)
        /// </summary>
        /// <returns></returns>
        public string BKF811 { get; set; }

        /// <summary>
        /// Mã quốc tịch
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("Mã quốc tịch")]
        public string BKF638 { get; set; }

        /// <summary>
        /// Tuổi theo tháng(Tháng)
        /// </summary>
        /// <returns></returns>
        public string BKF411 { get; set; }

        /// <summary>
        /// Cân nặng lúc sinh của trẻ sơ sinh(g)
        /// </summary>
        /// <returns></returns>
        public string BKF640 { get; set; }

        /// <summary>
        /// Cân nặng nhập viện của trẻ sơ sinh(g)
        /// </summary>
        /// <returns></returns>
        public string BKF641 { get; set; }

        /// <summary>
        /// Nơi sinh_Tỉnh(Quận,Thành phố)
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("Nơi sinh_Tỉnh")]
        public string BKF132 { get; set; }

        /// <summary>
        /// Nơi sinh_Thành phố
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("Nơi sinh_Thành phố")]
        public string BKF133 { get; set; }

        /// <summary>
        /// Nơi sinh_Huyện
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("Nơi sinh_Huyện")]
        public string BKF134 { get; set; }

        /// <summary>
        /// Quê quán_Tỉnh(Quận,Thành phố)
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("Quê quán_Tỉnh(Quận、Thành phố)")]
        public string BKF643 { get; set; }

        /// <summary>
        /// Quê quán_Thành phố
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("Quê quán_Thành phố")]
        public string BKF190 { get; set; }

        /// <summary>
        /// Mã dân tộc
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("Mã dân tộc")]
        public string AAC005 { get; set; }

        /// <summary>
        /// Mã loại giấy tờ tùy thân của bệnh nhân
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("Mã loại giấy tờ tùy thân của bệnh nhân")]
        public string AAC058 { get; set; }

        /// <summary>
        /// Số giấy tờ tùy thân của bệnh nhân
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("Số giấy tờ tùy thân của bệnh nhân")]
        public string AAC002 { get; set; }

        /// <summary>
        /// Mã loại nghề nghiệp
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("Mã loại nghề nghiệp")]
        public string BKF637 { get; set; }

        /// <summary>
        /// Mã tình trạng hôn nhân
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("Mã tình trạng hôn nhân")]
        public string AAC017 { get; set; }

        /// <summary>
        /// Địa chỉ hiện tại_Tỉnh(Quận,Thành phố)
        /// </summary>
        /// <returns></returns>
        public string BKF366 { get; set; }

        /// <summary>
        /// Địa chỉ hiện tại-Thành phố(Khu vực,Châu)
        /// </summary>
        /// <returns></returns>
        public string BKF363 { get; set; }

        /// <summary>
        /// Địa chỉ hiện tại-Huyện(Quận)
        /// </summary>
        /// <returns></returns>
        public string BKF364 { get; set; }

        /// <summary>
        /// Địa chỉ hiện tại-Xã(Thị trấn,Văn phòng phường)
        /// </summary>
        /// <returns></returns>
        public string BKF365 { get; set; }

        /// <summary>
        /// Địa chỉ hiện tại-thôn(phố,đường,ngõ,...)
        /// </summary>
        /// <returns></returns>
        public string BKF361 { get; set; }

        /// <summary>
        /// Địa chỉ hiện tại-Số nhà
        /// </summary>
        /// <returns></returns>
        public string BKF362 { get; set; }

        /// <summary>
        /// Địa chỉ hiện tại_Mã bưu chính
        /// </summary>
        /// <returns></returns>
        public string BKF649 { get; set; }

        /// <summary>
        /// Số điện thoại bệnh nhân
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("Số điện thoại bệnh nhân")]
        public string BKC280 { get; set; }

        /// <summary>
        /// Địa chỉ hộ khẩu-Tỉnh(Khu tự trị,Thành phố trực thuộc trung ương)
        /// </summary>
        /// <returns></returns>
        public string BKF180 { get; set; }

        /// <summary>
        /// Địa chỉ hộ khẩu-Thành phố(Khu vực,Châu)
        /// </summary>
        /// <returns></returns>
        public string BKF181 { get; set; }

        /// <summary>
        /// Địa chỉ hộ khẩu-Huyện(Quận)
        /// </summary>
        /// <returns></returns>
        public string BKF182 { get; set; }

        /// <summary>
        /// Địa chỉ hộ khẩu-Xã(Thị trấn,Văn phòng phường)
        /// </summary>
        /// <returns></returns>
        public string BKF183 { get; set; }

        /// <summary>
        /// Địa chỉ hộ khẩu-thôn(phố,đường,ngõ,...)
        /// </summary>
        /// <returns></returns>
        public string BKF178 { get; set; }

        /// <summary>
        /// Địa chỉ hộ khẩu-Số nhà
        /// </summary>
        /// <returns></returns>
        public string BKF179 { get; set; }

        /// <summary>
        /// Địa chỉ hộ khẩu_Mã bưu chính
        /// </summary>
        /// <returns></returns>
        public string BKF184 { get; set; }

        /// <summary>
        /// Tên đơn vị công tác
        /// </summary>
        /// <returns></returns>
        public string AAB004 { get; set; }

        /// <summary>
        /// Địa chỉ đơn vị công tác_Tỉnh(Khu tự trị,Thành phố trực thuộc trung ương)
        /// </summary>
        /// <returns></returns>
        public string BKF163 { get; set; }

        /// <summary>
        /// Địa chỉ đơn vị công tác_Thành phố(Khu vực,Châu)
        /// </summary>
        /// <returns></returns>
        public string BKF164 { get; set; }

        /// <summary>
        /// Địa chỉ đơn vị công tác_Huyện(Quận)
        /// </summary>
        /// <returns></returns>
        public string BKF165 { get; set; }

        /// <summary>
        /// Địa chỉ đơn vị công tác_Xã(Thị trấn,Văn phòng phường)
        /// </summary>
        /// <returns></returns>
        public string BKF166 { get; set; }

        /// <summary>
        /// Địa chỉ đơn vị công tác_thôn(phố,đường,ngõ,...)
        /// </summary>
        /// <returns></returns>
        public string BKF161 { get; set; }

        /// <summary>
        /// Địa chỉ đơn vị công tác_Số nhà
        /// </summary>
        /// <returns></returns>
        public string BKF162 { get; set; }

        /// <summary>
        /// Địa chỉ đơn vị công tác_Mã bưu chính
        /// </summary>
        /// <returns></returns>
        public string BKF652 { get; set; }

        /// <summary>
        /// Số điện thoại đơn vị công tác
        /// </summary>
        /// <returns></returns>
        public string BKF651 { get; set; }

        /// <summary>
        /// Tên người liên hệ
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("Tên người liên hệ")]
        public string AAE004 { get; set; }

        /// <summary>
        /// Mã quan hệ giữa người liên hệ và bệnh nhân
        /// </summary>
        /// <returns></returns>
        public string BKF654 { get; set; }

        /// <summary>
        /// Địa chỉ người liên hệ_Tỉnh(Khu tự trị,Thành phố trực thuộc trung ương)
        /// </summary>
        /// <returns></returns>
        public string BKF219 { get; set; }

        /// <summary>
        /// Địa chỉ người liên hệ_Thành phố(Khu vực,Châu)
        /// </summary>
        /// <returns></returns>
        public string BKF220 { get; set; }

        /// <summary>
        /// Địa chỉ người liên hệ_Huyện(Quận)
        /// </summary>
        /// <returns></returns>
        public string BKF221 { get; set; }

        /// <summary>
        /// Địa chỉ người liên hệ_Xã(Thị trấn,Văn phòng phường)
        /// </summary>
        /// <returns></returns>
        public string BKF222 { get; set; }

        /// <summary>
        /// Địa chỉ người liên hệ_thôn(phố,đường,ngõ,...)
        /// </summary>
        /// <returns></returns>
        public string BKF217 { get; set; }

        /// <summary>
        /// Địa chỉ người liên hệ_Số nhà
        /// </summary>
        /// <returns></returns>
        public string BKF218 { get; set; }

        /// <summary>
        /// Số điện thoại người liên hệ
        /// </summary>
        /// <returns></returns>
        public string AAE005 { get; set; }

        /// <summary>
        /// Đường nhập viện
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("Đường nhập viện")]
        public string BKF818 { get; set; }

        /// <summary>
        /// Ngày giờ nhập viện
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("Ngày giờ nhập viện")]
        public string BKC192 { get; set; }

        /// <summary>
        /// Mã khoa nhập viện
        /// </summary>
        /// <returns></returns>
        public string BKF659 { get; set; }

        /// <summary>
        /// Tên khoa nhập viện
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("Tên khoa nhập viện")]
        public string BKF660 { get; set; }

        /// <summary>
        /// Phòng bệnh nhập viện
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("Phòng bệnh nhập viện")]
        public string BKF261 { get; set; }

        /// <summary>
        /// Mã khoa chuyển
        /// </summary>
        /// <returns></returns>
        public string BKF551 { get; set; }

        /// <summary>
        /// Tên khoa chuyển
        /// </summary>
        /// <returns></returns>
        public string BKF552 { get; set; }

        /// <summary>
        /// Ngày giờ ra viện
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("Ngày giờ ra viện")]
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
        [Description("Tên khoa ra viện")]
        public string BKF670 { get; set; }

        /// <summary>
        /// Phòng bệnh ra viện
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("Phòng bệnh ra viện")]
        public string BKF853 { get; set; }

        /// <summary>
        /// Số ngày nằm viện thực tế
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("Số ngày nằm viện thực tế")]
        public string BKF668 { get; set; }

        /// <summary>
        /// Ngoại trú(cấp cứu)Chẩn đoán(Chẩn đoán Đông y)Mã tên bệnh
        /// </summary>
        /// <returns></returns>
        public string BKF855 { get; set; }

        /// <summary>
        /// Ngoại trú(cấp cứu)Chẩn đoán(Chẩn đoán Đông y)Tên
        /// </summary>
        /// <returns></returns>
        public string BKF854 { get; set; }

        /// <summary>
        /// Ngoại trú(cấp cứu)Chẩn đoán(Chứng hậu Đông y)Mã chứng hậu
        /// </summary>
        /// <returns></returns>
        public string BKF241 { get; set; }

        /// <summary>
        /// Ngoại trú(cấp cứu)Chẩn đoán(Chứng hậu Đông y)Tên
        /// </summary>
        /// <returns></returns>
        public string BKF240 { get; set; }

        /// <summary>
        /// Ngoại trú(cấp cứu)Chẩn đoán(Chẩn đoán Tây y)Mã bệnh
        /// </summary>
        /// <returns></returns>
        public string BKF857 { get; set; }

        /// <summary>
        /// Ngoại trú(cấp cứu)Chẩn đoán(Chẩn đoán Tây y)Tên
        /// </summary>
        /// <returns></returns>
        public string BKF856 { get; set; }

        /// <summary>
        /// Mã dấu hiệu thực hiện lộ trình lâm sàng
        /// </summary>
        /// <returns></returns>
        public string BKF858 { get; set; }

        /// <summary>
        /// Dấu hiệu sử dụng chế phẩm thuốc Đông y của cơ sở y tế
        /// </summary>
        /// <returns></returns>
        public string BKF859 { get; set; }

        /// <summary>
        /// Dấu hiệu sử dụng thiết bị chẩn đoán và điều trị Đông y
        /// </summary>
        /// <returns></returns>
        public string BKF860 { get; set; }

        /// <summary>
        /// Dấu hiệu sử dụng kỹ thuật Đông y
        /// </summary>
        /// <returns></returns>
        public string BKF861 { get; set; }

        /// <summary>
        /// Dấu hiệu chăm sóc theo biện chứng
        /// </summary>
        /// <returns></returns>
        public string BKF862 { get; set; }

        /// <summary>
        /// Mã nguyên nhân bên ngoài gây tổn thương/ngộ độc
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
        /// Dấu hiệu dị ứng thuốc 1 Có;2 Không
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("Dấu hiệu dị ứng thuốc")]
        public string BKF677 { get; set; }

        /// <summary>
        /// Thuốc dị ứng 
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("Thuốc dị ứng")]
        public string AKE001 { get; set; }

        /// <summary>
        /// Dấu hiệu khám nghiệm tử thi bệnh nhân tử vong1 Có;2 Không;
        /// </summary>
        /// <returns></returns>
        public string BKF324 { get; set; }

        /// <summary>
        /// Mã nhóm máu ABO
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("Mã nhóm máu ABO")]
        public string BKF867 { get; set; }

        /// <summary>
        /// Mã nhóm máu Rh(D)
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("Mã nhóm máu Rh(D)")]
        public string BKF868 { get; set; }

        /// <summary>
        /// Tên trưởng khoa
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("Tên trưởng khoa")]
        public string BKF684 { get; set; }

        /// <summary>
        /// Chủ nhiệm(Phó chủ nhiệm) Tên bác sĩ
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("Chủ nhiệm(Phó chủ nhiệm) Tên bác sĩ")]
        public string BKF696 { get; set; }

        /// <summary>
        /// Mã bác sĩ điều trị chính
        /// </summary>
        /// <returns></returns>
        public string BKF050 { get; set; }

        /// <summary>
        /// Tên bác sĩ điều trị chính
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("Tên bác sĩ điều trị chính")]
        public string AKC273 { get; set; }

        /// <summary>
        /// Tên bác sĩ nội trú
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("Tên bác sĩ nội trú")]
        public string BKF692 { get; set; }

        /// <summary>
        /// Tên y tá chịu trách nhiệm
        /// </summary>
        /// <returns></returns>
        public string BKF412 { get; set; }

        /// <summary>
        /// Tên bác sĩ tu nghiệp
        /// </summary>
        /// <returns></returns>
        public string BKF211 { get; set; }

        /// <summary>
        /// Tên bác sĩ thực tập
        /// </summary>
        /// <returns></returns>
        public string BKF267 { get; set; }

        /// <summary>
        /// Tên nhân viên mã hóa bệnh án
        /// </summary>
        /// <returns></returns>
        public string BKF105 { get; set; }

        /// <summary>
        /// Chất lượng hồ sơ bệnh án
        /// </summary>
        /// <returns></returns>
        public string BKF106 { get; set; }

        /// <summary>
        /// Tên bác sĩ kiểm soát chất lượng
        /// </summary>
        /// <returns></returns>
        public string BKF431 { get; set; }

        /// <summary>
        /// Tên y tá kiểm soát chất lượng
        /// </summary>
        /// <returns></returns>
        public string BKF429 { get; set; }

        /// <summary>
        /// Ngày kiểm soát chất lượng
        /// </summary>
        /// <returns></returns>
        public string BKF430 { get; set; }

        /// <summary>
        /// Cách rời viện
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("Cách rời viện")]
        public string BKF705 { get; set; }

        /// <summary>
        /// Tên cơ sở y tế dự kiến tiếp nhận
        /// </summary>
        /// <returns></returns>
        public string BKF706 { get; set; }

        /// <summary>
        /// Ra viện31 Dấu hiệu tái nhập viện trong 31 ngày
        /// </summary>
        /// <returns></returns>
        public string BKF137 { get; set; }

        /// <summary>
        /// Ra viện31 Mục đích tái nhập viện trong 31 ngày
        /// </summary>
        /// <returns></returns>
        public string BKF138 { get; set; }

        /// <summary>
        /// Thời gian hôn mê trước nhập viện của bệnh nhân chấn thương sọ não-d
        /// </summary>
        /// <returns></returns>
        public string BKF226 { get; set; }

        /// <summary>
        /// Thời gian hôn mê trước nhập viện của bệnh nhân chấn thương sọ não-h
        /// </summary>
        /// <returns></returns>
        public string BKF227 { get; set; }

        /// <summary>
        /// Thời gian hôn mê trước nhập viện của bệnh nhân chấn thương sọ não-min
        /// </summary>
        /// <returns></returns>
        public string BKF228 { get; set; }

        /// <summary>
        /// Thời gian hôn mê sau nhập viện của bệnh nhân chấn thương sọ não-d
        /// </summary>
        /// <returns></returns>
        public string BKF223 { get; set; }

        /// <summary>
        /// Thời gian hôn mê sau nhập viện của bệnh nhân chấn thương sọ não-h
        /// </summary>
        /// <returns></returns>
        public string BKF224 { get; set; }

        /// <summary>
        /// Thời gian hôn mê sau nhập viện của bệnh nhân chấn thương sọ não-min
        /// </summary>
        /// <returns></returns>
        public string BKF225 { get; set; }

        /// <summary>
        /// Tình hình ra viện(Diễn biến bệnh)
        /// </summary>
        /// <returns></returns>
        public string BKF758 { get; set; }

        /// <summary>
        /// Tình trạng phù hợp chẩn đoán-Ngoại trú và nội trú
        /// </summary>
        /// <returns></returns>
        public string BKF880 { get; set; }

        /// <summary>
        /// Tình trạng phù hợp chẩn đoán-Nhập viện và ra viện
        /// </summary>
        /// <returns></returns>
        public string BKF882 { get; set; }

        /// <summary>
        /// Tình trạng phù hợp chẩn đoán-Trước và sau phẫu thuật
        /// </summary>
        /// <returns></returns>
        public string BKF877 { get; set; }

        /// <summary>
        /// Tình trạng phù hợp chẩn đoán-Lâm sàng và bệnh lý
        /// </summary>
        /// <returns></returns>
        public string BKF878 { get; set; }

        /// <summary>
        /// Tình trạng phù hợp chẩn đoán-Xạ hình và bệnh lý
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
        /// Tổng chi phí nội trú( đồng)
        /// </summary>
        /// <returns></returns>
        public decimal? BKF478 { get; set; }

        /// <summary>
        /// Tổng chi phí nội trú_Số tiền tự trả( đồng)
        /// </summary>
        /// <returns></returns>
        public decimal? BKF479 { get; set; }

        /// <summary>
        /// Phí dịch vụ y tế tổng hợp_Phí dịch vụ y tế chung
        /// </summary>
        /// <returns></returns>
        public decimal? BKF563 { get; set; }

        /// <summary>
        /// Phí dịch vụ y tế tổng hợp_Phí thao tác điều trị chung
        /// </summary>
        /// <returns></returns>
        public decimal? BKF564 { get; set; }

        /// <summary>
        /// Phí dịch vụ y tế chung_Phí biện chứng luận trị Đông y
        /// </summary>
        /// <returns></returns>
        public decimal? BKF386 { get; set; }

        /// <summary>
        /// Phí dịch vụ y tế chung_Phí hội chẩn biện chứng luận trị Đông y
        /// </summary>
        /// <returns></returns>
        public decimal? BKF387 { get; set; }

        /// <summary>
        /// Phí dịch vụ y tế tổng hợp_Phí điều dưỡng
        /// </summary>
        /// <returns></returns>
        public decimal? BKF561 { get; set; }

        /// <summary>
        /// Phí dịch vụ y tế tổng hợp_Chi phí khác
        /// </summary>
        /// <returns></returns>
        public decimal? BKF562 { get; set; }

        /// <summary>
        /// Chẩn đoán_Phí chẩn đoán bệnh lý
        /// </summary>
        /// <returns></returns>
        public decimal? BKF413 { get; set; }

        /// <summary>
        /// Chẩn đoán_Phí chẩn đoán xét nghiệm
        /// </summary>
        /// <returns></returns>
        public decimal? BKF415 { get; set; }

        /// <summary>
        /// Chẩn đoán_Phí chẩn đoán hình ảnh
        /// </summary>
        /// <returns></returns>
        public decimal? BKF416 { get; set; }

        /// <summary>
        /// Chẩn đoán_Phí hạng mục chẩn đoán lâm sàng
        /// </summary>
        /// <returns></returns>
        public decimal? BKF414 { get; set; }

        /// <summary>
        /// Điều trị_Phí hạng mục điều trị không phẫu thuật
        /// </summary>
        /// <returns></returns>
        public decimal? BKF432 { get; set; }

        /// <summary>
        /// Điều trị_Phí hạng mục điều trị không phẫu thuật_Phí vật lý trị liệu lâm sàng
        /// </summary>
        /// <returns></returns>
        public decimal? BKF433 { get; set; }

        /// <summary>
        /// Điều trị_Phí điều trị phẫu thuật
        /// </summary>
        /// <returns></returns>
        public decimal? BKF434 { get; set; }

        /// <summary>
        /// Điều trị_Phí điều trị phẫu thuật_Phí gây mê
        /// </summary>
        /// <returns></returns>
        public decimal? BKF435 { get; set; }

        /// <summary>
        /// Điều trị_Phí điều trị phẫu thuật_Phí phẫu thuật
        /// </summary>
        /// <returns></returns>
        public decimal? BKF436 { get; set; }

        /// <summary>
        /// Loại phục hồi chức năng-Phí phục hồi chức năng
        /// </summary>
        /// <returns></returns>
        public decimal? AAE019 { get; set; }

        /// <summary>
        /// Loại đông y_Phí chẩn đoán Đông y
        /// </summary>
        /// <returns></returns>
        public decimal? BKF465 { get; set; }

        /// <summary>
        /// Loại đông y_Chi phí điều trị đông y
        /// </summary>
        /// <returns></returns>
        public decimal? BKF461 { get; set; }

        /// <summary>
        /// Loại đông y_Chi phí điều trị đông y_Phí điều trị ngoài Đông y
        /// </summary>
        /// <returns></returns>
        public decimal? BKF472 { get; set; }

        /// <summary>
        /// Loại đông y_Chi phí điều trị đông y_Phí chấn thương xương Đông y
        /// </summary>
        /// <returns></returns>
        public decimal? BKF469 { get; set; }

        /// <summary>
        /// Loại đông y_Chi phí điều trị đông y_Phí châm cứu và cứu ngải
        /// </summary>
        /// <returns></returns>
        public decimal? BKF467 { get; set; }

        /// <summary>
        /// Loại đông y_Chi phí điều trị đông y_Phí điều trị xoa bóp Đông y
        /// </summary>
        /// <returns></returns>
        public decimal? BKF471 { get; set; }

        /// <summary>
        /// Loại đông y_Chi phí điều trị đông y_Phí điều trị hậu môn trực tràng Đông y
        /// </summary>
        /// <returns></returns>
        public decimal? BKF468 { get; set; }

        /// <summary>
        /// Loại đông y_Chi phí điều trị đông y_Phí điều trị đặc biệt Đông y
        /// </summary>
        /// <returns></returns>
        public decimal? BKF470 { get; set; }

        /// <summary>
        /// Loại đông y_Phí Đông y khác
        /// </summary>
        /// <returns></returns>
        public decimal? BKF462 { get; set; }

        /// <summary>
        /// Loại đông y_Phí Đông y khác_Phí pha chế gia công đặc biệt Đông y
        /// </summary>
        /// <returns></returns>
        public decimal? BKF464 { get; set; }

        /// <summary>
        /// Loại đông y_Phí Đông y khác_Phí ăn uống theo biện chứng
        /// </summary>
        /// <returns></returns>
        public decimal? BKF463 { get; set; }

        /// <summary>
        /// Loại thuốc tây-Chi phí thuốc tây
        /// </summary>
        /// <returns></returns>
        public decimal? BKF356 { get; set; }

        /// <summary>
        /// Loại thuốc tây-Chi phí thuốc tây_Chi phí kháng sinh
        /// </summary>
        /// <returns></returns>
        public decimal? BKF357 { get; set; }

        /// <summary>
        /// Loại thuốc đông y_Chi phí thuốc thành phẩm đông y
        /// </summary>
        /// <returns></returns>
        public decimal? BKF453 { get; set; }

        /// <summary>
        /// Chi phí thuốc thành phẩm đông y-Phí chế phẩm thuốc Đông y của cơ sở y tế
        /// </summary>
        /// <returns></returns>
        public decimal? BKF450 { get; set; }

        /// <summary>
        /// Loại thuốc đông y_Chi phí thuốc đông y
        /// </summary>
        /// <returns></returns>
        public decimal? BKF452 { get; set; }

        /// <summary>
        /// Loại máu và chế phẩm máu-Chi phí máu
        /// </summary>
        /// <returns></returns>
        public decimal? BKF376 { get; set; }

        /// <summary>
        /// Loại máu và chế phẩm máu-Chi phí chế phẩm albumin
        /// </summary>
        /// <returns></returns>
        public decimal? BKF372 { get; set; }

        /// <summary>
        /// Loại máu và chế phẩm máu-Chi phí chế phẩm globulin
        /// </summary>
        /// <returns></returns>
        public decimal? BKF374 { get; set; }

        /// <summary>
        /// Loại máu và chế phẩm máu-Chi phí chế phẩm yếu tố đông máu
        /// </summary>
        /// <returns></returns>
        public decimal? BKF373 { get; set; }

        /// <summary>
        /// Loại máu và chế phẩm máu-Chi phí chế phẩm cytokine
        /// </summary>
        /// <returns></returns>
        public decimal? BKF375 { get; set; }

        /// <summary>
        /// Loại vật tư tiêu hao-Chi phí vật tư y tế dùng một lần cho kiểm tra
        /// </summary>
        /// <returns></returns>
        public decimal? BKF171 { get; set; }

        /// <summary>
        /// Loại vật tư tiêu hao-Chi phí vật tư y tế dùng một lần cho điều trị
        /// </summary>
        /// <returns></returns>
        public decimal? BKF173 { get; set; }

        /// <summary>
        /// Loại vật tư tiêu hao-Chi phí vật tư y tế dùng một lần cho phẫu thuật
        /// </summary>
        /// <returns></returns>
        public decimal? BKF172 { get; set; }

        /// <summary>
        /// Loại khác-Phí khác
        /// </summary>
        /// <returns></returns>
        public decimal? BKF250 { get; set; }

        /// <summary>
        /// Cân nặng(kg)
        /// </summary>
        /// <returns></returns>
        public string BKF353 { get; set; }
    }
}
