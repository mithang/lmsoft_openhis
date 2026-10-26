$(function () {
    gridList();
})
function gridList() {
    var $gridList = $("#gridList");
    $gridList.dataGrid({
        treeGrid: true,
        treeGridModel: "adjacency",
        url: "/SystemManage/SysCISpecialMark/GetbzGridJson",
        height: $(window).height() - 128,
        colModel: [
            { label: "Khóa chính", name: "sfxmtsbzbh", hidden: true, key: true },
            { label: 'Hạng mục thu phí', name: 'sfxmmc', width: 200, align: 'left' },
            { label: 'Tính chất bệnh nhân', name: 'brxzmc', width: 220, align: 'left' },
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
            { label: 'Người lập hồ sơ', name: 'CreatorCode', width: 100, align: 'left' },
            {
                label: 'Ngày lập hồ sơ', name: 'CreateTime', width: 100, align: 'left',
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
        title: "Bảo trì cờ đặc biệt hạng mục thu phí hệ thống",
        url: "/SystemManage/SysCISpecialMark/Form",
        width: "500px",
        height: "200px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}
function btn_edit() {
    var keyValue = $("#gridList").jqGridRowValue().sfxmtsbzbh;
    $.modalOpen({
        id: "Form",
        title: "Sửa thông tin",
        url: "/SystemManage/SysCISpecialMark/Form?keyValue=" + keyValue,
        width: "500px",
        height: "200px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}
function btn_delete() {
    $.deleteForm({
        url: "/SystemManage/SysCISpecialMark/DeleteForm",
        param: { keyValue: $("#gridList").jqGridRowValue().sfxmtsbzbh },
        success: function () {
            //$.currentWindow().$("#gridList").resetSelection();
            //$.currentWindow().$("#gridList").trigger("reloadGrid");
            $("#gridList").trigger("reloadGrid");
            $.loading(false);
        }
    })
}
function btn_details() {
    var keyValue = $("#gridList").jqGridRowValue().sfxmtsbzbh;
    $.modalOpen({
        id: "Details",
        title: "Xem cơ sở",
        url: "/SystemManage/SysCISpecialMark/Form?keyValue=" + keyValue,
        width: "400px",
        height: "200px",
        btn: null,
    });
}
function submitForm() {
    $.submitForm({
        url: "/SystemManage/SysCISpecialMark/SubmitForm?keyValue=" + keyValue,
        param: $("#form1").formSerialize(),
        success: function () {
            $.currentWindow().window.frames['iframeExpenseManage'].$("#gridList").resetSelection();
            $.currentWindow().window.frames['iframeExpenseManage'].$("#gridList").trigger("reloadGrid");
            $.loading(false);
            //$.currentWindow().$("#gridList").resetSelection();
            //$.currentWindow().$("#gridList").trigger("reloadGrid");
        }
    })
}