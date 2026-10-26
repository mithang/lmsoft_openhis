$(function () {
    gridList();
})
function gridList() {
    var $gridList = $("#gridList");
    $gridList.dataGrid({
        treeGrid: true,
        treeGridModel: "adjacency",
        url: "/SystemManage/SysMSItemCompared/GetdzGridJson",
        height: $(window).height() - 96,
        colModel: [
            { label: "Khóa chính", name: "id", hidden: true, key: true },
            { label: 'Khoa', name: 'ksmc', width: 100, align: 'left' },
            { label: 'Hạng mục thu phí', name: 'sfxmmc', width: 220, align: 'left' },
            {
                label: 'Có thể thu phí', name: 'ksbz', width: 50, align: 'left',
                formatter: function (cellvalue) {
                    if (cellvalue == 0) {
                        return "Chỉ cho phép xác nhận, không cho phép thu phí  "
                    } else {
                        return "Cho phép xác nhận và thu phí"
                    }
                }
            },
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
            { label: 'Người lập hồ sơ', name: 'jdry', width: 100, align: 'left' },
            {
                label: 'Ngày lập hồ sơ', name: 'jdrq', width: 100, align: 'left',
                formatter: "date", formatoptions: { srcformat: 'Y-m-d', newformat: 'Y-m-d' }
            }
        ]
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
        title: "Bảo trì đối chiếu hạng mục kỹ thuật y tế hệ thống",
        url: "/SystemManage/SysMSItemCompared/Form",
        width: "500px",
        height: "300px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}
function btn_edit() {
    var keyValue = $("#gridList").jqGridRowValue().id;
    $.modalOpen({
        id: "Form",
        title: "Sửa thông tin",
        url: "/SystemManage/SysMSItemCompared/Form?keyValue=" + keyValue,
        width: "500px",
        height: "300px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}
function btn_delete() {
    $.deleteForm({
        url: "/SystemManage/SysMSItemCompared/DeleteForm",
        param: { keyValue: $("#gridList").jqGridRowValue().id },
        success: function () {
            $.currentWindow().$("#gridList").resetSelection();
            $.currentWindow().$("#gridList").trigger("reloadGrid");
        }
    })
}
function btn_details() {
    var keyValue = $("#gridList").jqGridRowValue().id;
    $.modalOpen({
        id: "Details",
        title: "Xem cơ sở",
        url: "/SystemManage/SysMSItemCompared/Form?keyValue=" + keyValue,
        width: "500px",
        height: "300px",
        btn: null,
    });
}
function submitForm() {
    if (!$('#form1').formValid()) {
        return false;
    }
    $.submitForm({
        url: "/SystemManage/SysMSItemCompared/SubmitForm?keyValue=" + keyValue,
        param: $("#form1").formSerialize(),
        success: function () {
            $.currentWindow().$("#gridList").resetSelection();
            $.currentWindow().$("#gridList").trigger("reloadGrid");
            $.loading(false);
        }
    })
}