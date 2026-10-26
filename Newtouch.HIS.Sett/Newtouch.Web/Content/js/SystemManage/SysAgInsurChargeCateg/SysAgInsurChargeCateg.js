$(function () {
    gridList();
});
function gridList() {
    var $gridList = $("");
    $gridList.dataGrid({
        treeGrid: true,
        treeGridModel: "",
        url: "",
        height: $(window).height - 96,
        colModel: [
            { label: '', name: '', hidden: true, key: true },
            { label: '', name: '', width: 100, algin: '' },
            { label: '', name: '', width: 100, algin: '' },
            { label: '', name: '', width: 100, algin: '' },
            {
                label: '', name: '', width: 80, align: '',
                formatter: function (cellvalue) {
                    if (cellvalue == "") {
                        return "";
                    } else if (cellvalue == "") {
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
                label: '', name: '', width: 100, algin: '',
                formatter: function (cellvalue) {
                    if (cellvalue == 0) {
                        return "";
                    } else {
                        return "";
                    }
                }
            },
            { label: '', name: '', width: 100, algin: ''},
            {
                label: '', name: '', width: 100, align: '',
                formatter: "", formatoptions: { srcformat: '', newformat: '' }
            },
           { label: '', name: '', width: 80, align: '' }
        ]
    });
}

$("").click(function () {
    $gridList.jqGrid('', {
        postData: { keyword: $("").val() }
    }).trigger('');
})
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
    var keyValue = $("").jqGridRowValue().brsfjmbh;
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
        param: { keyValue: $("").jqGridRowValue().brsfjmbh },
        success: function () {
            $.currentWindow().$("").resetSelection();
            $.currentWindow().$("").trigger("");
        }
    })
}
function btn_details() {
    var keyValue = $("").jqGridRowValue().brsfjmbh;
    $.modalOpen({
        id: "",
        title: "",
        url: "" + keyValue,
        width: "",
        height: "",
        btn: null,
    });
}