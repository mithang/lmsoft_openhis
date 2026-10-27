# -*- coding: utf-8 -*-
"""CIS Web translation sweep, tier 3: remaining fragments + repairs + freq>=2 literals.
Same comment protection; skip ==/!=/Equals/SQL/log/URL lines."""
import os, re

W = "/home/hatch/workspace/openhis-core/Newtouch.HIS.CIS/Newtouch.CIS.Web"
COMMENT = re.compile(r"(@\*.*?\*@|<!--.*?-->|(?<!:)//[^\n]*|/\*.*?\*/)", re.DOTALL)
SKIP_LINE = re.compile(r"(==|!=|\.Equals\(|SELECT\s|FROM\s|WHERE\s|INSERT\s+INTO|UPDATE\s+\w+\s+SET|DELETE\s+FROM|\bEXEC\b|Log\.|logger\.|Console\.Write|https?://)")


def load_dict(path, var):
    src = open(path, encoding='utf-8').read()
    dsrc = var + " = " + src.split(var + ' = ', 1)[1]
    dsrc = dsrc[:dsrc.index('\n}\n') + 3]
    ns = {}
    exec(dsrc, ns)
    return ns[var]


D = load_dict('/tmp/cis-tier1.py', 'D')
for k, v in load_dict('/tmp/cis-tier2.py', 'NEW').items():
    D.setdefault(k, v)

