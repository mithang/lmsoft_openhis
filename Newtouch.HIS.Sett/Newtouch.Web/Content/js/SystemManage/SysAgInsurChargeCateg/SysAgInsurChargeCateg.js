$(function () {
    gridList();
});
function gridList() {
    var $gridList = $("#gridList");
    $gridList.dataGrid({
        treeGrid: true,
        treeGridModel: "adjacency",
        url: "/SystemManage/SysAgInsurChargeCateg/GetsfGridJson",
        height: $(window).height - 96,
        colModel: [
            { label: 'Khóa chính', name: 'dlbh', hidden: true, key: true },
            { label: 'Mã', name: 'dl', width: 100, algin: 'left' },
            { label: 'Nhóm lớn', name: 'dlmc', width: 100, algin: 'left' },
            { label: 'Phiên âm', name: 'py', width: 100, algin: 'left' },
            {
                label: 'Phạm vi', name: 'mzzybz', width: 80, align: 'left',
                formatter: function (cellvalue) {
                    if (cellvalue == "0") {
                        return "Chung";
                    } else if (cellvalue == "1") {
                        return "Khám ngoại trú";
                    } else if (cellvalue == "2") {
                        return "Bộ phận";
                    } else if (cellvalue == "3") {
                        return "Nội trú";
                    } else {
                        return "";
                    }
                }
            },
            {
                label: 'Có hiệu lực', name: 'zt', width: 100, algin: 'left',
                formatter: function (cellvalue) {
                    if (cellvalue == 0) {
                        return "否";
                    } else {
                        return "是";
                    }
                }
            },
            { label: 'Sắp xếp', name: 'px', width: 100, algin: 'left'},
            {
                label: 'Ngày lập hồ sơ', name: 'jdrq', width: 100, align: 'left',
                formatter: "date", formatoptions: { srcformat: 'Y-m-d', newformat: 'Y-m-d' }
            },
           { label: 'Người lập hồ sơ', name: 'jdry', width: 80, align: 'left' }
        ]
    });
}

$("#btn_search").click(function () {
    $gridList.jqGrid('setGridParam', {
        postData: { keyword: $("#txt_keyword").val() }
    }).trigger('reloadGrid');
})
function btn_add() {
    $.modalOpen({
        id: "Form",
        title: "Bảo trì nhóm lớn thu phí bảo hiểm nông nghiệp hệ thống",
        url: "/SystemManage/SysAgInsurChargeCateg/Form",
        width: "700px",
        height: "520px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}

function btn_edit() {
    var keyValue = $("#gridList").jqGridRowValue().brsfjmbh;
    $.modalOpen({
        id: "Form",
        title: "Sửa thông tin",
        url: "/SystemManage/AgInsurChargeCateg/Form?keyValue=" + keyValue,
        width: "700px",
        height: "520px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}
function btn_delete() {
    $.deleteForm({
        url: "/SystemManage/AgInsurChargeCateg/DeleteForm",
        param: { keyValue: $("#gridList").jqGridRowValue().brsfjmbh },
        success: function () {
            $.currentWindow().$("#gridList").resetSelection();
            $.currentWindow().$("#gridList").trigger("reloadGrid");
        }
    })
}
function btn_details() {
    var keyValue = $("#gridList").jqGridRowValue().brsfjmbh;
    $.modalOpen({
        id: "Details",
        title: "Xem cơ sở",
        url: "/SystemManage/AgInsurChargeCateg/Form?keyValue=" + keyValue,
        width: "700px",
        height: "520px",
        btn: null,
    });
}