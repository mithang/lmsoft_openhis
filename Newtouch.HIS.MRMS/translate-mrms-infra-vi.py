# MRMS Infrastructure Vietnamese translations: service/validation messages,
# SQL display values, enum Descriptions. Comments, region names, proper nouns skipped.
# File-scoped replacements; longest keys first.
import re
from pathlib import Path

ROOT = Path.home() / "workspace/openhis-core/Newtouch.HIS.MRMS/Newtouch.MRMS.Infrastructure"

MSG = {
    "获取病历列表异常，": "Bất thường khi lấy danh sách bệnh án,",
    "电子病历病案号异常，请退回。": "Số bệnh án EMR bất thường, vui lòng trả lại.",
    "病案号生成失败，请重试。": "Tạo số bệnh án thất bại, vui lòng thử lại.",
    "病案号生成失败，请重新加载。": "Tạo số bệnh án thất bại, vui lòng tải lại.",
    "患者基础病案信息未录入，无法保存。": "Thông tin bệnh án cơ bản của bệnh nhân chưa được nhập, không thể lưu.",
    "数据异常，请刷新后重新填写": "Dữ liệu bất thường, vui lòng làm mới rồi nhập lại",
    "诊断列表不可为空": "Danh sách chẩn đoán không được để trống",
    "手术列表不可为空": "Danh sách phẫu thuật không được để trống",
    "病案号获取失败，请刷新重试；": "Lấy số bệnh án thất bại, vui lòng làm mới và thử lại;",
    "入院途径不可为空；": "Đường nhập viện không được để trống;",
    "医疗付款方式不可为空；": "Phương thức thanh toán y tế không được để trống;",
    "住院次数不可为空；": "Số lần nội trú không được để trống;",
    "性别不可为空；": "Giới tính không được để trống;",
    "出生日期不可为空；": "Ngày sinh không được để trống;",
    "职业不可为空；": "Nghề nghiệp không được để trống;",
    "身份证号不可为空；": "Số CMND/CCCD không được để trống;",
    "婚姻状况不可为空；": "Tình trạng hôn nhân không được để trống;",
    "门（急）诊诊断疾病编码不可为空；": "Mã bệnh chẩn đoán ngoại trú (cấp cứu) không được để trống;",
    "门（急）诊诊断不可为空；": "Chẩn đoán ngoại trú (cấp cứu) không được để trống;",
    "离院方式不可为空；": "Hình thức rời viện không được để trống;",
    "未找到该患者病案信息，请刷新重试。": "Không tìm thấy thông tin bệnh án của bệnh nhân, vui lòng làm mới và thử lại.",
    "患者已存在病案信息，请勿重复提交。": "Bệnh nhân đã có thông tin bệnh án, vui lòng không gửi trùng lặp.",
    "住院院病案首页表导出成功！": "Xuất bảng trang đầu bệnh án nội trú thành công!",
    "住院院病案首页表导出异常：": "Xuất bảng trang đầu bệnh án nội trú bất thường:",
    "医院管理附页表导出成功！": "Xuất bảng phụ quản lý bệnh viện thành công!",
    "医院管理附页导出异常：": "Xuất bảng phụ quản lý bệnh viện bất thường:",
    "病案收费类别及HIS收费大类不可为空！": "Nhóm thu phí bệnh án và nhóm thu phí HIS không được để trống!",
    "编号不可重复": "Mã không được trùng lặp",
    "操作失败(": "Thao tác thất bại(",
    "异常：": "Bất thường:",
    # SQL display values (no code compares against these strings - verified)
    "'总费用'": "'Tổng chi phí'",
    "'有' RYBQMS": "'Có' RYBQMS",
    "'治愈' CYQKMS": "'Khỏi bệnh' CYQKMS",
    "then '女' when a.sex=1 then '男' else '未说明'": "then 'Nữ' when a.sex=1 then 'Nam' else 'Chưa rõ'",
}

