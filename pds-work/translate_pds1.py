#!/usr/bin/env python3
"""PDS Vietnamese translation pass 1: curated glossary, whole-CJK-run replacement.
Protections: comparison operands (==/!=/case), src/href attribute values, URLs,
full-comment lines, SQL lines in .cs. Excluded: dead controllers (not compiled).
Is/否 NOT translated (DB values compared in RepercussionIndex)."""
import glob, os, re, sys

WEB = "/home/hatch/workspace/openhis-core/Newtouch.HIS.PDS/Newtouch.PDS.Web"
INF = "/home/hatch/workspace/openhis-core/Newtouch.HIS.PDS/Newtouch.PDS.Infrastructure"

G = {
"药品名称": "Tên thuốc", "药品代码": "Mã thuốc", "药品类别": "Loại thuốc", "药品信息": "Thông tin thuốc",
"药品明细": "Chi tiết thuốc", "药品剂型": "Dạng bào chế thuốc", "药品规格": "Quy cách thuốc",
"药品分类": "Phân loại thuốc", "药品分组": "Nhóm thuốc", "药品状态": "Trạng thái thuốc",
"药品属性": "Thuộc tính thuốc", "药品性质": "Tính chất thuốc", "药品总量": "Tổng lượng thuốc",
"药品浮层": "Lớp nổi thuốc", "药品编码": "Mã thuốc", "药品": "Thuốc",
"规格": "Quy cách", "查询": "Tra cứu", "查询条件": "Điều kiện tra cứu", "元": "đ",
"全部": "Tất cả", "单位": "Đơn vị", "批次": "Lô", "名称": "Tên", "批号": "Số lô",
"生产厂家": "Nhà sản xuất", "批发价": "Giá bán buôn", "零售价": "Giá bán lẻ",
"有效期": "Hạn sử dụng", "数量": "Số lượng", "发票号": "Số hóa đơn", "姓名": "Họ tên",
"拼音": "Pinyin", "修改": "Sửa", "库存": "Tồn kho", "发药": "Phát thuốc",
"请选择": "Vui lòng chọn", "是否拆零": "Có tách lẻ không", "零价总额": "Tổng giá lẻ",
"代码": "Mã", "零售总额": "Tổng tiền bán lẻ", "打开编辑模式": "Mở chế độ sửa",
"科室": "Khoa", "进价": "Giá nhập", "初始化": "Khởi tạo", "操作成功": "Thao tác thành công",
"操作失败": "Thao tác thất bại", "退药": "Trả thuốc", "删除": "Xóa", "零售单价": "Đơn giá lẻ",
"批准文号": "Số đăng ký", "启用行内编辑框": "Bật khung sửa trong dòng", "主键": "Khóa chính",
"卡号": "Số thẻ", "类别": "Loại", "零售金额": "Tiền bán lẻ", "最小单位": "Đơn vị nhỏ nhất",
"处方号": "Số đơn thuốc", "追溯码": "Mã truy xuất", "编码": "Mã",
"批发总额": "Tổng giá bán buôn", "添加": "Thêm", "单据号": "Số chứng từ",
"部门单位零售价": "Giá lẻ theo đơn vị khoa", "请选中一条信息": "Vui lòng chọn một bản ghi",
"金额": "Số tiền", "单价": "Đơn giá", "保存": "Lưu", "转换因子": "Hệ số chuyển đổi",
"转化因子": "Hệ số chuyển đổi", "供应商": "Nhà cung cấp", "关键字": "Từ khóa",
"进价总额": "Tổng giá nhập", "启用": "Bật", "批发金额": "Tiền bán buôn",
"部门单位": "Đơn vị khoa", "提交": "Gửi", "操作": "Thao tác", "厂家": "Nhà sản xuất",
"大于": "Lớn hơn", "剂量": "Liều dùng", "剂量单位": "Đơn vị liều",
"作废": "Hủy bỏ", "单据明细": "Chi tiết chứng từ", "新增": "Thêm mới",
"排序": "Sắp xếp", "日期": "Ngày", "用法": "Cách dùng", "剂型": "Dạng bào chế",
"处方明细": "Chi tiết đơn thuốc", "首拼": "Chữ đầu pinyin", "创建时间": "Thời gian tạo",
"申领单号": "Số đơn lĩnh", "开始日期": "Ngày bắt đầu", "结束日期": "Ngày kết thúc",
"状态": "Trạng thái", "开始盘点": "Bắt đầu kiểm kê", "理论数": "Số lý thuyết",
"刷新列表": "Làm mới danh sách", "新建": "Tạo mới", "发药状态": "Trạng thái phát thuốc",
"发药成功": "Phát thuốc thành công", "发药失败": "Phát thuốc thất bại", "重置": "Đặt lại",
"输入码": "Mã nhập", "生成单据号": "Tạo số chứng từ", "初始化输入码": "Khởi tạo mã nhập",
"系数": "Hệ số", "打印": "In", "审核状态": "Trạng thái duyệt",
"批价总额": "Tổng giá bán buôn", "床位号": "Số giường", "报表链接": "Liên kết báo cáo",
"库存数量": "Số lượng tồn kho", "全选": "Chọn tất cả", "请确认": "Vui lòng xác nhận",
"浮层": "Lớp nổi", "默认": "Mặc định", "药企": "Công ty dược",
"申领数量": "Số lượng lĩnh", "供应商名称": "Tên nhà cung cấp", "清空": "Xóa trống",
"获取明细": "Lấy chi tiết", "禁用": "Vô hiệu hóa", "计算扣率": "Tính tỷ lệ chiết khấu",
"收费时间": "Thời gian thu phí", "单据主记录": "Bản ghi chính chứng từ", "病区": "Khu bệnh",
"实际数量": "Số lượng thực tế", "药企编码": "Mã công ty dược", "配送点": "Điểm phân phối",
"药房": "Nhà thuốc", "申领部门": "Khoa lĩnh", "供应商代码": "Mã nhà cung cấp",
"药库单位": "Đơn vị kho thuốc", "入库": "Nhập kho", "搜索": "Tìm kiếm",
"审核": "Duyệt", "产地": "Xuất xứ", "创建人": "Người tạo", "点击": "Nhấn",
"获取": "Lấy", "医生嘱托": "Lời dặn bác sĩ", "申领出库": "Xuất kho theo đơn lĩnh",
"单据类型": "Loại chứng từ", "开始时间": "Thời gian bắt đầu", "筛选条件": "Điều kiện lọc",
"住院号": "Số nội trú", "返回": "Quay lại", "其他": "Khác", "选项": "Tùy chọn",
"最后修改时间": "Thời gian sửa cuối", "终止": "Kết thúc", "现有库存": "Tồn kho hiện có",
"包装单位": "Đơn vị đóng gói", "生产日期": "Ngày sản xuất", "扣率": "Tỷ lệ chiết khấu",
"频次": "Tần suất", "新发药单": "Đơn phát thuốc mới", "医嘱性质": "Tính chất y lệnh",
"项目": "Hạng mục", "部门包装": "Đóng gói khoa", "已作废": "Đã hủy",
"进销差价": "Chênh lệch nhập-xuất", "请选择供应商": "Vui lòng chọn nhà cung cấp",
"单位切换": "Chuyển đơn vị", "发药数不得超过库存总量": "Số phát không được vượt tổng tồn kho",
"已保存": "Đã lưu", "损益原因": "Nguyên nhân lãi/lỗ", "报损": "Báo hỏng",
"开票日期": "Ngày lập hóa đơn", "审核通过": "Đã duyệt", "年龄": "Tuổi",
"用量": "Liều lượng", "出库": "Xuất kho", "总金额": "Tổng số tiền",
"科室名称": "Tên khoa", "结束时间": "Thời gian kết thúc", "导出": "Xuất",
"数据加载中": "Đang tải dữ liệu", "退药成功": "Trả thuốc thành công",
"患者列表": "Danh sách bệnh nhân", "取消排药": "Hủy xếp thuốc", "组号": "Số nhóm",
"保存成功": "Lưu thành công", "停用": "Ngừng dùng", "冻结数量": "Số lượng đóng băng",
"订单时间": "Thời gian đơn hàng", "已传报": "Đã gửi báo cáo", "数据源": "Nguồn dữ liệu",
"药库": "Kho thuốc", "报溢": "Báo thừa", "筛选信息": "Thông tin lọc",
"无效": "Không hiệu lực", "输入数量": "Nhập số lượng", "未审核": "Chưa duyệt",
"已过期": "Đã hết hạn", "撤销": "Thu hồi", "过期药品查询": "Tra cứu thuốc hết hạn",
"汇总打印": "In tổng hợp", "医生": "Bác sĩ", "操作类型": "Loại thao tác",
"按批次": "Theo lô", "不按批次": "Không theo lô", "处方": "Đơn thuốc",
"结束盘点": "Kết thúc kiểm kê", "库存信息": "Thông tin tồn kho", "商品类型": "Loại hàng hóa",
"订单类型": "Loại đơn hàng", "订单编号": "Số đơn hàng", "统计日期": "Ngày thống kê",
"组织": "Tổ chức", "商品名": "Tên hàng hóa", "清空明细": "Xóa trống chi tiết",
"检查明细": "Kiểm tra chi tiết", "计算入库总额": "Tính tổng nhập kho",
"药房名称": "Tên nhà thuốc", "发药数量": "Số lượng phát thuốc", "有效日期": "Ngày hiệu lực",
"直接出库": "Xuất kho trực tiếp", "自负比例": "Tỷ lệ tự trả", "消息提醒": "Nhắc tin nhắn",
"基本信息": "Thông tin cơ bản", "大类": "Nhóm lớn", "最后修改": "Sửa cuối",
"门诊排药": "Xếp thuốc ngoại trú", "排药": "Xếp thuốc", "门诊号": "Số ngoại trú",
"发药部门": "Khoa phát thuốc", "国家医保代码": "Mã BHYT quốc gia", "剂型信息": "Thông tin dạng bào chế",
"请选择发药部门": "Vui lòng chọn khoa phát thuốc", "配送点编码": "Mã điểm phân phối",
"申领信息": "Thông tin lĩnh", "已发数量": "Số lượng đã phát", "入库方式": "Cách nhập kho",
"可选单位": "Đơn vị có thể chọn", "检查主数据": "Kiểm tra dữ liệu chính",
"不可重复添加": "Không được thêm trùng", "药房编码": "Mã nhà thuốc",
"可用库存": "Tồn kho khả dụng", "数据是否完善": "Dữ liệu đã đầy đủ chưa",
"检查必填项": "Kiểm tra mục bắt buộc", "不能删除": "Không thể xóa",
"出库部门": "Khoa xuất kho", "科室编码": "Mã khoa", "进销存统计": "Thống kê nhập-xuất-tồn",
"零差率": "Tỷ lệ chênh lệch bằng 0", "入库进价总额": "Tổng giá nhập kho",
"出库进价总额": "Tổng giá xuất kho", "调价损益总额": "Tổng lãi/lỗ điều giá",
"自负性质": "Tính chất tự trả", "结转": "Kết chuyển", "当前页": "Trang hiện tại",
"打印明细": "In chi tiết", "未选中": "Chưa chọn", "住院发药查询": "Tra cứu phát thuốc nội trú",
"退数量": "Số lượng trả", "住院退药": "Trả thuốc nội trú", "组织机构": "Cơ quan tổ chức",
"库位": "Vị trí kho", "控制": "Điều khiển", "责任人": "Người chịu trách nhiệm",
"备注": "Ghi chú", "取消": "Hủy", "药厂名称": "Tên nhà máy dược",
"请输入名称": "Vui lòng nhập tên", "配药成功": "Cấp thuốc thành công",
"获取处方信息": "Lấy thông tin đơn thuốc", "请选中患者信息": "Vui lòng chọn thông tin bệnh nhân",
"全部发药": "Phát tất cả thuốc", "未通过": "Không đạt", "新申领单": "Đơn lĩnh mới",
"库存显示": "Hiển thị tồn kho", "部门数": "Số khoa", "最小数": "Số nhỏ nhất",
"发药信息": "Thông tin phát thuốc", "添加发药明细": "Thêm chi tiết phát thuốc",
"删除发药明细": "Xóa chi tiết phát thuốc", "批次必选": "Bắt buộc chọn lô",
"医院编码": "Mã bệnh viện", "最晚到货日期": "Ngày giao hàng muộn nhất",
"采购数量": "Số lượng mua", "主键值": "Giá trị khóa chính", "视图": "Chế độ xem",
"药房部门": "Khoa dược", "一级分类": "Phân loại cấp 1", "申领时间": "Thời gian lĩnh",
"申领单": "Đơn lĩnh", "入库数量": "Số lượng nhập kho",
"单据查询": "Tra cứu chứng từ", "初始化供应商": "Khởi tạo nhà cung cấp",
"药品性质": "Tính chất thuốc", "有效": "Hiệu lực", "显示": "Hiển thị",
"不显示": "Không hiển thị", "通用": "Dùng chung", "门诊": "Ngoại trú",
"住院": "Nội trú", "启用": "Bật", "停用": "Ngừng dùng",
"自费": "Tự trả", "甲类": "Hạng A", "乙类": "Hạng B", "丙类": "Hạng C",
"每天取整": "Lấy tròn hàng ngày", "每次取整": "Lấy tròn mỗi lần",
"国基药": "Thuốc cơ bản quốc gia", "贵安增补基药": "Thuốc cơ bản bổ sung Quý An",
"非基药": "Thuốc không cơ bản", "非前述几种": "Các loại khác",
"麻醉药品": "Thuốc gây mê", "毒性药品": "Thuốc độc", "精一药品": "Thuốc hướng thần nhóm 1",
"精二药品": "Thuốc hướng thần nhóm 2",
"清除": "Xóa trống", "刷新": "Làm mới", "退出": "Thoát",
"确认": "Xác nhận", "关闭": "Đóng", "选择": "Chọn", "请输入": "Vui lòng nhập",
"提示": "Thông báo", "成功": "Thành công", "失败": "Thất bại",
"警告": "Cảnh báo", "错误": "Lỗi", "信息": "Thông tin", "详情": "Chi tiết",
"列表": "Danh sách", "总计": "Tổng cộng", "合计": "Tổng cộng",
"页": "Trang", "行": "Dòng", "列": "Cột", "表": "Bảng",
"实际": "Thực tế", "理论": "Lý thuyết",
"(实)": "(Thực tế)", "(理)": "(Lý thuyết)",
"实际部门单位数": "Số đơn vị khoa thực tế", "实际最小单位数": "Số đơn vị nhỏ nhất thực tế",
"部门数": "Số khoa",
}

