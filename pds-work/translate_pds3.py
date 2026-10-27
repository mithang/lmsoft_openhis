#!/usr/bin/env python3
"""PDS translation pass 3: remaining visible labels/messages identified from
comment-stripped served HTML of the probed pages. Same protection machinery as pass 1."""
import sys, os, glob, re
sys.path.insert(0, os.path.dirname(os.path.abspath(__file__)))
import translate_pds1 as t1

G3 = {
"个人信息": "Thông tin cá nhân", "切换用户药房": "Chuyển nhà thuốc người dùng",
"全部关闭": "Đóng tất cả", "关闭当前": "Đóng hiện tại", "刷新当前": "Làm mới hiện tại",
"除此之外全部关闭": "Đóng tất cả trừ tab này", "库存预警": "Cảnh báo tồn kho",
"个月内过期预警": " tháng tới hết hạn (cảnh báo)",
"个月内过期药品查询": " tháng tới hết hạn - tra cứu thuốc",
"页签操作": "Thao tác tab", "请稍后": "Vui lòng đợi",
"库存量查询": "Tra cứu lượng tồn kho", "库房切换": "Chuyển kho",
"皮肤设置": "Cài đặt giao diện", "浏览器缓存清除成功": "Đã xóa bộ nhớ đệm trình duyệt thành công",
"清空缓存": "Xóa bộ nhớ đệm", "安全退出": "Thoát an toàn",
"消息列表": "Danh sách tin nhắn", "欢迎首页": "Trang chủ chào mừng",
"消息提醒": "Nhắc tin nhắn",
"发药单号": "Số đơn phát thuốc", "发药日期": "Ngày phát thuốc",
"瓶": "Chai", "账册序号": "Số thứ tự sổ sách", "领药信息": "Thông tin lĩnh thuốc",
"可退库存": "Tồn kho có thể trả", "该药品批次已登记": "Lô thuốc này đã đăng ký",
"不允许新增": "Không cho phép thêm mới", "该行正在处于编辑状态": "Dòng này đang ở trạng thái sửa",
"药品信息不能为空": "Thông tin thuốc không được để trống",
"领药部门必须填写": "Bắt buộc nhập khoa lĩnh thuốc",
"发药方式必须选择": "Bắt buộc chọn cách phát thuốc",
"本批次最大数量不得超过": "Số lượng tối đa của lô này không được vượt quá",
"取消控制": "Hủy kiểm soát", "已控制": "Đã kiểm soát",
"药品筛选信息": "Thông tin lọc thuốc",
"删除医嘱": "Xóa y lệnh", "刷新患者": "Làm mới bệnh nhân",
"明细打印": "In chi tiết",
"医嘱执行开始时间不能为空": "Thời gian bắt đầu thực hiện y lệnh không được để trống",
"医嘱执行结束时间不能为空": "Thời gian kết thúc thực hiện y lệnh không được để trống",
"发放状态": "Trạng thái cấp phát", "申请日期": "Ngày xin",
"批量审核通过": "Duyệt hàng loạt đạt", "撤销审核": "Thu hồi duyệt",
"目标药房": "Nhà thuốc đích", "入库单": "Đơn nhập kho",
"药品定位": "Định vị thuốc", "库存明细": "Chi tiết tồn kho",
"库存状态": "Trạng thái tồn kho", "本部门状态": "Trạng thái khoa này",
"请选择需要提交的单据": "Vui lòng chọn chứng từ cần gửi",
"只能选择一个单据进行提交": "Chỉ được chọn một chứng từ để gửi",
"单据提交成功": "Gửi chứng từ thành công", "单据提交失败": "Gửi chứng từ thất bại",
"提交成功": "Gửi thành công", "请选中信息": "Vui lòng chọn thông tin",
"传报": "Gửi báo cáo", "不能修改": "Không thể sửa", "直接填充": "Điền trực tiếp",
"转换成部门单位后的最大可用数": "Số tối đa khả dụng sau khi chuyển sang đơn vị khoa",
}
t1.G = G3
nfiles = 0
for pat, is_cs in [(t1.WEB + "/**/*.cshtml", False), (t1.WEB + "/**/*.cs", True)]:
    for f in glob.glob(pat, recursive=True):
        rel = os.path.relpath(f, t1.WEB)
        if rel in t1.DEAD:
            continue
        if t1.process(f, is_cs):
            nfiles += 1
print(f"pass3 translated files: {nfiles}")
tot = 0
for pat in [t1.WEB + "/**/*.cshtml", t1.WEB + "/**/*.cs"]:
    for f in glob.glob(pat, recursive=True):
        t = open(f, encoding="utf-8", errors="replace").read()
        tot += len(re.findall(t1.CJK, t))
print(f"remaining Web CJK chars (incl. comments): {tot}")
