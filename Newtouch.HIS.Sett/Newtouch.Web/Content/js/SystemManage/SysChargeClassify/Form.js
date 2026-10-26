
var keyValue = $.request("keyValue");
$(function () {
    //if (!!keyValue) {
    $.ajax({
        url: "/SystemManage/SysChargeClassify/GetFormJson?r=" + Math.random(),
        data: { keyValue: keyValue },
        dataType: "json",
        async: false,
        success: function (data) {
            $("#form1").formSerialize(data);
        }
    });
    //}
    $("#flmc").blur(function () {
        $('#py').val($(this).toPinyin());
    });
})
function submitForm() {
    if (checkNotNull()) {
        $.submitForm({
            url: "/SystemManage/SysChargeClassify/SubmitForm?keyValue=" + keyValue,
            param: $("#form1").formSerialize(),
            success: function () {
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
            flmc: { required: true },
            py: { required: true },
            zt: { required: true }
        },
        messages: {
            flmc: { required: "Tên phân loại bắt buộc phải nhập" },
            py: { required: "Phiên âm bắt buộc phải nhập" },
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