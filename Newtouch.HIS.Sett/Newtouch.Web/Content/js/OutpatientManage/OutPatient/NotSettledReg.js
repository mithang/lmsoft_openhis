$(function (){
    notSettledRegList();
})

function notSettledRegList() {
    var isJz = false;
    if ($("#sel_mzlx").val() == 3)
    {
        isJz = true;    //Cấp cứu
    }

    var isJs = false;  //nghĩa là yêu cầu chưa quyết toán
    var $notSettledRegList = $("#notSettledRegList");
    $notSettledRegList.dataGrid({
        treeGrid: true,
        treeGridModel: "adjacency",
        url: "/OutpatientManage/OutpatientReg/GetSettledFeesList",
        height: 142,
        colModel: [
            { label: 'STT', name: '', algin: 'left' },
            { label: 'Mã nội bộ đăng ký khám', name: 'ghnm', hidden:true, algin: 'left' },
            { label: 'Loại đăng ký khám', name: 'sfxmmc', algin: 'left' },
            { label: 'Loại bệnh nhân', name: 'brxzmc', algin: 'left' },
            { label: 'Hạng mục bệnh nặng', name: 'dbxmmc', algin: 'left' },
            { label: 'Tổng số tiền', name: 'zje', algin: 'left' },
            { label: 'Đã hủy bỏ hay chưa', name: 'iszf', algin: 'left' }
        ]
    });

    $("#linknotSettledReg").click(function () {
        //Mỗi lần nhấntab页，Điều chỉnhGridChiều rộng
        initLayout("MyTabGrid");

        $notSettledRegList.jqGrid('setGridParam', {
            postData: { kh: $("#txtkh").val(),isJz:isJz,isJs:isJs },
        }).trigger('reloadGrid');
    });
    
}
