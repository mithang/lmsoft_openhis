$.yibao = {
    $yibaoAX: $('#yibaoAX')[0],
    //Đọc thông tin thẻ
    GetCardInfo: function (Cardtype) {
        return eval('(' + this.$yibaoAX.GetCardInfo(Cardtype) + ')');
    },
    /// <summary>
    /// Đăng ký khám ngoại trú
    /// </summary>
    /// <param name="DAC001">Số bảo hiểm xã hội</param>
    /// <param name="AKE556">Tính chất chi phí</param>
    /// <param name="AKE186">Loại khám bệnh</param>
    /// <param name="AKE162">Lý do khám bệnh</param>
    /// <param name="DAE147">Mã xác minh（Có thể trống）</param>
    /// <param name="AKE276">Đăng ký khám/Mã người thao tác nhập viện</param>
    /// <param name="AKE277">Đăng ký khám/Người thao tác nhập viện</param>
    /// <param name="AKE189">Số thứ tự khám bệnh/Số nội trú</param>
    /// <param name="CUSTNAME">Tên người mua thuốc(Người tự trả bắt buộc phải nhập)</param>
    RegClinic: function (Cardtype, DAC001, AKE556, AKE186, AKE162, DAE147, AKE276, AKE277, AKE189, CUSTNAME) {
        return eval('(' + this.$yibaoAX.RegClinic(Cardtype, DAC001, AKE556, AKE186, AKE162, DAE147, AKE276, AKE277, AKE189, CUSTNAME) + ')');
    },
    /// <summary>
    /// Hủy đăng ký khám
    /// </summary>
    /// <param name="DKE005">Số quyết toán</param>
    /// <param name="DAC001">Số bảo hiểm xã hội</param>
    /// <param name="AKE556">Tính chất chi phí</param>
    DenyReg: function (DKE005, DAC001, AKE556) {
        return eval('(' + this.$yibaoAX.DenyReg(DKE005, DAC001, AKE556) + ')');
    },
    /// <summary>
    /// Ghi sổ chi tiết
    /// </summary>
    /// <param name="DKE005">Số quyết toán</param>
    /// <param name="Detail">Chi tiết hạng mục ghi sổ</param>
    PreAccount: function (DKE005, Detail) {
        return eval('(' + this.$yibaoAX.PreAccount(DKE005, Detail) + ')');
    },
    /// <summary>
    /// Xóa ghi sổ
    /// </summary>
    /// <param name="DKE005">Số quyết toán</param>
    ClearDetail: function (DKE005) {
        return eval('(' + this.$yibaoAX.ClearDetail(DKE005) + ')');
    },
    /// <summary>
    /// Quyết toán trước khám ngoại trú
    /// </summary>
    /// <param name="Cardtype">Loại thẻ</param>
    /// <param name="DKE005">Số quyết toán</param>
    /// <param name="DAC001">Số bảo hiểm xã hội</param>
    /// <param name="ZFY">Tổng chi phí phía bệnh viện</param>
    /// <param name="AKE162">Mã lý do khám bệnh</param>
    /// <param name="AKE181">Thời gian xuất viện</param>
    /// <param name="AKC195">Hiệu quả điều trị</param>
    /// <param name="BKC036">Mã bác sĩ xuất viện</param>
    /// <param name="AKE194">Tên bác sĩ xuất viện</param>
    /// <param name="AKA120">Mã bệnh chính chẩn đoán xuất viện ICD10</param>
    /// <param name="AKA121">Tên bệnh chính chẩn đoán xuất viện</param>
    /// <param name="AKE279">Mã người thực hiện quyết toán</param>
    /// <param name="AKE278">Người thực hiện quyết toán</param>
    /// <param name="BKC038">Khu bệnh xuất viện(Khoa)Số hiệu</param>
    /// <param name="BKC039">Khu bệnh xuất viện(Khoa)Tên</param>
    PreSettleclinic: function (Cardtype, DKE005, DAC001, ZFY, AKE162, AKE181, AKC195, BKC036,
            AKE194, AKA120, AKA121, AKE279, AKE278, BKC038, BKC039) {
        return eval('(' + this.$yibaoAX.PreSettleclinic(Cardtype, DKE005, DAC001, ZFY, AKE162, AKE181, AKC195, BKC036,
            AKE194, AKA120, AKA121, AKE279, AKE278, BKC038, BKC039) + ')');
    },
    /// <summary>
    /// Xác nhận quyết toán khám ngoại trú
    /// </summary>
    /// <param name="DKE005">Số quyết toán</param>
    SettleClinic: function (DKE005) {
        return eval('(' + this.$yibaoAX.SettleClinic(DKE005) + ')');
    },
    /// <summary>
    /// Giao diện quy trình đầy đủ xác nhận quyết toán khám ngoại trú
    /// <param name="Cardtype">Loại thẻ</param>
    /// </summary>
    SettleClinicA: function (Cardtype, DKE005, DAC001, ZFY, AKE162, AKE181, AKC195, BKC036, AKE194, AKA120, AKA121, AKE279, AKE278, BKC038, BKC039) {
        return eval('(' + this.$yibaoAX.SettleClinicA(Cardtype, DKE005, DAC001, ZFY, AKE162, AKE181, AKC195, BKC036, AKE194, AKA120, AKA121, AKE279, AKE278, BKC038, BKC039) + ')');
    },
    /// <summary>
    /// Xác nhận quyết toán hoàn phí
    /// </summary>
    /// <param name="Cardtype">Loại thẻ</param>
    /// <param name="DKE005">Số quyết toán</param>
    /// <param name="DAC001">Số bảo hiểm xã hội</param>
    /// <param name="AKE279">Mã người thực hiện quyết toán</param>
    /// <param name="AKE278">Người thực hiện quyết toán</param>
    DenySettle: function (Cardtype, DKE005, DAC001, AKE279, AKE278) {
        return eval('(' + this.$yibaoAX.DenySettle(Cardtype, DKE005, DAC001, AKE279, AKE278) + ')');
    },
    /// <summary>
    /// Tra cứu thông tin chưa quyết toán
    /// </summary>
    /// <param name="DAC001">Số bảo hiểm xã hội</param>
    QUnSettleFee: function (DAC001) {
        return eval('(' + this.$yibaoAX.QUnSettleFee(DAC001) + ')');
    },
    /// <summary>
    /// Tra cứu thông tin đã quyết toán
    /// </summary>
    /// <param name="DAC001">Số bảo hiểm xã hội</param>
    QSettleFee: function (DAC001) {
        return eval('(' + this.$yibaoAX.QSettleFee(DAC001) + ')');
    },
    /// <summary>
    /// Tra cứu danh mục BHYT
    /// </summary>
    /// <param name="DKE085">Loại hạng mục</param>
    /// <param name="DKE166">Mã phía bảo hiểm xã hội</param>
    /// <param name="DKE012">Mã phía bệnh viện</param>
    /// <param name="AKE002">Tên hạng mục</param>
    QCatalog: function (DKE085, DKE166, DKE012, AKE002) {
        return eval('(' + this.$yibaoAX.QCatalog(DKE085, DKE166, DKE012, AKE002) + ')');
    }
};

