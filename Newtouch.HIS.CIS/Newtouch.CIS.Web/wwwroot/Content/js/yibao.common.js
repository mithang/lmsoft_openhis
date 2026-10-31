$.yibao = {
    $yibaoAX: $('#yibaoAX')[0],
    //đọclấythẻthông tin
    GetCardInfo: function () {
        return eval('(' + this.$yibaoAX.GetCardInfo() + ')');
    },
    /// <summary>
    /// Ngoại trúđăng ký khám
    /// </summary>
    /// <param name="DAC001">xãbảoSố hiệu</param>
    /// <param name="AKE556">Chi phíTính chất</param>
    /// <param name="AKE186">Khám bệnhLoại</param>
    /// <param name="AKE162">Khám bệnhnguyên nhân</param>
    /// <param name="DAE147">Mã xác minh（có thểlàtrống）</param>
    /// <param name="AKE276">đăng ký khám/Nhập việnThao tácviênMã</param>
    /// <param name="AKE277">đăng ký khám/Nhập việnThao tácviên</param>
    /// <param name="AKE189">Khám bệnhthứ tựsố/Số nhập viện</param>
    /// <param name="CUSTNAME">Họ tên người mua thuốc (nhân viên tự chi trả bắt buộc)</param>
    RegClinic: function (DAC001, AKE556, AKE186, AKE162, DAE147, AKE276, AKE277, AKE189, CUSTNAME) {
        return eval('(' + this.$yibaoAX.RegClinic(DAC001, AKE556, AKE186, AKE162, DAE147, AKE276, AKE277, AKE189, CUSTNAME) + ')');
    },
    /// <summary>
    /// đăng ký khámHủy
    /// </summary>
    /// <param name="DKE005"></param>
    /// <param name="DAC001"></param>
    /// <param name="AKE556"></param>
    DenyReg: function (DKE005, DAC001, AKE556) {
        return eval('(' + this.$yibaoAX.DenyReg(DKE005, DAC001, AKE556) + ')');
    },
    /// <summary>
    /// chi tiếtghisổ
    /// </summary>
    /// <param name="DKE005"></param>
    /// <param name="Detail"></param>
    PreAccount: function (DKE005, Detail) {
        return eval('(' + this.$yibaoAX.PreAccount(DKE005, Detail) + ')');
    },
    /// <summary>
    /// xóaghisổ
    /// </summary>
    /// <param name="DKE005"></param>
    ClearDetail: function (DKE005) {
        return eval('(' + this.$yibaoAX.ClearDetail(DKE005) + ')');
    },
    /// <summary>
    /// Ngoại trúdựquyết toán
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
    /// Ngoại trúquyết toánxác nhận
    /// </summary>
    /// <param name="DKE005">quyết toánsố</param>
    SettleClinic: function (DKE005) {
        return eval('(' + this.$yibaoAX.SettleClinic(DKE005) + ')');
    },
    /// <summary>
    /// Hoàn phíquyết toánxác nhận
    /// </summary>
    /// <param name="DKE005">quyết toánsố</param>
    /// <param name="DAC001">xãbảoSố hiệu</param>
    /// <param name="AKE279">quyết toánqualàm ngườiMã</param>
    /// <param name="AKE278">quyết toánqualàm người</param>
    DenySettle: function (DKE005, DAC001, AKE279, AKE278) {
        return eval('(' + this.$yibaoAX.DenySettle(DKE005, DAC001, AKE279, AKE278) + ')');
    },
    /// <summary>
    /// Tra cứuchưaquyết toánthông tin
    /// </summary>
    /// <param name="DAC001">xãbảoSố hiệu</param>
    QUnSettleFee: function (DAC001) {
        return eval('(' + this.$yibaoAX.QUnSettleFee(DAC001) + ')');
    },
    /// <summary>
    /// Tra cứuđãquyết toánthông tin
    /// </summary>
    /// <param name="DAC001">xãbảoSố hiệu</param>
    QSettleFee: function (DAC001) {
        return eval('(' + this.$yibaoAX.QSettleFee(DAC001) + ')');
    }
};

$.shiqian = {
    $shiqianAX: $('#yibaoAX')[0],
    //Ngoại trúnhậnkhámlịch sửthông tin
    MzjzInfo: function (Head, JzInfo) {
        this.$shiqianAX.MzjzInfo(Head, JzInfo);
    },
    //Ngoại trúnhậpChẩn đoán
    MzlrZDInfo: function (Head, ZD, ZDList) {
        this.$shiqianAX.MzlrZDInfo(Head, ZD, ZDList);
    },
    //Đơn thuốc ngoại trúnhập
    MzcfxxlrInfo: function (Head, CFInfo, YP, XM) {
        this.$shiqianAX.MzcfxxlrInfo(Head, CFInfo, YP, XM);
    },
    //Đơn thuốc ngoại trúLưu
    MzcfxxSaveInfo: function (Head, CFInfo, YP, XM) {
        this.$shiqianAX.MzcfxxSaveInfo(Head, CFInfo, YP, XM);
    }
};