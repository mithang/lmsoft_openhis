#!/usr/bin/env python3
# EMR cshtml/controller Vietnamese translation pass. Guards: comments, value/name/id/
# data-field attributes, ==/!= comparison strings, font-family values, URLs, object keys
# are NEVER replaced; every skip is logged. Run: python3 emr-translate.py [--dry]
import re, os, sys, collections

ROOT = "/home/hatch/workspace/openhis-core/Newtouch.HIS.EMR/Newtouch.EMR.Web"
CJK = re.compile(r'[\u4e00-\u9fff]+')
DRY = "--dry" in sys.argv

# Strings that must NEVER be translated (comparison/logic values, font names, proper nouns)
SKIP_EXACT = {
    "宋体", "黑体", "楷体", "微软雅黑",  # font-family names
    "数字文本",  # nodeName value assigned in MedicalOpenEditor
    "住院证",  # demo filename 住院证.xml in dead DCSoft demo page
    "南京都昌信息科技有限公司", "湖北天天数链技术有限公司", "天天开源软件",
    "广东省惠亚", "张三", "李四主任医师",
    "中国",  # in "中国..." proper-noun contexts handled below; standalone kept only when part of names - handled by dict? keep skip for safety in lists
}
# Proper-noun demo template/hospital names (displayed template names in lists) - left, logged
PROPER_PREFIXES = ("某医院", "某", "常州市", "福建孟超", "三门县", "沈阳军区", "佛山市",
                   "南京机关", "天津人民", "南京都昌", "中华人民共和国", "藏文")

D = {}
def add(pairs):
    for k, v in pairs: D[k] = v

add([
# ---- generic UI ----
("请选择", "Vui lòng chọn"), ("请选中一条信息", "Vui lòng chọn một bản ghi"),
("患者信息", "Thông tin bệnh nhân"), ("患者姓名", "Tên bệnh nhân"), ("患者列表", "Danh sách bệnh nhân"),
("病人姓名", "Tên bệnh nhân"), ("病人简述", "Tóm tắt bệnh nhân"), ("病人性质", "Tính chất bệnh nhân"),
("我的病人", "Bệnh nhân của tôi"), ("选择患者", "Chọn bệnh nhân"), ("请选择患者", "Vui lòng chọn bệnh nhân"),
("请选择住院患者", "Vui lòng chọn bệnh nhân nội trú"), ("请选择病历模板", "Vui lòng chọn mẫu bệnh án"),
("请选择病历文书", "Vui lòng chọn văn bản bệnh án"), ("请选择诊断", "Vui lòng chọn chẩn đoán"),
("请选择科室", "Vui lòng chọn khoa"), ("请选择要删除的词条", "Vui lòng chọn từ cần xóa"),
("请输入名称", "Vui lòng nhập tên"), ("请输入编码", "Vui lòng nhập mã"), ("请输入时间", "Vui lòng nhập thời gian"),
("请输入模板名称", "Vui lòng nhập tên mẫu"), ("请输入医生工号", "Vui lòng nhập mã nhân viên bác sĩ"),
("请输入模板路径", "Vui lòng nhập đường dẫn mẫu"),
("姓名", "Họ tên"), ("性别", "Giới tính"), ("男", "Nam"), ("女", "Nữ"), ("年龄", "Tuổi"),
("出生年月", "Ngày tháng năm sinh"), ("出生地", "Nơi sinh"), ("民族", "Dân tộc"), ("汉族", "Dân tộc Hán"),
("婚姻", "Hôn nhân"), ("婚姻状况", "Tình trạng hôn nhân"), ("未婚", "Chưa kết hôn"), ("已婚", "Đã kết hôn"),
("丧偶", "Góa"), ("职业", "Nghề nghiệp"), ("电话", "Điện thoại"), ("联系人", "Người liên hệ"),
("地址", "Địa chỉ"), ("邮编", "Mã bưu chính"), ("卡号", "Số thẻ"), ("工号", "Mã nhân viên"),
("医生名称", "Tên bác sĩ"), ("医生工号", "Mã nhân viên bác sĩ"), ("医生", "Bác sĩ"),
("主刀医生", "Bác sĩ phẫu thuật chính"), ("麻醉医生", "Bác sĩ gây mê"),
("住院号", "Số nhập viện"), ("住院信息", "Thông tin nhập viện"), ("住院病历", "Bệnh án nội trú"),
("住院", "Nội trú"), ("住院总费用", "Tổng chi phí nội trú"), ("门诊号", "Số khám ngoại trú"),
("门诊", "Ngoại trú"), ("在院", "Đang nằm viện"), ("出院", "Ra viện"), ("出院天数", "Số ngày ra viện"),
("出院情况", "Tình hình ra viện"), ("出院诊断", "Chẩn đoán ra viện"), ("出院患者添加可提交按钮", "Bệnh nhân ra viện được thêm nút nộp"),
("入院记录", "Hồ sơ nhập viện"), ("入院时间", "Thời gian nhập viện"), ("入院日期", "Ngày nhập viện"),
("入院诊断", "Chẩn đoán nhập viện"), ("入院途径", "Đường nhập viện"), ("入院病情", "Tình trạng bệnh lúc nhập viện"),
("入院前", "Trước khi nhập viện"), ("入院后", "Sau khi nhập viện"), ("入院病例", "Ca bệnh nhập viện"),
("出院病例", "Ca bệnh ra viện"), ("取消入院", "Hủy nhập viện"), ("转区", "Chuyển khu"), ("转科", "Chuyển khoa"),
("床号", "Số giường"), ("科室", "Khoa"), ("科别", "Chuyên khoa"), ("病区", "Khu bệnh"),
("所属科室", "Khoa trực thuộc"), ("所属医生", "Bác sĩ phụ trách"),
("病历", "Bệnh án"), ("病历管理", "Quản lý bệnh án"), ("病历质控", "Kiểm soát chất lượng bệnh án"),
("病历新建", "Tạo mới bệnh án"), ("病历名称", "Tên bệnh án"), ("病历类型", "Loại bệnh án"),
("病历类型标识", "Mã định danh loại bệnh án"), ("病历大类", "Nhóm lớn bệnh án"),
("病历大类标识", "Mã định danh nhóm lớn bệnh án"), ("病历状态", "Trạng thái bệnh án"),
("病历日期", "Ngày bệnh án"), ("病历时间", "Thời gian bệnh án"), ("病历号", "Số bệnh án"),
("病历操作", "Thao tác bệnh án"), ("病历编辑器", "Trình soạn thảo bệnh án"),
("病历正在编辑", "Bệnh án đang được chỉnh sửa"), ("病历保存中", "Đang lưu bệnh án"),
("病历保存成功", "Lưu bệnh án thành công"), ("病历保存失败", "Lưu bệnh án thất bại"),
("病历已提交", "Bệnh án đã nộp"), ("病历退回", "Bệnh án bị trả lại"), ("病历修改", "Sửa bệnh án"),
("病历提交", "Nộp bệnh án"), ("病历删除", "Xóa bệnh án"), ("删除病历", "Xóa bệnh án"),
("修改病历", "Sửa bệnh án"), ("编辑病历", "Chỉnh sửa bệnh án"), ("浏览病历", "Xem bệnh án"),
("新增病历记录", "Thêm bản ghi bệnh án"), ("添加病历记录", "Thêm bản ghi bệnh án"),
("病历关系", "Quan hệ bệnh án"), ("患者病历列表树", "Cây danh sách bệnh án bệnh nhân"),
("患者病历已提交病案", "Bệnh án bệnh nhân đã nộp cho phòng hồ sơ"),
("在院患者不提交病案", "Bệnh nhân đang nằm viện không nộp bệnh án"),
("病案首页不能重复新建", "Trang đầu bệnh án không được tạo trùng"),
("病案首页", "Trang đầu bệnh án"), ("病案号", "Số hồ sơ bệnh án"), ("病案质量", "Chất lượng hồ sơ bệnh án"),
("病案首页分离", "Tách trang đầu bệnh án"), ("提交病案", "Nộp hồ sơ bệnh án"),
("是否提交病案", "Đã nộp hồ sơ bệnh án"), ("已提交", "Đã nộp"), ("未提交", "Chưa nộp"),
("提交日期", "Ngày nộp"), ("提交", "Nộp"), ("退回", "Trả lại"), ("退", "Trả lại"), ("签", "Ký"),
("解锁病历", "Mở khóa bệnh án"), ("解锁成功", "Mở khóa thành công"), ("尚未锁定", "Chưa bị khóa"),
("病程记录", "Ghi chép diễn biến bệnh"), ("首次病程", "Diễn biến bệnh lần đầu"),
("病程合并预览", "Xem trước gộp diễn biến bệnh"), ("合并打印", "In gộp"),
("护理记录", "Ghi chép điều dưỡng"), ("护理记录单", "Phiếu ghi chép điều dưỡng"),
("护理级别", "Cấp độ điều dưỡng"), ("护理等级", "Cấp độ điều dưỡng"), ("护理类型", "Loại điều dưỡng"),
("特级护理", "Điều dưỡng đặc biệt"), ("一级护理", "Điều dưỡng cấp I"), ("二级护理", "Điều dưỡng cấp II"),
("三级护理", "Điều dưỡng cấp III"), ("基础护理", "Điều dưỡng cơ bản"), ("特殊护理", "Điều dưỡng đặc biệt"),
("护士签名", "Chữ ký điều dưỡng"), ("医师签名", "Chữ ký bác sĩ"), ("已签名", "Đã ký"), ("未签名", "Chưa ký"),
("诊断", "Chẩn đoán"), ("诊断类型", "Loại chẩn đoán"), ("诊断列表", "Danh sách chẩn đoán"),
("诊断信息保存成功", "Lưu thông tin chẩn đoán thành công"), ("主要诊断", "Chẩn đoán chính"),
("次要诊断", "Chẩn đoán phụ"), ("病理诊断", "Chẩn đoán bệnh lý"), ("疾病编码", "Mã bệnh"),
("疾病史", "Tiền sử bệnh"), ("过敏史", "Tiền sử dị ứng"), ("过敏药物", "Thuốc dị ứng"),
("月经史", "Tiền sử kinh nguyệt"), ("手术外伤史", "Tiền sử phẫu thuật/chấn thương"),
("输血史", "Tiền sử truyền máu"), ("传染病史", "Tiền sử bệnh truyền nhiễm"),
("预防接种史", "Tiền sử tiêm chủng"), ("预防接种药品", "Thuốc tiêm chủng"),
("手术", "Phẫu thuật"), ("手术列表", "Danh sách phẫu thuật"), ("手术保存成功", "Lưu phẫu thuật thành công"),
("手术安排表", "Bảng sắp xếp phẫu thuật"), ("择期手术", "Phẫu thuật theo lịch"),
("切口部位", "Vị trí vết mổ"), ("切口愈合等级", "Mức độ lành vết mổ"),
("伤口裂开", "Vết thương toác ra"), ("出血或血肿", "Chảy máu hoặc tụ máu"),
("麻醉方式", "Phương pháp gây mê"), ("麻醉分级", "Cấp độ gây mê"), ("麻醉开始时间", "Thời gian bắt đầu gây mê"),
("麻醉护理记录单", "Phiếu ghi chép gây mê"), ("术前预防性", "Dự phòng trước mổ"),
("抗菌药物使用情况", "Tình hình sử dụng kháng sinh"), ("抗菌药物名称", "Tên kháng sinh"),
("抗菌药物费用", "Chi phí kháng sinh"), ("抗菌药物给药时间", "Thời gian dùng kháng sinh"),
("医嘱", "Y lệnh"), ("医嘱内容", "Nội dung y lệnh"), ("医嘱类别", "Loại y lệnh"),
("医嘱类型", "Kiểu y lệnh"), ("医嘱状态", "Trạng thái y lệnh"), ("医嘱单", "Phiếu y lệnh"),
("医嘱离院", "Y lệnh ra viện"), ("医嘱插入文书", "Chèn y lệnh vào văn bản"),
("长期医嘱", "Y lệnh dài hạn"), ("临时医嘱", "Y lệnh tạm thời"), ("临时医嘱单", "Phiếu y lệnh tạm thời"),
("出院带药", "Thuốc mang về khi ra viện"), ("膳食医嘱", "Y lệnh ăn uống"), ("中草药", "Thuốc đông y"),
("药品", "Thuốc"), ("文字", "Văn bản"), ("康复", "Phục hồi chức năng"),
("请至少选择一条医嘱导出", "Vui lòng chọn ít nhất một y lệnh để xuất"),
("体温", "Nhiệt độ"), ("脉率", "Mạch"), ("呼吸频率", "Tần số thở"), ("血氧饱和度", "Độ bão hòa oxy máu"),
("收缩压", "Huyết áp tâm thu"), ("舒张压", "Huyết áp tâm trương"), ("血压测量单位", "Đơn vị đo huyết áp"),
("体重", "Cân nặng"), ("新生儿入院体重", "Cân nặng nhập viện của trẻ sơ sinh"),
("新生儿体重范围为", "Khoảng cân nặng trẻ sơ sinh là"), ("新生儿年龄小于等于", "Tuổi trẻ sơ sinh nhỏ hơn hoặc bằng"),
("摄入量", "Lượng đưa vào"), ("摄出量", "Lượng thải ra"), ("摄入量录入", "Nhập lượng đưa vào"),
("摄出量录入", "Nhập lượng thải ra"), ("摄入摄出测量单位", "Đơn vị đo lượng vào/ra"),
("吸氧", "Thở oxy"), ("吸痰", "Hút đờm"), ("瞳孔", "Đồng tử"), ("对光反应", "Phản xạ với ánh sáng"),
("光反应", "Phản xạ ánh sáng"), ("意识", "Ý thức"), ("皮肤护理", "Chăm sóc da"), ("体位护理", "Chăm sóc tư thế"),
("营养护理", "Chăm sóc dinh dưỡng"), ("气管护理", "Chăm sóc khí quản"), ("翻身拍背", "Trở mình vỗ lưng"),
("雾化吸入", "Khí dung"), ("饮食情况", "Tình hình ăn uống"), ("饮食指导", "Hướng dẫn ăn uống"),
("普通饮食", "Ăn thường"), ("软食", "Ăn mềm"), ("半流食", "Ăn nửa lỏng"), ("流食", "Ăn lỏng"),
("禁食", "Nhịn ăn"), ("禁食水", "Nhịn ăn uống"), ("鼻饲饮食", "Ăn qua sonde dạ dày"),
("低盐低脂饮食", "Ăn ít muối ít mỡ"), ("糖尿病饮食", "Ăn cho bệnh tiểu đường"),
("心理护理", "Chăm sóc tâm lý"), ("根据病人心理状况实施心理护理", "Chăm sóc tâm lý theo tình trạng tâm lý bệnh nhân"),
("家属心理支持", "Hỗ trợ tâm lý người nhà"), ("安全护理", "Chăm sóc an toàn"),
("勤巡视病房", "Thường xuyên thăm buồng bệnh"), ("加床档", "Chặn giường thêm"),
("约束四肢", "Cố định tứ chi"), ("发出手术安全核对表", "Phát bảng kiểm tra an toàn phẫu thuật"),
("收回手术安全核对表", "Thu hồi bảng kiểm tra an toàn phẫu thuật"),
("发出手术风险评估表", "Phát bảng đánh giá rủi ro phẫu thuật"),
("收回手术风险评估表", "Thu hồi bảng đánh giá rủi ro phẫu thuật"),
("隔离种类", "Loại cách ly"), ("隔离标志", "Dấu hiệu cách ly"), ("消化道隔离", "Cách ly đường tiêu hóa"),
("呼吸道隔离", "Cách ly đường hô hấp"), ("接触隔离", "Cách ly tiếp xúc"),
("血液", "Máu"), ("体液隔离", "Cách ly dịch cơ thể"), ("严密隔离", "Cách ly nghiêm ngặt"),
("昆虫隔离", "Cách ly côn trùng"), ("保护性隔离", "Cách ly bảo vệ"),
("病情观察及措施", "Theo dõi tình trạng bệnh và biện pháp"), ("简要病情", "Tóm tắt tình trạng bệnh"),
("护理操作名称", "Tên thao tác điều dưỡng"), ("护理观察项目名称", "Tên hạng mục quan sát điều dưỡng"),
("护理操作项目类目名称", "Tên nhóm hạng mục thao tác điều dưỡng"),
("护理操作结果", "Kết quả thao tác điều dưỡng"), ("导管护理描述", "Mô tả chăm sóc ống thông"),
("辩证施护", "Biện chứng thi hộ"), ("危重", "Nguy kịch"),
("触诊", "Sờ nắn"), ("叩诊", "Gõ"), ("听诊", "Nghe"), ("检查结果", "Kết quả kiểm tra"),
("氏征", "Dấu hiệu"), ("肛门指诊检查结果描述", "Mô tả kết quả thăm trực tràng bằng ngón tay"),
("外生殖器检查结果描述", "Mô tả kết quả kiểm tra cơ quan sinh dục ngoài"),
("足背动脉搏动", "Mạch động mạch mu chân"), ("颜色形状", "Màu sắc hình dạng"),
("西医", "Tây y"), ("中医", "Đông y"), ("中医治疗费", "Chi phí điều trị đông y"),
("中成药费", "Chi phí thuốc thành phẩm đông y"), ("西药费", "Chi phí thuốc tây"),
("血费", "Chi phí máu"), ("白蛋白类制品费", "Chi phí chế phẩm albumin"),
("球蛋白类制品费", "Chi phí chế phẩm globulin"), ("凝血因子类制品费", "Chi phí chế phẩm yếu tố đông máu"),
("细胞因子类制品费", "Chi phí chế phẩm cytokine"),
("检查用一次性医用材料费", "Chi phí vật tư y tế dùng một lần cho kiểm tra"),
("治疗用一次性医用材料费", "Chi phí vật tư y tế dùng một lần cho điều trị"),
("手术用一次性医用材料费", "Chi phí vật tư y tế dùng một lần cho phẫu thuật"),
("一般医疗服务费", "Phí dịch vụ y tế chung"), ("一般治疗操作费", "Phí thao tác điều trị chung"),
("护理费", "Phí điều dưỡng"), ("其他费用", "Chi phí khác"), ("其他费", "Phí khác"),
("病理诊断费", "Phí chẩn đoán bệnh lý"), ("实验室诊断费", "Phí chẩn đoán xét nghiệm"),
("影像学诊断费", "Phí chẩn đoán hình ảnh"), ("临床诊断项目费", "Phí hạng mục chẩn đoán lâm sàng"),
("非手术治疗项目费", "Phí hạng mục điều trị không phẫu thuật"),
("临床物理治疗费", "Phí vật lý trị liệu lâm sàng"), ("手术治疗费", "Phí điều trị phẫu thuật"),
("麻醉费", "Phí gây mê"), ("手术费", "Phí phẫu thuật"), ("康复费", "Phí phục hồi chức năng"),
("自付金额", "Số tiền tự trả"), ("其他支付", "Thanh toán khác"), ("同步费用", "Đồng bộ chi phí"),
("费用信息", "Thông tin chi phí"), ("金额", "Số tiền"),
("检验", "Xét nghiệm"), ("检查", "Kiểm tra"), ("检查报告", "Báo cáo kiểm tra"),
("请至少选择一行检验结果导出", "Vui lòng chọn ít nhất một dòng kết quả xét nghiệm để xuất"),
("当前状态不是已完成状态不能查看报告", "Trạng thái hiện tại chưa hoàn thành nên không xem được báo cáo"),
("已完成", "Đã hoàn thành"), ("查看报告", "Xem báo cáo"), ("结果导入", "Nhập kết quả"),
("申请单时间范围", "Khoảng thời gian phiếu yêu cầu"), ("申请单号", "Số phiếu yêu cầu"),
("申请时间", "Thời gian yêu cầu"), ("申请单状态", "Trạng thái phiếu yêu cầu"),
("请双击一条已完成的数据来查看明细", "Vui lòng nhấp đúp một dòng dữ liệu đã hoàn thành để xem chi tiết"),
("超声诊断书", "Phiếu chẩn đoán siêu âm"), ("化验申请单", "Phiếu yêu cầu xét nghiệm"),
("血糖录入单", "Phiếu nhập đường huyết"), ("影像申请单", "Phiếu yêu cầu chẩn đoán hình ảnh"),
("评估文书", "Văn bản đánh giá"), ("待产记录表", "Bảng ghi chép chờ sinh"),
("量表", "Thang đánh giá"), ("住院志", "Hồ sơ nằm viện"),
])

