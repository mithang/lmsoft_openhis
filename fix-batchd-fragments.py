# Batch D fragment repair (in-place, whole-string replacements).
# The Batch D translate pass left fragmented strings: a dict entry translated
# PART of a phrase and left the rest as CJK (e.g. '岗 vị trí Tên' for 岗位名称).
# A translate-only re-run cannot fix these (the source phrase no longer
# matches); these exact current-string -> correct-Vietnamese pairs do.
# KEEP strings are deliberately absent: ReportServer URLs, the BHYT申报 folder
# path, 城保 option value, and logic comparisons (充值/取款/住院结算*/缺少账户/
# 当前收据号已使用/请选择/病人出院/病区中) must stay Chinese.
import os

PAIRS = [
    # ---------- ReportManage ----------
    ('Chi phí Tổng cộng kết quả phải là正数，Vui lòng xác nhận',
     'Tổng chi phí kết quả phải là số dương, vui lòng xác nhận'),
    ('Số tiền tính Lỗi，Vui lòng xác nhận<br/>（Chi phí+补差!=kết quả）',
     'Số tiền tính bị lỗi, vui lòng xác nhận<br/>(Chi phí + Bù chênh lệch != Kết quả)'),
    ('补差，Vui lòng điền lý do', 'Bù chênh lệch, vui lòng điền lý do'),
    ('补差：', 'Bù chênh lệch: '),
    ('Tra cứu间隔 Không thể相差 một tháng',
     'Khoảng thời gian tra cứu không thể chênh lệch quá một tháng'),
    ('Tra cứu có dữ liệu会 bật lên Tải xuống Nhắc nhở',
     'Khi tra cứu có dữ liệu sẽ hiện nhắc nhở tải xuống'),
    ('固定 báo cáo còn Chưa tạo', 'Báo cáo cố định vẫn chưa được tạo'),
    ('（báo cáo Đã固定 Tạo Ngày:', '(Ngày tạo báo cáo đã cố định: '),
    ('（còn Chưa固定 Chi phí Tổng cộng：', '(Tổng chi phí vẫn chưa cố định: '),
    ('Chưa能提 lấy đến Tháng hiện tại任何 Chi phí<br/>báo cáo Tạo Thất bại',
     'Chưa lấy được bất kỳ chi phí nào của tháng hiện tại<br/>Tạo báo cáo thất bại'),
    ('程序 Xảy ra bất thường<br/>báo cáo Tạo Thất bại',
     'Chương trình xảy ra bất thường<br/>Tạo báo cáo thất bại'),
    ('交账 Danh sách', 'Danh sách giao nhận'),
    ('xác nhận交账', 'Xác nhận giao nhận'),
    ('交账 Thành công', 'Giao nhận thành công'),
    ('Chọn交账', 'Chọn giao nhận'),
    ('申报 Loại', 'Loại khai báo'),
    ('结账 Ngày', 'Ngày quyết toán'),
    ('补差 Điều chỉnh', 'Điều chỉnh bù chênh lệch'),
    ('Tra cứu một ngày清', 'Tra cứu chi phí một ngày'),
    ('Chi phí một ngày清', 'Chi phí một ngày'),
    ('Nhập viện quyết toán主 đơn', 'Đơn quyết toán nhập viện chính'),
    ('Quyết toán ngoại trú主 đơn', 'Đơn quyết toán ngoại trú chính'),
    ('入 kho chi tiết Bảng', 'Bảng chi tiết nhập kho'),
    ('出 kho chi tiết Bảng', 'Bảng chi tiết xuất kho'),
    ('出 Đăng ký nhập viện đơn', 'Đơn đăng ký vào/ra viện'),
    ('物资 tồn kho Bảng', 'Bảng tồn kho vật tư'),
    ('onclick="guolv()" />过滤', 'onclick="guolv()" />Lọc'),
    ('onclick="huifu()" />恢复', 'onclick="huifu()" />Khôi phục'),
    ('城镇 dân cư', 'Dân cư thành thị'),
    ('城镇 người lao động', 'Người lao động thành thị'),
    ('ngoại trú Cấp cứu Bệnh nặng家 Giường Thu phí kho',
     'Kho thu phí giường tại nhà bệnh nặng cấp cứu ngoại trú'),
    ('西 nhà thuốc', 'Nhà thuốc tây'),
    ('西 kho thuốc', 'Kho thuốc tây'),
    ('Tự trả段 Tiền mặt', 'Tiền mặt tự trả theo đoạn'),
    ('bổ sung段 Tiền mặt', 'Tiền mặt bổ sung theo đoạn'),
    ('bổ sung段 Tài khoản', 'Tài khoản bổ sung theo đoạn'),
    ('quỹ chung段 Tiền mặt', 'Tiền mặt quỹ chung theo đoạn'),
    ('quỹ chung段 Tài khoản', 'Tài khoản quỹ chung theo đoạn'),
    ('报销 dấu hiệu', 'Dấu hiệu thanh toán bảo hiểm'),
    ('细 phút Số giao dịch', 'Số giao dịch chi tiết'),
    ('Thuốc Tên chung/医 dùng Vật tư品牌', 'Tên chung thuốc/Thương hiệu vật tư y dùng'),
    ('Chú thích册 số chứng nhận', 'Số chứng nhận đăng ký'),
    # ---------- SystemManage: specific phrases first ----------
    ('Vui lòng điền角色 thông tin，dùng于创建 hoặc Sửa角色 thông tin！',
     'Vui lòng điền thông tin vai trò, dùng để tạo hoặc sửa thông tin vai trò!'),
    ('Vui lòng là Cơ sở y tế维护 một', 'Vui lòng bảo trì một'),
    ('Bệnh viện quản trị viên', 'Quản trị viên bệnh viện'),
    ('có Cần theo', 'và cần dựa theo'),
    ('này Đặc biệt角色 Mã', 'mã vai trò đặc biệt này'),
    ('xác định là Không là Quản trị viên bệnh viện',
     'để xác định có phải là quản trị viên bệnh viện hay không'),
    ('，Mã<span style', ', mã <span style'),
    ('Vui lòng trước là dùng户 liên kết Nhân viên',
     'Vui lòng liên kết nhân viên cho người dùng trước'),
    ('Vui lòng维护 Nhân viên CMND/CCCD thông tin！',
     'Vui lòng bảo trì thông tin CMND/CCCD nhân viên!'),
    ('父级 Chọn Lỗi', 'Chọn cấp cha bị lỗi'),
    ('岗 vị trí Tên/岗 vị trí Mã số', 'Tên chức vụ/Mã chức vụ'),
    ('岗 vị trí Tên', 'Tên chức vụ'),
    ('岗 vị trí Mã số', 'Mã chức vụ'),
    ('Tạo mới岗 vị trí', 'Tạo mới chức vụ'),
    ('Thêm mới岗 vị trí', 'Thêm mới chức vụ'),
    ('Sửa岗 vị trí', 'Sửa chức vụ'),
    ('Xóa岗 vị trí', 'Xóa chức vụ'),
    ('Xem岗 vị trí', 'Xem chức vụ'),
    ('dùng户岗 vị trí', 'Chức vụ người dùng'),
    ('dùng户角色', 'Vai trò người dùng'),
    ('角色 dùng户', 'Người dùng vai trò'),
    ('角色 thông tin', 'Thông tin vai trò'),
    ('角色 Mã số', 'Mã vai trò'),
    ('角色 Tên', 'Tên vai trò'),
    ('角色授权', 'Phân quyền vai trò'),
    ('Sửa角色', 'Sửa vai trò'),
    ('Thêm mới角色', 'Thêm mới vai trò'),
    ('Hủy bỏ Tất cả授权', 'Hủy bỏ tất cả phân quyền'),
    ('授权 cho Tất cả Cơ sở', 'Phân quyền cho tất cả cơ sở'),
    ('Cơ sở授权', 'Phân quyền cơ sở'),
    ('授权 Tất cả', 'Phân quyền tất cả'),
    ('Phân loại维护', 'Bảo trì phân loại'),
    ('Thu phí Miễn giảm维护', 'Bảo trì miễn giảm thu phí'),
    ('Tính chất bệnh nhân维护', 'Bảo trì tính chất bệnh nhân'),
    ('bệnh nhân hệ thống Thu phí bổ sung维护', 'Bảo trì thu phí bổ sung hệ thống bệnh nhân'),
    ('bệnh nhân hệ thống Thu phí phạm vi维护', 'Bảo trì phạm vi thu phí hệ thống bệnh nhân'),
    ('hóa đơn cấu hình维护', 'Bảo trì cấu hình hóa đơn'),
    ('hệ thống Khoản thu phí gọi价维护', 'Bảo trì điều chỉnh giá hạng mục thu phí hệ thống'),
    ('hệ thống病案 Nhóm thu phí lớn维护', 'Bảo trì nhóm thu phí lớn bệnh án hệ thống'),
    ('nhắc nhở trong容维护', 'Bảo trì nội dung nhắc nhở'),
    ('Lịch sử Hồ sơ维护 Có hay không bật', 'Có bật bảo trì hồ sơ lịch sử hay không'),
    ('Tiếng Anh菜 đơn', 'Menu tiếng Anh'),
    ('Thao tác菜 đơn', 'Menu thao tác'),
    ('菜 đơn nút', 'Nút menu'),
    ('Sửa菜 đơn', 'Sửa menu'),
    ('Thêm mới菜 đơn', 'Thêm mới menu'),
    ('快捷菜 đơn Bảng đơn页', 'Trang đơn menu nhanh'),
    ('快捷菜 đơn Danh sách页', 'Trang danh sách menu nhanh'),
    ('mặc định một级菜 đơn Ngoại trú Quản lý，hiện tại Giá trị là hai级菜 đơn',
     'Menu cấp một mặc định là Quản lý ngoại trú, giá trị hiện tại là menu cấp hai'),
    ('Thêm mới区域', 'Thêm mới khu vực'),
    ('Sửa区域', 'Sửa khu vực'),
    ('Xóa区域', 'Xóa khu vực'),
    ('Tạo mới区域', 'Tạo mới khu vực'),
    ('Thứ ba方标示', 'Mã định danh bên thứ ba'),
    ('Thứ ba方 Mã', 'Mã bên thứ ba'),
    ('Thứ ba方 Tên', 'Tên bên thứ ba'),
    ('đối với照 Phân loại', 'Phân loại đối chiếu'),
    ('đối với照', 'Đối chiếu'),
    ('Ngoại trú Cảnh báo trong容', 'Nội dung cảnh báo ngoại trú'),
    ('Nhập viện Cảnh báo trong容', 'Nội dung cảnh báo nhập viện'),
    ('Ngoại trú Cảnh báo级别', 'Cấp độ cảnh báo ngoại trú'),
    ('Nhập viện Cảnh báo级别', 'Cấp độ cảnh báo nhập viện'),
    ('Nhật ký trong容', 'Nội dung nhật ký'),
    ('Mã/trong容', 'Mã/Nội dung'),
    ('算法级别', 'Cấp độ thuật toán'),
    ('适 dùng phạm vi', 'Phạm vi áp dụng'),
    ('部 ngoại trú', 'Khoa phòng'),
    ('ngoài部 phải dùng', 'Phải dùng bên ngoài'),
    ('公司', 'Công ty'),
    ('集团', 'Tập đoàn'),
    ('小组', 'Nhóm'),
    ('父节点', 'Nút cha'),
    ('trên级', 'Cấp trên'),
    ('领 dùng Nhân viên', 'Nhân viên lĩnh dùng'),
    ('领 dùng Ngày', 'Ngày lĩnh dùng'),
    ('领 dùng người Họ tên', 'Họ tên người lĩnh dùng'),
    ('Thêm mới dùng户', 'Thêm mới người dùng'),
    ('Chọn dùng户', 'Chọn người dùng'),
    ('gỡ dùng户', 'Gỡ người dùng'),
    ('Khóa chính dùng户Id', 'Id người dùng khóa chính'),
    ('Bảng đơn页', 'Trang đơn'),
    ('Danh sách页', 'Trang danh sách'),
    ('bật lên页', 'Trang mở'),
    ('khung架页', 'Trang khung'),
    ('子 trang', 'Trang con'),
    ('Có hay không mở放', 'Có mở hay không'),
    ('停 dùng', 'Ngừng dùng'),
    ('Vui lòng nhập账 số', 'Vui lòng nhập số tài khoản'),
    ('账 số', 'Tài khoản'),
    ('Vui lòng trống格 hoặc Từ khóa tiến hành Chọn',
     'Vui lòng nhập khoảng trắng hoặc từ khóa để chọn'),
    ('Từ始 Số biên lai', 'Số biên lai bắt đầu'),
    ('Từ始 Số hóa đơn', 'Số hóa đơn bắt đầu'),
    ('Biên lai phút配 cấu hình', 'Cấu hình phân phối biên lai'),
    ('Chưa配 lúc giá trị mặc định', 'Giá trị mặc định khi chưa cấu hình'),
    ('Có hay không必配', 'Có bắt buộc cấu hình hay không'),
    ('giao diện问题 Nhật ký', 'Nhật ký vấn đề giao diện'),
    ('<option value=true selected>问题</option>',
     '<option value=true selected>Có vấn đề</option>'),
    ('图片 Đường dẫn', 'Đường dẫn hình ảnh'),
    ('介绍：', 'Giới thiệu:'),
    ('介绍', 'Giới thiệu'),
    ('创建 Ngày', 'Ngày tạo'),
    ('详细 bất thường', 'Bất thường chi tiết'),
    ('Thanh toán/Hoàn tiền方式', 'Phương thức thanh toán/hoàn tiền'),
    ('Số giấy tờ，loại giấy tờ Có hay không必填', 'Số giấy tờ, loại giấy tờ có bắt buộc hay không'),
    ('Giá trị示例', 'Ví dụ giá trị'),
    ('ngoại trú ghi sổ khám Tra cứu Thông tin bệnh nhân điều kiện控制',
     'Kiểm soát điều kiện tra cứu thông tin bệnh nhân khi đăng ký ngoại trú'),
    ('Kỹ thuật viên trị liệu每 Ngày Công việc giờ数',
     'Số giờ làm việc mỗi ngày của kỹ thuật viên trị liệu'),
    ('Kỹ thuật viên trị liệu模块', 'Mô-đun kỹ thuật viên trị liệu'),
    ('ON Biểu thị bật Thu phí xác nhận功能，OFF Biểu thị Chưa启 dùng Thu phí xác nhận功能',
     'ON nghĩa là đã bật chức năng xác nhận thu phí, OFF nghĩa là chưa bật chức năng xác nhận thu phí'),
    ('hệ thống Có hay không bật Thu phí xác nhận功能',
     'Hệ thống có bật chức năng xác nhận thu phí hay không'),
    ('ngoại trú đăng ký khám của Đăng ký không cần thẻ trong，ON Biểu thị必填，OFF Biểu thị Không phải必填',
     'Đăng ký không cần thẻ trong đăng ký ngoại trú, ON nghĩa là bắt buộc, OFF nghĩa là không bắt buộc'),
    ('Ngoại trú sau khi thu phí thành công sau Có hay không tự động Tạo计划',
     'Sau khi thu phí ngoại trú thành công có tự động tạo kế hoạch hay không'),
    ('Ngoại trú计划 ghi sổ，Thu phí ngoại trú', 'Ghi sổ kế hoạch ngoại trú, thu phí ngoại trú'),
    ('ghi sổ Thực hiện mặc định提 trước Ngày phạm vi',
     'Phạm vi ngày mặc định lấy trước khi thực hiện ghi sổ'),
    ('này条 nạp tiền Đã hoàn款，Vui lòng kiểm tra',
     'Khoản nạp tiền này đã được hoàn lại, vui lòng kiểm tra'),
    ('Số tiền không thể低于0đồng', 'Số tiền không thể thấp hơn 0 đồng'),
    ('两 lần nhập của mật khẩu không nhất quán', 'Hai lần nhập mật khẩu không nhất quán'),
    ('mật khẩu复杂度 không够<br/>6-16vị trí，phải包 bao gồm chữ cái dữ liệu',
     'Độ phức tạp mật khẩu không đủ<br/>6-16 ký tự, phải bao gồm chữ cái và chữ số'),
    ('6-16vị trí，phải包 bao gồm chữ cái chữ số', '6-16 ký tự, phải bao gồm chữ cái và chữ số'),
    ('âm责 người', 'Người phụ trách'),
    ('Không có权限 Sửa', 'Không có quyền sửa'),
    ('近7Ngày', '7 ngày gần đây'),
    ('近1tháng', '1 tháng gần đây'),
    ('近3tháng', '3 tháng gần đây'),
    ('今 Ngày', 'Hôm nay'),
    # ---------- base single-term fallbacks (applied last) ----------
    ('岗 vị trí', 'Chức vụ'),
    ('角色', 'Vai trò'),
    ('授权', 'Phân quyền'),
    ('维护', 'Bảo trì'),
    ('菜 đơn', 'Menu'),
    ('区域', 'Khu vực'),
    ('连接', 'Liên kết'),
    ('图标', 'Biểu tượng'),
    ('目标', 'Mục tiêu'),
    ('Thứ ba方', 'Bên thứ ba'),
    ('trong容', 'Nội dung'),
    ('级别', 'Cấp độ'),
    ('父级', 'Cấp cha'),
    ('dùng户', 'người dùng'),
    ('消息', 'Tin nhắn'),
    ('功能', 'Chức năng'),
    ('算法', 'Thuật toán'),
    ('必填', 'Bắt buộc'),
    ('问题', 'Vấn đề'),
]

