#!/usr/bin/env python3
"""One-off MRQC view/controller/enum translation: Chinese -> Vietnamese (with diacritics).
Scope: user-visible strings only. Skips pure-comment lines (//, ///, <!--, *).
Per-file dicts (longest key first) so shared substrings (e.g. 严重 inside 特别严重)
and same-word-different-meaning cases (质控评分) translate correctly per file.
Vendor proper nouns, identifiers, URLs, filenames (开始/中间/结束.png) untouched.
"""
import re, sys

WEB = "/home/hatch/workspace/openhis-core/Newtouch.MRQC.Web"
INFRA = "/home/hatch/workspace/openhis-core/Newtouch.MRQC.Infrastructure"

def tr(path, mapping, skip_comment_lines=True):
    with open(path, encoding="utf-8") as f:
        lines = f.readlines()
    keys = sorted(mapping, key=len, reverse=True)
    total = 0
    out = []
    for line in lines:
        s = line.strip()
        if skip_comment_lines and (s.startswith("//") or s.startswith("///")
                                   or s.startswith("<!--") or s.startswith("*")):
            out.append(line)
            continue
        new = line
        for k in keys:
            if k in new:
                n = new.count(k)
                new = new.replace(k, mapping[k])
                total += n
        out.append(new)
    with open(path, "w", encoding="utf-8") as f:
        f.writelines(out)
    print(f"{total:4d}  {path}")
    return total

grand = 0

# ---------------- controllers ----------------
grand += tr(f"{WEB}/Areas/QualityControlManage/Controllers/ApplyController.cs", {
    "审批失败，请刷新待审批列表": "Phê duyệt thất bại, vui lòng làm mới danh sách chờ phê duyệt",
    "审批成功": "Phê duyệt thành công",
})
grand += tr(f"{WEB}/Areas/QualityControlManage/Controllers/MedicalCenterController.cs", {
    "文档编辑器 DCSoft.Writer 在 .NET Core 版本中暂不支持":
        "Trình soạn thảo tài liệu DCSoft.Writer tạm thời chưa được hỗ trợ trong phiên bản .NET Core",
    "姓名:{0},住院号:{1},床号:{2}，科室:{3},医生:{4},的病历质控异常,病历类型:{5},病历文书:{6},反馈内容:{7},问题等级:{8},请及时处理,限期处理时间:{9}":
        "Họ tên:{0}, số nhập viện:{1}, số giường:{2}, khoa:{3}, bác sĩ:{4}, hồ sơ bệnh án có bất thường kiểm soát chất lượng, loại hồ sơ:{5}, văn bản hồ sơ:{6}, nội dung phản hồi:{7}, mức độ vấn đề:{8}, vui lòng xử lý kịp thời, thời hạn xử lý:{9}",
    "姓名:{xm},住院号:{jzh},床号:{ch}，科室:{ks},医生:{zyys},的病历质控异常,质控日期:{zkrq},病历类型:{zklx},病历文书:{hzwd},反馈内容:{fknr},问题等级:{wtdj},请及时处理,限期处理时间:{qxclsj}":
        "Họ tên:{xm}, số nhập viện:{jzh}, số giường:{ch}, khoa:{ks}, bác sĩ:{zyys}, hồ sơ bệnh án có bất thường kiểm soát chất lượng, ngày kiểm soát:{zkrq}, loại kiểm soát:{zklx}, văn bản hồ sơ:{hzwd}, nội dung phản hồi:{fknr}, mức độ vấn đề:{wtdj}, vui lòng xử lý kịp thời, thời hạn xử lý:{qxclsj}",
    "您有新的病历文书质控消息待查收": "Bạn có tin nhắn kiểm soát chất lượng văn bản hồ sơ mới cần tiếp nhận",
    "您有新的病历质控消息待查收": "Bạn có tin nhắn kiểm soát chất lượng hồ sơ mới cần tiếp nhận",
    "病历文书质控组消息": "Tin nhắn nhóm kiểm soát chất lượng văn bản hồ sơ",
    "质控系统病历文书专属": "Dành riêng cho văn bản hồ sơ của hệ thống kiểm soát chất lượng",
    "质控消息发送成功": "Gửi tin nhắn kiểm soát chất lượng thành công",
    "发送消息失败：": "Gửi tin nhắn thất bại: ",
})

