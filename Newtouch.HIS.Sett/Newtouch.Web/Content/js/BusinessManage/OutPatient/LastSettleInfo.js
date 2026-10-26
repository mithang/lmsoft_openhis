$(function () {
    lastSettleInfoList();
})
function lastSettleInfoList() {
    var $lastSettleInfoList = $("");
    $lastSettleInfoList.dataGrid({
        treeGrid: true,
        treeGridModel: "",
        url: "", 
        height: 142,
        colModel: [
            { label: '', name: '', width: 100, algin: '' },
            { label: '', name: '', width: 100, algin: '' },
            { label: '', name: '', width: 100, algin: '' },
            { label: '', name: '', width: 100, algin: '' }
        ]
    });
    $("").click(function () {
        //Mỗi lần nhấntabTrang，Điều chỉnhGridChiều rộng
        initLayout("");

        $lastSettleInfoList.jqGrid('', {
        }).trigger('');
    });
}