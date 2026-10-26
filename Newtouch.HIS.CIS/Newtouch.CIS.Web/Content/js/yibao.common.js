$.yibao = {
    $yibaoAX: $('#yibaoAX')[0],
    //Đọc thông tin thẻ
    GetCardInfo: function () {
        return eval('(' + this.$yibaoAX.GetCardInfo() + ')');
    },
    /// <summary>
    /// Đăng ký khám ngoại trú
    /// </summary>
    /// <param name="DAC001">Số bảo hiểm xã hội</param>
    /// <param name="AKE556">Tính chất chi phí</param>
    /// <param name="AKE186">Loại khám</param>
    /// <param name="AKE162">Lý do khám</param>
    /// <param name="DAE147">Mã xác minh (có thể trống)</param>
    /// <param name="AKE276">Đăng ký khám/Mã nhân viên nhập viện</param>
    /// <param name="AKE277">Đăng ký khám/Nhân viên nhập viện</param>
    /// <param name="AKE189">Số thứ tự khám/Số nhập viện</param>
    /// <param name="CUSTNAME">Tên người mua thuốc(Người tự trả là bắt buộc)</param>
    RegClinic: function (DAC001, AKE556, AKE186, AKE162, DAE147, AKE276, AKE277, AKE189, CUSTNAME) {
        return eval('(' + this.$yibaoAX.RegClinic(DAC001, AKE556, AKE186, AKE162, DAE147, AKE276, AKE277, AKE189, CUSTNAME) + ')');
    },
    /// <summary>
    /// Hủy đăng ký khám
    /// </summary>
    /// <param name="DKE005"></param>
    /// <param name="DAC001"></param>
    /// <param name="AKE556"></param>
    DenyReg: function (DKE005, DAC001, AKE556) {
        return eval('(' + this.$yibaoAX.DenyReg(DKE005, DAC001, AKE556) + ')');
    },
    /// <summary>
    /// Ghi sổ chi tiết
    /// </summary>
    /// <param name="DKE005"></param>
    /// <param name="Detail"></param>
    PreAccount: function (DKE005, Detail) {
        return eval('(' + this.$yibaoAX.PreAccount(DKE005, Detail) + ')');
    },
    /// <summary>
    /// Xóa ghi sổ
    /// </summary>
    /// <param name="DKE005"></param>
    ClearDetail: function (DKE005) {
        return eval('(' + this.$yibaoAX.ClearDetail(DKE005) + ')');
    },
    /// <summary>
    /// Thanh toán trước ngoại trú
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
    /// Xác nhận thanh toán ngoại trú
    /// </summary>
    /// <param name="DKE005">Số thanh toán</param>
    SettleClinic: function (DKE005) {
        return eval('(' + this.$yibaoAX.SettleClinic(DKE005) + ')');
    },
    /// <summary>
    /// Xác nhận thanh toán hoàn phí
    /// </summary>
    /// <param name="DKE005">Số thanh toán</param>
    /// <param name="DAC001">Số bảo hiểm xã hội</param>
    /// <param name="AKE279">Mã người xử lý thanh toán</param>
    /// <param name="AKE278">Người xử lý thanh toán</param>
    DenySettle: function (DKE005, DAC001, AKE279, AKE278) {
        return eval('(' + this.$yibaoAX.DenySettle(DKE005, DAC001, AKE279, AKE278) + ')');
    },
    /// <summary>
    /// Tra cứu thông tin chưa thanh toán
    /// </summary>
    /// <param name="DAC001">Số bảo hiểm xã hội</param>
    QUnSettleFee: function (DAC001) {
        return eval('(' + this.$yibaoAX.QUnSettleFee(DAC001) + ')');
    },
    /// <summary>
    /// Tra cứu thông tin đã thanh toán
    /// </summary>
    /// <param name="DAC001">Số bảo hiểm xã hội</param>
    QSettleFee: function (DAC001) {
        return eval('(' + this.$yibaoAX.QSettleFee(DAC001) + ')');
    }
};

$.shiqian = {
    $shiqianAX: $('#yibaoAX')[0],
    //Thông tin lịch sử tiếp nhận ngoại trú
    MzjzInfo: function (Head, JzInfo) {
        this.$shiqianAX.MzjzInfo(Head, JzInfo);
    },
    //Nhập chẩn đoán ngoại trú
    MzlrZDInfo: function (Head, ZD, ZDList) {
        this.$shiqianAX.MzlrZDInfo(Head, ZD, ZDList);
    },
    //Nhập đơn thuốc ngoại trú
    MzcfxxlrInfo: function (Head, CFInfo, YP, XM) {
        this.$shiqianAX.MzcfxxlrInfo(Head, CFInfo, YP, XM);
    },
    //Lưu đơn thuốc ngoại trú
    MzcfxxSaveInfo: function (Head, CFInfo, YP, XM) {
        this.$shiqianAX.MzcfxxSaveInfo(Head, CFInfo, YP, XM);
    }
};