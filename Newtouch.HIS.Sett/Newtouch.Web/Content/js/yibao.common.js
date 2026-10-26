$.yibao = {
    $yibaoAX: $('')[0],
    //读取卡信息
    GetCardInfo: function (Cardtype) {
        return eval('' + this.$yibaoAX.GetCardInfo(Cardtype) + '');
    },
    /// <summary>
    /// Đăng ký khám ngoại trú
    /// </summary>
    /// <param name="">Số bảo hiểm xã hội</param>
    /// <param name="">Tính chất chi phí</param>
    /// <param name="">Khám bệnh loại</param>
    /// <param name="">Lý do khám</param>
    /// <param name="">验证码（Có thể trống）</param>
    /// <param name="">Đăng ký khám/入院操作员编码</param>
    /// <param name="">Đăng ký khám/入院操作员</param>
    /// <param name="">Số thứ tự khám/Số nhập viện</param>
    /// <param name="">购药人姓名(自费人员必填)</param>
    RegClinic: function (Cardtype, DAC001, AKE556, AKE186, AKE162, DAE147, AKE276, AKE277, AKE189, CUSTNAME) {
        return eval('' + this.$yibaoAX.RegClinic(Cardtype, DAC001, AKE556, AKE186, AKE162, DAE147, AKE276, AKE277, AKE189, CUSTNAME) + '');
    },
    /// <summary>
    /// Đăng ký khám hủy
    /// </summary>
    /// <param name="">Số quyết toán</param>
    /// <param name="">Số bảo hiểm xã hội</param>
    /// <param name="">Tính chất chi phí</param>
    DenyReg: function (DKE005, DAC001, AKE556) {
        return eval('' + this.$yibaoAX.DenyReg(DKE005, DAC001, AKE556) + '');
    },
    /// <summary>
    /// 明细记帐
    /// </summary>
    /// <param name="">Số quyết toán</param>
    /// <param name="">Chi tiết hạng mục ghi sổ</param>
    PreAccount: function (DKE005, Detail) {
        return eval('' + this.$yibaoAX.PreAccount(DKE005, Detail) + '');
    },
    /// <summary>
    /// Xóa ghi sổ
    /// </summary>
    /// <param name="">Số quyết toán</param>
    ClearDetail: function (DKE005) {
        return eval('' + this.$yibaoAX.ClearDetail(DKE005) + '');
    },
    /// <summary>
    /// Quyết toán trước ngoại trú
    /// </summary>
    /// <param name="">Loại thẻ</param>
    /// <param name="">Số quyết toán</param>
    /// <param name="">Số bảo hiểm xã hội</param>
    /// <param name="">Chi phí tổng đầu bệnh viện</param>
    /// <param name="">Khám bệnh mã nguyên nhân</param>
    /// <param name="">Xuất viện thời gian</param>
    /// <param name="">治疗效果</param>
    /// <param name="">Xuất viện số hiệu bác sĩ</param>
    /// <param name="">Xuất viện họ tên bác sĩ</param>
    /// <param name="">Xuất viện mã bệnh chính chẩn đoán ICD10</param>
    /// <param name="">Xuất viện tên bệnh chính chẩn đoán</param>
    /// <param name="">结算经办人编码</param>
    /// <param name="">结算经办人</param>
    /// <param name="">Xuất viện khu bệnh(Khoa)Số hiệu</param>
    /// <param name="">Xuất viện khu bệnh(Khoa)Tên</param>
    PreSettleclinic: function (Cardtype, DKE005, DAC001, ZFY, AKE162, AKE181, AKC195, BKC036,
            AKE194, AKA120, AKA121, AKE279, AKE278, BKC038, BKC039) {
        return eval('' + this.$yibaoAX.PreSettleclinic(Cardtype, DKE005, DAC001, ZFY, AKE162, AKE181, AKC195, BKC036,
            AKE194, AKA120, AKA121, AKE279, AKE278, BKC038, BKC039) + '');
    },
    /// <summary>
    /// Xác nhận quyết toán ngoại trú
    /// </summary>
    /// <param name="">Số quyết toán</param>
    SettleClinic: function (DKE005) {
        return eval('' + this.$yibaoAX.SettleClinic(DKE005) + '');
    },
    /// <summary>
    /// 门诊结算确认完全流程接口
    /// <param name="">Loại thẻ</param>
    /// </summary>
    SettleClinicA: function (Cardtype, DKE005, DAC001, ZFY, AKE162, AKE181, AKC195, BKC036, AKE194, AKA120, AKA121, AKE279, AKE278, BKC038, BKC039) {
        return eval('' + this.$yibaoAX.SettleClinicA(Cardtype, DKE005, DAC001, ZFY, AKE162, AKE181, AKC195, BKC036, AKE194, AKA120, AKA121, AKE279, AKE278, BKC038, BKC039) + '');
    },
    /// <summary>
    /// Hoàn phí xác nhận quyết toán
    /// </summary>
    /// <param name="">Loại thẻ</param>
    /// <param name="">Số quyết toán</param>
    /// <param name="">Số bảo hiểm xã hội</param>
    /// <param name="">结算经办人编码</param>
    /// <param name="">结算经办人</param>
    DenySettle: function (Cardtype, DKE005, DAC001, AKE279, AKE278) {
        return eval('' + this.$yibaoAX.DenySettle(Cardtype, DKE005, DAC001, AKE279, AKE278) + '');
    },
    /// <summary>
    /// Tra cứu thông tin chưa quyết toán
    /// </summary>
    /// <param name="">Số bảo hiểm xã hội</param>
    QUnSettleFee: function (DAC001) {
        return eval('' + this.$yibaoAX.QUnSettleFee(DAC001) + '');
    },
    /// <summary>
    /// Tra cứu thông tin đã quyết toán
    /// </summary>
    /// <param name="">Số bảo hiểm xã hội</param>
    QSettleFee: function (DAC001) {
        return eval('' + this.$yibaoAX.QSettleFee(DAC001) + '');
    },
    /// <summary>
    /// Tra cứu danh mục bảo hiểm y tế
    /// </summary>
    /// <param name="">Loại hạng mục</param>
    /// <param name="">Mã đầu bảo hiểm xã hội</param>
    /// <param name="">Mã đầu bệnh viện</param>
    /// <param name="">Tên hạng mục</param>
    QCatalog: function (DKE085, DKE166, DKE012, AKE002) {
        return eval('' + this.$yibaoAX.QCatalog(DKE085, DKE166, DKE012, AKE002) + '');
    }
};

