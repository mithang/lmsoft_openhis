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
$.fn.zdFloatingSelector = function (options) {
    var defaults = {
        url: '/SystemManage/Common/GetDiagnosisList',
        width: 600,
        height: 300,
        clickautotrigger: true,
        caption: "ChọnChẩn đoán",
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
            { label: 'Phiên âm', name: 'py', widthratio: 20 },
            { label: 'icd10', name: 'icd10', widthratio: 20 },
            { label: 'icd10Mã bổ sung', name: 'icd10fjm', hidden: true }
        ]
    };
    var options = $.extend(defaults, options);

    $(this).newtouchFloatingSelector(options);
}

//Phẫu thuật
$.fn.opFloatingSelector = function (options) {
    var defaults = {
        url: '/SystemManage/Common/GetOperationList',
        width: 600,
        height: 300,
        clickautotrigger: true,
        caption: "ChọnPhẫu thuật",
        ajaxparameters: function ($thisinput) {
            return "keyword=" + $thisinput.val() + "&type=true";
        },
        itemdbclickhandler: null,
        colModel: [
            { label: 'Mã', name: 'ssdm', hidden: true },
            { label: 'Tên', name: 'ssmc', widthratio: 60 },
            { label: 'Mã ghi nhớ', name: 'zjm', widthratio: 20 },
            { label: 'Cấp độ phẫu thuật', name: 'ssjb', widthratio: 20 },
        ]
    };
    var options = $.extend(defaults, options);

    $(this).newtouchFloatingSelector(options);
}

//Vị tríngườiBộ chọn nhân viên
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
            { label: 'Tên bác sĩ', name: 'StaffName', widthratio: 48 },
            { label: 'Tên khoa', name: 'ksmc', widthratio: 50 },
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

//Quốc tịchBộ chọn
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
            { label: 'Quốc giaMã', name: 'Code', widthratio: 30 },
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
//Dân tộcBộ chọn
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
            { label: 'Dân tộcMã', name: 'Code', widthratio: 30 },
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
//Nhóm thu phí lớnBộ chọn
$.fn.hissfdlFloatingSelector = function (options) {
    $(this).newtouchFloatingSelector({
        url: '/SystemManage/Common/GetHisSfdl',
        width: 300,
        height: 200,
        clickautotrigger: true,
        ajaxparameters: function ($thisinput) {
            return "keyword=" + $thisinput.val();
        },
        colModel: [
            { label: 'Nhóm thu phí lớn', name: 'dlcode', widthratio: 30 },
            { label: 'Tên nhóm lớn', name: 'dlmc', widthratio: 70 },
        ],
        itemdbclickhandler: function ($thistr, $thisinput) {
            //Khi lưu, kiểm tra val và data-Name nhất quán
            $thisinput.attr('attr-dlcode', $thistr.attr('data-dlcode'));
            $thisinput.val($thistr.attr('data-dlmc'));
        }
    });
}

