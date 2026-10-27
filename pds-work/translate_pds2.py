#!/usr/bin/env python3
"""PDS translation pass 2: phrase-level visible strings left after pass 1.
Reuses pass-1 protection logic (comparison operands, src/href, URLs, comment lines, SQL lines)."""
import sys, os, glob, re
sys.path.insert(0, os.path.dirname(os.path.abspath(__file__)))
import translate_pds1 as t1

G2 = {
"正在请求报表页面": "Đang yêu cầu trang báo cáo",
"生成厂家": "Nhà sản xuất", "操作时间": "Thời gian thao tác",
"已达到该行的最大扫码条目数量": "Đã đạt số lượng mục quét tối đa của dòng này",
"条目": "Mục: ", "数量与追溯码数量不一致": " số lượng không khớp với số lượng mã truy xuất",
"到": "Đến", "至": "đến", "的整数": " số nguyên",
"数量必须为大于": "Số lượng phải lớn hơn ",
"是否显示序号": "Có hiển thị số thứ tự không", "是否需要边框": "Có cần viền không",
"是否需要边框圆角": "Có cần bo góc viền không", "有圆角": "Có bo góc",
"请勾选编辑数据": "Vui lòng chọn dữ liệu cần sửa", "药品必选": "Bắt buộc chọn thuốc",
"开始时间不能大于结束时间": "Thời gian bắt đầu không được lớn hơn thời gian kết thúc",
"缺少开始时间": "Thiếu thời gian bắt đầu", "缺少结束时间": "Thiếu thời gian kết thúc",
"开始时间不能为空": "Thời gian bắt đầu không được để trống",
"结束时间不能为空": "Thời gian kết thúc không được để trống",
"药品类型": "Loại thuốc", "领药部门": "Khoa lĩnh thuốc",
"请选择领药部门": "Vui lòng chọn khoa lĩnh thuốc", "退货原因": "Lý do trả hàng",
"损益零售总额": "Tổng tiền lẻ lãi/lỗ", "定价单位": "Đơn vị định giá",
"门诊拆零数": "Số lẻ ngoại trú", "门诊拆零单位": "Đơn vị lẻ ngoại trú",
"住院拆零数": "Số lẻ nội trú", "住院拆零单位": "Đơn vị lẻ nội trú",
"使用数量": "Số lượng sử dụng", "使用单位": "Đơn vị sử dụng",
"处方性质": "Tính chất đơn thuốc", "已退药": "Đã trả thuốc",
"发药时间": "Thời gian phát thuốc", "可退数量": "Số lượng có thể trả",
"损溢情况": "Tình hình lãi/lỗ", "药库零售价": "Giá lẻ kho thuốc",
"药库批发价": "Giá bán buôn kho thuốc", "病人性质": "Tính chất bệnh nhân",
"请输入发票号": "Vui lòng nhập số hóa đơn", "部门名称": "Tên khoa",
"显示零库存": "Hiển thị tồn kho bằng 0", "返回首页": "Về trang chủ",
"盘点明细": "Chi tiết kiểm kê", "出入库方式": "Cách nhập/xuất kho",
"进价总金额": "Tổng số tiền giá nhập", "出入库明细序号": "Số thứ tự chi tiết nhập/xuất kho",
"您确定要将发票号改为": "Bạn có chắc muốn đổi số hóa đơn thành ",
"吗": " không", "发票号修改成功": "Đổi số hóa đơn thành công",
"请在浮层中选择供应商": "Vui lòng chọn nhà cung cấp trong lớp nổi",
"请选择药企": "Vui lòng chọn công ty dược", "商品统编代码": "Mã thống nhất hàng hóa",
"采购类型": "Loại mua hàng", "待审核": "Chờ duyệt", "二级分类": "Phân loại cấp 2",
"权限级别": "Cấp quyền", "删除成功": "Xóa thành công", "表单信息": "Thông tin biểu mẫu",
"已终止": "Đã kết thúc", "终止成功": "Kết thúc thành công",
"无药品信息": "Không có thông tin thuốc", "入库信息": "Thông tin nhập kho",
"入库总额": "Tổng nhập kho", "进价金额": "Tiền giá nhập", "换单号": "Đổi số đơn",
"确认按钮": "Nút xác nhận",
"有效期应晚于药品的生产日期": "Hạn sử dụng phải muộn hơn ngày sản xuất của thuốc",
"未选中行": "Chưa chọn dòng", "新单据号": "Số chứng từ mới",
"主数据清空": "Xóa trống dữ liệu chính", "明细清空": "Xóa trống chi tiết",
"性别": "Giới tính", "病人类型": "Loại bệnh nhân", "最小库存": "Tồn kho tối thiểu",
"包装数": "Số lượng đóng gói", "发药数量必须为正数": "Số lượng phát thuốc phải là số dương",
"出库数量": "Số lượng xuất kho", "发票信息": "Thông tin hóa đơn",
"损益数量": "Số lượng lãi/lỗ", "原批发价": "Giá bán buôn gốc",
"原零售价": "Giá lẻ gốc", "执行时间": "Thời gian thực hiện",
"操作员": "Người thao tác", "结转数量": "Số lượng kết chuyển",
"国家医保码": "Mã BHYT quốc gia", "发药方式": "Cách phát thuốc",
"内部发药单": "Đơn phát thuốc nội bộ", "最小单位数量": "Số lượng đơn vị nhỏ nhất",
"进价总额请输入正确的金额格式": "Tổng giá nhập vui lòng nhập đúng định dạng tiền",
"实际数量应为自然数": "Số lượng thực tế phải là số tự nhiên",
"初始化病区": "Khởi tạo khu bệnh", "医嘱": "Y lệnh", "费用": "Chi phí",
"开单时间": "Thời gian lập đơn", "特殊处方": "Đơn thuốc đặc biệt",
"申请退药单号": "Số đơn xin trả thuốc", "申请退药明细": "Chi tiết đơn xin trả thuốc",
"需退药列表": "Danh sách cần trả thuốc", "退药单号": "Số đơn trả thuốc",
"请输入药品代码": "Vui lòng nhập mã thuốc", "使用状态": "Trạng thái sử dụng",
"退药部门": "Khoa trả thuốc", "编号": "Số hiệu", "介绍": "Giới thiệu",
"机构授权": "Ủy quyền cơ quan", "秒": "giây", "处方列表": "Danh sách đơn thuốc",
"处方信息": "Thông tin đơn thuốc", "开立医生": "Bác sĩ kê đơn",
"参数无效": "Tham số không hợp lệ", "取消成功": "Hủy thành công",
"静脉滴注": "Truyền tĩnh mạch", "不通过": "Không đạt", "可领数量": "Số lượng có thể lĩnh",
"部门代码": "Mã khoa", "申领数量单位": "Đơn vị số lượng lĩnh",
"完成": "Hoàn thành", "结束": "Kết thúc", "页面跳转中": "Đang chuyển trang",
"没有需要保存的数据": "Không có dữ liệu cần lưu", "盘点完成": "Kiểm kê hoàn tất",
"批发价金额": "Tiền giá bán buôn", "出入库单查询": "Tra cứu đơn nhập/xuất kho",
"提交时间": "Thời gian gửi", "进价单价": "Đơn giá nhập", "发票": "Hóa đơn",
"不显示零库存": "Không hiển thị tồn kho bằng 0", "最多选中一条": "Chọn tối đa một dòng",
"说明": "Giải thích", "阿莫西林": "Amoxicillin", "片": "Viên", "盒": "Hộp",
"入库时间": "Thời gian nhập kho", "批发价总额": "Tổng giá bán buôn",
"药品类": "Loại thuốc", "顺序号": "Số thứ tự", "采购单价": "Đơn giá mua",
"采购计量单位": "Đơn vị đo mua", "备注说明": "Ghi chú giải thích",
"生产厂商": "Nhà sản xuất", "分类名称": "Tên phân loại",
"最后修改人": "Người sửa cuối", "请输入申领单号": "Vui lòng nhập số đơn lĩnh",
"药房部门名称": "Tên khoa dược", "该条记录已终止": "Bản ghi này đã kết thúc",
"该条记录已全发药": "Bản ghi này đã phát hết thuốc", "现发数量": "Số lượng phát hiện tại",
"正常出库": "Xuất kho bình thường", "出库单位": "Đơn vị xuất kho",
"数量格式错误": "Lỗi định dạng số lượng", "药品代码不能为空": "Mã thuốc không được để trống",
"批号不能为空": "Số lô không được để trống", "请输入卡号": "Vui lòng nhập số thẻ",
"请选择目标药房": "Vui lòng chọn nhà thuốc đích", "取消修改": "Hủy sửa",
"批发总价": "Tổng giá bán buôn", "零售总价": "Tổng giá lẻ",
"未发放": "Chưa phát", "已发放部分": "Đã phát một phần", "发放数量": "Số lượng phát",
"外部出库": "Xuất kho ra ngoài", "药品代码为空": "Mã thuốc trống",
"药品名称为空": "Tên thuốc trống", "出库方式": "Cách xuất kho",
"重复添加": "Thêm trùng", "显示未发生进销存药品": "Hiển thị thuốc chưa phát sinh nhập-xuất-tồn",
"统计": "Thống kê", "已审核": "Đã duyệt", "新批发价": "Giá bán buôn mới",
"新零售价": "Giá lẻ mới", "记账类型": "Loại ghi sổ", "库存结转": "Kết chuyển tồn kho",
"您确定结转吗": "Bạn có chắc muốn kết chuyển không", "退药数量": "Số lượng trả thuốc",
"发药单": "Đơn phát thuốc", "排药时间": "Thời gian xếp thuốc", "补打": "In bổ sung",
"请选择操作记录": "Vui lòng chọn bản ghi thao tác", "西药": "Thuốc tây",
"发药标志": "Cờ phát thuốc", "患者姓名": "Tên bệnh nhân", "未退药": "Chưa trả thuốc",
"确定进行退药吗": "Xác nhận thực hiện trả thuốc?", "可退数": "Số có thể trả",
"退药数量应为自然数": "Số lượng trả thuốc phải là số tự nhiên",
"退药数量不得超过": "Số lượng trả thuốc không được vượt quá",
"登录": "Đăng nhập", "同步": "Đồng bộ",
"显示零库存记录": "Hiển thị bản ghi tồn kho bằng 0",
"请选择退药部门": "Vui lòng chọn khoa trả thuốc", "同步成功": "Đồng bộ thành công",
"商品名称": "Tên hàng hóa", "医保代码": "Mã BHYT", "目标": "Mục tiêu",
"创建人员": "Người tạo", "修改人员": "Người sửa", "修改时间": "Thời gian sửa",
"处方内码": "Mã nội bộ đơn thuốc", "请选择科室": "Vui lòng chọn khoa",
"门诊发药": "Phát thuốc ngoại trú", "医保服务": "Dịch vụ BHYT",
"大类明细": "Chi tiết nhóm lớn", "剂型明细": "Chi tiết dạng bào chế",
"历史盘点信息": "Thông tin kiểm kê lịch sử", "实际数": "Số thực tế",
"期初零售总额": "Tổng tiền lẻ đầu kỳ", "期末零售总额": "Tổng tiền lẻ cuối kỳ",
"内部发药退回": "Trả lại phát thuốc nội bộ", "入库部门": "Khoa nhập kho",
"零售价金额": "Tiền giá lẻ", "请选择一条单据": "Vui lòng chọn một chứng từ",
"出库库房": "Kho xuất", "入库库房": "Kho nhập", "库存盘点": "Kiểm kê tồn kho",
"请联系管理员": "Vui lòng liên hệ quản trị viên", "医保限价": "Giá trần BHYT",
"剂量转换系数": "Hệ số chuyển đổi liều", "医保规格": "Quy cách BHYT",
"修改成功": "Sửa thành công", "请在浮层中选择发药部门": "Vui lòng chọn khoa phát thuốc trong lớp nổi",
"发票查询": "Tra cứu hóa đơn", "初始化进度条": "Khởi tạo thanh tiến trình",
"归零": "Về 0",
}

t1.G = G2
nfiles = 0
for pat, is_cs in [(t1.WEB + "/**/*.cshtml", False), (t1.WEB + "/**/*.cs", True)]:
    for f in glob.glob(pat, recursive=True):
        rel = os.path.relpath(f, t1.WEB)
        if rel in t1.DEAD:
            continue
        if t1.process(f, is_cs):
            nfiles += 1
for f in [t1.INF + "/WebCore/BottomButtonViewModel.cs",
          t1.INF + "/Enum/EnumDB.cs", t1.INF + "/Enum/EnumCommon.cs",
          t1.INF + "/Enum/BaseEnums.cs", t1.INF + "/Shims/MissingShims2.cs"]:
    if os.path.exists(f) and t1.process(f, True):
        nfiles += 1
print(f"pass2 translated files: {nfiles}")
tot = 0; fv = 0
for pat in [t1.WEB + "/**/*.cshtml", t1.WEB + "/**/*.cs"]:
    for f in glob.glob(pat, recursive=True):
        t = open(f, encoding="utf-8", errors="replace").read()
        n = len(re.findall(t1.CJK, t))
        if n: fv += 1; tot += n
print(f"remaining Web CJK chars: {tot} in {fv} files")
