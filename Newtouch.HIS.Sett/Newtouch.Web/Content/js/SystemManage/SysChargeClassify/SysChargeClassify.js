$(function () {
    gridList();
})
function gridList() {
    var $gridList = $("#gridList");
    $gridList.dataGrid({
        url: "/SystemManage/SysChargeClassify/GetdlGridJson",
        height: $(window).height() - 128,
        colModel: [
            { label: "Khóa chính", name: "flbh", hidden: true, key: true },
            { label: "Mã", name: "fl", width: 80, align: 'left' },
            { label: 'Phân loại', name: 'flmc', width: 100, align: 'left' },
             { label: 'Phiên âm', name: 'py', width: 100, align: 'left' },
            {
                label: 'Có hiệu lực', name: 'zt', width: 50, align: 'left',
                formatter: function (cellvalue) {
                    if (cellvalue == 0) {
                        return "Không hiệu lực"
                    } else {
                        return "Có hiệu lực"
                    }
                }
            },
            { label: 'Người lập hồ sơ', name: 'CreatorCode', width: 50, align: 'left' },
            {
                label: 'Ngày lập hồ sơ', name: 'CreateTime', width: 50, align: 'left',
                formatter: "date", formatoptions: { srcformat: 'Y-m-d', newformat: 'Y-m-d' }
            }
        ],
        pager: "#gridPager",
        sortname: 'CreateTime desc',
        rowNum: '10',
        viewrecords: true
    });
    $("#btn_search").click(function () {
        $gridList.jqGrid('setGridParam', {
            postData: { keyword: $("#txt_keyword").val() },
        }).trigger('reloadGrid');
    });
}
function btn_add() {
    $.modalOpen({
        id: "Form",
        title: "Bảo trì thông tin phân loại hệ thống",
        url: "/SystemManage/SysChargeClassify/Form",
        width: "700px",
        height: "220px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}
function btn_edit() {
    var keyValue = $("#gridList").jqGridRowValue().flbh;
    $.modalOpen({
        id: "Form",
        title: "Sửa thông tin",
        url: "/SystemManage/SysChargeClassify/Form?keyValue=" + keyValue,
        width: "700px",
        height: "220px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}
function btn_delete() {
    $.deleteForm({
        url: "/SystemManage/SysChargeClassify/DeleteForm",
        param: { keyValue: $("#gridList").jqGridRowValue().flbh },
        success: function () {
            //$.currentWindow().$("#gridList").resetSelection();
            //$.currentWindow().$("#gridList").trigger("reloadGrid");
            $("#gridList").trigger("reloadGrid");
        }
    })
}
function btn_details() {
    var keyValue = $("#gridList").jqGridRowValue().flbh;
    $.modalOpen({
        id: "Details",
        title: "Xem cơ sở",
        url: "/SystemManage/SysChargeClassify/Form?keyValue=" + keyValue,
        width: "700px",
        height: "220px",
        btn: null,
    });
}
$("#btn_search").click(function () {
    $gridList.jqGrid('setGridParam', {
        postData: { keyword: $("#txt_keyword").val() },
    }).trigger('reloadGrid');
});