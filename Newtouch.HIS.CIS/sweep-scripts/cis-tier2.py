# -*- coding: utf-8 -*-
"""CIS Web translation sweep, tier 2: missing fragments + whole-literal phrases.
Same comment protection as tier 1. Skip: ==/!=/Equals/SQL/log/URL lines
(case lines ARE translated this tier, for key consistency with UI values)."""
import os, re

W = "/home/hatch/workspace/openhis-core/Newtouch.HIS.CIS/Newtouch.CIS.Web"
COMMENT = re.compile(r"(@\*.*?\*@|<!--.*?-->|(?<!:)//[^\n]*|/\*.*?\*/)", re.DOTALL)
SKIP_LINE = re.compile(r"(==|!=|\.Equals\(|SELECT\s|FROM\s|WHERE\s|INSERT\s+INTO|UPDATE\s+\w+\s+SET|DELETE\s+FROM|\bEXEC\b|Log\.|logger\.|Console\.Write|https?://)")

# load tier-1 dict to avoid re-adding (and contradicting) it
_src = open('/tmp/cis-tier1.py', encoding='utf-8').read()
_dict_src = "D = " + _src.split('D = ', 1)[1]
_dict_src = _dict_src[:_dict_src.index('\n}\n') + 3]
_ns = {}
exec(_dict_src, _ns)
D1 = _ns['D']

