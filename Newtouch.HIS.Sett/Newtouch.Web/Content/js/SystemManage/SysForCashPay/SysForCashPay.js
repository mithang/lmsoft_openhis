$(function () {
    gridList();
})
function gridList() {
    var $gridList = $("");
    $gridList.dataGrid({
        treeGrid: true,
        treeGridModel: "",
        url: "",
        postData: { keyword: $("").val() },
        height: $(window).height() - 96,
        colModel: [
            { label: "", name: "", hidden: true, key: true },
            { label: '', name: '', width: 100, align: '' },
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
        ]
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
    var keyValue = $("").jqGridRowValue().xjzffsbh;
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
        param: { keyValue: $("").jqGridRowValue().xjzffsbh },
        success: function () {
            $.currentWindow().$("").resetSelection();
            $.currentWindow().$("").trigger("");
        }
    })
}
function btn_details() {
    var keyValue = $("").jqGridRowValue().id;
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
    var keyValue = $("").jqGridRowValue().xjzffsbh;
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