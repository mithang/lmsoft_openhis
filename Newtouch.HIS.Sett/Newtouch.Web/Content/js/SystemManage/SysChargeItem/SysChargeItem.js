$(function () {
    gridList();
})
function gridList() {
    var $gridList = $("");
    $gridList.dataGrid({
        url: "",
        height: $(window).height() - 128,
        colModel: [
            { label: "", name: "", hidden: true, key: true },
            { label: "", name: "", width: 80, align: '' },
            { label: '', name: '', width: 100, align: '' },
            { label: '', name: '', width: 50, align: '' },
            { label: '', name: '', width: 50, align: '' },
            { label: '', name: '', width: 50, align: '' },
            { label: '', name: '', width: 100, align: '' },
            { label: '', name: '', width: 100, align: '' },
            { label: '', name: '', width: 50, align: '' },
            { label: '', name: '', width: 100, align: '' },
            { label: '', name: '', width: 100, align: '' },
             {
                 label: '', name: '', width: 80, align: '',
                 formatter: function (cellvalue) {
                     if (cellvalue == "") {
                         return "";
                     } else if (cellvalue == "") {
                         return "";
                     } else if (cellvalue == "") {
                         return "";
                     } else {
                         return "";
                     }
                 }
             }, {
                 label: '', name: '', width: 50, align: '',
                 formatter: function (cellvalue) {
                     if (cellvalue == 0) {
                         return ""
                     } else {
                         return ""
                     }
                 }
             },
{
    label: '', name: '', width: 50, align: '',
    formatter: function (cellvalue) {
        if (cellvalue == 0) {
            return ""
        } else {
            return ""
        }
    }
},
{
    label: '', name: '', width: 50, align: '',
    formatter: function (cellvalue) {
        if (cellvalue == 0) {
            return ""
        } else {
            return ""
        }
    }
},
{ label: '', name: '', width: 50, align: '' },
{
    label: '', name: '', width: 50, align: '',
    formatter: function (cellvalue) {
        if (cellvalue == 0) {
            return ""
        } else {
            return ""
        }
    }
}],
        pager: "",
        sortname: '',
        rowNum: '',
        viewrecords: true
    });
    $("").click(function () {
        $gridList.jqGrid('', {
            postData: { keyword: $("").val() },
        }).trigger('');
    });
}
function btn_add() {
    $.modalOpen({
        id: "",
        title: "",
        url: "",
        width: "",
        height: "",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}
function btn_edit() {
    var keyValue = $("").jqGridRowValue().sfxmbh;
    $.modalOpen({
        id: "",
        title: "",
        url: "" + keyValue,
        width: "",
        height: "",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}
function btn_delete() {
    $.deleteForm({
        url: "",
        param: { keyValue: $("").jqGridRowValue().sfxmbh },
        success: function () {
            //$.currentWindow().$("").resetSelection();
            //$.currentWindow().$("").trigger("");
            $("").trigger("");
        }
    })
}
function btn_details() {
    var keyValue = $("").jqGridRowValue().sfxmbh;
    $.modalOpen({
        id: "",
        title: "",
        url: "" + keyValue,
        width: "",
        height: "",
        btn: null,
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
            //$.currentWindow().$("").resetSelection();
            //$.currentWindow().$("").trigger("");
            $.currentWindow().window.frames[''].$("").resetSelection();
            $.currentWindow().window.frames[''].$("").trigger("");
            $.loading(false);
        }
    })
}