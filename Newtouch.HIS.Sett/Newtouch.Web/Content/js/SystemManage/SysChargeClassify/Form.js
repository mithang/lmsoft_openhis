
var keyValue = $.request("");
$(function () {
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
    $("").blur(function () {
        $('').val($(this).toPinyin());
    });
})
function submitForm() {
    if (checkNotNull()) {
        $.submitForm({
            url: "" + keyValue,
            param: $("").formSerialize(),
            success: function () {
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
            flmc: { required: true },
            py: { required: true },
            zt: { required: true }
        },
        messages: {
            flmc: { required: "" },
            py: { required: "" },
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
    return true;
}