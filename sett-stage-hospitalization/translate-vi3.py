#!/usr/bin/env python3
"""Pass 3: direct fixes for mangled visible strings + global fragment dict.
Re-protects the 3 server/DB-sensitive literals."""
import glob, os

BASE = "/home/hatch/workspace/openhis-core/sett-stage-hospitalization"
FILES = (glob.glob(BASE + "/Newtouch.Sett.Web/Areas/HospitalizationManage/Views/*/*.cshtml")
         + glob.glob(BASE + "/Newtouch.Sett.Web/Areas/HospitalizationManage/Controllers/*.cs")
         + [BASE + "/Newtouch.Sett.Infrastructure/Domain/Enums.cs"])

PROTECT = [
    ("HosAccountDeposit.cshtml", "当前收据号已使用！", "@@P_RECEIPT@@"),
    ("SettConfirmForm.cshtml", 'brxzmc == "自费"', 'brxzmc == "@@P_ZIFEI@@"'),
    ("DischargeSettleController.cs", '"门诊退费"', '"@@P_MZTF@@"'),
]

# exact-string direct fixes (applied before the global dict)
DIRECT = [
    ('"đã执Dòng"', '"Số lần đã thực hiện"'),
    ('"今Ngàyđã执Dòng"', '"Số lần đã thực hiện hôm nay"'),
    ('F8Text = "执Dòng"', 'F8Text = "Thực hiện"'),
    ('"Bệnh nhân人数："', '"Số bệnh nhân："'),
    ('còn lạiSố lần提醒：', 'Nhắc nhở số lần còn lại：'),
    ('"Xem执DòngChi tiết"', '"Xem chi tiết số lần thực hiện"'),
    ('title: "更改执DòngChi tiết"', 'title: "Sửa đổi chi tiết số lần thực hiện"'),
    ('"đã执Dòng/Đã hoàn thành/Bản ghi đã dừng không thể sửa đổi"',
     '"Đã thực hiện/Đã hoàn thành/Bản ghi đã dừng không thể sửa đổi"'),
    ('value="&nbsp;&nbsp;查&nbsp;&nbsp;询&nbsp;&nbsp;"', 'value="&nbsp;&nbsp;Truy vấn&nbsp;&nbsp;"'),
    ('value=" 查 询 "', 'value=" Truy vấn "'),
    ('&nbsp;Thẻ&nbsp;&nbsp;号&nbsp;', '&nbsp;Số thẻ&nbsp;'),
    ('value="汇总清单"', 'value="Bảng tổng hợp"'),
    ('msg = "Hủyquyết toán thành công，phảihoànSố tiềnđã原路hoàn回";',
     'msg = "Hủy quyết toán thành công, số tiền phải hoàn đã hoàn trả theo đường cũ";'),
    ('msg = "HISHủyquyết toán thành công，nhưngphảihoànSố tiềnhoàn回Thất bại，Vui lòng人工核查";',
     'msg = "HIS hủy quyết toán thành công, nhưng số tiền phải hoàn hoàn trả thất bại, vui lòng kiểm tra thủ công";'),
    ('"mớiNhập viện"', '"Nhập viện mới"'),
    ('"事trongPhê duyệt"', '"Phê duyệt trong quá trình"'),
    ('này时', 'lúc này'),
    ('đãđã', 'đã'),
    ('Đảođang quyết toán nhập viện thất bại', 'Đảo ngược quyết toán nhập viện thất bại'),
    ('liên hệhisNhà phát triển', 'liên hệ nhà phát triển HIS'),
    ('Vui lòng chọn trước可停止củaBản ghi', 'Vui lòng chọn trước bản ghi có thể dừng'),
    ('Vui lòng chọn trước需停止củaBản ghi', 'Vui lòng chọn trước bản ghi cần dừng'),
    ('Vui lòng chọn trước需Phê duyệtcủathông tin', 'Vui lòng chọn trước thông tin cần phê duyệt'),
    ('Vui lòng chọn trước需Hoàn phícủathông tin', 'Vui lòng chọn trước thông tin cần hoàn phí'),
    ('Vui lòng chọn trước需Tải lêncủaChi tiết', 'Vui lòng chọn trước chi tiết cần tải lên'),
    ('Vui lòng chọn trước需hoàn trảcủaChi tiết', 'Vui lòng chọn trước chi tiết cần hoàn trả'),
]

D = {
    "历年": "các năm trước",
    "起付段": "đoạn khởi trả",
    "起付": "khởi trả",
    "预缴款": "khoản tạm nộp",
    "预约": "đặt hẹn",
    "预交": "tạm ứng",
    "可暂时忽略": "có thể tạm thời bỏ qua",
    "不支持": "không hỗ trợ",
    "保险": "bảo hiểm",
    "停止": "Dừng",
    "提醒": "nhắc nhở",
    "原因": "lý do",
    "人员": "nhân viên",
    "不可": "không thể",
    "小于": "nhỏ hơn",
    "晚于": "muộn hơn",
    "差额": "chênh lệch",
    "含": "bao gồm",
    "缴": "nộp",
    "负": "âm",
    "情况": "trường hợp",
    "标志": "cờ",
    "团体": "tập thể",
    "简拼": "mã viết tắt",
    "发票": "hóa đơn",
    "缴费": "nộp phí",
    "打开": "mở",
    "证号": "số chứng nhận",
    "汇总": "tổng hợp",
    "清单": "bảng",
    "提档": "nộp hồ sơ",
    "原来": "ban đầu",
    "组套": "bộ",
    "性质": "tính chất",
    "分中心": "trung tâm chi nhánh",
    "分心": "trung tâm",
    "社(电)": "Thẻ BHXH (điện tử)",
    "社": "Thẻ BHXH",
    "身": "CMND/CCCD",
    "临": "Tạm thời",
    "长": "Dài hạn",
    "执": "thực hiện",
    "号": "số",
    "查": "Truy vấn",
    "询": "vấn",
    "原路": "đường cũ",
    "人工": "thủ công",
    "核查": "kiểm tra",
    "得知": "biết",
    "产生": "phát sinh",
    "去": "đi",
    "此": "này",
    "需": "cần",
    "可": "có thể",
    "还": "còn",
    "它": "nó",
    "时": "lúc",
    "已经": "đã",
    "经": "đã",
    "倒": "đảo ngược",
    "冲": "đảo ngược",
}

def main():
    changed = 0
    for f in FILES:
        if not os.path.exists(f):
            continue
        t = open(f, encoding="utf-8").read()
        orig = t
        for suf, lit, ph in PROTECT:
            if f.endswith(suf) and lit in t:
                t = t.replace(lit, ph)
        for a, b in DIRECT:
            if a in t:
                t = t.replace(a, b)
        for zh, vi in sorted(D.items(), key=lambda kv: -len(kv[0])):
            if zh and zh in t:
                t = t.replace(zh, vi)
        # spacing repairs from fragment joins
        t = t.replace("bảo hiểmcòn lại", "bảo hiểm còn lại")
        t = t.replace("thực hiệnDòng", "thực hiện")
        for suf, lit, ph in PROTECT:
            if f.endswith(suf) and ph in t:
                t = t.replace(ph, lit)
        if t != orig:
            open(f, "w", encoding="utf-8").write(t)
            changed += 1
    print("files changed:", changed)

if __name__ == "__main__":
    main()
