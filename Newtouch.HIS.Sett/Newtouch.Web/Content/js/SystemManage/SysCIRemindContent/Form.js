
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
            if (data[""]) {
                $("").attr("", data[""]);
            }
        }
    });
    //}
})
function InitControl() {
    dlList();//Hạng mục thu phí
}
function dlList() {
    $("").newtouchFloatingSelector({
        height: 200,
        width: 300,
        filter: function (keyword) {
            if (!keyword) {
                return null;
            }
            //Duyệt nguồn dữ liệu，Dùngkeywordđể lọc ra kết quả
            var resultObjArr = new Array();
            $.each(top.window.clients.sysChargeItemList, function (idx, val) {
                if ((val.py && val.py.toLowerCase().indexOf(keyword.toLowerCase()) >= 0) || keyword.trim() == "") {
                    resultObjArr.push(val);
                }
            });
            return resultObjArr;

        },
        caption: "",
        colModel: [
            { label: '', name: '', widthratio: 25 },
            { label: '', name: '', widthratio: 25 },
            { label: '', name: '', widthratio: 25 },
            { label: '', name: '', widthratio: 25 }
        ],
        itemdbclickhandler: function ($thistr) {
            $("").attr("", $thistr.find("").html());
            $("").val($thistr.find('').html());
            return;
        }
    });
}
function submitForm() {
    if (checkNotNull()) {
        var data = $("").formSerialize();
        data[""] = $("").attr("");
        $.submitForm({
            url: "" + keyValue,
            param: data,
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
            mzjsnr: { required: true },
            zyjsnr: { required: true },
            mzjsjb: { required: true },
            zyjsjb: { required: true },
            zt: { required: true }
        },
        messages: {
            mzjsnr: { required: "" },
            zyjsnr: { required: "" },
            mzjsjb: { required: "" },
            zyjsjb: { required: "" },
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
    ////Trạng thái
    //var zt = false;
    //$('').each(function () {
    //    var $this = $(this);
    //    if ($this.parent().hasClass("")) {
    //        zt = true;
    //        $("").val()
    //    }
    //});
    //if (!zt) {
    //    $.modalAlert("", '');
    //    return false;
    //}
    return true;
}