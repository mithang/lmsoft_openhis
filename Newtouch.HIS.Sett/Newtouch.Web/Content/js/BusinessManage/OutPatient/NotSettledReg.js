$(function (){
    notSettledRegList();
})

function notSettledRegList() {
    var isJz = false;
    if ($("").val() == 3)
    {
        isJz = true;    //Cấp cứu
    }

    var isJs = false;  //表示未结算请求
    var $notSettledRegList = $("");
    $notSettledRegList.dataGrid({
        treeGrid: true,
        treeGridModel: "",
        url: "",
        height: 142,
        colModel: [
            { label: '', name: '', algin: '' },
            { label: '', name: '', hidden:true, algin: '' },
            { label: '', name: '', algin: '' },
            { label: '', name: '', algin: '' },
            { label: '', name: '', algin: '' },
            { label: '', name: '', algin: '' },
            { label: '', name: '', algin: '' }
        ]
    });

    $("").click(function () {
        //Mỗi lần nhấntabTrang，Điều chỉnhGridChiều rộng
        initLayout("");

        $notSettledRegList.jqGrid('', {
            postData: { kh: $("").val(),isJz:isJz,isJs:isJs },
        }).trigger('');
    });
    
}
