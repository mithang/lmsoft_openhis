$(function() {
    closedRegList();
});
function closedRegList() {
    var isJz = false;
    if ($("").val() == 3) {
        isJz = true;    //Cấp cứu
    }
    //var isJs = true;  //表示已结算请求
    var $closedRegList = $("");
    $closedRegList.dataGrid({
        treeGrid: true,
        treeGridModel: "",
        url: "",
        height: 142,
        colModel: [
            { label: '', name: '', hidden: true, algin: '' },
            { label: '', name: '', width: 90, algin: '' },
            { label: '', name: '', width: 90, algin: '' },
            { label: '', name: '', width: 90, algin: '' },
            { label: '', name: '', width: 90, algin: '' },
            { label: '', name: '', width: 90, algin: '' },
            { label: '', name: '',width: 90, formatter: "", formatoptions: { disabled: false } }
        ],
        postData: { kh: $("").val(), isJz: isJz }
    });

    $("").click(function () {
        //Mỗi lần nhấntabTrang，Điều chỉnhGridChiều rộng
        initLayout("");

        $closedRegList.jqGrid('', {
            postData: { kh: $("").val(), isJz: isJz },
        }).trigger('');
    });

}