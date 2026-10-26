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
            { label: '', name: '', width: 200, align: '' },
            { label: '', name: '', width: 220, align: '' },
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
            { label: '', name: '', width: 100, align: '' },
            {
                label: '', name: '', width: 100, align: '',
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
    var keyValue = $("").jqGridRowValue().sfxmtsbzbh;
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
        param: { keyValue: $("").jqGridRowValue().sfxmtsbzbh },
        success: function () {
            //$.currentWindow().$("").resetSelection();
            //$.currentWindow().$("").trigger("");
            $("").trigger("");
            $.loading(false);
        }
    })
}
function btn_details() {
    var keyValue = $("").jqGridRowValue().sfxmtsbzbh;
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
    $.submitForm({
        url: "" + keyValue,
        param: $("").formSerialize(),
        success: function () {
            $.currentWindow().window.frames[''].$("").resetSelection();
            $.currentWindow().window.frames[''].$("").trigger("");
            $.loading(false);
            //$.currentWindow().$("").resetSelection();
            //$.currentWindow().$("").trigger("");
        }
    })
}