add([
# ---- generic actions / grid / forms ----
("删除", "Xóa"), ("删除成功", "Xóa thành công"), ("确定要删除吗", "Bạn có chắc chắn muốn xóa không?"),
("确定删除该病历", "Xác nhận xóa bệnh án này"), ("删除选中行", "Xóa dòng đã chọn"),
("删除模板", "Xóa mẫu"), ("删除当前输入域", "Xóa trường nhập hiện tại"),
("删除表格", "Xóa bảng"), ("删除表格行", "Xóa dòng bảng"), ("删除行", "Xóa dòng"), ("删除列", "Xóa cột"),
("修改", "Sửa"), ("修改成功", "Sửa thành công"), ("修改日期", "Ngày sửa"),
("编辑", "Chỉnh sửa"), ("编辑词条", "Chỉnh sửa từ"), ("导入词条", "Nhập từ"),
("创建目录", "Tạo thư mục"), ("是否创建目录", "Có tạo thư mục không?"),
("当前未选择目录", "Hiện chưa chọn thư mục"), ("词条", "Từ"), ("词", "Từ"), ("条", "Dòng"),
("新增", "Thêm mới"), ("新增行", "Thêm dòng"), ("添加", "Thêm"), ("新建", "Tạo mới"),
("保存", "Lưu"), ("保存成功", "Lưu thành công"), ("保存失败", "Lưu thất bại"),
("保存文档", "Lưu tài liệu"), ("保存病历", "Lưu bệnh án"), ("保存病历模板", "Lưu mẫu bệnh án"),
("病历模板保存成功", "Lưu mẫu bệnh án thành công"), ("病历模板保存失败", "Lưu mẫu bệnh án thất bại"),
("保存基本信息", "Lưu thông tin cơ bản"), ("保存诊断", "Lưu chẩn đoán"), ("保存手术", "Lưu phẫu thuật"),
("完整病案保存", "Lưu toàn bộ hồ sơ bệnh án"), ("保存权限", "Lưu quyền"),
("保存时验证", "Kiểm tra khi lưu"), ("保存成功回调", "Gọi lại sau khi lưu thành công"),
("保存为个人模板", "Lưu thành mẫu cá nhân"), ("新增时显示", "Hiển thị khi thêm mới"),
("连续录入", "Nhập liên tục"), ("确认并关闭按钮", "Nút xác nhận và đóng"),
("操作", "Thao tác"), ("操作成功", "Thao tác thành công"), ("操作失败", "Thao tác thất bại"),
("操作失败，关键信息不可为空", "Thao tác thất bại, thông tin quan trọng không được để trống"),
("关键信息不可为空", "Thông tin quan trọng không được để trống"),
("取消", "Hủy"), ("确定", "Xác nhận"), ("关闭", "Đóng"), ("是否关闭页面", "Có đóng trang không?"),
("打开", "Mở"), ("打开窗口编辑", "Mở cửa sổ chỉnh sửa"), ("打印", "In"), ("打印预览", "Xem trước khi in"),
("打印预览后", "Sau khi xem trước khi in"), ("预览打印", "Xem trước và in"), ("预览副页打印", "Xem trước in trang phụ"),
("直接打印", "In trực tiếp"), ("取消预览", "Hủy xem trước"), ("续打", "In tiếp"),
("导出为", "Xuất ra"), ("导出为图片", "Xuất ra hình ảnh"), ("请查看日志", "Vui lòng xem nhật ký"),
("权限不足", "Không đủ quyền"), ("无权限修改", "Không có quyền sửa"),
("请重新登录", "Vui lòng đăng nhập lại"), ("登录", "Đăng nhập"),
("正在提交数据", "Đang gửi dữ liệu"), ("正在请求上传", "Đang yêu cầu tải lên"), ("请稍候", "Vui lòng đợi"),
("正在同步数据", "Đang đồng bộ dữ liệu"), ("同步成功", "Đồng bộ thành công"), ("同步数据", "Đồng bộ dữ liệu"),
("刷新列表", "Làm mới danh sách"), ("重新加载当前表格", "Tải lại bảng hiện tại"),
("查询", "Tìm kiếm"), ("查询事件", "Sự kiện tìm kiếm"), ("回车触发查询事件", "Nhấn Enter để tìm kiếm"),
("回车事件", "Sự kiện Enter"), ("重置", "Đặt lại"), ("搜索", "Tìm kiếm"),
("上一页", "Trang trước"), ("下一页", "Trang sau"), ("上一步", "Bước trước"), ("下一步", "Bước sau"),
("完成", "Hoàn thành"), ("当前页", "Trang hiện tại"), ("共", "Tổng cộng"), ("第", "Thứ"),
("页", "Trang"), ("行", "Dòng"), ("列", "Cột"), ("至", "đến"), ("年", "Năm"), ("月", "Tháng"),
("天", "Ngày"), ("小时", "Giờ"), ("分钟", "Phút"), ("个月", "Tháng"), ("周岁", "Tuổi tròn"),
("开始时间", "Thời gian bắt đầu"), ("开始日期", "Ngày bắt đầu"), ("结束时间", "Thời gian kết thúc"),
("结束日期", "Ngày kết thúc"), ("出区日期", "Ngày ra khỏi khu"), ("出区开始日期", "Ngày bắt đầu ra khỏi khu"),
("记录时间", "Thời gian ghi"), ("记录日期及时间备注不可为空", "Ghi chú ngày và giờ ghi không được để trống"),
("创建时间", "Thời gian tạo"), ("创建日期", "Ngày tạo"), ("创建人", "Người tạo"),
("最后修改", "Sửa đổi lần cuối"), ("最后修改时间", "Thời gian sửa đổi lần cuối"),
("更新日期", "Ngày cập nhật"), ("修改配置", "Sửa cấu hình"), ("配置名称", "Tên cấu hình"), ("配置编码", "Mã cấu hình"),
("状态", "Trạng thái"), ("有效", "Hiệu lực"), ("启用", "Bật"), ("只读权限", "Quyền chỉ đọc"),
("读写权限", "Quyền đọc/ghi"), ("启用则可选", "Bật thì có thể chọn"),
("名称", "Tên"), ("代码", "Mã"), ("编码", "Mã"), ("模板编码", "Mã mẫu"),
("字段名称", "Tên trường"), ("字段类型", "Kiểu trường"), ("字符串", "Chuỗi ký tự"), ("数字", "Số"),
("使用标志", "Cờ sử dụng"), ("路径", "Đường dẫn"), ("模板路径", "Đường dẫn mẫu"),
("引用路径", "Đường dẫn tham chiếu"), ("来源", "Nguồn"), ("类别", "Loại"), ("类型", "Loại"),
("业务类型", "Loại nghiệp vụ"), ("模板类型", "Loại mẫu"), ("模板性质", "Tính chất mẫu"),
("空模板", "Mẫu trống"), ("定制模板", "Mẫu tùy chỉnh"), ("模板", "Mẫu"),
("病历模板", "Mẫu bệnh án"), ("新建病历模板", "Tạo mới mẫu bệnh án"),
("模板基本信息", "Thông tin cơ bản của mẫu"), ("模板控制", "Điều khiển mẫu"),
("请维护模板相关信息", "Vui lòng bảo trì thông tin liên quan đến mẫu"),
("用于创建或修改病历文书", "Dùng để tạo hoặc sửa văn bản bệnh án"),
("模板大类", "Nhóm lớn mẫu"), ("双击病历大类处理", "Nhấp đúp nhóm lớn bệnh án để xử lý"),
("右侧元素", "Phần tử bên phải"), ("设置默认内容", "Đặt nội dung mặc định"),
("开始制作模板内容", "Bắt đầu soạn nội dung mẫu"), ("这是一个按钮", "Đây là một nút"),
("按钮", "Nút"), ("复选框文本", "Văn bản hộp kiểm"), ("元素", "Phần tử"),
("元素名称", "Tên phần tử"), ("元素类型", "Loại phần tử"), ("元素控件", "Điều khiển phần tử"),
("元素属性", "Thuộc tính phần tử"), ("下拉", "Danh sách thả xuống"), ("下拉多选", "Chọn nhiều từ danh sách"),
("下拉列表绑定", "Liên kết danh sách thả xuống"), ("数据源", "Nguồn dữ liệu"),
("绑定字典", "Liên kết từ điển"), ("设置自定义下拉列表", "Đặt danh sách thả xuống tùy chỉnh"),
("设置默认值", "Đặt giá trị mặc định"), ("默认值", "Giá trị mặc định"), ("文本", "Văn bản"),
("选项", "Tùy chọn"), ("选项卡", "Thẻ tab"), ("单选", "Chọn một"), ("多选", "Chọn nhiều"),
("复选框", "Hộp kiểm"), ("单选框", "Nút radio"), ("插入单选框", "Chèn nút radio"),
("插入复选框", "Chèn hộp kiểm"), ("插入单选复选分组", "Chèn nhóm radio/hộp kiểm"),
("插入文本输入域", "Chèn trường nhập văn bản"), ("插入年龄输入域", "Chèn trường nhập tuổi"),
("插入性别输入域", "Chèn trường nhập giới tính"), ("插入日期输入域", "Chèn trường nhập ngày"),
("插入按钮", "Chèn nút"), ("插入图片", "Chèn hình ảnh"), ("插入表格", "Chèn bảng"),
("插入批注", "Chèn chú thích"), ("插入日期", "Chèn ngày"), ("插入时间", "Chèn thời gian"),
("插入行", "Chèn dòng"), ("插入列", "Chèn cột"), ("插入特殊字符", "Chèn ký tự đặc biệt"),
("插入分隔符", "Chèn dấu phân cách"), ("插入超链接", "Chèn siêu liên kết"), ("超链接", "Siêu liên kết"),
("取消超链接", "Hủy siêu liên kết"), ("插入页码", "Chèn số trang"),
("插入自定义下拉菜单", "Chèn menu thả xuống tùy chỉnh"), ("插入的医学表达式", "Biểu thức y học đã chèn"),
("插入医学表达式", "Chèn biểu thức y học"), ("插入二维码", "Chèn mã QR"),
("表格属性", "Thuộc tính bảng"), ("表格行属性", "Thuộc tính dòng bảng"),
("表格前插入行", "Chèn dòng trước bảng"), ("在上面插入行", "Chèn dòng phía trên"),
("在下面插入行", "Chèn dòng phía dưới"), ("拆分成行", "Tách thành dòng"), ("拆分成列", "Tách thành cột"),
("右合并单元格", "Gộp ô sang phải"), ("下合并单元格", "Gộp ô xuống dưới"),
("单元格属性", "Thuộc tính ô"), ("边框和底纹", "Viền và nền"), ("标题层次", "Cấp tiêu đề"),
("段落", "Đoạn văn"), ("段前距", "Khoảng cách trước đoạn"), ("段后距", "Khoảng cách sau đoạn"),
("行间距", "Khoảng cách dòng"), ("首行缩进", "Thụt đầu dòng"), ("页面设置", "Cài đặt trang"),
("页面边距", "Lề trang"), ("上边距", "Lề trên"), ("下边距", "Lề dưới"),
("左边距", "Lề trái"), ("右边距", "Lề phải"), ("纵向", "Dọc"), ("横向", "Ngang"),
("表单视图", "Chế độ xem biểu mẫu"), ("阅读视图", "Chế độ xem đọc"),
("表单模式", "Chế độ biểu mẫu"), ("留痕模式", "Chế độ lưu dấu vết"), ("自动缩放模式", "Chế độ tự động thu phóng"),
("字体", "Phông chữ"), ("字体大小", "Cỡ chữ"), ("字体颜色", "Màu chữ"), ("清除字号", "Xóa cỡ chữ"),
("清除字体", "Xóa phông chữ"), ("打印字体处理", "Xử lý phông chữ khi in"),
("加粗", "Đậm"), ("粗体", "Đậm"), ("斜体", "Nghiêng"), ("删除线", "Gạch ngang"),
("下划线", "Gạch chân"), ("下划线样式", "Kiểu gạch chân"), ("上标", "Chỉ số trên"),
("下标", "Chỉ số dưới"), ("大写", "Chữ hoa"), ("小写", "Chữ thường"),
("字符边框", "Viền ký tự"), ("背景色", "Màu nền"), ("颜色", "Màu sắc"), ("通用颜色", "Màu chung"),
("标准颜色", "Màu chuẩn"), ("剪切", "Cắt"), ("粘贴", "Dán"), ("纯文本粘贴", "Dán văn bản thuần"),
("复制", "Sao chép"), ("撤销", "Hoàn tác"), ("重做", "Làm lại"), ("格式刷", "Sao chép định dạng"),
("简体转繁体", "Chuyển giản thể sang phồn thể"), ("繁体转简体", "Chuyển phồn thể sang giản thể"),
("有序列表", "Danh sách có thứ tự"), ("无序列表", "Danh sách không thứ tự"),
("居左对齐", "Căn trái"), ("居右对齐", "Căn phải"), ("居中对齐", "Căn giữa"),
("居左", "Căn trái"), ("居右", "Căn phải"), ("居中", "Căn giữa"),
("放大", "Phóng to"), ("缩小", "Thu nhỏ"), ("查找和替换", "Tìm và thay thế"),
("字数统计", "Đếm số từ"), ("清除全部痕迹", "Xóa toàn bộ dấu vết"),
("工具栏", "Thanh công cụ"), ("右键菜单", "Menu chuột phải"), ("右键菜单展示版", "Bản hiển thị menu chuột phải"),
("设置快捷菜单", "Đặt menu phím tắt"), ("设置当前输入域文本", "Đặt văn bản trường nhập hiện tại"),
("获得被选中的文本", "Lấy văn bản đang chọn"), ("获得被选中的", "Lấy phần đang chọn"),
("管理员模式", "Chế độ quản trị viên"), ("多语言", "Đa ngôn ngữ"),
("模式设置", "Cài đặt chế độ"), ("是否启用设计模式", "Có bật chế độ thiết kế không"),
("是否启用修订模式", "Có bật chế độ sửa đổi không"), ("是否启用只读模式", "Có bật chế độ chỉ đọc không"),
("编辑器初始化失败", "Khởi tạo trình soạn thảo thất bại"), ("编辑器初始化完成", "Khởi tạo trình soạn thảo hoàn tất"),
("创建编辑器", "Tạo trình soạn thảo"), ("编辑器配置项", "Hạng mục cấu hình trình soạn thảo"),
("查看所有配置项", "Xem tất cả hạng mục cấu hình"), ("自定义参数", "Tham số tùy chỉnh"),
("根据命令类型执行不同操作", "Thực hiện thao tác khác nhau theo loại lệnh"),
("病历加载失败", "Tải bệnh án thất bại"), ("模式加载文档", "Tải tài liệu theo chế độ"),
("新版编辑器", "Trình soạn thảo phiên bản mới"), ("编辑器", "Trình soạn thảo"),
("模板加载方式", "Cách tải mẫu"), ("模板权限", "Quyền mẫu"),
("若需实施配置权限", "Nếu cần áp dụng quyền cấu hình"), ("请保证启用当前岗位权限控制", "Vui lòng đảm bảo bật kiểm soát quyền vị trí hiện tại"),
("组织对应的科室下拉框加载", "Tải danh sách thả xuống khoa tương ứng với tổ chức"),
("控制关系加载", "Tải quan hệ điều khiển"), ("组织机构下拉框", "Danh sách thả xuống tổ chức"),
("关系主键", "Khóa chính quan hệ"), ("岗位主键", "Khóa chính vị trí"), ("岗位名称", "Tên vị trí"),
("文档校验", "Kiểm tra tài liệu"), ("页面设置", "Cài đặt trang"),
("获取元数据", "Lấy siêu dữ liệu"), ("获取所有行数据", "Lấy dữ liệu tất cả các dòng"),
("获取行数据", "Lấy dữ liệu dòng"), ("追加到", "Thêm vào"), ("集合里", "Trong tập hợp"),
("当前数据元值", "Giá trị phần tử dữ liệu hiện tại"), ("关联数据元名称", "Tên phần tử dữ liệu liên quan"),
("关联数据元值", "Giá trị phần tử dữ liệu liên quan"), ("关联数据元显示隐藏", "Hiển thị/ẩn phần tử dữ liệu liên quan"),
("主键值", "Giá trị khóa chính"), ("主键", "Khóa chính"), ("是否为空", "Có để trống không"),
("有效数值为", "Giá trị hợp lệ là"), ("数字", "Số"), ("数值", "Giá trị số"),
("一致", "Nhất quán"), ("不为空", "Không được để trống"), ("为空", "Để trống"),
("是否页面加载时就必须选择", "Có bắt buộc chọn ngay khi tải trang không"),
("是否属于修改诊断", "Có thuộc sửa chẩn đoán không"),
("在行号序列中获取最大的编号", "Lấy số lớn nhất trong dãy số dòng"),
("浏览器窗口调整大小时重新加载", "Tải lại khi thay đổi kích thước cửa sổ trình duyệt"),
("初次加载患者树标志", "Cờ tải lần đầu cây bệnh nhân"), ("的宽", "Chiều rộng của"),
("文件读取编辑权限控制标志", "Cờ kiểm soát quyền đọc/sửa tệp"),
("出院病历", "Bệnh án ra viện"), ("侧病历模板", "Mẫu bệnh án bên"),
("保存病历模板", "Lưu mẫu bệnh án"), ("测试出院病历模板", "Mẫu bệnh án ra viện thử nghiệm"),
("测试病历名称", "Tên bệnh án thử nghiệm"), ("获取测试用数据", "Lấy dữ liệu thử nghiệm"),
("创建编辑器框架", "Tạo khung trình soạn thảo"), ("协议发送", "Gửi theo giao thức"),
("请求", "Yêu cầu"), ("响应", "Phản hồi"), ("请求错误", "Lỗi yêu cầu"),
("使用", "Sử dụng"), ("根据", "Theo"), ("浏览", "Xem"), ("展示", "Hiển thị"), ("显示", "Hiển thị"),
("隐藏", "Ẩn"), ("显示顺序", "Thứ tự hiển thị"), ("排序", "Sắp xếp"), ("选中当前", "Chọn hiện tại"),
("最多选中一条", "Chọn tối đa một dòng"), ("最少", "Ít nhất"), ("位字符", "Ký tự"),
("请至少选择一行", "Vui lòng chọn ít nhất một dòng"), ("至少选择", "Chọn ít nhất"),
("抱歉", "Rất tiếc"), ("通知", "Thông báo"), ("公告", "Thông báo chung"),
("系统版本发布", "Phát hành phiên bản hệ thống"), ("管理系统", "Hệ thống quản lý"),
("版本", "Phiên bản"), ("介绍", "Giới thiệu"), ("图标", "Biểu tượng"),
("外部应用", "Ứng dụng bên ngoài"), ("连接", "Kết nối"), ("目标", "Đích"),
("无页面", "Không có trang"), ("框架页", "Trang khung"), ("弹出页", "Trang bật lên"),
("新窗口", "Cửa sổ mới"), ("子页面", "Trang con"), ("当前窗口", "Cửa sổ hiện tại"),
("报表管理器", "Trình quản lý báo cáo"), ("评分项目", "Hạng mục chấm điểm"),
("测试", "Kiểm tra"), ("恢复", "Khôi phục"), ("倾斜", "Nghiêng"),
("是否显示标尺", "Có hiển thị thước không"), ("回车", "Enter"),
])

