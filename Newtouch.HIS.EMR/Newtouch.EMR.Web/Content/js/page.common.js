//Từ điển
$.fn.dicCommonFloatingSelector = function (options) {
    $(this).newtouchFloatingSelector({
        url: '/SystemManage/Common/GetCommonList',
        width: 300,
        height: 200,
        clickautotrigger: true,
        ajaxparameters: function ($thisinput) {
            return "keyword=" + $thisinput.val() + "&type=" + options.rule;
        },
        colModel: [
            { label: 'Mã', name: 'Code', widthratio: 30 },
            { label: 'Tên', name: 'Name', widthratio: 70 },
        ],
        itemdbclickhandler: function ($thistr, $thisinput) {
            //Khi lưu, kiểm tra val và data-Name nhất quán
            $thisinput.attr('data-code', $thistr.attr('data-code'));
            $thisinput.attr('data-name', $thistr.attr('data-name'));
            $thisinput.val($thistr.attr('data-name'));
        }
    });
}
//Khoa
$.fn.departmentFloatingSelector = function (options) {
    $(this).newtouchFloatingSelector({
        url: '/SystemManage/Common/GetDepartment',
        width: 300,
        height: 200,
        clickautotrigger: true,
        caption: "Chọn khoa",
        ajaxparameters: function ($thisinput) {
            return "keyword=" + $thisinput.val();
        },
        colModel: [
            { label: 'Mã khoa', name: 'Code', widthratio: 40 },
            { label: 'Tên khoa', name: 'Name', widthratio: 60 },
        ],
        itemdbclickhandler: function ($thistr, $thisinput) {
            //Khi lưu, kiểm tra val và data-Name nhất quán
            $thisinput.attr('data-code', $thistr.attr('data-code'));
            $thisinput.attr('data-name', $thistr.attr('data-name'));
            $thisinput.val($thistr.attr('data-name'));
        }
    });
}
//Chẩn đoán
$.fn.zxyzdFloatingSelector = function (options) {
    var defaults = {
        url: '/SystemManage/Common/GetDiagnosisList',
        width: 600,
        height: 300,
        clickautotrigger: true,
        caption: "Chọn chẩn đoán",
        ajaxparameters: null,   //Vui lòng chỉ định
        itemdbclickhandler: null,
        colModel: [
           { label: 'Mã', name: 'zdCode', hidden: true },
           { label: 'Tên', name: 'zdmc', widthratio: 60 },
           { label: 'Pinyin', name: 'py', widthratio: 20 },
           { label: 'icd10', name: 'icd10', widthratio: 20 },
           { label: 'Mã bổ sung icd10', name: 'icd10fjm', hidden: true }
        ]
    };
    var options = $.extend(defaults, options);

    $(this).newtouchFloatingSelector(options);
}

//Chẩn đoán
$.fn.zdFloatingSelector = function (options) {
    var defaults = {
        url: '/SystemManage/Common/GetDiagnosisList',
        width: 600,
        height: 300,
        clickautotrigger: true,
        caption: "Chọn chẩn đoán",
        ajaxparameters: function ($thisinput) {
            if (!!!options.ybnhlx) {
                return "keyword=" + $thisinput.val() + "&zdlx=" + options.zdlx;
            } else {
                return "keyword=" + $thisinput.val() + "&zdlx=" + options.zdlx + "&ybnhlx=" + options.ybnhlx;
            }

        },
        itemdbclickhandler: null,
        colModel: [
            { label: 'Mã', name: 'zdCode', hidden: true },
            { label: 'Tên', name: 'zdmc', widthratio: 60 },
            { label: 'Pinyin', name: 'py', widthratio: 20 },
            { label: 'icd10', name: 'icd10', widthratio: 20 },
            { label: 'Mã bổ sung icd10', name: 'icd10fjm', hidden: true }
        ]
    };
    var options = $.extend(defaults, options);

    $(this).newtouchFloatingSelector(options);
}

//Phẫu thuật
$.fn.opFloatingSelector = function (options) {
    var defaults = {
        url: '/SystemManage/Common/GetOperationList',
        width: 450,
        height: 300,
        clickautotrigger: true,
        caption: "Chọn phẫu thuật",
        ajaxparameters: function ($thisinput) {
            return "keyword=" + $thisinput.val() + "&type=true";
        },
        itemdbclickhandler: null,
        colModel: [
            { label: 'Mã', name: 'ssdm', hidden: true },
            { label: 'Tên', name: 'ssmc', widthratio: 60 },
            { label: 'Mã gợi nhớ', name: 'zjm', widthratio: 20 },
            { label: 'Cấp phẫu thuật', name: 'ssjb', widthratio: 18 },
        ],
        rowNum: 100,
        rownumbers: true,
        rownumWidth: 40,
        gridview: true,
        pager: '#pscrolling',
        sortname: 'ssmc desc',
        viewrecords: false,
    };
    var options = $.extend(defaults, options);

    $(this).newtouchFloatingSelector(options);
}

