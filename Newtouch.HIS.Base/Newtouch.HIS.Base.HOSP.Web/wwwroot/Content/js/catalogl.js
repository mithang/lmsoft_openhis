function mlbh(mlbh)
{
    var colModel;
    switch (mlbh) {
        case "1301":
            colModel = [
                { label: 'Mã danh mục y tế', name: 'MED_LIST_CODG', width: 130, align: 'left' },
                { label: 'Tên', name: 'REG_NAME', width: 100, align: 'left' },
                { label: 'Mã', name: 'DRUGSTDCODE', width: 100, align: 'left' },
                { label: 'Dạng bào chế thuốc', name: 'DRUG_DOSFORM', width: 60, align: 'left' },
                { label: 'Tên dạng bào chế', name: 'DRUG_DOSFORM_NAME', width: 100, align: 'left',hidden:true },
                { label: 'Loại thuốc', name: 'DRUG_TYPE', width: 50, align: 'left' },
                { label: 'Quy cách thuốc', name: 'DRUG_SPEC', width: 50, align: 'left' }, 
                { label: 'Dạng bào chế đăng ký', name: 'REG_DOSFORM', width: 60, align: 'left' },
                { label: 'Quy cách đăng ký', name: 'REG_SPEC', width: 60, align: 'left' },
                { label: 'Liều dùng mỗi lần', name: 'EACH_DOS', width: 100, align: 'left' },
                { label: 'Cờ thuốc không kê đơn', name: 'OTC_FLAG', width: 20, align: 'left', hidden: true },
                { label: 'Chất liệu bao bì', name: 'PACMATL', width: 50, align: 'left' },
                { label: 'Chức năng và chỉ định', name: 'EFCC_ATD', width: 100, align: 'left' },
                { label: 'Số lượng đóng gói nhỏ nhất', name: 'MIN_PAC_CNT', width: 40, align: 'left' },
                { label: 'Đơn vị đóng gói nhỏ nhất', name: 'MIN_PACUNT', width: 40, align: 'left' },
                { label: 'Đơn vị bào chế nhỏ nhất', name: 'MIN_PREPUNT', width: 40, align: 'left' },
                { label: 'Hạn dùng của thuốc', name: 'DRUG_EXPY', width: 60, align: 'left' },
                { label: 'Đơn vị tính giá nhỏ nhất', name: 'MIN_PRCUNT', width: 40, align: 'left' },
                { label: 'Số hiệu doanh nghiệp sản xuất', name: 'PRODENTP_CODE', width: 80, align: 'left' },
                { label: 'Tên doanh nghiệp sản xuất', name: 'PRODENTP_NAME', width: 120, align: 'left' },
                { label: 'Trạng thái thị trường', name: 'MKT_STAS', width: 50, align: 'left' },
                { label: 'Cờ hiệu lực', name: 'VALI_FLAG', width: 20, align: 'left' },
                { label: 'Số phiên bản', name: 'VER', width: 60, align: 'left' },
                { label: 'Tên phiên bản', name: 'VER_NAME', width: 120, align: 'left' },

            ];
            break;
        case "1302":
            colModel = [
                { label: 'Mã danh mục y tế', name: 'MED_LIST_CODG', width: 130, align: 'left' },
                { label: 'Tên thuốc đơn vị', name: 'TCMHERB_NAME', width: 100, align: 'left' },
                { label: 'Bộ phận dùng làm thuốc', name: 'MED_PART', width: 100, align: 'left' },
                { label: 'Cách dùng thông thường', name: 'CNVL_USED', width: 60, align: 'left' },
                { label: 'Tính vị', name: 'NATFLA', width: 100, align: 'left' },
                { label: 'Chủng loại', name: 'CAT', width: 60, align: 'left' },
                { label: 'Ngày bắt đầu', name: 'BEGNDATE', width: 100, align: 'left' },
                { label: 'Ngày kết thúc', name: 'ENDDATE', width: 80, align: 'left' },
                { label: 'Cờ hiệu lực', name: 'VALI_FLAG', width: 40, align: 'left' },
                { label: 'Số phiên bản', name: 'VER', width: 60, align: 'left' },
                { label: 'Tên phiên bản', name: 'VER_NAME', width: 120, align: 'left' },
                { label: 'Tên dược liệu', name: 'MLMS_NAME', width: 60, align: 'left' },
                { label: 'Chức năng và chỉ định', name: 'EFCC_ATD', width: 120, align: 'left' },
                { label: 'Phương pháp bào chế', name: 'PSDG_MTD', width: 120, align: 'left' },
                { label: 'Phân loại công hiệu', name: 'ECY_TYPE', width: 80, align: 'left' },
                { label: 'Nguồn gốc chủng loại dược liệu', name: 'MLMS_CAT_SOUC', width: 80, align: 'left' },
            ];
            break;
        case "1303":
            colModel = [
                 { label: 'Mã danh mục y tế', name: 'MED_LIST_CODG', width: 130, align: 'left' },
                { label: 'Tên thương mại của thuốc', name: 'DRUG_PRODNAME', width: 100, align: 'left' },
                { label: 'Dạng bào chế', name: 'DOSFORM', width: 80, align: 'left' },
                { label: 'Chức năng và chỉ định', name: 'EFCC_ATD', width: 120, align: 'left' },
                { label: 'Quy cách thuốc', name: 'DRUG_SPEC', width: 100, align: 'left' },
                { label: 'Liều dùng mỗi lần', name: 'EACH_DOS', width: 100, align: 'left' },
                { label: 'Loại thuốc', name: 'DRUG_TYPE', width: 80, align: 'left' },
                { label: 'Chất liệu bao bì', name: 'PACMATL', width: 100, align: 'left' },
                { label: 'Số lượng đóng gói nhỏ nhất', name: 'MIN_PAC_CNT', width: 50, align: 'left' },
                { label: 'Đơn vị đóng gói nhỏ nhất', name: 'MIN_PACUNT', width: 50, align: 'left' },
                { label: 'Đơn vị bào chế nhỏ nhất', name: 'MIN_PREPUNT', width: 50, align: 'left' },
                { label: 'Tên doanh nghiệp sản xuất', name: 'EFCC_ATD', width: 120, align: 'left' },
                { label: 'Địa chỉ doanh nghiệp sản xuất', name: 'PSDG_MTD', width: 120, align: 'left' },
                { label: 'Cờ hiệu lực', name: 'VALI_FLAG', width: 40, align: 'left' },
                { label: 'Thời gian bắt đầu', name: 'BEGNTIME', width: 80, align: 'left' },
                { label: 'Thời gian kết thúc', name: 'ENDTIME', width: 80, align: 'left' },
                { label: 'Số phiên bản', name: 'VER', width: 60, align: 'left' },
                { label: 'Tên phiên bản', name: 'VER_NAME', width: 120, align: 'left' },
            ];
            break;
        case "1305":
            colModel = [
                 { label: 'Mã danh mục y tế', name: 'MED_LIST_CODG', width: 130, align: 'left' },
                { label: 'Đơn vị tính giá', name: 'PRCUNT', width: 60, align: 'left' },
                { label: 'Tên đơn vị tính giá', name: 'PRCUNT_NAME', width: 100, align: 'left' },
                { label: 'Giải thích hạng mục chẩn trị', name: 'TRT_ITEM_DSCR', width: 120, align: 'left' },
                { label: 'Nội dung loại trừ chẩn trị', name: 'TRT_EXCT_CONT', width: 120, align: 'left' },
                { label: 'Cờ hiệu lực', name: 'VALI_FLAG', width: 60, align: 'left' },
                { label: 'Loại hạng mục dịch vụ', name: 'SERVITEM_TYPE', width: 80, align: 'left' },
                { label: 'Tên hạng mục dịch vụ y tế', name: 'SERVITEM_NAME', width: 100, align: 'left' },
                { label: 'Ngày bắt đầu', name: 'BEGNDATE', width: 80, align: 'left' },
                { label: 'Ngày kết thúc', name: 'ENDDATE', width: 80, align: 'left' },
                { label: 'Số phiên bản', name: 'VER', width: 60, align: 'left' },
                { label: 'Tên phiên bản', name: 'VER_NAME', width: 120, align: 'left' },
            ];
            break;
        case "1306":
            colModel = [
                 { label: 'Mã danh mục y tế', name: 'MED_LIST_CODG', width: 130, align: 'left' },
                { label: 'Tên chung BHYT', name: 'HI_GENNAME', width: 100, align: 'left' },
                { label: 'Quy cách', name: 'SPEC', width: 60, align: 'left' },
                { label: 'Chất liệu vật tư tiêu hao', name: 'MCS_MATL', width: 100, align: 'left' },
                { label: 'Ngày bắt đầu', name: 'BEGNDATE', width: 100, align: 'left' },
                { label: 'Ngày kết thúc', name: 'ENDDATE', width: 100, align: 'left' },
                { label: 'Số hiệu doanh nghiệp sản xuất', name: 'PRODENTP_CODE', width: 100, align: 'left' },
                { label: 'Tên doanh nghiệp sản xuất', name: 'PRODENTP_NAME', width: 120, align: 'left' },
                { label: 'Cờ hiệu lực', name: 'VALI_FLAG', width: 40, align: 'left' },
                { label: 'Số phiên bản', name: 'VER', width: 60, align: 'left' },
                { label: 'Tên phiên bản', name: 'VER_NAME', width: 120, align: 'left' },
            ];
            break;
        case "1307":
            colModel = [
                { label: 'Chẩn đoán bệnh tây yID', name: 'DIAG_LIST_ID', width: 100, align: 'left', hidden:true },
                 { label: 'Chương', name: 'CPR', width: 30, align: 'left' },
                { label: 'Phạm vi mã chương', name: 'CPR_CODE_SCP', width: 80, align: 'left' },
                { label: 'Tên chương', name: 'CPR_NAME', width: 100, align: 'left' },
                { label: 'Phạm vi mã tiết', name: 'SEC_CODE_SCP', width: 80, align: 'left' },
                { label: 'Tên tiết', name: 'SEC_NAME', width: 100, align: 'left' },
                { label: 'Mã loại mục', name: 'CGY_CODE', width: 60, align: 'left' },
                { label: 'Tên loại mục', name: 'CGY_NAME', width: 100, align: 'left' },
                { label: 'Mã tiểu mục', name: 'SOR_CODE', width: 60, align: 'left' },
                { label: 'Tên tiểu mục', name: 'SOR_NAME', width: 100, align: 'left' },
                { label: 'Mã chẩn đoán', name: 'DIAG_CODE', width: 80, align: 'left' },
                { label: 'Tên chẩn đoán', name: 'DIAG_NAME', width: 100, align: 'left' },
                { label: 'Dấu sử dụng', name: 'USED_STD', width: 40, align: 'left' },
                { label: 'Mã chẩn đoán bản tiêu chuẩn quốc gia', name: 'NATSTD_DIAG_CODE', width: 80, align: 'left' },
                { label: 'Tên chẩn đoán bản tiêu chuẩn quốc gia', name: 'NATSTD_DIAG_NAME', width: 100, align: 'left' },
                { label: 'Mã chẩn đoán bản lâm sàng', name: 'CLNC_DIAG_CODE', width: 80, align: 'left' },
                { label: 'Tên chẩn đoán bản lâm sàng', name: 'CLNC_DIAG_NAME', width: 100, align: 'left' },
                { label: 'Số phiên bản', name: 'VER', width: 60, align: 'left' },
                { label: 'Tên phiên bản', name: 'VER_NAME', width: 120, align: 'left' },
            ];
            break;
        case "1308":
            colModel = [
                { label: 'Danh mục chuẩn phẫu thuậtID', name: 'OPRN_STD_LIST_ID', width: 130, align: 'left', hidden: true },
                 { label: 'Chương', name: 'CPR', width: 30, align: 'left' },
                { label: 'Phạm vi mã chương', name: 'CPR_CODE_SCP', width: 60, align: 'left' },
                { label: 'Tên chương', name: 'CPR_NAME', width: 100, align: 'left' },
                { label: 'Mã loại mục', name: 'CGY_CODE', width: 60, align: 'left' },
                { label: 'Tên loại mục', name: 'CGY_NAME', width: 100, align: 'left' },
                { label: 'Mã tiểu mục', name: 'SOR_CODE', width: 60, align: 'left' },
                { label: 'Tên tiểu mục', name: 'SOR_NAME', width: 100, align: 'left' },
                { label: 'Mã tiểu mục chi tiết', name: 'DTLS_CODE', width: 60, align: 'left' },
                { label: 'Tên tiểu mục chi tiết', name: 'DTLS_NAME', width: 100, align: 'left' },
                { label: 'Mã thao tác phẫu thuật', name: 'OPRN_OPRT_CODE', width: 60, align: 'left' },
                { label: 'Tên thao tác phẫu thuật', name: 'OPRN_OPRT_NAME', width: 100, align: 'left' },
                { label: 'Cờ hiệu lực', name: 'VALI_FLAG', width: 40, align: 'left' },
                { label: 'Số phiên bản', name: 'VER', width: 60, align: 'left' },
                { label: 'Tên phiên bản', name: 'VER_NAME', width: 120, align: 'left' },
            ];
            break;
        case "1309":
            colModel = [
                { label: 'Mã danh mục bệnh mạn tính/đặc biệt ngoại trú', name: 'OPSP_DISE_CODE', width: 120, align: 'left' },
                { label: 'Tên danh mục lớn bệnh mạn tính/đặc biệt ngoại trú', name: 'OPSP_DISE_MAJCLS_NAME', width: 120, align: 'left' },
                { label: 'Tên phân loại chi tiết bệnh mạn tính/đặc biệt ngoại trú', name: 'OPSP_DISE_SUBD_CLSS_NAME', width: 120, align: 'left' },
                { label: 'Khu vực BHYT', name: 'ADMDVS', width: 100, align: 'left' },
                { label: 'Cờ hiệu lực', name: 'VALI_FLAG', width: 60, align: 'left' },
                { label: 'Tên bệnh mạn tính/đặc biệt ngoại trú', name: 'OPSP_DISE_NAME', width: 120, align: 'left' },
                { label: 'Mã danh mục lớn bệnh mạn tính/đặc biệt ngoại trú', name: 'OPSP_DISE_MAJCLS_CODE', width: 80, align: 'left' },
                { label: 'Số phiên bản', name: 'VER', width: 60, align: 'left' },
                { label: 'Tên phiên bản', name: 'VER_NAME', width: 120, align: 'left' },
            ];
            break;
        case "1310":
            colModel = [
                { label: 'Danh mục quyết toán bệnhID', name: 'DISE_SETL_LIST_ID', width: 100, align: 'left', hidden: true },
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
                 { label: 'Mã danh mục bệnh phẫu thuật trong ngày', name: 'DAYSRG_DISE_LIST_CODE', width: 120, align: 'left' },
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
                { label: 'Hình thái học khối uID', name: 'TMOR_MPY_ID', width: 100, align: 'left', hidden: true },
                 { label: 'U bướu/Mã loại tế bào', name: 'TMOR_CELL_TYPE_CODE', width: 120, align: 'left' },
                { label: 'U bướu/Loại tế bào', name: 'TMOR_CELL_TYPE', width: 120, align: 'left' },
                { label: 'Mã phân loại hình thái học', name: 'MPY_TYPE_CODE', width: 100, align: 'left' },
                { label: 'Phân loại hình thái học', name: 'MPY_TYPE', width: 100, align: 'left' },
                { label: 'Cờ hiệu lực', name: 'VALI_FLAG', width: 80, align: 'left' },
                { label: 'Số phiên bản', name: 'VER', width: 60, align: 'left' },
                { label: 'Tên phiên bản', name: 'VER_NAME', width: 120, align: 'left' },
            ];
            break;
        case "1314":
            colModel = [
                { label: 'Chẩn đoán bệnh y học cổ truyềnID', name: 'TCM_DIAG_ID', width: 100, align: 'left', hidden: true },
                 { label: 'Chẩn đoán bệnh y học cổ truyền', name: 'CATY_CGY_CODE', width: 100, align: 'left' },
                { label: 'Tên loại mục chuyên khoa', name: 'CATY_CGY_NAME', width: 120, align: 'left' },
                { label: 'Mã loại mục phân loại hệ thống chuyên khoa', name: 'SPCY_SYS_TAXA_CODE', width: 120, align: 'left' },
                { label: 'Tên loại mục phân loại hệ thống chuyên khoa', name: 'SPCY_SYS_TAXA_NAME', width: 120, align: 'left' },
                { label: 'Mã phân loại bệnh', name: 'DISE_TYPE_CODE', width: 100, align: 'left' },
                { label: 'Tên phân loại bệnh', name: 'DISE_TYPE_NAME', width: 120, align: 'left' },
                { label: 'Cờ hiệu lực', name: 'VALI_FLAG', width: 60, align: 'left' },
                { label: 'Số phiên bản', name: 'VER', width: 60, align: 'left' },
                { label: 'Tên phiên bản', name: 'VER_NAME', width: 100, align: 'left' },
            ];
            break;
        case "1315":
            colModel = [
                { label: 'Chứng hậu Đông yID', name: 'TCMSYMP_ID', width: 100, align: 'left', hidden: true },
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
            //Danh mục loại truy vấn
        case "1304":
            colModel = [
                { label: 'Mã danh mục y tế', name: 'med_list_codg', width: 100, align: 'left', },
                { label: 'Tên thương mại của thuốc', name: 'drug_prodname', width: 100, align: 'left' },
                { label: 'Số hiệu tên chung', name: 'genname_codg', width: 120, align: 'left' },
                { label: 'Tên chung của thuốc', name: 'drug_genname', width: 120, align: 'left' },
                { label: 'Chủng loại thuốc dân tộc', name: 'ethdrug_type', width: 100, align: 'left' },
                { label: 'Tên hóa học', name: 'chemname', width: 100, align: 'left' },
                { label: 'Bí danh', name: 'alis', width: 100, align: 'left' },
                { label: 'Tên tiếng Anh', name: 'eng_name', width: 100, align: 'left' },
                { label: 'Dạng bào chế', name: 'dosform', width: 120, align: 'left' },
                { label: 'Liều dùng mỗi lần', name: 'each_dos', width: 60, align: 'left' },
                { label: 'Tần suất sử dụng', name: 'used_frqu', width: 60, align: 'left' },
                { label: 'Số hiệu thuốc quốc gia', name: 'nat_drug_no', width: 100, align: 'left' },
                { label: 'Cách dùng', name: 'used_mtd', width: 100, align: 'left', },
                { label: 'Thành phần', name: 'ing', width: 80, align: 'left' },
                { label: 'Tính chất', name: 'chrt', width: 100, align: 'left' },
                { label: 'Phản ứng không mong muốn', name: 'defs', width: 120, align: 'left' },
                { label: 'Chống chỉ định', name: 'tabo', width: 100, align: 'left' },
                { label: 'Lưu ý', name: 'mnan', width: 100, align: 'left' },
                { label: 'Bảo quản', name: 'stog', width: 60, align: 'left' },
                { label: 'Quy cách thuốc', name: 'drug_spec', width: 60, align: 'left' },
                { label: 'Loại đơn vị tính giá', name: 'prcunt_type', width: 60, align: 'left' },
                { label: 'Cờ thuốc không kê đơn', name: 'otc_flag', width: 60, align: 'left' },
                { label: 'Chất liệu bao bì', name: 'pacmatl', width: 60, align: 'left' },
                { label: 'Quy cách đóng gói', name: 'pacspec', width: 60, align: 'left' },
                 { label: 'Đơn vị sử dụng nhỏ nhất', name: 'min_useunt', width: 60, align: 'left' },
                { label: 'Đơn vị bán nhỏ nhất', name: 'min_salunt', width: 60, align: 'left' },
                { label: 'Hướng dẫn sử dụng', name: 'manl', width: 60, align: 'left' },
                { label: 'Đường dùng thuốc', name: 'rute', width: 60, align: 'left' },
                { label: 'Ngày bắt đầu', name: 'begndate', width: 60, align: 'left' },
                { label: 'Ngày kết thúc', name: 'enddate', width: 60, align: 'left' },
                { label: 'Phân loại dược lý', name: 'pham_type', width: 60, align: 'left' },
                { label: 'Ghi chú', name: 'memo', width: 60, align: 'left' },
                { label: 'Số lượng đóng gói', name: 'pac_cnt', width: 60, align: 'left' },
                 { label: 'Đơn vị đo nhỏ nhất', name: 'min_unt', width: 60, align: 'left' },
                { label: 'Số lượng đóng gói nhỏ nhất', name: 'min_pac_cnt', width: 60, align: 'left' },
                 { label: 'Đơn vị đóng gói nhỏ nhất', name: 'min_pacunt', width: 60, align: 'left' },
                { label: 'Đơn vị bào chế nhỏ nhất', name: 'min_prepunt', width: 60, align: 'left' },
                { label: 'Hạn dùng của thuốc', name: 'drug_expy', width: 60, align: 'left' },
                { label: 'Chức năng và chỉ định', name: 'efcc_atd', width: 60, align: 'left' },
                { label: 'Đơn vị tính giá nhỏ nhất', name: 'min_prcunt', width: 60, align: 'left' },
                { label: 'Mã gợi nhớ Wubi', name: 'wubi', width: 60, align: 'left' },
                { label: 'Mã gợi nhớ pinyin', name: 'pinyin', width: 60, align: 'left' },
                { label: 'Cờ hiệu lực', name: 'vali_flag', width: 60, align: 'left' },
                { label: 'Số bản ghi duy nhất', name: 'rid', width: 60, align: 'left' },
                { label: 'Thời gian tạo dữ liệu', name: 'crte_time', width: 60, align: 'left' },
                { label: 'Thời gian cập nhật dữ liệu', name: 'updt_time', width: 60, align: 'left' },
                { label: 'Người tạo', name: 'crter_id', width: 60, align: 'left' },
                { label: 'Tên người tạo', name: 'crter_name', width: 60, align: 'left' },
                { label: 'Tạo tổ chức xử lý', name: 'crte_optins_no', width: 60, align: 'left' },
                { label: 'Người xử lý', name: 'opter_id', width: 60, align: 'left' },
                { label: 'Tên người xử lý', name: 'opter_name', width: 60, align: 'left' },
                { label: 'Thời gian xử lý', name: 'opt_time', width: 60, align: 'left' },
                { label: 'Tổ chức xử lý', name: 'optins_no', width: 60, align: 'left' },
                //{ label: 'Chức năng và chỉ định', name: 'efcc_atd', width: 100, align: 'left' },
                //{ label: 'Cờ hiệu lực', name: 'vali_flag', width: 40, align: 'left' },
                { label: 'Số phiên bản', name: 'ver', width: 60, align: 'left' }
            ];
            break;
        case "1312":
            colModel = [
                { label: 'Mã danh mục BHYT', name: 'hilist_code', width: 100, align: 'left' },
                { label: 'Tên danh mục BHYT', name: 'hilist_name', width: 100, align: 'left' },
                { label: 'Khu vực BHYT của tổ chức tham gia bảo hiểm', name: 'insu_admdvs', width: 100, align: 'left' },
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
                { label: 'Mã gợi nhớ Wubi', name: 'wubi', width: 100, align: 'left' },
                { label: 'Mã gợi nhớ pinyin', name: 'pinyin', width: 100, align: 'left' },
                { label: 'Ghi chú', name: 'memo', width: 100, align: 'left' },
                { label: 'Cờ hiệu lực', name: 'vali_flag', width: 100, align: 'left' },
                { label: 'Số bản ghi duy nhất', name: 'rid', width: 100, align: 'left' },
                { label: 'Thời gian cập nhật', name: 'updt_time', width: 100, align: 'left' },
                { label: 'Người tạo', name: 'crter_id', width: 100, align: 'left' },
                { label: 'Tên người tạo', name: 'crter_name', width: 100, align: 'left' },
                { label: 'Thời gian tạo', name: 'crte_time', width: 100, align: 'left' },
                { label: 'Tổ chức tạo', name: 'crte_optins_no', width: 100, align: 'left' },
                { label: 'Người xử lý', name: 'opter_id', width: 100, align: 'left' },
                { label: 'Tên người xử lý', name: 'opter_name', width: 100, align: 'left' },
                { label: 'Thời gian xử lý', name: 'opt_time', width: 100, align: 'left' },
                { label: 'Tổ chức xử lý', name: 'optins_no', width: 100, align: 'left' },
                { label: 'Khu vực quỹ chung', name: 'poolarea_no', width: 100, align: 'left' }
            ];
            break;
        case "1316":
            colModel = [
                { label: 'Mã danh mục y tế', name: 'med_list_codg', width: 100, align: 'left' },
                { label: 'Mã danh mục BHYT', name: 'hilist_code', width: 100, align: 'left' },
                { label: 'Loại danh mục', name: 'list_type', width: 100, align: 'left' },
                { label: 'Khu vực BHYT của tổ chức tham gia bảo hiểm', name: 'insu_admdvs', width: 100, align: 'left' },
                { label: 'Ngày bắt đầu', name: 'begndate', width: 100, align: 'left' },
                { label: 'Ngày kết thúc', name: 'enddate', width: 60, align: 'left' },
                { label: 'Ghi chú', name: 'memo', width: 60, align: 'left' },
                { label: 'Cờ hiệu lực', name: 'vali_flag', width: 60, align: 'left' },
                { label: 'Số bản ghi duy nhất', name: 'rid', width: 60, align: 'left' },
                { label: 'Thời gian cập nhật', name: 'updt_time', width: 100, align: 'left' },
                { label: 'Người tạo', name: 'crter_id', width: 100, align: 'left' },
                { label: 'Tên người tạo', name: 'crter_name', width: 100, align: 'left' },
                { label: 'Thời gian tạo', name: 'crte_time', width: 100, align: 'left' },
                { label: 'Tổ chức tạo', name: 'crte_optins_no', width: 100, align: 'left' },
                { label: 'Người xử lý', name: 'opter_id', width: 100, align: 'left' },
                { label: 'Tên người xử lý', name: 'opter_name', width: 100, align: 'left' },
                { label: 'Thời gian xử lý', name: 'opt_time', width: 100, align: 'left' },
                { label: 'Tổ chức xử lý', name: 'optins_no', width: 100, align: 'left' },
                { label: 'Khu vực quỹ chung', name: 'poolarea_no', width: 100, align: 'left' }
            ];
            break;
        case "1317":
            colModel = [
                { label: 'Số hiệu tổ chức y dược chỉ định', name: 'fixmedins_code', width: 100, align: 'left' },
                { label: 'Số hiệu danh mục tổ chức y dược chỉ định', name: 'medins_list_codg', width: 100, align: 'left' },
                { label: 'Tên danh mục tổ chức y dược chỉ định', name: 'medins_list_name', width: 100, align: 'left' },
                { label: 'Khu vực BHYT của tổ chức tham gia bảo hiểm', name: 'insu_admdvs', width: 100, align: 'left' },
                { label: 'Loại danh mục', name: 'list_type', width: 100, align: 'left' },
                { label: 'Mã danh mục y tế', name: 'med_list_codg', width: 100, align: 'left' },
                { label: 'Ngày bắt đầu', name: 'begndate', width: 60, align: 'left' },
                { label: 'Ngày kết thúc', name: 'enddate', width: 60, align: 'left' },
                { label: 'Số đăng ký', name: 'aprvno', width: 60, align: 'left' },
                { label: 'Dạng bào chế', name: 'dosform', width: 100, align: 'left' },
                { label: 'Nội dung loại trừ', name: 'exct_cont', width: 100, align: 'left' },
                { label: 'Nội hàm hạng mục', name: 'item_cont', width: 100, align: 'left' },
                { label: 'Đơn vị tính giá', name: 'prcunt', width: 100, align: 'left' },
                { label: 'Quy cách', name: 'spec', width: 100, align: 'left' },
                { label: 'Quy cách đóng gói', name: 'pacspec', width: 100, align: 'left' },
                { label: 'Ghi chú', name: 'memo', width: 100, align: 'left' },
                { label: 'Cờ hiệu lực', name: 'vali_flag', width: 100, align: 'left' },
                { label: 'Số bản ghi duy nhất', name: 'rid', width: 100, align: 'left' },
                { label: 'Thời gian cập nhật', name: 'updt_time', width: 100, align: 'left' },
                { label: 'Người tạo', name: 'crter_id', width: 100, align: 'left' },
                { label: 'Tên người tạo', name: 'crter_name', width: 100, align: 'left' },
                { label: 'Thời gian tạo', name: 'crte_time', width: 100, align: 'left' },
                { label: 'Tổ chức tạo', name: 'crte_optins_no', width: 100, align: 'left' },
                { label: 'Người xử lý', name: 'opter_id', width: 100, align: 'left' },
                { label: 'Tên người xử lý', name: 'opter_name', width: 100, align: 'left' },
                { label: 'Thời gian xử lý', name: 'opt_time', width: 100, align: 'left' },
                { label: 'Tổ chức xử lý', name: 'optins_no', width: 100, align: 'left' },
                { label: 'Khu vực quỹ chung', name: 'poolarea_no', width: 100, align: 'left' }
            ];
            break;
        case "1318":
            colModel = [
                { label: 'Mã danh mục BHYT', name: 'hilist_code', width: 100, align: 'left' },
                { label: 'Loại giới hạn giá danh mục BHYT', name: 'hilist_lmtpric_type', width: 100, align: 'left' },
                { label: 'Cách xử lý vượt giới hạn danh mục BHYT', name: 'overlmt_dspo_way', width: 100, align: 'left' },
                { label: 'Khu vực BHYT của tổ chức tham gia bảo hiểm', name: 'insu_admdvs', width: 100, align: 'left' },
                { label: 'Ngày bắt đầu', name: 'begndate', width: 100, align: 'left' },
                { label: 'Ngày kết thúc', name: 'enddate', width: 100, align: 'left' },
                { label: 'Số tiền giới hạn định giá danh mục BHYT', name: 'hilist_pric_uplmt_amt', width: 60, align: 'left' },
                { label: 'Cờ hiệu lực', name: 'vali_flag', width: 60, align: 'left' },
                { label: 'Số bản ghi duy nhất', name: 'rid', width: 60, align: 'left' },
                { label: 'Thời gian cập nhật', name: 'updt_time', width: 100, align: 'left' },
                { label: 'Người tạo', name: 'crter_id', width: 100, align: 'left' },
                { label: 'Tên người tạo', name: 'crter_name', width: 100, align: 'left' },
                { label: 'Thời gian tạo', name: 'crte_time', width: 100, align: 'left' },
                { label: 'Tổ chức tạo', name: 'crte_optins_no', width: 100, align: 'left' },
                { label: 'Người xử lý', name: 'opter_id', width: 100, align: 'left' },
                { label: 'Tên người xử lý', name: 'opter_name', width: 100, align: 'left' },
                { label: 'Thời gian xử lý', name: 'opt_time', width: 100, align: 'left' },
                { label: 'Tổ chức xử lý', name: 'optins_no', width: 100, align: 'left' },
                { label: 'Tên bảng', name: 'tabname', width: 100, align: 'left' },
                { label: 'Khu vực quỹ chung', name: 'poolarea_no', width: 100, align: 'left' }
            ];
            break;
        case "1319":
            colModel = [
                { label: 'Mã danh mục BHYT', name: 'hilist_code', width: 100, align: 'left' },
                { label: 'Loại nhân viên theo tỷ lệ tự trả danh mục BHYT', name: 'selfpay_prop_psn_type', width: 100, align: 'left' },
                { label: 'Loại tỷ lệ tự trả theo danh mục', name: 'selfpay_prop_type', width: 100, align: 'left' },
                { label: 'Khu vực BHYT của tổ chức tham gia bảo hiểm', name: 'insu_admdvs', width: 100, align: 'left' },
                { label: 'Ngày bắt đầu', name: 'begndate', width: 100, align: 'left' },
                { label: 'Ngày kết thúc', name: 'enddate', width: 100, align: 'left' },
                { label: 'Tỷ lệ tự trả', name: 'selfpay_prop', width: 60, align: 'left' },
                { label: 'Cờ hiệu lực', name: 'vali_flag', width: 60, align: 'left' },
                { label: 'Số bản ghi duy nhất', name: 'rid', width: 60, align: 'left' },
                { label: 'Thời gian cập nhật', name: 'updt_time', width: 100, align: 'left' },
                { label: 'Người tạo', name: 'crter_id', width: 100, align: 'left' },
                { label: 'Tên người tạo', name: 'crter_name', width: 100, align: 'left' },
                { label: 'Thời gian tạo', name: 'crte_time', width: 100, align: 'left' },
                { label: 'Tổ chức tạo', name: 'crte_optins_no', width: 100, align: 'left' },
                { label: 'Người xử lý', name: 'opter_id', width: 100, align: 'left' },
                { label: 'Tên người xử lý', name: 'opter_name', width: 100, align: 'left' },
                { label: 'Thời gian xử lý', name: 'opt_time', width: 100, align: 'left' },
                { label: 'Tổ chức xử lý', name: 'optins_no', width: 100, align: 'left' },
                { label: 'Tên bảng', name: 'tabname', width: 100, align: 'left' },
                { label: 'Khu vực quỹ chung', name: 'poolarea_no', width: 100, align: 'left' }
            ];
            break;
        case "1901":
            colModel = [
                { label: 'Mã danh mục BHYT', name: 'hilist_code', width: 100, align: 'left' },
                { label: 'Loại nhân viên theo tỷ lệ tự trả danh mục BHYT', name: 'selfpay_prop_psn_type', width: 100, align: 'left' },
                { label: 'Loại tỷ lệ tự trả theo danh mục', name: 'selfpay_prop_type', width: 100, align: 'left' },
                { label: 'Khu vực BHYT của tổ chức tham gia bảo hiểm', name: 'insu_admdvs', width: 100, align: 'left' },
                { label: 'Ngày bắt đầu', name: 'begndate', width: 100, align: 'left' },
                { label: 'Ngày kết thúc', name: 'enddate', width: 100, align: 'left' },
                { label: 'Tỷ lệ tự trả', name: 'selfpay_prop', width: 60, align: 'left' },
                { label: 'Cờ hiệu lực', name: 'vali_flag', width: 60, align: 'left' },
                { label: 'Số bản ghi duy nhất', name: 'rid', width: 60, align: 'left' },
                { label: 'Thời gian cập nhật', name: 'updt_time', width: 100, align: 'left' },
                { label: 'Người tạo', name: 'crter_id', width: 100, align: 'left' },
                { label: 'Tên người tạo', name: 'crter_name', width: 100, align: 'left' },
                { label: 'Thời gian tạo', name: 'crte_time', width: 100, align: 'left' },
                { label: 'Tổ chức tạo', name: 'crte_optins_no', width: 100, align: 'left' },
                { label: 'Người xử lý', name: 'opter_id', width: 100, align: 'left' },
                { label: 'Tên người xử lý', name: 'opter_name', width: 100, align: 'left' },
                { label: 'Thời gian xử lý', name: 'opt_time', width: 100, align: 'left' },
                { label: 'Tổ chức xử lý', name: 'optins_no', width: 100, align: 'left' },
                { label: 'Tên bảng', name: 'tabname', width: 100, align: 'left' },
                { label: 'Khu vực quỹ chung', name: 'poolarea_no', width: 100, align: 'left' }
            ];
            break;

    }
    return colModel;
}