DEAD = [
    "Controllers/AntibioticTypeController.cs", "Controllers/PharmacyWindowController.cs",
    "Controllers/SysMedicineController.cs", "Controllers/SystemManagerController.cs",
    "Controllers/Base/ModuleController.cs", "Controllers/Base/OrganizeController.cs",
    "Controllers/Base/RoleAuthorizeController.cs", "Controllers/Base/RoleController.cs",
    "Controllers/Base/UserController.cs", "Areas/OutOrInStoredManage/Controllers/TransferController.cs",
]

CJK = r"[\u4e00-\u9fff]"
# quoted string containing CJK
QSTR = re.compile(r"""(["'])([^"'\n]*?""" + CJK + r"""[^"'\n]*?)\1""")

def protect_spans(line):
    """Return (masked_line, [(placeholder, original), ...]) for protected spans."""
    spans = []
    def repl(m):
        spans.append(m.group(0))
        return f"\x00P{len(spans)-1}\x00"
    # comparison operands: ==/!=/===/!== followed by quoted CJK string, or quoted CJK string followed by ==/!=
    line2 = re.sub(r"(==|!=|===|!==)\s*" + QSTR.pattern, lambda m: m.group(1) + " " + repl(m.group(0)[len(m.group(1)):].lstrip()) if False else m.group(0), line)
    # Simpler robust approach: mask quoted CJK strings adjacent to comparison ops via scanning
    # (do it with a dedicated regex over the original line)
    masked = line
    protected = []
    def mask_match(s):
        protected.append(s)
        return f"\x00Q{len(protected)-1}\x00"
    # operand on right of ==/!=
    masked = re.sub(r"((?:==|!=|===|!==)\s*)([\"'][^\"'\n]*?" + CJK + r"[^\"'\n]*?[\"'])",
                    lambda m: m.group(1) + mask_match(m.group(2)), masked)
    # operand on left of ==/!=
    masked = re.sub(r"([\"'][^\"'\n]*?" + CJK + r"[^\"'\n]*?[\"'])(\s*(?:==|!=|===|!==))",
                    lambda m: mask_match(m.group(1)) + m.group(2), masked)
    # case "xxx"
    masked = re.sub(r"(\bcase\s+)([\"'][^\"'\n]*?" + CJK + r"[^\"'\n]*?[\"'])",
                    lambda m: m.group(1) + mask_match(m.group(2)), masked)
    # src="..."/href="..." attribute values with CJK
    masked = re.sub(r"((?:src|href)\s*=\s*[\"'][^\"'\n]*?" + CJK + r"[^\"'\n]*?[\"'])",
                    lambda m: mask_match(m.group(1)), masked)
    # URLs containing CJK (http...)
    masked = re.sub(r"https?://[^\s\"']*" + CJK + r"[^\s\"']*",
                    lambda m: mask_match(m.group(0)), masked)
    return masked, protected

