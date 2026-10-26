
var keyValue = $.request("keyValue");
$(function () {
    InitControl();
    //if (!!keyValue) {
    $.ajax({
        url: "/SystemManage/SysChargeItem/GetFormJson?r=" + Math.random(),
        data: { keyValue: keyValue },
        dataType: "json",
        async: false,
        success: function (data) {
            $("#form1").formSerialize(data);
            if (data["dlmc"]) {
                $("#dlmc").attr("data-label", data["dl"]);
            }
            if (data["flmc"]) {
                $("#flmc").attr("data-label", data["fl"]);
            }
            if (data["nbdlmc"]) {
                $("#nbdlmc").attr("data-label", data["nbdl"]);
            }
            if (data["badlmc"]) {
                $("#badlmc").attr("data-label", data["badl"]);
            }
        }
    });
    //}
    $("#sfxmmc").blur(function () {
        //$("#py").val($(this).toPinyin());
        $('#py').val($(this).toPinyin());
    });
})
function InitControl() {
    dlList();//Nhóm lớn
    flList();//Phân loại 
    ybdmList(); //Mã bảo hiểm y tế
    nbList();//Nhóm lớn bảo hiểm nông nghiệp
    baList();//Nhóm lớn hồ sơ bệnh án
}
function dlList() {
    $("#dlmc").newtouchFloatingSelector({
        height: 200,
        width: 300,
        filter: function (keyword) {
            if (!keyword) {
                return null;
            }
            //Duyệt nguồn dữ liệu，用keywordđể lọc ra kết quả
            var resultObjArr = new Array();
            $.each(top.window.newtouchclients.sysMajorClassList, function (idx, val) {
                if ((val.py && val.py.toLowerCase().indexOf(keyword.toLowerCase()) >= 0) || keyword.trim() == "") {
                    resultObjArr.push(val);
                }
            });
            return resultObjArr;

        },
        caption: "Nhóm lớn",
        colModel: [
            { label: 'Số hiệu', name: 'dlbh', widthratio: 25 },
            { label: 'Mã', name: 'dl', widthratio: 25 },
            { label: 'Tên', name: 'dlmc', widthratio: 25 },
            { label: 'Phiên âm', name: 'py', widthratio: 25 }
        ],
        itemdbclickhandler: function ($thistr) {
            $("#dlmc").attr("data-label", $thistr.find("td:eq(1)").html());
            $("#dlmc").val($thistr.find('td:eq(2)').html());
            return;
        }
    });
}

function flList() {
    $("#flmc").newtouchFloatingSelector({
        height: 200,
        width: 300,
        filter: function (keyword) {
            if (!keyword) {
                return null;
            }
            //Duyệt nguồn dữ liệu，用keywordđể lọc ra kết quả
            var resultObjArr = new Array();
            $.each(top.window.newtouchclients.sysChargeClassifyList, function (idx, val) {
                if ((val.py && val.py.toLowerCase().indexOf(keyword.toLowerCase()) >= 0) || keyword.trim() == "") {
                    resultObjArr.push(val);
                }
            });
            return resultObjArr;

        },
        caption: "Phân loại",
        colModel: [
            { label: 'Số hiệu', name: 'flbh', widthratio: 25 },
            { label: 'Mã', name: 'fl', widthratio: 25 },
            { label: 'Tên', name: 'flmc', widthratio: 25 },
            { label: 'Phiên âm', name: 'py', widthratio: 25 }
        ],
        itemdbclickhandler: function ($thistr) {
            $("#flmc").attr("data-label", $thistr.find("td:eq(1)").html());
            $("#flmc").val($thistr.find('td:eq(2)').html());
            return;
        }
    });
}

function nbList() {
    $("#nbdlmc").newtouchFloatingSelector({
        height: 200,
        width: 300,
        filter: function (keyword) {
            if (!keyword) {
                return null;
            }
            //Duyệt nguồn dữ liệu，用keywordđể lọc ra kết quả
            var resultObjArr = new Array();
            $.each(top.window.newtouchclients.sysnbsfdlList, function (idx, val) {
                if ((val.py && val.py.toLowerCase().indexOf(keyword.toLowerCase()) >= 0) || keyword.trim() == "") {
                    resultObjArr.push(val);
                }
            });
            return resultObjArr;

        },
        caption: "Nhóm lớn",
        colModel: [
            { label: 'Số hiệu', name: 'dlbh', widthratio: 25 },
            { label: 'Mã', name: 'dl', widthratio: 25 },
            { label: 'Tên', name: 'dlmc', widthratio: 25 },
            { label: 'Phiên âm', name: 'py', widthratio: 25 }
        ],
        itemdbclickhandler: function ($thistr) {
            $("#nbdlmc").attr("data-label", $thistr.find("td:eq(1)").html());
            $("#nbdlmc").val($thistr.find('td:eq(2)').html());
            return;
        }
    });
}