NEW = {
# --- missing word fragments ---
"右上": "Trên phải", "左上": "Trên trái", "左下": "Dưới trái", "右下": "Dưới phải",
"内码": "Mã nội bộ", "电子": "Điện tử", "性质": "Tính chất", "范围": "Phạm vi",
"续打": "In tiếp", "备药": "Chuẩn bị thuốc", "多张": "nhiều tờ", "同组": "Cùng nhóm",
"科别": "Chuyên khoa", "注意事项": "Lưu ý", "给药途径": "Đường dùng thuốc",
"给药频率": "Tần suất dùng thuốc", "批准文号": "Số phê duyệt", "费别": "Loại phí",
"报销政策": "Chính sách thanh toán", "建议": "Đề xuất", "清除": "Xóa sạch",
"返院": "Trở lại viện", "饮食": "Ăn uống", "出生年月": "Ngày sinh",
"连续": "Liên tục", "中间": "Ở giữa", "标志": "Dấu hiệu",
"超过": "Vượt quá", "单次": "Một lần", "进价": "Giá nhập",
"撤回": "Thu hồi", "上传": "Tải lên", "辅助": "Hỗ trợ",
"取药": "Lấy thuốc", "第三方": "Bên thứ ba", "关键字": "Từ khóa",
"绑定": "Gắn kết", "颜色": "Màu sắc", "对照": "Đối chiếu",
"加载中": "Đang tải", "国家": "Quốc gia", "未知": "Không rõ",
"相关": "liên quan", "绘制": "Vẽ", "调用": "Gọi",
"接口": "Giao diện", "提醒": "Nhắc nhở", "事前": "Trước",
"动态": "Động", "远程": "Từ xa", "调阅": "Tra cứu",
"心电": "Điện tim", "波形": "Dạng sóng", "唯一": "Duy nhất",
"继承": "Kế thừa", "暂停": "Tạm dừng", "恢复": "Khôi phục",
"召回": "Thu hồi", "提取": "Trích xuất", "手动": "Thủ công",
"自动": "Tự động", "跳过": "Bỏ qua", "不完整": "Không đầy đủ",
"合理": "Hợp lý", "用药": "Dùng thuốc", "个人": "Cá nhân",
"过敏": "Dị ứng", "反应": "Phản ứng", "阴性": "Âm tính",
"可疑": "Nghi ngờ", "确诊": "Chẩn đoán xác định", "初步": "Sơ bộ",
"补充": "Bổ sung", "更正": "Sửa đổi", "既往": "Trước đây",
"家族": "Gia đình", "婚育": "Hôn nhân sinh đẻ", "月经": "Kinh nguyệt",
"生育": "Sinh đẻ", "主诉": "Triệu chứng chính",
# --- order-sensitive phrases / whole literals ---
"新Chi tiết": "Chi tiết mới", "Số nhóm为": "Số nhóm là",
"长<br>临": "Dài hạn<br>Tạm thời", "Y lệnh性质": "Tính chất y lệnh",
"Y lệnh性质Mã": "Mã tính chất y lệnh", "Y lệnh号": "Số y lệnh",
"零售Đơn giá": "Đơn giá bán lẻ", "进价Đơn giá": "Đơn giá nhập",
"零售Số tiền(理)": "Số tiền bán lẻ (lý thuyết)",
"零售Số tiền(实)": "Số tiền bán lẻ (thực tế)",
"(实)Phòng ban数": "Số phòng ban (thực tế)",
"(实)Nhỏ nhất数": "Số nhỏ nhất (thực tế)",
"理论数": "Số lý thuyết", "存为Mẫu": "Lưu thành mẫu",
"天)": "ngày)", "岁-": "tuổi-", " 查 询 ": " Tra cứu ",
" 至 ": " đến ", " 新 录 入 ": " Nhập mới ",
" 恒牙": " Răng vĩnh viễn", "次/分": "lần/phút", "升/分": "lít/phút",
'<th class="formTitle">副</th>': '<th class="formTitle">Phụ</th>',
'<option value="JI">精I</option>': '<option value="JI">Hướng thần I</option>',
'<option value="JII">精II</option>': '<option value="JII">Hướng thần II</option>',
'<option value="MZ">麻醉</option>': '<option value="MZ">Thuốc gây nghiện</option>',
"Thực hiện者": "Người thực hiện", "Thực hiện人": "Người thực hiện",
"Quản lý员": "Quản trị viên", "Hiện tại行": "Dòng hiện tại",
"Thời gian点": "Thời điểm", "Số khám ngoại trú码": "Mã số khám ngoại trú",
"国家Bảo hiểm y tế码": "Mã BHYT quốc gia", "Vui lòng chọn牙位": "Vui lòng chọn vị trí răng",
# loading messages
"Dữ liệu góiXóa中，Vui lòng đợi trong giây lát...": "Đang xóa dữ liệu gói, vui lòng đợi trong giây lát...",
"Dữ liệu góiLưu中，Vui lòng đợi trong giây lát...": "Đang lưu dữ liệu gói, vui lòng đợi trong giây lát...",
"Dữ liệuTra cứu中，Vui lòng đợi trong giây lát...": "Đang tra cứu dữ liệu, vui lòng đợi trong giây lát...",
"Bệnh ánDữ liệu加载中，Vui lòng đợi trong giây lát…": "Đang tải dữ liệu bệnh án, vui lòng đợi trong giây lát…",
"Y lệnhDữ liệuLưu中，Vui lòng đợi trong giây lát...": "Đang lưu dữ liệu y lệnh, vui lòng đợi trong giây lát...",
"Dữ liệu加载中，Vui lòng đợi trong giây lát...": "Đang tải dữ liệu, vui lòng đợi trong giây lát...",
"Y lệnhThực hiện中，Vui lòng đợi trong giây lát...": "Đang thực hiện y lệnh, vui lòng đợi trong giây lát...",
"MẫuTrích dẫn中，Vui lòng đợi trong giây lát...": "Đang trích dẫn mẫu, vui lòng đợi trong giây lát...",
"Phiếu nhiệt độ绘制中，Vui lòng đợi trong giây lát...": "Đang vẽ phiếu nhiệt độ, vui lòng đợi trong giây lát...",
# 的-sentences and message literals
"Vui lòngĐã chọn需Thực hiện的Bệnh nhân": "Vui lòng chọn bệnh nhân cần thực hiện",
"Hiện tạiKhông cóLưu的Nội dung y lệnh": "Hiện tại không có nội dung y lệnh đã lưu",
"Số lượngphải lớn hơn0的整数！": "Số lượng phải là số nguyên lớn hơn 0!",
"转化因子phải lớn hơn0的整数！": "Hệ số chuyển đổi phải là số nguyên lớn hơn 0!",
"尚未Đã chọn任何Chi tiết": "Chưa chọn bất kỳ chi tiết nào",
"Xác nhận要ThêmĐơn thuốc mới么？": "Xác nhận muốn thêm đơn thuốc mới?",
"Vui lòng chọnThời gian范围": "Vui lòng chọn khoảng thời gian",
"Vui lòng chọnThời gian点": "Vui lòng chọn thời điểm",
"不可重复Thêm": "Không được thêm trùng lặp",
"Chính症状": "Triệu chứng chính",
"Hiện tạiTrạng thái不是Đã hoàn thànhTrạng tháiKhông thểXemBáo cáo": "Trạng thái hiện tại không phải đã hoàn thành nên không thể xem báo cáo",
"事前提醒调用thất bại：": "Gọi nhắc trước thất bại:",
"Xác nhận要Xóa吗？": "Xác nhận muốn xóa?",
"Đơn thuốc内容": "Nội dung đơn thuốc",
"未找到Hiện tại行": "Không tìm thấy dòng hiện tại",
"未Đã chọnBệnh nhân": "Chưa chọn bệnh nhân",
"Liều lượngNhỏ hơnKháng sinh单次Liều lượngNhỏ nhất范围，Vui lòng xác nhận。": "Liều lượng nhỏ hơn phạm vi liều tối thiểu một lần của kháng sinh, vui lòng xác nhận.",
"Liều lượng超过Kháng sinh单次Liều lượngLớn nhất范围，Vui lòng xác nhận。": "Liều lượng vượt quá phạm vi liều tối đa một lần của kháng sinh, vui lòng xác nhận.",
"您的Quyền无法Kê此Kháng sinhMục，Vui lòng xác nhận。": "Quyền của bạn không thể kê mục kháng sinh này, vui lòng xác nhận.",
"该Tần suất低于Kháng sinh指定Nhỏ nhấtThời gianKhoảng cách，Vui lòng xác nhận。": "Tần suất này thấp hơn khoảng thời gian tối thiểu kháng sinh quy định, vui lòng xác nhận.",
"该Tần suất超过Kháng sinh指定Lớn nhấtThời gianKhoảng cách，Vui lòng xác nhận。": "Tần suất này vượt quá khoảng thời gian tối đa kháng sinh quy định, vui lòng xác nhận.",
"的Cách dùngKhông nhất quán": "Cách dùng không nhất quán",
"的Tần suấtKhông nhất quán": "Tần suất không nhất quán",
"的Thực hiện频率Không nhất quán": "Tần suất thực hiện không nhất quán",
"的Thời gian bắt đầuKhông nhất quán": "Thời gian bắt đầu không nhất quán",
"未知Loại": "Loại không rõ", "Thao tác区": "Khu thao tác",
"Y lệnh dài hạn不Hỗ trợ续打,请勿勾选续打": "Y lệnh dài hạn không hỗ trợ in tiếp, vui lòng không tick chọn in tiếp",
"Y lệnh tạm thời续打": "In tiếp y lệnh tạm thời",
"Y lệnh dài hạn续打": "In tiếp y lệnh dài hạn",
"Vui lòng liên hệQuản lý员Cấu hình过敏MụcCấu hình动态Tham số,Tham sốMã：AllergyItemSetting": "Vui lòng liên hệ quản trị viên cấu hình mục dị ứng và tham số động, mã tham số: AllergyItemSetting",
"绑定Bệnh nhân所属Bác sĩ": "Gắn bác sĩ phụ trách của bệnh nhân",
"Nhập viện床卡": "Thẻ giường nhập viện", "连续Nhập": "Nhập liên tục",
"[停]": "[Dừng]", "nhiều dòngY lệnh无法绑定": "Nhiều dòng y lệnh không thể gắn kết",
"nhiều dòngY lệnh无法Hủy bỏ/Dừng": "Nhiều dòng y lệnh không thể hủy bỏ/dừng",
"发药数不得超过Tồn khoTổng lượng": "Số lượng phát thuốc không được vượt quá tổng lượng tồn kho",
"请Đã chọn一Phòng bệnh": "Vui lòng chọn một phòng bệnh",
"请Đã chọn一病床": "Vui lòng chọn một giường bệnh",
"最多Đã chọnmột dòng": "Đã chọn tối đa một dòng",
"辅助Kiểm tra": "Kiểm tra hỗ trợ", "家长Họ tên": "Họ tên phụ huynh",
"Điện thoại号码": "Số điện thoại",
"Không thểĐồng thờiSửa多张Thuốc YHCTĐơn thuốc": "Không thể đồng thời sửa nhiều tờ đơn thuốc YHCT",
"该Bệnh nhân为Bảo hiểm y tếBệnh nhân，Không thể chọnmục tự chi trả【": "Bệnh nhân này là bệnh nhân BHYT, không thể chọn mục tự chi trả [",
"其中Mục[": "Trong đó mục [", "]，Kết quả为:": "], kết quả là:",
"Bệnh nhânThông tinBất thường，Hiện tạiBệnh nhân已Kết thúcKhám bệnh。": "Thông tin bệnh nhân bất thường, bệnh nhân hiện tại đã kết thúc khám bệnh.",
"给药途径": "Đường dùng thuốc",
"Tốc độ truyền<br>滴/phút": "Tốc độ truyền<br>giọt/phút",
"Phục hồi chức năng建议": "Đề xuất phục hồi chức năng",
"Thiếu suất ăn型号": "Thiếu mẫu suất ăn", "未Duyệt": "Chưa duyệt",
"Bảo hiểm y tếDuyệtDịch vụ不可访问：[": "Dịch vụ duyệt BHYT không thể truy cập: [",
"不是Thuốc gây nghiện - hướng thầnY lệnhKhông thểIn!": "Không phải y lệnh thuốc gây nghiện - hướng thần nên không thể in!",
"请Đã chọn要In的Thuốc gây nghiện - hướng thầnY lệnh": "Vui lòng chọn y lệnh thuốc gây nghiện - hướng thần cần in",
"Vui lòng chọnCầnTra cứu的Đơn thuốc": "Vui lòng chọn đơn thuốc cần tra cứu",
"电子Đơn thuốc取药Kết quả査询thất bại：": "Tra cứu kết quả lấy thuốc đơn điện tử thất bại:",
"无Tồn kho，Vui lòng chọn lại": "Không có tồn kho, vui lòng chọn lại",
"Đơn giá为不Nhỏ hơn0的数字": "Đơn giá phải là số không nhỏ hơn 0",
"召回thành công": "Thu hồi thành công", "出生Ngày": "Ngày sinh",
"请Đã chọnmột dòngSuất ănThông tin": "Vui lòng chọn một dòng thông tin suất ăn",
"Hiện tạiKhông cóLưu的Suất ăn内容": "Hiện tại không có nội dung suất ăn đã lưu",
"请Đã chọnKhu bệnh": "Vui lòng chọn khu bệnh",
"分Số nhóm": "Số nhóm", "Vui lòngĐã chọn需In的Y lệnh": "Vui lòng chọn y lệnh cần in",
"Vui lòng chọn trướcThực hiện单Loại": "Vui lòng chọn trước loại phiếu thực hiện",
"摄出量": "Lượng vào ra", "Nhậpthất bại或者已Nhập": "Nhập thất bại hoặc đã nhập",
"返院": "Trở lại viện", "Khoa备药Đề nghị": "Đề nghị chuẩn bị thuốc của khoa",
"Thừa thiếu标志": "Dấu hiệu thừa thiếu",
"Vui lòng nhập第三方Mã": "Vui lòng nhập mã bên thứ ba",
"将替换Bệnh ánChi tiết的内容，Có tiếp tục không？": "Sẽ thay thế nội dung chi tiết bệnh án, có tiếp tục không?",
"的同种Thuốc超过5个，请ThêmĐơn thuốc mới": "Thuốc cùng loại vượt quá 5, vui lòng thêm đơn thuốc mới",
"Vui lòng chọn对应的Chẩn đoán!": "Vui lòng chọn chẩn đoán tương ứng!",
"Hiện tạiKhông cóLưu的内容": "Hiện tại không có nội dung đã lưu",
"该Bệnh nhân存在未Duyệt的Y lệnh，Vui lòng liên hệĐiều dưỡngDuyệt后再做Thao tác": "Bệnh nhân này có y lệnh chưa duyệt, vui lòng liên hệ điều dưỡng duyệt rồi mới thao tác",
"Có hay khôngXác nhậnXuất việnDừng toàn bộ该Bệnh nhân的Y lệnh dài hạn？": "Có xác nhận dừng toàn bộ y lệnh dài hạn của bệnh nhân này khi xuất viện không?",
"Có hay khôngXác nhậnDừng toàn bộ该Bệnh nhân的Y lệnh dài hạn？": "Có xác nhận dừng toàn bộ y lệnh dài hạn của bệnh nhân này không?",
"已撤回的Đơn thuốc不可Tra cứu": "Đơn thuốc đã thu hồi không thể tra cứu",
"请传入要Thực hiện的Thuốc": "Vui lòng truyền vào thuốc cần thực hiện",
"请Đã chọnmột dòng已Thực hiện的Mục": "Vui lòng chọn một dòng mục đã thực hiện",
"Vui lòngĐã chọn需Thực hiện的Y lệnh": "Vui lòng chọn y lệnh cần thực hiện",
"Vui lòngĐã chọn需绑定Chi phí的Bệnh nhân": "Vui lòng chọn bệnh nhân cần gắn chi phí",
"请Đã chọnCầnHủy bỏ或Dừng的Y lệnh": "Vui lòng chọn y lệnh cần hủy bỏ hoặc dừng",
"Vui lòng chọnCầnSửa的Chứng từ": "Vui lòng chọn chứng từ cần sửa",
"Vui lòng chọnCầnGửi的Chứng từ": "Vui lòng chọn chứng từ cần gửi",
"Vui lòng chọnCần撤回的Chứng từ": "Vui lòng chọn chứng từ cần thu hồi",
"Vui lòng chọnCầnHủy bỏ的Chứng từ": "Vui lòng chọn chứng từ cần hủy bỏ",
"Không thểKê多个Nhà thuốc的Thuốc": "Không thể kê thuốc của nhiều nhà thuốc",
"未找到可上传的Xét nghiệmPhiếu": "Không tìm thấy phiếu xét nghiệm có thể tải lên",
"Vui lòng chọn要Hủy的Đặt hẹnĐăng ký khám": "Vui lòng chọn đăng ký hẹn khám cần hủy",
"不允许Xem上次Khám bệnh中的内容": "Không được xem nội dung lần khám trước",
"Vui lòng chọn要Xóa的词条": "Vui lòng chọn từ cần xóa",
"Hiện tạiĐăng ký khám您的待Khám bệnhBệnh nhânĐãKhông có了！ ": "Hiện tại bệnh nhân chờ khám đã đăng ký của bạn đã hết!",
"找不到该Bệnh nhânĐã thu phí的动态心电Đề nghị!": "Không tìm thấy đề nghị điện tâm đồ động đã thu phí của bệnh nhân này!",
"所上传的远程动态心电Dữ liệuTênBắt buộc为：": "Tên dữ liệu điện tâm đồ động từ xa tải lên bắt buộc là:",
"您的Quyền无法Kê此Kháng sinhMục，Vui lòng xác nhận。": "Quyền của bạn không thể kê mục kháng sinh này, vui lòng xác nhận.",
}
# only add keys not already covered by tier 1 (avoid contradictions)
D = {k: v for k, v in NEW.items() if k not in D1}
D = {k: v for k, v in D.items() if len(k) >= 2 or k in ("[停]",)}
keys = sorted(D, key=len, reverse=True)
changed = 0; replaced = 0
for root, dirs, files in os.walk(W):
    dirs[:] = [d for d in dirs if d not in ('obj', 'bin')]
    for fn in files:
        if not (fn.endswith('.cshtml') or fn.endswith('.cs') or fn.endswith('.js')):
            continue
        p = os.path.join(root, fn)
        s = open(p, encoding='utf-8-sig', errors='ignore').read()
        parts = COMMENT.split(s)
        out = []; n = 0
        for i, part in enumerate(parts):
            if i % 2 == 1:
                out.append(part); continue
            plines = part.split('\n')
            nlines = []
            for ln in plines:
                if SKIP_LINE.search(ln):
                    nlines.append(ln); continue
                for k in keys:
                    if k in ln:
                        c = ln.count(k)
                        ln = ln.replace(k, D[k]); n += c
                nlines.append(ln)
            out.append('\n'.join(nlines))
        if n:
            open(p, 'w', encoding='utf-8').write(''.join(out))
            changed += 1; replaced += n
print("tier2 files changed:", changed, "| replacements:", replaced, "| dict size:", len(D))