//Bộ chọn nhân sự theo vị trí
$.fn.dutyStaffFloatingSelector = function (options) {
    $(this).newtouchFloatingSelector({
        url: '/SystemManage/Common/GetStaffListByDutyCode',
        width: 300,
        height: 200,
        clickautotrigger: true,
        ajaxparameters: function ($thisinput) {
            return "dutyCode=" + options.dutyCode + "&keyword=" + $thisinput.val();
        },
        colModel: [
            { label: 'StaffGh', name: 'StaffGh', hidden: true },
            { label: 'ks', name: 'ks', hidden: true },
            { label: 'Tên bác sĩ', name: 'StaffName', widthratio: 38 },
            { label: 'Mã gợi nhớ', name: 'StaffPY', widthratio: 20 },
            { label: 'Tên khoa', name: 'ksmc', widthratio: 40 },
        ],
        itemdbclickhandler: function ($thistr, $thisinput) {
            //Khi lưu, kiểm tra val và data-Name nhất quán
            $thisinput.attr('data-StaffGh', $thistr.attr('data-StaffGh'));
            $thisinput.attr('data-ks', $thistr.attr('data-ks'));
            $thisinput.attr('data-ksmc', $thistr.attr('data-ksmc'));
            $thisinput.val($thistr.attr('data-StaffName'));
        }
    });
}

//Bộ chọn quốc tịch
$.fn.NationalityFloatingSelector = function (options) {
    $(this).newtouchFloatingSelector({
        url: '/SystemManage/Common/GetNationality',
        width: 300,
        height: 200,
        clickautotrigger: true,
        ajaxparameters: function ($thisinput) {
            return "keyword=" + $thisinput.val();
        },
        colModel: [
            { label: 'Mã quốc gia', name: 'Code', widthratio: 30 },
            { label: 'Quốc gia', name: 'Name', widthratio: 70 },
        ],
        itemdbclickhandler: function ($thistr, $thisinput) {
            //Khi lưu, kiểm tra val và data-Name nhất quán
            $thisinput.attr('data-code', $thistr.attr('data-code'));
            $thisinput.attr('data-name', $thistr.attr('data-name'));
            $thisinput.val($thistr.attr('data-name'));
        }
    });
}
//Bộ chọn dân tộc
$.fn.NationsFloatingSelector = function (options) {
    $(this).newtouchFloatingSelector({
        url: '/SystemManage/Common/GetNationas',
        width: 300,
        height: 200,
        clickautotrigger: true,
        ajaxparameters: function ($thisinput) {
            return "keyword=" + $thisinput.val();
        },
        colModel: [
            { label: 'Mã dân tộc', name: 'Code', widthratio: 30 },
            { label: 'Dân tộc', name: 'Name', widthratio: 70 },
        ],
        itemdbclickhandler: function ($thistr, $thisinput) {
            //Khi lưu, kiểm tra val và data-Name nhất quán
            $thisinput.attr('data-code', $thistr.attr('data-code'));
            $thisinput.attr('data-name', $thistr.attr('data-name'));
            $thisinput.val($thistr.attr('data-name'));
        }
    });
}

//Bộ chọn nghề nghiệp
$.fn.ZYFloatingSelector = function (options) {
    $(this).newtouchFloatingSelector({
        url: '/SystemManage/Common/ZYgetlist',
        width: 300,
        height: 200,
        clickautotrigger: true,
        ajaxparameters: function ($thisinput) {
            return "keyword=" + $thisinput.val();
        },
        colModel: [
            { label: 'Mã', name: 'Code', widthratio: 30 },
            { label: 'Tên nghề nghiệp', name: 'Name', widthratio: 70 },
        ],
        itemdbclickhandler: function ($thistr, $thisinput) {
            //Khi lưu, kiểm tra val và data-Name nhất quán
            $thisinput.attr('data-code', $thistr.attr('data-code'));
            $thisinput.attr('data-name', $thistr.attr('data-name'));
            $thisinput.val($thistr.attr('data-name'));
        }
    });
}
//Bộ chọn quan hệ
$.fn.GXFloatingSelector = function (options) {
    $(this).newtouchFloatingSelector({
        url: '/SystemManage/Common/GXgetlist',
        width: 300,
        height: 200,
        clickautotrigger: true,
        ajaxparameters: function ($thisinput) {
            return "keyword=" + $thisinput.val();
        },
        colModel: [
            { label: 'Mã', name: 'Code', widthratio: 30 },
            { label: 'Tên quan hệ', name: 'Name', widthratio: 70 },
        ],
        itemdbclickhandler: function ($thistr, $thisinput) {
            //Khi lưu, kiểm tra val và data-Name nhất quán
            $thisinput.attr('data-code', $thistr.attr('data-code'));
            $thisinput.attr('data-name', $thistr.attr('data-name'));
            $thisinput.val($thistr.attr('data-name'));
        }
    });
}

