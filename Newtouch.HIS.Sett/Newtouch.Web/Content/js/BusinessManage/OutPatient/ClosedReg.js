$(function (){
    closedRegList();
})
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
            { label: '', name: '', width: 100, algin: '' },
            { label: '', name: '', hidden: true, width: 100, algin: '' },
            { label: '', name: '', width: 100, algin: '' },
            { label: '', name: '', width: 100, algin: '' },
            { label: '', name: '', width: 100, algin: '' },
            { label: '', name: '', width: 100, algin: '' },
            { label: '', name: '', width: 100, algin: '' },
            { label:'',id:'', formatter: "" }
        ]
    });

    $("").click(function () {
        //Mỗi lần nhấntabTrang，Điều chỉnhGridChiều rộng
        initLayout("");

        $closedRegList.jqGrid('', {
            postData: { kh: $("").val(), isJz: isJz },
        }).trigger('');
    });

}