# ---------------- root views ----------------
grand += tr(f"{WEB}/Views/Home/Index.cshtml", {
    "MRQC 质控系统 (ASP.NET Core 试点)": "MRQC Hệ thống kiểm soát chất lượng (bản thử nghiệm ASP.NET Core)",
    "MRQC 质控系统 — ASP.NET Core 6.0 试点版": "MRQC Hệ thống kiểm soát chất lượng — Bản thử nghiệm ASP.NET Core 6.0",
    "本页为迁移试点占位首页（原门户框架页依赖 Framework 专属组件，试点暂以简单导航页代替）。":
        "Trang này là trang chủ tạm thời của bản thử nghiệm di chuyển (trang khung cổng thông tin gốc phụ thuộc vào các thành phần chuyên dụng của Framework, bản thử nghiệm tạm dùng trang điều hướng đơn giản thay thế).",
    "质控病历申请（Apply，本地数据库验证页）": "Đơn đăng ký hồ sơ kiểm soát chất lượng (Apply, trang kiểm chứng cơ sở dữ liệu nội bộ)",
    "质控项目维护（SysItem）": "Bảo trì hạng mục kiểm soát chất lượng (SysItem)",
    "质控中心（MedicalCenter，文档编辑器暂不支持）": "Trung tâm kiểm soát chất lượng (MedicalCenter, trình soạn thảo tài liệu tạm thời chưa được hỗ trợ)",
    "岗位人员列表接口（JSON，Base 真实数据）": "Giao diện danh sách nhân sự theo vị trí (JSON, dữ liệu thực của Base)",
    "退出登录": "Đăng xuất",
})
grand += tr(f"{WEB}/Views/Home/MessageBox.cshtml", {
    "消息盒子（试点占位页）。": "Hộp tin nhắn (trang tạm thời của bản thử nghiệm).",
    "消息盒子": "Hộp tin nhắn",
})
grand += tr(f"{WEB}/Views/Login/Index.cshtml", {
    "您还在使用老掉牙的IE，正常使用系统前请升级您的浏览器到 IE8以上版本": "Bạn vẫn đang dùng trình duyệt IE đã cũ, vui lòng nâng cấp trình duyệt lên phiên bản IE8 trở lên trước khi sử dụng hệ thống bình thường",
    "系统登录已超时,请重新登录": "Phiên đăng nhập hệ thống đã hết hạn, vui lòng đăng nhập lại",
    "该账号已在其他地方登录，您已被迫下线": "Tài khoản này đã đăng nhập ở nơi khác, bạn đã bị đăng xuất",
    "系统未知错误,请重新登录": "Hệ thống gặp lỗi không xác định, vui lòng đăng nhập lại",
    "请输入用户名/手机号/邮箱。": "Vui lòng nhập tên đăng nhập / số điện thoại / email.",
    "登录成功，正在跳转...": "Đăng nhập thành công, đang chuyển hướng...",
    "强烈建议您更改换浏览器：": "Chúng tôi đặc biệt khuyến nghị bạn đổi trình duyệt:",
    "请输入登录密码。": "Vui lòng nhập mật khẩu đăng nhập.",
    "请输入验证码。": "Vui lòng nhập mã xác minh.",
    "MRQC 质控系统": "MRQC Hệ thống kiểm soát chất lượng",
    # NOTE: shortened to one word after screenshotting: the full phrase wrapped
    # onto two lines in this view's fixed-height tab and overlapped the input.
    "欢迎登录": "Chào mừng",
    "登录账号": "Tài khoản đăng nhập",
    "登录密码": "Mật khẩu đăng nhập",
    "点击升级": "Nhấn để nâng cấp",
    "谷歌 Chrome": "Google Chrome",
    "验证码": "Mã xác minh",
    "换一张": "Đổi mã khác",
    "登录": "Đăng nhập",
})
grand += tr(f"{WEB}/Views/Shared/Error.cshtml", {
    "程序员开小差了，请联系管理员": "Đã xảy ra sự cố, vui lòng liên hệ quản trị viên",
    "查看异常详情": "Xem chi tiết ngoại lệ",
    "异常详情": "Chi tiết ngoại lệ",
    "未知异常": "Ngoại lệ không xác định",
})

