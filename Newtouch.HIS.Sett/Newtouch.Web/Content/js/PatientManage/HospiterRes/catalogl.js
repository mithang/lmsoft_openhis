function mlbh(mlbh)
{
    var colModel;
    switch (mlbh) {
        case "1301":
            colModel = [
                { label: 'Mã danh mục y tế', name: 'MED_LIST_CODG', width: 100, align: 'left' },
                { label: 'Tên', name: 'REG_NAME', width: 100, align: 'left' },
                { label: 'Mã số', name: 'DRUGSTDCODE', width: 100, align: 'left' },
                { label: 'ThuốcDạng bào chế', name: 'DRUG_DOSFORM', width: 60, align: 'left' },
                { label: 'Dạng bào chếTên', name: 'DRUG_DOSFORM_NAME', width: 100, align: 'left',hidden:true },
                { label: 'Loại thuốc', name: 'DRUG_TYPE', width: 50, align: 'left' },
                { label: 'Quy cách thuốc', name: 'DRUG_SPEC', width: 50, align: 'left' }, 
                { label: '注册Dạng bào chế', name: 'REG_DOSFORM', width: 60, align: 'left' },
                { label: '注册Quy cách', name: 'REG_SPEC', width: 60, align: 'left' },
                { label: 'Liều dùng mỗi lần', name: 'EACH_DOS', width: 100, align: 'left' },
                { label: 'Cờ thuốc không kê đơn', name: 'OTC_FLAG', width: 20, align: 'left', hidden: true },
                { label: 'Chất liệu đóng gói', name: 'PACMATL', width: 50, align: 'left' },
                { label: 'Công năng chủ trị', name: 'EFCC_ATD', width: 100, align: 'left' },
                { label: 'Số lượng đóng gói tối thiểu', name: 'MIN_PAC_CNT', width: 40, align: 'left' },
                { label: 'Đơn vị đóng gói tối thiểu', name: 'MIN_PACUNT', width: 40, align: 'left' },
                { label: 'Đơn vị bào chế tối thiểu', name: 'MIN_PREPUNT', width: 40, align: 'left' },
                { label: 'Hạn dùng thuốc', name: 'DRUG_EXPY', width: 60, align: 'left' },
                { label: 'Đơn vị tính giá tối thiểu', name: 'MIN_PRCUNT', width: 40, align: 'left' },
                { label: 'Mã doanh nghiệp sản xuất', name: 'PRODENTP_CODE', width: 80, align: 'left' },
                { label: 'Tên doanh nghiệp sản xuất', name: 'PRODENTP_NAME', width: 120, align: 'left' },
                { label: '市场Trạng thái', name: 'MKT_STAS', width: 50, align: 'left' },
                { label: 'Cờ hiệu lực', name: 'VALI_FLAG', width: 20, align: 'left' },
                { label: 'Số phiên bản', name: 'VER', width: 60, align: 'left' },
                { label: 'Tên phiên bản', name: 'VER_NAME', width: 120, align: 'left' },

            ];
            break;
        case "1302":
            colModel = [
                { label: 'Mã danh mục y tế', name: 'MED_LIST_CODG', width: 100, align: 'left' },
                { label: '单味药Tên', name: 'TCMHERB_NAME', width: 100, align: 'left' },
                { label: '药用Vị trí', name: 'MED_PART', width: 100, align: 'left' },
                { label: 'Cách dùng thông thường', name: 'CNVL_USED', width: 60, align: 'left' },
                { label: '性味', name: 'NATFLA', width: 100, align: 'left' },
                { label: '品种', name: 'CAT', width: 60, align: 'left' },
                { label: 'Ngày bắt đầu', name: 'BEGNDATE', width: 100, align: 'left' },
                { label: 'Ngày kết thúc', name: 'ENDDATE', width: 80, align: 'left' },
                { label: 'Cờ hiệu lực', name: 'VALI_FLAG', width: 40, align: 'left' },
                { label: 'Số phiên bản', name: 'VER', width: 60, align: 'left' },
                { label: 'Tên phiên bản', name: 'VER_NAME', width: 120, align: 'left' },
                { label: 'Tên dược liệu', name: 'MLMS_NAME', width: 60, align: 'left' },
                { label: 'Công năng chủ trị', name: 'EFCC_ATD', width: 120, align: 'left' },
                { label: '炮制Phương thức', name: 'PSDG_MTD', width: 120, align: 'left' },
                { label: 'Phân loại công dụng', name: 'ECY_TYPE', width: 80, align: 'left' },
                { label: '药材种来Nguồn', name: 'MLMS_CAT_SOUC', width: 80, align: 'left' },
            ];
            break;
        case "1303":
            colModel = [
                 { label: 'Mã danh mục y tế', name: 'MED_LIST_CODG', width: 100, align: 'left' },
                { label: 'Tên thương mại thuốc', name: 'DRUG_PRODNAME', width: 100, align: 'left' },
                { label: 'Dạng bào chế', name: 'DOSFORM', width: 80, align: 'left' },
                { label: 'Công năng chủ trị', name: 'EFCC_ATD', width: 120, align: 'left' },
                { label: 'Quy cách thuốc', name: 'DRUG_SPEC', width: 100, align: 'left' },
                { label: 'Liều dùng mỗi lần', name: 'EACH_DOS', width: 100, align: 'left' },
                { label: 'Loại thuốc', name: 'DRUG_TYPE', width: 80, align: 'left' },
                { label: 'Chất liệu đóng gói', name: 'PACMATL', width: 100, align: 'left' },
                { label: 'Số lượng đóng gói tối thiểu', name: 'MIN_PAC_CNT', width: 50, align: 'left' },
                { label: 'Đơn vị đóng gói tối thiểu', name: 'MIN_PACUNT', width: 50, align: 'left' },
                { label: 'Đơn vị bào chế tối thiểu', name: 'MIN_PREPUNT', width: 50, align: 'left' },
                { label: 'Tên doanh nghiệp sản xuất', name: 'EFCC_ATD', width: 120, align: 'left' },
                { label: '生产企业Địa chỉ', name: 'PSDG_MTD', width: 120, align: 'left' },
                { label: 'Cờ hiệu lực', name: 'VALI_FLAG', width: 40, align: 'left' },
                { label: 'Thời gian bắt đầu', name: 'BEGNTIME', width: 80, align: 'left' },
                { label: 'Thời gian kết thúc', name: 'ENDTIME', width: 80, align: 'left' },
                { label: 'Số phiên bản', name: 'VER', width: 60, align: 'left' },
                { label: 'Tên phiên bản', name: 'VER_NAME', width: 120, align: 'left' },
            ];
            break;
        case "1305":
            colModel = [
                 { label: 'Mã danh mục y tế', name: 'MED_LIST_CODG', width: 100, align: 'left' },
                { label: 'Đơn vị tính giá', name: 'PRCUNT', width: 60, align: 'left' },
                { label: 'Đơn vị tính giáTên', name: 'PRCUNT_NAME', width: 100, align: 'left' },
                { label: 'Giải thích hạng mục khám chữa bệnh', name: 'TRT_ITEM_DSCR', width: 120, align: 'left' },
                { label: 'Khám chữa bệnhNội dung loại trừ', name: 'TRT_EXCT_CONT', width: 120, align: 'left' },
                { label: 'Cờ hiệu lực', name: 'VALI_FLAG', width: 60, align: 'left' },
                { label: '服务Loại hạng mục', name: 'SERVITEM_TYPE', width: 80, align: 'left' },
                { label: 'Tên hạng mục dịch vụ y tế', name: 'SERVITEM_NAME', width: 100, align: 'left' },
                { label: 'Ngày bắt đầu', name: 'BEGNDATE', width: 80, align: 'left' },
                { label: 'Ngày kết thúc', name: 'ENDDATE', width: 80, align: 'left' },
                { label: 'Số phiên bản', name: 'VER', width: 60, align: 'left' },
                { label: 'Tên phiên bản', name: 'VER_NAME', width: 120, align: 'left' },
            ];
            break;
        case "1306":
            colModel = [
                 { label: 'Mã danh mục y tế', name: 'MED_LIST_CODG', width: 100, align: 'left' },
                { label: 'Bảo hiểm y tếTên chung', name: 'HI_GENNAME', width: 100, align: 'left' },
                { label: 'Quy cách', name: 'SPEC', width: 60, align: 'left' },
                { label: '耗材材质', name: 'MCS_MATL', width: 100, align: 'left' },
                { label: 'Ngày bắt đầu', name: 'BEGNDATE', width: 100, align: 'left' },
                { label: 'Ngày kết thúc', name: 'ENDDATE', width: 100, align: 'left' },
                { label: 'Mã doanh nghiệp sản xuất', name: 'PRODENTP_CODE', width: 100, align: 'left' },
                { label: 'Tên doanh nghiệp sản xuất', name: 'PRODENTP_NAME', width: 120, align: 'left' },
                { label: 'Cờ hiệu lực', name: 'VALI_FLAG', width: 40, align: 'left' },
                { label: 'Số phiên bản', name: 'VER', width: 60, align: 'left' },
                { label: 'Tên phiên bản', name: 'VER_NAME', width: 120, align: 'left' },
            ];
            break;
        case "1307":
            colModel = [
                { label: '西医疾病诊断ID', name: 'DIAG_LIST_ID', width: 100, align: 'left', hidden:true },
                 { label: '章', name: 'CPR', width: 30, align: 'left' },
                { label: '章Phạm vi mã', name: 'CPR_CODE_SCP', width: 80, align: 'left' },
                { label: 'Tên chương', name: 'CPR_NAME', width: 100, align: 'left' },
                { label: 'Phạm vi mã tiết', name: 'SEC_CODE_SCP', width: 80, align: 'left' },
                { label: 'Tên tiết', name: 'SEC_NAME', width: 100, align: 'left' },
                { label: 'Mã loại mục', name: 'CGY_CODE', width: 60, align: 'left' },
                { label: 'Tên loại mục', name: 'CGY_NAME', width: 100, align: 'left' },
                { label: 'Mã tiểu mục', name: 'SOR_CODE', width: 60, align: 'left' },
                { label: 'Tên tiểu mục', name: 'SOR_NAME', width: 100, align: 'left' },
                { label: 'Mã chẩn đoán', name: 'DIAG_CODE', width: 80, align: 'left' },
                { label: 'Tên chẩn đoán', name: 'DIAG_NAME', width: 100, align: 'left' },
                { label: 'Dấu hiệu sử dụng', name: 'USED_STD', width: 40, align: 'left' },
                { label: 'Số phiên bản', name: 'VER', width: 60, align: 'left' },
                { label: 'Tên phiên bản', name: 'VER_NAME', width: 120, align: 'left' },
            ];
            break;
        case "1308":
            colModel = [
                { label: 'Danh mục chuẩn phẫu thuậtID', name: 'OPRN_STD_LIST_ID', width: 100, align: 'left', hidden: true },
                 { label: '章', name: 'CPR', width: 30, align: 'left' },
                { label: '章Phạm vi mã', name: 'CPR_CODE_SCP', width: 60, align: 'left' },
                { label: 'Tên chương', name: 'CPR_NAME', width: 100, align: 'left' },
                { label: 'Mã loại mục', name: 'CGY_CODE', width: 60, align: 'left' },
                { label: 'Tên loại mục', name: 'CGY_NAME', width: 100, align: 'left' },
                { label: 'Mã tiểu mục', name: 'SOR_CODE', width: 60, align: 'left' },
                { label: 'Tên tiểu mục', name: 'SOR_NAME', width: 100, align: 'left' },
                { label: 'Mã chi tiết', name: 'DTLS_CODE', width: 60, align: 'left' },
                { label: 'Tên chi tiết', name: 'DTLS_NAME', width: 100, align: 'left' },
                { label: 'Mã thao tác phẫu thuật', name: 'OPRN_OPRT_CODE', width: 60, align: 'left' },
                { label: 'Tên thao tác phẫu thuật', name: 'OPRN_OPRT_NAME', width: 100, align: 'left' },
                { label: 'Cờ hiệu lực', name: 'VALI_FLAG', width: 40, align: 'left' },
                { label: 'Số phiên bản', name: 'VER', width: 60, align: 'left' },
                { label: 'Tên phiên bản', name: 'VER_NAME', width: 120, align: 'left' },
            ];
            break;
        case "1309":
            colModel = [
                { label: 'Mã danh mục bệnh mãn tính/đặc biệt ngoại trú', name: 'OPSP_DISE_CODE', width: 120, align: 'left' },
                { label: 'Tên nhóm lớn bệnh mãn tính/đặc biệt ngoại trú', name: 'OPSP_DISE_MAJCLS_NAME', width: 120, align: 'left' },
                { label: 'Tên phân loại nhỏ bệnh mãn tính/đặc biệt ngoại trú', name: 'OPSP_DISE_SUBD_CLSS_NAME', width: 120, align: 'left' },
                { label: 'Vùng BHYT', name: 'ADMDVS', width: 100, align: 'left' },
                { label: 'Cờ hiệu lực', name: 'VALI_FLAG', width: 60, align: 'left' },
                { label: 'Tên bệnh mãn tính/đặc biệt ngoại trú', name: 'OPSP_DISE_NAME', width: 120, align: 'left' },
                { label: 'Mã nhóm lớn bệnh mãn tính/đặc biệt ngoại trú', name: 'OPSP_DISE_MAJCLS_CODE', width: 80, align: 'left' },
                { label: 'Số phiên bản', name: 'VER', width: 60, align: 'left' },
                { label: 'Tên phiên bản', name: 'VER_NAME', width: 120, align: 'left' },
            ];
            break;
        case "1310":
            colModel = [
                { label: 'Danh mục quyết toán theo bệnhID', name: 'DISE_SETL_LIST_ID', width: 100, align: 'left', hidden: true },
                 { label: 'Mã danh mục bệnh quyết toán theo bệnh', name: 'BYDISE_SETL_LIST_CODE', width: 100, align: 'left' },
                { label: 'Tên bệnh quyết toán theo bệnh', name: 'BYDISE_SETL_DISE_NAME', width: 120, align: 'left' },
                { label: 'Mã thao tác phẫu thuật giới hạn', name: 'QUA_OPRN_OPRT_CODE', width: 100, align: 'left' },
                { label: 'Tên thao tác phẫu thuật giới hạn', name: 'QUA_OPRN_OPRT_NAME', width: 120, align: 'left' },
                { label: 'Cờ hiệu lực', name: 'VALI_FLAG', width: 60, align: 'left' },
                { label: 'Số phiên bản', name: 'VER', width: 60, align: 'left' },
                { label: 'Tên phiên bản', name: 'VER_NAME', width: 120, align: 'left' },
            ];
            break;
        case "1311":
            colModel = [
                 { label: 'Danh mục điều trị phẫu thuật trong ngàyID', name: 'DAYSRG_TRT_LIST_ID', width: 100, align: 'left', hidden: true },
                 { label: 'Mã bệnh phẫu thuật trong ngày', name: 'DAYSRG_DISE_LIST_CODE', width: 120, align: 'left' },
                { label: 'Tên bệnh phẫu thuật trong ngày', name: 'DAYSRG_DISE_NAME', width: 120, align: 'left' },
                { label: 'Cờ hiệu lực', name: 'VALI_FLAG', width: 60, align: 'left' },
                { label: 'Tên thao tác phẫu thuật', name: 'QUA_OPRN_OPRT_CODE', width: 100, align: 'left' },
                { label: 'Mã thao tác phẫu thuật', name: 'QUA_OPRN_OPRT_NAME', width: 120, align: 'left' },
                { label: 'Số phiên bản', name: 'VER', width: 60, align: 'left' },
                { label: 'Tên phiên bản', name: 'VER_NAME', width: 120, align: 'left' },
            ];
            break;
        case "1313":
            colModel = [
                { label: 'Hình thái học u bướuID', name: 'TMOR_MPY_ID', width: 100, align: 'left', hidden: true },
                 { label: 'U bướu/Mã loại tế bào', name: 'TMOR_CELL_TYPE_CODE', width: 120, align: 'left' },
                { label: 'U bướu/Loại tế bào', name: 'TMOR_CELL_TYPE', width: 120, align: 'left' },
                { label: 'Mã phân loại hình thái', name: 'MPY_TYPE_CODE', width: 100, align: 'left' },
                { label: 'Phân loại hình thái', name: 'MPY_TYPE', width: 100, align: 'left' },
                { label: 'Cờ hiệu lực', name: 'VALI_FLAG', width: 80, align: 'left' },
                { label: 'Số phiên bản', name: 'VER', width: 60, align: 'left' },
                { label: 'Tên phiên bản', name: 'VER_NAME', width: 120, align: 'left' },
            ];
            break;
        case "1314":
            colModel = [
                { label: 'Chẩn đoán bệnh y học cổ truyềnID', name: 'TCM_DIAG_ID', width: 100, align: 'left', hidden: true },
                 { label: 'Chẩn đoán bệnh y học cổ truyền', name: 'CATY_CGY_CODE', width: 100, align: 'left' },
                { label: 'Tên loại mục khoa', name: 'CATY_CGY_NAME', width: 120, align: 'left' },
                { label: 'Mã loại mục phân loại hệ chuyên khoa', name: 'SPCY_SYS_TAXA_CODE', width: 120, align: 'left' },
                { label: 'Tên loại mục phân loại hệ chuyên khoa', name: 'SPCY_SYS_TAXA_NAME', width: 120, align: 'left' },
                { label: 'Mã phân loại bệnh', name: 'DISE_TYPE_CODE', width: 100, align: 'left' },
                { label: 'Tên phân loại bệnh', name: 'DISE_TYPE_NAME', width: 120, align: 'left' },
                { label: 'Cờ hiệu lực', name: 'VALI_FLAG', width: 60, align: 'left' },
                { label: 'Số phiên bản', name: 'VER', width: 60, align: 'left' },
                { label: 'Tên phiên bản', name: 'VER_NAME', width: 100, align: 'left' },
            ];
            break;
        case "1315":
            colModel = [
                { label: 'Chứng hậu y học cổ truyềnID', name: 'TCMSYMP_ID', width: 100, align: 'left', hidden: true },
                 { label: 'Mã loại mục chứng hậu', name: 'SDS_CGY_CODE', width: 100, align: 'left' },
                { label: 'Tên loại mục chứng hậu', name: 'SDS_CGY_NAME', width: 120, align: 'left' },
                { label: 'Mã thuộc tính chứng hậu', name: 'SDS_ATTR_CODE', width: 120, align: 'left' },
                { label: 'Thuộc tính chứng hậu', name: 'SDS_ATTR', width: 120, align: 'left' },
                { label: 'Mã phân loại chứng hậu', name: 'SDS_TYPE_CODE', width: 100, align: 'left' },
                { label: 'Tên phân loại chứng hậu', name: 'SDS_TYPE_NAME', width: 120, align: 'left' },
                { label: 'Cờ hiệu lực', name: 'VALI_FLAG', width: 60, align: 'left' },
                { label: 'Số phiên bản', name: 'VER', width: 60, align: 'left' },
                { label: 'Tên phiên bản', name: 'VER_NAME', width: 100, align: 'left' },
            ];
            break;
        case "1320":
            colModel = [
                { label: 'Mã hạt công thức thuốc Đông y', name: 'ZYPFKL_CODE', width: 100, align: 'left' },
                { label: 'Tên hạt công thức thuốc Đông y', name: 'ZYPFKL_NAME', width: 100, align: 'left' },
                { label: 'Dược liệu chính gốc', name: 'DDYC', width: 100, align: 'left' },
                { label: 'Quy cách', name: 'GG', width: 100, align: 'left' },
                { label: 'Tiêu chuẩn thực hiện hạt công thức thuốc Đông y', name: 'ZYPFKLZXBZ', width: 100, align: 'left' },
                { label: 'Thông tin kiểm tra phản ứng phụ', name: 'BLFYJCXX', width: 100, align: 'left' },
                { label: 'Mã thuốc Đông y cắt lát', name: 'ZYYP_CODE', width: 60, align: 'left' },
                { label: 'Tên thuốc Đông y cắt lát', name: 'ZYYP_NAME', width: 60, align: 'left' },
                { label: 'Tên dược liệu', name: 'YC_NAME', width: 60, align: 'left' },
                { label: 'Phân loại công dụng', name: 'GXFL', width: 100, align: 'left' },
                { label: 'Công năng chủ trị', name: 'GNZZ', width: 100, align: 'left' },
                { label: 'Cách dùng thông thường', name: 'CGYF', width: 100, align: 'left' },
                { label: 'Thời gian cập nhật dữ liệu', name: 'SJGXSJ', width: 100, align: 'left' },
                { label: 'Mã cơ sở tạo', name: 'CJJG_CODE', width: 100, align: 'left' },
                { label: 'Vùng BHYT', name: 'YBQH', width: 100, align: 'left' },
                { label: 'Số phiên bản', name: 'VER', width: 100, align: 'left' }
            ];
            break;
        case "1321":
            colModel = [
                { label: 'Mã danh mục y tế', name: 'YLML_CODE', width: 100, align: 'left' },
                { label: 'Tên hạng mục dịch vụ y tế', name: 'YLFWXM_NAME', width: 120, align: 'left' },
                { label: 'Đầu ra hạng mục dịch vụ y tế', name: 'YLFWXMSC', width: 120, align: 'left' },
                { label: 'Cấu thành giá', name: 'JGGC', width: 100, align: 'left' },
                { label: 'Đơn vị tính giá', name: 'JJDW', width: 60, align: 'left' },
                { label: 'Giải thích tính giá', name: 'JJSM', width: 120, align: 'left' },
                { label: 'Giải thích hạng mục khám chữa bệnh', name: 'ZLXMSM', width: 200, align: 'left' },
                { label: 'Khẩu độ loại chi phí', name: 'FYLXKJ', width: 60, align: 'left' },
                { label: 'Thời gian có hiệu lực', name: 'EFFECTIVE_TIME', width: 80, align: 'left' },
                { label: 'Thời gian hết hiệu lực', name: 'EXPIRATION_TIME', width: 80, align: 'left' },
                { label: 'Số phiên bản', name: 'VER', width: 60, align: 'left' },
                { label: 'Tên phiên bản', name: 'VER_NAME', width: 120, align: 'left' },
            ];
            break;
            //Danh mục loại tra cứu
        case "1304":
            colModel = [
                { label: 'Mã danh mục y tế', name: 'med_list_codg', width: 100, align: 'left', },
                { label: 'Tên thương mại thuốc', name: 'drug_prodname', width: 100, align: 'left' },
                { label: 'Mã tên chung', name: 'genname_codg', width: 120, align: 'left' },
                { label: 'Tên chung thuốc', name: 'drug_genname', width: 120, align: 'left' },
                { label: 'Loại thuốc dân tộc', name: 'ethdrug_type', width: 100, align: 'left' },
                { label: 'Tên hóa học', name: 'chemname', width: 100, align: 'left' },
                { label: 'Tên khác', name: 'alis', width: 100, align: 'left' },
                { label: 'Tên tiếng Anh', name: 'eng_name', width: 100, align: 'left' },
                { label: 'Dạng bào chế', name: 'dosform', width: 120, align: 'left' },
                { label: 'Liều dùng mỗi lần', name: 'each_dos', width: 60, align: 'left' },
                { label: 'Tần suất sử dụng', name: 'used_frqu', width: 60, align: 'left' },
                { label: 'Mã thuốc quốc gia', name: 'nat_drug_no', width: 100, align: 'left' },
                { label: 'Cách dùng', name: 'used_mtd', width: 100, align: 'left', },
                { label: 'Thành phần', name: 'ing', width: 80, align: 'left' },
                { label: 'Tính chất', name: 'chrt', width: 100, align: 'left' },
                { label: 'Phản ứng phụ', name: 'defs', width: 120, align: 'left' },
                { label: 'Chống chỉ định', name: 'tabo', width: 100, align: 'left' },
                { label: 'Lưu ý', name: 'mnan', width: 100, align: 'left' },
                { label: 'Bảo quản', name: 'stog', width: 60, align: 'left' },
                { label: 'Quy cách thuốc', name: 'drug_spec', width: 60, align: 'left' },
                { label: 'Loại đơn vị tính giá', name: 'prcunt_type', width: 60, align: 'left' },
                { label: 'Cờ thuốc không kê đơn', name: 'otc_flag', width: 60, align: 'left' },
                { label: 'Chất liệu đóng gói', name: 'pacmatl', width: 60, align: 'left' },
                { label: 'Quy cách đóng gói', name: 'pacspec', width: 60, align: 'left' },
                 { label: 'Đơn vị sử dụng tối thiểu', name: 'min_useunt', width: 60, align: 'left' },
                { label: 'Đơn vị bán tối thiểu', name: 'min_salunt', width: 60, align: 'left' },
                { label: 'Hướng dẫn sử dụng', name: 'manl', width: 60, align: 'left' },
                { label: 'Đường dùng thuốc', name: 'rute', width: 60, align: 'left' },
                { label: 'Ngày bắt đầu', name: 'begndate', width: 60, align: 'left' },
                { label: 'Ngày kết thúc', name: 'enddate', width: 60, align: 'left' },
                { label: 'Phân loại dược lý', name: 'pham_type', width: 60, align: 'left' },
                { label: 'Ghi chú', name: 'memo', width: 60, align: 'left' },
                { label: 'Số lượng đóng gói', name: 'pac_cnt', width: 60, align: 'left' },
                 { label: 'Đơn vị đo tối thiểu', name: 'min_unt', width: 60, align: 'left' },
                { label: 'Số lượng đóng gói tối thiểu', name: 'min_pac_cnt', width: 60, align: 'left' },
                 { label: 'Đơn vị đóng gói tối thiểu', name: 'min_pacunt', width: 60, align: 'left' },
                { label: 'Đơn vị bào chế tối thiểu', name: 'min_prepunt', width: 60, align: 'left' },
                { label: 'Hạn dùng thuốc', name: 'drug_expy', width: 60, align: 'left' },
                { label: 'Công năng chủ trị', name: 'efcc_atd', width: 60, align: 'left' },
                { label: 'Đơn vị tính giá tối thiểu', name: 'min_prcunt', width: 60, align: 'left' },
                { label: 'Mã ghi nhớ Wubi', name: 'wubi', width: 60, align: 'left' },
                { label: 'Mã ghi nhớ phiên âm', name: 'pinyin', width: 60, align: 'left' },
                { label: 'Cờ hiệu lực', name: 'vali_flag', width: 60, align: 'left' },
                { label: 'Số ghi duy nhất', name: 'rid', width: 60, align: 'left' },
                { label: 'Thời gian tạo dữ liệu', name: 'crte_time', width: 60, align: 'left' },
                { label: 'Thời gian cập nhật dữ liệu', name: 'updt_time', width: 60, align: 'left' },
                { label: 'Người tạo', name: 'crter_id', width: 60, align: 'left' },
                { label: 'Tên người tạo', name: 'crter_name', width: 60, align: 'left' },
                { label: 'Cơ sở thực hiện tạo', name: 'crte_optins_no', width: 60, align: 'left' },
                { label: 'Người thực hiện', name: 'opter_id', width: 60, align: 'left' },
                { label: 'Tên người thực hiện', name: 'opter_name', width: 60, align: 'left' },
                { label: 'Thời gian thực hiện', name: 'opt_time', width: 60, align: 'left' },
                { label: 'Cơ sở thực hiện', name: 'optins_no', width: 60, align: 'left' },
                //{ label: 'Công năng chủ trị', name: 'efcc_atd', width: 100, align: 'left' },
                //{ label: 'Cờ hiệu lực', name: 'vali_flag', width: 40, align: 'left' },
                { label: 'Số phiên bản', name: 'ver', width: 60, align: 'left' }
            ];
            break;
        case "1312":
            colModel = [
                { label: 'Mã danh mục BHYT', name: 'hilist_code', width: 100, align: 'left' },
                { label: 'Tên danh mục BHYT', name: 'hilist_name', width: 100, align: 'left' },
                { label: 'Vùng BHYT của cơ sở tham gia bảo hiểm', name: 'insu_admdvs', width: 100, align: 'left' },
                { label: 'Ngày bắt đầu', name: 'begndate', width: 100, align: 'left' },
                { label: 'Ngày kết thúc', name: 'enddate', width: 100, align: 'left' },
                { label: 'Loại hạng mục thu phí y tế', name: 'med_chrgitm_type', width: 60, align: 'left' },
                { label: 'Cấp hạng mục thu phí', name: 'chrgitm_lv', width: 60, align: 'left' },
                { label: 'Cờ hạn chế sử dụng', name: 'lmt_used_flag', width: 60, align: 'left' },
                { label: 'Loại danh mục', name: 'list_type', width: 60, align: 'left' },
                { label: 'Cờ sử dụng y tế', name: 'med_use_flag', width: 100, align: 'left' },
                { label: 'Cờ sử dụng sinh đẻ', name: 'matn_used_flag', width: 100, align: 'left' },
                { label: 'Loại sử dụng danh mục BHYT', name: 'hilist_use_type', width: 100, align: 'left' },
                { label: 'Loại sử dụng hạn chế thuốc phối hợp', name: 'lmt_cpnd_type', width: 100, align: 'left' },
                { label: 'Mã ghi nhớ Wubi', name: 'wubi', width: 100, align: 'left' },
                { label: 'Mã ghi nhớ phiên âm', name: 'pinyin', width: 100, align: 'left' },
                { label: 'Ghi chú', name: 'memo', width: 100, align: 'left' },
                { label: 'Cờ hiệu lực', name: 'vali_flag', width: 100, align: 'left' },
                { label: 'Số ghi duy nhất', name: 'rid', width: 100, align: 'left' },
                { label: 'Thời gian cập nhật', name: 'updt_time', width: 100, align: 'left' },
                { label: 'Người tạo', name: 'crter_id', width: 100, align: 'left' },
                { label: 'Tên người tạo', name: 'crter_name', width: 100, align: 'left' },
                { label: 'Thời gian tạo', name: 'crte_time', width: 100, align: 'left' },
                { label: 'Cơ sở tạo', name: 'crte_optins_no', width: 100, align: 'left' },
                { label: 'Người thực hiện', name: 'opter_id', width: 100, align: 'left' },
                { label: 'Tên người thực hiện', name: 'opter_name', width: 100, align: 'left' },
                { label: 'Thời gian thực hiện', name: 'opt_time', width: 100, align: 'left' },
                { label: 'Cơ sở thực hiện', name: 'optins_no', width: 100, align: 'left' },
                { label: 'Khu vực điều phối chung', name: 'poolarea_no', width: 100, align: 'left' }
            ];
            break;
        case "1316":
            colModel = [
                { label: 'Mã danh mục y tế', name: 'med_list_codg', width: 100, align: 'left' },
                { label: 'Mã danh mục BHYT', name: 'hilist_code', width: 100, align: 'left' },
                { label: 'Loại danh mục', name: 'list_type', width: 100, align: 'left' },
                { label: 'Vùng BHYT của cơ sở tham gia bảo hiểm', name: 'insu_admdvs', width: 100, align: 'left' },
                { label: 'Ngày bắt đầu', name: 'begndate', width: 100, align: 'left' },
                { label: 'Ngày kết thúc', name: 'enddate', width: 60, align: 'left' },
                { label: 'Ghi chú', name: 'memo', width: 60, align: 'left' },
                { label: 'Cờ hiệu lực', name: 'vali_flag', width: 60, align: 'left' },
                { label: 'Số ghi duy nhất', name: 'rid', width: 60, align: 'left' },
                { label: 'Thời gian cập nhật', name: 'updt_time', width: 100, align: 'left' },
                { label: 'Người tạo', name: 'crter_id', width: 100, align: 'left' },
                { label: 'Tên người tạo', name: 'crter_name', width: 100, align: 'left' },
                { label: 'Thời gian tạo', name: 'crte_time', width: 100, align: 'left' },
                { label: 'Cơ sở tạo', name: 'crte_optins_no', width: 100, align: 'left' },
                { label: 'Người thực hiện', name: 'opter_id', width: 100, align: 'left' },
                { label: 'Tên người thực hiện', name: 'opter_name', width: 100, align: 'left' },
                { label: 'Thời gian thực hiện', name: 'opt_time', width: 100, align: 'left' },
                { label: 'Cơ sở thực hiện', name: 'optins_no', width: 100, align: 'left' },
                { label: 'Khu vực điều phối chung', name: 'poolarea_no', width: 100, align: 'left' }
            ];
            break;
        case "1317":
            colModel = [
                { label: 'Mã cơ sở y dược định điểm', name: 'fixmedins_code', width: 100, align: 'left' },
                { label: 'Mã danh mục cơ sở y dược định điểm', name: 'medins_list_codg', width: 100, align: 'left' },
                { label: 'Tên danh mục cơ sở y dược định điểm', name: 'medins_list_name', width: 100, align: 'left' },
                { label: 'Vùng BHYT của cơ sở tham gia bảo hiểm', name: 'insu_admdvs', width: 100, align: 'left' },
                { label: 'Loại danh mục', name: 'list_type', width: 100, align: 'left' },
                { label: 'Mã danh mục y tế', name: 'med_list_codg', width: 100, align: 'left' },
                { label: 'Ngày bắt đầu', name: 'begndate', width: 60, align: 'left' },
                { label: 'Ngày kết thúc', name: 'enddate', width: 60, align: 'left' },
                { label: 'Số phê duyệt', name: 'aprvno', width: 60, align: 'left' },
                { label: 'Dạng bào chế', name: 'dosform', width: 100, align: 'left' },
                { label: 'Nội dung loại trừ', name: 'exct_cont', width: 100, align: 'left' },
                { label: 'Nội hàm hạng mục', name: 'item_cont', width: 100, align: 'left' },
                { label: 'Đơn vị tính giá', name: 'prcunt', width: 100, align: 'left' },
                { label: 'Quy cách', name: 'spec', width: 100, align: 'left' },
                { label: 'Quy cách đóng gói', name: 'pacspec', width: 100, align: 'left' },
                { label: 'Ghi chú', name: 'memo', width: 100, align: 'left' },
                { label: 'Cờ hiệu lực', name: 'vali_flag', width: 100, align: 'left' },
                { label: 'Số ghi duy nhất', name: 'rid', width: 100, align: 'left' },
                { label: 'Thời gian cập nhật', name: 'updt_time', width: 100, align: 'left' },
                { label: 'Người tạo', name: 'crter_id', width: 100, align: 'left' },
                { label: 'Tên người tạo', name: 'crter_name', width: 100, align: 'left' },
                { label: 'Thời gian tạo', name: 'crte_time', width: 100, align: 'left' },
                { label: 'Cơ sở tạo', name: 'crte_optins_no', width: 100, align: 'left' },
                { label: 'Người thực hiện', name: 'opter_id', width: 100, align: 'left' },
                { label: 'Tên người thực hiện', name: 'opter_name', width: 100, align: 'left' },
                { label: 'Thời gian thực hiện', name: 'opt_time', width: 100, align: 'left' },
                { label: 'Cơ sở thực hiện', name: 'optins_no', width: 100, align: 'left' },
                { label: 'Khu vực điều phối chung', name: 'poolarea_no', width: 100, align: 'left' }
            ];
            break;
        case "1318":
            colModel = [
                { label: 'Mã danh mục BHYT', name: 'hilist_code', width: 100, align: 'left' },
                { label: 'Loại giới hạn giá danh mục BHYT', name: 'hilist_lmtpric_type', width: 100, align: 'left' },
                { label: 'Cách xử lý vượt giới hạn danh mục BHYT', name: 'overlmt_dspo_way', width: 100, align: 'left' },
                { label: 'Vùng BHYT của cơ sở tham gia bảo hiểm', name: 'insu_admdvs', width: 100, align: 'left' },
                { label: 'Ngày bắt đầu', name: 'begndate', width: 100, align: 'left' },
                { label: 'Ngày kết thúc', name: 'enddate', width: 100, align: 'left' },
                { label: 'Số tiền giới hạn định giá danh mục BHYT', name: 'hilist_pric_uplmt_amt', width: 60, align: 'left' },
                { label: 'Cờ hiệu lực', name: 'vali_flag', width: 60, align: 'left' },
                { label: 'Số ghi duy nhất', name: 'rid', width: 60, align: 'left' },
                { label: 'Thời gian cập nhật', name: 'updt_time', width: 100, align: 'left' },
                { label: 'Người tạo', name: 'crter_id', width: 100, align: 'left' },
                { label: 'Tên người tạo', name: 'crter_name', width: 100, align: 'left' },
                { label: 'Thời gian tạo', name: 'crte_time', width: 100, align: 'left' },
                { label: 'Cơ sở tạo', name: 'crte_optins_no', width: 100, align: 'left' },
                { label: 'Người thực hiện', name: 'opter_id', width: 100, align: 'left' },
                { label: 'Tên người thực hiện', name: 'opter_name', width: 100, align: 'left' },
                { label: 'Thời gian thực hiện', name: 'opt_time', width: 100, align: 'left' },
                { label: 'Cơ sở thực hiện', name: 'optins_no', width: 100, align: 'left' },
                { label: 'Tên bảng', name: 'tabname', width: 100, align: 'left' },
                { label: 'Khu vực điều phối chung', name: 'poolarea_no', width: 100, align: 'left' }
            ];
            break;
        case "1319":
            colModel = [
                { label: 'Mã danh mục BHYT', name: 'hilist_code', width: 100, align: 'left' },
                { label: 'Loại người tỷ lệ tự trả danh mục BHYT', name: 'selfpay_prop_psn_type', width: 100, align: 'left' },
                { label: 'Loại tỷ lệ tự trả danh mục', name: 'selfpay_prop_type', width: 100, align: 'left' },
                { label: 'Vùng BHYT của cơ sở tham gia bảo hiểm', name: 'insu_admdvs', width: 100, align: 'left' },
                { label: 'Ngày bắt đầu', name: 'begndate', width: 100, align: 'left' },
                { label: 'Ngày kết thúc', name: 'enddate', width: 100, align: 'left' },
                { label: 'Tỷ lệ tự trả', name: 'selfpay_prop', width: 60, align: 'left' },
                { label: 'Cờ hiệu lực', name: 'vali_flag', width: 60, align: 'left' },
                { label: 'Số ghi duy nhất', name: 'rid', width: 60, align: 'left' },
                { label: 'Thời gian cập nhật', name: 'updt_time', width: 100, align: 'left' },
                { label: 'Người tạo', name: 'crter_id', width: 100, align: 'left' },
                { label: 'Tên người tạo', name: 'crter_name', width: 100, align: 'left' },
                { label: 'Thời gian tạo', name: 'crte_time', width: 100, align: 'left' },
                { label: 'Cơ sở tạo', name: 'crte_optins_no', width: 100, align: 'left' },
                { label: 'Người thực hiện', name: 'opter_id', width: 100, align: 'left' },
                { label: 'Tên người thực hiện', name: 'opter_name', width: 100, align: 'left' },
                { label: 'Thời gian thực hiện', name: 'opt_time', width: 100, align: 'left' },
                { label: 'Cơ sở thực hiện', name: 'optins_no', width: 100, align: 'left' },
                { label: 'Tên bảng', name: 'tabname', width: 100, align: 'left' },
                { label: 'Khu vực điều phối chung', name: 'poolarea_no', width: 100, align: 'left' }
            ];
            break;
        case "1901":
            colModel = [
                { label: 'Loại từ điển', name: 'type', width: 150, align: 'left' },
                { label: 'Nhãn từ điển', name: 'label', width: 100, align: 'left' },
                { label: 'Giá trị khóa từ điển', name: 'value', width: 100, align: 'left' },
                { label: 'Giá trị khóa từ điển cha', name: 'parent_value', width: 100, align: 'left' },
                { label: 'STT', name: 'sort', width: 60, align: 'left' },
                { label: 'Dấu hiệu quyền', name: 'vali_flag', width: 60, align: 'left' },
                { label: 'Tạo tài khoản', name: 'create_user', width: 60, align: 'left' },
                { label: 'Thời gian tạo', name: 'create_date', width: 100, align: 'left' },
                { label: 'Số phiên bản', name: 'version', width: 60, align: 'left' }
            ];
            break;

    }
    return colModel;
}