add([
# ---- MRHomePage validation / clinical ----
("录入日期不能小于入院日期", "Ngày nhập không được nhỏ hơn ngày nhập viện"),
("录入日期不能大于出院日期", "Ngày nhập không được lớn hơn ngày ra viện"),
("年龄不能大于", "Tuổi không được lớn hơn"),
("新生儿年龄小于等于", "Tuổi trẻ sơ sinh nhỏ hơn hoặc bằng"),
("天必须填写", "ngày thì phải điền"),
("疾病编码必填", "Mã bệnh là bắt buộc"),
("病理诊断不为空", "Chẩn đoán bệnh lý không được để trống"),
("没有选择随诊", "Chưa chọn tái khám"),
("不能填写随诊期限", "Không được điền thời hạn tái khám"),
("不能填写医嘱转院拟接收医疗机构名称", "Không được điền tên cơ sở y tế dự kiến tiếp nhận khi chuyển viện theo y lệnh"),
("颅脑损伤患者昏迷时间必填", "Thời gian hôn mê của bệnh nhân chấn thương sọ não là bắt buộc"),
("医院感染情况", "Tình trạng nhiễm khuẩn bệnh viện"),
("不能填写医院感染相关操作", "Không được điền thao tác liên quan đến nhiễm khuẩn bệnh viện"),
("医院有感染情况", "Có tình trạng nhiễm khuẩn bệnh viện"),
("没有填写医院感染相关操作", "Chưa điền thao tác liên quan đến nhiễm khuẩn bệnh viện"),
("没有发生压疮", "Không xảy ra loét do tì đè"),
("不能填写压疮相关操作", "Không được điền thao tác liên quan đến loét do tì đè"),
("有压疮反应", "Có phản ứng loét do tì đè"),
("没有填写压疮相关操作", "Chưa điền thao tác liên quan đến loét do tì đè"),
("有输液反应", "Có phản ứng truyền dịch"),
("没有填写输液相关操作", "Chưa điền thao tác liên quan đến truyền dịch"),
("无输液反应", "Không có phản ứng truyền dịch"),
("不能填写输液相关操作", "Không được điền thao tác liên quan đến truyền dịch"),
("住院期间没有发生跌倒或坠床", "Trong thời gian nằm viện không xảy ra ngã hoặc rơi khỏi giường"),
("不能填写跌倒或坠床相关操作", "Không được điền thao tác liên quan đến ngã hoặc rơi khỏi giường"),
("住院期间发生跌倒或坠床", "Trong thời gian nằm viện xảy ra ngã hoặc rơi khỏi giường"),
("没有填写跌倒或坠床相关操作", "Chưa điền thao tác liên quan đến ngã hoặc rơi khỏi giường"),
("损伤中毒原因", "Nguyên nhân tổn thương/ngộ độc"), ("损伤", "Tổn thương"),
("中毒的外部原因", "Nguyên nhân bên ngoài gây ngộ độc"),
("实施临床路径", "Áp dụng đường đi lâm sàng"),
("死亡患者尸检", "Khám nghiệm tử thi bệnh nhân tử vong"),
("血型", "Nhóm máu"), ("不详", "Không rõ"), ("输血反应", "Phản ứng truyền máu"),
("主任", "Chủ nhiệm"), ("副主任", "Phó chủ nhiệm"), ("质控日期", "Ngày kiểm soát chất lượng"),
("离院方式", "Cách rời viện"), ("拟接收医疗机构名称", "Tên cơ sở y tế dự kiến tiếp nhận"),
("是否有出院", "Có ra viện không"), ("入院途径不为空", "Đường nhập viện không được để trống"),
("入院途径不为其他医疗机构转入", "Đường nhập viện không phải chuyển từ cơ sở y tế khác"),
("不能填写其他医疗机构转入", "Không được điền chuyển từ cơ sở y tế khác"),
("患者信息入院途径不为空", "Thông tin bệnh nhân: đường nhập viện không được để trống"),
("年龄不足", "Tuổi chưa đủ"), ("年龄", "Tuổi"),
("未输入联系人", "Chưa nhập người liên hệ"),
("不能填写联系人相关信息", "Không được điền thông tin liên quan đến người liên hệ"),
("智能审核", "Kiểm tra thông minh"), ("全院", "Toàn viện"), ("通用", "Chung"),
("专家", "Chuyên gia"), ("普通", "Thường"), ("急诊", "Cấp cứu"),
("请确认患者信息", "Vui lòng xác nhận thông tin bệnh nhân"),
("是否当前", "Là hiện tại"), ("最多选中一条", "Chọn tối đa một dòng"),
("正常", "Bình thường"), ("异常", "Bất thường"), ("阴性", "Âm tính"), ("阳性", "Dương tính"),
("备注", "Ghi chú"), ("机构名称", "Tên cơ quan"), ("发病节气", "Tiết khí phát bệnh"),
("关于", "Về"), ("转码", "Chuyển mã"),
("如果不是当前地址链接", "Nếu không phải liên kết địa chỉ hiện tại"),
("可以在这里填写需要的链接", "Có thể điền liên kết cần thiết tại đây"),
("分割字符串", "Tách chuỗi"), ("切割成数组形式的对象", "Cắt thành đối tượng dạng mảng"),
("再次分割成数组", "Tách lại thành mảng"), ("如果匹配输出结果", "Nếu khớp thì xuất kết quả"),
("停止", "Dừng"), ("未做", "Chưa thực hiện"), ("未查", "Chưa kiểm tra"), ("未输", "Chưa truyền"),
("未婚", "Chưa kết hôn"), ("已存在", "Đã tồn tại"), ("撤回", "Thu hồi"), ("不限", "Không giới hạn"),
("该病历无需上传医保", "Bệnh án này không cần tải lên bảo hiểm y tế"),
("该病历不支持转换", "Bệnh án này không hỗ trợ chuyển đổi"),
("病历退回", "Bệnh án bị trả lại"), ("病历提交", "Nộp bệnh án"),
# ---- system / login / home ----
("账户", "Tài khoản"), ("密码", "Mật khẩu"), ("密码复杂度不够", "Mật khẩu chưa đủ phức tạp"),
("验证码", "Mã xác minh"), ("记住密码", "Ghi nhớ mật khẩu"), ("忘记密码", "Quên mật khẩu"),
("医院", "Bệnh viện"), ("欢迎", "Chào mừng"), ("系统", "Hệ thống"), ("用户名", "Tên đăng nhập"),
("工作台", "Bàn làm việc"), ("待办", "Việc cần làm"), ("消息", "Tin nhắn"), ("提醒", "Nhắc nhở"),
("退出", "Đăng xuất"), ("个人信息", "Thông tin cá nhân"), ("修改密码", "Đổi mật khẩu"),
("帮助", "Trợ giúp"), ("首页", "Trang chủ"), ("导航", "Điều hướng"),
("角色", "Vai trò"), ("用户", "Người dùng"), ("岗位", "Vị trí"), ("组织", "Tổ chức"),
("机构", "Cơ quan"), ("部门", "Phòng ban"), ("权限", "Quyền"), ("菜单", "Menu"),
("日志", "Nhật ký"), ("配置", "Cấu hình"), ("参数", "Tham số"), ("管理", "Quản lý"),
("列表", "Danh sách"), ("详情", "Chi tiết"), ("查看", "Xem"), ("导出", "Xuất"), ("导入", "Nhập"),
("下载", "Tải xuống"), ("上传", "Tải lên"), ("上传成功", "Tải lên thành công"),
("上传失败", "Tải lên thất bại"), ("上传医保", "Tải lên bảo hiểm y tế"),
("模块", "Mô-đun"), ("副页", "Trang phụ"), ("角色用户", "Người dùng vai trò"),
("用户角色", "Vai trò người dùng"), ("让其他的都不选中", "Để các mục khác đều không được chọn"),
("但不包括其父级的", "Nhưng không bao gồm cấp cha của nó"),
("本系统软件源代码许可来源于", "Mã nguồn phần mềm hệ thống này được cấp phép theo"),
("许可协议", "Thỏa thuận cấp phép"), ("社区版", "Bản cộng đồng"),
("病区病人信息", "Thông tin bệnh nhân trong khu bệnh"), ("创建病人", "Tạo bệnh nhân"),
("出院带药", "Thuốc mang về khi ra viện"), ("审核", "Duyệt"), ("执行", "Thực hiện"),
# ---- controller messages ----
("手术保存成功", "Lưu phẫu thuật thành công"),
("病案信息异常", "Thông tin bệnh án bất thường"),
("请刷新重试", "Vui lòng làm mới và thử lại"),
("请填写完整病历保存后上传", "Vui lòng điền đầy đủ bệnh án, lưu rồi mới tải lên"),
("无法获取接口地址", "Không lấy được địa chỉ giao diện"),
("历史备份无效", "Bản sao lưu lịch sử không hợp lệ"),
("上传失败，请检查病历信息", "Tải lên thất bại, vui lòng kiểm tra thông tin bệnh án"),
("作废成功", "Hủy hiệu lực thành công"),
("删除模板异常", "Xóa mẫu bất thường"),
# ---- single chars / short labels (display-only; comparisons guarded out) ----
("有", "Có"), ("无", "Không có"), ("是", "Có"), ("否", "Không"),
("左", "Trái"), ("右", "Phải"), ("次", "Lần"), ("页", "Trang"),
("行", "Dòng"), ("列", "Cột"), ("人", "Người"), ("年", "Năm"), ("月", "Tháng"),
("天", "Ngày"), ("时", "Giờ"),
# ---- sentence-segmentation helpers ----
("不能", "không được "), ("填写", "điền"), ("选择", "chọn"), ("必须", "phải"),
("请", "Vui lòng "), ("输入", "nhập"), ("小于", "nhỏ hơn"), ("大于", "lớn hơn"),
("等于", "bằng"), ("当前", "hiện tại"), ("数据", "dữ liệu"), ("信息", "thông tin"),
("重新", "lại"), ("加载", "tải"), ("检查", "kiểm tra"), ("联系管理员", "liên hệ quản trị viên"),
("不可为空", "không được để trống"),
("为空", "để trống"), ("必须填写", "phải điền"),
])