# ---------------- Apply ----------------
grand += tr(f"{WEB}/Areas/QualityControlManage/Views/Apply/Index.cshtml", {
    "请先选中需审核的申请": "Vui lòng chọn đơn đăng ký cần phê duyệt trước",
    "病历质控完成时间": "Thời gian hoàn thành kiểm soát chất lượng hồ sơ",
    "申请病历名称": "Tên hồ sơ đăng ký",
    "申请完成时间": "Thời gian hoàn thành đăng ký",
    "质控病历申请": "Đơn đăng ký hồ sơ kiểm soát chất lượng",
    "申请日期：": "Ngày đăng ký:",
    "申请科室": "Khoa đăng ký",
    "申请医师": "Bác sĩ đăng ký",
    "申请类型": "Loại đăng ký",
    "申请时间": "Thời gian đăng ký",
    "申请原因": "Lý do đăng ký",
    "申请状态": "Trạng thái đăng ký",
    "批准科室": "Khoa phê duyệt",
    "批准时间": "Thời gian phê duyệt",
    "入院时间": "Thời gian nhập viện",
    "出院时间": "Thời gian xuất viện",
    "筛选条件": "Điều kiện lọc",
    "未审批": "Chưa phê duyệt",
    "已审批": "Đã phê duyệt",
    "批准人": "Người phê duyệt",
    "审批失败：": "Phê duyệt thất bại: ",
    "床号": "Số giường",
    "姓名": "Họ tên",
    "全部": "Tất cả",
    "审核": "Phê duyệt",
    "查询": "Tra cứu",
})

