$.yibao = {
    $yibaoAX: $('#yibaoAX')[0],
    //đọc lấy thẻ thông tin
    GetCardInfo: function () {
        return eval('(' + this.$yibaoAX.GetCardInfo() + ')');
    },
    /// <summary>
    /// Ngoại trú đăng ký khám
    /// </summary>
    /// <param name="DAC001">xã bảo Số hiệu</param>
    /// <param name="AKE556">Chi phí Tính chất</param>
    /// <param name="AKE186">Khám bệnh Loại</param>
    /// <param name="AKE162">Khám bệnh nguyên nhân</param>
    /// <param name="DAE147">Mã xác minh(có thể là trống)</param>
    /// <param name="AKE276">đăng ký khám/Nhập viện Thao tác viên Mã</param>
    /// <param name="AKE277">đăng ký khám/Nhập viện Thao tác viên</param>
    /// <param name="AKE189">Khám bệnh thứ tự số/Số nhập viện</param>
    /// <param name="CUSTNAME">Họ tên người mua thuốc (nhân viên tự chi trả bắt buộc)</param>
    RegClinic: function (DAC001, AKE556, AKE186, AKE162, DAE147, AKE276, AKE277, AKE189, CUSTNAME) {
        return eval('(' + this.$yibaoAX.RegClinic(DAC001, AKE556, AKE186, AKE162, DAE147, AKE276, AKE277, AKE189, CUSTNAME) + ')');
    },
    /// <summary>
    /// đăng ký khám Hủy
    /// </summary>
    /// <param name="DKE005"></param>
    /// <param name="DAC001"></param>
    /// <param name="AKE556"></param>
    DenyReg: function (DKE005, DAC001, AKE556) {
        return eval('(' + this.$yibaoAX.DenyReg(DKE005, DAC001, AKE556) + ')');
    },
    /// <summary>
    /// chi tiết ghi sổ
    /// </summary>
    /// <param name="DKE005"></param>
    /// <param name="Detail"></param>
    PreAccount: function (DKE005, Detail) {
        return eval('(' + this.$yibaoAX.PreAccount(DKE005, Detail) + ')');
    },
    /// <summary>
    /// xóa ghi sổ
    /// </summary>
    /// <param name="DKE005"></param>
    ClearDetail: function (DKE005) {
        return eval('(' + this.$yibaoAX.ClearDetail(DKE005) + ')');
    },
    /// <summary>
    /// Ngoại trú dự quyết toán
    /// </summary>
    /// <param name="DKE005"></param>
    /// <param name="DAC001"></param>
    /// <param name="ZFY"></param>
    /// <param name="AKE162"></param>
    /// <param name="AKE181"></param>
    /// <param name="AKC195"></param>
    /// <param name="BKC036"></param>
    /// <param name="AKE194"></param>
    /// <param name="AKA120"></param>
    /// <param name="AKA121"></param>
    /// <param name="AKE279"></param>
    /// <param name="AKE278"></param>
    /// <param name="BKC038"></param>
    /// <param name="BKC039"></param>
    PreSettleclinic: function (DKE005, DAC001, ZFY, AKE162, AKE181, AKC195, BKC036,
            AKE194, AKA120, AKA121, AKE279, AKE278, BKC038, BKC039) {
        return eval('(' + this.$yibaoAX.PreSettleclinic(DKE005, DAC001, ZFY, AKE162, AKE181, AKC195, BKC036,
            AKE194, AKA120, AKA121, AKE279, AKE278, BKC038, BKC039) + ')');
    },
    /// <summary>
    /// Ngoại trú quyết toán xác nhận
    /// </summary>
    /// <param name="DKE005">quyết toán số</param>
    SettleClinic: function (DKE005) {
        return eval('(' + this.$yibaoAX.SettleClinic(DKE005) + ')');
    },
    /// <summary>
    /// Hoàn phí quyết toán xác nhận
    /// </summary>
    /// <param name="DKE005">quyết toán số</param>
    /// <param name="DAC001">xã bảo Số hiệu</param>
    /// <param name="AKE279">quyết toán qua làm người Mã</param>
    /// <param name="AKE278">quyết toán qua làm người</param>
    DenySettle: function (DKE005, DAC001, AKE279, AKE278) {
        return eval('(' + this.$yibaoAX.DenySettle(DKE005, DAC001, AKE279, AKE278) + ')');
    },
    /// <summary>
    /// Tra cứu chưa quyết toán thông tin
    /// </summary>
    /// <param name="DAC001">xã bảo Số hiệu</param>
    QUnSettleFee: function (DAC001) {
        return eval('(' + this.$yibaoAX.QUnSettleFee(DAC001) + ')');
    },
    /// <summary>
    /// Tra cứu đã quyết toán thông tin
    /// </summary>
    /// <param name="DAC001">xã bảo Số hiệu</param>
    QSettleFee: function (DAC001) {
        return eval('(' + this.$yibaoAX.QSettleFee(DAC001) + ')');
    }
};

$.shiqian = {
    $shiqianAX: $('#yibaoAX')[0],
    //Ngoại trú nhận khám lịch sử thông tin
    MzjzInfo: function (Head, JzInfo) {
        this.$shiqianAX.MzjzInfo(Head, JzInfo);
    },
    //Ngoại trú nhập Chẩn đoán
    MzlrZDInfo: function (Head, ZD, ZDList) {
        this.$shiqianAX.MzlrZDInfo(Head, ZD, ZDList);
    },
    //Đơn thuốc ngoại trú nhập
    MzcfxxlrInfo: function (Head, CFInfo, YP, XM) {
        this.$shiqianAX.MzcfxxlrInfo(Head, CFInfo, YP, XM);
    },
    //Đơn thuốc ngoại trú Lưu
    MzcfxxSaveInfo: function (Head, CFInfo, YP, XM) {
        this.$shiqianAX.MzcfxxSaveInfo(Head, CFInfo, YP, XM);
    }
};