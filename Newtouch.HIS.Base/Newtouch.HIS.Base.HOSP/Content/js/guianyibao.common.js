$.guianyibao = {
    $yibaoAX: $('#guianyibaoAX')[0],
    /// <summary>
    ///Đổi mật khẩu（02）
    /// <summary>
    ModifyPassword: function () {
        return eval('(' + this.$yibaoAX.ModifyPassword() + ')');
    },
    /// <summary>
    /// Nhận dạng（03）
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
    /// Trung tâm ICD10 Tải xuống dữ liệu（61）
    /// <summary>
    UploadICD10: function (inDataJson) {
        return eval('(' + this.$yibaoAX.UploadICD10(inDataJson) + ')');
    },
    /// <summary>
    /// Thanh toán ngoại trú thường và bệnh mạn tính (48)
    /// <summary>
    MzFeejs: function (inDataJson) {
        return eval('(' + this.$yibaoAX.MzFeejs(inDataJson) + ')');
    },
    /// <summary>
    /// Hoàn trả thanh toán ngoại trú(42) 
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
    /// Hoàn trả thủ tục nhập viện（22）
    /// </summary>
    ZyRyblht: function (inDataJson) {
        return eval('(' + this.$yibaoAX.ZyRyblht(inDataJson) + ')');
    },
    /// <summary>
    /// Sửa thông tin thủ tục nhập viện（23）
    /// </summary>
    ZyRyblxxXg: function (inDataJson) {
        return eval('(' + this.$yibaoAX.ZyRyblxxXg(inDataJson) + ')');
    },
    /// <summary>
    /// Ngoại trú,Nội trú Xóa hàng loạt chi tiết（33）
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
    /// Thanh toán mô phỏng nội trú(43)
    /// </summary>
    ZyMnjs: function (inDataJson) {
        return eval('(' + this.$yibaoAX.ZyMnjs(inDataJson) + ')');
    },
    /// <summary>
    /// Thanh toán chi phí nội trú(41) 
    /// </summary>
    ZyFeejs: function (inDataJson) {
        return eval('(' + this.$yibaoAX.ZyFeejs(inDataJson) + ')');
    },
    /// <summary>
    /// Hoàn trả thanh toán nội trú(42)
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
    /// Hoàn trả thủ tục xuất viện（26）
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
    /// Đối với giao dịch loại xử lý，HIS Hoàn thành sau khi gọi giao diện thành công HIS Lưu dữ liệu hệ thống，thì gọi giao dịch này để trung tâm xử lý xác nhận
    /// </summary>
    /// str_jylsh：Số thứ tự giao dịch，str_jyyzm：Mã xác minh giao dịch （Như dưới）
    YibaoConfirm: function (str_jylsh, str_jyyzm) {
        return eval('(' + this.$yibaoAX.YibaoConfirm(str_jylsh, str_jyyzm) + ')');
    },
    /// <summary>
    /// Đối với giao dịch loại xử lý，HIS Chưa hoàn thành sau khi gọi giao diện thành công HIS Lưu dữ liệu hệ thống，thì gọi giao dịch này để trung tâm xử lý hủy
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
    /// Đọc chứng minh nhân dân
    /// </summary>
    ReadIdCard: function () {
        return eval('(' + this.$yibaoAX.ReadIdCard() + ')');
    }
};