# ---------------- MedicalCenter/Index ----------------
grand += tr(f"{WEB}/Areas/QualityControlManage/Views/MedicalCenter/Index.cshtml", {
    "请选中一种病历类型进行评分": "Vui lòng chọn một loại hồ sơ để chấm điểm",
    "病历质量终末评分": "Chấm điểm chất lượng hồ sơ cuối kỳ",
    "请选中一份病历文书": "Vui lòng chọn một văn bản hồ sơ",
    "反馈质控消息": "Phản hồi tin nhắn kiểm soát chất lượng",
    "主诊(住院医师)": "Bác sĩ chính (bác sĩ nội trú)",
    "最后执行时间": "Thời gian thực hiện cuối",
    "病历关系ID": "ID quan hệ hồ sơ",
    "(副)主任医师": "Bác sĩ (phó) chủ nhiệm",
    "(门诊)住院号": "Số (ngoại trú) nhập viện",
    "姓名/住院号": "Họ tên / Số nhập viện",
    "住院/就诊号：": "Số nhập viện / khám:",
    "入院起日期": "Từ ngày nhập viện",
    "入院止日期": "Đến ngày nhập viện",
    "出院起日期": "Từ ngày xuất viện",
    "出院止日期": "Đến ngày xuất viện",
    "就诊起日期": "Từ ngày khám",
    "就诊止日期": "Đến ngày khám",
    "入院医生": "Bác sĩ nhập viện",
    "申请医生": "Bác sĩ đăng ký",
    "停止医生": "Bác sĩ dừng",
    "执行科室": "Khoa thực hiện",
    "送检项目": "Hạng mục gửi xét nghiệm",
    "送检部位": "Vị trí gửi xét nghiệm",
    "申请单号": "Số đơn đăng ký",
    "申请日期": "Ngày đăng ký",
    "报告日期": "Ngày báo cáo",
    "门诊处方": "Đơn thuốc ngoại trú",
    "表单护理记录": "Phiếu ghi chép điều dưỡng",
    "在院病人": "Bệnh nhân đang nằm viện",
    "出院病人": "Bệnh nhân đã xuất viện",
    "检验报告": "Báo cáo xét nghiệm",
    "检查报告": "Báo cáo kiểm tra",
    "质控中心": "Trung tâm kiểm soát chất lượng",
    "筛选条件": "Điều kiện lọc",
    "病人信息": "Thông tin bệnh nhân",
    "健康卡号": "Số thẻ sức khỏe",
    "费用类别": "Loại chi phí",
    "主治医师": "Bác sĩ điều trị",
    "出生日期": "Ngày sinh",
    "入院日期": "Ngày nhập viện",
    "出院日期": "Ngày xuất viện",
    "诊断类型": "Loại chẩn đoán",
    "诊断编码": "Mã chẩn đoán",
    "诊断名称": "Tên chẩn đoán",
    "病历名称": "Tên hồ sơ",
    "病历日期": "Ngày hồ sơ",
    "医嘱类别": "Loại y lệnh",
    "医嘱类型": "Kiểu y lệnh",
    "医嘱名称": "Tên y lệnh",
    "开始时间": "Thời gian bắt đầu",
    "结束时间": "Thời gian kết thúc",
    "停止时间": "Thời gian dừng",
    "联系电话": "Điện thoại liên hệ",
    "联系地址": "Địa chỉ liên hệ",
    "单位地址": "Địa chỉ đơn vị",
    "归档标志": "Cờ lưu trữ",
    "医疗证号": "Số chứng nhận y tế",
    "社保卡号": "Số thẻ bảo hiểm xã hội",
    "质控评分": "Chấm điểm kiểm soát chất lượng",
    "质控消息": "Tin nhắn kiểm soát chất lượng",
    "病案首页": "Trang đầu hồ sơ bệnh án",
    "查执行": "Tra cứu đã thực hiện",
    "查今天": "Tra cứu hôm nay",
    "查临时": "Tra cứu tạm thời",
    "查长期": "Tra cứu dài hạn",
    "录入日期": "Ngày nhập",
    "出生地": "Nơi sinh",
    "联系人": "Người liên hệ",
    "录入人": "Người nhập",
    "书写者": "Người viết",
    "身份证": "CMND/CCCD",
    "三测单": "Phiếu đo ba chỉ số",
    "诊断": "Chẩn đoán",
    "病历": "Hồ sơ",
    "医嘱": "Y lệnh",
    "门诊": "Khám ngoại trú",
    "处方": "Đơn thuốc",
    "剂量": "Liều lượng",
    "用法": "Cách dùng",
    "频次": "Tần suất",
    "同组": "Cùng nhóm",
    "医生": "Bác sĩ",
    "科室": "Khoa",
    "床号": "Số giường",
    "姓名": "Họ tên",
    "性别": "Giới tính",
    "年龄": "Tuổi",
    "状态": "Trạng thái",
    "全部": "Tất cả",
})

# ---------------- MedicalCenter/MessageQuery ----------------
grand += tr(f"{WEB}/Areas/QualityControlManage/Views/MedicalCenter/MessageQuery.cshtml", {
    "质控消息查询": "Tra cứu tin nhắn kiểm soát chất lượng",
    "消息组业务类型": "Loại nghiệp vụ nhóm tin nhắn",
    "消息通知范围": "Phạm vi thông báo tin nhắn",
    "队列执行类型": "Loại thực thi hàng đợi",
    "内容关联数据": "Dữ liệu liên quan nội dung",
    "收件人类型": "Loại người nhận",
    "消息状态": "Trạng thái tin nhắn",
    "执行表达式": "Biểu thức thực thi",
    "筛选条件": "Điều kiện lọc",
    "详细内容": "Nội dung chi tiết",
    "发送时间": "Thời gian gửi",
    "发送人": "Người gửi",
    "收信人": "Người nhận",
    "日期：": "Ngày:",
    "状态：": "Trạng thái:",
    "主题": "Chủ đề",
    "全部": "Tất cả",
    "查询": "Tra cứu",
})