$.shiqian = {
    $shiqianAX: $('')[0],
    //门诊接诊历史信息
    MzjzInfo: function (Head, JzInfo) {
        this.$shiqianAX.MzjzInfo(Head, JzInfo);
    },
    //Chẩn đoán nhập ngoại trú
    MzlrZDInfo: function (Head, ZD, ZDList) {
        this.$shiqianAX.MzlrZDInfo(Head, ZD, ZDList);
    },
    //门诊处方录入
    MzcfxxlrInfo: function (Head, CFInfo, YP, XM) {
        this.$shiqianAX.MzcfxxlrInfo(Head, CFInfo, YP, XM);
    },
    //门诊处方保存
    MzcfxxSaveInfo: function (Head, CFInfo, YP, XM) {
        this.$shiqianAX.MzcfxxSaveInfo(Head, CFInfo, YP, XM);
    }
};

$.printAX = {
    $printAX: $('')[0],
    /// <summary>
    /// In hóa đơn
    /// </summary>
    /// <param name="">发票主体信息</param>
    /// <param name="">Thông tin chi tiết hóa đơn</param>
    ///InitJson = "";
    ///InvoiceItemJson = "";
    PrintInvoice: function (InitJson, InvoiceItemJson) {
        return eval('' + this.$printAX.PrintInvoice(InitJson, InvoiceItemJson) + '');
    },
    /// <summary>
    /// 已开发票作废
    /// </summary>
    /// <param name="">发票十位代码</param>
    /// <param name="">发票号码</param>
    CancelInvoice: function (InfoTypeCode, InfoNumber) {
        return eval('' + this.$printAX.CancelInvoice(InfoTypeCode, InfoNumber) + '');
    },
    /// <summary>
    /// 关闭金税卡
    /// </summary>
    CloseCard: function () {
        return eval('' + this.$printAX.CloseCard() + '');
    }
};