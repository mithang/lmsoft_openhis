$.guianyibao = {
    $yibaoAX: $('#guianyibaoAX')[0],
    /// <summary>
    ///Đổi mật khẩu（02）
    /// <summary>
    ModifyPassword: function () {
        return eval('(' + this.$yibaoAX.ModifyPassword() + ')');
    },
    /// <summary>
    /// Nhận dạng danh tính（03）
    /// <summary>
    Identification: function () {
        return eval('(' + this.$yibaoAX.Identification() + ')');
    },
    /// <summary>
    /// Lấy thông tin đối chiếu mã（57）
    /// <summary>
    BmdzxxQuery: function () {
        return eval('(' + this.$yibaoAX.BmdzxxQuery() + ')');
    },
    /// <summary>
    /// Trung tâm ICD10 Tải dữ liệu（61）
    /// <summary>
    UploadICD10: function (inDataJson) {
        return eval('(' + this.$yibaoAX.UploadICD10(inDataJson) + ')');
    },
    /// <summary>
    /// Quyết toán khám thông thường và bệnh mãn tính (48)
    /// <summary>
    MzFeejs: function (inDataJson) {
        return eval('(' + this.$yibaoAX.MzFeejs(inDataJson) + ')');
    },
    /// <summary>
    /// Hoàn tác quyết toán khám ngoại trú(42) 
    /// </summary>
    MzJsht: function (inDataJson) {
        return eval('(' + this.$yibaoAX.MzJsht(inDataJson) + ')');
    },
    /// <summary>
    /// Làm thủ tục nhập viện（21）
    /// </summary>
    ZyRybl: function (inDataJson) {
        return eval('(' + this.$yibaoAX.ZyRybl(inDataJson) + ')');
    },
    /// <summary>
    /// Hoàn tác làm thủ tục nhập viện（22）
    /// </summary>
    ZyRyblht: function (inDataJson) {
        return eval('(' + this.$yibaoAX.ZyRyblht(inDataJson) + ')');
    },
    /// <summary>
    /// Sửa thông tin làm thủ tục nhập viện（23）
    /// </summary>
    ZyRyblxxXg: function (inDataJson) {
        return eval('(' + this.$yibaoAX.ZyRyblxxXg(inDataJson) + ')');
    },
    /// <summary>
    /// Khám ngoại trú,Nội trú Xóa hàng loạt chi tiết（33）
    /// </summary>
    DeleteMx: function (inDataJson) {
        return eval('(' + this.$yibaoAX.DeleteMx(inDataJson) + ')');
    },
    /// <summary>
    /// Ghi chi tiết chi phí nội trú（31）
    /// </summary>
    ZyFeemxXr: function (inDataJson) {
        return eval('(' + this.$yibaoAX.ZyFeemxXr(DAC001) + ')');
    },
    /// <summary>
    /// Mô phỏng quyết toán nội trú(43)
    /// </summary>
    ZyMnjs: function (inDataJson) {
        return eval('(' + this.$yibaoAX.ZyMnjs(inDataJson) + ')');
    },
    /// <summary>
    /// Quyết toán chi phí nội trú(41) 
    /// </summary>
    ZyFeejs: function (inDataJson) {
        return eval('(' + this.$yibaoAX.ZyFeejs(inDataJson) + ')');
    },
    /// <summary>
    /// Hoàn tác quyết toán nội trú(42)
    /// </summary>
    ZyFeejsht: function (inDataJson) {
        return eval('(' + this.$yibaoAX.ZyFeejsht(inDataJson) + ')');
    },
    /// <summary>
    /// Làm thủ tục xuất viện（25）
    /// </summary>
    ZyCybl: function (inDataJson) {
        return eval('(' + this.$yibaoAX.ZyCybl(inDataJson) + ')');
    },
    /// <summary>
    /// Hoàn tác làm thủ tục xuất viện（26）
    /// </summary>
    ZyCyblht: function (inDataJson) {
        return eval('(' + this.$yibaoAX.ZyCyblht(inDataJson) + ')');
    },
    /// <summary>
    /// Hàm giải phóng tài nguyên，Trong HIS Gọi khi ứng dụng thoát
    /// </summary>
    YibaoDestroy: function () {
        return eval('(' + this.$yibaoAX.YibaoDestroy() + ')');
    },
    /// <summary>
    /// Đối với giao dịch loại xử lý，HIS Sau khi gọi giao diện thành công thì hoàn tất HIS Lưu dữ liệu hệ thống，thì gọi giao dịch này để xử lý xác nhận trung tâm
    /// </summary>
    /// str_jylsh：Số giao dịch，str_jyyzm：Mã xác minh giao dịch （tương tự bên dưới）
    YibaoConfirm: function (str_jylsh, str_jyyzm) {
        return eval('(' + this.$yibaoAX.YibaoConfirm(str_jylsh, str_jyyzm) + ')');
    },
    /// <summary>
    /// Đối với giao dịch loại xử lý，HIS Sau khi gọi giao diện thành công nhưng chưa hoàn tất HIS Lưu dữ liệu hệ thống，thì gọi giao dịch này để xử lý hủy trung tâm
    /// </summary>
    YibaoCancle: function (str_jylsh) {
        return eval('(' + this.$yibaoAX.YibaoCancle(str_jylsh) + ')');
    },
    /// <summary>
    /// Tra cứu giao dịch
    /// </summary>
    YibaoGetUncertaintyTrade: function (str_jylsh) {
        return eval('(' + this.$yibaoAX.YibaoGetUncertaintyTrade(str_jylsh) + ')');
    },
    /// <summary>
    /// Đọc CMND/CCCD
    /// </summary>
    ReadIdCard: function () {
        return eval('(' + this.$yibaoAX.ReadIdCard() + ')');
    }
};