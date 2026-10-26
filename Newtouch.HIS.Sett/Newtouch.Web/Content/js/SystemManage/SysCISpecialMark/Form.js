
var keyValue = $.request("keyValue");
$(function () {
    InitControl();
    //if (!!keyValue) {
    $.ajax({
        url: "/SystemManage/SysCISpecialMark/GetFormJson?r=" + Math.random(),
        data: { keyValue: keyValue },
        dataType: "json",
        async: false,
        success: function (data) {
            $("#form1").formSerialize(data);
            if (data["sfxm"]) {
                $("#sfxmmc").attr("data-label", data["sfxm"]);
            }
            if (data["brxz"]) {
                $("#brxzmc").attr("data-label", data["brxz"]);
            }
        }
    });
    //}
})
function InitControl() {
    xmList();//Hạng mục thu phí
    xzList();//Tính chất bệnh nhân
}
function xmList() {
    $("#sfxmmc").newtouchFloatingSelector({
        height: 200,
        width: 300,
        filter: function (keyword) {
            if (!keyword) {
                return null;
            }
            //Duyệt nguồn dữ liệu，用keywordđể lọc ra kết quả
            var resultObjArr = new Array();
            $.each(top.window.clients.sysChargeItemList, function (idx, val) {
                if ((val.py && val.py.toLowerCase().indexOf(keyword.toLowerCase()) >= 0) || keyword.trim() == "") {
                    resultObjArr.push(val);
                }
            });
            return resultObjArr;

        },
        caption: "Hạng mục thu phí",
        colModel: [
            { label: 'Số hiệu', name: 'sfxmbh', widthratio: 25 },
            { label: 'Mã', name: 'sfxm', widthratio: 25 },
            { label: 'Tên', name: 'sfxmmc', widthratio: 25 },
            { label: 'Phiên âm', name: 'py', widthratio: 25 }
        ],
        itemdbclickhandler: function ($thistr) {
            $("#sfxmmc").attr("data-label", $thistr.find("td:eq(1)").html());
            $("#sfxmmc").val($thistr.find('td:eq(2)').html());
            return;
        }
    });
}

function xzList() {
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
            { label: 'Số hiệu', name: 'brxzbh', widthratio: 25 },
            { label: 'Mã', name: 'brxz', widthratio: 25 },
            { label: 'Tên', name: 'brxzmc', widthratio: 25 },
            { label: 'Phiên âm', name: 'py', widthratio: 25 }
        ],
        itemdbclickhandler: function ($thistr) {
            $("#brxzmc").attr("data-label", $thistr.find("td:eq(1)").html());
            $("#brxzmc").val($thistr.find('td:eq(2)').html());
            return;
        }
    });
}
function submitForm() {
    if (checkNotNull()) {
        var data = $("#form1").formSerialize();
        data["sfxm"] = $("#sfxmmc").attr("data-label");
        data["brxz"] = $("#brxzmc").attr("data-label");
        $.submitForm({
            url: "/SystemManage/SysCISpecialMark/SubmitForm?keyValue=" + keyValue,
            param: data,
            success: function () {
                //$.currentWindow().$("#gridList").resetSelection();
                //$.currentWindow().$("#gridList").trigger("reloadGrid");
                //$.loading(false);
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
            brxzmc: { required: true },
            zt: { required: true }
        },
        messages: {
            brxzmc: { required: "Tính chất bệnh nhân bắt buộc phải nhập" },
            zt: { required: "Trạng thái bắt buộc phải chọn" }
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
    return true;
}