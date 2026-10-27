import os, re, sys
W = "/home/hatch/workspace/openhis-core/Newtouch.HIS.CIS/Newtouch.CIS.Web"
D = {
"保存成功": "Lưu thành công", "保存失败": "Lưu thất bại", "删除成功": "Xóa thành công",
"操作成功": "Thao tác thành công", "删除明细": "Xóa chi tiết", "保存数据": "Lưu dữ liệu",
"明细总量": "Tổng số lượng chi tiết", "明细金额和处方金额": "Số tiền chi tiết và số tiền đơn thuốc",
"已开申请单明细": "Chi tiết phiếu đã kê", "申请单总金额": "Tổng số tiền phiếu",
"处方数据发生了变更": "Dữ liệu đơn thuốc đã thay đổi", "缺少医嘱数据": "Thiếu dữ liệu y lệnh",
"缺少单位": "Thiếu đơn vị", "缺少剂量": "Thiếu liều lượng",
"请选中一条信息": "Vui lòng chọn một bản ghi", "请填写数字": "Vui lòng nhập số",
"浏览器窗口调整大小时重新加载": "Khi thay đổi kích thước cửa sổ trình duyệt, tải lại",
"浏览器窗口调整大小时重新计算宽高": "Khi thay đổi kích thước cửa sổ trình duyệt, tính lại rộng cao",
"正在请求报表页面": "Đang tải trang báo cáo", "另存为模板": "Lưu thành mẫu",
"长期医嘱": "Y lệnh dài hạn", "临时医嘱": "Y lệnh tạm thời", "附属医嘱": "Y lệnh phụ",
"新医嘱": "Y lệnh mới", "医嘱类别": "Loại y lệnh", "医嘱内容": "Nội dung y lệnh",
"执行科室": "Khoa thực hiện", "作废处方": "Hủy đơn thuốc", "新处方": "Đơn thuốc mới",
"患者姓名": "Tên bệnh nhân", "患者列表": "Danh sách bệnh nhân", "入院日期": "Ngày nhập viện",
"入院诊断": "Chẩn đoán nhập viện", "中医诊断": "Chẩn đoán YHCT", "西医诊断": "Chẩn đoán Tây y",
"诊断记录": "Hồ sơ chẩn đoán", "药品名称": "Tên thuốc", "项目名称": "Tên mục",
"收费项目": "Mục thu phí", "组套名称": "Tên bộ mẫu", "处方号": "Số đơn thuốc",
"住院号": "Số nhập viện", "门诊号": "Số khám ngoại trú", "病历号": "Số bệnh án",
"开始时间": "Thời gian bắt đầu", "开始日期": "Ngày bắt đầu", "结束日期": "Ngày kết thúc",
"创建时间": "Thời gian tạo", "申请单号": "Số phiếu", "每次治疗量": "Liều mỗi lần điều trị",
"请确认": "Vui lòng xác nhận", "请选择": "Vui lòng chọn", "请稍后": "Vui lòng đợi trong giây lát",
"已收费": "Đã thu phí", "未收费": "Chưa thu phí", "已退费": "Đã hoàn phí",
"转自费": "Chuyển sang tự chi trả", "自费项目": "mục tự chi trả", "自费本": "Sổ tự chi trả",
"暂无开立": "Chưa kê đơn", "先清空明细": "Xóa trắng chi tiết trước", "先保存": "Lưu trước",
"基本信息": "Thông tin cơ bản", "筛选条件": "Điều kiện lọc", "二级菜单": "Menu cấp 2",
"部分列只读": "Một số cột chỉ đọc", "剂量单位": "Đơn vị liều", "生产厂家": "Nhà sản xuất",
"自定义按钮": "Nút tùy chỉnh", "启用行内编辑": "Bật sửa trong dòng", "间隔超过": "Khoảng cách vượt quá",
"天不画连线": "ngày thì không vẽ đường nối", "线条颜色": "Màu đường kẻ", "设置线宽": "Đặt độ rộng nét",
"画线框": "Vẽ khung", "先清空": "Xóa trắng trước", "不可去掉": "Không thể bỏ",
"同步状态": "Trạng thái đồng bộ", "类药品权限": "Quyền thuốc theo loại", "频次浮层": "Lớp nổi tần suất",
"药品浮层": "Lớp nổi thuốc", "嘱托浮层": "Lớp nổi dặn dò", "回车事件": "Sự kiện Enter",
"这种找元素的方法不可取": "Cách tìm phần tử này không nên dùng", "方便后面使用": "Để tiện dùng về sau",
"该页面初始化": "Trang này khởi tạo", "再次打开该页面": "Mở lại trang này", "或属性": "hoặc thuộc tính",
"设置单元格样式": "Đặt kiểu ô", "隔行换色": "Đổi màu xen kẽ dòng", "计算处方金额": "Tính số tiền đơn thuốc",
"格式的数据": "dữ liệu dạng", "住院单位": "Đơn vị nhập viện", "颜色默认": "Màu mặc định",
"门诊日志列表": "Danh sách nhật ký ngoại trú", "通用报表打印": "In báo cáo chung",
"姓名": "Họ tên", "数量": "Số lượng", "单位": "Đơn vị", "名称": "Tên",
"保存": "Lưu", "删除": "Xóa", "查询": "Tra cứu", "全部": "Tất cả",
"去掉": "Bỏ", "剂量": "Liều lượng", "频次": "Tần suất", "单价": "Đơn giá",
"操作": "Thao tác", "规格": "Quy cách", "性别": "Giới tính", "用法": "Cách dùng",
"科室": "Khoa", "金额": "Số tiền", "体温": "Nhiệt độ", "日期": "Ngày",
"默认": "Mặc định", "呼吸": "Hô hấp", "拼音": "Pinyin", "编码": "Mã",
"有效": "Hiệu lực", "年龄": "Tuổi", "修改": "Sửa", "隐藏": "Ẩn",
"开关": "Công tắc", "医嘱": "Y lệnh", "脉搏": "Mạch", "天数": "Số ngày",
"备注": "Ghi chú", "状态": "Trạng thái", "药品": "Thuốc", "作废": "Hủy bỏ",
"关闭": "Đóng", "主键": "Khóa chính", "没有": "Không có", "居中": "Căn giữa",
"重置": "Đặt lại", "疑似": "Nghi ngờ", "有效期": "Hạn hiệu lực", "按钮": "Nút",
"批次": "Lô", "打印": "In", "计算": "Tính toán", "床号": "Số giường",
"组套": "Bộ mẫu", "血压": "Huyết áp", "卡号": "Số thẻ", "嘱托": "Dặn dò",
"批号": "Số lô", "医生": "Bác sĩ", "检验": "Xét nghiệm", "新增": "Thêm mới",
"执行": "Thực hiện", "启用": "Kích hoạt", "清空": "Xóa trắng", "临时": "Tạm thời",
"长期": "Dài hạn", "检查": "Kiểm tra", "返回": "Quay lại", "属性": "Thuộc tính",
"停止": "Dừng", "病区": "Khu bệnh", "选中": "Đã chọn", "结束": "Kết thúc",
"部位": "Vị trí", "创建人": "Người tạo", "禁用": "Vô hiệu hóa", "编号": "Số hiệu",
"新建": "Tạo mới", "类别": "Loại", "门诊": "Ngoại trú", "项目": "Mục",
"添加": "Thêm", "组号": "Số nhóm", "空床": "Giường trống", "刷新": "Làm mới",
"说明": "Mô tả", "总量": "Tổng lượng", "报损": "Báo hỏng", "使用": "Sử dụng",
"根据": "Theo", "获取": "Lấy", "赋值": "Gán giá trị", "筛选": "Lọc",
"体温单": "Phiếu nhiệt độ", "申请单": "Phiếu", "就诊": "Khám bệnh",
"处方": "Đơn thuốc", "费用": "Chi phí", "收费": "Thu phí",
"退费": "Hoàn phí", "自费": "tự chi trả", "医保": "Bảo hiểm y tế",
"患者": "Bệnh nhân", "住院": "Nhập viện", "出院": "Xuất viện",
"入院": "Nhập viện", "诊断": "Chẩn đoán", "治疗": "Điều trị",
"手术": "Phẫu thuật", "护士": "Điều dưỡng", "药房": "Nhà thuốc",
"库存": "Tồn kho", "仓库": "Kho", "供应商": "Nhà cung cấp",
"采购": "Mua hàng", "入库": "Nhập kho", "出库": "Xuất kho",
"盘点": "Kiểm kê", "报表": "Báo cáo", "统计": "Thống kê",
"系统": "Hệ thống", "设置": "Cài đặt", "管理": "Quản lý",
"用户": "Người dùng", "角色": "Vai trò", "权限": "Quyền",
"登录": "Đăng nhập", "退出": "Thoát", "密码": "Mật khẩu",
"账号": "Tài khoản", "成功": "thành công", "失败": "thất bại",
"错误": "Lỗi", "警告": "Cảnh báo", "提示": "Nhắc nhở",
"确定": "Xác nhận", "取消": "Hủy", "提交": "Gửi",
"开始": "Bắt đầu", "时间": "Thời gian", "年月日": "Ngày tháng năm",
}
# longest-first replacement; protect comments: @* *@, <!-- -->, //line (not after :), /* */
COMMENT = re.compile(r"(@\*.*?\*@|<!--.*?-->|(?<!:)//[^\n]*|/\*.*?\*/)", re.DOTALL)
keys = sorted(D, key=len, reverse=True)
changed_files = 0
replaced = 0
for root, _, files in os.walk(W):
    if "bin" in root or "obj" in root: continue
    for fn in files:
        if not (fn.endswith(".cshtml") or fn.endswith(".cs")): continue
        p = os.path.join(root, fn)
        s = open(p, encoding="utf-8-sig", errors="ignore").read()
        parts = COMMENT.split(s)
        out = []
        n = 0
        for i, part in enumerate(parts):
            if i % 2 == 1:  # comment segment
                out.append(part)
            else:
                for k in keys:
                    if k in part:
                        c = part.count(k)
                        part = part.replace(k, D[k])
                        n += c
                out.append(part)
        if n:
            open(p, "w", encoding="utf-8").write("".join(out))
            changed_files += 1
            replaced += n
print("files changed:", changed_files, "| replacements:", replaced)
