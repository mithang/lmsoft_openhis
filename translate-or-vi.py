#!/usr/bin/env python3
"""OR Vietnamese translation pass (ported views + controllers only).
Rules: .cs -> translate inside double-quoted string literals on non-comment
lines only (doc comments untouched). .cshtml -> translate visible text,
skipping @* *@, <!-- -->, // and /* */ comments. Vendor proper nouns,
font names, URLs/IDs and demo person names are NOT in the dictionary.
Longest-first matching. On a line where a replacement happened and no CJK
remains, full-width punctuation is normalized to ASCII.
"""
import re
import sys
from pathlib import Path

D = {
    # full sentences / prompts (longest first handled by sort)
    "数量为大于0的整数": "Số lượng phải là số nguyên lớn hơn 0",
    "注：请确认是否撤销此项手术申请？": "Lưu ý: Vui lòng xác nhận có hủy đơn phẫu thuật này không?",
    "请确认是否撤销此项手术申请": "Vui lòng xác nhận có hủy đơn phẫu thuật này không",
    "手术申请时间不得早于当天": "Thời gian đăng ký phẫu thuật không được sớm hơn ngày hiện tại",
    "请填写手术开始时间与结束时间": "Vui lòng nhập thời gian bắt đầu và kết thúc phẫu thuật",
    "请仔细核对以上手术安排": "Vui lòng kiểm tra kỹ lịch phẫu thuật trên",
    "至少选择一条收费项目": "Vui lòng chọn ít nhất một hạng mục thu phí",
    "患者信息不可为空": "Thông tin bệnh nhân không được để trống",
    "请输入项目名称或拼音": "Vui lòng nhập tên hạng mục hoặc phiên âm",
    "请输入要查询关键字": "Vui lòng nhập từ khóa cần tra cứu",
    "请输入模板名称": "Vui lòng nhập tên mẫu",
    "请输入模板编码": "Vui lòng nhập mã mẫu",
    "请输入登录密码": "Vui lòng nhập mật khẩu đăng nhập",
    "请输入用户名": "Vui lòng nhập tên đăng nhập",
    "请输入验证码": "Vui lòng nhập mã xác minh",
    "请选择时间范围": "Vui lòng chọn khoảng thời gian",
    "请选中一条排班信息": "Vui lòng chọn một lịch phẫu thuật",
    "请选中一条信息": "Vui lòng chọn một bản ghi",
    "请填写手术时间": "Vui lòng nhập thời gian phẫu thuật",
    "请填写完整信息": "Vui lòng nhập đầy đủ thông tin",
    "您还在使用老掉牙的": "Bạn vẫn đang dùng trình duyệt",
    "正常使用系统前请升级您的浏览器到": "Trước khi dùng hệ thống bình thường, vui lòng nâng cấp trình duyệt lên",
    "以上版本": "trở lên",
    "点击升级": "Nhấn để nâng cấp",
    "强烈建议您更改换浏览器": "Khuyến nghị bạn đổi trình duyệt",
    "系统登录已超时": "Phiên đăng nhập đã hết hạn",
    "该账号已在其他地方登录": "Tài khoản đã đăng nhập ở nơi khác",
    "您已被迫下线": "Bạn đã bị đăng xuất",
    "系统未知错误": "Lỗi hệ thống không xác định",
    "请重新登录": "Vui lòng đăng nhập lại",
    "未找到有效手术申请": "Không tìm thấy đơn phẫu thuật hợp lệ",
    "撤销申请操作成功": "Hủy đơn thành công",
    "正在请求报表页面": "Đang yêu cầu trang báo cáo",
    "取消安排失败": "Hủy lịch thất bại",
    "不能重复添加": "Không được thêm trùng lặp",
    # spaced button labels
    "详 情": "Chi tiết", "查 询": "Tra cứu", "排 班": "Xếp lịch",
    "登 记": "Đăng ký", "作 废": "Hủy bỏ", "创 建": "Tạo",
    "日 期": "Ngày", "描 述": "Mô tả",
    # newly found visible terms (pass 3)
    "新建": "Tạo mới",
    "排班详情": "Chi tiết xếp lịch", "排班": "Xếp lịch",
    "登记详情": "Chi tiết đăng ký", "登记": "Đăng ký",
    "撤销手术申请详情": "Chi tiết hủy đơn phẫu thuật",
    "一病区": "Khoa 1", "主键": "Khóa chính", "有效": "Hiệu lực",
    "谷歌": "Google", "患者详情": "Chi tiết bệnh nhân",
    # demo placeholders (JS-overwritten at runtime, but visible on first paint)
    "张三": "Nguyễn Văn A", "阑尾炎": "Viêm ruột thừa",
    "阑尾切除术": "Cắt ruột thừa",
    "护理级别": "Cấp độ chăm sóc",
    # login page
    "欢迎登录": "Chào mừng đăng nhập", "登录账号": "Tài khoản đăng nhập",
    "登录密码": "Mật khẩu đăng nhập", "验证码": "Mã xác minh",
    "换一张": "Đổi mã khác", "登录成功": "Đăng nhập thành công",
    "正在跳转": "Đang chuyển hướng", "手机号": "Số điện thoại",
    "邮箱": "Email",
    # surgery domain
    "手术申请编号": "Số đơn đăng ký phẫu thuật",
    "手术申请单号": "Số đơn đăng ký phẫu thuật",
    "手术申请号": "Số đơn đăng ký phẫu thuật",
    "手术申请信息": "Thông tin đơn đăng ký phẫu thuật",
    "手术申请详情": "Chi tiết đơn đăng ký phẫu thuật",
    "手术申请明细": "Chi tiết đơn đăng ký phẫu thuật",
    "手术申请时间": "Thời gian đăng ký phẫu thuật",
    "手术申请列表": "Danh sách đơn đăng ký phẫu thuật",
    "手术排班信息": "Thông tin xếp lịch phẫu thuật",
    "手术排班列表": "Danh sách xếp lịch phẫu thuật",
    "手术排班记录": "Bản ghi xếp lịch phẫu thuật",
    "手术登记本": "Sổ đăng ký phẫu thuật",
    "手术登记首页": "Trang chính đăng ký phẫu thuật",
    "术后登记首页": "Trang chính đăng ký sau mổ",
    "手术开始时间": "Thời gian bắt đầu phẫu thuật",
    "手术结束时间": "Thời gian kết thúc phẫu thuật",
    "手术收费项目": "Hạng mục thu phí phẫu thuật",
    "手术收费模板": "Mẫu thu phí phẫu thuật",
    "手术患者信息": "Thông tin bệnh nhân phẫu thuật",
    "手术详细信息": "Thông tin chi tiết phẫu thuật",
    "手术费用查询": "Tra cứu chi phí phẫu thuật",
    "手术名称列表": "Danh sách tên phẫu thuật",
    "手术名称浮层": "Lớp tên phẫu thuật",
    "手术人员详细信息": "Chi tiết nhân viên phẫu thuật",
    "手术人员": "Nhân viên phẫu thuật",
    "手术通知单": "Phiếu thông báo phẫu thuật",
    "手术排班": "Xếp lịch phẫu thuật",
    "手术登记": "Đăng ký phẫu thuật",
    "手术申请": "Đơn đăng ký phẫu thuật",
    "手术安排": "Sắp xếp phẫu thuật",
    "手术房间": "Phòng phẫu thuật",
    "手术名称": "Tên phẫu thuật",
    "手术代码": "Mã phẫu thuật",
    "手术时间": "Thời gian phẫu thuật",
    "手术日期": "Ngày phẫu thuật",
    "手术状态": "Trạng thái phẫu thuật",
    "手术级别": "Cấp độ phẫu thuật",
    "手术部位": "Vị trí phẫu thuật",
    "手术数量": "Số lượng phẫu thuật",
    "手术列表": "Danh sách phẫu thuật",
    "手术记账": "Ghi sổ phẫu thuật",
    "手术室": "Phòng mổ", "手术间": "Phòng phẫu thuật",
    "手术": "Phẫu thuật",
    # anesthesia
    "麻醉方式详细信息": "Chi tiết phương pháp gây mê",
    "新增麻醉方式": "Thêm phương pháp gây mê",
    "修改麻醉方式": "Sửa phương pháp gây mê",
    "麻醉方式": "Phương pháp gây mê",
    "麻醉代码": "Mã gây mê", "麻醉名称": "Tên gây mê",
    "麻醉医师": "Bác sĩ gây mê",
    "全身麻醉": "Gây mê toàn thân", "局部麻醉": "Gây mê cục bộ",
    "针刺麻醉": "Gây mê châm cứu", "复合麻醉": "Gây mê phối hợp",
    # notch grade / rooms / staff
    "切口等级详细信息": "Chi tiết cấp độ vết mổ",
    "新增切口等级": "Thêm cấp độ vết mổ",
    "修改切口等级": "Sửa cấp độ vết mổ",
    "切口等级": "Cấp độ vết mổ",
    "新增房间": "Thêm phòng", "修改房间": "Sửa phòng",
    "房间代码": "Mã phòng", "房间名称": "Tên phòng",
    "新增手术人员": "Thêm nhân viên phẫu thuật",
    "修改手术人员": "Sửa nhân viên phẫu thuật",
    "新增手术": "Thêm phẫu thuật", "修改手术": "Sửa phẫu thuật",
    "医生人员列表": "Danh sách nhân viên y tế",
    "医护人员安排": "Sắp xếp nhân viên y tế",
    "主刀医生": "Bác sĩ mổ chính",
    "巡回护士": "Y tá lưu động", "洗手护士": "Y tá rửa tay",
    "主手术浮层": "Lớp phẫu thuật chính",
    "主手术": "Phẫu thuật chính",
    "一助": "Phụ mổ 1", "二助": "Phụ mổ 2",
    # patient / admission
    "患者姓名": "Tên bệnh nhân", "患者信息": "Thông tin bệnh nhân",
    "患者列表": "Danh sách bệnh nhân", "患者详情": "Chi tiết bệnh nhân",
    "住院患者": "Bệnh nhân nội trú", "住院号": "Số nhập viện",
    "入院诊断": "Chẩn đoán nhập viện",
    "术后诊断": "Chẩn đoán sau mổ",
    "入院日期": "Ngày nhập viện", "出院日期": "Ngày xuất viện",
    "拟手术名称": "Tên phẫu thuật dự kiến",
    "申请主刀医生": "Bác sĩ mổ chính được đăng ký",
    "申请手术时间": "Thời gian phẫu thuật được đăng ký",
    "申请手术名称": "Tên phẫu thuật được đăng ký",
    "申请手术": "Đăng ký phẫu thuật",
    "申请状态": "Trạng thái đơn",
    "申请编号": "Số đơn",
    "申请时间": "Thời gian đăng ký",
    "安排时间": "Thời gian sắp xếp",
    "申请": "Đăng ký",
    # charge template
    "新增手术收费模板": "Thêm mẫu thu phí phẫu thuật",
    "修改手术收费模板": "Sửa mẫu thu phí phẫu thuật",
    "收费模板名称": "Tên mẫu thu phí",
    "收费项目选择浮层": "Lớp chọn hạng mục thu phí",
    "收费项目名称": "Tên hạng mục thu phí",
    "收费项目科室": "Khoa hạng mục thu phí",
    "收费项目": "Hạng mục thu phí",
    "收费大类": "Nhóm thu phí lớn",
    "门诊住院标志": "Dấu hiệu ngoại trú/nội trú",
    "模板名称": "Tên mẫu", "模板编码": "Mã mẫu",
    "添加项目": "Thêm hạng mục",
    "添加手术辅助": "Thêm phẫu thuật phụ",
    "删除手术辅助": "Xóa phẫu thuật phụ",
    "清空手术辅助": "Xóa phẫu thuật phụ",
    "清空手术名称": "Xóa tên phẫu thuật",
    "选择诊断": "Chọn chẩn đoán",
    "总金额": "Tổng số tiền", "支付方式": "Phương thức thanh toán",
    "权限科室": "Khoa có quyền",
    # status values
    "已取消申请": "Đã hủy đơn", "已取消": "Đã hủy",
    "已作废": "Đã hủy bỏ", "登记作废": "Hủy đăng ký",
    "默认待登记": "Mặc định chờ đăng ký",
    "已申请": "Đã gửi đơn", "已审核": "Đã duyệt",
    "待审核": "Chờ duyệt", "已排班": "Đã xếp lịch",
    "待排班": "Chờ xếp lịch", "已登记": "Đã đăng ký",
    "待登记": "Chờ đăng ký", "未申请": "Chưa đăng ký",
    "是否隔离": "Cách ly", "有无菌": "Vô khuẩn",
    "自费": "Tự chi trả", "通用": "Chung",
    "门诊": "Ngoại trú", "住院": "Nội trú",
    "男": "Nam", "女": "Nữ", "是": "Có", "否": "Không",
    # actions / generic
    "操作成功": "Thao tác thành công",
    "删除成功": "Xóa thành công", "取消成功": "Hủy thành công",
    "申请成功": "Gửi đơn thành công", "申请失败": "Gửi đơn thất bại",
    "信息不完整": "Thông tin chưa đầy đủ",
    "确定要删除吗": "Bạn có chắc muốn xóa?",
    "确定要取消吗": "Bạn có chắc muốn hủy?",
    "提交申请信息": "Gửi thông tin đơn",
    "提交申请": "Gửi đơn", "保存排班": "Lưu lịch",
    "保存登记": "Lưu đăng ký", "保存操作": "Lưu thao tác",
    "取消排班": "Hủy lịch", "取消登记": "Hủy đăng ký",
    "取消申请": "Hủy đơn", "撤销申请": "Hủy đơn",
    "打印通知单": "In phiếu thông báo",
    "刷新列表": "Làm mới danh sách",
    "重新载入": "Tải lại", "重置": "Đặt lại", "返回": "Quay lại",
    "清空选中行": "Xóa các dòng đã chọn",
    "查看手术申请详情": "Xem chi tiết đơn đăng ký phẫu thuật",
    "撤销手术申请详情": "Hủy chi tiết đơn phẫu thuật",
    "请选择": "Vui lòng chọn", "全部病区": "Tất cả khoa phòng",
    "选择病区": "Chọn khoa phòng", "全部": "Tất cả",
    "新增": "Thêm mới", "修改": "Sửa", "删除": "Xóa",
    "查看": "Xem", "详情": "Chi tiết", "查询": "Tra cứu",
    "检索": "Tìm kiếm", "添加": "Thêm",
    "状态异常": "Trạng thái bất thường",
    "状态": "Trạng thái", "台次": "Ca mổ",
    "姓名": "Họ tên", "性别": "Giới tính", "年龄": "Tuổi",
    "病区": "Khoa phòng", "床号": "Số giường", "科室": "Khoa",
    "位置": "Vị trí", "工号": "Mã nhân viên", "代码": "Mã",
    "名称": "Tên", "诊断": "Chẩn đoán", "病情": "Tình trạng bệnh",
    "特殊说明": "Ghi chú đặc biệt",
    "登记号": "Số đăng ký", "登记状态": "Trạng thái đăng ký",
    "数量": "Số lượng", "选项": "Tùy chọn", "单位": "Đơn vị",
    "部位": "Vị trí", "拼音": "Phiên âm", "助记码": "Mã gợi nhớ",
    "创建时间": "Thời gian tạo", "创建人": "Người tạo",
    "基本信息": "Thông tin cơ bản", "数据": "Dữ liệu",
    "未知": "Không xác định", "测试数据": "Dữ liệu kiểm tra",
    "测试时间": "Thời gian kiểm tra",
    "最近更新": "Cập nhật gần nhất",
    "输血量": "Lượng máu truyền", "失血量": "Lượng máu mất",
    "总入量": "Tổng lượng vào", "总出量": "Tổng lượng ra",
    "下拉列表": "Danh sách thả xuống",
    "表格头部设置": "Thiết lập tiêu đề bảng",
    "分页设置": "Thiết lập phân trang",
    "刷新按钮": "Nút làm mới",
    "显示表格头部": "Hiển thị tiêu đề bảng",
    "登录": "Đăng nhập",
    "注": "Lưu ý",
    "岁": " tuổi",
}