# ---- pass 2 additions (from dry-run untranslated list) ----
add([
("单位", "Đơn vị"), ("量", "Lượng"), ("其他", "Khác"), ("日期", "Ngày"), ("文件", "Tệp"),
("说明", "Mô tả"), ("途径", "Đường dùng"), ("时间", "Thời gian"), ("一般", "Thông thường"),
("作废", "Hủy hiệu lực"), ("个人", "Cá nhân"), ("组套名称", "Tên bộ"),
("全部", "Tất cả"), ("时间点", "Thời điểm"), ("不可编辑", "Không thể chỉnh sửa"),
("录入成功", "Nhập thành công"), ("中草药费", "Chi phí thuốc đông y"), ("级别", "Cấp độ"),
("插入", "Chèn"), ("表格", "Bảng"), ("项目", "Hạng mục"), ("长期", "Dài hạn"), ("临时", "Tạm thời"),
("病史陈述者", "Người trình bày bệnh sử"), ("良好", "Tốt"), ("较差", "Kém"), ("形状", "Hình dạng"),
("专病", "Bệnh chuyên khoa"), ("自定义", "Tùy chỉnh"), ("近", "Gần"),
("一级", "Cấp I"), ("二级", "Cấp II"), ("三级", "Cấp III"),
("未造成伤害", "Không gây tổn thương"), ("跌倒或坠床的原因", "Nguyên nhân ngã hoặc rơi khỏi giường"),
("住院期间跌倒或坠床的伤害程度", "Mức độ tổn thương do ngã hoặc rơi khỏi giường trong thời gian nằm viện"),
("住院期间是否发生跌倒或坠床", "Trong thời gian nằm viện có xảy ra ngã hoặc rơi khỏi giường không"),
("是否发生压疮", "Có xảy ra loét do tì đè không"), ("压疮分期", "Giai đoạn loét do tì đè"),
("输液反应", "Phản ứng truyền dịch"), ("引发反应的药物", "Thuốc gây phản ứng"),
("临床表现", "Biểu hiện lâm sàng"),
("医院感染是否与手术相关", "Nhiễm khuẩn bệnh viện có liên quan đến phẫu thuật không"),
("医院感染是否与侵入性操作性相关", "Nhiễm khuẩn bệnh viện có liên quan đến thao tác xâm lấn không"),
("是否住院期间发生", "Có xảy ra trong thời gian nằm viện không"),
("主病已存在或相同诊断已存在", "Chẩn đoán chính đã tồn tại hoặc đã có chẩn đoán giống nhau"),
("主病", "Chẩn đoán chính"), ("主证", "Chứng chính"),
("已结算病人不能添加诊断", "Bệnh nhân đã quyết toán không được thêm chẩn đoán"),
("如需请联系管理员", "Nếu cần vui lòng liên hệ quản trị viên"),
("乡镇卫生院", "Trạm y tế xã"),
("工作单位及地址", "Địa chỉ nơi làm việc"), ("单位电话", "Điện thoại cơ quan"),
("单位邮编", "Mã bưu chính cơ quan"),
("工作单位及地址没有填写", "Chưa điền địa chỉ nơi làm việc"),
("单位电话没有填写", "Chưa điền điện thoại cơ quan"),
("单位邮编没有填写", "Chưa điền mã bưu chính cơ quan"),
("委托检验", "Ủy thác xét nghiệm"), ("血液记录单", "Phiếu ghi chép máu"),
("帕金森病非运动症状评定量表", "Thang đánh giá triệu chứng phi vận động bệnh Parkinson"),
("冠心病", "Bệnh mạch vành"),
("明细", "Chi tiết"), ("双击", "Nhấp đúp"), ("初始化", "Khởi tạo"), ("结果", "Kết quả"),
("申请单", "Phiếu yêu cầu"), ("报告", "Báo cáo"), ("记录", "Bản ghi"),
("检验结果", "Kết quả xét nghiệm"), ("诊诊断", "Chẩn đoán"),
("周岁的", "tuổi tròn"), ("月者", "tháng tuổi"),
])
# ---- pass 3 additions ----
add([
("岁", " tuổi"), ("分", "phút"),
("健康原因", "Nguyên nhân sức khỏe"), ("治疗", "Điều trị"), ("药物", "Thuốc"),
("麻醉原因", "Nguyên nhân gây mê"), ("环境因素", "Yếu tố môi trường"), ("其他原因", "Nguyên nhân khác"),
("住院期间身体约束", "Cố định cơ thể trong thời gian nằm viện"),
("离院时透析尿素氮值", "Giá trị nitơ urê thẩm tách lúc rời viện"), ("元", " đồng"),
("综合医疗服务类", "Loại dịch vụ y tế tổng hợp"), ("诊断类", "Loại chẩn đoán"),
("治疗类", "Loại điều trị"), ("康复类", "Loại phục hồi chức năng"), ("中医类", "Loại đông y"),
("西药类", "Loại thuốc tây"), ("中药类", "Loại thuốc đông y"),
("血液和血液制品类", "Loại máu và chế phẩm máu"), ("耗材类", "Loại vật tư tiêu hao"),
("其他类", "Loại khác"), ("手术及", "Phẫu thuật và"),
("手术及操作", "Phẫu thuật và thao tác"), ("手术及操作日期", "Ngày phẫu thuật và thao tác"),
("术前准备天数", "Số ngày chuẩn bị trước mổ"), ("切口愈合类别", "Loại lành vết mổ"),
("手术切口感染", "Nhiễm trùng vết mổ"), ("手术并发症", "Biến chứng phẫu thuật"),
("手术并发症名称", "Tên biến chứng phẫu thuật"), ("呼吸衰竭", "Suy hô hấp"),
("医源性气胸", "Tràn khí màng phổi do thầy thuốc"), ("髋关节骨折", "Gãy xương khớp háng"),
("生理与代谢紊乱", "Rối loạn sinh lý và chuyển hóa"),
("手术过程中异物遗留", "Dị vật còn sót trong quá trình phẫu thuật"),
("肺栓塞", "Tắc mạch phổi"), ("肺部感染", "Nhiễm trùng phổi"),
("深静脉血栓", "Huyết khối tĩnh mạch sâu"),
("医源性意外穿刺伤", "Vết đâm xuyên ngoài ý muốn do thầy thuốc"),
("医源性撕裂伤", "Vết rách do thầy thuốc"), ("败血症", "Nhiễm trùng huyết"),
("请选择手术及操作日期", "Vui lòng chọn ngày phẫu thuật và thao tác"),
("术前抗菌药物时间给定", "Đã cho thời gian kháng sinh trước mổ"),
("术前准备天数则为必填", "Số ngày chuẩn bị trước mổ là bắt buộc"),
("术前准备天数给定", "Đã cho số ngày chuẩn bị trước mổ"),
("术前抗菌药物时间则为必填", "Thời gian kháng sinh trước mổ là bắt buộc"),
("切口部位为必填项", "Vị trí vết mổ là bắt buộc"),
("切口愈合等级为必填项", "Mức độ lành vết mổ là bắt buộc"),
("切口愈合类别为必填项", "Loại lành vết mổ là bắt buộc"),
("并发症名称给定", "Đã cho tên biến chứng"), ("手术并发症则为必填", "Biến chứng phẫu thuật là bắt buộc"),
("手术并发症给定", "Đã cho biến chứng phẫu thuật"), ("并发症名称则为必填", "Tên biến chứng là bắt buộc"),
("手术开始日期不能小于入院日期且不能大于出院日期", "Ngày bắt đầu phẫu thuật không được nhỏ hơn ngày nhập viện và không được lớn hơn ngày ra viện"),
("手术结束日期不能小于手术开始日期且不能大于出院日期", "Ngày kết thúc phẫu thuật không được nhỏ hơn ngày bắt đầu phẫu thuật và không được lớn hơn ngày ra viện"),
("麻醉开始日期不能小于手术开始日期且不能大于手术结束日期", "Ngày bắt đầu gây mê không được nhỏ hơn ngày bắt đầu phẫu thuật và không được lớn hơn ngày kết thúc phẫu thuật"),
("住院期间是否", "Trong thời gian nằm viện có"), ("发生跌倒或坠床", "xảy ra ngã hoặc rơi khỏi giường"),
("且", "và"), ("则", "thì"), ("为必填", "là bắt buộc"), ("为必填项", "là bắt buộc"), ("给定", "đã cho"),
("默认", "Mặc định"), ("黏贴", "Dán"), ("黎族", "Dân tộc Lê"), ("苗族", "Dân tộc Miêu"),
("蒙古族", "Dân tộc Mông Cổ"), ("藏族", "Dân tộc Tạng"), ("高低标记", "Đánh dấu cao/thấp"),
("高", "Cao"), ("首页登记号", "Số đăng ký trang đầu"), ("频次", "Tần suất"),
("颅脑损伤患者昏迷时间", "Thời gian hôn mê của bệnh nhân chấn thương sọ não"),
("预览", "Xem trước"), ("页面方向", "Hướng trang"), ("页面尺寸", "Kích thước trang"),
("页面大小", "Kích thước trang"), ("随诊期限", "Thời hạn tái khám"), ("随诊", "Tái khám"),
("自费", "Tự trả"), ("若未选中一条新的诊断信息", "Nếu chưa chọn thông tin chẩn đoán mới"),
("若要保留", "Nếu muốn giữ lại"), ("英文名称", "Tên tiếng Anh"), ("英文菜单", "Menu tiếng Anh"),
("药物过敏", "Dị ứng thuốc"), ("获取", "Lấy"), ("血压", "Huyết áp"), ("血小板", "Tiểu cầu"),
("血浆", "Huyết tương"), ("表单信息", "Thông tin biểu mẫu"), ("表单页", "Trang biểu mẫu"),
("表名", "Tên bảng"), ("表名称", "Tên bảng"), ("规则列表项", "Hạng mục danh sách quy tắc"),
("规则天", "Ngày quy tắc"), ("规则维护", "Bảo trì quy tắc"), ("记录范围", "Phạm vi ghi"),
("评分说明", "Mô tả chấm điểm"), ("诊断简拼", "Mã viết tắt chẩn đoán"),
("词条内容", "Nội dung từ"), ("词条管理表单页", "Trang biểu mẫu quản lý từ"),
("确定要删除", "Bạn có chắc chắn muốn xóa"), ("该条数据", "dữ liệu này"), ("吗", "không?"),
("该模板配置不可删除", "Cấu hình mẫu này không thể xóa"),
("该申请单号无检查结果显示无法导入内容", "Số phiếu yêu cầu này không có kết quả kiểm tra hiển thị nên không thể nhập nội dung"),
("该账号已在其他地方登录", "Tài khoản này đã đăng nhập ở nơi khác"),
("请先为用户关联人员", "Vui lòng liên kết nhân sự cho người dùng trước"),
("请点击", "Vui lòng nhấp"), ("请申请", "Vui lòng yêu cầu"),
("请确认无误后提交", "Vui lòng xác nhận chính xác rồi nộp"),
("请确认是否重置", "Vui lòng xác nhận có đặt lại không"), ("请稍后", "Vui lòng đợi chút"),
("请联系质控办", "Vui lòng liên hệ phòng kiểm soát chất lượng"),
("请输入医保编码", "Vui lòng nhập mã bảo hiểm y tế"), ("请输入批注内容", "Vui lòng nhập nội dung chú thích"),
("请输入正确的排序序号", "Vui lòng nhập số thứ tự sắp xếp chính xác"),
("请输入账号", "Vui lòng nhập tài khoản"), ("请输入过滤的关键字", "Vui lòng nhập từ khóa lọc"),
("请选中一条病历文书", "Vui lòng chọn một văn bản bệnh án"),
("请选中一条要删除的病历规则", "Vui lòng chọn một quy tắc bệnh án cần xóa"),
("请选中一条规则项", "Vui lòng chọn một hạng mục quy tắc"),
("请选中一项病历类型", "Vui lòng chọn một loại bệnh án"), ("请选中元素", "Vui lòng chọn phần tử"),
("请选中病历文书", "Vui lòng chọn văn bản bệnh án"),
("请选中需要修改的数据列", "Vui lòng chọn cột dữ liệu cần sửa"),
("请选择右侧岗位列表", "Vui lòng chọn danh sách vị trí bên phải"),
("请选择病人", "Vui lòng chọn bệnh nhân"), ("请选择要删除的元素", "Vui lòng chọn phần tử cần xóa"),
("请选择要删除的数据行", "Vui lòng chọn dòng dữ liệu cần xóa"),
("责任护士", "Điều dưỡng chịu trách nhiệm"), ("责任护士不能为空", "Điều dưỡng chịu trách nhiệm không được để trống"),
("质控医师", "Bác sĩ kiểm soát chất lượng"), ("质控护士", "Điều dưỡng kiểm soát chất lượng"),
("账号", "Tài khoản"), ("超声心动图", "Siêu âm tim"),
("足背动脉搏动标志", "Dấu hiệu mạch động mạch mu chân"), ("跌倒预防", "Phòng ngừa ngã"),
("压疮预防", "Phòng ngừa loét do tì đè"), ("持续心电监测", "Theo dõi điện tim liên tục"),
("持续", "Liên tục"), ("心电监测", "Theo dõi điện tim"), ("基护级别", "Cấp độ điều dưỡng cơ bản"),
("协助进食", "Hỗ trợ ăn uống"), ("进食", "Ăn uống"), ("拍背及有效咳痰", "Vỗ lưng và ho khạc hiệu quả"),
("拍背", "Vỗ lưng"), ("有效咳痰", "Ho khạc hiệu quả"), ("进修医师", "Bác sĩ đào tạo nâng cao"),
("退出原因", "Lý do thoát"), ("透明", "Trong suốt"), ("邮箱", "Email"),
("重置密码失败", "Đặt lại mật khẩu thất bại"), ("重置密码成功", "Đặt lại mật khẩu thành công"),
("重复", "Trùng lặp"), ("除此之外全部关闭", "Đóng tất cả trừ cái này"), ("除颤", "Khử rung tim"),
("门诊挂号", "Đăng ký khám ngoại trú"), ("门诊收费", "Thu phí ngoại trú"),
("输入链接", "Nhập liên kết"), ("输血反应不能为未输", "Phản ứng truyền máu không được là chưa truyền"),
("输血反应必须为未输", "Phản ứng truyền máu phải là chưa truyền"), ("输血品种", "Loại máu truyền"),
("返回", "Quay lại"), ("返回修改", "Quay lại sửa"), ("这是一个单选框", "Đây là một nút radio"),
("这是一个复选框", "Đây là một hộp kiểm"), ("身份证号", "Số CMND/CCCD"),
("身份证格式不正确", "Định dạng CMND không chính xác"), ("选择了随诊", "Đã chọn tái khám"),
])

