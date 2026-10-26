$(function () {
    lastSettleInfoList();
})
function lastSettleInfoList() {
    var $lastSettleInfoList = $("#lastSettleInfoList");
    $lastSettleInfoList.dataGrid({
        treeGrid: true,
        treeGridModel: "adjacency",
        url: "/BusinessManage/OutPatient/GetLastSettleInfo", 
        height: 142,
        colModel: [
            { label: 'Họ tên', name: 'xm', width: 100, algin: 'left' },
            { label: 'Phải thu', name: 'ysk', width: 100, algin: 'left' },
            { label: 'Thực thu', name: 'xjzf', width: 100, algin: 'left' },
            { label: 'Trả lại tiền thừa', name: 'zl', width: 100, algin: 'left' }
        ]
    });
    $("#linkLastSettledInfo").click(function () {
        //Mỗi lần nhấntab页，Điều chỉnhGridChiều rộng
        initLayout("MyTabGrid");

        $lastSettleInfoList.jqGrid('setGridParam', {
        }).trigger('reloadGrid');
    });
}