NEW3 = {
# repair phrases for verb-object compounds fragmented in tier 2
"Giao diệnGọiGọi": "Gọi giao diện", "Giao diệnGọi": "Gọi giao diện",
"签到": "Check-in",
# fragments
"医疗": "Y tế", "选项卡": "tab", "选项": "Tùy chọn", "词典": "Từ điển",
"登记": "Đăng ký", "等级": "Cấp độ", "预览": "Xem trước",
"指示": "Chỉ định", "请求": "Yêu cầu", "撤销": "Hủy bỏ",
"只能": "Chỉ có thể", "现有": "Hiện có", "同步": "Đồng bộ",
"留观": "Theo dõi", "院内": "Trong viện", "代煎": "Sắc thuốc hộ",
"目录": "Danh mục", "剂数": "Số thang", "附加": "Bổ sung",
"不允许": "Không được phép", "标签": "Nhãn", "测量": "Đo",
"该条": "Dòng này", "账户": "Tài khoản", "总额": "Tổng số tiền",
"途径": "Đường", "单号": "Số phiếu", "必选": "Bắt buộc chọn",
"发药": "Phát thuốc", "生成": "Tạo", "词条": "Từ",
"视图": "Chế độ xem", "本次": "Lần này", "首页": "Trang chủ",
"缓存": "Bộ nhớ đệm", "皮肤": "Da", "临床印象": "Ấn tượng lâm sàng",
"推送": "Đẩy", "行号": "Số dòng", "参考值": "Giá trị tham chiếu",
"签名": "Chữ ký", "过号": "Quá số", "医师": "Bác sĩ",
"材料费": "Phí vật liệu", "付数": "Số lần trả", "安排": "Sắp xếp",
"能量密度": "Mật độ năng lượng", "禁食": "Nhịn ăn", "自备": "Tự chuẩn bị",
"药师": "Dược sĩ", "不可访问": "Không thể truy cập", "请填写": "Vui lòng điền",
"陪伴者": "Người đi cùng", "转床": "Chuyển giường", "一种": "Một loại",
"留观登记": "Đăng ký theo dõi", "可退": "Có thể trả", "标注": "Đánh dấu",
"补录": "Bổ sung", "未勾选": "Chưa tick chọn", "已申请": "Đã đề nghị",
"系数": "Hệ số", "责任": "Trách nhiệm", "治愈": "Khỏi bệnh",
"未愈": "Chưa khỏi", "中草药": "Thuốc thảo dược", "不可": "Không thể",
"违规": "Vi phạm", "诊疗": "Khám chữa bệnh", "病例号": "Số bệnh án",
"流水号": "Số thứ tự", "方式": "Phương thức", "账户余额": "Số dư tài khoản",
"清单": "Danh sách", "特病": "Bệnh đặc biệt", "部位": "Vị trí",
"控制": "Kiểm soát", "病案": "Bệnh án", "内容": "Nội dung",
"药物": "Thuốc", "加急": "Khẩn", "剂型": "Dạng bào chế",
"病种": "Loại bệnh", "填写": "Điền", "地点": "Địa điểm",
"加载": "Tải", "所属": "Thuộc", "事件": "Sự kiện",
"单个": "Đơn lẻ", "摄入摄出测量": "Đo lượng vào ra", "测量方式": "Phương thức đo",
"口温": "Nhiệt độ miệng", "腋温": "Nhiệt độ nách", "肛温": "Nhiệt độ hậu môn",
"起搏器": "Máy tạo nhịp", "意识": "Ý thức", "物理降温": "Hạ nhiệt vật lý",
"管道": "Ống", "不是": "Không phải", "介绍": "Giới thiệu",
"批价总额": "Tổng tiền định giá", "零价总额": "Tổng tiền giá lẻ", "主证": "Chứng chính",
"菜单": "Menu", "日志": "Nhật ký", "签字": "Ký tên",
"生命体征": "Dấu hiệu sinh tồn", "中心": "Trung tâm", "危重级别": "Mức độ nguy kịch",
"简拼": "Viết tắt", "仅可": "Chỉ có thể", "页面还有": "Trang còn",
"仍需跳转": "vẫn cần chuyển", "书写": "Viết", "会诊": "Hội chẩn",
"集成": "Tích hợp", "请确保已": "Vui lòng đảm bảo đã", "拟收住": "Dự kiến nhận vào",
"严重": "Nghiêm trọng", "停用": "Ngừng sử dụng", "周岁": "Tuổi tròn",
"有值": "Có giá trị", "不可用": "Không khả dụng", "重复": "Trùng lặp",
"中有": "trong có", "或多个空值": "hoặc nhiều giá trị trống", "暂无": "Tạm thời không có",
"或已重复": "hoặc đã trùng lặp", "乳牙": "Răng sữa", "己出报告": "Đã có báo cáo",
"影像": "Hình ảnh", "压缩": "Nén", "命名": "Đặt tên",
"品牌": "Thương hiệu", "转归": "Kết quả điều trị", "商品名": "Tên thương mại",
# phrases / whole literals
"库房Tên": "Tên kho", "生厂Nhà sản xuất": "Nhà sản xuất",
"组<br>号": "Nhóm<br>Số", "Bác sĩ签字": "Chữ ký bác sĩ",
"Bác sĩ签名": "Chữ ký bác sĩ",
"Tập tinXuấtthất bại，请Quay lạiDanh sách页重试": "Xuất tập tin thất bại, vui lòng quay lại trang danh sách thử lại",
"（本次Khám bệnh）": "(Lần khám này)",
"Điều dưỡng站-Bệnh nhân中心": "Trạm điều dưỡng - Trung tâm bệnh nhân",
"已Kết thúcKhám bệnh仅可Xem": "Đã kết thúc khám bệnh, chỉ có thể xem",
"Báo cáo预览": "Xem trước báo cáo",
"Từ xa医疗Báo cáoTra cứu": "Tra cứu báo cáo y tế từ xa",
"Nhập việnThông báo单": "Phiếu thông báo nhập viện",
"】不可用，Vui lòng chọn lại": "] không khả dụng, vui lòng chọn lại",
"TrướcDuyệt违规Thông tin：": "Thông tin vi phạm duyệt trước:",
"Thêmthất bại或已重复": "Thêm thất bại hoặc đã trùng lặp",
"Duyệt中": "Đang duyệt", "不Thông qua": "Không thông qua",
"登记Nguyên nhân": "Nguyên nhân đăng ký", "登记人员": "Người đăng ký",
"登记Thời gian": "Thời gian đăng ký", "Vui lòng chọn待NhậpMục": "Vui lòng chọn mục chờ nhập",
"更新者": "Người cập nhật", "床卡模式": "Chế độ thẻ giường",
"Danh sách模式": "Chế độ danh sách", "最少1位字符": "Ít nhất 1 ký tự",
"Khu bệnh未Chọn": "Khu bệnh chưa chọn",
"Đơn lẻĐơn thuốc下Chi tiết或Bộ mẫu内Chi tiết không được trùng lặp": "Chi tiết trong đơn thuốc lẻ hoặc chi tiết trong bộ mẫu không được trùng lặp",
"] Số lượngLớn hơnTồn kho量，Tồn kho量为[": "] số lượng lớn hơn lượng tồn kho, lượng tồn kho là [",
"]，KêSố lượng为[": "], số lượng kê là [",
"未Đã chọn": "Chưa chọn", "Số đơn thuốc为": "Số đơn thuốc là",
"Chọn组套": "Chọn bộ mẫu", "Chọn部位": "Chọn vị trí",
"拼音": "Pinyin", "Suất ăn指示": "Chỉ định suất ăn",
"Thiếu suất ăn指示": "Thiếu chỉ định suất ăn",
"Thiếu能量密度": "Thiếu mật độ năng lượng",
"无DừngThời gian": "Không có thời gian dừng",
"XóaTrướcDuyệt内容Giao diệnGọithất bại:": "Xóa nội dung duyệt trước gọi giao diện thất bại:",
"Y lệnhHủy bỏ后，请及时联系Điều dưỡngXử lý已GửiThuốc！该条Y lệnh已发药或者已Gửi，Có hay khôngXác nhậnHủy bỏ": "[Lưu ý] Sau khi hủy bỏ y lệnh, vui lòng liên hệ điều dưỡng xử lý thuốc đã gửi ngay! Y lệnh này đã phát thuốc hoặc đã gửi, có xác nhận hủy bỏ không",
"该Thuốc无Tồn kho，不Tùy chọn": "Thuốc này không có tồn kho, không thể chọn",
"该ThuốcVô hiệu hóaTrạng thái，不Tùy chọn": "Thuốc này ở trạng thái vô hiệu hóa, không thể chọn",
"病案DuyệtGiao diệnGọiGọithất bại:": "Gọi giao diện duyệt bệnh án thất bại:",
"医疗Loại": "Loại y tế",
"未ChọnMục/Thuốc，Có tiếp tục không？": "Chưa chọn mục/thuốc, có tiếp tục không?",
"Có hay khôngHủy bỏ/Dừng该Y lệnh？": "Có hủy bỏ/dừng y lệnh này không?",
"Thuốc商品名": "Tên thương mại thuốc", "Thuốc剂型": "Dạng bào chế thuốc",
"该Chi phí已Thực hiện，不允许Sửa。": "Chi phí này đã thực hiện, không được phép sửa.",
"Đơn giá为0，Xác nhậnCó hay khôngSửa": "Đơn giá bằng 0, xác nhận có sửa không",
"Không thể对此Chi tiết做Sửa": "Không thể sửa chi tiết này",
"F|该Giường bệnh已被占用": "F|Giường bệnh này đã có người",
"Số nhập viện为": "Số nhập viện là",
"PacsKiểm traPhiếu入参：{0}，出参Kết quả：{1}": "Tham số vào phiếu kiểm tra Pacs: {0}, Kết quả ra: {1}",
"请Đã chọnmộtTest daMục": "Vui lòng chọn một mục test da",
"Đã做过Test da，禁止重复Thao tác": "Đã làm test da, cấm thao tác trùng lặp",
"Giường bệnh等级": "Cấp độ giường bệnh",
"Số lượng不得Vượt quáTồn khoTổng lượng": "Số lượng không được vượt quá tổng lượng tồn kho",
"Xuất kho单号không được để trống": "Số phiếu xuất kho không được để trống",
"Vật tư必选": "Vật tư bắt buộc chọn", "新Mục thu phí": "Mục thu phí mới",
"Chuyển khu待入": "Chờ vào chuyển khu",
"Xác nhận要HủyĐăng ký vào khu吗？": "Xác nhận muốn hủy đăng ký vào khu?",
"Bệnh nhânVào khu中，Vui lòng đợi trong giây lát...": "Bệnh nhân đang vào khu, vui lòng đợi trong giây lát...",
"Xác nhận转床？": "Xác nhận chuyển giường?",
"Bệnh nhân转床中，Vui lòng đợi trong giây lát...": "Bệnh nhân đang chuyển giường, vui lòng đợi trong giây lát...",
"转床thành công！": "Chuyển giường thành công!",
"Xác nhận要Thu hồi吗？": "Xác nhận muốn thu hồi?",
"InNgàyVui lòng chọn同一天": "In theo ngày, vui lòng chọn cùng một ngày",
"Vui lòng chọn一种Y lệnhLoại（Dài hạn/Tạm thời）": "Vui lòng chọn một loại y lệnh (dài hạn/tạm thời)",
"Hóa đơn号": "Số hóa đơn", "Test da标识": "Dấu hiệu test da",
"留观Dấu hiệu": "Dấu hiệu theo dõi",
"Vui lòng chọn同一组Đơn thuốcTruyền dịchThuốcTiến hànhIn": "Vui lòng chọn cùng một nhóm đơn thuốc truyền dịch để in",
"还Không có填写登记Nguyên nhân，Không thểTiến hànhLưu": "Chưa điền nguyên nhân đăng ký, không thể lưu",
"发药Số lượng": "Số lượng phát thuốc", "可退Số lượng": "Số lượng có thể trả",
"Xác nhận对Hiện tạiĐã chọnThuốcĐề nghịTrả thuốc？": "Xác nhận đề nghị trả thuốc đã chọn hiện tại?",
"）退Số lượngđể trống，Vui lòng xác nhận。": ") số lượng trả để trống, vui lòng xác nhận.",
"退Số lượngLớn hơn可退Số lượng，Vui lòng xác nhận<br/>": "Số lượng trả lớn hơn số lượng có thể trả, vui lòng xác nhận<br/>",
"Trạng thái标注<br />(时)": "Đánh dấu trạng thái<br />(giờ)",
"Trạng thái标注<br />(分)": "Đánh dấu trạng thái<br />(phút)",
"TrướcDuyệt": "Duyệt trước", "Lý thuyết数": "Số lý thuyết",
"InĐiều trị单": "In phiếu điều trị", "InXét nghiệm单": "In phiếu xét nghiệm",
"InKiểm tra单": "In phiếu kiểm tra", "Đóng会议": "Đóng cuộc họp",
"Vui lòng chọn待NhậpMục": "Vui lòng chọn mục chờ nhập",
}
D3 = {k: v for k, v in NEW3.items() if k not in D and len(k) >= 2}
keys = sorted(D3, key=len, reverse=True)
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
                        ln = ln.replace(k, D3[k]); n += c
                nlines.append(ln)
            out.append('\n'.join(nlines))
        if n:
            open(p, 'w', encoding='utf-8').write(''.join(out))
            changed += 1; replaced += n
print("tier3 files changed:", changed, "| replacements:", replaced, "| dict size:", len(D3))
