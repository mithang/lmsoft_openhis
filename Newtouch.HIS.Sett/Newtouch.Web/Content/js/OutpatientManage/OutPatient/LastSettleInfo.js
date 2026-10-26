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
            { label: '', name: '', width: 100, algin: '' },
            { label: '', name: '', width: 100, algin: '' }
        ],
        loadComplete: function (data) {
            if (data && data.length && data.length > 0) {
                $("").text(data[0].fph);
                $("").text(data[0].xjzf);
                $("").text(data[0].ysk);
                $("").text(data[0].zl);
            }
        }
    });
    $("").click(function () {
        //Mỗi lần nhấntabTrang，Điều chỉnhGridChiều rộng
        initLayout("");

        $lastSettleInfoList.jqGrid('', {
        }).trigger('');

    });

}