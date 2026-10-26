
var keyValue = $.request("");
$(function () {
    InitControl();
    if (!!keyValue) {
        $.ajax({
            url: "",
            data: { keyValue: keyValue },
            dataType: "",
            async: false,
            success: function (data) {
                $("").formSerialize(data);
            }
        });
    }
})
function InitControl() {
    xmList();//Hạng mục thu phí
    ksList();//Tính chất bệnh nhân
}
function xmList() {
    $("").bindSelect({
        url: "",
    });
}

function ksList() {
    $("").bindSelect({
        url: ""
    });
}
function submitForm() {
    if (!$('').formValid()) {
        return false;
    }
    $.submitForm({
        url: "" + keyValue,
        param: $("").formSerialize(),
        success: function () {
            $.currentWindow().$("").resetSelection();
            $.currentWindow().$("").trigger("");
            $.loading(false);
        }
    })
}