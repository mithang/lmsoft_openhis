$(function () {
    gridList();
})
function gridList() {
    var $gridList = $("#gridList");
    $gridList.dataGrid({
        url: "/SystemManage/SysChargeMajorClass/GetdlGridJson",
        height: $(window).height() - 128,
        cache: false,
        colModel: [
            { label: "Khóa chính", name: "dlId", hidden: true, key: true },
            { label: "Mã", name: "dlCode", width: 80, align: 'left' },
            { label: 'Nhóm lớn', name: 'dlmc', width: 100, align: 'left' },
             { label: 'Phiên âm', name: 'py', width: 100, align: 'left' },
            {
                label: 'Phạm vi', name: 'mzzybz', width: 80, align: 'left',
                formatter: function (cellvalue) {
                    if (cellvalue == "0") {
                        return "Chung";
                    } else if (cellvalue == "1") {
                        return "Khám ngoại trú";
                    } else if (cellvalue == "2") {
                        return "Nội trú";
                    } else {
                        return "";
                    }
                }
            },
            {
                label: 'Nhóm lớn báo cáo khám ngoại trú', name: 'mzprintreportcode', width: 80, align: 'left'
            },
            {
                label: 'Nhóm lớn hóa đơn khám ngoại trú', name: 'mzprintbillcode', width: 80, align: 'left'
            },
            { label: 'Sắp xếp', name: 'px', width: 80, align: 'left' },
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
        title: "Bảo trì thuật toán thu phí bệnh nhân hệ thống",
        url: "/SystemManage/SysChargeMajorClass/Form",
        width: "700px",
        height: "320px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}
function btn_edit() {
    var keyValue = $("#gridList").jqGridRowValue().dlId;
    $.modalOpen({
        id: "Form",
        title: "Sửa thông tin",
        url: "/SystemManage/SysChargeMajorClass/Form?keyValue=" + keyValue,
        width: "700px",
        height: "320px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}
function btn_delete() {
    $.deleteForm({
        url: "/SystemManage/SysChargeMajorClass/DeleteForm",
        param: { keyValue: $("#gridList").jqGridRowValue().dlId },
        success: function () {
            //$.currentWindow().$("#gridList").resetSelection();
            //$.currentWindow().$("#gridList").trigger("reloadGrid");
            $.currentWindow().window.frames['iframeExpenseManage'].$("#gridList").trigger("reloadGrid");
        }
    })
}
function btn_details() {
    var keyValue = $("#gridList").jqGridRowValue().dlId;
    $.modalOpen({
        id: "Details",
        title: "Xem cơ sở",
        url: "/SystemManage/SysChargeMajorClass/Form?keyValue=" + keyValue,
        width: "700px",
        height: "320px",
        btn: null,
    });
}
$("#btn_search").click(function () {
    $gridList.jqGrid('setGridParam', {
        postData: { keyword: $("#txt_keyword").val() },
    }).trigger('reloadGrid');
});