def unmask(line, protected):
    for i, orig in enumerate(protected):
        line = line.replace(f"\x00Q{i}\x00", orig)
    return line

def translate_line(line, is_cs):
    s = line.strip()
    if s.startswith("//") or s.startswith("<!--") or s.startswith("@*") or s.startswith("*"):
        return line
    if is_cs and re.search(r"\bselect\s", line, re.IGNORECASE):
        return line
    masked, protected = protect_spans(line)
    for term in sorted(G, key=len, reverse=True):
        if term in masked:
            masked = re.sub(r"(?<![\u4e00-\u9fff])" + re.escape(term) + r"(?![\u4e00-\u9fff])",
                            G[term], masked)
    return unmask(masked, protected)

def process(path, is_cs):
    t = open(path, encoding="utf-8").read()
    lines = t.split("\n")
    out = [translate_line(l, is_cs) for l in lines]
    t2 = "\n".join(out)
    if t2 != t:
        open(path, "w", encoding="utf-8").write(t2)
        return True
    return False

def main():
    nfiles = 0
    for pat, is_cs in [(WEB + "/**/*.cshtml", False), (WEB + "/**/*.cs", True)]:
        for f in glob.glob(pat, recursive=True):
            rel = os.path.relpath(f, WEB)
            if rel in DEAD:
                continue
            if process(f, is_cs):
                nfiles += 1
    # Infra display strings: BottomButton defaults + enum descriptions
    for f in [INF + "/WebCore/BottomButtonViewModel.cs",
              INF + "/Enum/EnumDB.cs", INF + "/Enum/EnumCommon.cs",
              INF + "/Enum/BaseEnums.cs", INF + "/Shims/MissingShims2.cs"]:
        if os.path.exists(f) and process(f, True):
            nfiles += 1
    print(f"translated files: {nfiles}")
    # recount
    tot = 0; fv = 0
    for pat in [WEB + "/**/*.cshtml", WEB + "/**/*.cs"]:
        for f in glob.glob(pat, recursive=True):
            t = open(f, encoding="utf-8", errors="replace").read()
            n = len(re.findall(CJK, t))
            if n: fv += 1; tot += n
    print(f"remaining Web CJK chars: {tot} in {fv} files")

if __name__ == "__main__":
    main()