# ---- pass 4 additions ----
add([
("籍贯", "Quê quán"), ("出生日期", "Ngày sinh"), ("新生儿出生体重", "Cân nặng lúc sinh của trẻ sơ sinh"),
("离婚", "Ly hôn"), ("现住址", "Địa chỉ hiện tại"), ("户口地址", "Địa chỉ hộ khẩu"),
("关系", "Quan hệ"), ("其他医疗机构转入", "Chuyển từ cơ sở y tế khác"),
("入院科别", "Chuyên khoa nhập viện"), ("实际住院", "Nằm viện thực tế"),
("使用医疗机构中药制剂", "Sử dụng chế phẩm thuốc đông y của cơ sở y tế"),
("使用中医诊疗设备", "Sử dụng thiết bị chẩn trị đông y"),
("使用中医诊疗技术", "Sử dụng kỹ thuật chẩn trị đông y"),
("住院期间是否病危或病重", "Trong thời gian nằm viện có nguy kịch hoặc nặng không"),
("抢救次数", "Số lần cấp cứu"), ("成功次数", "Số lần thành công"),
("数据绑定", "Liên kết dữ liệu"), ("地址联动", "Liên động địa chỉ"), ("病理号", "Số bệnh lý"),
("红细胞", "Hồng cầu"), ("全血", "Máu toàn phần"), ("自体血回输", "Truyền máu tự thân"),
("白蛋白", "Albumin"), ("冷沉淀", "Tủa lạnh"), ("周", "Tuần"), ("科主任", "Chủ nhiệm khoa"),
("医师", "Bác sĩ"), ("主诊医师", "Bác sĩ chẩn trị chính"), ("主治医师", "Bác sĩ điều trị chính"),
("住院医师", "Bác sĩ nội trú"), ("实习医师", "Bác sĩ thực tập"), ("编码员", "Nhân viên mã hóa"),
("类手术切口预防性应用抗菌药物使用持续时间", "Thời gian sử dụng kháng sinh dự phòng vết mổ theo loại"),
("联合用药", "Dùng thuốc kết hợp"), ("是否实施临", "Có áp dụng đường đi lâm sàng"),
("床路径管理", "quản lý"), ("是否实施临床路径管理", "Có áp dụng quản lý đường đi lâm sàng"),
("是否完成临床路径", "Đã hoàn thành đường đi lâm sàng"), ("是否变异", "Có biến dị không"),
("变异原因", "Nguyên nhân biến dị"), ("没有实施临床路径管理", "Không áp dụng quản lý đường đi lâm sàng"),
("实施临床路径管理", "Áp dụng quản lý đường đi lâm sàng"),
("没有填写是否完成临床路径", "Chưa điền đã hoàn thành đường đi lâm sàng hay chưa"),
("医嘱转院拟接收医疗机构名称", "Tên cơ sở y tế dự kiến tiếp nhận khi chuyển viện theo y lệnh"),
("医嘱转社区卫生服务机构", "Chuyển đến cơ sở dịch vụ y tế cộng đồng theo y lệnh"),
("检查情况", "Tình hình kiểm tra"), ("诊断类别", "Loại chẩn đoán"),
("自定义目录不可为空", "Thư mục tùy chỉnh không được để trống"),
("请为医疗机构维护一个", "Vui lòng bảo trì cho cơ sở y tế một"),
("医院管理员", "Quản trị viên bệnh viện"),
("住院信息没有实施临床路径管理", "Thông tin nằm viện: không áp dụng quản lý đường đi lâm sàng"),
])