ENUM = {
    "未知的性别": "Giới tính chưa rõ", "未说明的性别": "Giới tính chưa rõ",
    "待录入": "Chờ nhập", "已录入": "Đã nhập", "已归档": "Đã lưu trữ",
    "已提交": "Đã gửi", "退回": "Trả lại", "已签收": "Đã ký nhận",
    "未提交": "Chưa gửi", "不详": "Không rõ", "未查": "Chưa kiểm tra",
    "阴": "Âm tính", "阳": "Dương tính",
    "本人或户主": "Bản thân hoặc chủ hộ", "配偶": "Vợ/chồng",
    "孙子、孙女或外孙子、外孙女": "Cháu nội, cháu ngoại",
    "父母": "Cha mẹ", "祖父母或外祖父母": "Ông bà nội/ngoại",
    "兄、弟、姐、妹": "Anh/chị/em", "其他": "Khác",
    "临床未确定": "Lâm sàng chưa xác định", "情况不明": "Tình trạng chưa rõ",
    "治愈": "Khỏi bệnh", "好转": "Đỡ hơn", "未愈": "Chưa khỏi",
    "死亡": "Tử vong", "主要诊断": "Chẩn đoán chính",
    "次要诊断": "Chẩn đoán phụ", "门诊": "Ngoại trú", "急诊": "Cấp cứu",
    "其他医疗机构转入": "Chuyển đến từ cơ sở y tế khác",
    "住院病历": "Bệnh án nội trú", "病程记录": "Hồ sơ diễn biến bệnh",
    "医疗文书": "Văn bản y tế", "护理记录": "Hồ sơ điều dưỡng",
    "病案首页": "Trang đầu bệnh án", "康复评估": "Đánh giá phục hồi",
    "西医诊断": "Chẩn đoán tây y", "中医诊断": "Chẩn đoán y học cổ truyền",
    "医嘱离院": "Rời viện theo y lệnh", "医嘱转院": "Chuyển viện theo y lệnh",
    "医嘱转社区卫生服务机构/乡镇卫生院": "Chuyển đến cơ sở y tế cộng đồng/trạm y tế xã theo y lệnh",
    "非医嘱离院": "Rời viện không theo y lệnh",
    "病危": "Nguy kịch", "病重": "Bệnh nặng", "疑难": "Khó",
    "抢救": "Cấp cứu", "一般": "Thông thường",
}


def apply_map(path, mapping):
    p = Path(path)
    t = p.read_text(encoding="utf-8")
    n = 0
    for k in sorted(mapping, key=len, reverse=True):
        if k in t:
            n += t.count(k)
            t = t.replace(k, mapping[k])
    p.write_text(t, encoding="utf-8")
    print(f"{p.relative_to(ROOT)}: {n} replacements")
    return t


def main():
    base = ROOT / "DomainServices"
    apply_map(base / "EMRDmnService.cs", MSG)
    apply_map(base / "MainRecordDmnService.cs", MSG)
    apply_map(base / "RecordDmnService.cs", MSG)
    apply_map(base / "MrFeeRelDmnService.cs", MSG)
    apply_map(ROOT / "Repository/MrdicdeptRepo.cs", MSG)
    # EnumDB: quoted single-char values need quote-scoped handling
    ep = ROOT / "Infrastructure/EnumDB.cs"
    t = ep.read_text(encoding="utf-8")
    n = 0
    for k in sorted(ENUM, key=len, reverse=True):
        if k in t:
            n += t.count(k)
            t = t.replace(k, ENUM[k])
    # single chars, quote-scoped (Descriptions only)
    for cn, vi in [("男", "Nam"), ("女", "Nữ"), ("否", "Không"), ("是", "Có"),
                   ("无", "Không có"), ("有", "Có"), ("甲", "A"), ("乙", "B"),
                   ("丙", "C"), ("子", "Con trai")]:
        old = f'("{cn}")'
        if old in t:
            n += t.count(old)
            t = t.replace(old, f'("{vi}")')
    ep.write_text(t, encoding="utf-8")
    print(f"Infrastructure/EnumDB.cs: {n} replacements")
    # relationship 女 (daughter) vs sex 女: exactly two Description("Nữ");
    # first is sex, second is the relationship entry -> Con gái
    lines = ep.read_text(encoding="utf-8").splitlines()
    idx = [i for i, line in enumerate(lines) if 'Description("Nữ")' in line]
    if len(idx) == 2:
        lines[idx[1]] = lines[idx[1]].replace('Description("Nữ")', 'Description("Con gái")')
        print(f"EnumDB line {idx[1]+1}: relationship 女 -> Con gái")
    else:
        print(f"EnumDB Nữ lines found: {idx} (expected 2, no fix applied)")
    ep.write_text("\n".join(lines) + "\n", encoding="utf-8")


if __name__ == "__main__":
    main()