def area_files():
    for area in ['ReportManage', 'SystemManage']:
        base = f'Newtouch.Sett.Web/Areas/{area}'
        for root, dirs, files in os.walk(base):
            for fn in files:
                if fn.endswith('.bak') or not (fn.endswith('.cs') or fn.endswith('.cshtml')):
                    continue
                yield os.path.join(root, fn)

def main():
    pairs = sorted(PAIRS, key=lambda p: -len(p[0]))
    # count pre-apply matches first (distinguishes applied vs never-present)
    pre = {}
    for old, new in pairs:
        n = 0
        for p in area_files():
            n += open(p, encoding='utf-8-sig').read().count(old)
        pre[old] = n
    total_files = 0
    for p in area_files():
        text = open(p, encoding='utf-8-sig').read()
        orig = text
        # two passes: chained pairs (a replacement creating the input of a
        # longer pair that already ran) resolve on the second pass
        for _ in range(2):
            for old, new in pairs:
                text = text.replace(old, new)
        if text != orig:
            open(p, 'w', encoding='utf-8').write(text)
            total_files += 1
    print('files changed:', total_files)
    print('pairs never present (check map for typos):')
    for old, new in pairs:
        if pre[old] == 0:
            print('  NEVER:', old)
    print('total pre-apply matches:', sum(pre.values()))

if __name__ == '__main__':
    main()