# ---- pass 5 additions ----
add([
("没有填写", "chưa điền"), ("没有选择", "chưa chọn"), ("没有药物过敏", "Không có dị ứng thuốc"),
("没有输血量时", "Khi không có lượng máu truyền"), ("有输血量时", "Khi có lượng máu truyền"),
("输血量", "Lượng máu truyền"), ("目的", "Mục đích"), ("原因", "Nguyên nhân"),
("必填", "là bắt buộc"), ("病理诊断必填", "Chẩn đoán bệnh lý là bắt buộc"),
("病理号必填", "Số bệnh lý là bắt buộc"), ("国籍", "Quốc tịch"), ("出生地址", "Địa chỉ nơi sinh"),
("现住址电话", "Điện thoại địa chỉ hiện tại"), ("现住址邮编", "Mã bưu chính địa chỉ hiện tại"),
("工作地址邮编", "Mã bưu chính địa chỉ làm việc"), ("工作地址电话", "Điện thoại địa chỉ làm việc"),
("联系人关系", "Quan hệ người liên hệ"), ("联系人电话", "Điện thoại người liên hệ"),
("联系人地址", "Địa chỉ người liên hệ"), ("格式不正确", "Định dạng không chính xác"),
("格式有误", "Định dạng sai"), ("与", "với"), ("不符", "không khớp"),
("中西医诊断至少填写一个", "Chẩn đoán đông-tây y phải điền ít nhất một"),
("已输入联系人", "Đã nhập người liên hệ"),
("患者住院总费用必须大于", "Tổng chi phí nằm viện của bệnh nhân phải lớn hơn"),
("患者住院总费用", "Tổng chi phí nằm viện của bệnh nhân"), ("必须大于", "phải lớn hơn"),
("必须大于等于", "phải lớn hơn hoặc bằng"),
("手术治疗费必须大于等于麻醉费与手术费之和", "Phí điều trị phẫu thuật phải lớn hơn hoặc bằng tổng phí gây mê và phí phẫu thuật"),
("麻醉费与手术费之和", "tổng phí gây mê và phí phẫu thuật"),
("出院诊断的出院情况为死亡时", "Khi tình hình ra viện trong chẩn đoán ra viện là tử vong"),
("离院方式必须为死亡", "Cách rời viện phải là tử vong"),
("当出院情况不为死亡时", "Khi tình hình ra viện không phải tử vong "),
("当出院情况为死亡时", "Khi tình hình ra viện là tử vong "),
("抢救次数需等于成功次数", "Số lần cấp cứu cần bằng số lần thành công"),
("抢救成功次数需等于抢救次数", "Số lần cấp cứu thành công cần bằng số lần cấp cứu"),
("需等于", "cần bằng"), ("离院方式为死亡", "Cách rời viện là tử vong"),
("离院方式不为死亡", "Cách rời viện không phải tử vong"),
("离院方式不为医嘱转院", "Cách rời viện không phải chuyển viện theo y lệnh"),
("离院方式不为医嘱转社区卫生服务机构", "Cách rời viện không phải chuyển đến cơ sở dịch vụ y tế cộng đồng theo y lệnh"),
("完成临床路径", "Hoàn thành đường đi lâm sàng"),
("当离院方式不为死亡时", "Khi cách rời viện không phải tử vong "),
("当离院方式为死亡时", "Khi cách rời viện là tử vong "),
("患者基本信息提取失败", "Trích xuất thông tin cơ bản bệnh nhân thất bại"),
("信息填写不完整", "Thông tin điền chưa đầy đủ"),
("同步数据会重置页面", "Đồng bộ dữ liệu sẽ đặt lại trang"), ("同步完成", "Đồng bộ hoàn tất"),
("在院病人", "Bệnh nhân đang nằm viện"), ("出院病人", "Bệnh nhân đã ra viện"),
("不可重复提交", "Không được nộp trùng lặp"), ("病历已退回", "Bệnh án đã bị trả lại"),
("患者病历未提交或已退回", "Bệnh án bệnh nhân chưa nộp hoặc đã bị trả lại"),
("不可退回", "Không thể trả lại"), ("新版编辑器开关", "Công tắc trình soạn thảo phiên bản mới"),
("在院标志", "Dấu hiệu đang nằm viện"), ("床位医生", "Bác sĩ giường bệnh"), ("已出区", "Đã ra khỏi khu"),
("图片居左对齐", "Căn trái hình ảnh"), ("图片居左", "Hình ảnh căn trái"),
("图片居右对齐", "Căn phải hình ảnh"), ("图片居右", "Hình ảnh căn phải"),
("图片居中对齐", "Căn giữa hình ảnh"), ("图片居中", "Hình ảnh căn giữa"),
("选择插入的医学表达式", "Chọn biểu thức y học để chèn"),
("在左面插入列", "Chèn cột bên trái"), ("在右面插入列", "Chèn cột bên phải"),
("合并单元格", "Gộp ô"), ("拆分单元格", "Tách ô"), ("折叠列表", "Thu gọn danh sách"),
("折叠菜单", "Thu gọn menu"), ("插入字符", "Chèn ký tự"), ("特殊字符", "Ký tự đặc biệt"),
("罗马字符", "Ký tự La Mã"), ("数学字符", "Ký tự toán học"), ("医学字符", "Ký tự y học"),
("宽", "Rộng"), ("纯文本复制", "Sao chép văn bản thuần"),
("在上方插入表格行", "Chèn dòng bảng phía trên"), ("在下方插入表格行", "Chèn dòng bảng phía dưới"),
("在左方插入表格列", "Chèn cột bảng bên trái"), ("在右方插入表格列", "Chèn cột bảng bên phải"),
("在左侧插入列", "Chèn cột bên trái"), ("在右侧插入列", "Chèn cột bên phải"),
("数值", "Giá trị số"), ("输入域文本值", "Giá trị văn bản trường nhập"),
("输入域数值", "Giá trị số trường nhập"), ("宽度", "Chiều rộng"), ("高度", "Chiều cao"),
("弹框", "Hộp thoại"), ("最小列数和行数", "Số cột và dòng tối thiểu"),
("最大列数和行数", "Số cột và dòng tối đa"), ("回调", "Gọi lại"),
("当前选择尺寸", "Kích thước đang chọn"), ("弹框显示位置", "Vị trí hiển thị hộp thoại"),
("弹框可选区域位置", "Vị trí vùng chọn hộp thoại"), ("鼠标位置", "Vị trí chuột"),
("初始化弹框", "Khởi tạo hộp thoại"), ("初始化事件", "Khởi tạo sự kiện"),
("列数", "Số cột"), ("行数", "Số dòng"), ("数据源路径", "Đường dẫn nguồn dữ liệu"),
("第一个参数为指定的元素", "Tham số đầu tiên là phần tử chỉ định"),
("若为", "Nếu là"), ("则表示光标所在位置的单元格", "thì biểu thị ô tại vị trí con trỏ"),
("则表示光标所在位置的表格行", "thì biểu thị dòng bảng tại vị trí con trỏ"),
("这里的第一个参数可以是", "Tham số đầu tiên ở đây có thể là"),
("粘贴内部复制或者剪切的", "Dán nội dung đã sao chép hoặc cắt bên trong"),
("批注人", "Người chú thích"), ("演示文档", "Tài liệu trình diễn"),
("藏文演示文档", "Tài liệu trình diễn tiếng Tạng"),
("插入输入域", "Chèn trường nhập"), ("单复选分组", "Nhóm radio/hộp kiểm"),
("插入一维码", "Chèn mã vạch một chiều"), ("插入条形码", "Chèn mã vạch"),
("命令行清单", "Danh sách dòng lệnh"), ("命令清单", "Danh sách lệnh"),
("字体名称", "Tên phông chữ"), ("管理员权限", "Quyền quản trị viên"),
("用户登录", "Đăng nhập người dùng"),
("没能下载安装控件", "Không tải xuống và cài đặt được điều khiển"),
("请联系管理员来设置安全站点", "Vui lòng liên hệ quản trị viên để đặt trang web an toàn"),
("痕迹", "Dấu vết"), ("留痕视图", "Chế độ xem lưu dấu vết"), ("权限相关", "Liên quan đến quyền"),
("样式", "Kiểu"), ("命令", "Lệnh"), ("级", " cấp"),
])