function baList() {
    $("#badlmc").newtouchFloatingSelector({
        height: 200,
        width: 300,
        filter: function (keyword) {
            if (!keyword) {
                return null;
            }
            //Duyệt nguồn dữ liệu，用keywordđể lọc ra kết quả
            var resultObjArr = new Array();
            $.each(top.window.newtouchclients.sysbasfdlList, function (idx, val) {
                if ((val.py && val.py.toLowerCase().indexOf(keyword.toLowerCase()) >= 0) || keyword.trim() == "") {
                    resultObjArr.push(val);
                }
            });
            return resultObjArr;

        },
        caption: "Nhóm lớn",
        colModel: [
            { label: 'Số hiệu', name: 'dlbh', widthratio: 25 },
            { label: 'Mã', name: 'dl', widthratio: 25 },
            { label: 'Tên', name: 'dlmc', widthratio: 25 },
            { label: 'Phiên âm', name: 'py', widthratio: 25 }
        ],
        itemdbclickhandler: function ($thistr) {
            $("#badlmc").attr("data-label", $thistr.find("td:eq(1)").html());
            $("#badlmc").val($thistr.find('td:eq(2)').html());
            return;
        }
    });
}

function ybdmList() {
    $("#ybdm").newtouchFloatingSelector({
        height: 200,
        width: 400,
        filter: function (keyword) {
            if (!keyword) {
                return null;
            }
            //Duyệt nguồn dữ liệu，用keywordđể lọc ra kết quả
            var resultObjArr = new Array();
            $.each(top.window.newtouchclients.syszlxmList, function (idx, val) {
                if ((val.py && val.py.toLowerCase().indexOf(keyword.toLowerCase()) >= 0) || keyword.trim() == "") {
                    resultObjArr.push(val);
                }
            });
            return resultObjArr;

        },
        caption: "Phân loại",
        colModel: [
            { label: 'Số hiệu', name: 'py', widthratio: 25 },
            { label: 'Mã bảo hiểm y tế', name: 'ybdm', widthratio: 25 },
            { label: 'Mã giá', name: 'wjdm', widthratio: 25 },
            { label: 'Tên', name: 'xmmc', widthratio: 25 }
        ],
        itemdbclickhandler: function ($thistr) {
            $("#ybdm").attr("data-label", $thistr.find("td:eq(0)").html());
            $("#ybdm").val($thistr.find('td:eq(1)').html());
            $("#wjdm").val($thistr.find('td:eq(2)').html());
            return;
        }
    });
}

function submitForm() {
    if (checkNotNull()) {
        var data = $("#form1").formSerialize();
        data["dl"] = $("#dlmc").attr("data-label");
        data["fl"] = $("#flmc").attr("data-label");
        data["badl"] = $("#badlmc").attr("data-label");
        data["nbdl"] = $("#nbdlmc").attr("data-label");
        $.submitForm({
            url: "/SystemManage/SysChargeItem/SubmitForm?keyValue=" + keyValue,
            param: data,
            success: function () {
                //$.currentWindow().$("#gridList").resetSelection();
                //$.currentWindow().$("#gridList").trigger("reloadGrid");
                $.currentWindow().window.frames['iframeExpenseManage'].$("#gridList").resetSelection();
                $.currentWindow().window.frames['iframeExpenseManage'].$("#gridList").trigger("reloadGrid");
                $.loading(false);
            }
        })
    }
}


function checkNotNull() {
    var validator = $('#form1').validate();
    validator.settings = {
        rules: {
            sfxmmc: { required: true },
            py: { required: true },
            dw: { required: true },
            dj: { required: true },
            zfbl: { required: true },
            zfxz: { required: true },
            mzzybz: { required: true },
            ssbz: { required: true },
            tsbz: { required: true },
            jsbz: { required: true },
            sfbz: { required: true },
            zt: { required: true },
            dl: { required: true },
        },
        messages: {
            sfxmmc: { required: "Tên hạng mục thu phí bắt buộc phải nhập" },
            py: { required: "Phiên âm bắt buộc phải nhập" },
            dw: { required: "Đơn vị bắt buộc phải nhập" },
            dj: { required: "Đơn giá bắt buộc phải nhập" },
            zfbl: { required: "Tỷ lệ tự chịu bắt buộc phải nhập" },
            zfxz: { required: "Tính chất tự chịu bắt buộc phải chọn" },
            mzzybz: { required: "Phạm vi bắt buộc phải chọn" },
            ssbz: { required: "Cờ thực hiện bắt buộc phải chọn" },
            tsbz: { required: "Cờ đặc biệt bắt buộc phải chọn" },
            jsbz: { required: "Cờ cảnh báo bắt buộc phải chọn" },
            sfbz: { required: "Cờ thu phí bắt buộc phải chọn" },
            zt: { required: "Trạng thái bắt buộc phải chọn" },
            dl: { required: "Nhóm lớn bắt buộc phải chọn" },
        },
        showErrors: function (errorMap, errorList) {
            if (!$.isEmptyObject(errorList)) {
                $.modalAlert(errorList[0].message, 'warning');
            }
        }
    }
    if (!validator.form()) {
        return false;
    }
    //Trạng thái
    var zt = false;
    $('input[name="zt"]').each(function () {
        var $this = $(this);
        if ($this.parent().hasClass("active")) {
            zt = true;
            $("#zt").val()
        }
    });
    if (!zt) {
        $.modalAlert("Vui lòng chọn có hiệu lực hay không！", 'warning');
        return false;
    }
    return true;
}