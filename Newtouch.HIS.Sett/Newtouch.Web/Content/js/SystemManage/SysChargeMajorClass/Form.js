
var keyValue = $.request("");
$(function () {
    InitControl();
    //if (!!keyValue) {
        $.ajax({
            url: "" + Math.random(),
            data: { keyValue: keyValue },
            dataType: "",
            async: false,
            success: function (data) {
                $("").formSerialize(data);
            }
        });
    //}
})
function InitControl() {
    printreportList();
    printbillList();
}
function printreportList() {
    $("").bindSelect({
        url: "",
    });
}

function printbillList() {
    $("").bindSelect({
        url: ""
    });
}

function submitForm() {
    if (checkNotNull()) {
        $.submitForm({
            url: "" + keyValue,
            param: $("").formSerialize(),
            success: function () {
                //$.currentWindow().$("").resetSelection();
                //$.currentWindow().$("").trigger("");
                $.currentWindow().window.frames[''].$("").resetSelection();
                $.currentWindow().window.frames[''].$("").trigger("");
                $.loading(false);
            }
        })
    }
}

function checkNotNull() {
    var validator = $('').validate();
    validator.settings = {
        rules: {
            dlmc: { required: true },
            py: { required: true },
            mzzybz: { required: true },
            mzprintreportcode: { required: true },
            mzprintbillcode: { required: true },
            zt: { required: true },
        },
        messages: {
            dlmc: { required: "" },
            py: { required: "" },
            mzzybz: { required: "" },
            mzprintreportcode: { required: "" },
            mzprintbillcode: { required: "" },
            zt: { required: "" }
        },
        showErrors: function (errorMap, errorList) {
            if (!$.isEmptyObject(errorList)) {
                if (Array.isArray(errorList) && errorList.length == 0) {
                    return;
                }
                $.modalAlert(errorList[0].message, '');
            }
        }
    }
    if (!validator.form()) {
        return false;
    }
    //Trạng thái
    var zt = false;
    $('').each(function () {
        var $this = $(this);
        if ($this.parent().hasClass("")) {
            zt = true;
            $("").val()
        }
    });
    if (!zt) {
        $.modalAlert("", '');
        return false;
    }
    return true;
}