$.shiqian = {
    $shiqianAX: $('#yibaoAX')[0],
    //Thông tin lịch sử tiếp nhận khám ngoại trú
    MzjzInfo: function (Head, JzInfo) {
        this.$shiqianAX.MzjzInfo(Head, JzInfo);
    },
    //Nhập chẩn đoán khám ngoại trú
    MzlrZDInfo: function (Head, ZD, ZDList) {
        this.$shiqianAX.MzlrZDInfo(Head, ZD, ZDList);
    },
    //Nhập đơn thuốc khám ngoại trú
    MzcfxxlrInfo: function (Head, CFInfo, YP, XM) {
        this.$shiqianAX.MzcfxxlrInfo(Head, CFInfo, YP, XM);
    },
    //Lưu đơn thuốc khám ngoại trú
    MzcfxxSaveInfo: function (Head, CFInfo, YP, XM) {
        this.$shiqianAX.MzcfxxSaveInfo(Head, CFInfo, YP, XM);
    }
};

$.printAX = {
    $printAX: $('#yibaoAX')[0],
    /// <summary>
    /// In hóa đơn
    /// </summary>
    /// <param name="InitJson">Thông tin chính hóa đơn</param>
    /// <param name="InvoiceItemJson">Thông tin chi tiết hóa đơn</param>
    ///InitJson = "{InfoClientName:'张冬林',InfoClientTaxCode:'',InfoClientBankAccount:'',InfoClientAddressPhone:'16621173664',InfoSellerBankAccount:'',InfoSellerAddressPhone:'',InfoTaxRate:0,InfoNotes:'测试',InfoInvoicer:'美迪柯',InfoChecker:'',InfoCashier:'',InfoListName:'',InfoBillNumber:''}";
    ///InvoiceItemJson = "[{ListGoodsName:'Phí đăng ký khám',ListTaxItem:'4001',ListStandard:'',ListUnit:'项',ListNumber:1,ListPrice:1,ListAmount:1,ListPriceKind:0,ListTaxAmount:0}]";
    PrintInvoice: function (InitJson, InvoiceItemJson) {
        return eval('(' + this.$printAX.PrintInvoice(InitJson, InvoiceItemJson) + ')');
    },
    /// <summary>
    /// Hủy hóa đơn đã phát hành
    /// </summary>
    /// <param name="InfoTypeCode">Mã hóa đơn 10 ký tự</param>
    /// <param name="InfoNumber">Số hóa đơn</param>
    CancelInvoice: function (InfoTypeCode, InfoNumber) {
        return eval('(' + this.$printAX.CancelInvoice(InfoTypeCode, InfoNumber) + ')');
    },
    /// <summary>
    /// Đóng thẻ thuế vàng
    /// </summary>
    CloseCard: function () {
        return eval('(' + this.$printAX.CloseCard() + ')');
    }
};