KEYS = sorted(D.keys(), key=len, reverse=True)
CJK = re.compile(r"[\u4e00-\u9fff]")
STR_LIT = re.compile(r'"(?:[^"\\]|\\.)*"')

PUNCT = [("。", "."), ("？", "?"), ("！", "!"), ("：", ":"), ("、", ","), ("．", "."), ("（", "("), ("）", ")")]


def apply_dict(text):
    changed = False
    for k in KEYS:
        if k in text:
            text = text.replace(k, D[k])
            changed = True
    return text, changed


def norm_punct(text):
    for a, b in PUNCT:
        text = text.replace(a, b)
    return text


def translate_cs(path):
    lines = path.read_text(encoding="utf-8").split("\n")
    out = []
    in_block = False
    changed_files = 0
    for line in lines:
        stripped = line.strip()
        if in_block:
            if "*/" in line:
                in_block = False
            out.append(line)
            continue
        if "/*" in line:
            # single-line /* ... */ comment: do NOT enter block mode
            if "*/" in line[line.index("/*") + 2:]:
                out.append(line)
                continue
            in_block = True
            out.append(line)
            continue
        if stripped.startswith("//"):
            out.append(line)
            continue
        # translate only inside string literals; keep code before // comment
        code, sep, comment = line.partition("//")
        # NB: // inside a string literal (e.g. URLs) would split wrongly;
        # OR controllers have no such literals with CJK, acceptable risk.
        def repl(m):
            lit = m.group(0)
            new, ch = apply_dict(lit)
            if ch and not CJK.search(new):
                new = norm_punct(new)
            return new
        new_code = STR_LIT.sub(repl, code)
        out.append(new_code + (sep + comment if sep else ""))
    new_text = "\n".join(out)
    if new_text != path.read_text(encoding="utf-8"):
        path.write_text(new_text, encoding="utf-8")
        return True
    return False


