$(function () {
    lastSettleInfoList();
})

function lastSettleInfoList() {
    var $lastSettleInfoList = $("#lastSettleInfoList");
    $lastSettleInfoList.dataGrid({
        treeGrid: true,
        treeGridModel: "adjacency",
        url: "/OutpatientManage/OutpatientReg/GetLastSettleInfo",
        height: 142,
        colModel: [
            { label: 'Số hóa đơn', name: 'fph', width: 100, algin: 'left' },
            { label: 'Họ tên', name: 'xm', width: 100, algin: 'left' },
            { label: 'Phải thu', name: 'ysk', width: 100, algin: 'left' },
            { label: 'Thực thu', name: 'xjzf', width: 100, algin: 'left' },
            { label: 'Trả lại tiền thừa', name: 'zl', width: 100, algin: 'left' }
        ],
        loadComplete: function (data) {
            if (data && data.length && data.length > 0) {
                $("#lblqfph").text(data[0].fph);
                $("#lblzje").text(data[0].xjzf);
                $("#lblys").text(data[0].ysk);
                $("#lblzl").text(data[0].zl);
            }
        }
    });
    $("#linkLastSettledInfo").click(function () {
        //Mỗi lần nhấntab页，Điều chỉnhGridChiều rộng
        initLayout("MyTabGrid");

        $lastSettleInfoList.jqGrid('setGridParam', {
        }).trigger('reloadGrid');

    });

}