# ---- pass 6 additions ----
add([
("副页信息", "Thông tin trang phụ"), ("没有变异", "Không có biến dị"),
("同步患者信息", "Đồng bộ thông tin bệnh nhân"), ("加载本地缓存", "Tải bộ nhớ đệm cục bộ"),
("清除格式", "Xóa định dạng"), ("最近使用", "Dùng gần đây"), ("更多", "Thêm"),
("背景", "Nền"), ("前景", "Màu chữ"), ("左对齐", "Căn trái"), ("右对齐", "Căn phải"),
("两端对齐", "Căn đều hai bên"), ("减少缩进", "Giảm thụt lề"), ("增加缩进", "Tăng thụt lề"),
("您还在使用老掉牙的", "Bạn vẫn đang dùng"), ("正常使用系统前请升级您的浏览器到", "Trước khi dùng hệ thống bình thường, vui lòng nâng cấp trình duyệt của bạn lên"),
("以上版本", "phiên bản trở lên"), ("点击升级", "Nhấp để nâng cấp"),
("强烈建议您更改换浏览器", "Rất khuyên bạn nên đổi trình duyệt"), ("谷歌", "Google"),
("换一张", "Đổi hình khác"), ("手机号", "Số điện thoại"), ("登录成功", "Đăng nhập thành công"),
("正在跳转", "Đang chuyển hướng"), ("系统登录已超时", "Phiên đăng nhập hệ thống đã hết hạn"),
("您已被迫下线", "Bạn đã bị buộc đăng xuất"), ("系统未知错误", "Lỗi hệ thống không xác định"),
("关键数据实时指标", "Chỉ số thời gian thực của dữ liệu quan trọng"),
("住院收费", "Thu phí nội trú"), ("出院结算", "Quyết toán ra viện"), ("患者管理", "Quản lý bệnh nhân"),
("挂号统计", "Thống kê đăng ký khám"), ("挂号类别统计", "Thống kê loại đăng ký khám"),
("系统更新", "Cập nhật hệ thống"), ("系统版本升级", "Nâng cấp phiên bản hệ thống"),
("新版员工守则", "Nội quy nhân viên phiên bản mới"), ("即日执行", "Thi hành ngay từ hôm nay"),
("办公使用指南", "Hướng dẫn sử dụng văn phòng"),
("劳动节集体出游指南", "Hướng dẫn du lịch tập thể ngày Quốc tế Lao động"),
("品牌价值的最佳选择", "Lựa chọn tốt nhất cho giá trị thương hiệu"),
("企业文化", "Văn hóa doanh nghiệp"),
("创新企业党建思想政治工作", "Đổi mới công tác tư tưởng chính trị xây dựng Đảng trong doanh nghiệp"),
("现代国有企业道德体系初构", "Phác thảo hệ thống đạo đức doanh nghiệp nhà nước hiện đại"),
("国有企业文化问题根源及文化重塑任务分析", "Phân tích nguồn gốc vấn đề văn hóa doanh nghiệp nhà nước và nhiệm vụ tái tạo văn hóa"),
("国有企业文化建设必须突出员工文化", "Xây dựng văn hóa doanh nghiệp nhà nước phải nhấn mạnh văn hóa nhân viên"),
("加强企业公民建设", "Tăng cường xây dựng công dân doanh nghiệp"),
("推进企业和谐发展", "Thúc đẩy phát triển hài hòa doanh nghiệp"),
("不断提升大庆精神", "Không ngừng nâng cao tinh thần Đại Khánh"),
("实现企业持续发展", "Thực hiện phát triển bền vững doanh nghiệp"),
])

# ---- pass 7 additions ----
add([
("是否急诊手术", "Có phải phẫu thuật cấp cứu không"),
("是否审核", "Có duyệt không"),
("急诊手术", "Phẫu thuật cấp cứu"),
])

# ---- pass 8 additions ----
add([
("抢救成功次数", "Số lần cấp cứu thành công"),
("没有填写联系人相关信息", "Chưa điền thông tin liên quan đến người liên hệ"),
("没有完成临床路径", "Chưa hoàn thành đường đi lâm sàng"),
("脉搏", "Mạch"), ("呼吸", "Hô hấp"), ("氧饱和度", "Độ bão hòa oxy"),
("基护", "Điều dưỡng cơ bản"), ("协助", "Hỗ trợ"), ("水", "Nước"),
("拍背及", "Vỗ lưng và"), ("其他基护", "Điều dưỡng cơ bản khác"),
("专科护理", "Điều dưỡng chuyên khoa"), ("导管分类", "Phân loại ống thông"),
("措施", "Biện pháp"), ("护理", "Điều dưỡng"), ("指导", "Hướng dẫn"),
("尿量", "Lượng nước tiểu"),
("筛选条件", "Điều kiện lọc"), ("病危", "Nguy kịch"),
("住院患者查询", "Tra cứu bệnh nhân nội trú"),
("发出手术安全核对表标志", "Cờ phát bảng kiểm tra an toàn phẫu thuật"),
("收回手术安全核对表标志", "Cờ thu hồi bảng kiểm tra an toàn phẫu thuật"),
("待审", "Chờ duyệt"), ("撤", "Thu hồi"),
("所选中的医嘱内容", "Nội dung y lệnh đã chọn"),
("保存更新", "Lưu cập nhật"), ("全部更新", "Cập nhật tất cả"),
("待结账", "Chờ quyết toán"),
("患者类型", "Loại bệnh nhân"), ("挂号日期", "Ngày đăng ký khám"),
("就诊状态", "Trạng thái khám bệnh"),
("病情分型", "Phân loại tình trạng bệnh"), ("临床未确定", "Lâm sàng chưa xác định"),
("情况不明", "Chưa rõ tình hình"), ("阴", "Âm tính"), ("阳", "Dương tính"),
("双源", "Nguồn kép"), ("超", "Siêu âm"), ("片", "Phim"),
("同位素检查", "Kiểm tra đồng vị"),
])

# ---- pass 9 additions ----
add([
("大类", "Nhóm lớn"), ("子目录", "Thư mục con"), ("简码", "Mã rút gọn"),
("关联岗位", "Vị trí liên quan"), ("大类简码", "Mã rút gọn nhóm lớn"),
("简码与业务表一经保存无法修改", "Mã rút gọn và bảng nghiệp vụ sau khi lưu không thể sửa"),
("业务表", "Bảng nghiệp vụ"), ("无法", "không thể"),
("确认", "Xác nhận"), ("后会清空当前诊断信息", "sau đó sẽ xóa thông tin chẩn đoán hiện tại"),
])

# ---- pass 10 additions (exact phrases beat segmentation word order) ----
add([
("修改病历大类", "Sửa nhóm lớn bệnh án"),
("新增病历子目录", "Thêm mới thư mục con bệnh án"),
])

# ---- pass 11 additions (EMR completion follow-up 2026-10-26) ----
add([
# MRHomePage record editor
("次序", "Thứ tự"), ("单位负责人", "Người phụ trách đơn vị"),
("统计负责人", "Người phụ trách thống kê"), ("填表人", "Người lập biểu"),
("联系电话", "Điện thoại liên hệ"), ("手机", "Di động"),
("报出日期", "Ngày báo cáo"), ("填表说明", "Hướng dẫn lập biểu"),
("其中", "Trong đó"), ("手术已录入", "Đã nhập phẫu thuật"),
("切口感染", "Nhiễm trùng vết mổ"), ("并发症", "Biến chứng"),
("并发症名称", "Tên biến chứng"), ("医疗付款方式", "Phương thức thanh toán y tế"),
("医保", "BHYT"), ("住院次数", "Số lần nhập viện"),
("术前", "Trước mổ"), ("准备天数", "Số ngày chuẩn bị"),
("联", "Liên"),
# Blctgl / Bljgh / Blys
("目录名称", "Tên danh mục"), ("使用简单", "Sử dụng đơn giản"),
("引入", "thêm"), ("和", "và"), ("代码即可", "mã là được"),
("病历结构维护", "Bảo trì cấu trúc bệnh án"), ("字段代码", "Mã trường"),
("字段序号", "Số thứ tự trường"), ("新建病历表", "Tạo bảng bệnh án mới"),
("字段", "Trường"), ("字段排序", "Thứ tự trường"),
("值", "Giá trị"), ("元素序号", "Số thứ tự phần tử"),
("正则匹配", "Khớp biểu thức chính quy"),
("上级元素", "Phần tử cấp trên"), ("元素标识", "Mã định danh phần tử"),
("自动更新", "tự động cập nhật"), ("只读", "chỉ đọc"),
("元素值", "Giá trị phần tử"), ("允许编辑", "Cho phép sửa"),
("格式", "Định dạng"), ("元素表列表页", "Trang danh sách bảng phần tử"),
("同步到模板编辑器", "Đồng bộ sang trình sửa mẫu"),
("修改", "Sửa"), ("新增成功", "Thêm mới thành công"),
# MedRecordTemplate
("医保编码", "Mã BHYT"), ("绑定数据源", "Gắn nguồn dữ liệu"),
("模板大类不可修改", "Không được sửa nhóm lớn của mẫu"),
("纯文本", "Văn bản thuần"), ("插入日期", "Chèn ngày"),
("表格属性", "Thuộc tính bảng"), ("转", "Chuyển"),
# MedicalRecord editor toolbar
("预览", "Xem trước"), ("留痕视图", "Chế độ xem lưu vết"),
("新建", "Tạo mới"), ("打开", "Mở"), ("复制", "Sao chép"),
("黏贴", "Dán"), ("剪切", "Cắt"), ("格式刷", "Cọ định dạng"),
("删除", "Xóa"), ("撤销", "Hoàn tác"), ("重复", "Làm lại"),
("字体", "Phông chữ"), ("加粗", "Đậm"), ("倾斜", "Nghiêng"),
("下划线", "Gạch chân"), ("样式", "Kiểu"), ("文本颜色", "Màu chữ"),
("简体转繁体", "Giản thể sang Phồn thể"), ("繁体转简体", "Phồn thể sang Giản thể"),
("边框和底纹", "Viền và nền"), ("段落", "Đoạn"),
("标题层次", "Cấp tiêu đề"), ("插入文本输入域", "Chèn trường nhập văn bản"),
("插入单选框", "Chèn hộp chọn đơn"), ("插入复选框", "Chèn hộp kiểm"),
("插入单选复选分组", "Chèn nhóm chọn đơn/kiểm"),
("插入图片", "Chèn ảnh"), ("插入一维条码", "Chèn mã vạch 1D"),
("插入二维条码", "Chèn mã QR"), ("插入医学表达式", "Chèn biểu thức y học"),
("表格行属性", "Thuộc tính hàng bảng"), ("插入批注", "Chèn chú thích"),
("表单视图", "Chế độ xem biểu mẫu"), ("阅读视图", "Chế độ xem đọc"),
("清除全部痕迹", "Xóa toàn bộ dấu vết"), ("权限相关", "Liên quan đến quyền"),
("查看", "Xem"), ("代码", "Mã"), ("字数统计", "Đếm số từ"),
("关于", "Giới thiệu"), ("管理员模式", "Chế độ quản trị"),
("放大", "Phóng to"), ("缩小", "Thu nhỏ"), ("命令", "Lệnh"),
("医生签名", "Chữ ký bác sĩ"),
# YZForm / MedicalRecord Form
("用量", "Liều dùng"), ("用法", "Cách dùng"),
("用量单位", "Đơn vị liều"), ("医嘱托", "Lời dặn y lệnh"),
("同组", "Cùng nhóm"), ("查", "Tra"), ("询", "cứu"),
("异常结果勾选", "Đánh dấu kết quả bất thường"),
("报告", "Báo cáo"), ("参考值范围", "Khoảng giá trị tham chiếu"),
# MedicalRules
("病历文书规则", "Quy tắc văn bản bệnh án"),
("天数限制", "Giới hạn số ngày"),
("新增病历规则项", "Thêm mục quy tắc bệnh án"),
("修改病历规则项", "Sửa mục quy tắc bệnh án"),
("是否删除该条规则", "Có xóa quy tắc này không"),
# NursingRecord
("气管插管", "Đặt ống nội khí quản"),
("建立静脉通道", "Thiết lập đường truyền tĩnh mạch"),
("心电监护", "Theo dõi điện tim"), ("心肺复苏", "Hồi sức tim phổi"),
("洗胃", "Rửa dạ dày"), ("显示已作废", "Hiển thị đã hủy"),
("发出手术风险评估表标志", "Cờ đã phát bảng đánh giá rủi ro phẫu thuật"),
("收回手术风险评估表标志", "Cờ đã thu hồi bảng đánh giá rủi ro phẫu thuật"),
("录入用户", "Người nhập"), ("录入时间", "Thời gian nhập"),
("缺少住院号", "Thiếu số nhập viện"),
("危重护理记录录入", "Nhập hồ sơ chăm sóc bệnh nặng"),
("手术护理记录录入", "Nhập hồ sơ chăm sóc phẫu thuật"),
("一般护理记录录入", "Nhập hồ sơ chăm sóc chung"),
# PatInfo
("患者同步", "Đồng bộ bệnh nhân"), ("分管", "Phụ trách"),
("床卡模式", "Chế độ thẻ giường"), ("列表模式", "Chế độ danh sách"),
("检索到", "Tìm thấy"), ("病区未选择", "Chưa chọn khu bệnh"),
("数据处理", "Xử lý dữ liệu"), ("在病区", "Tại khu bệnh"),
("患者信息同步", "Đồng bộ thông tin bệnh nhân"),
("关键字", "Từ khóa"),
("的编写申请暂未审批通过", "Đơn xin viết chưa được phê duyệt"),
("未在规定时间内书写", "Chưa viết trong thời gian quy định"),
("病历编写申请", "Đơn xin viết bệnh án"),
("申请科室", "Khoa xin"), ("申请医师", "Bác sĩ xin"),
("申请类型", "Loại đơn"), ("申请病历名称", "Tên bệnh án xin"),
("申请完成时间", "Thời gian hoàn thành đơn"),
("申请原因", "Lý do xin"), ("病案得分", "Điểm bệnh án"),
("缺陷数", "Số lỗi"), ("已査审", "Đã thẩm tra"),
("胃液", "Dịch dạ dày"), ("病情观察", "Theo dõi tình trạng"),
("护理措施效果", "Hiệu quả biện pháp chăm sóc"),
("签名", "Chữ ký"), ("更新", "Cập nhật"), ("移除行", "Xóa hàng"),
("未找到待提交的记录", "Không tìm thấy hồ sơ chờ nộp"),
("提交成功", "Nộp thành công"), ("提交失败", "Nộp thất bại"),
("我的", "Của tôi"),
# TemplateDesign
("更新", "Cập nhật"),
# SystemManage
("大类标识", "Mã định danh nhóm lớn"),
("关联业务表", "Bảng nghiệp vụ liên kết"),
("授权岗位", "Vị trí được ủy quyền"), ("正整数", "Số nguyên dương"),
("同步", "Đồng bộ"),
("确定要同步所有系统参数吗", "Bạn có chắc muốn đồng bộ tất cả tham số hệ thống?"),
("数据同步中", "Đang đồng bộ dữ liệu"), ("今天", "Hôm nay"),
("日志内容", "Nội dung nhật ký"), ("上级", "Cấp trên"),
("仅当前机构", "chỉ tổ chức hiện tại"),
("功能权限", "Quyền chức năng"),
("用于创建或修改角色信息", "Dùng để tạo hoặc sửa thông tin vai trò"),
("允许登录", "Cho phép đăng nhập"), ("停用", "Vô hiệu hóa"),
("注册信息", "Thông tin đăng ký"), ("安全验证", "Xác minh bảo mật"),
("新密码", "Mật khẩu mới"), ("确认新密码", "Xác nhận mật khẩu mới"),
("必须包含字母数字", "Phải chứa chữ cái và số"),
("必须包含字母数据", "Phải chứa chữ cái và số"),
("区域选择打印", "In theo vùng đã chọn"),
# Home
("患者一览", "Tổng quan bệnh nhân"), ("刷新当前", "Làm mới hiện tại"),
("浏览器缓存清除成功", "Đã xóa bộ nhớ đệm trình duyệt thành công"),
("清空缓存", "Xóa bộ nhớ đệm"), ("皮肤设置", "Cài đặt giao diện"),
("安全退出", "Thoát an toàn"), ("切换机构", "Chuyển tổ chức"),
("数据加载中", "Đang tải dữ liệu"),
("切换医疗机构", "Chuyển cơ sở y tế"),
# controller message
("修改锁定状态失败", "Sửa trạng thái khóa thất bại"),
# pass-11 strays found in dry run
("位", "ký tự"), ("嘱托", "Dặn dò"),
("两次输入的密码不一致", "Hai lần nhập mật khẩu không khớp"),
])

