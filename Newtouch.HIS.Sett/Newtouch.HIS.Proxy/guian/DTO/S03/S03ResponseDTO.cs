namespace Newtouch.HIS.Proxy.guian.DTO.S03
{
    /// <summary>
    /// theo truyền vàocủa số chứng nhận y tế lấy gia đ ình của tham gia hợp tác thành viên danh sách bản tin trả về
    /// </summary>
    public class member
    {
        /// <summary>
        /// đ ịa Quận mã 
        /// </summary>
        public string areaCode { get; set; }

        /// <summary>
        /// Năm
        /// </summary>
        public string year { get; set; }

        /// <summary>
        /// mã cá nhân 
        /// </summary>
        public string memberId { get; set; }

        /// <summary>
        /// Tên bệnh nhân
        /// </summary>
        public string memberName { get; set; }

        /// <summary>
        /// Bệnh nhân giới tính 1nam 2nữ
        /// </summary>
        public string memberSex { get; set; }

        /// <summary>
        /// Bệnh nhânSố CMND
        /// </summary>
        public string idCard { get; set; }

        /// <summary>
        /// Số hộ gia đ ình
        /// </summary>
        public string familyId { get; set; }

        /// <summary>
        /// Số thẻ chứng nhận y tế
        /// </summary>
        public string medicalNo { get; set; }

        /// <summary>
        /// Tuổi
        /// </summary>
        public string memberAge { get; set; }

        /// <summary>
        /// Ngày sinh
        /// </summary>
        public string birthday { get; set; }

        /// <summary>
        /// Đ ịa chỉ
        /// </summary>
        public string areaName { get; set; }

        /// <summary>
        /// Vàchủ hộ quan hệ 
        /// </summary>
        public string relation { get; set; }

        /// <summary>
        /// Thuộc tính nhân sự
        /// </summary>
        public string memberPro { get; set; }

        /// <summary>
        /// Số dư tài khoản
        /// </summary>
        public decimal account { get; set; }

        /// <summary>
        /// tham gia bảo hiểmtrạng thái,có hay khôngdừng bảo hiểm(0 bình thư ờng,1dừng bảo hiểm)
        /// </summary>
        public string memberStauts { get; set; }

        /// <summary>
        /// Thông thư ờng nhập viện tổng chi phí
        /// </summary>
        public decimal hosTotalCost { get; set; }

        /// <summary>
        /// Thông thư ờng nhập việnchi phí trong phạm vi bảo hiểm
        /// </summary>
        public decimal hosInsuranceCost { get; set; }

        /// <summary>
        /// Thông thư ờng nhập việnchi phí bồi thư ờng
        /// </summary>
        public decimal hosCompensateCost { get; set; }

        /// <summary>
        /// bệnh đ ơn lẻ tổng chi phí
        /// </summary>
        public decimal sigTotalCost { get; set; }

        /// <summary>
        /// bệnh đ ơn lẻ chi phí trong phạm vi bảo hiểm
        /// </summary>
        public decimal sigInsuranceCost { get; set; }

        /// <summary>
        /// bệnh đ ơn lẻ chi phí bồi thư ờng
        /// </summary>
        public decimal sigCompensateCost { get; set; }

        /// <summary>
        /// Phòng khám thông thư ờng tổng chi phí
        /// </summary>
        public decimal outpTotalCost { get; set; }

        /// <summary>
        /// Phòng khám thông thư ờngchi phí trong phạm vi bảo hiểm
        /// </summary>
        public decimal outpInsuranceCost { get; set; }

        /// <summary>
        /// Phòng khám thông thư ờngchi phí bồi thư ờng
        /// </summary>
        public decimal outpCompensateCost { get; set; }

        /// <summary>
        /// chậm bệnh tình dụctổng chi phí
        /// </summary>
        public decimal chroTotalCost { get; set; }

        /// <summary>
        /// chậm bệnh tình dụcchi phí trong phạm vi bảo hiểm
        /// </summary>
        public decimal chroInsuranceCost { get; set; }

        /// <summary>
        /// chậm bệnh tình dụcchi phí bồi thư ờng
        /// </summary>
        public decimal chroCompensateCost { get; set; }

        /// <summary>
        /// dân chính chính xác thuộc tính 
        /// </summary>
        public string jzmzName { get; set; }

        /// <summary>
        /// kế hoạch hóa gia đình chính xác thuộc tính 
        /// </summary>
        public string jzjsName { get; set; }

        /// <summary>
        /// Điện thoại số 
        /// </summary>
        public string tel { get; set; }

        /// <summary>
        /// cá nhân thân phận thuộc tính tên
        /// </summary>
        public string ideName { get; set; }

        /// <summary>
        /// giảm nghèo chính xácĐ ối tư ợng thuộc tính 
        /// </summary>
        public string PoorObject { get; set; }

        /// <summary>
        /// Ngoại trú lũy kế bồi thư ờng số lần 
        /// </summary>
        public string currYearRedeemCount { get; set; }

        /// <summary>
        /// Ngoại trú lũy kế bồi thư ờngsố tiền 
        /// </summary>
        public string currYearReddemMoney { get; set; }

        /// <summary>
        /// Nhập viện lũy kế bồi thư ờng số lần 
        /// </summary>
        public string inYearRedeemCount { get; set; }

        /// <summary>
        /// Nhập viện lũy kế bồi thư ờngsố tiền 
        /// </summary>
        public string inYearRedeemMoney { get; set; }

        /// <summary>
        /// gia đ ình số dư tài khoản
        /// </summary>
        public string familyAccountBalance { get; set; }


    }
}