# ---------------- MedicalCenter/PreView ----------------
grand += tr(f"{WEB}/Areas/QualityControlManage/Views/MedicalCenter/PreView.cshtml", {
    "病历预览": "Xem trước hồ sơ",
})

# ---------------- MedicalCenter/QcMessageManage ----------------
grand += tr(f"{WEB}/Areas/QualityControlManage/Views/MedicalCenter/QcMessageManage.cshtml", {
    "未处理环节质控消息查询": "Tra cứu tin nhắn kiểm soát chất lượng theo khâu chưa xử lý",
    "环节质控消息发送": "Gửi tin nhắn kiểm soát chất lượng theo khâu",
    "反馈环节质控信息": "Thông tin kiểm soát chất lượng khâu phản hồi",
    "输入质控名称/简拼按回车键盘": "Nhập tên kiểm soát chất lượng / mã giản lược rồi nhấn Enter",
    "质控消息": "Tin nhắn kiểm soát chất lượng",
    "模板质控项目": "Hạng mục kiểm soát chất lượng theo mẫu",
    "通用质控项目": "Hạng mục kiểm soát chất lượng chung",
    "限期处理时间：": "Thời hạn xử lý:",
    "质控名称": "Tên kiểm soát chất lượng",
    "质控编码": "Mã kiểm soát chất lượng",
    "质控类型": "Loại kiểm soát chất lượng",
    "质控日期": "Ngày kiểm soát chất lượng",
    "经管医生": "Bác sĩ quản lý",
    "病人科室：": "Khoa bệnh nhân:",
    "住院医生：": "Bác sĩ nội trú:",
    "患者文档：": "Tài liệu bệnh nhân:",
    "反馈内容：": "Nội dung phản hồi:",
    "问题等级：": "Mức độ vấn đề:",
    "特别严重": "Đặc biệt nghiêm trọng",
    "限定日期": "Ngày giới hạn",
    "住院号": "Số nhập viện",
    "确定发送": "Xác nhận gửi",
    "姓名": "Họ tên",
    "床号": "Số giường",
    "科室": "Khoa",
    "严重": "Nghiêm trọng",
    "一般": "Bình thường",
    "事故": "Sự cố",
    "关闭": "Đóng",
})
# mojibake placeholder on the mbxmKeyword line (contains U+FFFD, not matched above)
p = f"{WEB}/Areas/QualityControlManage/Views/MedicalCenter/QcMessageManage.cshtml"
with open(p, encoding="utf-8") as f:
    t = f.read()
t2, n = re.subn(r'placeholder="输入质控名称/简拼按回[^"]*键盘"',
                'placeholder="Nhập tên kiểm soát chất lượng / mã giản lược rồi nhấn Enter"', t)
if n:
    with open(p, "w", encoding="utf-8") as f:
        f.write(t2)
    print(f"{n:4d}  {p} (mojibake placeholder)")
    grand += n

# ---------------- MedicalCenter/ScoreFrom ----------------
grand += tr(f"{WEB}/Areas/QualityControlManage/Views/MedicalCenter/ScoreFrom.cshtml", {
    "缺陷数：填写数值必须为0~20之间": "Số lỗi: giá trị nhập phải trong khoảng 0~20",
    "数值为空，请确认。": "Giá trị trống, vui lòng xác nhận.",
    "质控评分": "Chấm điểm kiểm soát chất lượng",
    "评分项目": "Hạng mục chấm điểm",
    "评分说明": "Giải thích chấm điểm",
    "服务不可访问": "Không thể truy cập dịch vụ",
    "保存成功": "Lưu thành công",
    "缺陷数": "Số lỗi",
    "已审查": "Đã xem xét",
    "得分：": "Điểm số:",
    '"甲"': '"Hạng A"',
    '"乙"': '"Hạng B"',
    '"丙"': '"Hạng C"',
    "评分": "Chấm điểm",
    "保存": "Lưu",
    "关闭": "Đóng",
})

