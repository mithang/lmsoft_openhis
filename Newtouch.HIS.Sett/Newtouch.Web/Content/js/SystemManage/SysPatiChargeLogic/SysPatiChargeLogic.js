$(function () {
    gridList();
});
function gridList() {
    var $gridList = $("");
    $gridList.dataGrid({
        url: "",
        ExpandColumn: "",
        postData: { keyword: $("").val(), ogrId: $("").val() },
        height: $(window).height() - 128,
        colModel: [
            { label: "", name: "", hidden: true, key: true },
            { label: "", name: "", width: 80, align: '' },
            { label: '', name: "", width: 100, align: '' },
            { label: '', name: "", width: 100, align: '' },
            {
                label: '', name: "", width: 80, align: '',
                formatter: function (cellvalue) {
                    if (cellvalue) {
                        if (cellvalue === "") {
                            return "";
                        } else if (cellvalue === "") {
                            return "";
                        } else if (cellvalue === "") {
                            return "";
                        } else if (cellvalue === "") {
                            return "";
                        } else if (cellvalue === "") {
                            return "";
                        } else {
                            return "";
                        }
                    }
                    return "";
                }
            },
            {
                label: '', name: '', width: 80, align: ''
            },
            {
                label: "", name: '', width: 130, align: '',
                formatter: function (cellvalue) {
                    if (cellvalue) {
                        if (cellvalue == "") {
                            return "";
                        } else if (cellvalue == "") {
                            return "";
                        } else if (cellvalue == "") {
                            return "";
                        } else if (cellvalue == "") {
                            return "";
                        }
                    }
                    return "";
                }
            },
            { label: '', name: '', width: 80, align: '' },
            {
                label: '', name: '', width: 80, align: '',
                formatter: function (cellvalue) {
                    if (cellvalue) {
                        if (cellvalue == "") {
                            return "";
                        } else if (cellvalue == "") {
                            return "";
                        }
                    }
                    return "";
                }
            },
            { label: '', name: '', width: 50, align: '' },
            {
                label: '', name: '', width: 50, align: '',
                formatter: function (cellvalue) {
                    if (cellvalue) {
                        if (cellvalue == 0) {
                            return "";
                        } else {
                            return "";
                        }
                    }
                    return "";
                }
            }
        ],
        pager: "",
        sortname: "",
        rowNum: "",
        viewrecords: true
    });
    $("").click(function () {
        $gridList.jqGrid("",
            {
                postData: { keyword: $("").val(), ogrId: $("").val() }
            }).trigger("");
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
    var seleRowid = jQuery("").jqGrid("", "");
    if (!(seleRowid)) {
        $.modalAlert("", "");
        return;
    }
    var keyValue = $("").jqGridRowValue().brsfsfbh;
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
    var seleRowid = jQuery("").jqGrid("", "");
    if (!(seleRowid)) {
        $.modalAlert("", "");
        return;
    }
    $.deleteForm({
        url: "",
        param: { keyValue: $("").jqGridRowValue().brsfsfbh },
        success: function() {
            $.currentWindow().$("").resetSelection();
            $.currentWindow().$("").trigger("");
        }
    });
}
function btn_details() {
    var seleRowid = jQuery("").jqGrid("", "");
    if (!(seleRowid)) {
        $.modalAlert("", "");
        return;
    }
    var keyValue = $("").jqGridRowValue().brsfsfbh;
    $.modalOpen({
        id: "",
        title: "",
        url: "" + keyValue,
        width: "",
        height: "",
        btn: null,
    });
}