//Thư mục con loại lớn của bệnh án
$.fn.BllxFloatingSelector = function (options) {
    $(this).newtouchFloatingSelector({
        url: '/SystemManage/Common/GetBllxListGrid',
        width: 300,
        height: 200,
        treeGrid: true,
        treeGridModel: "adjacency",
        ExpandColumn: "Code",
        clickautotrigger: true,
        ajaxparameters: function ($thisinput) {
            return "keyword=" + $thisinput.val() + "&bllx=" + options.code;
        },
        colModel: [
            { label: 'Danh mục bệnh án', name: 'Name', widthratio: 50 },
            { label: 'Mã loại', name: 'Code', widthratio: 50 },
            //{ label: "Mã rút gọn", name: "Id", width: 20, align: 'center', },
        ],
        itemdbclickhandler: function ($thistr, $thisinput) {
            //Khi lưu, kiểm tra val và data-Name nhất quán
            $thisinput.attr('data-code', $thistr.attr('data-code'));
            $thisinput.attr('data-name', $thistr.attr('data-name'));
            $thisinput.val($thistr.attr('data-name'));
        }
    });
}

//Chọn phần tử bệnh án
$.fn.ElementFloatingSelector = function (options) {
    var defaults = {
        url: '/MedicalRecordManage/Bljgh/GetYsList',
        width: 550,
        height: 200,
        clickautotrigger: true,
        caption: "Chọn chẩn đoán",
        ajaxparameters: function ($thisinput) {
            return "keyword=" + $thisinput.val();
        },
        itemdbclickhandler: null,
        colModel: [

             { label: 'ysId', name: 'ysId', widthratio: 70, hidden: true },
             { label: 'Mã phần tử', name: 'ysdm', widthratio: 15 },
             { label: 'Tên phần tử', name: 'ysmc', widthratio: 40 },
             { label: 'Loại phần tử', name: 'yslx', widthratio: 70, hidden: true },
             { label: 'Tên loại phần tử', name: 'yslxmc', widthratio: 20 },
             { label: 'Phần tử cha', name: 'ysdl', widthratio: 25 },
             { label: 'BindingPath', name: 'BindingPath', widthratio: 30, hidden: true }
        ],
    };
    var options = $.extend(defaults, options);

    $(this).newtouchFloatingSelector(options);
}
//Chọn trường bảng bệnh án
$.fn.TabColumnFloatingSelector = function (options) {
    var defaults = {
        url: '/MedicalRecordManage/Bljgh/GetTableColumnList',
        width: 350,
        height: 200,
        clickautotrigger: true,
        caption: "Chọn trường",
        ajaxparameters: function ($thisinput) {
            return "TableName=" + options.TableName + "&keyword=" + $thisinput.val();
        },
        itemdbclickhandler: null,
        colModel: [
             { label: 'Số thứ tự', name: 'column_id', widthratio: 20 },
             { label: 'Trường', name: 'columnName', widthratio: 50 },
             { label: 'Loại trường', name: 'columnType', widthratio: 20 },
             { label: 'Độ dài tối đa', name: 'max_length', widthratio: 30, hidden: true },
             { label: 'Số thập phân', name: 'scale', widthratio: 70, hidden: true }
        ],
    };
    var options = $.extend(defaults, options);

    $(this).newtouchFloatingSelector(options);
}
//Chọn chi tiết phần tử bệnh án
$.fn.ElementMxFloatingSelector = function (options) {
    var defaults = {
        url: '/MedicalRecordManage/Bljgh/GetElementMx',
        width: 350,
        height: 200,
        clickautotrigger: true,
        caption: "Chọn giá trị phần tử",
        ajaxparameters: function ($thisinput) {
            return "keyword=" + options.ysId;
        },
        itemdbclickhandler: null,
        colModel: [
             { label: 'ysId', name: 'ysId', widthratio: 20, hidden: true },
             { label: 'Mã', name: 'ysmxcode', widthratio: 40 },
             { label: 'Giá trị', name: 'ysmxName', widthratio: 60}
        ],
    };
    var options = $.extend(defaults, options);

    $(this).newtouchFloatingSelector(options);
}
//Lớp nổi văn bản bệnh án
$.fn.BindBlTableFloatingSelector = function (options) {
    var defaults = {
        url: '/MedicalRecordManage/Bljgh/GetBlTable',
        width: 350,
        height: 200,
        clickautotrigger: true,
        caption: "Chọn giá trị phần tử",
        ajaxparameters: function ($thisinput) {
            return "keyword=" +$thisinput.val();
        },
        itemdbclickhandler: null,
        colModel: [
             { label: 'bllx', name: 'bllx', widthratio: 30,hidden:true },
             { label: 'Tên bệnh án', name: 'bllxmc', widthratio: 50 },
             { label: 'Tên bảng', name: 'relTB', widthratio: 50 }
        ],
    };
    var options = $.extend(defaults, options);

    $(this).newtouchFloatingSelector(options);
}