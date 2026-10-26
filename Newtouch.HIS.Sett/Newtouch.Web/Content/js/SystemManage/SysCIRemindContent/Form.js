
var keyValue = $.request("keyValue");
$(function () {
    InitControl();
    //if (!!keyValue) {
    $.ajax({
        url: "/SystemManage/SysCIRemindContent/GetFormJson?r=" + Math.random(),
        data: { keyValue: keyValue },
        dataType: "json",
        async: false,
        success: function (data) {
            $("#form1").formSerialize(data);
            if (data["sfxmmc"]) {
                $("#sfxmmc").attr("data-label", data["sfxmmc"]);
            }
        }
    });
    //}
})
function InitControl() {
    dlList();//Hạng mục thu phí
}
function dlList() {
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
function submitForm() {
    if (checkNotNull()) {
        var data = $("#form1").formSerialize();
        data["sfxm"] = $("#sfxmmc").attr("data-label");
        $.submitForm({
            url: "/SystemManage/SysCIRemindContent/SubmitForm?keyValue=" + keyValue,
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
            mzjsnr: { required: true },
            zyjsnr: { required: true },
            mzjsjb: { required: true },
            zyjsjb: { required: true },
            zt: { required: true }
        },
        messages: {
            mzjsnr: { required: "Nội dung cảnh báo khám ngoại trú bắt buộc phải nhập" },
            zyjsnr: { required: "Nội dung cảnh báo nội trú bắt buộc phải nhập" },
            mzjsjb: { required: "Mức cảnh báo khám ngoại trú bắt buộc phải chọn" },
            zyjsjb: { required: "Mức cảnh báo nội trú bắt buộc phải chọn" },
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
    ////Trạng thái
    //var zt = false;
    //$('input[name="zt"]').each(function () {
    //    var $this = $(this);
    //    if ($this.parent().hasClass("active")) {
    //        zt = true;
    //        $("#zt").val()
    //    }
    //});
    //if (!zt) {
    //    $.modalAlert("Vui lòng chọn có hiệu lực hay không！", 'warning');
    //    return false;
    //}
    return true;
}