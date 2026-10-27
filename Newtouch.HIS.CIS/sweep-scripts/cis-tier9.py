import re, os, sys
COMMENT = re.compile(r'(@\*.*?\*@|<!--.*?-->|(?<!:)//[^\n]*|/\*.*?\*/)', re.S)
D = {
"label: '证件Loại'": "label: 'Loại giấy tờ'",
"'Cư dânCMND/CCCD（户口簿）'": "'CMND/CCCD cư dân (sổ hộ khẩu)'",
"'中国人民解放军军官证'": "'Giấy chứng nhận sĩ quan Quân Giải phóng Nhân dân Trung Quốc'",
"'中国人民武装Cảnh sát警官证'": "'Giấy chứng nhận cảnh sát vũ trang nhân dân Trung Quốc'",
"'香港特区护照/港澳Cư dân来往内地通行证'": "'Hộ chiếu Đặc khu Hồng Kông/Giấy thông hành cư dân Hồng Kông-Ma Cao đi lại nội địa'",
"'澳门特区护照/港澳Cư dân来往内地通行证'": "'Hộ chiếu Đặc khu Ma Cao/Giấy thông hành cư dân Hồng Kông-Ma Cao đi lại nội địa'",
"'台湾Cư dân来往大陆通行证'": "'Giấy thông hành cư dân Đài Loan đi lại đại lục'",
"'外国人永久居留证'": "'Giấy phép cư trú vĩnh viễn cho người nước ngoài'",
"'外国人护照'": "'Hộ chiếu người nước ngoài'",
"'残疾人证'": "'Giấy chứng nhận người khuyết tật'",
"'军烈属证明'": "'Giấy chứng nhận thân nhân liệt sĩ quân đội'",
"'外国人就业证'": "'Giấy phép làm việc cho người nước ngoài'",
"'外国Chuyên gia证'": "'Giấy chứng nhận chuyên gia nước ngoài'",
"'外国人常驻记者证'": "'Thẻ nhà báo thường trú cho người nước ngoài'",
"'台港澳人员就业证'": "'Giấy phép làm việc cho nhân viên Đài Loan-Hồng Kông-Ma Cao'",
"'回国（来华）定居Chuyên gia证'": "'Giấy chứng nhận chuyên gia định cư về nước (đến Trung Quốc)'",
"'中国护照'": "'Hộ chiếu Trung Quốc'",
"'港澳台Cư dân居住证'": "'Giấy phép cư trú cho cư dân Hồng Kông-Ma Cao-Đài Loan'",
"'Xã hội保障卡'": "'Thẻ bảo hiểm xã hội'",
"'KhácCMND/CCCD件'": "'Giấy tờ khác'",
"'医学出生证明'": "'Giấy chứng sinh y học'",
"'扶贫人口Mã'": "'Mã hộ nghèo'",
"Ghi nhận chi phíNgàyKhông ởNhập việnTrong thời gian<br/>Xuất việnNgày:": "Ngày ghi nhận chi phí không nằm trong thời gian nhập viện<br/>Ngày xuất viện:",
"Ghi nhận chi phíNgàyKhông thể晚于Chuyển khuNgày<br/>Chuyển khuNgày:": "Ngày ghi nhận chi phí không thể muộn hơn ngày chuyển khu<br/>Ngày chuyển khu:",
"Ghi nhận chi phíNgàyKhông thể晚于中途Quyết toánNgày<br/>中途Quyết toánNgày:": "Ngày ghi nhận chi phí không thể muộn hơn ngày quyết toán giữa chừng<br/>Ngày quyết toán giữa chừng:",
"Tạm ứngSố dư tài khoản已达到限额,请Nhắc nhởBệnh nhân充值": "Số dư tài khoản tạm ứng đã đạt hạn mức, vui lòng nhắc bệnh nhân nạp tiền",
"Tạm ứngTài khoản已Nợ phí,Không thể产生Tính phí,需先充值Tạm ứngTài khoản": "Tài khoản tạm ứng đã nợ phí, không thể phát sinh tính phí, cần nạp tiền vào tài khoản tạm ứng trước",
"24giờ内有已KêY lệnh,Có tiếp tục khôngLưu？": "Trong 24 giờ đã có y lệnh kê, có tiếp tục lưu không?",
"]已在Y lệnh dài hạn中Sử dụng，Có tiếp tục khôngLưu？": "]Đã sử dụng trong y lệnh dài hạn, có tiếp tục lưu không?",
"Vui lòngĐiềnChính xác的Xuất việnNgày和Thời gian,": "Vui lòng điền chính xác ngày và giờ xuất viện,",
"【Xác nhận】后该Bệnh nhânY lệnh dài hạnsẽ bịTất cảDừng,": "Sau khi 【Xác nhận】, toàn bộ y lệnh dài hạn của bệnh nhân này sẽ bị dừng,",
"Đồng thờiHệ thốngsẽ cho bạnTự độngNhậpmột dòng【Xuất viện】Y lệnh!": "Đồng thời hệ thống sẽ tự động nhập cho bạn một dòng y lệnh 【Xuất viện】!",
"2.Xuất việnHôm nayKhông cầnDùng thuốc的Bệnh nhân请个别调整Y lệnhDừngThời gian为Xuất viện前一天(如:静脉Dùng thuốc)": "2. Bệnh nhân xuất viện hôm nay không cần dùng thuốc, vui lòng điều chỉnh riêng thời gian dừng y lệnh thành ngày trước ngày xuất viện (ví dụ: dùng thuốc đường tĩnh mạch)",
"Vui lòngĐiềnChính xác的Mục tiêuKhu bệnh和Thời gian bắt đầu,": "Vui lòng điền chính xác khu bệnh đích và thời gian bắt đầu,",
"Đồng thờiHệ thốngsẽ cho bạnTự độngNhậpmột dòng【Chuyển khu】Y lệnh!": "Đồng thời hệ thống sẽ tự động nhập cho bạn một dòng y lệnh 【Chuyển khu】!",
'<option value="">不限</option>': '<option value="">Không giới hạn</option>',
'<option value="1">出院结算</option>': '<option value="1">Quyết toán xuất viện</option>',
'<option value="2">中途结算</option>': '<option value="2">Quyết toán giữa chừng</option>',
}
DRY = os.environ.get('DRY') == '1'
ROOT = os.path.expanduser('~/workspace/openhis-core/Newtouch.HIS.CIS/Newtouch.CIS.Web')
targets = ["Areas/DoctorManage/Views/ElectronicPrescription/Index.cshtml",
"Areas/NurseManage/Views/OrderExecution/BindingFeeForm.cshtml",
"Areas/DoctorManage/Views/DoctorsAdvice/Index.cshtml",
"Areas/DoctorManage/Views/Medicine/Index.cshtml",
"Areas/DoctorManage/Views/Medicine/TCMMedicine.cshtml",
"Areas/DoctorManage/Views/DoctorsAdvice/AdviceLeaveHospitalStop.cshtml",
"Areas/DoctorManage/Views/DoctorsAdvice/TransferWardsTOP.cshtml",
"Areas/DoctorManage/Views/DoctorsAdvice/SettInfoShow.cshtml"]
total = 0
for rel in targets:
    p = os.path.join(ROOT, rel)
    txt = open(p, encoding='utf-8').read()
    parts = COMMENT.split(txt)
    n = 0
    for i in range(0, len(parts), 2):
        seg = parts[i]
        for k, v in D.items():
            c = seg.count(k)
            if c:
                seg = seg.replace(k, v); n += c
        parts[i] = seg
    print(f"{rel}: {n}")
    total += n
    if not DRY and n:
        open(p, 'w', encoding='utf-8').write(''.join(parts))
print("TOTAL:", total)
