var keyValue = $.request("");
$(function () {
    InitControl();
    if (!!keyValue) {
        $.ajax({
            url: "" + Math.random(),
            data: { keyValue: keyValue },
            dataType: "",
            async: false,
            success: function (data) {
                if (data) {
                    $("").formSerialize(data);
                    if (data[""]) {
                        $("").attr("", data[""]);
                    }
                    if (data[""]) {
                        $("").attr("", data[""]);
                    }
                    if (data[""]) {
                        $("").attr("", data[""]);
                    }
                }
            }
        });
    }

    $("").keyup(function () {
        if ($(this).val().trim() === "") {
            $("").attr("", "");
            $("").css("", "");
        } else {
            $("").removeAttr("");
            $("").css("", "");
        }
    });
});
function InitControl() {
    brxzList();
    dlList();
    sfxmList();

    $("").bindSelect({
        url: ""
    });
}
function brxzList() {
    ///Chính sách hoàn trả
    $("").newtouchFloatingSelector({
        height: 200,
        width: 300,
        filter: function (keyword) {
            if (!keyword) {
                return null;
            }
            //Duyệt nguồn dữ liệu，Dùngkeywordđể lọc ra kết quả
            var resultObjArr = new Array();
            $.each(top.window.clients.sysPatientNatureList, function (idx, val) {
                if ((val.py && val.py.toLowerCase().indexOf(keyword.toLowerCase()) >= 0) || keyword.trim() == "") {
                    resultObjArr.push(val);
                }
            });
            return resultObjArr;

        },
        caption: "",
        colModel: [
            { label: "", name: '', widthratio: 25 },
            { label: "", name: '', widthratio: 25 },
            { label: "", name: '', widthratio: 25 },
            { label: "", name: '', widthratio: 25 }
        ],
        itemdbclickhandler: function ($thistr) {
            $("").attr("", $thistr.find("").html());
            $("").val($thistr.find("").html());
            return;
        }
    });
}

function dlList() {
    $("").newtouchFloatingSelector({
        height: 200,
        width: 300,
        filter: function (keyword) {
            
            //Vui lòng dùngajax

        },
        caption: "",
        colModel: [
            { label: "", name: "", widthratio: 25 },
            { label: "", name: "", widthratio: 25 },
            { label: "", name: "", widthratio: 25 }
        ],
        itemdbclickhandler: function ($thistr) {
            $("").attr("", $thistr.find("").html());
            $("").val($thistr.find('').html());
            return;
        }
    });
}

function sfxmList() {
    ///Hạng mục thu phí
    //$("").newtouchFloatingSelector({
    //    height: 200,
    //    width: 300,
    //    filter: function (keyword) {
    //        if (!keyword) {
    //            return null;
    //        }
    //        //Duyệt nguồn dữ liệu，Dùngkeywordđể lọc ra kết quả
    //        var resultObjArr = new Array();
    //        $.each(top.window.clients.sysChargeItemList, function (idx, val) {
    //            if ((val.py && val.py.toLowerCase().indexOf(keyword.toLowerCase()) >= 0) || keyword.trim() == "") {
    //                resultObjArr.push(val);
    //            }
    //        });
    //        return resultObjArr;
    //    },
    //    caption: "",
    //    colModel: [
    //        { label: '', name: '', widthratio: 25 },
    //        { label: '', name: '', widthratio: 25 },
    //        { label: '', name: '', widthratio: 25 },
    //        { label: '', name: '', widthratio: 25 }
    //    ],
    //    itemdbclickhandler: function ($thistr) {
    //        $("").attr("", $thistr.find("").html());
    //        $("").val($thistr.find('').html());
    //        return;
    //    }
    //});

    $("").newtouchFloatingSelector({
        height: 200,
        width: 400,
        url: "",
        ajaxmethod: "",
        ajaxreqdata: function () {
            var reqData = {};
            reqData.keyword = $.trim($("").val());
            reqData.dlCode = $.trim($("").attr(""));
            return reqData;
        },
        caption: "",
        colModel: [
            { label: "", name: "", widthratio: 25, hidden: true },
            { label: "", name: "", widthratio: 25 },
            { label: "", name: "", widthratio: 25 },
            { label: "", name: "", widthratio: 25, hidden: true },
            { label: "", name: "", widthratio: 25, hidden: true },
            { label: "", name: "", widthratio: 25, hidden: true },
            { label: "", name: "", widthratio: 25, hidden: true },
            { label: "", name: "", widthratio: 25 },
            { label: "", name: "", widthratio: 25, hidden: true },
            { label: "", name: "", widthratio: 25, hidden: true },
            { label: "", name: "", widthratio: 25, hidden: true },
            { label: "", name: "", widthratio: 25, hidden: true },
            { label: "", name: "", widthratio: 25, hidden: true },
            { label: "", name: "", widthratio: 25, hidden: true },
            { label: "", name: "", widthratio: 25, hidden: true },
            { label: "", name: "", widthratio: 25, hidden: true }
        ],
        itemdbclickhandler: function ($thistr) {
            $("").attr("", $thistr.find("").html());
            $("").val($thistr.find('').html());
            return;
        }
    });
}

function submitForm() {
    var result = checkNotNull();
    if (result) {
        var data = $("").formSerialize();
        data[""] = $("").attr("");
        data[""] = $("").attr("");
        data[""] = $("").attr("");
        $.submitForm({
            url: "" + keyValue,
            param: data,
            success: function () {
                $.currentWindow().$("").resetSelection();
                $.currentWindow().$("").trigger("");
                //$.currentWindow().window.frames[''].$("").resetSelection();
                //$.currentWindow().window.frames[''].$("").trigger("");
                $.loading(false);
            }
        });
    }
}

function checkNotNull() {
    var validator = $("").validate();
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
            syfw: { required: "" },
            sfjb: { required: "" },
            zfbl: { required: "" },
            zfxz: { required: "" },
            fysx: { required: "" }
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
    //        $("").val();
    //    }
    //});
    //if (!zt) {
    //    $.modalAlert("", "");
    //    return false;
    //}
    return true;
}