$.guianyibao = {
    $yibaoAX: $('')[0],
    /// <summary>
    ///修改密码（02）
    /// <summary>
    ModifyPassword: function () {
        return eval('' + this.$yibaoAX.ModifyPassword() + '');
    },
    /// <summary>
    /// 身份识别（03）
    /// <summary>
    Identification: function () {
        return eval('' + this.$yibaoAX.Identification() + '');
    },
    /// <summary>
    /// Mã lấy thông tin đối chiếu（57）
    /// <summary>
    BmdzxxQuery: function () {
        return eval('' + this.$yibaoAX.BmdzxxQuery() + '');
    },
    /// <summary>
    /// 中心 ICD10 Tải xuống dữ liệu（61）
    /// <summary>
    UploadICD10: function (inDataJson) {
        return eval('' + this.$yibaoAX.UploadICD10(inDataJson) + '');
    },
    /// <summary>
    /// 普通门诊及慢性病结算 (48)
    /// <summary>
    MzFeejs: function (inDataJson) {
        return eval('' + this.$yibaoAX.MzFeejs(inDataJson) + '');
    },
    /// <summary>
    /// Trả lại lần quyết toán ngoại trú(42) 
    /// </summary>
    MzJsht: function (inDataJson) {
        return eval('' + this.$yibaoAX.MzJsht(inDataJson) + '');
    },
    /// <summary>
    /// Làm thủ tục nhập viện（21）
    /// </summary>
    ZyRybl: function (inDataJson) {
        return eval('' + this.$yibaoAX.ZyRybl(inDataJson) + '');
    },
    /// <summary>
    /// Trả lại lần làm thủ tục nhập viện（22）
    /// </summary>
    ZyRyblht: function (inDataJson) {
        return eval('' + this.$yibaoAX.ZyRyblht(inDataJson) + '');
    },
    /// <summary>
    /// Sửa thông tin làm thủ tục nhập viện（23）
    /// </summary>
    ZyRyblxxXg: function (inDataJson) {
        return eval('' + this.$yibaoAX.ZyRyblxxXg(inDataJson) + '');
    },
    /// <summary>
    /// Ngoại trú,Nhập viện Chi tiết xóa lượng lô（33）
    /// </summary>
    DeleteMx: function (inDataJson) {
        return eval('' + this.$yibaoAX.DeleteMx(inDataJson) + '');
    },
    /// <summary>
    /// Nhập viện ghi chi tiết chi phí（31）
    /// </summary>
    ZyFeemxXr: function (inDataJson) {
        return eval('' + this.$yibaoAX.ZyFeemxXr(DAC001) + '');
    },
    /// <summary>
    /// 住院模拟结算(43)
    /// </summary>
    ZyMnjs: function (inDataJson) {
        return eval('' + this.$yibaoAX.ZyMnjs(inDataJson) + '');
    },
    /// <summary>
    /// Nhập viện quyết toán chi phí(41) 
    /// </summary>
    ZyFeejs: function (inDataJson) {
        return eval('' + this.$yibaoAX.ZyFeejs(inDataJson) + '');
    },
    /// <summary>
    /// Nhập viện trả lại lần quyết toán(42)
    /// </summary>
    ZyFeejsht: function (inDataJson) {
        return eval('' + this.$yibaoAX.ZyFeejsht(inDataJson) + '');
    },
    /// <summary>
    /// Làm thủ tục xuất viện（25）
    /// </summary>
    ZyCybl: function (inDataJson) {
        return eval('' + this.$yibaoAX.ZyCybl(inDataJson) + '');
    },
    /// <summary>
    /// Quay lại làm thủ tục xuất viện（26）
    /// </summary>
    ZyCyblht: function (inDataJson) {
        return eval('' + this.$yibaoAX.ZyCyblht(inDataJson) + '');
    },
    /// <summary>
    /// Hàm giải phóng tài nguyên，Ở HIS 应用退出时调用
    /// </summary>
    YibaoDestroy: function () {
        return eval('' + this.$yibaoAX.YibaoDestroy() + '');
    },
    /// <summary>
    /// 对于处理类交易，HIS Gọi hoàn thành sau thành công giao diện HIS Lưu dữ liệu hệ thống，则调用该交易进行中心确认处理
    /// </summary>
    /// str_jylsh：Số giao dịch，str_jyyzm：交易验证码 （下同）
    YibaoConfirm: function (str_jylsh, str_jyyzm) {
        return eval('' + this.$yibaoAX.YibaoConfirm(str_jylsh, str_jyyzm) + '');
    },
    /// <summary>
    /// 对于处理类交易，HIS Gọi chưa hoàn thành sau thành công giao diện HIS Lưu dữ liệu hệ thống，则调用该交易进行中心取消处理
    /// </summary>
    YibaoCancle: function (str_jylsh) {
        return eval('' + this.$yibaoAX.YibaoCancle(str_jylsh) + '');
    },
    /// <summary>
    /// Tra cứu giao dịch
    /// </summary>
    YibaoGetUncertaintyTrade: function (str_jylsh) {
        return eval('' + this.$yibaoAX.YibaoGetUncertaintyTrade(str_jylsh) + '');
    },
    /// <summary>
    /// 读身份证
    /// </summary>
    ReadIdCard: function () {
        return eval('' + this.$yibaoAX.ReadIdCard() + '');
    }
};