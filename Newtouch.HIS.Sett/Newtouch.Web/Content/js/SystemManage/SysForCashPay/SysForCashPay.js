$(function () {
    gridList();
})
function gridList() {
    var $gridList = $("#gridList");
    $gridList.dataGrid({
        treeGrid: true,
        treeGridModel: "adjacency",
        url: "/SystemManage/SysForCashPay/GetfsGridJson",
        postData: { keyword: $("#txt_keyword").val() },
        height: $(window).height() - 96,
        colModel: [
            { label: "Khóa chính", name: "xjzffsbh", hidden: true, key: true },
            { label: 'Mã', name: 'xjzffs', width: 100, align: 'left' },
            { label: 'Phương thức thanh toán tiền mặt', name: 'xjzffsmc', width: 220, align: 'left' },
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
            { label: 'Người tạo', name: 'CreatorCode', width: 100, align: 'left' },
            {
                label: 'Ngày tạo', name: 'CreateTime', width: 100, align: 'left',
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
        title: "Bảo trì cờ đặc biệt hạng mục thu phí hệ thống",
        url: "/SystemManage/SysForCashPay/Form",
        width: "500px",
        height: "300px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}
function btn_edit() {
    var keyValue = $("#gridList").jqGridRowValue().xjzffsbh;
    $.modalOpen({
        id: "Form",
        title: "Sửa thông tin",
        url: "/SystemManage/SysForCashPay/Form?keyValue=" + keyValue,
        width: "500px",
        height: "300px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}
function btn_delete() {
    $.deleteForm({
        url: "/SystemManage/SysForCashPay/DeleteForm",
        param: { keyValue: $("#gridList").jqGridRowValue().xjzffsbh },
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
        url: "/SystemManage/SysForCashPay/Form?keyValue=" + keyValue,
        width: "500px",
        height: "300px",
        btn: null,
    });
}
function submitForm() {
    if (!$('#form1').formValid()) {
        return false;
    }
    var keyValue = $("#gridList").jqGridRowValue().xjzffsbh;
    $.submitForm({
        url: "/SystemManage/SysForCashPay/SubmitForm?keyValue=" + keyValue,
        param: $("#form1").formSerialize(),
        success: function () {
            $.currentWindow().$("#gridList").resetSelection();
            $.currentWindow().$("#gridList").trigger("reloadGrid");
            $.loading(false);
        }
    })
}