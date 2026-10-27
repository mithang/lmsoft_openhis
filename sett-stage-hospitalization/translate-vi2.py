#!/usr/bin/env python3
"""Pass 2: translate the domain terms pass 1 left behind (incl. terms pass 1
itself emitted, e.g. '结算' inside '结算 nhập viện'). Longest-first.
Protected literals re-applied before/after."""
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

D = {
    # domain cores first (longest-first handles compounds)
    "预结算": "quyết toán trước",
    "预交金": "tạm ứng",
    "结算": "quyết toán",
    "记账": "ghi sổ",
    "收费": "thu phí",
    "住院": "nhập viện",
    "医嘱": "y lệnh",
    "新农合": "nông thôn mới",
    "农保": "bảo hiểm nông nghiệp",
    "统筹": "quỹ chung",
    "共济": "đồng chi trả",
    "抵用": "khấu trừ",
    "退费": "hoàn phí",
    "自费": "tự phí",
    "自理": "tự chi trả",
    "回退": "hoàn trả",
    "落地": "lưu",
    "充值": "nạp tiền",
    "支付": "thanh toán",
    "基金": "quỹ",
    "职工": "người lao động",
    "居民": "dân cư",
    "地方": "địa phương",
    "普通报账报表": "báo cáo thanh toán thông thường",
    "产生费用": "phát sinh phí",
    "处方退掉": "hoàn đơn thuốc",
    "照国家": "theo quốc gia",
    "定点": "chỉ định",
    "范围总额": "tổng số phạm vi",
    "作废": "hủy bỏ",
    "系统": "hệ thống",
    "急诊": "cấp cứu",
    "挂号": "đăng ký khám",
    "重大": "lớn",
    "主键": "khóa chính",
    "流程": "quy trình",
    "附加": "bổ sung",
    "不可访问": "không thể truy cập",
    "窗口事件": "sự kiện cửa sổ",
    "窗口": "cửa sổ",
    "指定": "chỉ định",
    "使用": "sử dụng",
    "属性": "thuộc tính",
    "普通": "thông thường",
    "离休": "nghỉ hưu",
    "包括": "bao gồm",
    "切换": "chuyển đổi",
    "根据": "theo",
    "属于": "thuộc về",
    "经结算": "đã quyết toán",
    "正结算": "đang quyết toán",
    "声明": "khai báo",
    "事件": "sự kiện",
    "自动": "tự động",
    "更新": "cập nhật",
    "互通": "liên thông",
    "服务": "dịch vụ",
    "办法": "phương pháp",
    "表示": "biểu thị",
    "剩余": "còn lại",
    "传过去": "chuyển qua",
    "什么没找": "không tìm thấy gì",
    "跳出循环": "thoát vòng lặp",
    "区分系统": "phân biệt hệ thống",
    "何处": "ở đâu",
    "链接": "liên kết",
    "条件": "điều kiện",
    "信息": "thông tin",
    "费用": "phí",
    "获取": "lấy",
    "新项": "mục mới",
    "大类收费": "thu phí大类",
    "大类": "nhóm lớn",
    "非": "không",
    "空": "trống",
    "尚": "còn",
    "新": "mới",
    "流产": "sảy thai",
    "婚": "hôn nhân",
    "按": "theo",
    "早": "sớm",
    "全": "toàn bộ",
    "部分": "một phần",
    "没": "không",
    "值": "giá trị",
    "退": "hoàn",
    "应": "phải",
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
        for zh, vi in sorted(D.items(), key=lambda kv: -len(kv[0])):
            if zh and zh in t:
                t = t.replace(zh, vi)
        for suf, lit, ph in PROTECT:
            if f.endswith(suf) and ph in t:
                t = t.replace(ph, lit)
        if t != orig:
            open(f, "w", encoding="utf-8").write(t)
            changed += 1
    print("files changed:", changed)

if __name__ == "__main__":
    main()