# ---------------- SysItem/Form ----------------
grand += tr(f"{WEB}/Areas/QualityControlManage/Views/SysItem/Form.cshtml", {
    "质控项目：": "Hạng mục kiểm soát chất lượng:",
    "质控名称：": "Tên kiểm soát chất lượng:",
    "质控评分：": "Điểm kiểm soát chất lượng:",
    "评分说明：": "Giải thích chấm điểm:",
    "保存成功": "Lưu thành công",
    "排序：": "Thứ tự:",
})

# ---------------- SysItem/Index ----------------
grand += tr(f"{WEB}/Areas/QualityControlManage/Views/SysItem/Index.cshtml", {
    "请选中一条要删除的质控明细": "Vui lòng chọn một chi tiết kiểm soát chất lượng cần xóa",
    "请选中一条质控项目": "Vui lòng chọn một hạng mục kiểm soát chất lượng",
    "请选中一条质控明细": "Vui lòng chọn một chi tiết kiểm soát chất lượng",
    '"是否删除"+seleRow.Name': '"Có xóa " + seleRow.Name + " không?"',
    "新增质控项": "Thêm hạng mục kiểm soát chất lượng mới",
    "修改质控项": "Sửa hạng mục kiểm soát chất lượng",
    "质控明细项": "Chi tiết hạng mục kiểm soát chất lượng",
    "质控项目": "Hạng mục kiểm soát chất lượng",
    "质控代码": "Mã kiểm soát chất lượng",
    "质控编码": "Mã kiểm soát chất lượng",
    "质控名称": "Tên kiểm soát chất lượng",
    "质控评分": "Điểm kiểm soát chất lượng",
    "评分说明": "Giải thích chấm điểm",
    "显示顺序": "Thứ tự hiển thị",
    "删除成功": "Xóa thành công",
    "新增": "Thêm mới",
    "修改": "Sửa",
    "删除": "Xóa",
})

# ---------------- enums served to the Apply page (Description attrs only) ----------------
# Robust: line-range between the two enum declarations and ApplyStatusEnum's closing brace.
# (Do NOT anchor on member text: 'th = 2' also occurs in EnumRecordStu and a naive
# slice once duplicated ~160 lines of this file. That corruption was repaired and the
# file verified byte-identical to the original source before this section was rewritten.)
p = f"{INFRA}/Infrastructure/EnumDB.cs"
with open(p, encoding="utf-8") as f:
    elines = f.readlines()
i0 = next(i for i, l in enumerate(elines) if "public enum ApplyTypeEnum" in l)
i1 = next(i for i, l in enumerate(elines) if "public enum ApplyStatusEnum" in l)
i2 = next(i for i in range(i1, len(elines)) if elines[i].rstrip() == "    }")
emap = {"新建病历": "Hồ sơ mới", "修改病历": "Sửa hồ sơ",
        "未批准": "Chưa phê duyệt", "已审批": "Đã phê duyệt", "退回": "Trả lại"}
n = 0
for i in range(i0, i2 + 1):
    for k, v in emap.items():
        pat = f'[Description("{k}")]'
        if pat in elines[i]:
            elines[i] = elines[i].replace(pat, f'[Description("{v}")]')
            n += 1
with open(p, "w", encoding="utf-8") as f:
    f.writelines(elines)
print(f"{n:4d}  {p} (ApplyType/ApplyStatus Description attrs)")
grand += n

print(f"TOTAL view/controller/enum replacements: {grand}")