# Demo / placeholder content that must stay (sample document titles, home demo ticker)
SKIP_EXACT |= {
    "跨行业应用", "税收优惠政策管理", "限速器校验报告",
    "财长和央行行", "采购商城全新升级自营业正品",
    "科学发展作指导", "人水和谐路更宽",
}

# Longest-match first
KEYS = sorted(D.keys(), key=len, reverse=True)

def segment_translate(run):
    """Greedy longest-match segmentation; returns None if any part untranslatable.
    Special case: leading 是否 X -> "Có <X> không" (Vietnamese yes/no question order)."""
    if run.startswith("是否") and len(run) > 2:
        rest = segment_translate(run[2:])
        if rest is not None:
            return "Có " + rest + " không"
    # Backtracking segmentation: greedy longest-match can strand a tail
    # (e.g. 修改病历 + 大类 fails, while 修改 + 病历大类 succeeds).
    from functools import lru_cache

    @lru_cache(maxsize=None)
    def rec(i):
        if i == len(run):
            return ()
        for k in KEYS:
            if run.startswith(k, i):
                rest = rec(i + len(k))
                if rest is not None:
                    return (D[k],) + rest
        return None
    parts = rec(0)
    if parts is None:
        return None
    return re.sub(r'\s+', ' ', " ".join(parts)).strip()

ATTR_RE = re.compile(r'(value|name|id|data-field|data-value|for|data-name)\s*=\s*"')

def guarded_replace(line, fname, lineno, skipped, untranslated):
    """Replace CJK runs in one line honouring guards. Returns new line."""
    def repl(m):
        run = m.group(0); start = m.start()
        if run in SKIP_EXACT or any(run.startswith(p) for p in PROPER_PREFIXES):
            skipped.append((fname, lineno, run, "must-keep/proper noun/font")); return run
        prefix = line[:start]
        # attribute-value guard: nearest HTML attr=" before match without closing quote.
        # The attr name must be preceded by whitespace (HTML attribute), NOT by '.'
        # (JS .value assignments like btn.value = "..." are display text, not attributes).
        am = None
        for mm in ATTR_RE.finditer(prefix):
            if mm.start() == 0 or prefix[mm.start()-1] in ' \t<':
                am = mm
        if am and '"' not in prefix[am.end():]:
            attr = am.group(1)
            if attr == 'value':
                # <input type="button" value="..."> is a display label -> translate.
                # <option>/<param> values and submit/hidden/checkbox/radio values are data -> skip.
                tag_start = prefix.rfind('<')
                tag = prefix[tag_start:] if tag_start >= 0 else ''
                tagname = re.match(r'<\s*([A-Za-z]+)', tag)
                tn = tagname.group(1).lower() if tagname else ''
                if tn in ('option', 'param') or (tn == 'input' and any(
                        x in tag for x in ('type="submit"', 'type="hidden"',
                                           'type="checkbox"', 'type="radio"'))):
                    skipped.append((fname, lineno, run, "option/param/submit value (data)")); return run
                # otherwise fall through to translation
            else:
                skipped.append((fname, lineno, run, "attribute value (name/id/data-field)")); return run
        if 'font-family' in prefix[-60:]:
            skipped.append((fname, lineno, run, "font name")); return run
        if 'url(' in prefix[-80:] and ')' not in prefix[prefix.rfind('url('):]:
            skipped.append((fname, lineno, run, "filename inside url()")); return run
        if 'http' in prefix[-60:] and ('"' not in prefix[prefix.rfind('http'):]):
            skipped.append((fname, lineno, run, "URL")); return run
        # comparison guard (refined): only when the quoted string containing the run
        # is directly adjacent to == / != (operand), not merely on the same line.
        if ('==' in line or '!=' in line):
            q1 = prefix.rfind('"'); q2 = prefix.rfind("'")
            q = max(q1, q2)
            if q >= 0:
                qc = line[q]
                close = line.find(qc, m.end())
                if close > 0:
                    before = prefix[q-2:q].strip() if q >= 2 else prefix[:q].strip()
                    after = line[close+1:close+4].strip()
                    if before.endswith('==') or before.endswith('!=') or \
                       after.startswith('==') or after.startswith('!='):
                        skipped.append((fname, lineno, run, "comparison literal (adjacent to ==/!=)")); return run
        # object key: closing quote followed by ':' AND the opening quote is
        # preceded by '{' or ',' (a ternary `? "a" : b` is NOT a key).
        suffix = line[m.end():]
        if suffix.startswith('"') or suffix.startswith("'"):
            rest = suffix[1:].lstrip()
            if rest.startswith(':'):
                q1 = prefix.rfind('"'); q2 = prefix.rfind("'")
                q = max(q1, q2)
                before_q = prefix[:q].rstrip() if q >= 0 else ""
                if before_q.endswith('{') or before_q.endswith(','):
                    skipped.append((fname, lineno, run, "object key")); return run
        if run in D: return D[run]
        seg = segment_translate(run)
        if seg is not None: return seg
        untranslated.append((fname, lineno, run)); return run
    return CJK.sub(repl, line)

def strip_comments(text):
    """Return list of (line, in_comment) handling @* *@, <!-- -->, // comment-only lines."""
    out = []; in_block = False; in_html = False
    for raw in text.split('\n'):
        line = raw
        if in_block:
            if '@' in line and '*@' in line.split('@', 1)[1]:
                pass
            if '*@' in line:
                line = line.split('*@', 1)[1]; in_block = False
            else:
                out.append((raw, True)); continue
        if '@*' in line:
            pre, _, rest = line.partition('@*')
            if '*@' in rest:
                post = rest.split('*@', 1)[1]; out.append((pre + post, False)); continue
            out.append((pre, False)); in_block = True; continue
        if in_html:
            if '-->' in line:
                line = line.split('-->', 1)[1]; in_html = False
            else:
                out.append((raw, True)); continue
        if '<!--' in line:
            pre, _, rest = line.partition('<!--')
            if '-->' in rest:
                out.append((pre + rest.split('-->', 1)[1], False)); continue
            out.append((pre, False)); in_html = True; continue
        out.append((line, False))
    return out

def translate_cshtml(path, skipped, untranslated, changed):
    text = open(path, encoding='utf-8', errors='ignore').read()
    lines = strip_comments(text)
    new_lines = []; n = 0
    for idx, (line, is_comment) in enumerate(lines, 1):
        if is_comment:
            for run in CJK.findall(line):
                skipped.append((path, idx, run, "comment"))
            new_lines.append(line); continue
        stripped = line.strip()
        if stripped.startswith('//'):
            for run in CJK.findall(line):
                skipped.append((path, idx, run, "comment"))
            new_lines.append(line); continue
        # inline // comment: split code part and comment part (// not after ':')
        code, comment = line, ""
        mm = re.search(r'(?<!:)//', line)
        if mm:
            # avoid splitting URLs inside quotes: only split if // is outside quotes
            before = line[:mm.start()]
            if before.count('"') % 2 == 0 and before.count("'") % 2 == 0:
                code, comment = before, line[mm.start():]
                for run in CJK.findall(comment):
                    skipped.append((path, idx, run, "comment"))
        new = guarded_replace(code, path, idx, skipped, untranslated)
        if new != code: n += 1
        new_lines.append(new + comment)
    if n and not DRY:
        open(path, 'w', encoding='utf-8').write('\n'.join(new_lines))
    if n: changed.append((path, n))
    return n

def translate_controller(path, skipped, untranslated, changed):
    text = open(path, encoding='utf-8', errors='ignore').read()
    n = 0
    def repl(m):
        nonlocal n
        inner = m.group(2)
        runs = CJK.findall(inner)
        if not runs: return m.group(0)
        new = inner
        for run in runs:
            if run in D: new = new.replace(run, D[run]); 
            else:
                seg = segment_translate(run)
                if seg is not None: new = new.replace(run, seg)
                else: untranslated.append((path, 0, run))
        if new != inner: n += 1
        return m.group(1) + new + m.group(3)
    pat = re.compile(r'((?:Success|Error|Content)\s*\(\s*")([^"]*)("\s*\))')
    new_text = pat.sub(repl, text)
    if n and not DRY:
        open(path, 'w', encoding='utf-8').write(new_text)
    if n: changed.append((path, n))
    return n

def main():
    skipped, untranslated, changed = [], [], []
    total_runs = 0
    for dp, dn, fn in os.walk(ROOT):
        if 'bin' in dp or 'obj' in dp or 'wwwroot' in dp: continue
        for f in fn:
            p = os.path.join(dp, f)
            rel = p.replace(ROOT, "")
            if f.endswith('.cshtml') and not f.startswith('_DCSW') and 'testbl' not in f:
                total_runs += translate_cshtml(p, skipped, untranslated, changed)
            elif f.endswith('.cs') and ('Controllers' in dp):
                total_runs += translate_controller(p, skipped, untranslated, changed)
    print("changed-line counts per file:")
    for p, n in sorted(changed, key=lambda x: -x[1]): print(f"  {n} {p.replace(ROOT,'')}")
    print("total changed lines:", sum(n for _, n in changed))
    print("skipped occurrences:", len(skipped))
    print("untranslated occurrences:", len(untranslated))
    with open('/tmp/emr_skipped.txt', 'w', encoding='utf-8') as fh:
        for f, ln, run, why in skipped: fh.write(f"{f}:{ln}\t{run}\t{why}\n")
    with open('/tmp/emr_untranslated.txt', 'w', encoding='utf-8') as fh:
        for f, ln, run in untranslated: fh.write(f"{f}:{ln}\t{run}\n")
    # summary of untranslated by run
    c = collections.Counter(r for _, _, r in untranslated)
    print("top untranslated runs:")
    for r, n in c.most_common(80): print(f"  {n} {r}")
    cs = collections.Counter(w for _, _, _, w in skipped)
    print("skip reasons:", dict(cs))

if __name__ == '__main__':
    main()


