
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
    });
    //}
    $("").blur(function () {
        //$("").val($(this).toPinyin());
        $('').val($(this).toPinyin());
    });
})
function InitControl() {
    dlList();//Nhóm lớn
    flList();//Phân loại 
    ybdmList(); //Mã bảo hiểm y tế
    nbList();//Nhóm lớn bảo hiểm nông nghiệp
    baList();//Nhóm lớn bệnh án
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
            $.each(top.window.newtouchclients.sysMajorClassList, function (idx, val) {
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

function flList() {
    $("").newtouchFloatingSelector({
        height: 200,
        width: 300,
        filter: function (keyword) {
            if (!keyword) {
                return null;
            }
            //Duyệt nguồn dữ liệu，Dùngkeywordđể lọc ra kết quả
            var resultObjArr = new Array();
            $.each(top.window.newtouchclients.sysChargeClassifyList, function (idx, val) {
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

function nbList() {
    $("").newtouchFloatingSelector({
        height: 200,
        width: 300,
        filter: function (keyword) {
            if (!keyword) {
                return null;
            }
            //Duyệt nguồn dữ liệu，Dùngkeywordđể lọc ra kết quả
            var resultObjArr = new Array();
            $.each(top.window.newtouchclients.sysnbsfdlList, function (idx, val) {
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

function baList() {
    $("").newtouchFloatingSelector({
        height: 200,
        width: 300,
        filter: function (keyword) {
            if (!keyword) {
                return null;
            }
            //Duyệt nguồn dữ liệu，Dùngkeywordđể lọc ra kết quả
            var resultObjArr = new Array();
            $.each(top.window.newtouchclients.sysbasfdlList, function (idx, val) {
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

function ybdmList() {
    $("").newtouchFloatingSelector({
        height: 200,
        width: 400,
        filter: function (keyword) {
            if (!keyword) {
                return null;
            }
            //Duyệt nguồn dữ liệu，Dùngkeywordđể lọc ra kết quả
            var resultObjArr = new Array();
            $.each(top.window.newtouchclients.syszlxmList, function (idx, val) {
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
            $("").val($thistr.find('').html());
            return;
        }
    });
}

function submitForm() {
    if (checkNotNull()) {
        var data = $("").formSerialize();
        data[""] = $("").attr("");
        data[""] = $("").attr("");
        data[""] = $("").attr("");
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
            sfxmmc: { required: "" },
            py: { required: "" },
            dw: { required: "" },
            dj: { required: "" },
            zfbl: { required: "" },
            zfxz: { required: "" },
            mzzybz: { required: "" },
            ssbz: { required: "" },
            tsbz: { required: "" },
            jsbz: { required: "" },
            sfbz: { required: "" },
            zt: { required: "" },
            dl: { required: "" },
        },
        showErrors: function (errorMap, errorList) {
            if (!$.isEmptyObject(errorList)) {
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