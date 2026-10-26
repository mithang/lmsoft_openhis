$(function () {
    gridList();
})
function gridList() {
    var $gridList = $("");
    $gridList.dataGrid({
        treeGrid: true,
        treeGridModel: "",
        url: "",
        height: $(window).height() - 128,
        colModel: [
            { label: "", name: "", hidden: true, key: true },
            { label: '', name: '', width: 100, align: '' },
            { label: '', name: '', width: 100, align: '' },
            { label: '', name: '', width: 220, align: '' },
             {
                 label: '', name: '', width: 50, align: '',
                 formatter: function (cellvalue) {
                     if (cellvalue == 0) {
                         return "";
                     } else if (cellvalue == 1) {
                         return "";
                     } else if (cellvalue == 2) {
                         return "";
                     } else if (cellvalue == 3) {
                         return "";
                     }
                 }
             },
            {
                label: '', name: '', width: 50, align: '',
                formatter: function (cellvalue) {
                    if (cellvalue == 0) {
                        return "";
                    } else if (cellvalue == 1) {
                        return "";
                    } else if (cellvalue == 2) {
                        return "";
                    } else if (cellvalue == 3) {
                        return "";
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
                formatter: "", formatoptions: { srcformat: '', newformat: '' }
            }
        ],
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
    var keyValue = $("").jqGridRowValue().sfxjsnrbh;
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
        param: { keyValue: $("").jqGridRowValue().sfxjsnrbh },
        success: function () {
            //$.currentWindow().$("").resetSelection();
            //$.currentWindow().$("").trigger("");
            $("").trigger("");
        }
    })
}
function btn_details() {
    var keyValue = $("").jqGridRowValue().sfxjsnrbh;
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