def translate_cshtml(path):
    text = path.read_text(encoding="utf-8")
    # split into segments: comment blocks stay untouched
    parts = re.split(r"(@\*.*?\*@|<!--.*?-->)", text, flags=re.DOTALL)
    out = []
    for part in parts:
        if part.startswith("@*") or part.startswith("<!--"):
            out.append(part)
            continue
        lines = part.split("\n")
        in_block = False
        new_lines = []
        for line in lines:
            if in_block:
                if "*/" in line:
                    in_block = False
                new_lines.append(line)
                continue
            if "/*" in line:
                # single-line /* ... */ comment: translate only outside the span
                m = re.search(r"/\*.*?\*/", line)
                if m:
                    before, after = line[:m.start()], line[m.end():]
                    nb, ch1 = apply_dict(before)
                    na, ch2 = apply_dict(after)
                    if (ch1 or ch2):
                        nl = nb + m.group(0) + na
                        if not CJK.search(nl):
                            nl = norm_punct(nl)
                        new_lines.append(nl)
                    else:
                        new_lines.append(line)
                    continue
                in_block = True
                new_lines.append(line)
                continue
            if line.strip().startswith("//"):
                new_lines.append(line)
                continue
            # strip trailing // comment (code part only gets translated;
            # a // inside a URL string is harmless: URLs contain no CJK)
            code, sep, comment = line.partition("//")
            new_code, ch = apply_dict(code)
            if ch and not CJK.search(new_code):
                new_code = norm_punct(new_code)
            new_lines.append(new_code + (sep + comment if sep else ""))
        out.append("\n".join(new_lines))
    new_text = "".join(out)
    if new_text != text:
        path.write_text(new_text, encoding="utf-8")
        return True
    return False


def main():
    root = Path(sys.argv[1])
    n_cs = n_html = 0
    for p in sorted(root.rglob("*.cs")):
        if "obj" in p.parts or "bin" in p.parts:
            continue
        if translate_cs(p):
            n_cs += 1
    for p in sorted(root.rglob("*.cshtml")):
        if translate_cshtml(p):
            n_html += 1
    print(f"translated files: {n_cs} .cs, {n_html} .cshtml")


if __name__ == "__main__":
    main()
