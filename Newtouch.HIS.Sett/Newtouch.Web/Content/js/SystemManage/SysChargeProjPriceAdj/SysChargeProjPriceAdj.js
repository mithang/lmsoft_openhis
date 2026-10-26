$(function () {
    gridList();
});
function gridList() {
    var $gridList = $("#gridList");
    $gridList.dataGrid({
        treeGrid: true,
        treeGridModel: "adjacency",
        url: "/SystemManage/SysChargeProjPriceAdj/GetGridJson",
        height: $(window).height - 96,
        colModel: [
            { label: 'Khóa chính', name: 'tjbh', hidden: true, key: true },
            { label: 'Hạng mục thu phí', name: 'sfxm', width: 100, algin: 'left' },
            { label: 'Tên hạng mục thu phí', name: 'sfxmmc', width: 100, algin: 'left' },
            { label: 'Đơn giá', name: 'dj', width: 100, algin: 'left' },
            { label: 'Tỷ lệ tự trả', name: 'zfbl', width: 100, algin: 'left' },
            {
                label: 'Tính chất tự trả', name: 'zfxz', width: 80, align: 'left',
                formatter: function (cellvalue) {
                    if (cellvalue == "0") {
                        return "Tự trả";
                    } else if (cellvalue == "1") {
                        return "Tự lo";
                    }
                }
             },
            {
                label: 'Trạng thái', name: 'zt', width: 100, algin: 'left',
                formatter: function (cellvalue) {
                    if (cellvalue == 0) {
                        return "否";
                    } else {
                        return "是";
                    }
                }
            },
            { label: 'Cờ thực hiện', name: 'zxbz', width: 100, algin: 'left' },
            {
                label: 'Ngày thực hiện', name: 'zxrq', width: 100, align: 'left',
                formatter: "date", formatoptions: { srcformat: 'Y-m-d', newformat: 'Y-m-d' }
             },
            {
                label: 'Ngày có hiệu lực định điểm', name: 'ddsxrq', width: 100, algin: 'left',
                formatter: "date", formatoptions: { srcformat: 'Y-m-d', newformat: 'Y-m-d' }
            },
            { label: 'Cờ có hiệu lực', name: 'sxbz', width: 100, algin: 'left' },
            {
                label: 'Ngày lập hồ sơ', name: 'CreateTime', width: 100, align: 'left',
                formatter: "date", formatoptions: { srcformat: 'Y-m-d', newformat: 'Y-m-d' }
            },
           { label: 'Người lập hồ sơ', name: 'CreatorName', width: 80, align: 'left' }
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
        title: "Bảo trì điều chỉnh giá hạng mục thu phí hệ thống",
        url: "/SystemManage/SysChargeProjPriceAdj/Form",
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
        url: "/SystemManage/SysChargeProjPriceAdj/Form?keyValue=" + keyValue,
        width: "700px",
        height: "520px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}
function btn_delete() {
    $.deleteForm({
        url: "/SystemManage/SysChargeProjPriceAdj/DeleteForm",
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
        url: "/SystemManage/SysChargeProjPriceAdj/Form?keyValue=" + keyValue,
        width: "700px",
        height: "520px",
        btn: null,
    });
}