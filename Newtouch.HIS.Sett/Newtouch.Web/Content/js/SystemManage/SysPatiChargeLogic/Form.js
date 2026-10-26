var keyValue = $.request("keyValue");
$(function () {
    InitControl();
    if (!!keyValue) {
        $.ajax({
            url: "/SystemManage/SysPatiChargeLogic/GetFormJson?r=" + Math.random(),
            data: { keyValue: keyValue },
            dataType: "json",
            async: false,
            success: function (data) {
                if (data) {
                    $("#form1").formSerialize(data);
                    if (data["dlmc"]) {
                        $("#dlmc").attr("data-label", data["dl"]);
                    }
                    if (data["brxzmc"]) {
                        $("#brxzmc").attr("data-label", data["brxz"]);
                    }
                    if (data["sfxmmc"]) {
                        $("#sfxmmc").attr("data-label", data["sfxm"]);
                    }
                }
            }
        });
    }

    $("#dlmc").keyup(function () {
        if ($(this).val().trim() === "*") {
            $("#sfxmmc").attr("disabled", "disabled");
            $("#sfxmmc").css("background-color", "#f1f4f6");
        } else {
            $("#sfxmmc").removeAttr("disabled");
            $("#sfxmmc").css("background-color", "#fff");
        }
    });
});
function InitControl() {
    brxzList();
    dlList();
    sfxmList();

    $("#OrganizeId").bindSelect({
        url: "/SystemManage/Organize/GetChildTreeSelectJson"
    });
}
function brxzList() {
    ///Chính sách thanh toán BHYT
    $("#brxzmc").newtouchFloatingSelector({
        height: 200,
        width: 300,
        filter: function (keyword) {
            if (!keyword) {
                return null;
            }
            //Duyệt nguồn dữ liệu，用keywordđể lọc ra kết quả
            var resultObjArr = new Array();
            $.each(top.window.clients.sysPatientNatureList, function (idx, val) {
                if ((val.py && val.py.toLowerCase().indexOf(keyword.toLowerCase()) >= 0) || keyword.trim() == "") {
                    resultObjArr.push(val);
                }
            });
            return resultObjArr;

        },
        caption: "Tính chất bệnh nhân",
        colModel: [
            { label: "Số hiệu", name: 'brxzbh', widthratio: 25 },
            { label: "Mã", name: 'brxz', widthratio: 25 },
            { label: "Tên", name: 'brxzmc', widthratio: 25 },
            { label: "Phiên âm", name: 'py', widthratio: 25 }
        ],
        itemdbclickhandler: function ($thistr) {
            $("#brxzmc").attr("data-label", $thistr.find("td:eq(1)").html());
            $("#brxzmc").val($thistr.find("td:eq(2)").html());
            return;
        }
    });
}

function dlList() {
    $("#dlmc").newtouchFloatingSelector({
        height: 200,
        width: 300,
        filter: function (keyword) {
            
            //Vui lòng dùngajax

        },
        caption: "Nhóm lớn",
        colModel: [
            { label: "Số hiệu", name: "dlCode", widthratio: 25 },
            { label: "Tên", name: "dlmc", widthratio: 25 },
            { label: "Phiên âm", name: "py", widthratio: 25 }
        ],
        itemdbclickhandler: function ($thistr) {
            $("#dlmc").attr("data-label", $thistr.find("td:eq(0)").html());
            $("#dlmc").val($thistr.find('td:eq(1)').html());
            return;
        }
    });
}

