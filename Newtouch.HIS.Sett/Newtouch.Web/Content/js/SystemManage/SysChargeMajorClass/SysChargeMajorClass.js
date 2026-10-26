$(function () {
    gridList();
})
function gridList() {
    var $gridList = $("");
    $gridList.dataGrid({
        url: "",
        height: $(window).height() - 128,
        cache: false,
        colModel: [
            { label: "", name: "", hidden: true, key: true },
            { label: "", name: "", width: 80, align: '' },
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
            },
            {
                label: '', name: '', width: 80, align: ''
            },
            {
                label: '', name: '', width: 80, align: ''
            },
            { label: '', name: '', width: 80, align: '' },
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
    var keyValue = $("").jqGridRowValue().dlId;
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
        param: { keyValue: $("").jqGridRowValue().dlId },
        success: function () {
            //$.currentWindow().$("").resetSelection();
            //$.currentWindow().$("").trigger("");
            $.currentWindow().window.frames[''].$("").trigger("");
        }
    })
}
function btn_details() {
    var keyValue = $("").jqGridRowValue().dlId;
    $.modalOpen({
        id: "",
        title: "",
        url: "" + keyValue,
        width: "",
        height: "",
        btn: null,
    });
}
$("").click(function () {
    $gridList.jqGrid('', {
        postData: { keyword: $("").val() },
    }).trigger('');
});