function sfxmList() {
    ///Hạng mục thu phí
    //$("#sfxmmc").newtouchFloatingSelector({
    //    height: 200,
    //    width: 300,
    //    filter: function (keyword) {
    //        if (!keyword) {
    //            return null;
    //        }
    //        //Duyệt nguồn dữ liệu，用keywordđể lọc ra kết quả
    //        var resultObjArr = new Array();
    //        $.each(top.window.clients.sysChargeItemList, function (idx, val) {
    //            if ((val.py && val.py.toLowerCase().indexOf(keyword.toLowerCase()) >= 0) || keyword.trim() == "") {
    //                resultObjArr.push(val);
    //            }
    //        });
    //        return resultObjArr;
    //    },
    //    caption: "Hạng mục thu phí",
    //    colModel: [
    //        { label: 'Số hiệu', name: 'sfxmbh', widthratio: 25 },
    //        { label: 'Mã', name: 'sfxm', widthratio: 25 },
    //        { label: 'Tên', name: 'sfxmmc', widthratio: 25 },
    //        { label: 'Phiên âm', name: 'py', widthratio: 25 }
    //    ],
    //    itemdbclickhandler: function ($thistr) {
    //        $("#sfxmmc").attr("data-label", $thistr.find("td:eq(1)").html());
    //        $("#sfxmmc").val($thistr.find('td:eq(2)').html());
    //        return;
    //    }
    //});

    $("#sfxmmc").newtouchFloatingSelector({
        height: 200,
        width: 400,
        url: "/SystemManage/SysPatiChargeLogic/GetSFXMItemInfoByDlCode",
        ajaxmethod: "POST",
        ajaxreqdata: function () {
            var reqData = {};
            reqData.keyword = $.trim($("#sfxm").val());
            reqData.dlCode = $.trim($("#dlmc").attr("data-label"));
            return reqData;
        },
        caption: "Hạng mục thu phí",
        colModel: [
            { label: "có phải hạng mục hay không", name: "xmtype", widthratio: 25, hidden: true },
            { label: "Mã số", name: "py", widthratio: 25 },
            { label: "Tên chung", name: "xmmc", widthratio: 25 },
            { label: "Tên thương mại", name: "spm", widthratio: 25, hidden: true },
            { label: "Đơn vị", name: "dw", widthratio: 25, hidden: true },
            { label: "Đơn giá", name: "dj", widthratio: 25, hidden: true },
            { label: "Quy cách", name: "ypgg", widthratio: 25, hidden: true },
            { label: "Mã bảo hiểm y tế", name: "ybdm", widthratio: 25 },
            { label: "Mã đóng gói", name: "ypbzdm", widthratio: 25, hidden: true },
            { label: "xmbh", name: "xmbh", widthratio: 25, hidden: true },
            { label: "sfxm", name: "sfxm", widthratio: 25, hidden: true },
            { label: "dl", name: "dl", widthratio: 25, hidden: true },
            { label: "zfbl", name: "zfbl", widthratio: 25, hidden: true },
            { label: "zfxz", name: "zfxz", widthratio: 25, hidden: true },
            { label: "ybdm", name: "ybdm", widthratio: 25, hidden: true },
            { label: "yfdm", name: "yfdm", widthratio: 25, hidden: true }
        ],
        itemdbclickhandler: function ($thistr) {
            $("#sfxmmc").attr("data-label", $thistr.find("td:eq(10)").html());
            $("#sfxmmc").val($thistr.find('td:eq(2)').html());
            return;
        }
    });
}

function submitForm() {
    var result = checkNotNull();
    if (result) {
        var data = $("#form1").formSerialize();
        data["sfxm"] = $("#sfxmmc").attr("data-label");
        data["brxz"] = $("#brxzmc").attr("data-label");
        data["dl"] = $("#dlmc").attr("data-label");
        $.submitForm({
            url: "/SystemManage/SysPatiChargeLogic/SubmitForm?keyValue=" + keyValue,
            param: data,
            success: function () {
                $.currentWindow().$("#gridList").resetSelection();
                $.currentWindow().$("#gridList").trigger("reloadGrid");
                //$.currentWindow().window.frames['iframeExpenseManage'].$("#gridList").resetSelection();
                //$.currentWindow().window.frames['iframeExpenseManage'].$("#gridList").trigger("reloadGrid");
                $.loading(false);
            }
        });
    }
}

function checkNotNull() {
    var validator = $("#form1").validate();
    validator.settings = {
        rules: {
            syfw: { required: true },
            sfjb: { required: true },
            fyfw: { required: true },
            zfbl: { required: true },
            zfxz: { required: true },
            fysx: { required: true }
        },
        messages: {
            syfw: { required: "Phạm vi áp dụng bắt buộc phải nhập" },
            sfjb: { required: "Cấp độ thuật toán bắt buộc phải nhập" },
            zfbl: { required: "Tỷ lệ tự trả bắt buộc phải nhập" },
            zfxz: { required: "Tính chất tự trả bắt buộc phải nhập" },
            fysx: { required: "Giới hạn chi phí bắt buộc phải nhập" }
        },
        showErrors: function (errorMap, errorList) {
            if (!$.isEmptyObject(errorList)) {
                if (Array.isArray(errorList) && errorList.length == 0) {
                    return;
                }
                $.modalAlert(errorList[0].message, 'warning');
            }
        }
    }
    if (!validator.form()) {
        return false;
    }
    ////Trạng thái
    //var zt = false;
    //$('input[name="zt"]').each(function () {
    //    var $this = $(this);
    //    if ($this.parent().hasClass("active")) {
    //        zt = true;
    //        $("#zt").val();
    //    }
    //});
    //if (!zt) {
    //    $.modalAlert("Vui lòng chọn có hiệu lực hay không！", "warning");
    //    return false;
    //}
    return true;
}